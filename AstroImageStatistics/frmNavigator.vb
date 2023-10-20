Option Explicit On
Option Strict On

'The navigator form allows to select and display certain SAME areas in different files
'This is usefull to e.g. detect hot pixel, see alignment problems, ...

Public Class frmNavigator

    '''<summary>Settings for the navigator.</summary>
    Public Class cSettings
        <ComponentModel.Category("1. Files")>
        <ComponentModel.DisplayName("1. Files root")>
        Public Property Files_Root As String = "\\192.168.100.10\astro\2023_10_11 (NGC 7380)"
        <ComponentModel.Category("1. Files")>
        <ComponentModel.DisplayName("2. Files filter")>
        Public Property Files_Filter As String = "QHY600_B*.fit*"
        <ComponentModel.Category("2. ROI")>
        <ComponentModel.DisplayName("1. Base ROI - X")>
        Public Property ROI_X As Integer = 3000
        <ComponentModel.Category("2. ROI")>
        <ComponentModel.DisplayName("2. Base ROI - Y")>
        Public Property ROI_Y As Integer = 2000
        <ComponentModel.Category("2. ROI")>
        <ComponentModel.DisplayName("3. ROI size")>
        Public Property ROI_size As Integer = 100
        <ComponentModel.Category("3. Tile")>
        <ComponentModel.DisplayName("1. Border")>
        Public Property Tile_border As Integer = 1
        <ComponentModel.Category("4. Display")>
        <ComponentModel.DisplayName("1. Blur")>
        Public Property Display_blur As Integer = 1
    End Class

    Public WithEvents Settings As New cSettings

    '''<summary>Instalce of the IPP class to use.</summary>
    Public IPP As cIntelIPP

    Private UseIPP As Boolean = True

    '''<summary>Mosaik statistics processor.</summary>
    Private MosaikStatCalc As New AstroNET.Statistics(IPP)
    '''<summary>Mosaik statistics.</summary>
    Private MosaikStatistics As AstroNET.Statistics.sStatistics
    '''<summary>Form showing the mosaik.</summary>
    Private MosaikForm As New frmImageForm

    Private WithEvents clbDropHandler As Ato.DragDrop
    '''<summary>Flag to block updating when a selection change is running.</summary>
    Private UpdateRunning As Boolean = False



    Private Sub ErrorStatus(ByVal Text As String)
        tsslStatus.Text = Text
        tsslStatus.BackColor = Color.Red
    End Sub

    Private Sub ShowDataForm(ByRef FormToShow As frmImageForm, ByRef Data(,) As UInt16, ByVal Min As Long, ByVal Max As Long)

        Dim NewWindowRequired As Boolean = False
        If IsNothing(FormToShow) = True Then
            NewWindowRequired = True
        Else
            If FormToShow.IsDisposed = True Then NewWindowRequired = True
        End If
        If NewWindowRequired = True Then
            FormToShow = New frmImageForm
        End If
        FormToShow.Show()
        FormToShow.Text = "MosaikForm <" & Settings.Files_Root & ">"
        FormToShow.ColorMap = CType(cbColorModes.SelectedIndex, cColorMaps.eMaps)
        FormToShow.ShowData(Data, Min, Max)

    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub frmNavigator_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Dim Delta As Integer = (CInt(Settings.ROI_size) \ 2)
        Select Case e.KeyCode
            Case Keys.Up
                Settings.ROI_Y += Delta
            Case Keys.Down
                Settings.ROI_Y -= Delta
            Case Keys.Right
                Settings.ROI_X += Delta
            Case Keys.Left
                Settings.ROI_X -= Delta
        End Select
        UpdateAll()
    End Sub

    '''<summary>Update the mosaik if anything changed.</summary>
    Private Sub cbColorModes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbColorModes.SelectedIndexChanged
        UpdateAll()
    End Sub

    Private Sub lbSpecialPixel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbSpecialPixel.SelectedIndexChanged
        Dim SelectedText As String = CStr(lbSpecialPixel.SelectedItem)
        If IsNothing(SelectedText) Then Exit Sub
        Dim Splitter As String() = {":", "->", " "}
        Dim Split As String() = SelectedText.Split(Splitter, StringSplitOptions.RemoveEmptyEntries)
        If Split.Length >= 2 Then
            Settings.ROI_X = CInt(Split(0))
            Settings.ROI_Y = CInt(Split(1))
        End If
        UpdateAll()
    End Sub

    Private Sub frmNavigator_Load(sender As Object, e As EventArgs) Handles Me.Load
        cbColorModes.SelectedIndex = 2
        clbDropHandler = New Ato.DragDrop(clbFiles)
        clbDropHandler.FillControl = False
        pgParameter.SelectedObject = Settings
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
        UpdateAll()
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
        UpdateAll()
    End Sub

    Private Sub tsmiSel_UncheckAll_Click(sender As Object, e As EventArgs) Handles tsmiSel_UncheckAll.Click
        UpdateRunning = True
        For Idx As Integer = 0 To clbFiles.Items.Count - 1
            clbFiles.SetItemCheckState(Idx, CheckState.Unchecked)
        Next Idx
        UpdateRunning = False
        UpdateAll()
    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        Me.Close()
    End Sub

    Private Sub pgParameter_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgParameter.MouseWheel
        Dim Parameter As String = pgParameter.SelectedGridItem.PropertyDescriptor.Name
        Select Case Parameter
            Case "ROI_X"
                Dim Delta As Integer = (CInt(Settings.ROI_size) \ 10) * Math.Sign(e.Delta)
                Settings.ROI_X += Delta
            Case "ROI_Y"
                Dim Delta As Integer = (CInt(Settings.ROI_size) \ 10) * Math.Sign(e.Delta)
                Settings.ROI_Y += Delta
            Case "ROI_size"
                Dim Delta As Integer = 2 * Math.Sign(e.Delta)
                Settings.ROI_size += Delta
            Case "Tile_border"
                Dim Delta As Integer = 1 * Math.Sign(e.Delta)
                Settings.Tile_border += Delta
            Case "Display_blur"
                Dim Delta As Integer = 1 * Math.Sign(e.Delta)
                Settings.Display_blur += Delta
                If Settings.Display_blur < 1 Then Settings.Display_blur = 1
            Case Else
                Exit Sub
        End Select
        UpdateAll()
    End Sub

    Private Sub pgParameter_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgParameter.PropertyValueChanged
        UpdateAll()
    End Sub

    Private Sub UpdateAll()
        pgParameter.SelectedObject = Settings
        ShowMosaik()
    End Sub

    Private Sub tsmiFile_AddFiles_Click(sender As Object, e As EventArgs) Handles tsmiFile_AddFiles.Click
        'Get root folder
        Dim RootFolder As String = String.Empty
        If System.IO.File.Exists(Settings.Files_Root) Then
            RootFolder = System.IO.Path.GetDirectoryName(Settings.Files_Root)
        Else
            If System.IO.Directory.Exists(Settings.Files_Root) Then
                RootFolder = Settings.Files_Root
            End If
        End If
        If String.IsNullOrEmpty(RootFolder) Then Exit Sub
        'Add all files that match the filter criteria
        Dim AllFiles As New List(Of String)(System.IO.Directory.GetFiles(RootFolder, Settings.Files_Filter))
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

        'Plaubility check
        Try
            TileSize = Settings.ROI_size : If TileSize < 1 Then Throw New Exception("TileSize < 1")
            OffsetX = Settings.ROI_X - (TileSize \ 2) : If OffsetX < TileSize \ 2 Then Throw New Exception("ROI left edge < 1")
            OffsetY = Settings.ROI_Y - (TileSize \ 2) : If OffsetY < TileSize \ 2 Then Throw New Exception("ROI top edge < 1")
        Catch ex As Exception
            ErrorStatus(Text)
            Exit Sub
        End Try

        Dim FITSReader As New cFITSReader

        Dim MosaikWidth As Integer = CInt(Math.Ceiling(Math.Sqrt(AllFiles.Count)))              'Number of tiles in X direction
        Dim MosaikHeight As Integer = CInt(Math.Ceiling(AllFiles.Count / MosaikWidth))          'Number of tiles in Y direction
        Dim TileBorderWidth As Integer = Settings.Tile_border * (MosaikWidth - 1)
        Dim TileBorderHeight As Integer = Settings.Tile_border * (MosaikHeight - 1)

        ReDim MosaikStatCalc.DataProcessor_UInt16.ImageData(0).Data((MosaikWidth * TileSize) + TileBorderWidth - 1, (MosaikHeight * TileSize) + TileBorderHeight - 1)

        'Compose the mosaik
        Dim WidthPtr As Integer = 0 : Dim WidthIdx As Integer = 0
        Dim HeightPtr As Integer = 0
        pbMain.Maximum = AllFiles.Count
        For FileIdx As Integer = 0 To AllFiles.Count - 1
            Dim File As String = AllFiles(FileIdx)
            pbMain.Value = FileIdx : DE()
            Dim SingleFileTile(,) As UInt16 = FITSReader.ReadInUInt16(File, UseIPP, OffsetX, TileSize, OffsetY, TileSize, False)
            If Settings.Display_blur > 1 Then cOpenCvSharp.MedianBlur(SingleFileTile, Settings.Display_blur)
            Try
                For X As Integer = 0 To TileSize - 1
                    For Y As Integer = 0 To TileSize - 1
                        MosaikStatCalc.DataProcessor_UInt16.ImageData(0).Data(WidthPtr + X, HeightPtr + Y) = SingleFileTile(X, Y)
                    Next Y
                Next X
            Catch ex As Exception
                'Log this error ...
            End Try
            WidthPtr += TileSize + Settings.Tile_border : WidthIdx += 1
            If WidthIdx >= MosaikWidth Then
                HeightPtr += TileSize + Settings.Tile_border
                WidthPtr = 0
                WidthIdx = 0
            End If
        Next FileIdx

        'Run mosaik statistics
        MosaikStatistics = MosaikStatCalc.ImageStatistics()
        tbStatResult.Text = Join(MosaikStatistics.StatisticsReport(True, True).ToArray, System.Environment.NewLine)

        ShowDataForm(MosaikForm, MosaikStatCalc.DataProcessor_UInt16.ImageData(0).Data, MosaikStatistics.MonoStatistics_Int.Min.Key, MosaikStatistics.MonoStatistics_Int.Max.Key)
        pbMain.Value = 0

        tsslStatus.Text = AllFiles.Count.ToString.Trim & " files filtered and displayed."
        tsslStatus.BackColor = Color.Green

    End Sub

End Class