Option Explicit On
Option Strict On

'''<summary>This form should handle actions that apply to multiple files, e.g. dark statistics, hot pixel search, basic stacking, ...</summary>
Partial Public Class frmMultiFileAction

    '''<summary>Folder to move bad files to.</summary>
    Public Property BadFolderName As String = "BAD"

    '''<summary>Available columns.</summary>
    Private Enum eColumns
        Process = 0
        FileName
        DateTime
        NAxis1
        NAxis2
        DeltaX
        DeltaY
        MonoMin
        MonoMax
        MonoMean
        MonoMedian
        DSS_OverallQuality
        DSS_SkyBackground
        DSS_NrStars
    End Enum


    '''<summary>Files in the list and it's properties.</summary>
    Public AllFiles As New List(Of cFileProps)

    Public adgvBinding As New BindingSource

    Private Plotter As cZEDGraph

    Private Config As New cConfig
    Private WithEvents Stacker As New cStacker
    Private LogContent As New System.Text.StringBuilder
    Private WithEvents FITSGrepper As New cFITSGrepper

    '''<summary>Common ROI calculator.</summary>
    Private ROICombiner As New cAstroImageProcessing.cROICombiner

    '''<summary>ROI image generator.</summary>
    Private ROIImageGenerator As New cImageFromData

    Private Class c2D(Of T)
        Public Matrix(,) As T = {}
        Public Sub New(ByRef Data(,) As T)
            Matrix = Data.CreateCopy
        End Sub
    End Class

    '═════════════════════════════════════════════════════════════════════════════
    'Form load code
    '═════════════════════════════════════════════════════════════════════════════

    Private Sub frmMultiFileAction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Prepare the GUI and the table
        pgMain.SelectedObject = Config
        adgvBinding.DataSource = AllFiles
        adgvMain.DataSource = adgvBinding
        Plotter = New cZEDGraph(zgcSinglePixelStat)
    End Sub

    '═════════════════════════════════════════════════════════════════════════════
    'List connected to the table and functions related to the list
    '═════════════════════════════════════════════════════════════════════════════

    '''<summary>Check if the given file name is already in the list of files.</summary>
    '''<param name="FileName">File to search in the list.</param>
    '''<returns>TRUE if file is in the list, FALSE else.</returns>
    Private Function FileInList(ByVal FileName As String) As Boolean
        If AllFiles.Count = 0 Then Return False
        For Each Entry As cFileProps In AllFiles
            If Entry.FileName = FileName Then Return True
        Next Entry
        Return False
    End Function

    '''<summary>Get the index of this file name within the AllFiles list.</summary>
    Private Function GetFileListIndex(ByVal FileName As String) As Integer
        If AllFiles.Count = 0 Then Return Nothing
        For Idx As Integer = 0 To AllFiles.Count - 1
            If AllFiles(Idx).FileName = FileName Then Return Idx
        Next Idx
        Return Nothing
    End Function

    '''<summary>Return the index of the selected table element in the AllFiles list.</summary>
    Private Function GetFileListIndex() As Integer
        Dim FileNameToSearch As String = GetSelectedFileName()
        For Idx As Integer = 0 To AllFiles.Count - 1
            If AllFiles(Idx).FileName = FileNameToSearch Then
                Return Idx
            End If
        Next Idx
        Return -1
    End Function

    '''<summary>Add files to the list.</summary>
    '''<param name="NewFiles">Files that should be added.</param>
    Private Sub AddFiles(NewFiles As IEnumerable(Of String))
        'Handle drag-and-drop for all dropped FIT(s) files
        For Each File As String In NewFiles
            If System.IO.Path.GetExtension(File).ToUpper.StartsWith(".FIT") Then
                If FileInList(File) = False Then
                    AllFiles.Add(New cFileProps(File))
                End If
            End If
        Next File
        'Grep all headers
        FITSGrepper.Grep(NewFiles)
        'Add fits header info
        For Each GreppedFile As String In FITSGrepper.AllFileHeaders.Keys
            AllFiles(GetFileListIndex(GreppedFile)).FITSHeader = FITSGrepper.AllFileHeaders(GreppedFile)
        Next GreppedFile
        'Update table
        RefreshTable()
    End Sub

    '''<summary>Get all available data formats from the DragEventArgs</summary>
    '''<param name="e">Dragged content.</param>
    '''<returns>All drop content entries.</returns>
    Private Function GetDropContent(ByRef e As System.Windows.Forms.DragEventArgs) As Dictionary(Of String, Object)
        Dim RetVal As New Dictionary(Of String, Object)
        For Each DataContent As String In e.Data.GetFormats
            RetVal.Add(DataContent, e.Data.GetData(DataContent))
        Next DataContent
        Return RetVal
    End Function

    '═════════════════════════════════════════════════════════════════════════════
    ' Logging
    '═════════════════════════════════════════════════════════════════════════════

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
    Private Sub RefreshTable()
        adgvBinding.ResetBindings(False)
        adgvMain.Columns(eColumns.FileName).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    End Sub

    Private Function ShiftROI(ByVal ROI As Drawing.Rectangle, ByVal X As Integer, ByVal Y As Integer) As Drawing.Rectangle
        Dim RetVal As Drawing.Rectangle = ROI
        ROI.X -= X
        ROI.Y -= Y
        Return ROI
    End Function

    Private Sub tsmiFileList_ClearList_Click(sender As Object, e As EventArgs) Handles tsmiFileList_ClearList.Click
        'Clear all items
        AllFiles = New List(Of cFileProps)
        FITSGrepper = New cFITSGrepper
        RefreshTable()
    End Sub

    Private Sub tsmiFile_OpenWorkingDir_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenWorkingDir.Click
        Process.Start("explorer.exe", Config.Gen_root)
    End Sub

    Private Sub tsmiAction_Stack_Click(sender As Object, e As EventArgs) Handles tsmiAction_StackSpecial.Click
        Stacker.CalcTotalStatistics = Config.Stat_CalcHist
        Stacker.FileToWrite_Min = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMinFile)
        Stacker.FileToWrite_Max = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMaxFile)
        Stacker.FileToWrite_Mean = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMeanFile)
        Stacker.FileToWrite_Sigma = IO.Path.Combine(Config.Gen_root, Config.Stat_StatSigmaFile)
        Stacker.StackFITSFiles(GetListFileNames(True))
    End Sub

    Private Sub tsmiAction_Run_Click(sender As Object, e As EventArgs) Handles tsmiAction_Run.Click

        Dim AllCheckedFiles As List(Of cFileProps) = GetListFiles(True)
        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)
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
            For Each FileToProcess As cFileProps In AllCheckedFiles

                'Start with this file
                FileCount += 1
                Dim CurrentRow As Integer = GetFileRow(FileToProcess)
                MarkFile(FileToProcess.FileName, Color.Red)
                tspbMain.Value = FileCount
                DE()

                'Read in the file
                Dim Container As New AstroNET.Statistics(AIS.DB.IPP)
                Container.ResetAllProcessors()
                Container.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileToProcess.FileName, True, True)

                'Calculate statistics
                If Config.Processing_CalcStatistics = True Then
                    FileToProcess.Statistics = Container.ImageStatistics
                End If

                'Calculate alignment
                If Config.Processing_CalcAlignment = True Then

                    MarkFileDelta(CurrentRow, Double.NaN, Double.NaN, Color.Orange)

                    Dim FileContentAsFloat32(,) As Single = {}
                    Dim Shift As System.Drawing.Point

                    'Run a Bin2 with max removal if selected (to remove hot pixel)
                    If Config.Stack_RunBin2 = True Then
                        FileContentAsFloat32 = ImageProcessing.Binning.Mean_RemoveOuter_Single(Container.DataProcessor_UInt16.ImageData(0).Data, 2, 1)
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
                    MarkFileDelta(CurrentRow, Shift.X, Shift.Y, Color.LimeGreen)

                End If

                'File done
                MarkFile(FileToProcess.FileName, Color.White)
                DE()

            Next FileToProcess
            tspbMain.Value = 0

        End If

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Cut certain ROI's (for stacking or separate storage)
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        If (Config.Processing_StoreAlignedFiles = True) Or (Config.Processing_CalcStackedFile = True) Or (Config.Processing_CalcSigmaClip = True) Then

            Log("Running common ROI calculation ...")
            ROICombiner.Clear()

            'Add all combiner-specific parameters (size and delta) to the ROICombiner
            For Each OriginalFile As cFileProps In AllCheckedFiles
                Dim FileSpecs As New cAstroImageProcessing.cROICombiner.cFileSpecifics(OriginalFile.NAXIS1, OriginalFile.NAXIS2, OriginalFile.DeltaX, OriginalFile.DeltaY)
                ROICombiner.Files.Add(OriginalFile.FileName, FileSpecs)
            Next OriginalFile

            'Calculate
            ROICombiner.Calculate()
            Log("  DONE, I have " & ROICombiner.CutROIs.Count.ValRegIndep & " common ROI's, size <" & ROICombiner.CutROIs.First.Value.Width.ValRegIndep & "x" & ROICombiner.CutROIs.First.Value.Height.ValRegIndep & ">")

            'Last.) Generate new files
            Dim FileIdx As Integer = -1
            Dim AlignFolder As String = String.Empty
            For Each FileToProcess As cFileProps In AllCheckedFiles
                Dim FileName As String = FileToProcess.FileName
                FileIdx += 1
                MarkFile(FileName, Color.Red)
                DE()
                'Load defined ROI
                Dim FileROI As UInt16(,) = FITSReader.ReadInUInt16(FileToProcess.FileName, True, ROICombiner.CutROIs(FileName), True)
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
                    AlignFolder = System.IO.Path.Combine(Config.Gen_root, System.IO.Path.GetFileNameWithoutExtension(FileName) & "_aligned")
                    If System.IO.Directory.Exists(AlignFolder) = False Then System.IO.Directory.CreateDirectory(AlignFolder)
                End If
                'Store aligned file (if selected)
                If Config.Processing_StoreAlignedFiles = True Then
                    Dim NewFile As String = System.IO.Path.Combine(AlignFolder, System.IO.Path.GetFileName(FileName))
                    cFITSWriter.Write(NewFile, FileROI, cFITSWriter.eBitPix.Int16)
                    Log("Stored file <" & FileName & "> as aligned version in <" & NewFile & ">")
                    Log("   (CutROIs: " & ROICombiner.CutROIs(FileName).Describe)
                End If
                MarkFile(FileToProcess.FileName, Color.LimeGreen)
            Next FileToProcess

        End If

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Stack with sigma clipping and store if requested
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        If Config.Processing_CalcSigmaClip = True Then

            Log("Running sigma-clipping processing (# of files: <" & SigmaClipROIs.Count.ValRegIndep & "> ...")
            Dim NAXIS1 As Integer = SigmaClipROIs(0).Matrix.GetUpperBound(0) - 1
            Dim NAXIS2 As Integer = SigmaClipROIs(0).Matrix.GetUpperBound(1) - 1
            Dim Calculated(NAXIS1 - 1, NAXIS2 - 1) As Double
            Dim SigmaClipped As New AstroDSP.cSigmaClipped(Config.Stack_SigClip_LowBound, Config.Stack_SigClip_HighBound)
            Dim ClipStat(NAXIS1 - 1, NAXIS2 - 1) As UInt16
            Dim TaskOptions As New ParallelOptions
            TaskOptions.MaxDegreeOfParallelism = 4
            Parallel.For(0, Calculated.GetUpperBound(0) + 1, TaskOptions, Sub(Idx1)
                                                                              Dim LocalIdx1 As Integer = Idx1
                                                                              For Idx2 As Integer = 0 To Calculated.GetUpperBound(1)
                                                                                  Dim Samples(SigmaClipROIs.GetUpperBound(0) - 1) As UInt16
                                                                                  For FileIdx As Integer = 0 To SigmaClipROIs.GetUpperBound(0) - 1
                                                                                      Samples(FileIdx) = SigmaClipROIs(FileIdx).Matrix(LocalIdx1, Idx2)
                                                                                  Next FileIdx
                                                                                  Calculated(LocalIdx1, Idx2) = SigmaClipped.SigmaClipped_mean(Samples, ClipStat(LocalIdx1, Idx2))

                                                                              Next Idx2
                                                                          End Sub)

            'Calculate the clipping statistics
            Dim ClipStatistics As New Dictionary(Of UInt16, UInt32)         'dictionary with number of pixel clipped
            For Idx As Integer = 0 To ClipStat.GetUpperBound(0)
                For Idx1 As Integer = 0 To ClipStat.GetUpperBound(1)
                    If ClipStatistics.ContainsKey(ClipStat(Idx, Idx1)) = False Then
                        ClipStatistics.Add(ClipStat(Idx, Idx1), 1)
                    Else
                        ClipStatistics(ClipStat(Idx, Idx1)) += CType(1, UInt16)
                    End If
                Next Idx1
            Next Idx
            Dim TotalPixel As Long = CLng(SigmaClipROIs.Count) * CLng(NAXIS1 * NAXIS2)
            Dim PixelClipped As UInt32 = 0
            For Each Key As UInt16 In ClipStatistics.Keys
                TotalPixel += ClipStatistics(Key)
                PixelClipped += Key * ClipStatistics(Key)
            Next Key

            Log("Sigma-clipping done, " & PixelClipped.ValRegIndep & " of " & TotalPixel.ValRegIndep & " pixel removed, this are " & (100 * (PixelClipped / TotalPixel)).ValRegIndep("0.00") & "%")

            Dim StackFileName_SigmaClip As String = IO.Path.Combine(Config.Gen_root, Config.Gen_OutputFileSigmaClip)
            cFITSWriter.Write(StackFileName_SigmaClip, Calculated, cFITSWriter.eBitPix.Single)
            If Config.Stat_OpenStackedFile Then Utils.StartWithItsEXE(StackFileName_SigmaClip)

            Log("Sigma-clipped file <" & StackFileName_SigmaClip & "> generated")

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
            If Config.Stat_OpenStackedFile Then Utils.StartWithItsEXE(StackFileName_UInt16)
            If Config.Stat_OpenStackedFile Then Utils.StartWithItsEXE(StackFileName_float32)

        End If

        tbLog.BackColor = Color.White

    End Sub

    Private Sub pgMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgMain.MouseWheel
        'Scroll in the property grid
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "CutLimit_Left"
                ROICombiner.CutLimit_Left = CUInt(ROICombiner.CutLimit_Left + (Math.Sign(e.Delta) * 5))
            Case "CutLimit_Right"
                ROICombiner.CutLimit_Right = CUInt(ROICombiner.CutLimit_Right + (Math.Sign(e.Delta) * 5))
            Case "ROIDisplay_X"
                Config.ROIDisplay_X += Math.Sign(e.Delta) * Config.ROIDisplay_PositionAndSize
                CalcAndDisplayCombinedROI()
            Case "ROIDisplay_Y"
                Config.ROIDisplay_Y += Math.Sign(e.Delta) * Config.ROIDisplay_PositionAndSize
                CalcAndDisplayCombinedROI()
            Case "ROIDisplay_Width"
                Config.ROIDisplay_Width += Math.Sign(e.Delta) * Config.ROIDisplay_PositionAndSize
                CalcAndDisplayCombinedROI()
            Case "ROIDisplay_Height"
                Config.ROIDisplay_Height += Math.Sign(e.Delta) * Config.ROIDisplay_PositionAndSize
                CalcAndDisplayCombinedROI()
            Case "Stat_SinglePixelX"
                Config.Stat_SinglePixelX += Math.Sign(e.Delta) * Config.ROIDisplay_DeltaXYStep
                If Config.Stat_SinglePixelX < 0 Then Config.Stat_SinglePixelX = 0
                RunSinglePixelStat(New Point(Config.Stat_SinglePixelX, Config.Stat_SinglePixelY))
            Case "Stat_SinglePixelY"
                Config.Stat_SinglePixelY += Math.Sign(e.Delta) * Config.ROIDisplay_DeltaXYStep
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
        e.Effect = System.Windows.Forms.DragDropEffects.None
        Dim DropContent As Dictionary(Of String, Object) = GetDropContent(e)
        'File
        If e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop) Then
            If CType(e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop), String()).Length >= 1 Then
                e.Effect = System.Windows.Forms.DragDropEffects.All
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
        Select Case e.ColumnIndex
            Case 0
                'Activate or deactivate the file processing for this file
                Dim Cell As DataGridViewCheckBoxCell = CType(adgvMain.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex), DataGridViewCheckBoxCell)
                Cell.Value = Not CType(Cell.Value, Boolean)
        End Select
        CalcAndDisplayCombinedROI()
    End Sub

    Private Sub adgvMain_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles adgvMain.RowEnter
        'Display the FITS header information
        If Config.Processing_DisplayFITSHeader Then
            Dim TextToDisplay As New List(Of String)
            TextToDisplay.Add("FITS header of file <" & AllFiles(GetFileListIndex).FileName & ">")
            TextToDisplay.AddRange(cFITSHeaderParser.GetListToDisplay(AllFiles(GetFileListIndex).FITSHeader))
            tbFITSHeader.Text = Join(TextToDisplay.ToArray, System.Environment.NewLine)
        End If
        'Display ROI
        CalcAndDisplayCombinedROI()
    End Sub

    Private Sub adgvMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles adgvMain.MouseWheel
        'Scroll mouse wheel
        If adgvMain.SelectedCells.Count = 0 Then Exit Sub
        Select Case adgvMain.SelectedCells(0).ColumnIndex
            Case eColumns.DeltaX
                'Delta X -> change the cell value, the update of the ROI value is done in the CellValueChanged event
                adgvMain.SelectedCells(0).Value = AllFiles(GetFileListIndex).DeltaX + (Math.Sign(e.Delta) * Config.ROIDisplay_DeltaXYStep)
            Case eColumns.DeltaY
                'Delta Y -> change the cell value, the update of the ROI value is done in the CellValueChanged event
                adgvMain.SelectedCells(0).Value = AllFiles(GetFileListIndex).DeltaY + (Math.Sign(e.Delta) * Config.ROIDisplay_DeltaXYStep)
        End Select
    End Sub

    Private Sub adgvMain_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles adgvMain.CellValueChanged
        'React on changed cell values
        If adgvMain.SelectedCells.Count = 0 Then Exit Sub
        Select Case adgvMain.SelectedCells(0).ColumnIndex
            Case eColumns.DeltaX
                'Delta X
                AllFiles(GetFileListIndex).DeltaX = CInt(adgvMain.SelectedCells(0).Value)
                CalcAndDisplayCombinedROI()
            Case eColumns.DeltaY
                'Delta Y
                AllFiles(GetFileListIndex).DeltaY = CInt(adgvMain.SelectedCells(0).Value)
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

    '''<summary>Get all list files.</summary>
    Private Function GetListFiles(ByVal CheckedOnly As Boolean) As List(Of cFileProps)
        Dim CheckedFiles As New List(Of cFileProps)
        For Each Item As cFileProps In AllFiles
            If Item.Process Then CheckedFiles.Add(Item)
        Next Item
        Return CheckedFiles
    End Function

    '''<summary>Get all file names.</summary>
    Private Function GetListFileNames(ByVal CheckedOnly As Boolean) As List(Of String)
        Dim RetVal As New List(Of String)
        For Each Entry As cFileProps In AllFiles
            If Entry.Process Then RetVal.Add(Entry.FileName)
        Next
        Return RetVal
    End Function

    '''<summary>Get the row for the selected file.</summary>
    Private Function GetFileRow(ByVal FileToMark As cFileProps) As Integer
        Return GetFileRow(FileToMark.FileName)
    End Function

    '''<summary>Get the row for the selected file.</summary>
    Private Function GetFileRow(ByVal FileToMark As String) As Integer
        For Idx As Integer = 0 To AllFiles.Count
            If FileToMark = CStr(adgvMain.Rows(Idx).Cells(eColumns.FileName).Value) Then Return Idx
        Next Idx
        Return -1
    End Function

    '''<summary>Generate a combined ROI for all selected files (e.g. for manual alignment).</summary>
    Private Sub CalcAndDisplayCombinedROI()

        If Config.ROIDisplay_Mode = cConfig.eROIDisplay.Off Then Exit Sub
        If Config.ROIDisplay_Height <= 0 Then Exit Sub
        If Config.ROIDisplay_Width <= 0 Then Exit Sub

        Dim UseIPP As Boolean = True
        Dim ForceDirect As Boolean = False

        'Read ROI's for manual alignment
        Dim Data As New AstroNET.Statistics(AIS.DB.IPP)
        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)

        'Get all files or only the selected file
        Dim FilesToLoad As New List(Of cFileProps)
        If Config.ROIDisplay_Mode = cConfig.eROIDisplay.Single Then
            'Display only active file
            FilesToLoad.Add(AllFiles(GetFileListIndex(GetSelectedFileName)))
        Else
            'Display all checked files
            FilesToLoad.AddRange(GetListFiles(True))
        End If

        'Sum up all images
        Dim ROIImage_UInt16(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt16
        Dim ROIImage_UInt32(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt32
        If Config.ROIDisplay_Mode = cConfig.eROIDisplay.Mosaik Then
            'Calculate mosaik (each ROI individual, no sum-up)
            ROIImage_UInt16 = CalculateMosaik(FilesToLoad, Config.ROIDisplay_MosaikBorderWidth)
        Else
            For Each File As cFileProps In FilesToLoad
                Dim DeltaX As Integer = 0 : If (Config.ROIDisplay_UseDeltaXY) And (Double.IsNaN(File.DeltaX) = False) Then DeltaX = CInt(File.DeltaX)
                Dim DeltaY As Integer = 0 : If (Config.ROIDisplay_UseDeltaXY) And (Double.IsNaN(File.DeltaY) = False) Then DeltaY = CInt(File.DeltaY)
                Data.ResetAllProcessors()
                Dim ROI As New Rectangle(Config.ROIDisplay_X + DeltaX, Config.ROIDisplay_Y + DeltaY, Config.ROIDisplay_Width, Config.ROIDisplay_Height)
                Data.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(File.FileName, UseIPP, ROI, ForceDirect)
                Select Case Config.ROIDisplay_Mode
                    Case cConfig.eROIDisplay.Single
                        ROIImage_UInt16 = Data.DataProcessor_UInt16.ImageData(0).Data
                    Case cConfig.eROIDisplay.Stacked_max
                        AIS.DB.IPP.MaxEvery(Data.DataProcessor_UInt16.ImageData(0).Data, ROIImage_UInt16)
                    Case cConfig.eROIDisplay.Stacked_mean
                        For Idx1 As Integer = 0 To ROIImage_UInt32.GetUpperBound(0)
                            For Idx2 As Integer = 0 To ROIImage_UInt32.GetUpperBound(1)
                                ROIImage_UInt32(Idx1, Idx2) += Data.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2)
                            Next Idx2
                        Next Idx1
                End Select
            Next File
        End If

        'Get a UInt16 value again
        Dim ImageToDisplay(Config.ROIDisplay_Width - 1, Config.ROIDisplay_Height - 1) As UInt16
        Select Case Config.ROIDisplay_Mode
            Case cConfig.eROIDisplay.Single, cConfig.eROIDisplay.Stacked_max, cConfig.eROIDisplay.Mosaik
                ImageToDisplay = ROIImage_UInt16.CreateCopy
            Case cConfig.eROIDisplay.Stacked_mean
                Dim StarImageSum_Min As UInt32 = UInt32.MaxValue
                Dim StarImageSum_Max As UInt32 = UInt32.MinValue
                AIS.DB.IPP.MinMax(ROIImage_UInt32, StarImageSum_Min, StarImageSum_Max)
                For Idx1 As Integer = 0 To ROIImage_UInt32.GetUpperBound(0)
                    For Idx2 As Integer = 0 To ROIImage_UInt32.GetUpperBound(1)
                        ImageToDisplay(Idx1, Idx2) = ScaleToUInt16(ROIImage_UInt32(Idx1, Idx2), StarImageSum_Min, StarImageSum_Max)
                    Next Idx2
                Next Idx1
        End Select

        'Condition check
        If IsNothing(ImageToDisplay) = True Then Exit Sub
        If ImageToDisplay.LongLength = 0 Then Exit Sub

        'Calculate statistics on the image
        Dim SumStat As New AstroNET.Statistics(AIS.DB.IPP)
        SumStat.DataProcessor_UInt16.ImageData(0).Data = ImageToDisplay
        Dim SumStatStat As AstroNET.Statistics.sStatistics = SumStat.ImageStatistics

        'Display the image
        Dim NoROI As Rectangle = Nothing
        With ROIImageGenerator
            .CM = Config.Stack_ROIDisplay_ColorMode
            .CM_LowerEnd_Absolute = SumStatStat.MonoStatistics_Int.Min.Key
            .CM_UpperEnd_Absolute = SumStatStat.MonoStatistics_Int.Max.Key
            .GenerateDisplayImage(ImageToDisplay, NoROI, SumStatStat, AIS.DB.IPP)
            .OutputImage.UnlockBits()
            pbImage.Image = .OutputImage.BitmapToProcess
        End With

    End Sub

    '''<summary>Calculate a mosaik image from several ROI's of the files.</summary>
    '''<param name="FilesToLoad">List of FITS files (UInt16 mode only) to load.</param>
    '''<param name="TileBorderWidth">Width [pixel] of the tile borders.</param>
    '''<returns></returns>
    Private Function CalculateMosaik(ByVal FilesToLoad As List(Of cFileProps), ByVal TileBorderWidth As Integer) As UInt16(,)

        Dim Display_blur As Integer = 1
        Dim UseIPP As Boolean = True
        Dim ForceDirect As Boolean = False

        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)

        'Enter condition
        If IsNothing(FilesToLoad) = True Then Return Nothing
        If FilesToLoad.Count = 0 Then Return Nothing

        Dim MosaikWidth As Integer = CInt(Math.Ceiling(Math.Sqrt(FilesToLoad.Count)))              'Number of tiles in X direction
        Dim MosaikHeight As Integer = CInt(Math.Ceiling(FilesToLoad.Count / MosaikWidth))          'Number of tiles in Y direction
        Dim TileBorderPixel_X As Integer = TileBorderWidth * (MosaikWidth - 1)
        Dim TileBorderPixel_Y As Integer = TileBorderWidth * (MosaikHeight - 1)

        Dim MosaikImage((MosaikWidth * Config.ROIDisplay_Width) + TileBorderPixel_X - 1, (MosaikHeight * Config.ROIDisplay_Height) + TileBorderPixel_Y - 1) As UInt16

        'Compose the mosaik
        Dim MosaikBasePtr_X As Integer = 0 : Dim MosaikElement_X As Integer = 0
        Dim MosaikBasePtr_Y As Integer = 0

        For Each File As cFileProps In FilesToLoad
            Dim DeltaX As Integer = 0 : If (Config.ROIDisplay_UseDeltaXY = True) And (Double.IsNaN(File.DeltaX) = False) Then DeltaX = CInt(File.DeltaX)
            Dim DeltaY As Integer = 0 : If (Config.ROIDisplay_UseDeltaXY = True) And (Double.IsNaN(File.DeltaY) = False) Then DeltaY = CInt(File.DeltaY)
            Dim ROI As New Rectangle(Config.ROIDisplay_X + DeltaX, Config.ROIDisplay_Y + DeltaY, Config.ROIDisplay_Width, Config.ROIDisplay_Height)
            Dim SingleFileTile(,) As UInt16 = FITSReader.ReadInUInt16(File.FileName, UseIPP, ROI, ForceDirect)
            If Display_blur > 1 Then cOpenCvSharp.MedianBlur(SingleFileTile, Display_blur)
            Try
                For X As Integer = 0 To Config.ROIDisplay_Width - 1
                    For Y As Integer = 0 To Config.ROIDisplay_Height - 1
                        MosaikImage(MosaikBasePtr_X + X, MosaikBasePtr_Y + Y) = SingleFileTile(X, Y)
                    Next Y
                Next X
            Catch ex As Exception
                'Log this error ...
            End Try
            'Move to the next tile
            MosaikBasePtr_X += Config.ROIDisplay_Width + TileBorderWidth
            MosaikElement_X += 1
            'Jump to next row if required
            If MosaikElement_X >= MosaikWidth Then
                MosaikBasePtr_Y += Config.ROIDisplay_Height + TileBorderWidth
                MosaikBasePtr_X = 0
                MosaikElement_X = 0
            End If
        Next File

        Return MosaikImage

    End Function

    '''<summary>Mark a file in the list.</summary>
    Private Sub MarkFile(ByVal FileName As String, ByVal Color As Color)
        Dim CurrentRow As Integer = GetFileRow(FileName)
        If CurrentRow = -1 Then Exit Sub
        adgvMain.Rows(CurrentRow).Cells(eColumns.FileName).Style.BackColor = Color
        DE()
    End Sub

    '''<summary>Mark a file in the list.</summary>
    Private Sub MarkFileDelta(ByVal CurrentRow As Integer, ByVal DeltaX As Double, ByVal DeltaY As Double, ByVal Color As Color)
        If CurrentRow = -1 Then Exit Sub
        adgvMain.Rows(CurrentRow).Cells(eColumns.DeltaX).Value = DeltaX
        adgvMain.Rows(CurrentRow).Cells(eColumns.DeltaY).Value = DeltaY
        adgvMain.Rows(CurrentRow).Cells(eColumns.DeltaX).Style.BackColor = Color
        adgvMain.Rows(CurrentRow).Cells(eColumns.DeltaY).Style.BackColor = Color
        DE()
    End Sub

    Private Function ScaleToUInt16(ByVal Value As UInt32, ByVal Min As UInt32, ByVal Max As UInt32) As UInt16
        Return CType(((Value - Min) / (Max - Min)) * UInt16.MaxValue, UInt16)
    End Function

    '''<summary>Return the selected file name in the table.</summary>
    Private Function GetSelectedFileName() As String
        Try
            Return CStr(adgvMain.Rows(adgvMain.SelectedCells(0).RowIndex).Cells(eColumns.FileName).Value)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    '''<summary>Return the selected file name in the table.</summary>
    Private Function GetSelectedFileName(e As DataGridViewCellEventArgs) As String
        Return CStr(adgvMain.Rows(e.RowIndex).Cells(eColumns.FileName).Value)
    End Function

    Private Sub tbPosX_MouseWheel(sender As Object, e As MouseEventArgs)
        'Change the value with the mouse wheel; calculation is triggered via the TextChanged event
        Dim NewValue As Integer = (CInt(CType(sender, TextBox).Text) + (Math.Sign(e.Delta) * CInt(1)))
        If NewValue < 0 Then NewValue = 0
        CType(sender, TextBox).Text = NewValue.ValRegIndep
    End Sub

    Private Sub RunSinglePixelStat(ByVal Pixel As Point)
        Dim SelectedPixel As String = "< " & Pixel.X.ValRegIndep & " : " & Pixel.Y.ValRegIndep & ">"
        Dim Log As New List(Of String)
        Log.Add("Statistics for " & SelectedPixel)
        Dim AllPixel As UInt16() = GetSamePixelFromMultipleFiles(Pixel)
        Log.Add("  " & AllPixel.Length.ValRegIndep & " pixel")
        Dim SamplesIgnored As UInt16 = 0
        Dim NewMean As Double = (New AstroDSP.cSigmaClipped).SigmaClipped_mean(AllPixel, SamplesIgnored)
        Log.Add("  SigmaClipped_mean: " & NewMean.ValRegIndep)
        Log.Add("  Samples Ignored: " & SamplesIgnored.ValRegIndep)
        tbPixelStat.Text = Join(Log.ToArray, System.Environment.NewLine)

        Plotter.Clear()
        Plotter.PlotData("Pixel vs file", AllPixel)
        Plotter.ForceUpdate()

    End Sub

    '''<summary>Get pixel from the same position from all specified files.</summary>
    Private Function GetSamePixelFromMultipleFiles(ByVal Pixel As Point) As UInt16()

        Dim AllPixel As New List(Of UInt16)

        'Get all pixel
        Dim StringValues As New List(Of String)
        For Each Item As cFileProps In GetListFiles(True)
            Dim PixelValueFast As UInt16 = GetOnePixel(Item.FileName, Item.DataStartPosition, Item.NAXIS1, Pixel.X, Pixel.Y)
            AllPixel.Add(PixelValueFast)
        Next Item

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

        Dim AllCheckedFiles As List(Of cFileProps) = GetListFiles(True)

        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        'Get combined hist mono X axis (INT only)
        Dim AllADUValues As New List(Of Long)
        Dim FileList As New List(Of String)
        FileList.Add("ADU value")
        For Each Item As cFileProps In AllCheckedFiles
            FileList.Add(System.IO.Path.GetFileNameWithoutExtension(Item.FileName))
            If Item.Statistics.Count > 0 Then
                For Each ADUValue As Long In Item.Statistics.MonochromHistogram_Int.Keys
                    If AllADUValues.Contains(ADUValue) = False Then AllADUValues.Add(ADUValue)
                Next ADUValue
            End If
        Next Item
        AllADUValues.Sort()

        Using workbook As New ClosedXML.Excel.XLWorkbook

            'Generate data
            Dim XY As New List(Of Object())
            For Each ADUValue As Long In AllADUValues
                Dim Values As New List(Of Object)
                Values.Add(ADUValue)
                For Each Item As cFileProps In AllCheckedFiles
                    If Item.Statistics.MonochromHistogram_Int.ContainsKey(ADUValue) Then Values.Add(Item.Statistics.MonochromHistogram_Int(ADUValue)) Else Values.Add(String.Empty)
                Next Item
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
            Utils.StartWithItsEXE(FileToGenerate)

        End Using

    End Sub

    Private Sub tsmiFile_SaveFITSandStats_Click(sender As Object, e As EventArgs) Handles tsmiFile_SaveFITSandStats.Click

        Dim AllCheckedFiles As List(Of cFileProps) = GetListFiles(True)

        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        'Generate a list of all FITS keys and statistics including the maximum entry length
        Dim FoundFitsKeywords As New Dictionary(Of eFITSKeywords, Integer)
        Dim FoundStatParameters As New List(Of String)
        For Each Item As cFileProps In AllFiles
            For Each Key As eFITSKeywords In Item.FITSHeader.Keys
                Dim HeaderValue As String = CStr(Item.FITSHeader(Key))
                If FoundFitsKeywords.ContainsKey(Key) = False Then FoundFitsKeywords.Add(Key, -1)
                If FoundFitsKeywords(Key) < HeaderValue.Length Then
                    FoundFitsKeywords(Key) = HeaderValue.Length
                End If
            Next Key
            If Item.Statistics.Count > 0 Then
                For Each StatParameter As String In Item.Statistics.MonoStatistics_Int.AllStats.Keys
                    If FoundStatParameters.Contains(StatParameter) = False Then FoundStatParameters.Add(StatParameter)
                Next StatParameter
            End If
        Next Item

        Using workbook As New ClosedXML.Excel.XLWorkbook

            Dim worksheet As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Overview")
            Dim HeaderRow As Integer = 1
            Dim RowIdx As Integer = 1
            Dim KeyIdx As Integer = 1

            '═════════════════════════════════════════════════════════════════════════════
            'Build EXCEL header
            '═════════════════════════════════════════════════════════════════════════════

            'FITS keywords
            For Each Key As eFITSKeywords In FoundFitsKeywords.Keys
                KeyIdx += 1
                worksheet.Cell(HeaderRow, KeyIdx).Value = Key.ToString
            Next Key

            'Statistic parameters
            For Each StatParameter As String In FoundStatParameters
                KeyIdx += 1
                worksheet.Cell(HeaderRow, KeyIdx).Value = StatParameter
            Next StatParameter

            'Parameters edited in MultiFileAction
            KeyIdx += 1 : worksheet.Cell(HeaderRow, KeyIdx).Value = "Use"
            KeyIdx += 1 : worksheet.Cell(HeaderRow, KeyIdx).Value = "DeltaX"
            KeyIdx += 1 : worksheet.Cell(HeaderRow, KeyIdx).Value = "DeltaY"
            KeyIdx += 1 : worksheet.Cell(HeaderRow, KeyIdx).Value = "DSS - quality"
            KeyIdx += 1 : worksheet.Cell(HeaderRow, KeyIdx).Value = "DSS - background"
            KeyIdx += 1 : worksheet.Cell(HeaderRow, KeyIdx).Value = "DSS - stars"

            '═════════════════════════════════════════════════════════════════════════════
            'Build entries for all files
            '═════════════════════════════════════════════════════════════════════════════

            'Add all checked files
            For Each Item As cFileProps In AllCheckedFiles
                RowIdx += 1
                KeyIdx = 1
                worksheet.Cell(RowIdx, KeyIdx).Value = Item.FileName
                'Add all FITS headers
                For Each Key As eFITSKeywords In FoundFitsKeywords.Keys
                    KeyIdx += 1
                    'Add the found entry or no entry
                    If Item.FITSHeader.ContainsKey(Key) Then
                        worksheet.Cell(RowIdx, KeyIdx).SetTypedValue(Item.FITSHeader(Key))
                    Else
                        worksheet.Cell(RowIdx, KeyIdx).Value = "XXXXXX"
                    End If
                Next Key
                'Add all statistics values
                For Each Key As String In FoundStatParameters
                    KeyIdx += 1
                    If Item.Statistics.MonoStatistics_Int.AllStats.ContainsKey(Key) Then
                        worksheet.Cell(RowIdx, KeyIdx).SetTypedValue(Item.Statistics.MonoStatistics_Int.AllStats(Key))
                    Else
                        worksheet.Cell(RowIdx, KeyIdx).Value = "XXXXXX"
                    End If
                Next Key
                'Add all MultiFileAction values
                KeyIdx += 1 : worksheet.Cell(RowIdx, KeyIdx).Value = "X"
                KeyIdx += 1 : If Double.IsNaN(Item.DeltaX) = False Then worksheet.Cell(RowIdx, KeyIdx).Value = Item.DeltaX
                KeyIdx += 1 : If Double.IsNaN(Item.DeltaY) = False Then worksheet.Cell(RowIdx, KeyIdx).Value = Item.DeltaY
            Next Item

            '═════════════════════════════════════════════════════════════════════════════
            'Check which values are all the same
            '═════════════════════════════════════════════════════════════════════════════

            'Auto-adjust all collumns
            For Each col In worksheet.ColumnsUsed
                'Get all column values and remove first entry (header)
                Dim AllEntries As New List(Of String)
                For Each Entry As ClosedXML.Excel.IXLCell In col.CellsUsed
                    AllEntries.Add(Entry.GetValue(Of String))
                Next Entry
                AllEntries.RemoveAt(0)
                'If there is only 1 same element, hide, else adjust to content
                If AllEntries.Distinct.Count = 1 Then
                    col.Hide()
                Else
                    col.AdjustToContents()
                End If

            Next col

            'Save and open
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Utils.StartWithItsEXE(FileToGenerate)

        End Using

    End Sub

    Private Sub cmsMain_ToClipboard_Click(sender As Object, e As EventArgs) Handles cmsMain_ToClipboard.Click
        Clipboard.Clear()
        Clipboard.SetImage(pbImage.Image)
    End Sub

    Private Sub tsmiAction_SelectAll_Click(sender As Object, e As EventArgs) Handles tsmiAction_SelectAll.Click
        'Select all files for processing
        SetCheckValue(True)
        CalcAndDisplayCombinedROI()
    End Sub

    Private Sub tsmiAction_DeSelectAll_Click(sender As Object, e As EventArgs) Handles tsmiAction_DeSelectAll.Click
        'Deselect all files for processing
        SetCheckValue(False)
        CalcAndDisplayCombinedROI()
    End Sub

    Private Sub SetCheckValue(ByVal Value As Boolean)
        For Each Row As DataGridViewRow In adgvMain.Rows
            Row.Cells(eColumns.Process).Value = Value
        Next Row
    End Sub

    Private Sub tsmiAction_DSSParam_Click(sender As Object, e As EventArgs) Handles tsmiAction_DSSParam.Click

        'Read DeepSkyStacker (DSS) info if available
        For Idx As Integer = 0 To AllFiles.Count - 1
            Dim CurrentRow As Integer = GetFileRow(AllFiles(Idx))
            Dim FileDirectory As String = System.IO.Path.GetDirectoryName(AllFiles(Idx).FileName)
            'Load .info.txt file
            Dim InfoFile As String = System.IO.Path.Combine(FileDirectory, System.IO.Path.GetFileNameWithoutExtension(AllFiles(Idx).FileName) & "." & cDeepSkyStacker.DSS_FileInfo)
            If System.IO.File.Exists(InfoFile) Then
                Dim Stars As List(Of DSSFileParser.sStarInfo) = DSSFileParser.ParseInfoFile(InfoFile, AllFiles(Idx).NrStars, AllFiles(Idx).OverallQuality, AllFiles(Idx).SkyBackground)
                If tbStars.Items.Count = 1 Then
                    tbStars.Items.Clear()
                    Dim ListOfStars(Stars.Count - 1) As String
                    For StarIdx As Integer = 0 To Stars.Count - 1
                        ListOfStars(StarIdx) = Stars(StarIdx).Format
                    Next StarIdx
                    tbStars.Items.AddRange(ListOfStars)
                End If
            End If
            'Load .stackinfo.txt (the stack info) for this file (parsed multiple times but is fast so left like this ...)
            Dim StackInfoFiles As New List(Of String)(System.IO.Directory.GetFiles(FileDirectory, "*." & cDeepSkyStacker.DSS_StackInfo))
            If StackInfoFiles.Count > 0 Then
                DSSFileParser.ParseStackInfo(StackInfoFiles.First, AllFiles(Idx).FileName, AllFiles(Idx).DeltaX, AllFiles(Idx).DeltaY)
            End If
        Next Idx
        RefreshTable()

    End Sub

    Private Sub cmsTable_OpenFile_Click(sender As Object, e As EventArgs) Handles cmsTable_OpenFile.Click
        AIS.OpenFile(GetSelectedFileName)
    End Sub

    Private Sub tbStars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbStars.SelectedIndexChanged
        Try
            Dim CurrentEntry As String() = CStr(tbStars.SelectedItem).Split("|")
            Config.ROIDisplay_X = CInt(CurrentEntry(0).ValRegIndep) - (Config.ROIDisplay_Width \ 2)
            Config.ROIDisplay_Y = CInt(CurrentEntry(1).ValRegIndep) - (Config.ROIDisplay_Height \ 2)
            CalcAndDisplayCombinedROI()
            RunSinglePixelStat(New Point(CurrentEntry(0).ValRegIndepInteger, CurrentEntry(1).ValRegIndepInteger))
        Catch ex As Exception
            'Do nothing ...
        End Try

    End Sub

    Private Function SortByIntensity(ByVal A As String, ByVal B As String) As Integer
        Return Split(A, ":").Last.ValRegIndep.CompareTo(Split(B, ":").Last.ValRegIndep)
    End Function

    Private Sub tsmiAction_HotPixel_Method1_Click(sender As Object, e As EventArgs) Handles tsmiAction_HotPixel_Method1.Click

        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)
        Dim MaxOfAll(,) As UInt16 = {}

        'Get max of all
        Dim AllCheckedFiles As List(Of cFileProps) = GetListFiles(True)
        Dim FirstFile As Boolean = True
        For Each FileToProcess As cFileProps In AllCheckedFiles
            MarkFile(FileToProcess.FileName, Color.Red)
            DE()
            'Load defined ROI
            Dim FileROI As UInt16(,) = FITSReader.ReadInUInt16(FileToProcess.FileName, True, True)
            If FirstFile = True Then
                MaxOfAll = AIS.DB.IPP.Copy(FileROI)
            Else
                AIS.DB.IPP.MaxEvery(FileROI, MaxOfAll)
            End If
            FirstFile = False
        Next FileToProcess

        'Get statistics
        Dim SumStat As New AstroNET.Statistics(AIS.DB.IPP)
        SumStat.DataProcessor_UInt16.ImageData(0).Data = MaxOfAll
        Dim SumStatStat As AstroNET.Statistics.sStatistics = SumStat.ImageStatistics
        Dim HotPixelLimit As Int64 = Int64.MaxValue
        Dim HotPixelCount As System.UInt64 = 0
        For Each Entry As System.Int64 In SumStatStat.MonochromHistogram_Int.Keys.Reverse
            HotPixelLimit = Entry
            HotPixelCount += SumStatStat.MonochromHistogram_Int(Entry)
            If HotPixelCount > 10000 Then Exit For
        Next Entry

        'Find all hotpixel
        tbStars.Items.Clear()
        Dim HotPixels As New List(Of String)
        For Idx1 As Integer = 0 To MaxOfAll.GetUpperBound(0)
            For Idx2 As Integer = 0 To MaxOfAll.GetUpperBound(1)
                If MaxOfAll(Idx1, Idx2) >= HotPixelLimit Then
                    HotPixels.Add(Idx1.ValRegIndep.PadLeft(4, " "c) & ":" & Idx2.ValRegIndep.PadLeft(4, " "c) & ":" & MaxOfAll(Idx1, Idx2).ValRegIndep)
                End If
            Next Idx2
        Next Idx1
        HotPixels.Sort(AddressOf SortByIntensity)
        tbStars.Items.AddRange(HotPixels.ToArray)

    End Sub

    Private Sub tsmiAction_HotPixel_Method2_Click(sender As Object, e As EventArgs) Handles tsmiAction_HotPixel_Method2.Click

        Const UInt32One As UInt32 = 1

        'For each pixel take the area around and check if the value is significantly too high
        Dim FixedPixelCount As UInt32 = 0
        Dim HotPixelLimit As Double = 5
        With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
            For Idx1 As Integer = 1 To .NAXIS1 - 2
                For Idx2 As Integer = 1 To .NAXIS2 - 2
                    Dim SurSum As New Ato.cSingleValueStatistics(True)
                    SurSum.AddValue(.Data(Idx1 - 1, Idx2 - 1))
                    SurSum.AddValue(.Data(Idx1 - 1, Idx2))
                    SurSum.AddValue(.Data(Idx1 - 1, Idx2 + 1))
                    SurSum.AddValue(.Data(Idx1, Idx2 - 1))
                    SurSum.AddValue(.Data(Idx1, Idx2 + 1))
                    SurSum.AddValue(.Data(Idx1 + 1, Idx2 - 1))
                    SurSum.AddValue(.Data(Idx1 + 1, Idx2))
                    SurSum.AddValue(.Data(Idx1 + 1, Idx2 + 1))
                    If .Data(Idx1, Idx2) > HotPixelLimit * SurSum.Percentile(50) Then
                        .Data(Idx1, Idx2) = CUShort(SurSum.Percentile(50))
                        FixedPixelCount += UInt32One
                    End If
                Next Idx2
            Next Idx1
        End With
        'Log.Log("Fixed " & FixedPixelCount.ValRegIndep & " pixel")
        'Idle()

    End Sub

    Private Sub tsmiAction_HotPixel_Fix_Click(sender As Object, e As EventArgs) Handles tsmiAction_HotPixel_Fix.Click

        With ofdMain
            .Filter = "Hot pixel file (*." & cDeepSkyStacker.DSS_HotPixel & ")|*." & cDeepSkyStacker.DSS_HotPixel
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With
        Dim HotPixels As List(Of Drawing.Point) = cDeepSkyStacker.GetHotPixel(ofdMain.FileName)

        Dim ReplaceLog As New List(Of String)
        With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
            For Each HotPixel As Drawing.Point In HotPixels
                Dim X As Integer = HotPixel.X
                Dim Y As Integer = HotPixel.Y
                'Run median
                Dim SurPix As New List(Of UInt16)
                SurPix.Add(.Data(X - 1, Y - 1))
                SurPix.Add(.Data(X - 1, Y))
                SurPix.Add(.Data(X - 1, Y + 1))
                SurPix.Add(.Data(X, Y - 1))
                SurPix.Add(.Data(X, Y))
                SurPix.Add(.Data(X, Y + 1))
                SurPix.Add(.Data(X + 1, Y - 1))
                SurPix.Add(.Data(X + 1, Y))
                SurPix.Add(.Data(X + 1, Y + 1))
                SurPix.Sort()
                Dim NewVal As UInt16 = SurPix(SurPix.Count \ 2)
                'Replace wrong pixel
                ReplaceLog.Add(X.ValRegIndep & ":" & Y.ValRegIndep & ": " & .Data(X, Y).ValRegIndep & "->" & NewVal.ValRegIndep)
                .Data(X, Y) = NewVal
            Next HotPixel
        End With
        'Log.Log(ReplaceLog)
        'Log.Log("Fixed " & HotPixel.Count & " pixel")

        Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
        'Log.Log(StatisticsReport)

    End Sub

    Private Sub cmsTable_MoveToBad_Click(sender As Object, e As EventArgs) Handles cmsTable_MoveToBad.Click
        'Get path parts and create BAD folder if it does not exist
        Dim SelectedFile As String = GetSelectedFileName()
        Dim FileNameOnly As String = System.IO.Path.GetFileName(SelectedFile)
        Dim FileNameNoExtension As String = System.IO.Path.GetFileNameWithoutExtension(SelectedFile)
        Dim ParentFolder As String = System.IO.Path.GetDirectoryName(SelectedFile)
        Dim BadFolder As String = System.IO.Path.Combine(ParentFolder, BadFolderName)
        If System.IO.Directory.Exists(BadFolder) = False Then System.IO.Directory.CreateDirectory(BadFolder)
        'Move the file in the BAD folder
        System.IO.File.Move(System.IO.Path.Combine(ParentFolder, FileNameOnly), System.IO.Path.Combine(BadFolder, FileNameOnly))
        'Move DSS file is exists
        Dim DSS_FI As String = System.IO.Path.Combine(ParentFolder, FileNameNoExtension & "." & cDeepSkyStacker.DSS_FileInfo)
        If System.IO.File.Exists(DSS_FI) Then System.IO.File.Move(DSS_FI, System.IO.Path.Combine(BadFolder, FileNameNoExtension & "." & cDeepSkyStacker.DSS_FileInfo))
    End Sub

End Class