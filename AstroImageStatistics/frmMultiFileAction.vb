Option Explicit On
Option Strict On

'''<summary>This form should handle actions that apply to multiple files, e.g. dark statistics, hot pixel search, basic stacking, ...</summary>
Partial Public Class frmMultiFileAction

    '''<summary>Available columns.</summary>
    Private Enum eColumns
        Process
        File
        DateTime
        NAxis1
        NAxis2
        DeltaX
        DeltaY
        MonoMin
        MonoMax
        MonoMean
        MonoMedian
    End Enum

    '''<summary>Files in the list and it's properties.</summary>
    Private AllListFiles As New Dictionary(Of String, cFileProps)

    Private Config As New cConfig
    Private WithEvents Stacker As New cStacker
    Private LogContent As New System.Text.StringBuilder
    Private WithEvents FITSGrepper As New cFITSGrepper
    '''<summary>PROI image generator.</summary>
    Private ROIImageGenerator As New cImageFromData

    Private Class c2D(Of T)
        Public Matrix(,) As T = {}
        Public Sub New(ByRef Data(,) As T)
            Matrix = Data.CreateCopy
        End Sub
    End Class

    Private Sub frmMultiFileAction_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Prepare the GUI and the table
        pgMain.SelectedObject = Config
        With adgvMain
            .Columns.Add(New DataGridViewCheckBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Process"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "File" : .Columns(.ColumnCount - 1).ReadOnly = True
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "DateTime" : .Columns(.ColumnCount - 1).ReadOnly = True
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "NAXIS1" : .Columns(.ColumnCount - 1).ReadOnly = True
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "NAXIS2" : .Columns(.ColumnCount - 1).ReadOnly = True
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Delta X"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Delta Y"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Mono min" : .Columns(.ColumnCount - 1).ReadOnly = True
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Mono max" : .Columns(.ColumnCount - 1).ReadOnly = True
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Mono mean" : .Columns(.ColumnCount - 1).ReadOnly = True
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(.ColumnCount - 1).HeaderText = "Mono median" : .Columns(.ColumnCount - 1).ReadOnly = True
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        End With
    End Sub

    '''<summary>Add files to the list.</summary>
    '''<param name="NewFiles">Files that should be added.</param>
    Private Sub AddFiles(NewFiles As IEnumerable(Of String))
        'Handle drag-and-drop for all dropped FIT(s) files
        For Each File As String In NewFiles
            If System.IO.Path.GetExtension(File).ToUpper.StartsWith(".FIT") Then
                If AllListFiles.ContainsKey(File) = False Then
                    AllListFiles.Add(File, New cFileProps)
                    Using BaseIn As New System.IO.StreamReader(File)
                        Dim FITSHeaderParser As New cFITSHeaderParser(cFITSReader.ReadHeader(BaseIn, AllListFiles(File).DataStartPosition))
                    End Using
                End If
            End If
        Next File
        'Grep all headers
        FITSGrepper.Grep(NewFiles)
        'Add fits header info
        For Each GreppedFile As String In FITSGrepper.AllFileHeaders.Keys
            AllListFiles(GreppedFile).FITSHeader = FITSGrepper.AllFileHeaders(GreppedFile)
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

    '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
    ' Logging
    '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

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
        For Each File As String In AllListFiles.Keys
            adgvMain.Rows.Add(New Object() {True, File, AllListFiles(File).DateTime, AllListFiles(File).NAXIS1, AllListFiles(File).NAXIS2, AllListFiles(File).DeltaX.ValRegIndep, AllListFiles(File).DeltaY.ValRegIndep})
        Next File
        adgvMain.Columns(eColumns.File).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    End Sub

    Private Function ShiftROI(ByVal ROI As Drawing.Rectangle, ByVal X As Integer, ByVal Y As Integer) As Drawing.Rectangle
        Dim RetVal As Drawing.Rectangle = ROI
        ROI.X -= X
        ROI.Y -= Y
        Return ROI
    End Function

    Private Sub tsmiFileList_ClearList_Click(sender As Object, e As EventArgs) Handles tsmiFileList_ClearList.Click
        'Clear all items
        AllListFiles = New Dictionary(Of String, cFileProps)
        FITSGrepper = New cFITSGrepper
        UpdateTable()
    End Sub

    Private Sub tsmiFile_OpenWorkingDir_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenWorkingDir.Click
        Process.Start(Config.Gen_root)
    End Sub

    Private Sub tsmiAction_Stack_Click(sender As Object, e As EventArgs) Handles tsmiAction_StackSpecial.Click
        Stacker.CalcTotalStatistics = Config.Stat_CalcHist
        Stacker.FileToWrite_Min = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMinFile)
        Stacker.FileToWrite_Max = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMaxFile)
        Stacker.FileToWrite_Mean = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMeanFile)
        Stacker.FileToWrite_Sigma = IO.Path.Combine(Config.Gen_root, Config.Stat_StatSigmaFile)
        Stacker.StackFITSFiles(GetCheckedFiles)
    End Sub

    Private Sub tsmiAction_Run_Click(sender As Object, e As EventArgs) Handles tsmiAction_Run.Click

        Dim AllCheckedFiles As List(Of String) = GetCheckedFiles()
        Dim FITSReader As New cFITSReader
        Dim CorrRefData(,) As Single = {}

        Dim Stacked_UInt32(,) As UInt32 = {}
        Dim SigmaClipROIs(AllCheckedFiles.Count - 1) As c2D(Of UInt16)

        'Prepare

        tbLog.BackColor = Color.Orange
        LogContent.Clear()
        DE()

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Calculate statistics and alignment (needs to read the complete file)
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        If (Config.Processing_CalcAlignment = True) Or (Config.Processing_CalcStatistics = True) Then

            Dim FileCount As Integer = 0
            tspbMain.Maximum = AllCheckedFiles.Count
            tspbMain.Value = 0
            For Each FileToProcess As String In AllCheckedFiles

                'Start with this file
                FileCount += 1
                Dim CurrentRow As Integer = GetFileRow(FileToProcess)
                MarkFile(FileToProcess, Color.Red)
                tspbMain.Value = FileCount
                DE()

                'Read in the file
                Dim Container As New AstroNET.Statistics(AIS.DB.IPP)
                Container.ResetAllProcessors()
                Container.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileToProcess, True, True)

                'Calculate statistics
                If Config.Processing_CalcStatistics = True Then
                    AllListFiles(FileToProcess).Statistics = Container.ImageStatistics
                    adgvMain.Rows(CurrentRow).Cells(eColumns.MonoMin).Value = AllListFiles(FileToProcess).Statistics.MonoStatistics_Int.Min.Key.ValRegIndep
                    adgvMain.Rows(CurrentRow).Cells(eColumns.MonoMax).Value = AllListFiles(FileToProcess).Statistics.MonoStatistics_Int.Max.Key.ValRegIndep
                    adgvMain.Rows(CurrentRow).Cells(eColumns.MonoMean).Value = AllListFiles(FileToProcess).Statistics.MonoStatistics_Int.Mean.ValRegIndep
                    adgvMain.Rows(CurrentRow).Cells(eColumns.MonoMedian).Value = AllListFiles(FileToProcess).Statistics.MonoStatistics_Int.Median.ValRegIndep
                End If

                'Calculate alignment
                If Config.Processing_CalcAlignment = True Then

                    Dim FileContentAsFloat32(,) As Single = {}
                    Dim Shift As System.Drawing.Point

                    'Run a Bin2 with max removal if selected (to remove hot pixel)
                    If Config.Stack_RunBin2 = True Then
                        FileContentAsFloat32 = Container.DataProcessor_UInt16.ImageData(0).Data.Bin2MaxOut32f
                    Else
                        AIS.DB.IPP.Convert(Container.DataProcessor_UInt16.ImageData(0).Data, FileContentAsFloat32)
                    End If

                    'For the 1st file, store it as reference with shift (0,0), for the others, run a correlation
                    If FileCount = 1 Then
                        CorrRefData = AIS.DB.IPP.Copy(FileContentAsFloat32)
                        Shift = New Drawing.Point(0, 0)
                    Else
                        Shift = cRegistration.MultiAreaCorrelate(CorrRefData, FileContentAsFloat32, Config.Stack_XCorrSegmentation, Config.Stack_TlpReduction)
                    End If

                    'Correct shift if binning was selected and sign
                    If Config.Stack_RunBin2 Then
                        Shift = New Drawing.Point(-Shift.X * 2, -Shift.Y * 2)
                    Else
                        Shift = New Drawing.Point(-Shift.X, -Shift.Y)
                    End If

                    'Store calculated shifts
                    AllListFiles(FileToProcess).DeltaX = Shift.X : adgvMain.Rows(CurrentRow).Cells(eColumns.DeltaX).Value = AllListFiles(FileToProcess).DeltaX
                    AllListFiles(FileToProcess).DeltaY = Shift.Y : adgvMain.Rows(CurrentRow).Cells(eColumns.DeltaY).Value = AllListFiles(FileToProcess).DeltaY

                End If

                'File done
                MarkFile(FileToProcess, Color.White)
                DE()

            Next FileToProcess
            tspbMain.Value = 0

        End If

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Cut certain ROI's (for stacking or separate storage)
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        If (Config.Processing_StoreAlignedFiles = True) Or (Config.Processing_CalcStackedFile) Or (Config.Processing_CalcSigmaClip) Then

            Dim CutROIs As New Dictionary(Of String, Rectangle)

            '1.) We start in the center of each image at a point shifted by the given DeltaX and DeltaY
            For Each OriginalFile As String In AllCheckedFiles
                Dim CenterX As Integer = (AllListFiles(OriginalFile).NAXIS1 \ 2) + AllListFiles(OriginalFile).DeltaX
                Dim CenterY As Integer = (AllListFiles(OriginalFile).NAXIS2 \ 2) + AllListFiles(OriginalFile).DeltaY
                CutROIs.Add(OriginalFile, New Rectangle(CenterX, CenterY, 0, 0))
            Next OriginalFile

            '2.) We move left until any ROI reaches the border
            Do
                Dim NewROIs As New Dictionary(Of String, Rectangle)
                For Each FileName As String In CutROIs.Keys
                    NewROIs.Add(FileName, CutROIs(FileName).ExpandLeft(1))      'Calculate all new ROIs by expanding to the left
                    If NewROIs(FileName).Left < 0 Then Exit Do
                    If NewROIs(FileName).Left < Config.CutLimit_Left Then Exit Do
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
                    If NewROIs(FileName).Top < Config.CutLimit_Top Then Exit Do
                Next FileName
                'Copy new ROIs if they are valid
                CutROIs = NewROIs.Clone
            Loop Until 1 = 0

            '4.) We increase the width until any ROI reaches the border
            Do
                Dim NewROIs As New Dictionary(Of String, Rectangle)
                For Each FileName As String In CutROIs.Keys
                    NewROIs.Add(FileName, CutROIs(FileName).ExpandRight(1))      'Calculate all new ROIs by expanding to the right
                    If NewROIs(FileName).Right > AllListFiles(FileName).NAXIS1 Then Exit Do
                    If NewROIs(FileName).Right > Config.CutLimit_Right Then Exit Do
                Next FileName
                'Copy new ROIs if they are valid
                CutROIs = NewROIs.Clone
            Loop Until 1 = 0

            '5.) We increase the heigth until any ROI reaches the border
            Do
                Dim NewROIs As New Dictionary(Of String, Rectangle)
                For Each FileName As String In CutROIs.Keys
                    NewROIs.Add(FileName, CutROIs(FileName).ExpandBottom(1))      'Calculate all new ROIs by expanding to the bottom
                    If NewROIs(FileName).Bottom > AllListFiles(FileName).NAXIS2 Then Exit Do
                    If NewROIs(FileName).Bottom > Config.CutLimit_Bottom Then Exit Do
                Next FileName
                'Copy new ROIs if they are valid
                CutROIs = NewROIs.Clone
            Loop Until 1 = 0

            'Last.) Generate new files
            Dim FileIdx As Integer = -1
            Dim AlignFolder As String = String.Empty
            For Each FileToProcess As String In AllCheckedFiles
                FileIdx += 1
                MarkFile(FileToProcess, Color.Red)
                DE()
                'Load defined ROI
                Dim FileROI As UInt16(,) = FITSReader.ReadInUInt16(FileToProcess, True, CutROIs(FileToProcess), True)
                'Stack file (if requested)
                If Config.Processing_CalcStackedFile = True Then
                    Dim DataAsUInt32(,) As UInt32 = {} : AIS.DB.IPP.Convert(FileROI, DataAsUInt32)
                    AIS.DB.IPP.Add(DataAsUInt32, Stacked_UInt32)
                End If
                'Store data if a sigma clipping should be applied
                If Config.Processing_CalcSigmaClip = True Then
                    SigmaClipROIs(FileIdx) = New c2D(Of UShort)(FileROI)
                End If
                'Generate folder
                If FileIdx = 0 Then
                    AlignFolder = System.IO.Path.Combine(Config.Gen_root, System.IO.Path.GetFileNameWithoutExtension(FileToProcess) & "_aligned")
                    If System.IO.Directory.Exists(AlignFolder) = False Then System.IO.Directory.CreateDirectory(AlignFolder)
                End If
                'Store aligned file (if selected)
                If Config.Processing_StoreAlignedFiles = True Then
                    Dim NewFile As String = System.IO.Path.Combine(AlignFolder, System.IO.Path.GetFileName(FileToProcess))
                    cFITSWriter.Write(NewFile, FileROI, cFITSWriter.eBitPix.Int16)
                End If
                MarkFile(FileToProcess, Color.Green)
            Next FileToProcess

        End If

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Stack with sigma clipping and store if requested
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        If Config.Processing_CalcSigmaClip = True Then

            Dim NAXIS1 As Integer = SigmaClipROIs(0).Matrix.GetUpperBound(0) - 1
            Dim NAXIS2 As Integer = SigmaClipROIs(0).Matrix.GetUpperBound(1) - 1
            Dim Calculated(NAXIS1 - 1, NAXIS2 - 1) As Double
            Dim SigmaClipped As New AstroDSP.cSigmaClipped(Config.Stack_SigClip_LowBound, Config.Stack_SigClip_HighBound)
            For Idx1 As Integer = 0 To Calculated.GetUpperBound(0)
                For Idx2 As Integer = 0 To Calculated.GetUpperBound(1)
                    Dim IgnoredPixel As Integer = 0
                    Dim Samples(SigmaClipROIs.GetUpperBound(0) - 1) As UInt16
                    For FileIdx As Integer = 0 To SigmaClipROIs.GetUpperBound(0) - 1
                        Samples(FileIdx) = SigmaClipROIs(FileIdx).Matrix(Idx1, Idx2)
                    Next FileIdx
                    Calculated(Idx1, Idx2) = SigmaClipped.SigmaClipped_mean(Samples, IgnoredPixel)
                Next Idx2
            Next Idx1
            Dim StackFileName_SigmaClip As String = IO.Path.Combine(Config.Gen_root, Config.Gen_OutputFileSigmaClip)
            cFITSWriter.Write(StackFileName_SigmaClip, Calculated, cFITSWriter.eBitPix.Single)
            If Config.Stat_OpenStackedFile Then AstroImageStatistics.Ato.Utils.StartWithItsEXE(StackFileName_SigmaClip)

        End If

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Store the stacked file
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        If Config.Processing_CalcStackedFile = True Then

            'Get the common ROI for all elements and cut this common ROI
            'Dim CommonROI As Drawing.Rectangle = GetCommonROI(UsedROIs)

            'Scale the output to the maximum range
            Dim Min As UInt32 = UInt32.MaxValue
            Dim Max As UInt32 = UInt32.MinValue
            AIS.DB.IPP.MinMax(Stacked_UInt32, Min, Max)
            Dim Scaler_UInt16 As Double = UInt16.MaxValue / Max
            Dim Scaler_Single As Double = 1 / Max
            Dim OutputData_UInt16(Stacked_UInt32.GetUpperBound(0), Stacked_UInt32.GetUpperBound(1)) As UInt16
            Dim OutputData_Float32(Stacked_UInt32.GetUpperBound(0), Stacked_UInt32.GetUpperBound(1)) As Single
            For Idx1 As Integer = 0 To OutputData_UInt16.GetUpperBound(0)
                For Idx2 As Integer = 0 To OutputData_UInt16.GetUpperBound(1)
                    OutputData_UInt16(Idx1, Idx2) = CType(Stacked_UInt32(Idx1, Idx2) * Scaler_UInt16, UInt16)
                    OutputData_Float32(Idx1, Idx2) = CType(Stacked_UInt32(Idx1, Idx2) * Scaler_Single, Single)
                Next Idx2
            Next Idx1

            'Write stacked file - UInt 16 scaled
            Dim StackFileName_UInt16 As String = IO.Path.Combine(Config.Gen_root, Config.Gen_OutputFileUInt16)
            cFITSWriter.Write(StackFileName_UInt16, OutputData_UInt16, cFITSWriter.eBitPix.Int16)

            'Write stacked file - float
            Dim StackFileName_float32 As String = IO.Path.Combine(Config.Gen_root, Config.Gen_OutputFilefloat32)
            cFITSWriter.Write(StackFileName_float32, OutputData_Float32, cFITSWriter.eBitPix.Single)

            'Open file(s)
            If Config.Stat_OpenStackedFile Then AstroImageStatistics.Ato.Utils.StartWithItsEXE(StackFileName_UInt16)
            If Config.Stat_OpenStackedFile Then AstroImageStatistics.Ato.Utils.StartWithItsEXE(StackFileName_float32)

        End If

        tbLog.BackColor = Color.White

    End Sub

    Private Sub pgMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgMain.MouseWheel
        'Scroll in the property grid
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "CutLimit_Left"
                Config.CutLimit_Left += Math.Sign(e.Delta) * 5
            Case "CutLimit_Right"
                Config.CutLimit_Right += Math.Sign(e.Delta) * 5
            Case "ROIDisplay_X"
                Config.ROIDisplay_X += Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps
                CalcAndDisplayCombinedROI()
            Case "ROIDisplay_Y"
                Config.ROIDisplay_Y += Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps
                CalcAndDisplayCombinedROI()
            Case "ROIDisplay_Width"
                Config.ROIDisplay_Width += Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps
                CalcAndDisplayCombinedROI()
            Case "ROIDisplay_Height"
                Config.ROIDisplay_Height += Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps
                CalcAndDisplayCombinedROI()
            Case "Stat_SinglePixelX"
                Config.Stat_SinglePixelX += Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps
                If Config.Stat_SinglePixelX < 0 Then Config.Stat_SinglePixelX = 0
                RunSinglePixelStat(New Point(Config.Stat_SinglePixelX, Config.Stat_SinglePixelY))
            Case "Stat_SinglePixelY"
                Config.Stat_SinglePixelY += Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps
                If Config.Stat_SinglePixelY < 0 Then Config.Stat_SinglePixelY = 0
                RunSinglePixelStat(New Point(Config.Stat_SinglePixelX, Config.Stat_SinglePixelY))
        End Select
        pgMain.SelectedObject = Config
    End Sub

    Private Sub ClearLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiAction_ClearLog.Click
        LogContent.Clear()
        UpdateLog()
    End Sub

    Private Sub AddFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiFile_AddFiles.Click
        With ofdMain
            .Filter = "FITS files (*.fit?)|*.fit?"
            .Multiselect = True
            If .ShowDialog = DialogResult.OK Then
                Dim SortedList As New List(Of String)(.FileNames)
                SortedList.Sort()
                AddFiles(SortedList)
            End If
        End With
    End Sub

    '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
    ' Interaction with the table
    '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

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
                Dim DroppedFiles As New List(Of String) : DroppedFiles.AddRange(CType(DropContent("FileDrop"), String()))
                DroppedFiles.Sort()
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
        If Config.Processing_DisplayFITSHeader Then
            Dim TextToDisplay As New List(Of String)
            TextToDisplay.Add("FITS header of file <" & SelectedFile & ">")
            TextToDisplay.AddRange(cFITSHeaderParser.GetListToDisplay(AllListFiles(SelectedFile).FITSHeader))
            tbFITSHeader.Text = Join(TextToDisplay.ToArray, System.Environment.NewLine)
        End If
        'Display ROI
        CalcAndDisplayCombinedROI()
    End Sub

    Private Sub adgvMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles adgvMain.MouseWheel
        'Scroll mouse wheel
        If adgvMain.SelectedCells.Count = 0 Then Exit Sub
        Dim SelectedFile As String = GetSelectedFileName()
        Select Case adgvMain.SelectedCells(0).ColumnIndex
            Case eColumns.DeltaX
                'Delta X
                AllListFiles(SelectedFile).DeltaX += (Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps)
                adgvMain.SelectedCells(0).Value = AllListFiles(SelectedFile).DeltaX
            Case eColumns.DeltaY
                'Delta Y
                AllListFiles(SelectedFile).DeltaY += (Math.Sign(e.Delta) * Config.Stack_ROIDisplay_MouseWheelSteps)
                adgvMain.SelectedCells(0).Value = AllListFiles(SelectedFile).DeltaY
        End Select
    End Sub

    Private Sub adgvMain_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles adgvMain.CellValueChanged
        If adgvMain.SelectedCells.Count = 0 Then Exit Sub
        Dim SelectedFile As String = GetSelectedFileName()
        Select Case adgvMain.SelectedCells(0).ColumnIndex
            Case eColumns.DeltaX
                'Delta X
                AllListFiles(SelectedFile).DeltaX = CInt(adgvMain.SelectedCells(0).Value)
                CalcAndDisplayCombinedROI()
            Case eColumns.DeltaY
                'Delta Y
                AllListFiles(SelectedFile).DeltaY = CInt(adgvMain.SelectedCells(0).Value)
                CalcAndDisplayCombinedROI()
        End Select
    End Sub

    Private Sub tsmiAction_Mode_StoreAlignedFiles_Click(sender As Object, e As EventArgs) Handles tsmiAction_Mode_StoreAlignedFiles.Click
        With Config
            .Processing_CalcAlignment = False
            .Processing_CalcStackedFile = False
            .Processing_CalcStatistics = False
            .Processing_StoreAlignedFiles = True
        End With
    End Sub

    '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
    ' Functions with no form handle
    '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

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

    '''<summary>Generate a combined ROI for all selected files (e.g. for manual alignment).</summary>
    Private Sub CalcAndDisplayCombinedROI()

        If Config.ROIDisplay_Mode = cConfig.eROIDisplay.Off Then Exit Sub

        Dim UseIPP As Boolean = True
        Dim ForceDirect As Boolean = False

        'Read ROI's for manual alignment
        Dim Data As New AstroNET.Statistics(AIS.DB.IPP)
        Dim FITSReader As New cFITSReader

        'Get all files or only the selected file
        Dim FilesToLoad As New List(Of String)
        If Config.ROIDisplay_Mode = cConfig.eROIDisplay.Single Then
            'Display only active file
            FilesToLoad.Add(GetSelectedFileName)
        Else
            'Display all checked files
            FilesToLoad.AddRange(GetCheckedFiles)
        End If

        'Sum up all images
        Dim StarImageSum_UInt16(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt16
        Dim StarImageSum_UInt32(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt32
        If Config.ROIDisplay_Mode = cConfig.eROIDisplay.Mosaik Then
            StarImageSum_UInt16 = CalculateMosaik(FilesToLoad)
        Else
            For Each File As String In FilesToLoad
                Dim DeltaX As Integer = 0 : If Config.ROIDisplay_UseDeltaXY Then DeltaX = AllListFiles(File).DeltaX
                Dim DeltaY As Integer = 0 : If Config.ROIDisplay_UseDeltaXY Then DeltaY = AllListFiles(File).DeltaY
                Data.ResetAllProcessors()
                Dim ROI As New Rectangle(Config.ROIDisplay_X + DeltaX, Config.ROIDisplay_Y + DeltaY, Config.ROIDisplay_Width, Config.ROIDisplay_Height)
                Data.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(File, UseIPP, ROI, ForceDirect)
                Select Case Config.ROIDisplay_Mode
                    Case cConfig.eROIDisplay.Single
                        StarImageSum_UInt16 = Data.DataProcessor_UInt16.ImageData(0).Data
                    Case cConfig.eROIDisplay.Stacked_max
                        AIS.DB.IPP.MaxEvery(Data.DataProcessor_UInt16.ImageData(0).Data, StarImageSum_UInt16)
                    Case cConfig.eROIDisplay.Stacked_mean
                        For Idx1 As Integer = 0 To StarImageSum_UInt32.GetUpperBound(0)
                            For Idx2 As Integer = 0 To StarImageSum_UInt32.GetUpperBound(1)
                                StarImageSum_UInt32(Idx1, Idx2) += Data.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2)
                            Next Idx2
                        Next Idx1
                End Select
            Next File
        End If

        'Get a UInt16 value again
        Dim ImageToDisplay(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt16
        Select Case Config.ROIDisplay_Mode
            Case cConfig.eROIDisplay.Single, cConfig.eROIDisplay.Stacked_max, cConfig.eROIDisplay.Mosaik
                ImageToDisplay = StarImageSum_UInt16.CreateCopy
            Case cConfig.eROIDisplay.Stacked_mean
                Dim StarImageSum_Min As UInt32 = UInt32.MaxValue
                Dim StarImageSum_Max As UInt32 = UInt32.MinValue
                AIS.DB.IPP.MinMax(StarImageSum_UInt32, StarImageSum_Min, StarImageSum_Max)

                For Idx1 As Integer = 0 To StarImageSum_UInt32.GetUpperBound(0)
                    For Idx2 As Integer = 0 To StarImageSum_UInt32.GetUpperBound(1)
                        ImageToDisplay(Idx1, Idx2) = ScaleToUInt16(StarImageSum_UInt32(Idx1, Idx2), StarImageSum_Min, StarImageSum_Max)
                    Next Idx2
                Next Idx1
        End Select

        'Calculate statistics on the image
        Dim SumStat As New AstroNET.Statistics(AIS.DB.IPP)
        SumStat.DataProcessor_UInt16.ImageData(0).Data = ImageToDisplay
        Dim SumStatStat As AstroNET.Statistics.sStatistics = SumStat.ImageStatistics

        'Display the image
        ROIImageGenerator.ColorMap = Config.Stack_ROIDisplay_ColorMode
        ROIImageGenerator.GenerateDisplayImage(ImageToDisplay, CUShort(SumStatStat.MonoStatistics_Int.Min.Key), CUShort(SumStatStat.MonoStatistics_Int.Max.Key), AIS.DB.IPP)
        ROIImageGenerator.OutputImage.UnlockBits()
        pbImage.Image = ROIImageGenerator.OutputImage.BitmapToProcess

    End Sub

    Private Function CalculateMosaik(ByVal FilesToLoad As List(Of String)) As UInt16(,)

        Dim Tile_border As Integer = 3
        Dim Display_blur As Integer = 1
        Dim UseIPP As Boolean = True
        Dim ForceDirect As Boolean = False

        Dim FITSReader As New cFITSReader

        Dim MosaikWidth As Integer = CInt(Math.Ceiling(Math.Sqrt(FilesToLoad.Count)))              'Number of tiles in X direction
        Dim MosaikHeight As Integer = CInt(Math.Ceiling(FilesToLoad.Count / MosaikWidth))          'Number of tiles in Y direction
        Dim TileBorderWidth As Integer = Tile_border * (MosaikWidth - 1)
        Dim TileBorderHeight As Integer = Tile_border * (MosaikHeight - 1)

        Dim MosaikImage((MosaikWidth * Config.ROIDisplay_Width) + TileBorderWidth - 1, (MosaikHeight * Config.ROIDisplay_Height) + TileBorderHeight - 1) As UInt16

        'Compose the mosaik
        Dim WidthPtr As Integer = 0 : Dim WidthIdx As Integer = 0
        Dim HeightPtr As Integer = 0

        For Each File As String In FilesToLoad
            Dim DeltaX As Integer = 0 : If Config.ROIDisplay_UseDeltaXY Then DeltaX = AllListFiles(File).DeltaX
            Dim DeltaY As Integer = 0 : If Config.ROIDisplay_UseDeltaXY Then DeltaY = AllListFiles(File).DeltaY
            Dim ROI As New Rectangle(Config.ROIDisplay_X + DeltaX, Config.ROIDisplay_Y + DeltaY, Config.ROIDisplay_Width, Config.ROIDisplay_Height)
            Dim SingleFileTile(,) As UInt16 = FITSReader.ReadInUInt16(File, UseIPP, ROI, ForceDirect)
            If Display_blur > 1 Then cOpenCvSharp.MedianBlur(SingleFileTile, Display_blur)
            Try
                For X As Integer = 0 To Config.ROIDisplay_Width - 1
                    For Y As Integer = 0 To Config.ROIDisplay_Height - 1
                        MosaikImage(WidthPtr + X, HeightPtr + Y) = SingleFileTile(X, Y)
                    Next Y
                Next X
            Catch ex As Exception
                'Log this error ...
            End Try
            WidthPtr += Config.ROIDisplay_Width + Tile_border : WidthIdx += 1
            If WidthIdx >= MosaikWidth Then
                HeightPtr += Config.ROIDisplay_Height + Tile_border
                WidthPtr = 0
                WidthIdx = 0
            End If
        Next File

        Return MosaikImage

    End Function

    '''<summary>Mark a file in the list.</summary>
    Private Sub MarkFile(ByVal FileName As String, ByVal Color As Color)
        Dim CurrentRow As Integer = GetFileRow(FileName)
        If CurrentRow = -1 Then Exit Sub
        adgvMain.Rows(CurrentRow).Cells(eColumns.File).Style.BackColor = Color
        DE()
    End Sub

    Private Function ScaleToUInt16(ByVal Value As UInt32, ByVal Min As UInt32, ByVal Max As UInt32) As UInt16
        Return CType(((Value - Min) / (Max - Min)) * UInt16.MaxValue, UInt16)
    End Function

    '''<summary>Return the selected file name in the table.</summary>
    Private Function GetSelectedFileName() As String
        Return CStr(adgvMain.Rows(adgvMain.SelectedCells(0).RowIndex).Cells(eColumns.File).Value)
    End Function

    '''<summary>Return the selected file name in the table.</summary>
    Private Function GetSelectedFileName(e As DataGridViewCellEventArgs) As String
        Return CStr(adgvMain.Rows(e.RowIndex).Cells(eColumns.File).Value)
    End Function

    Private Sub tbPosX_MouseWheel(sender As Object, e As MouseEventArgs)
        'Change the value with the mouse wheel; calculation is triggered via the TextChanged event
        Dim NewValue As Integer = (CInt(CType(sender, TextBox).Text) + (Math.Sign(e.Delta) * CInt(1)))
        If NewValue < 0 Then NewValue = 0
        CType(sender, TextBox).Text = NewValue.ValRegIndep
    End Sub

    Private Sub RunSinglePixelStat(ByVal Pixel As Point)
        Dim Log As New List(Of String)
        Log.Add("Statistics for <" & Pixel.X.ValRegIndep & ":" & Pixel.Y.ValRegIndep & ">")
        Dim AllPixel As UInt16() = GetSamePixelFromMultipleFiles(Pixel)
        Log.Add("  " & AllPixel.Length.ValRegIndep & " pixel")
        Dim SamplesIgnored As Integer = 0
        Dim NewMean As Double = (New AstroDSP.cSigmaClipped).SigmaClipped_mean(AllPixel, SamplesIgnored)
        Log.Add("  SigmaClipped_mean: " & NewMean.ValRegIndep)
        Log.Add("  Samples Ignored: " & SamplesIgnored.ValRegIndep)
        tbPixelStat.Text = Join(Log.ToArray, System.Environment.NewLine)
    End Sub

    '''<summary>Get pixel from the same position from all specified files.</summary>
    Private Function GetSamePixelFromMultipleFiles(ByVal Pixel As Point) As UInt16()

        Dim AllPixel As New List(Of UInt16)

        'Get all pixel
        Dim StringValues As New List(Of String)
        For Each File As String In GetCheckedFiles()
            Dim PixelValueFast As UInt16 = GetOnePixel(File, AllListFiles(File).DataStartPosition, AllListFiles(File).NAXIS1, Pixel.X, Pixel.Y)
            AllPixel.Add(PixelValueFast)
        Next File

        Return AllPixel.ToArray

    End Function

    '''<summary>Get only 1 pixel value from the specified FITS file.</summary>
    Private Function GetOnePixel(ByVal FileName As String, ByVal DataStartPosition As Long, ByVal ImageWidth As Integer, ByVal XOffset As Integer, ByVal YOffset As Integer) As UInt16

        Dim BytePerPixel As Integer = 2

        'Open reader and position to start
        Dim DataReader As New System.IO.BinaryReader(System.IO.File.OpenRead(FileName))
        DataReader.BaseStream.Position = DataStartPosition

        'Read only 1 pixel
        Dim PixelOffset As Integer = ((YOffset * ImageWidth) + XOffset)
        DataReader.BaseStream.Position = DataStartPosition + (BytePerPixel * PixelOffset)
        Dim Bytes() As Byte = DataReader.ReadBytes(BytePerPixel)

        'Close data stream
        DataReader.Close()

        Return CUShort(BitConverter.ToInt16({Bytes(1), Bytes(0)}, 0) + 32768)

    End Function

    Private Sub tsmiFile_SaveAllFilesHisto_Click(sender As Object, e As EventArgs) Handles tsmiFile_SaveAllFilesHisto.Click

        Dim AllCheckedFiles As List(Of String) = GetCheckedFiles()

        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        'Get combined hist mono X axis (INT only)
        Dim AllADUValues As New List(Of Long)
        Dim FileList As New List(Of String)
        FileList.Add("ADU value")
        For Each SingleFile As String In AllCheckedFiles
            FileList.Add(System.IO.Path.GetFileNameWithoutExtension(SingleFile))
            If AllListFiles(SingleFile).Statistics.Count > 0 Then
                For Each ADUValue As Long In AllListFiles(SingleFile).Statistics.MonochromHistogram_Int.Keys
                    If AllADUValues.Contains(ADUValue) = False Then AllADUValues.Add(ADUValue)
                Next ADUValue
            End If
        Next SingleFile
        AllADUValues.Sort()

        Using workbook As New ClosedXML.Excel.XLWorkbook

            'Generate data
            Dim XY As New List(Of Object())
            For Each ADUValue As Long In AllADUValues
                Dim Values As New List(Of Object)
                Values.Add(ADUValue)
                For Each SingleFile As String In AllCheckedFiles
                    If AllListFiles(SingleFile).Statistics.MonochromHistogram_Int.ContainsKey(ADUValue) Then Values.Add(AllListFiles(SingleFile).Statistics.MonochromHistogram_Int(ADUValue)) Else Values.Add(String.Empty)
                Next SingleFile
                XY.Add(Values.ToArray)
            Next ADUValue
            Dim worksheet As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Histogram")
            worksheet.Cell(1, 1).InsertData(FileList, True)                                             'file names
            worksheet.Cell(2, 1).InsertData(XY)                                                         'combined histogram
            For Each col In worksheet.ColumnsUsed
                col.AdjustToContents()
            Next col

            'Save and open
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Process.Start(FileToGenerate)

        End Using

    End Sub

    Private Sub tsmiFile_SaveFITSandStats_Click(sender As Object, e As EventArgs) Handles tsmiFile_SaveFITSandStats.Click

        Dim AllCheckedFiles As List(Of String) = GetCheckedFiles()

        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        'Generate a list of all FITS keys and statistics including the maximum entry length
        Dim FoundFitsKeywords As New Dictionary(Of eFITSKeywords, Integer)
        Dim FoundStatParameters As New List(Of String)
        For Each FileName As String In AllCheckedFiles
            For Each Key As eFITSKeywords In AllListFiles(FileName).FITSHeader.Keys
                Dim HeaderValue As String = CStr(AllListFiles(FileName).FITSHeader(Key))
                If FoundFitsKeywords.ContainsKey(Key) = False Then FoundFitsKeywords.Add(Key, -1)
                If FoundFitsKeywords(Key) < HeaderValue.Length Then
                    FoundFitsKeywords(Key) = HeaderValue.Length
                End If
            Next Key
            If AllListFiles(FileName).Statistics.Count > 0 Then
                For Each StatParameter As String In AllListFiles(FileName).Statistics.MonoStatistics_Int.AllStats.Keys
                    If FoundStatParameters.Contains(StatParameter) = False Then FoundStatParameters.Add(StatParameter)
                Next StatParameter
            End If
        Next FileName

        Using workbook As New ClosedXML.Excel.XLWorkbook

            Dim worksheet As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Overview")
            Dim FileIdx As Integer = 1
            Dim KeyIdx As Integer = 1

            'Add header
            For Each Key As eFITSKeywords In FoundFitsKeywords.Keys
                KeyIdx += 1
                worksheet.Cell(FileIdx, KeyIdx).Value = Key.ToString
            Next Key
            For Each StatParameter As String In FoundStatParameters
                KeyIdx += 1
                worksheet.Cell(FileIdx, KeyIdx).Value = StatParameter
            Next StatParameter
            FileIdx += 1

            'Add all files
            For Each FileName As String In AllCheckedFiles
                KeyIdx = 1
                worksheet.Cell(FileIdx, KeyIdx).Value = FileName
                'Add all FITS headers
                For Each Key As eFITSKeywords In FoundFitsKeywords.Keys
                    KeyIdx += 1
                    'Add the found entry or no entry
                    If AllListFiles(FileName).FITSHeader.ContainsKey(Key) Then
                        worksheet.Cell(FileIdx, KeyIdx).SetTypedValue(AllListFiles(FileName).FITSHeader(Key))
                    Else
                        worksheet.Cell(FileIdx, KeyIdx).Value = "XXXXXX"
                    End If
                Next Key
                'Add all statistics values
                For Each Key As String In FoundStatParameters
                    KeyIdx += 1
                    If AllListFiles(FileName).Statistics.MonoStatistics_Int.AllStats.ContainsKey(Key) Then
                        worksheet.Cell(FileIdx, KeyIdx).SetTypedValue(AllListFiles(FileName).Statistics.MonoStatistics_Int.AllStats(Key))
                    Else
                        worksheet.Cell(FileIdx, KeyIdx).Value = "XXXXXX"
                    End If

                Next Key
                FileIdx += 1
            Next FileName

            'Auto-adjust all collumns
            For Each col In worksheet.ColumnsUsed
                col.AdjustToContents()
            Next col

            'Save and open
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Process.Start(FileToGenerate)

        End Using

    End Sub

    Private Sub cmsMain_ToClipboard_Click(sender As Object, e As EventArgs) Handles cmsMain_ToClipboard.Click
        Clipboard.Clear()
        Clipboard.SetImage(pbImage.Image)
    End Sub

End Class