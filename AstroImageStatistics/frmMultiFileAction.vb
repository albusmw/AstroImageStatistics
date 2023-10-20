Option Explicit On
Option Strict On

'''<summary>This form should handle actions that apply to multiple files, e.g. dark statistics, hot pixel search, basic stacking, ...</summary>
Public Class frmMultiFileAction

    Private LogContent As New System.Text.StringBuilder
    Private WithEvents FITSGrepper As New cFITSGrepper
    Private LoopStat As AstroNET.Statistics.sStatistics

    Private Sub frmMultiFileAction_Load(sender As Object, e As EventArgs) Handles Me.Load
        With adgvMain
            .Columns.Add(New DataGridViewCheckBoxColumn) : .Columns(0).HeaderText = "Process"
            .Columns.Add(New DataGridViewTextBoxColumn) : .Columns(1).HeaderText = "File"
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        End With
    End Sub

    Private Sub DropOccured(Files() As String)
        'Handle drag-and-drop for all dropped FIT(s) files
        Dim AllFiles As New List(Of String)
        For Each File As String In Files
            If System.IO.Path.GetExtension(File).ToUpper.StartsWith(".FIT") Then AllFiles.Add(File)
        Next File
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

        Dim DataAsUInt16(,) As UInt16 = {}
        Dim DataAsUInt32(,) As UInt32 = {}      'read data as UInt32 for IPP processing
        Dim DataAsDouble(,) As Double = {}
        Dim PowerSum(,) As Double = {}
        Dim TotalSum(,) As UInt32 = {}          'per-pixel sum of all images
        Dim MaxEvery(,) As UInt32 = {}
        Dim MinEvery(,) As UInt32 = {}

        LoopStat = New AstroNET.Statistics.sStatistics

        'Get all files to run
        Dim AllFiles As New List(Of String)
        For Idx As Integer = 0 To adgvMain.RowCount - 1
            Dim Use As Boolean = CType(adgvMain.Rows(Idx).Cells(0).Value, Boolean)
            Dim FileName As String = CStr(adgvMain.Rows(Idx).Cells(1).Value)
            If Use Then AllFiles.Add(FileName)
        Next Idx

        'Run files
        For Idx As Integer = 0 To AllFiles.Count - 1

            'Display file processing
            Dim FileName As String = AllFiles(Idx)
            Log("Reading file " & (Idx + 1).ValRegIndep & "/" & AllFiles.Count.ValRegIndep & ":  <" & FileName & ">")

            'Read the FITS header
            Dim DataStartPos As Integer = 0
            Dim FITSHeader As New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))
            Dim FITSHeaderDict As Dictionary(Of eFITSKeywords, Object) = FITSHeader.GetCardsAsDictionary

            'Read content - TODO: ensure that only UInt16 is processed!
            Dim FITSReader As New cFITSReader
            If cbCalcHisto.Checked Then
                Dim Container As New AstroNET.Statistics(AIS.DB.IPP)
                Container.ResetAllProcessors()
                Container.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileName, AIS.DB.UseIPP, AIS.DB.ForceDirect)
                AIS.DB.IPP.Convert(Container.DataProcessor_UInt16.ImageData(0).Data, DataAsUInt32)
                Dim SingleStat As AstroNET.Statistics.sStatistics = Container.ImageStatistics()
                LoopStat = AstroNET.Statistics.CombineStatistics(SingleStat.DataMode, SingleStat, LoopStat)
            Else
                DataAsUInt16 = FITSReader.ReadInUInt16(FileName, AIS.DB.UseIPP, AIS.DB.ForceDirect)
                AIS.DB.IPP.Convert(DataAsUInt16, DataAsUInt32)
            End If
            AIS.DB.IPP.Convert(DataAsUInt32, DataAsDouble)

            'Sum up all data and get total MAX and total MIN
            AIS.DB.IPP.Add(DataAsUInt32, TotalSum)
            If Idx = 0 Then
                AIS.DB.IPP.Copy(DataAsUInt32, MaxEvery)
                AIS.DB.IPP.Copy(DataAsUInt32, MinEvery)
            Else
                AIS.DB.IPP.MaxEvery(DataAsUInt32, MaxEvery)
                AIS.DB.IPP.MinEvery(DataAsUInt32, MinEvery)
            End If

            'Square sum - factor of 2 too much as we add Re^2 + Re^2
            AIS.DB.IPP.PowerSpectr(DataAsDouble, DataAsDouble, DataAsDouble)
            AIS.DB.IPP.Add(DataAsDouble, PowerSum)

        Next Idx

        'Get sigma of each pixel - calculation is checked and OK
        Log("Get sigma of each pixel")
        Dim Norm As Double = AllFiles.Count
        Dim Sigma(,) As Double = {}
        Dim SumSum(,) As Double = {}
        AIS.DB.IPP.DivC(PowerSum, 2)            'correct Square sum by a factor of 2
        Sigma = AIS.DB.IPP.Copy(PowerSum)       'E[X^2]
        AIS.DB.IPP.Convert(TotalSum, SumSum)    'E[X]
        AIS.DB.IPP.Mul(SumSum, SumSum)          'E^2[X]
        AIS.DB.IPP.DivC(SumSum, Norm)           'E^2[X]/count
        AIS.DB.IPP.Sub(SumSum, PowerSum)        'PowerSum = E[X^2] - E^2[x]
        AIS.DB.IPP.DivC(PowerSum, Norm - 1)
        AIS.DB.IPP.Sqrt(PowerSum)

        'Calculate MAX-MIN range for each pixel
        Log("Calculate MAX-MIN range for each pixel")
        Dim MaxMin(,) As UInt32 = {}
        Dim MaxMin_min As UInt32 = UInt32.MaxValue
        Dim MaxMin_max As UInt32 = UInt32.MinValue
        AIS.DB.IPP.Copy(MaxEvery, MaxMin)
        AIS.DB.IPP.Sub(MinEvery, MaxMin)
        AIS.DB.IPP.MinMax(MaxMin, MaxMin_min, MaxMin_max)

        'Save if requested
        Log("Saving data ...")
        If cbSaveMin.Checked = True Then
            Log(" -> Min")
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, "Min.fits")
            Dim DataToSave(TotalSum.GetUpperBound(0), TotalSum.GetUpperBound(1)) As UInt16
            For Idx1 As Integer = 0 To DataToSave.GetUpperBound(0)
                For Idx2 As Integer = 0 To DataToSave.GetUpperBound(1)
                    DataToSave(Idx1, Idx2) = CType(MinEvery(Idx1, Idx2), UInt16)
                Next Idx2
            Next Idx1
            cFITSWriter.Write(FileToGenerate, DataToSave, cFITSWriter.eBitPix.Int16)
        End If
        If cbSaveMax.Checked = True Then
            Log(" -> Max")
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, "Max.fits")
            Dim DataToSave(TotalSum.GetUpperBound(0), TotalSum.GetUpperBound(1)) As UInt16
            For Idx1 As Integer = 0 To DataToSave.GetUpperBound(0)
                For Idx2 As Integer = 0 To DataToSave.GetUpperBound(1)
                    DataToSave(Idx1, Idx2) = CType(MaxEvery(Idx1, Idx2), UInt16)
                Next Idx2
            Next Idx1
            cFITSWriter.Write(FileToGenerate, DataToSave, cFITSWriter.eBitPix.Int16)
        End If
        If cbSaveMean.Checked = True Then
            Log(" -> Mean")
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, "Mean.fits")
            Dim DataToSave(TotalSum.GetUpperBound(0), TotalSum.GetUpperBound(1)) As UInt16
            For Idx1 As Integer = 0 To DataToSave.GetUpperBound(0)
                For Idx2 As Integer = 0 To DataToSave.GetUpperBound(1)
                    DataToSave(Idx1, Idx2) = CType(Math.Round(TotalSum(Idx1, Idx2) / AllFiles.Count), UInt16)
                Next Idx2
            Next Idx1
            cFITSWriter.Write(FileToGenerate, DataToSave, cFITSWriter.eBitPix.Int16)
        End If
        If cbSaveSigma.Checked = True Then
            Log(" -> Sigma")
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, "Sigma.fits")
            Dim DataToSave(TotalSum.GetUpperBound(0), TotalSum.GetUpperBound(1)) As UInt16
            For Idx1 As Integer = 0 To DataToSave.GetUpperBound(0)
                For Idx2 As Integer = 0 To DataToSave.GetUpperBound(1)
                    DataToSave(Idx1, Idx2) = CType(PowerSum(Idx1, Idx2), UInt16)
                Next Idx2
            Next Idx1
            cFITSWriter.Write(FileToGenerate, DataToSave, cFITSWriter.eBitPix.Int16)
        End If

        Log("END.")

    End Sub

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

    '==============================================================================================

End Class