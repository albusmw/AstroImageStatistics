Option Explicit On
Option Strict On
Imports System.DirectoryServices.ActiveDirectory

'''<summary>This form should handle actions that apply to multiple files, e.g. dark statistics, hot pixel search, basic stacking, ...</summary>
Partial Public Class frmMultiFileAction

    Private Config As New cConfig
    Private WithEvents Stacker As New cStacker
    Private LogContent As New System.Text.StringBuilder
    Private WithEvents FITSGrepper As New cFITSGrepper
    '''<summary>PROI image generator.</summary>
    Private ROIImageGenerator As New cImageFromData

    '''<summary>Properties of loaded files.</summary>
    Private Class cFileProps
        '''<summary>Loaded FITS header entries.</summary>
        Public FITSHeader As Dictionary(Of eFITSKeywords, Object)
        '''<summary>Calculated statistics values.</summary>
        Public Statistics As AstroNET.Statistics.sStatistics
        '''<summary>Entered DeltaX.</summary>
        Public DeltaX As Integer = 0
        '''<summary>Entered DeltaY.</summary>
        Public DeltaY As Integer = 0
        '''<summary>Width from the FITS header info.</summary>
        Public ReadOnly Property NAXIS1() As Integer
            Get
                If IsNothing(FITSHeader) Then Return -1
                If FITSHeader.ContainsKey(eFITSKeywords.NAXIS1) = False Then Return -1
                Return CType(FITSHeader(eFITSKeywords.NAXIS1), Integer)
            End Get
        End Property
        '''<summary>Width from the FITS header info.</summary>
        Public ReadOnly Property NAXIS2() As Integer
            Get
                If IsNothing(FITSHeader) Then Return -1
                If FITSHeader.ContainsKey(eFITSKeywords.NAXIS2) = False Then Return -1
                Return CType(FITSHeader(eFITSKeywords.NAXIS2), Integer)
            End Get
        End Property
    End Class

    '''<summary>Files in the list and it's properties.</summary>
    Private AllFiles As New Dictionary(Of String, cFileProps)

    Private Sub frmMultiFileAction_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Prepare the GUI and the table
        pgMain.SelectedObject = Config
        With adgvMain
            .Columns.Add(New DataGridViewCheckBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Process"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "File"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Delta X"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Delta Y"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Mono min"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Mono max"
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        End With
    End Sub

    '''<summary>Handle the prop of files.</summary>
    '''<param name="NewFiles">Files that should be added.</param>
    Private Sub AddFiles(NewFiles() As String)
        'Handle drag-and-drop for all dropped FIT(s) files
        For Each File As String In NewFiles
            If System.IO.Path.GetExtension(File).ToUpper.StartsWith(".FIT") Then
                If AllFiles.ContainsKey(File) = False Then
                    AllFiles.Add(File, New cFileProps)
                End If
            End If
        Next File
        'Grep all headers
        FITSGrepper.Grep(NewFiles)
        'Add fits header info
        For Each GreppedFile As String In FITSGrepper.AllFileHeaders.Keys
            AllFiles(GreppedFile).FITSHeader = FITSGrepper.AllFileHeaders(GreppedFile)
        Next GreppedFile
        'Update table
        UpdateTable()
    End Sub

    '''<summary>Get all available data formats from the DragEventArgs</summary>
    '''<param name="e">Dragged content.</param>
    '''<returns>All drop content entries.</returns>
    Private Function GetDropContent(ByRef e As Windows.Forms.DragEventArgs) As Dictionary(Of String, Object)
        Dim RetVal As New Dictionary(Of String, Object)
        For Each DataContent As String In e.Data.GetFormats
            RetVal.Add(DataContent, e.Data.GetData(DataContent))
        Next DataContent
        Return RetVal
    End Function

    '==============================================================================================

    '''<summary>Get all checked files.</summary>
    Private Function GetCheckedFiles() As List(Of String)
        Dim CheckedFiles As New List(Of String)
        For Idx As Integer = 0 To adgvMain.RowCount - 1
            Dim Use As Boolean = CType(adgvMain.Rows(Idx).Cells(0).Value, Boolean)
            Dim FileName As String = CStr(adgvMain.Rows(Idx).Cells(1).Value)
            If Use Then CheckedFiles.Add(FileName)
        Next Idx
        Return CheckedFiles
    End Function

    '''<summary>Get the row for the selected file.</summary>
    Private Function GetFileRow(ByVal FileName As String) As Integer
        For Idx As Integer = 0 To adgvMain.RowCount - 1
            If FileName = CStr(adgvMain.Rows(Idx).Cells(1).Value) Then Return Idx
        Next Idx
        Return -1
    End Function

    '==============================================================================================

    Private Sub Log(ByVal Text As String)
        Log(Text, False, True)
    End Sub

    Private Sub Log(ByVal Text As List(Of String))
        Log(Text.ToArray)
    End Sub

    Private Sub Log(ByVal Indent As String, ByVal Text As String)
        Log(Indent & Text, False, False)
    End Sub

    Private Sub Log(ByVal Indent As String, ByVal Text() As String)
        For Each Line As String In Text
            Log(Indent & Line, False, False)
        Next Line
        UpdateLog()
    End Sub

    Private Sub Log(ByVal Text() As String)
        For Each Line As String In Text
            Log(Line, False, False)
        Next Line
        UpdateLog()
    End Sub

    Private Sub Log(ByVal Text As String, ByVal LogInStatus As Boolean, ByVal AutoUpdate As Boolean)
        Text = Format(Now, "HH.mm.ss:fff") & "|" & Text
        With LogContent
            If .Length = 0 Then
                .Append(Text)
            Else
                .Append(System.Environment.NewLine & Text)
            End If
            If AutoUpdate = True Then UpdateLog()
            'If LogInStatus = True Then tsslMain.Text = Text
        End With
        DE()
    End Sub

    Private Sub UpdateLog()
        With tbLog
            .Text = LogContent.ToString
            If .Text.Length > 0 Then
                .SelectionStart = .Text.Length - 1
                .SelectionLength = 0
                .ScrollToCaret()
            End If
        End With
    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub Stacker_Message(Text As String) Handles Stacker.Message
        Log(Text)
    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        Me.Close()
    End Sub

    '==============================================================================================

    '''<summary>Display internal list content in the table</summary>
    Private Sub UpdateTable()
        adgvMain.Rows.Clear()
        For Each File As String In AllFiles.Keys
            adgvMain.Rows.Add(New Object() {True, File, AllFiles(File).DeltaX.ValRegIndep, AllFiles(File).DeltaY.ValRegIndep})
        Next File
        adgvMain.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    End Sub

    Private Function GetCommonROI(ByRef ROIs As List(Of Drawing.Rectangle)) As Drawing.Rectangle
        Dim X_low As New List(Of Integer)
        Dim Y_low As New List(Of Integer)
        Dim X_high As New List(Of Integer)
        Dim Y_high As New List(Of Integer)
        For Each Entry As Drawing.Rectangle In ROIs
            X_low.Add(Entry.X)
            Y_low.Add(Entry.Y)
            X_high.Add(Entry.X + Entry.Width)
            Y_high.Add(Entry.Y + Entry.Height)
        Next Entry
        Return New Drawing.Rectangle(X_low.Max, Y_low.Max, X_high.Min - X_low.Max, Y_high.Min - Y_low.Max)
    End Function

    Private Function ShiftROI(ByVal ROI As Drawing.Rectangle, ByVal X As Integer, ByVal Y As Integer) As Drawing.Rectangle
        Dim RetVal As Drawing.Rectangle = ROI
        ROI.X -= X
        ROI.Y -= Y
        Return ROI
    End Function

    Private Sub ClearListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearListToolStripMenuItem.Click
        'Clear all items
        AllFiles = New Dictionary(Of String, cFileProps)
        FITSGrepper = New cFITSGrepper
        UpdateTable()
    End Sub

    Private Sub tsmiFile_OpenStackDir_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenStackDir.Click
        Process.Start(Config.Gen_root)
    End Sub

    Private Sub StackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiAction_Stack.Click
        Stacker.CalcTotalStatistics = Config.Stat_CalcHist
        Stacker.FileToWrite_Min = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMinFile)
        Stacker.FileToWrite_Max = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMaxFile)
        Stacker.FileToWrite_Mean = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMeanFile)
        Stacker.FileToWrite_Sigma = IO.Path.Combine(Config.Gen_root, Config.Stat_StatSigmaFile)
        Stacker.StackFITSFiles(GetCheckedFiles)
    End Sub

    Private Sub CorrelateAndStackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiAction_Run.Click

        Dim FITSReader As New cFITSReader
        Dim FilesToStack As List(Of String) = GetCheckedFiles()
        Dim FileContentAsFloat32(,) As Single = {}
        Dim RefData(,) As Single = {}
        Dim StackedData(,) As Single = {}
        Dim BaseROI As System.Drawing.Rectangle

        tbLog.BackColor = Color.Orange
        LogContent.Clear()
        DE()

        Dim FileCount As Integer = 0

        Dim UsedROIs As New List(Of Drawing.Rectangle)
        For Each FileToProcess As String In FilesToStack

            FileCount += 1
            Log("Processing file " & FileCount.ValRegIndep & "/" & FilesToStack.Count.ValRegIndep & ": " & FileToProcess)
            Dim CurrentRow As Integer = GetFileRow(FileToProcess)
            adgvMain.Rows(CurrentRow).Cells(1).Style.BackColor = Color.Red
            DE()

            'Read in the file

            Dim Container As New AstroNET.Statistics(AIS.DB.IPP)
            Container.ResetAllProcessors()
            Container.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileToProcess, True, True)

            'Calculate statistics
            If Config.Processing_CalcStatistics = True Then
                AllFiles(FileToProcess).Statistics = Container.ImageStatistics
                adgvMain.Rows(CurrentRow).Cells(4).Value = AllFiles(FileToProcess).Statistics.MonoStatistics_Int.Min.Key.ValRegIndep
                adgvMain.Rows(CurrentRow).Cells(5).Value = AllFiles(FileToProcess).Statistics.MonoStatistics_Int.Max.Key.ValRegIndep
            End If

            'Calculate alignment
            If Config.Processing_CalcAlignment = True Then

                Dim Shift As System.Drawing.Point

                'Run a Bin2 with max removal if selected (to remove hot pixel)
                If Config.Stack_RunBin2 = True Then
                    FileContentAsFloat32 = FITSReader.ReadInUInt16(FileToProcess, True, True).Bin2MaxOut32f
                Else
                    AIS.DB.IPP.Convert(FITSReader.ReadInUInt16(FileToProcess, True, True), FileContentAsFloat32)
                End If

                'For the 1st file, store it as reference with shift (0,0), for the others, run a correlation
                If FileCount = 1 Then
                    RefData = AIS.DB.IPP.Copy(FileContentAsFloat32)
                    Shift = New Drawing.Point(0, 0)
                    BaseROI = New Drawing.Rectangle(Config.Stack_ShiftMargin, Config.Stack_ShiftMargin, RefData.GetUpperBound(0) - (2 * Config.Stack_ShiftMargin), RefData.GetUpperBound(1) - (2 * Config.Stack_ShiftMargin))
                Else
                    Shift = cRegistration.MultiAreaCorrelate(RefData, FileContentAsFloat32, Config.Stack_XCorrSegmentation, Config.Stack_TlpReduction)
                End If

                'Store calculated shifts
                AllFiles(FileToProcess).DeltaX = Shift.X : adgvMain.Rows(CurrentRow).Cells(2).Value = AllFiles(FileToProcess).DeltaX
                AllFiles(FileToProcess).DeltaY = Shift.Y : adgvMain.Rows(CurrentRow).Cells(3).Value = AllFiles(FileToProcess).DeltaY

            End If

            'Calculate stacked file
            If Config.Processing_CalcStackedFile = True Then
                'Calculate the wanted ROI, get the ROI from the data and store
                Dim ThisROI As Drawing.Rectangle = ShiftROI(BaseROI, AllFiles(FileToProcess).DeltaX, AllFiles(FileToProcess).DeltaY)
                UsedROIs.Add(ThisROI)
                Dim DataToStack(,) As Single = FileContentAsFloat32.GetROI(ThisROI)
                AIS.DB.IPP.Add(DataToStack, StackedData)
            End If

            'File done
            adgvMain.Rows(CurrentRow).Cells(1).Style.BackColor = Color.White
            DE()

        Next FileToProcess

        'Store aligned files (e.g. for post-processing in a software that does not support alignment)
        If Config.Processing_StoreAlignedFiles = True Then

            Dim CutROIs As New Dictionary(Of String, Rectangle)

            '1.) We start in the center of each image at a point shifted by the given DeltaX and DeltaY
            For Each OriginalFile As String In AllFiles.Keys
                Dim CenterX As Integer = (AllFiles(OriginalFile).NAXIS1 \ 2) + AllFiles(OriginalFile).DeltaX
                Dim CenterY As Integer = (AllFiles(OriginalFile).NAXIS2 \ 2) + AllFiles(OriginalFile).DeltaY
                CutROIs.Add(OriginalFile, New Rectangle(CenterX, CenterY, 0, 0))
            Next OriginalFile

            '2.) We move left until any ROI reaches the border
            Do
                Dim NewROIs As New Dictionary(Of String, Rectangle)
                For Each FileName As String In CutROIs.Keys
                    NewROIs.Add(FileName, CutROIs(FileName).ExpandLeft(1))      'Calculate all new ROIs by expanding to the left
                    If NewROIs(FileName).Left < 0 Then Exit Do
                Next FileName
                'Copy new ROIs if they are valid
                CutROIs = NewROIs.Clone
            Loop Until 1 = 0

            '3.) We move up until any ROI reaches the border
            Do
                Dim NewROIs As New Dictionary(Of String, Rectangle)
                For Each FileName As String In CutROIs.Keys
                    NewROIs.Add(FileName, CutROIs(FileName).ExpandTop(1))      'Calculate all new ROIs by expanding to the top
                    If NewROIs(FileName).Top < 0 Then Exit Do
                Next FileName
                'Copy new ROIs if they are valid
                CutROIs = NewROIs.Clone
            Loop Until 1 = 0

            '4.) We increase the width until any ROI reaches the border
            Do
                Dim NewROIs As New Dictionary(Of String, Rectangle)
                For Each FileName As String In CutROIs.Keys
                    NewROIs.Add(FileName, CutROIs(FileName).ExpandRight(1))      'Calculate all new ROIs by expanding to the right
                    If NewROIs(FileName).Right > AllFiles(FileName).NAXIS1 - 1 Then Exit Do
                Next FileName
                'Copy new ROIs if they are valid
                CutROIs = NewROIs.Clone
            Loop Until 1 = 0

            '5.) We increase the heigth until any ROI reaches the border
            Do
                Dim NewROIs As New Dictionary(Of String, Rectangle)
                For Each FileName As String In CutROIs.Keys
                    NewROIs.Add(FileName, CutROIs(FileName).ExpandBottom(1))      'Calculate all new ROIs by expanding to the bottom
                    If NewROIs(FileName).Bottom > AllFiles(FileName).NAXIS2 - 1 Then Exit Do
                Next FileName
                'Copy new ROIs if they are valid
                CutROIs = NewROIs.Clone
            Loop Until 1 = 0

            'Last.) Generate new files
            For Each OriginalFile As String In AllFiles.Keys
                Dim FileNameOnly As String = System.IO.Path.GetFileName(OriginalFile)
                Dim NewFile As String = System.IO.Path.Combine("C:\!Astro\!DSC\NGC3372\Aligned", FileNameOnly)
                Dim FileROI As UInt16(,) = FITSReader.ReadInUInt16(OriginalFile, True, CutROIs(OriginalFile).Left, CutROIs(OriginalFile).Width, CutROIs(OriginalFile).Top, CutROIs(OriginalFile).Height, True)
                cFITSWriter.Write(NewFile, FileROI, cFITSWriter.eBitPix.Int16)
            Next OriginalFile
        End If

        'Calculate stacked file
        If Config.Processing_CalcStackedFile = True Then

            'Get the common ROI for all elements and cut this common ROI
            Dim CommonROI As Drawing.Rectangle = GetCommonROI(UsedROIs)

            'Scale the output to the maximum range
            Dim Min As Single = Single.NaN
            Dim Max As Single = Single.NaN
            AIS.DB.IPP.MinMax(StackedData, Min, Max)
            Dim Scaler As Double = UInt16.MaxValue / Max
            Dim OutputData(StackedData.GetUpperBound(0), StackedData.GetUpperBound(1)) As UInt16
            For Idx1 As Integer = 0 To OutputData.GetUpperBound(0)
                For Idx2 As Integer = 0 To OutputData.GetUpperBound(1)
                    OutputData(Idx1, Idx2) = CUShort(StackedData(Idx1, Idx2) * Scaler)
                Next Idx2
            Next Idx1

            'Write stacked file and open file
            Dim StackFile_uint16 As String = IO.Path.Combine(Config.Gen_root, Config.Gen_OutputFileUInt16)
            cFITSWriter.Write(StackFile_uint16, OutputData, cFITSWriter.eBitPix.Int16)
            Dim StackFile_float32 As String = IO.Path.Combine(Config.Gen_root, Config.Gen_OutputFilefloat32)
            cFITSWriter.Write(StackFile_float32, StackedData, cFITSWriter.eBitPix.Single)
            If Config.Stat_OpenStackedFile Then AstroImageStatistics.Ato.Utils.StartWithItsEXE(StackFile_uint16)
            If Config.Stat_OpenStackedFile Then AstroImageStatistics.Ato.Utils.StartWithItsEXE(StackFile_float32)

        End If

        tbLog.BackColor = Color.White

    End Sub

    Private Sub ClearLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearLogToolStripMenuItem.Click
        LogContent.Clear()
        UpdateLog()
    End Sub

    Private Sub AddFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddFilesToolStripMenuItem.Click
        With ofdMain
            .Filter = "FITS files (*.fit|*.fits)|*.fit?"
            .Multiselect = True
            If .ShowDialog = DialogResult.OK Then
                AddFiles(.FileNames)
            End If
        End With
    End Sub

    '==============================================================================================
    ' Interaction with the table
    '==============================================================================================

    Private Sub adgvMain_DragEnter(sender As Object, e As DragEventArgs) Handles adgvMain.DragEnter
        e.Effect = Windows.Forms.DragDropEffects.None
        Dim DropContent As Dictionary(Of String, Object) = GetDropContent(e)
        'File
        If e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop) Then
            If CType(e.Data.GetData(Windows.Forms.DataFormats.FileDrop), String()).Length >= 1 Then
                e.Effect = Windows.Forms.DragDropEffects.All
            End If
        End If
    End Sub

    Private Sub adgvMain_DragDrop(sender As Object, e As DragEventArgs) Handles adgvMain.DragDrop
        Dim DropContent As Dictionary(Of String, Object) = GetDropContent(e)
        Try
            'Drop files direct from the explorer
            If DropContent.ContainsKey("FileDrop") Then
                Dim DroppedFiles As String() = CType(DropContent("FileDrop"), String())
                AddFiles(DroppedFiles)
            End If
            'Drop text file that contains file paths
            If DropContent.ContainsKey("Text") Then
                Dim Content As New List(Of String)
                Content.Add(CStr(DropContent("Text")))
                AddFiles(Content.ToArray)
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub adgvMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles adgvMain.CellContentClick
        'Click in a certain cell
        If e.ColumnIndex = 0 Then
            'Activate or deactivate the file processing for this file
            Dim Cell As DataGridViewCheckBoxCell = CType(adgvMain.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex), DataGridViewCheckBoxCell)
            Cell.Value = Not CType(Cell.Value, Boolean)
            CalcAndDisplayCombinedROI()
        End If
    End Sub

    Private Sub adgvMain_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles adgvMain.RowEnter
        'Move focus to a certain row
        Dim SelectedFile As String = GetSelectedFileName(e)
        'Display the FITS header information
        Dim TextToDisplay As New List(Of String)
        TextToDisplay.Add("Header of file <" & SelectedFile & ">")
        TextToDisplay.AddRange(cFITSHeaderParser.GetListToDisplay(AllFiles(SelectedFile).FITSHeader))
        tbFITSHeader.Text = Join(TextToDisplay.ToArray, System.Environment.NewLine)
    End Sub

    Private Sub adgvMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles adgvMain.MouseWheel
        'Scroll mouse wheel
        If adgvMain.SelectedCells.Count = 0 Then Exit Sub
        Dim SelectedFile As String = GetSelectedFileName()
        Select Case adgvMain.SelectedCells(0).ColumnIndex
            Case 2
                'Delta X
                AllFiles(SelectedFile).DeltaX += (Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps)
                adgvMain.SelectedCells(0).Value = AllFiles(SelectedFile).DeltaX
            Case 3
                'Delta Y
                AllFiles(SelectedFile).DeltaY += (Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps)
                adgvMain.SelectedCells(0).Value = AllFiles(SelectedFile).DeltaY
        End Select
    End Sub

    Private Sub adgvMain_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles adgvMain.CellValueChanged
        If adgvMain.SelectedCells.Count = 0 Then Exit Sub
        Dim SelectedFile As String = GetSelectedFileName()
        Select Case adgvMain.SelectedCells(0).ColumnIndex
            Case 2
                'Delta X
                AllFiles(SelectedFile).DeltaX = CInt(adgvMain.SelectedCells(0).Value)
                CalcAndDisplayCombinedROI()
            Case 3
                'Delta Y
                AllFiles(SelectedFile).DeltaY = CInt(adgvMain.SelectedCells(0).Value)
                CalcAndDisplayCombinedROI()
        End Select
    End Sub

    '''<summary>Generate a combined ROI for all selected files (e.g. for manual alignment).</summary>
    Private Sub CalcAndDisplayCombinedROI()

        Dim UseIPP As Boolean = True
        Dim ForceDirect As Boolean = False

        'Read ROI's for manual alignment
        Dim Data As New AstroNET.Statistics(AIS.DB.IPP)
        Dim FITSReader As New cFITSReader

        'Sum up all images
        Dim StarImageSum_UInt16(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt16
        Dim StarImageSum_UInt32(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt32
        For Idx As Integer = 0 To adgvMain.RowCount - 1
            Dim Use As Boolean = CType(adgvMain.Rows(Idx).Cells(0).Value, Boolean)
            Dim FileName As String = CStr(adgvMain.Rows(Idx).Cells(1).Value)
            If Use Then
                Dim DeltaX As Integer = AllFiles(FileName).DeltaX
                Dim DeltaY As Integer = AllFiles(FileName).DeltaY
                Data.ResetAllProcessors()
                Data.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileName, UseIPP, Config.ROIDisplay_X + DeltaX, Config.ROIDisplay_Width, Config.ROIDisplay_Y + DeltaY, Config.ROIDisplay_Height, ForceDirect)
                If Config.ROIDisplay_MaxMode = True Then
                    AIS.DB.IPP.MaxEvery(Data.DataProcessor_UInt16.ImageData(0).Data, StarImageSum_UInt16)
                Else
                    For Idx1 As Integer = 0 To StarImageSum_UInt32.GetUpperBound(0)
                        For Idx2 As Integer = 0 To StarImageSum_UInt32.GetUpperBound(1)
                            StarImageSum_UInt32(Idx1, Idx2) += Data.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2)
                        Next Idx2
                    Next Idx1
                End If
            End If
        Next Idx

        'Get a UInt16 value again
        Dim StarImage(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt16
        If Config.ROIDisplay_MaxMode = True Then
            StarImage = StarImageSum_UInt16.CreateCopy
        Else
            Dim StarImageSum_Min As UInt32 = UInt32.MaxValue
            Dim StarImageSum_Max As UInt32 = UInt32.MinValue
            AIS.DB.IPP.MinMax(StarImageSum_UInt32, StarImageSum_Min, StarImageSum_Max)

            For Idx1 As Integer = 0 To StarImageSum_UInt32.GetUpperBound(0)
                For Idx2 As Integer = 0 To StarImageSum_UInt32.GetUpperBound(1)
                    StarImage(Idx1, Idx2) = ScaleToUInt16(StarImageSum_UInt32(Idx1, Idx2), StarImageSum_Min, StarImageSum_Max)
                Next Idx2
            Next Idx1
        End If

        Dim SumStat As New AstroNET.Statistics(AIS.DB.IPP)
        SumStat.DataProcessor_UInt16.ImageData(0).Data = StarImage
        Dim SumStatStat As AstroNET.Statistics.sStatistics = SumStat.ImageStatistics

        ROIImageGenerator.ColorMap = cColorMaps.eMaps.FalseColor
        ROIImageGenerator.GenerateDisplayImage(StarImage, CUShort(SumStatStat.MonoStatistics_Int.Min.Key), CUShort(SumStatStat.MonoStatistics_Int.Max.Key), AIS.DB.IPP)
        ROIImageGenerator.OutputImage.UnlockBits()
        pbImage.Image = ROIImageGenerator.OutputImage.BitmapToProcess

    End Sub

    Private Function ScaleToUInt16(ByVal Value As UInt32, ByVal Min As UInt32, ByVal Max As UInt32) As UInt16
        Return CType(((Value - Min) / (Max - Min)) * UInt16.MaxValue, UInt16)
    End Function

    '==============================================================================================
    ' Utility functions
    '==============================================================================================

    '''<summary>Return the selected file name in the table.</summary>
    Private Function GetSelectedFileName() As String
        Return CStr(adgvMain.Rows.Item(adgvMain.SelectedCells(0).RowIndex).Cells.Item(1).Value)
    End Function

    '''<summary>Return the selected file name in the table.</summary>
    Private Function GetSelectedFileName(e As DataGridViewCellEventArgs) As String
        Return CStr(adgvMain.Rows.Item(e.RowIndex).Cells.Item(1).Value)
    End Function

    Private Sub StoreAlignedFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StoreAlignedFilesToolStripMenuItem.Click
        With Config
            .Processing_CalcAlignment = False
            .Processing_CalcStackedFile = False
            .Processing_CalcStatistics = False
            .Processing_StoreAlignedFiles = True
        End With
    End Sub

End Class