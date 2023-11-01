Option Explicit On
Option Strict On

'''<summary>This form should handle actions that apply to multiple files, e.g. dark statistics, hot pixel search, basic stacking, ...</summary>
Public Class frmMultiFileAction

    Public Class cConfig

        Private Const Cat_generic As String = "1.) Generic settings"
        Private Const Cat_stack As String = "2.) Stacking"
        Private Const Cat_statistics As String = "3.) Statistics"

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("a) Root working folder")>
        <ComponentModel.Description("Root working folder")>
        Public Property Gen_root As String
            Get
                Return MyGen_root
            End Get
            Set(value As String)
                MyGen_root = value
            End Set
        End Property
        Private MyGen_root As String = AIS.DB.MyPath

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("b) Output file - UInt16")>
        <ComponentModel.Description("Output file")>
        <ComponentModel.DefaultValue("Stacked_UInt16.fits")>
        Public Property Gen_OutputFileUInt16 As String
            Get
                Return MyGen_OutputFileUInt16
            End Get
            Set(value As String)
                MyGen_OutputFileUInt16 = value
            End Set
        End Property
        Private MyGen_OutputFileUInt16 As String = "Stacked_UInt16.fits"

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("c) Output file - float32")>
        <ComponentModel.Description("Output file")>
        <ComponentModel.DefaultValue("Stacked_float32.fits")>
        Public Property Gen_OutputFilefloat32 As String
            Get
                Return MyGen_OutputFilefloat32
            End Get
            Set(value As String)
                MyGen_OutputFilefloat32 = value
            End Set
        End Property
        Private MyGen_OutputFilefloat32 As String = "Stacked_float32.fits"

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("d) Auto-open stack files?")>
        <ComponentModel.Description("Open stacked file on finish?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stat_OpenStackedFile As Boolean
            Get
                Return MyStat_OpenStackedFile
            End Get
            Set(value As Boolean)
                MyStat_OpenStackedFile = value
            End Set
        End Property
        Private MyStat_OpenStackedFile As Boolean = True

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("a) Run Bin2 on input data?")>
        <ComponentModel.Description("Run Bin2 with max removal?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stack_RunBin2 As Boolean
            Get
                Return MyStack_RunBin2
            End Get
            Set(value As Boolean)
                MyStack_RunBin2 = value
            End Set
        End Property
        Private MyStack_RunBin2 As Boolean = True

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("b) XCorr segmentation")>
        <ComponentModel.Description("Segments per smaller axis for XCorr")>
        <ComponentModel.DefaultValue(4)>
        Public Property Stack_XCorrSegmentation As Integer
            Get
                Return MyStack_XCorrSegmentation
            End Get
            Set(value As Integer)
                MyStack_XCorrSegmentation = value
            End Set
        End Property
        Private MyStack_XCorrSegmentation As Integer = 4

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("c) XCorr tile reduction")>
        <ComponentModel.Description("Pixel to make tile smaller - equals to maximum shift")>
        <ComponentModel.DefaultValue(50)>
        Public Property Stack_TlpReduction As Integer
            Get
                Return MyStack_TlpReduction
            End Get
            Set(value As Integer)
                MyStack_TlpReduction = value
            End Set
        End Property
        Private MyStack_TlpReduction As Integer = 50

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("d) Shift margin")>
        <ComponentModel.Description("Shift margin for match all ROIs to 1 big image")>
        <ComponentModel.DefaultValue(20)>
        Public Property Stack_ShiftMargin As Integer
            Get
                Return MyStack_ShiftMargin
            End Get
            Set(value As Integer)
                MyStack_ShiftMargin = value
            End Set
        End Property
        Private MyStack_ShiftMargin As Integer = 20

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("a) Calculate histogram?")>
        <ComponentModel.Description("Calculate the total histogram")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stat_CalcHist As Boolean
            Get
                Return MyStat_CalcHist
            End Get
            Set(value As Boolean)
                MyStat_CalcHist = value
            End Set
        End Property
        Private MyStat_CalcHist As Boolean = True

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("b) Statistic image - max of all")>
        <ComponentModel.Description("Store a FITS file with the total max - leave blank to skip storing")>
        Public Property Stat_StatMaxFile As String
            Get
                Return MyStat_StatMaxFile
            End Get
            Set(value As String)
                MyStat_StatMaxFile = value
            End Set
        End Property
        Private MyStat_StatMaxFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_max.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("c) Statistic image - min of all")>
        <ComponentModel.Description("Store a FITS file with the total min - leave blank to skip storing")>
        Public Property Stat_StatMinFile As String
            Get
                Return MyStat_StatMinFile
            End Get
            Set(value As String)
                MyStat_StatMinFile = value
            End Set
        End Property
        Private MyStat_StatMinFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_min.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("d) Statistic image - mean of all")>
        <ComponentModel.Description("Store a FITS file with the total mean - leave blank to skip storing")>
        Public Property Stat_StatMeanFile As String
            Get
                Return MyStat_StatMeanFile
            End Get
            Set(value As String)
                MyStat_StatMeanFile = value
            End Set
        End Property
        Private MyStat_StatMeanFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_mean.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("e) Statistic image - sigma of all")>
        <ComponentModel.Description("Store a FITS file with the total sigma - leave blank to skip storing")>
        Public Property Stat_StatSigmaFile As String
            Get
                Return MyStat_StatSigmaFile
            End Get
            Set(value As String)
                MyStat_StatSigmaFile = value
            End Set
        End Property
        Private MyStat_StatSigmaFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_sigma.fits")

    End Class

    Private Config As New cConfig
    Private WithEvents Stacker As New cStacker
    Private LogContent As New System.Text.StringBuilder
    Private WithEvents FITSGrepper As New cFITSGrepper

    Private Sub frmMultiFileAction_Load(sender As Object, e As EventArgs) Handles Me.Load
        pgMain.SelectedObject = Config
        With adgvMain
            .Columns.Add(New DataGridViewCheckBoxColumn) : .Columns(0).HeaderText = "Process"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(1).HeaderText = "File"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(2).HeaderText = "Delta X"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(3).HeaderText = "Delta Y"
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        End With
    End Sub

    Private Sub DropOccured(Files() As String)
        'Handle drag-and-drop for all dropped FIT(s) files
        Dim AllFiles As New List(Of String)
        For Each File As String In Files
            If System.IO.Path.GetExtension(File).ToUpper.StartsWith(".FIT") Then AllFiles.Add(File)
        Next File
        AllFiles.Sort()
        'Grep all headers
        FITSGrepper.Grep(AllFiles)
        'Display results
        For Each GreppedFile As String In FITSGrepper.AllFileHeaders.Keys
            adgvMain.Rows.Add(New Object() {True, GreppedFile})
        Next GreppedFile
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

    Private Sub adgvMain_DragEnter(sender As Object, e As DragEventArgs) Handles adgvMain.DragEnter
        e.Effect = Windows.Forms.DragDropEffects.None
        Dim X As Dictionary(Of String, Object) = GetDropContent(e)
        'File
        If e.Data.GetDataPresent(Windows.Forms.DataFormats.FileDrop) Then
            If CType(e.Data.GetData(Windows.Forms.DataFormats.FileDrop), String()).Length >= 1 Then
                e.Effect = Windows.Forms.DragDropEffects.All
            End If
        End If
    End Sub

    Private Sub adgvMain_DragDrop(sender As Object, e As DragEventArgs) Handles adgvMain.DragDrop
        Dim X As Dictionary(Of String, Object) = GetDropContent(e)
        Try
            'Drop files direct from the explorer
            If X.ContainsKey("FileDrop") Then
                Dim DroppedFiles As String() = CType(X("FileDrop"), String())
                DropOccured(DroppedFiles)
            End If
            'Drop text that contains file paths
            If X.ContainsKey("Text") Then
                Dim Content As New List(Of String)
                Content.Add(CStr(X("Text")))
                DropOccured(Content.ToArray)
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub adgvMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles adgvMain.CellContentClick
        If e.ColumnIndex = 0 Then
            Dim Cell As DataGridViewCheckBoxCell = CType(adgvMain.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex), DataGridViewCheckBoxCell)
            Cell.Value = Not CType(Cell.Value, Boolean)
        End If
    End Sub

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click

        Stacker.CalcTotalStatistics = Config.Stat_CalcHist
        Stacker.FileToWrite_Min = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMinFile)
        Stacker.FileToWrite_Max = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMaxFile)
        Stacker.FileToWrite_Mean = IO.Path.Combine(Config.Gen_root, Config.Stat_StatMeanFile)
        Stacker.FileToWrite_Sigma = IO.Path.Combine(Config.Gen_root, Config.Stat_StatSigmaFile)
        Stacker.StackFITSFiles(GetCheckedFiles)

    End Sub

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

    Private Sub btnClearLog_Click(sender As Object, e As EventArgs) Handles btnClearLog.Click
        LogContent.Clear()
        UpdateLog()
    End Sub

    Private Sub Stacker_Message(Text As String) Handles Stacker.Message
        Log(Text)
    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        Me.Close()
    End Sub

    '==============================================================================================

    '''<summary>Stack files.</summary>
    Private Sub btnStackCorr_Click(sender As Object, e As EventArgs) Handles btnStackCorr.Click

        Dim FilesToStack As List(Of String) = GetCheckedFiles()
        Dim FileContent(,) As Single = {}
        Dim RefData(,) As Single = {}
        Dim StackedData(,) As Single = {}
        Dim BaseROI As System.Drawing.Rectangle
        Dim Shift As System.Drawing.Point

        LogContent.Clear()
        Dim FileCount As Integer = 0

        Dim UsedROIs As New List(Of Drawing.Rectangle)
        For Each File As String In FilesToStack

            FileCount += 1
            Log("Processing file " & FileCount.ValRegIndep & "/" & FilesToStack.Count.ValRegIndep & ": " & File)
            Dim CurrentRow As Integer = GetFileRow(File)
            adgvMain.Rows(CurrentRow).Cells(1).Style.BackColor = Color.Red
            adgvMain.Rows(CurrentRow).Cells(2).Value = "---"
            adgvMain.Rows(CurrentRow).Cells(3).Value = "---"
            DE()

            'Read in the file, run a Bin2 with max removal if selected (to remove hot pixel)
            Dim FITSReader As New cFITSReader
            If Config.Stack_RunBin2 = True Then
                FileContent = FITSReader.ReadInUInt16(File, True, True).Bin2MaxOut32f
            Else
                AIS.DB.IPP.Convert(FITSReader.ReadInUInt16(File, True, True), FileContent)
            End If

            'For the 1st file, store it as reference with shift (0,0), for the others, run a correlation
            If FileCount = 1 Then
                RefData = AIS.DB.IPP.Copy(FileContent)
                Shift = New Drawing.Point(0, 0)
                BaseROI = New Drawing.Rectangle(Config.Stack_ShiftMargin, Config.Stack_ShiftMargin, RefData.GetUpperBound(0) - (2 * Config.Stack_ShiftMargin), RefData.GetUpperBound(1) - (2 * Config.Stack_ShiftMargin))
            Else
                Shift = cRegistration.MultiAreaCorrelate(RefData, FileContent, Config.Stack_XCorrSegmentation, Config.Stack_TlpReduction)
            End If
            adgvMain.Rows(CurrentRow).Cells(2).Value = Shift.X
            adgvMain.Rows(CurrentRow).Cells(3).Value = Shift.Y
            adgvMain.Rows(CurrentRow).Cells(1).Style.BackColor = Color.White
            DE()

            'Calculate the wanted ROI, get the ROI from the data and store
            Dim ThisROI As Drawing.Rectangle = ShiftROI(BaseROI, Shift.X, Shift.Y)
            UsedROIs.Add(ThisROI)
            Dim DataToStack(,) As Single = FileContent.GetROI(ThisROI)
            AIS.DB.IPP.Add(DataToStack, StackedData)

        Next File

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
        adgvMain.Rows.Clear()
    End Sub

    Private Sub tsmiFile_OpenStackDir_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenStackDir.Click
        Process.Start(Config.Gen_root)
    End Sub

End Class