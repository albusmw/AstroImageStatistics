Option Explicit On
Option Strict On

'The navigator form allows to select and display certain SAME areas in different files
'This is usefull to e.g. detect hot pixel, see alignment problems, ...

Public Class frmNavigator

    '''<summary>Instalce of the IPP class to use.</summary>
    Public IPP As cIntelIPP

    Private UseIPP As Boolean = True

    '''<summary>Mosaik statistics processor.</summary>
    Private MosaikStatCalc As New AstroNET.Statistics(IPP)
    '''<summary>Mosaik statistics.</summary>
    Private MosaikStatistics As AstroNET.Statistics.sStatistics
    '''<summary>Form showing the mosaik.</summary>
    Private MosaikForm As New cImgForm

    Private WithEvents clbDropHandler As Ato.DragDrop
    '''<summary>Flag to block updating when a selection change is running.</summary>
    Private UpdateRunning As Boolean = False

    '''<summary>Main function to plot the mosaik.</summary>
    Public Sub ShowMosaik()

        'Get all files to show
        Dim AllFiles As New List(Of String)
        For Each SingleFile As String In clbFiles.CheckedItems
            If System.IO.File.Exists(SingleFile) Then AllFiles.Add(SingleFile)
        Next SingleFile
        If AllFiles.Count = 0 Then Exit Sub

        'Do not do anything as long as items are added from a queue
        If UpdateRunning Then Exit Sub


        'Read the same segment from all files and compose a new combined image
        Dim TileSize As Integer = 0                                 'size for 1 tile
        Dim OffsetX As Integer = 0                                  'X offset start position
        Dim OffsetY As Integer = 0                                  'Y offset start position
        Dim TileBorderSize As Integer = CInt(tbTileBoarder.Text)    'border between the displayed mosaik tiles

        'Plaubility check
        Try
            TileSize = CInt(tbTileSize.Text) : If TileSize < 1 Then Throw New Exception("TileSize < 1")
            OffsetX = CInt(tbOffsetX.Text) - (TileSize \ 2) : If OffsetX < TileSize \ 2 Then Throw New Exception("ROI left edge < 1")
            OffsetY = CInt(tbOffsetY.Text) - (TileSize \ 2) : If OffsetY < TileSize \ 2 Then Throw New Exception("ROI top edge < 1")
        Catch ex As Exception
            ErrorStatus(Text)
            Exit Sub
        End Try

        Dim FITSReader As New cFITSReader



        Dim MosaikWidth As Integer = CInt(Math.Ceiling(Math.Sqrt(AllFiles.Count)))              'Number of tiles in X direction
        Dim MosaikHeight As Integer = CInt(Math.Ceiling(AllFiles.Count / MosaikWidth))          'Number of tiles in Y direction
        Dim TileBorderWidth As Integer = TileBorderSize * (MosaikWidth - 1)
        Dim TileBorderHeight As Integer = TileBorderSize * (MosaikHeight - 1)

        ReDim MosaikStatCalc.DataProcessor_UInt16.ImageData(0).Data((MosaikWidth * TileSize) + TileBorderWidth - 1, (MosaikHeight * TileSize) + TileBorderHeight - 1)

        'Compose the mosaik
        Dim WidthPtr As Integer = 0 : Dim WidthIdx As Integer = 0
        Dim HeightPtr As Integer = 0
        pbMain.Maximum = AllFiles.Count
        For FileIdx As Integer = 0 To AllFiles.Count - 1
            Dim File As String = AllFiles(FileIdx)
            pbMain.Value = FileIdx : DE()
            Dim SingleFileTile(,) As UInt16 = FITSReader.ReadInUInt16(File, UseIPP, OffsetX, TileSize, OffsetY, TileSize, False)
            Dim SelectedBlur As Integer = CInt(tbBlur.Text)
            If SelectedBlur > 1 Then cOpenCvSharp.MedianBlur(SingleFileTile, SelectedBlur)
            Try
                For X As Integer = 0 To TileSize - 1
                    For Y As Integer = 0 To TileSize - 1
                        MosaikStatCalc.DataProcessor_UInt16.ImageData(0).Data(WidthPtr + X, HeightPtr + Y) = SingleFileTile(X, Y)
                    Next Y
                Next X
            Catch ex As Exception
                'Log this error ...
            End Try
            WidthPtr += TileSize + TileBorderSize : WidthIdx += 1
            If WidthIdx >= MosaikWidth Then
                HeightPtr += TileSize + TileBorderSize
                WidthPtr = 0
                WidthIdx = 0
            End If
        Next FileIdx

        'Run mosaik statistics
        MosaikStatistics = MosaikStatCalc.ImageStatistics(AstroNET.Statistics.sStatistics.eDataMode.Fixed)
        tbStatResult.Text = Join(MosaikStatistics.StatisticsReport(True, True).ToArray, System.Environment.NewLine)

        ShowDataForm(MosaikForm, MosaikStatCalc.DataProcessor_UInt16.ImageData(0).Data, MosaikStatistics.MonoStatistics_Int.Min.Key, MosaikStatistics.MonoStatistics_Int.Max.Key)
        pbMain.Value = 0

        tsslStatus.Text = AllFiles.Count.ToString.Trim & " files filtered and displayed."
        tsslStatus.BackColor = Color.Green

    End Sub

    Private Sub ErrorStatus(ByVal Text As String)
        tsslStatus.Text = Text
        tsslStatus.BackColor = Color.Red
    End Sub

    Private Sub ShowDataForm(ByRef FormToShow As cImgForm, ByRef Data(,) As UInt16, ByVal Min As Long, ByVal Max As Long)

        Dim NewWindowRequired As Boolean = False
        If IsNothing(FormToShow) = True Then
            NewWindowRequired = True
        Else
            If FormToShow.Hoster.IsDisposed = True Then NewWindowRequired = True
        End If
        If NewWindowRequired = True Then
            FormToShow = New cImgForm
        End If
        FormToShow.Show()
        FormToShow.Hoster.Text = "MosaikForm <" & tbRootFile.Text & ">"
        FormToShow.ColorMap = CType(cbColorModes.SelectedIndex, cColorMaps.eMaps)
        FormToShow.ShowData(Data, Min, Max)

    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub frmNavigator_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.KeyCode
            Case Keys.Up
                tbOffsetY.Text = CStr(CInt(tbOffsetY.Text) + (CInt(tbTileSize.Text) \ 2)).Trim
            Case Keys.Down
                tbOffsetY.Text = CStr(CInt(tbOffsetY.Text) - (CInt(tbTileSize.Text) \ 2)).Trim
            Case Keys.Right
                tbOffsetX.Text = CStr(CInt(tbOffsetX.Text) + (CInt(tbTileSize.Text) \ 2)).Trim
            Case Keys.Left
                tbOffsetX.Text = CStr(CInt(tbOffsetX.Text) - (CInt(tbTileSize.Text) \ 2)).Trim
        End Select
    End Sub

    Private Sub tbOffsetX_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbOffsetX.MouseWheel
        Select Case e.Delta
            Case Is > 0
                tbOffsetX.Text = CStr(CInt(tbOffsetX.Text) + (CInt(tbTileSize.Text) \ 10)).Trim
            Case Is < 0
                tbOffsetX.Text = CStr(CInt(tbOffsetX.Text) - (CInt(tbTileSize.Text) \ 10)).Trim
        End Select
    End Sub

    Private Sub tbOffsetY_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbOffsetY.MouseWheel
        Select Case e.Delta
            Case Is > 0
                tbOffsetY.Text = CStr(CInt(tbOffsetY.Text) + (CInt(tbTileSize.Text) \ 10)).Trim
            Case Is < 0
                tbOffsetY.Text = CStr(CInt(tbOffsetY.Text) - (CInt(tbTileSize.Text) \ 10)).Trim
        End Select
    End Sub

    Private Sub tbTileSize_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbTileSize.MouseWheel
        Select Case e.Delta
            Case Is > 0
                tbTileSize.Text = CStr(CInt(tbTileSize.Text) - 10).Trim
            Case Is < 0
                tbTileSize.Text = CStr(CInt(tbTileSize.Text) + 10).Trim
        End Select
    End Sub

    '''<summary>Update the mosaik if anything changed.</summary>
    Private Sub AnythingChanged(sender As Object, e As EventArgs) Handles tbOffsetX.TextChanged, tbOffsetY.TextChanged, tbTileSize.TextChanged, tbFilterString.TextChanged, cbColorModes.SelectedIndexChanged
        ShowMosaik()
    End Sub

    Private Sub lbSpecialPixel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbSpecialPixel.SelectedIndexChanged
        Dim SelectedText As String = CStr(lbSpecialPixel.SelectedItem)
        If IsNothing(SelectedText) Then Exit Sub
        Dim Split As String() = SelectedText.Split(":"c)
        If Split.Length >= 2 Then
            tbOffsetX.Text = Split(0)
            tbOffsetY.Text = Split(1)
        End If
    End Sub

    Private Sub frmNavigator_Load(sender As Object, e As EventArgs) Handles Me.Load
        cbColorModes.SelectedIndex = 2
        clbDropHandler = New Ato.DragDrop(clbFiles)
        clbDropHandler.FillList = False
    End Sub

    Private Sub clbDropHandler_DropOccured(Files() As String) Handles clbDropHandler.DropOccured
        'Add all files from a drop event
        UpdateRunning = True
        For Each SingleFile As String In Files
            If clbFiles.Items.Contains(SingleFile) = False Then
                clbFiles.Items.Add(SingleFile, True)
            End If
        Next SingleFile
        UpdateRunning = False
        ShowMosaik()
    End Sub

    Private Sub clbFiles_SelectedIndexChanged(sender As Object, e As EventArgs)
        tbSelected.Text = clbFiles.SelectedItem.ToString
    End Sub

    Private Sub bntAddRange_Click(sender As Object, e As EventArgs) Handles bntAddRange.Click
        'Get root folder
        Dim RootFolder As String = String.Empty
        If System.IO.File.Exists(tbRootFile.Text) Then
            RootFolder = System.IO.Path.GetDirectoryName(tbRootFile.Text)
        Else
            If System.IO.Directory.Exists(tbRootFile.Text) Then
                RootFolder = tbRootFile.Text
            End If
        End If
        If String.IsNullOrEmpty(RootFolder) Then Exit Sub
        'Add all files that match the filter criteria
        Dim AllFiles As New List(Of String)(System.IO.Directory.GetFiles(RootFolder, tbFilterString.Text))
        If AllFiles.Count = 0 Then Exit Sub
        UpdateRunning = True
        For Each SingleFile As String In AllFiles
            If clbFiles.Items.Contains(SingleFile) = False Then
                clbFiles.Items.Add(SingleFile, True)
            End If
        Next SingleFile
        UpdateRunning = False
        ShowMosaik()
    End Sub

    Private Sub tsmiFile_SaveMosaik_Click(sender As Object, e As EventArgs) Handles tsmiFile_SaveMosaik.Click
        With sfdMain
            .Filter = "FITS (*.fits)|*.fits"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With
        cFITSWriter.Write(sfdMain.FileName, MosaikStatCalc.DataProcessor_UInt16.ImageData(0).Data, cFITSWriter.eBitPix.Int16)
        Process.Start(sfdMain.FileName)
    End Sub

    Private Sub tsmiSel_DeleteAll_Click(sender As Object, e As EventArgs) Handles tsmiSel_DeleteAll.Click
        clbFiles.Items.Clear()
    End Sub

    Private Sub tsmi_CheckAll_Click(sender As Object, e As EventArgs) Handles tsmi_CheckAll.Click
        UpdateRunning = True
        For Idx As Integer = 0 To clbFiles.Items.Count - 1
            clbFiles.SetItemCheckState(Idx, CheckState.Checked)
        Next Idx
        UpdateRunning = False
    End Sub

    Private Sub tsmiSel_UncheckAll_Click(sender As Object, e As EventArgs) Handles tsmiSel_UncheckAll.Click
        UpdateRunning = True
        For Idx As Integer = 0 To clbFiles.Items.Count - 1
            clbFiles.SetItemCheckState(Idx, CheckState.Unchecked)
        Next Idx
        UpdateRunning = False
    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        Me.Close()
    End Sub

End Class