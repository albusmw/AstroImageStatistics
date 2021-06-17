Option Explicit On
Option Strict On

Public Class MainForm

    Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal pDst As IntPtr,
                                                                 ByVal pSrc As IntPtr,
                                                                 ByVal ByteLen As Long)

    Private LastOpenedFiles As New frmLastOpenedFiles

    Const UInt32One As UInt32 = 1

    Private LogContent As New System.Text.StringBuilder

    Private DB As New cDB

    '''<summary>Drag-and-drop handler.</summary>
    Private WithEvents DD As Ato.DragDrop

    '''<summary>Last file opened.</summary>
    Private LastFile As String = String.Empty
    '''<summary>Last FITS header.</summary>
    Private LastFITSHeader As cFITSHeaderParser

    '''<summary>Evaluation results for 1 file (FITS header and statistics).</summary>
    Private Structure sFileEvalOut
        Public Header As Dictionary(Of eFITSKeywords, Object)
        Public Statistics As AstroNET.Statistics.sStatistics
    End Structure

    '''<summary>Statistics of all processed files.</summary>
    Private AllFiles As New Dictionary(Of String, sFileEvalOut)

    '''<summary>Detailed evaluation reults.</summary>
    Public Structure sFileEvalResults
        '''<summary>Statistics for pixel with identical Y value.</summary>
        Dim StatPerRow() As Ato.cSingleValueStatistics
        '''<summary>Statistics for pixel with identical X value.</summary>
        Dim StatPerCol() As Ato.cSingleValueStatistics
        '''<summary>Raw vignette.</summary>
        Public Vig_RawData As Dictionary(Of Double, Double)
        '''<summary>Reduced and binned vignette.</summary>
        Public Vig_BinUsedData As Dictionary(Of Double, Double)
        '''<summary>Fitted vignette.</summary>
        Public Vig_Fitting() As Double
    End Structure

    '''<summary>Statistics processor (for the last file).</summary>
    Public CurrentData As AstroNET.Statistics
    '''<summary>Statistics of the last plot.</summary>
    Public CurrentStatistics As AstroNET.Statistics.sStatistics
    '''<summary>Statistics of the last plot.</summary>
    Public CurrentFileEvalResults As sFileEvalResults

    '''<summary>Storage for a simple stack processing.</summary>
    Private StackingStatistics(,) As Ato.cSingleValueStatistics

    Private Sub tsmiFile_Open_Click(sender As Object, e As EventArgs) Handles tsmiFile_Open.Click
        ofdMain.Filter = "FIT(s) files (FIT/FITS/FTS)|*.FIT;*.FITS;*.FTS"
        If ofdMain.ShowDialog <> DialogResult.OK Then Exit Sub
        For Each File As String In ofdMain.FileNames
            LoadFile(File, CurrentData)
        Next File
    End Sub

    '''<summary>Load the given file.</summary>
    '''<param name="FileName">File to read in.</param>
    '''<returns>Position where the data start.</returns>
    Private Function LoadFile(ByVal FileName As String, ByRef Container As AstroNET.Statistics) As Integer

        Dim FileNameOnly As String = System.IO.Path.GetFileName(FileName)
        Dim Stopper As New cStopper
        Dim FITSReader As New cFITSReader
        Dim DataStartPos As Integer = 0

        Container = New AstroNET.Statistics(DB.IPP)

        Running()

        If DB.AutoClearLog = True Then
            LogContent.Clear()
            UpdateLog()
        End If

        '=========================================================================================================
        'Log header data and detect if NAXIS3 is set
        Log("Loading file <" & FileName & "> ...")
        Log("  -> <" & System.IO.Path.GetFileNameWithoutExtension(FileName) & ">")
        LastFile = FileName
        Log("FITS header:")
        LastFITSHeader = New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))
        Dim FITSHeaderDict As Dictionary(Of eFITSKeywords, Object) = LastFITSHeader.GetCardsAsDictionary
        Dim ContentToPrint As New List(Of String)
        For Each Entry As eFITSKeywords In FITSHeaderDict.Keys
            ContentToPrint.Add("  " & FITSKeyword.GetKeywords(Entry)(0).PadRight(10) & "=" & CStr(FITSHeaderDict(Entry)).Trim.PadLeft(40))
        Next Entry
        Log(ContentToPrint)
        Log(New String("-"c, 107))

        '=========================================================================================================
        'Read the FITS data

        Container.ResetAllProcessors()
        Select Case LastFITSHeader.BitPix
            Case 8
                Container.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt8(FileName, DB.UseIPP)
            Case 16
                With Container.DataProcessor_UInt16
                    .ImageData(0).Data = FITSReader.ReadInUInt16(FileName, DB.UseIPP, DB.ForceDirect)
                    If LastFITSHeader.NAXIS3 > 1 Then
                        For Idx As Integer = 1 To LastFITSHeader.NAXIS3 - 1
                            DataStartPos += CInt(.ImageData(Idx - 1).Length * LastFITSHeader.BytesPerSample)        'move to next plane
                            .ImageData(Idx).Data = FITSReader.ReadInUInt16(FileName, DataStartPos, DB.UseIPP, DB.ForceDirect)
                        Next Idx
                    End If
                End With
            Case 32
                Container.DataProcessor_Int32.ImageData = FITSReader.ReadInInt32(FileName, DB.UseIPP)
            Case -32
                With Container.DataProcessor_Float32
                    .ImageData(0).Data = FITSReader.ReadInFloat32(FileName, DB.UseIPP)
                    If LastFITSHeader.NAXIS3 > 1 Then
                        For Idx As Integer = 1 To LastFITSHeader.NAXIS3 - 1
                            DataStartPos += CInt(.ImageData(Idx - 1).Length * LastFITSHeader.BytesPerSample)        'move to next plane
                            .ImageData(Idx).Data = FITSReader.ReadInFloat32(FileName, DB.UseIPP)
                        Next Idx
                    End If
                End With
            Case Else
                Log("!!! File format <" & LastFITSHeader.BitPix.ToString.Trim & "> not yet supported!")
                Return -1
        End Select
        Stopper.Stamp(FileNameOnly & ": Reading")

        '=========================================================================================================
        'Calculate the statistics

        CalculateStatistics(Container, DB.BayerPatternNames, CurrentStatistics)
        Stopper.Stamp(FileNameOnly & ": Statistics")

        'Record statistics
        Dim RecStat As New sFileEvalOut
        RecStat.Header = FITSHeaderDict
        RecStat.Statistics = CurrentStatistics
        If AllFiles.ContainsKey(FileName) = False Then
            AllFiles.Add(FileName, RecStat)
        Else
            AllFiles(FileName) = RecStat
        End If

        'Run the "stacking" (statistics for each point) is selected
        If DB.Stacking = True Then
            'Init new
            If IsNothing(StackingStatistics) = True Then
                ReDim StackingStatistics(Container.NAXIS1 - 1, Container.NAXIS2 - 1)
                For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                    For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                        StackingStatistics(Idx1, Idx2) = New Ato.cSingleValueStatistics(False)
                    Next Idx2
                Next Idx1
            End If
            'Add up statistics if dimension is matching
            If StackingStatistics.GetUpperBound(0) = Container.NAXIS1 - 1 And StackingStatistics.GetUpperBound(1) = Container.NAXIS2 - 1 Then
                Select Case LastFITSHeader.BitPix
                    Case 8, 16
                        For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                            For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                                StackingStatistics(Idx1, Idx2).AddValue(Container.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2))
                            Next Idx2
                        Next Idx1
                    Case 32
                        For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                            For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                                StackingStatistics(Idx1, Idx2).AddValue(Container.DataProcessor_Int32.ImageData(Idx1, Idx2))
                            Next Idx2
                        Next Idx1
                End Select
            Else
                Log("!!! Dimension mismatch between the different images!")
            End If
            Stopper.Stamp(FileNameOnly & ": Stacking")
        End If

        '=========================================================================================================
        'Plot statistics and remember this file as last processed file
        If DB.AutoOpenStatGraph = True Then PlotStatistics(FileName, CurrentStatistics)
        'Me.Focus()

        'Store recent file
        DB.StoreRecentFile(FileName)

        Idle()
        Return DataStartPos

    End Function

    '''<summary>Run the statistics calcuation.</summary>
    Private Sub CalculateStatistics(ByRef Container As AstroNET.Statistics, ByVal ChannelNames As List(Of String), ByRef Stat As AstroNET.Statistics.sStatistics)
        Dim Indent As String = "  "
        'Calculate statistics
        Stat = Container.ImageStatistics(Container.DataFixFloat)
        'Log statistics
        Log("Statistics:")
        Log(Indent, Stat.StatisticsReport(DB.MonoStatistics, DB.BayerStatistics, ChannelNames).ToArray())
        Log(New String("="c, 109))
    End Sub

    '''<summary>A point of the graph was selected - give information.</summary>
    Public Function PointValueHandler(ByVal Curve As String, ByVal X As Double, ByVal Y As Double) As String
        Dim Text As New List(Of String)
        Dim Indent As String = "   "
        Text.Add("Curve <" & Curve & ">")
        Text.Add("X <" & X.ValRegIndep & ">")
        Text.Add("Y <" & Y.ValRegIndep & ">")
        Dim HistKey As Long = CLng(X)
        If Curve.Contains("[") = True Then
            Dim HIdx0 As Integer = CInt(Curve.Substring(Curve.IndexOf("[") + 1, 1))
            Dim HIdx1 As Integer = CInt(Curve.Substring(Curve.IndexOf("[") + 3, 1))
        End If
        With CurrentStatistics
            'Get the histogram data from mono histo
            Text.Add("MONO:")
            If Not IsNothing(.MonochromHistogram_Int) Then
                If .MonochromHistogram_Int.ContainsKey(HistKey) Then
                    Dim ValuesAbove As UInt64 = .MonochromHistogram_Int_ValuesAbove(HistKey)
                    Text.Add(Indent & (100 * (X / .MonoStatistics_Int.Max.Key)).ValRegIndep("0.00") & " % of MAX")
                    Text.Add(Indent & (100 * (X / UInt16.MaxValue)).ValRegIndep("0.00") & " % of UInt16")
                    Text.Add(Indent & ValuesAbove.ValRegIndep & " values are greater")
                    Text.Add(Indent & (100 * (ValuesAbove / .MonoStatistics_Int.Samples)).ValRegIndep("0.00") & " % are greater")
                End If
                'Get the histogram data from Text "G1[1,0]"
                For HIdx0 As Integer = 0 To 1
                    For HIdx1 As Integer = 0 To 1
                        Text.Add("BAYER[" & HIdx0.ValRegIndep & ":" & HIdx1.ValRegIndep & "]:")
                        If .BayerHistograms_Int_Present(HIdx0, HIdx1, HistKey) Then
                            Dim ValuesAbove As UInt64 = .BayerHistograms_Int_ValuesAbove(HIdx0, HIdx1, HistKey)
                            Text.Add(Indent & (100 * (X / .BayerStatistics_Int(HIdx0, HIdx1).Max.Key)).ValRegIndep("0.00") & " % of MAX")                'percentage of maximum value
                            Text.Add(Indent & (100 * (X / UInt16.MaxValue)).ValRegIndep("0.00") & " % of UInt16")                                        'percentage of UInt16 range
                            Text.Add(Indent & ValuesAbove.ValRegIndep & " values are greater")
                            Text.Add(Indent & (100 * (ValuesAbove / .BayerStatistics_Int(HIdx0, HIdx1).Samples)).ValRegIndep("0.00") & " % are greater")
                        Else
                            Text.Add(Indent & "-----")
                            Text.Add(Indent & "-----")
                            Text.Add(Indent & "-----")
                            Text.Add(Indent & "-----")
                        End If
                    Next HIdx1
                Next HIdx0
            End If
        End With
        tbDetails.Text = Join(Text.ToArray, System.Environment.NewLine)
        Return Curve
    End Function

    '''<summary>Open a simple form with a ZEDGraph on it and plots the statistical data.</summary>
    '''<param name="FileName">Filename that is plotted (indicated in the header).</param>
    '''<param name="Stats">Statistics data to plot.</param>
    Private Sub PlotStatistics(ByVal FileName As String, ByRef Stats As AstroNET.Statistics.sStatistics)
        MyDB.AllPlots.Add(New cZEDGraphForm)
        Dim Disp As cZEDGraphForm = MyDB.AllPlots.Item(MyDB.AllPlots.Count - 1)
        AddHandler Disp.PointValueHandler, AddressOf PointValueHandler
        Disp.PlotData("Test", New Double() {1, 2, 3, 4}, Color.Red)
        Dim XAxisMargin As Integer = 128                                    'axis margin to see the most outer values
        Select Case Stats.DataMode
            Case AstroNET.Statistics.sStatistics.eDataMode.Fixed
                'Plot histogram
                Disp.Plotter.Clear()
                If IsNothing(Stats.BayerHistograms_Int) = False And DB.BayerStatistics Then
                    Disp.Plotter.PlotXvsY(DB.BayerPatternName(0) & "[0,0]", Stats.BayerHistograms_Int(0, 0), 1, New cZEDGraphService.sGraphStyle(Color.Red, DB.PlotStyle, 1))
                    Disp.Plotter.PlotXvsY(DB.BayerPatternName(1) & "[0,1]", Stats.BayerHistograms_Int(0, 1), 1, New cZEDGraphService.sGraphStyle(Color.LightGreen, DB.PlotStyle, 1))
                    Disp.Plotter.PlotXvsY(DB.BayerPatternName(2) & "[1,0]", Stats.BayerHistograms_Int(1, 0), 1, New cZEDGraphService.sGraphStyle(Color.Green, DB.PlotStyle, 1))
                    Disp.Plotter.PlotXvsY(DB.BayerPatternName(3) & "[1,1]", Stats.BayerHistograms_Int(1, 1), 1, New cZEDGraphService.sGraphStyle(Color.Blue, DB.PlotStyle, 1))
                End If
                If IsNothing(Stats.MonochromHistogram_Int) = False And DB.MonoStatistics Then
                    Disp.Plotter.PlotXvsY("Mono histo", Stats.MonochromHistogram_Int, 1, New cZEDGraphService.sGraphStyle(Color.Black, DB.PlotStyle, 1))
                End If
                Disp.Plotter.ManuallyScaleXAxis(Stats.MonoStatistics_Int.Min.Key - XAxisMargin, Stats.MonoStatistics_Int.Max.Key + XAxisMargin)
            Case AstroNET.Statistics.sStatistics.eDataMode.Float
                'Plot histogram
                Disp.Plotter.Clear()
                If IsNothing(Stats.BayerHistograms_Float32) = False And DB.BayerStatistics Then
                    Disp.Plotter.PlotXvsY(DB.BayerPatternName(0) & "[0,0]", Stats.BayerHistograms_Float32(0, 0), 1, New cZEDGraphService.sGraphStyle(Color.Red, DB.PlotStyle, 1))
                    Disp.Plotter.PlotXvsY(DB.BayerPatternName(1) & "[0,1]", Stats.BayerHistograms_Float32(0, 1), 1, New cZEDGraphService.sGraphStyle(Color.LightGreen, DB.PlotStyle, 1))
                    Disp.Plotter.PlotXvsY(DB.BayerPatternName(2) & "[1,0]", Stats.BayerHistograms_Float32(1, 0), 1, New cZEDGraphService.sGraphStyle(Color.Green, DB.PlotStyle, 1))
                    Disp.Plotter.PlotXvsY(DB.BayerPatternName(3) & "[1,1]", Stats.BayerHistograms_Float32(1, 1), 1, New cZEDGraphService.sGraphStyle(Color.Blue, DB.PlotStyle, 1))
                End If
                If IsNothing(Stats.MonochromHistogram_Float32) = False And DB.MonoStatistics Then
                    Disp.Plotter.PlotXvsY("Mono histo", Stats.MonochromHistogram_Float32, 1, New cZEDGraphService.sGraphStyle(Color.Black, DB.PlotStyle, 1))
                End If
                Disp.Plotter.ManuallyScaleXAxis(Stats.MonoStatistics_Int.Min.Key - XAxisMargin, Stats.MonoStatistics_Int.Max.Key + XAxisMargin)
        End Select
        Disp.Plotter.AutoScaleYAxisLog()
        Disp.Plotter.GridOnOff(True, True)
        Disp.Plotter.ForceUpdate()
        'Set style of the window
        Disp.Plotter.SetCaptions(String.Empty, "Pixel value", "# of pixel with this value")
        Disp.Plotter.MaximizePlotArea()
        Disp.Hoster.Text = FileName
        Disp.Hoster.Icon = Me.Icon
        Disp.Tag = "Statistics"
        'Position window below the main window
        If DB.StackGraphs = True Then
            Disp.Hoster.Left = Me.Left
            Disp.Hoster.Top = Me.Top + Me.Height
            Disp.Hoster.Height = Me.Height
            Disp.Hoster.Width = Me.Width
        End If
    End Sub

    Private Sub PlotStatistics(ByVal FileName As String, ByRef Stats() As Ato.cSingleValueStatistics)
        Dim Disp As New cZEDGraphForm
        Disp.PlotData("Test", New Double() {1, 2, 3, 4}, Color.Red)
        'Plot data
        Dim XAxis() As Double = Ato.cSingleValueStatistics.GetAspectVectorXAxis(Stats)
        Disp.Plotter.Clear()
        Disp.Plotter.PlotXvsY("Mean", XAxis, Ato.cSingleValueStatistics.GetAspectVector(Stats, Ato.cSingleValueStatistics.eAspects.Mean), New cZEDGraphService.sGraphStyle(Color.Black, DB.PlotStyle, 1))
        Disp.Plotter.PlotXvsY("Max", XAxis, Ato.cSingleValueStatistics.GetAspectVector(Stats, Ato.cSingleValueStatistics.eAspects.Maximum), New cZEDGraphService.sGraphStyle(Color.Red, DB.PlotStyle, 1))
        Disp.Plotter.PlotXvsY("Min", XAxis, Ato.cSingleValueStatistics.GetAspectVector(Stats, Ato.cSingleValueStatistics.eAspects.Minimum), New cZEDGraphService.sGraphStyle(Color.Green, DB.PlotStyle, 1))
        Disp.Plotter.PlotXvsY("Sigma", XAxis, Ato.cSingleValueStatistics.GetAspectVector(Stats, Ato.cSingleValueStatistics.eAspects.Sigma), New cZEDGraphService.sGraphStyle(Color.Orange, DB.PlotStyle, 1), True)
        Disp.Plotter.ManuallyScaleXAxis(XAxis(0), XAxis(XAxis.GetUpperBound(0)))
        Disp.Plotter.GridOnOff(True, True)
        Disp.Plotter.ForceUpdate()
        'Set style of the window
        Disp.Plotter.SetCaptions(String.Empty, "Pixel index", "Statistics value")
        Disp.Plotter.MaximizePlotArea()
        Disp.Hoster.Text = FileName
        Disp.Hoster.Icon = Me.Icon
        'Position window below the main window
        Disp.Hoster.Left = Me.Left
        Disp.Hoster.Top = Me.Top + Me.Height
        Disp.Hoster.Height = Me.Height
        Disp.Hoster.Width = Me.Width
    End Sub

    Private Sub PlotStatistics(ByVal FileName As String, ByRef Stats As Dictionary(Of Double, AstroImageStatistics.AstroNET.Statistics.sSingleChannelStatistics_Int))
        Dim Disp As New cZEDGraphForm
        Disp.PlotData("Test", New Double() {1, 2, 3, 4}, Color.Red)
        'Plot data
        Dim XAxis As New List(Of Double)
        Dim YAxis As New List(Of Double)
        For Each Entry As Double In Stats.Keys
            XAxis.Add(Entry)
            YAxis.Add(Stats(Entry).Mean)
        Next Entry
        Disp.Plotter.Clear()
        Disp.Plotter.PlotXvsY("StdDev", XAxis.ToArray, YAxis.ToArray, New cZEDGraphService.sGraphStyle(Color.Black, cZEDGraphService.eCurveMode.Dots, 1))
        Disp.Plotter.GridOnOff(True, True)
        Disp.Plotter.ForceUpdate()
        'Set style of the window
        Disp.Plotter.SetCaptions(String.Empty, "Gain", "StdDev")
        Disp.Plotter.MaximizePlotArea()
        Disp.Hoster.Text = FileName
        Disp.Hoster.Icon = Me.Icon
        'Position window below the main window
        Disp.Hoster.Left = Me.Left
        Disp.Hoster.Top = Me.Top + Me.Height
        Disp.Hoster.Height = Me.Height
        Disp.Hoster.Width = Me.Width
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Get build data
        Dim BuildDate As String = String.Empty
        Dim AllResources As String() = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames
        For Each Entry As String In AllResources
            If Entry.EndsWith(".BuildDate.txt") Then
                BuildDate = " (Build of " & (New System.IO.StreamReader(System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(Entry)).ReadToEnd.Trim).Replace(",", ".") & ")"
                Exit For
            End If
        Next Entry
        Me.Text &= BuildDate

        'Load IPP
        Dim IPPLoadError As String = String.Empty
        Dim IPPPathToUse As String = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(DB.MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            DB.IPP = New cIntelIPP(IPPPathToUse)
            cFITSWriter.UseIPPForWriting = True
        Else
            cFITSWriter.UseIPPForWriting = False
        End If
        cFITSWriter.IPPPath = DB.IPP.IPPPath
        cFITSReader.IPPPath = DB.IPP.IPPPath

        DD = New Ato.DragDrop(tbLogOutput, False)
        pgMain.SelectedObject = DB

        'If a file is droped to the EXE (icon), use this as filename
        With My.Application
            If .CommandLineArgs.Count > 0 Then
                Dim FileName As String = .CommandLineArgs.Item(0)
                If System.IO.File.Exists(FileName) Then LoadFile(FileName, CurrentData)
            End If
        End With

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub OpenEXELocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenEXELocationToolStripMenuItem.Click
        Process.Start(DB.MyPath)
    End Sub

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
            If LogInStatus = True Then tsslMain.Text = Text
        End With
        DE()
    End Sub

    Private Sub UpdateLog()
        With tbLogOutput
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

    Private Sub DD_DropOccured(Files() As String) Handles DD.DropOccured
        'Handle drag-and-drop for all dropped FIT(s) files
        For Each File As String In Files
            If System.IO.Path.GetExtension(File).ToUpper.StartsWith(".FIT") Then LoadFile(File, CurrentData)
        Next File
    End Sub

    Private Sub tsmiTest_WriteTestData_Click(sender As Object, e As EventArgs) Handles tsmiTest_WriteTestData.Click
        cFITSWriter.WriteTestFile_Int8(System.IO.Path.Combine(DB.MyPath, "FITS_BitPix8.FITS"))
        cFITSWriter.WriteTestFile_Int16(System.IO.Path.Combine(DB.MyPath, "FITS_BitPix16.FITS"))
        cFITSWriter.WriteTestFile_Int32(System.IO.Path.Combine(DB.MyPath, "FITS_BitPix32.FITS"))
        cFITSWriter.WriteTestFile_Float32(System.IO.Path.Combine(DB.MyPath, "FITS_BitPix32f.FITS"))
        cFITSWriter.WriteTestFile_Float64(System.IO.Path.Combine(DB.MyPath, "FITS_BitPix64f.FITS"))
        cFITSWriter.WriteTestFile_UInt16_Cross(System.IO.Path.Combine(DB.MyPath, "UInt16_Cross_mono.fits"))
        cFITSWriter.WriteTestFile_UInt16_Cross_RGB(System.IO.Path.Combine(DB.MyPath, "UInt16_Cross_rgb.fits"))
        cFITSWriter.WriteTestFile_UInt16_XYIdent(System.IO.Path.Combine(DB.MyPath, "UInt16_XYIdent.fits"))
        MsgBox("OK")
    End Sub

    Private Sub tsmiFile_OpenLastFile_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenLastFile.Click
        If System.IO.File.Exists(LastFile) = True Then
            Try
                Process.Start(LastFile)
            Catch ex As Exception
                Log("Error opening <" & LastFile & ">: <" & ex.Message & ">")
            End Try
        End If
    End Sub

    Private Sub tsmiSaveMeanFile_Click(sender As Object, e As EventArgs) Handles tsmiSaveMeanFile.Click
        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Integer
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).Mean)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(DB.MyPath, "Stacking_Mean.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Int32)
            Process.Start(FileToGenerate)
        End If
    End Sub

    Private Sub StdDevImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StdDevImageToolStripMenuItem.Click
        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Integer
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).Sigma)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(DB.MyPath, "Stacking_StdDev.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Int32)
            Process.Start(FileToGenerate)
        End If
    End Sub

    Private Sub SumImageDoubleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SumImageDoubleToolStripMenuItem.Click
        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Double
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).Mean * StackingStatistics(Idx1, Idx2).ValueCount)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(DB.MyPath, "Stacking_Sum.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Double)
            Process.Start(FileToGenerate)
        End If
    End Sub

    Private Sub MaxMinInt32ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaxMinInt32ToolStripMenuItem.Click
        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Integer
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).MaxMin)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(DB.MyPath, "Stacking_MaxMin.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Int32)
            Process.Start(FileToGenerate)
        End If
    End Sub

    Private Function StackedStatPresent() As Boolean
        If IsNothing(StackingStatistics) = True Then Return False
        If StackingStatistics.LongLength = 0 Then Return False
        Return True
    End Function

    Private Sub RowAndColumnStatisticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_RowColStat.Click

        Running()

        Dim DataProcessed As Boolean = False

        '1. Load data
        Select Case CurrentData.DataMode
            Case AstroNET.Statistics.eDataMode.UInt16
                With CurrentData.DataProcessor_UInt16
                    ReDim CurrentFileEvalResults.StatPerRow(.ImageData(0).Data.GetUpperBound(1)) : InitStat(CurrentFileEvalResults.StatPerRow)
                    ReDim CurrentFileEvalResults.StatPerCol(.ImageData(0).Data.GetUpperBound(0)) : InitStat(CurrentFileEvalResults.StatPerCol)
                    For Idx1 As Integer = 0 To .ImageData(0).Data.GetUpperBound(0)
                        For Idx2 As Integer = 0 To .ImageData(0).Data.GetUpperBound(1)
                            CurrentFileEvalResults.StatPerRow(Idx2).AddValue(.ImageData(0).Data(Idx1, Idx2))
                            CurrentFileEvalResults.StatPerCol(Idx1).AddValue(.ImageData(0).Data(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                    DataProcessed = True
                End With
            Case AstroNET.Statistics.eDataMode.UInt32
                With CurrentData.DataProcessor_UInt32
                    ReDim CurrentFileEvalResults.StatPerRow(.ImageData(0).Data.GetUpperBound(1)) : InitStat(CurrentFileEvalResults.StatPerRow)
                    ReDim CurrentFileEvalResults.StatPerCol(.ImageData(0).Data.GetUpperBound(0)) : InitStat(CurrentFileEvalResults.StatPerCol)
                    For Idx1 As Integer = 0 To .ImageData(0).Data.GetUpperBound(0)
                        For Idx2 As Integer = 0 To .ImageData(0).Data.GetUpperBound(1)
                            CurrentFileEvalResults.StatPerRow(Idx2).AddValue(.ImageData(0).Data(Idx1, Idx2))
                            CurrentFileEvalResults.StatPerCol(Idx1).AddValue(.ImageData(0).Data(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                    DataProcessed = True
                End With
            Case AstroNET.Statistics.eDataMode.Int32
                With CurrentData.DataProcessor_Int32
                    ReDim CurrentFileEvalResults.StatPerRow(.ImageData.GetUpperBound(1)) : InitStat(CurrentFileEvalResults.StatPerRow)
                    ReDim CurrentFileEvalResults.StatPerCol(.ImageData.GetUpperBound(0)) : InitStat(CurrentFileEvalResults.StatPerCol)
                    For Idx1 As Integer = 0 To .ImageData.GetUpperBound(0)
                        For Idx2 As Integer = 0 To .ImageData.GetUpperBound(1)
                            CurrentFileEvalResults.StatPerRow(Idx2).AddValue(.ImageData(Idx1, Idx2))
                            CurrentFileEvalResults.StatPerCol(Idx1).AddValue(.ImageData(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                    DataProcessed = True
                End With
        End Select

        '2. Plot data
        If DataProcessed = True Then
            PlotStatistics(LastFile & " - ROW STAT", CurrentFileEvalResults.StatPerRow)
            PlotStatistics(LastFile & " - COL STAT", CurrentFileEvalResults.StatPerCol)
        End If

        Idle()

    End Sub

    Private Sub InitStat(ByRef Vector() As Ato.cSingleValueStatistics)
        For Idx As Integer = 0 To Vector.GetUpperBound(0)
            Vector(Idx) = New Ato.cSingleValueStatistics(True)
        Next Idx
    End Sub

    Private Sub TranslateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AfiineTranslateToolStripMenuItem.Click
        IntelIPP_NewCode.Translate("C:\Users\albus\Dropbox\Astro\!Bilder\Test-Daten\Debayer\Stack_16bits_936frames_152s.fits")
    End Sub

    Private Sub StoreStatisticsEXCELFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiSaveLastStatXLS.Click

        Dim AddHisto As Boolean = True

        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        Using workbook As New ClosedXML.Excel.XLWorkbook

            '1.) Histogram
            If AddHisto = True Then
                Dim XY As New List(Of Object())
                For Each Key As Long In CurrentStatistics.MonochromHistogram_Int.Keys
                    Dim Values As New List(Of Object)
                    Values.Add(Key)
                    Values.Add(CurrentStatistics.MonochromHistogram_Int(Key))
                    If CurrentStatistics.BayerHistograms_Int(0, 0).ContainsKey(Key) Then Values.Add(CurrentStatistics.BayerHistograms_Int(0, 0)(Key)) Else Values.Add(String.Empty)
                    If CurrentStatistics.BayerHistograms_Int(0, 1).ContainsKey(Key) Then Values.Add(CurrentStatistics.BayerHistograms_Int(0, 1)(Key)) Else Values.Add(String.Empty)
                    If CurrentStatistics.BayerHistograms_Int(1, 0).ContainsKey(Key) Then Values.Add(CurrentStatistics.BayerHistograms_Int(1, 0)(Key)) Else Values.Add(String.Empty)
                    If CurrentStatistics.BayerHistograms_Int(1, 1).ContainsKey(Key) Then Values.Add(CurrentStatistics.BayerHistograms_Int(1, 1)(Key)) Else Values.Add(String.Empty)
                    XY.Add(Values.ToArray)
                Next Key
                Dim worksheet As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Histogram")
                worksheet.Cell(1, 1).InsertData(New List(Of String)({"Pixel value", "Count Mono", "Count Bayer_0_0", "Count Bayer_0_1", "Count Bayer_1_0", "Count Bayer_1_1"}), True)
                worksheet.Cell(2, 1).InsertData(XY)
                For Each col In worksheet.ColumnsUsed
                    col.AdjustToContents()
                Next col
            End If

            '2.) Histo density
            Dim HistDens As New List(Of Object())
            For Each Key As UInteger In CurrentStatistics.MonoStatistics_Int.HistXDist.Keys
                HistDens.Add(New Object() {Key, CurrentStatistics.MonoStatistics_Int.HistXDist(Key)})
            Next Key
            Dim worksheet2 As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Histogram Density")
            worksheet2.Cell(1, 1).InsertData(New List(Of String)({"Step size", "Count"}), True)
            worksheet2.Cell(2, 1).InsertData(HistDens)
            For Each col In worksheet2.ColumnsUsed
                col.AdjustToContents()
            Next col

            '3.) Row and column
            If IsNothing(CurrentFileEvalResults.StatPerRow) = False Then
                Dim XY As New List(Of Object())
                For Idx As Integer = 0 To CurrentFileEvalResults.StatPerRow.GetUpperBound(0)
                    With CurrentFileEvalResults.StatPerRow(Idx)
                        XY.Add(New Object() {Idx + 1, .Minimum, .Mean, .Maximum, .Sigma})
                        If Idx = 492 Then
                            Console.WriteLine("!!!")
                        End If
                    End With
                Next Idx
                Dim worksheet As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Row Statistics")
                worksheet.Cell(1, 1).InsertData(New List(Of String)({"Row #", "Min", "Mean", "Max", "Sigma"}), True)
                worksheet.Cell(2, 1).InsertData(XY)
                For Each col In worksheet.ColumnsUsed
                    col.AdjustToContents()
                Next col
            End If
            If IsNothing(CurrentFileEvalResults.StatPerCol) = False Then
                Dim XY As New List(Of Object())
                For Idx As Integer = 0 To CurrentFileEvalResults.StatPerCol.GetUpperBound(0)
                    With CurrentFileEvalResults.StatPerCol(Idx)
                        XY.Add(New Object() {Idx + 1, .Minimum, .Mean, .Maximum, .Sigma})
                    End With
                Next Idx
                Dim worksheet As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Column Statistics")
                worksheet.Cell(1, 1).InsertData(New List(Of String)({"Column #", "Min", "Mean", "Max", "Sigma"}), True)
                worksheet.Cell(2, 1).InsertData(XY)
                For Each col In worksheet.ColumnsUsed
                    col.AdjustToContents()
                Next col
            End If

            '4) Save and open
            Dim FileToGenerate As String = IO.Path.Combine(DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Process.Start(FileToGenerate)

        End Using

    End Sub

    Private Sub ResetStackingToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ResetStackingToolStripMenuItem1.Click
        StackingStatistics = Nothing
    End Sub

    Private Sub tsmiAdjustRGB_Click(sender As Object, e As EventArgs) Handles tsmiAdjustRGB.Click

        'Calculate the maximum modus (the most propable value in the channel) and normalize all channels to this channel
        Running()
        Dim ClipCount(1, 1) As Integer
        If CurrentData.DataProcessor_UInt16.ImageData(0).Length > 0 Then
            Dim ModusRef As Long = Long.MinValue
            For BayerIdx1 As Integer = 0 To 1
                For BayerIdx2 As Integer = 0 To 1
                    ModusRef = Math.Max(ModusRef, CurrentStatistics.BayerStatistics_Int(BayerIdx1, BayerIdx2).Modus.Key)
                Next BayerIdx2
            Next BayerIdx1
            For BayerIdx1 As Integer = 0 To 1
                For BayerIdx2 As Integer = 0 To 1
                    ClipCount(BayerIdx1, BayerIdx2) = 0
                    Dim Norm As Double = ModusRef / CurrentStatistics.BayerStatistics_Int(BayerIdx1, BayerIdx2).Modus.Key
                    If ModusRef <> CurrentStatistics.BayerStatistics_Int(BayerIdx1, BayerIdx2).Modus.Key Then                                                        'skip channels that do not need a change
                        For PixelIdx1 As Integer = BayerIdx1 To CurrentData.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(0) Step 2
                            For PixelIdx2 As Integer = BayerIdx2 To CurrentData.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(1) Step 2
                                Dim NewValue As Double = Math.Round(CurrentData.DataProcessor_UInt16.ImageData(0).Data(PixelIdx1, PixelIdx2) * Norm)
                                If NewValue > UInt16.MaxValue Then
                                    CurrentData.DataProcessor_UInt16.ImageData(0).Data(PixelIdx1, PixelIdx2) = UInt16.MaxValue
                                    ClipCount(BayerIdx1, BayerIdx2) += 1
                                Else
                                    CurrentData.DataProcessor_UInt16.ImageData(0).Data(PixelIdx1, PixelIdx2) = CUShort(NewValue)
                                End If
                            Next PixelIdx2
                        Next PixelIdx1
                    End If
                Next BayerIdx2
            Next BayerIdx1
        End If

        'Log
        For BayerIdx1 As Integer = 0 To 1
            For BayerIdx2 As Integer = 0 To 1
                Log("Clip count for channel [" & BayerIdx1.ValRegIndep & ":" & BayerIdx2.ValRegIndep & "]: " & ClipCount(BayerIdx1, BayerIdx2).ValRegIndep)
            Next BayerIdx2
        Next BayerIdx1

        CalculateStatistics(CurrentData, DB.BayerPatternNames, CurrentStatistics)
        Idle()

    End Sub

    Private Sub tsmiSaveImageData_Click(sender As Object, e As EventArgs) Handles tsmiSaveImageData.Click
        'TODO: Save also non-UInt16 data
        With sfdMain
            .Filter = "FITS 16-bit fixed|*.fits|FITS 32-bit fixed|*.fits|FITS 32-bit float|*.fits|TIFF 16-bit|*.tif|JPG|*.jpg|PNG|*.png"
            If .ShowDialog = DialogResult.OK Then
                Running()
                With CurrentData.DataProcessor_UInt16
                    Select Case sfdMain.FilterIndex
                        Case 1
                            'FITS 16 bit fixed
                            cFITSWriter.Write(sfdMain.FileName, .ImageData(0).Data, cFITSWriter.eBitPix.Int16, LastFITSHeader.GetCardsAsList)
                        Case 2
                            'FITS 32 bit fixed
                            cFITSWriter.Write(sfdMain.FileName, .ImageData(0).Data, cFITSWriter.eBitPix.Int32, LastFITSHeader.GetCardsAsList)
                        Case 3
                            'FITS 32 bit float
                            cFITSWriter.Write(sfdMain.FileName, .ImageData(0).Data, cFITSWriter.eBitPix.Single, LastFITSHeader.GetCardsAsList)
                        Case 4
                            'TIFF
                            If .ImageData.Count = 1 Then
                                ImageFileFormatSpecific.SaveTIFF_Format16bppGrayScale(sfdMain.FileName, .ImageData(0).Data)
                            Else
                                ImageFileFormatSpecific.SaveTIFF_Format48bppColor(sfdMain.FileName, .ImageData)
                            End If
                        Case 5
                            'JPG
                            Dim myEncoderParameters As New System.Drawing.Imaging.EncoderParameters(1)
                            myEncoderParameters.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, DB.ImageQuality)
                            cLockBitmap.GetGrayscaleImage(.ImageData(0).Data, CurrentStatistics.MonoStatistics_Int.Max.Key).BitmapToProcess.Save(sfdMain.FileName, GetEncoderInfo("image/jpeg"), myEncoderParameters)
                        Case 6
                            'PNG - try to do 8bit but only get a palett with 256 values but still 24-bit ...
                            Dim myEncoderParameters As New System.Drawing.Imaging.EncoderParameters(2)
                            myEncoderParameters.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, DB.ImageQuality)
                            myEncoderParameters.Param(1) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, 8)
                            cLockBitmap.GetGrayscaleImage(.ImageData(0).Data, CurrentStatistics.MonoStatistics_Int.Max.Key).BitmapToProcess.Save(sfdMain.FileName, GetEncoderInfo("image/png"), myEncoderParameters)

                            Dim X As New System.Windows.Media.Imaging.PngBitmapEncoder

                    End Select
                End With
                Idle()
            End If
        End With
    End Sub

    '''<summary>Get an encoder by its MIME name</summary>
    '''<param name="mimeType"></param>
    '''<returns></returns>
    Private Function GetEncoderInfo(ByVal mimeType As String) As Imaging.ImageCodecInfo
        Dim RetVal As Imaging.ImageCodecInfo = Nothing
        Dim AllEncoder As New List(Of String)
        For Each Encoder As Imaging.ImageCodecInfo In Imaging.ImageCodecInfo.GetImageEncoders
            If Encoder.MimeType = mimeType Then RetVal = Encoder
            AllEncoder.Add(Encoder.MimeType)
        Next Encoder
        Return RetVal
    End Function

    Private Sub tsmiStretch_Click(sender As Object, e As EventArgs) Handles tsmiStretch.Click
        Running()
        ImageProcessing.MakeHistoStraight(CurrentData.DataProcessor_UInt16.ImageData(0).Data)
        CalculateStatistics(CurrentData, DB.BayerPatternNames, CurrentStatistics)
        Idle()
    End Sub

    '''<summary>Processing running.</summary>
    Private Sub Running()
        tsslRunning.ForeColor = Drawing.Color.Red
        DE()
    End Sub

    '''<summary>Processing idle.</summary>
    Private Sub Idle()
        tsslRunning.ForeColor = Drawing.Color.Silver
        DE()
    End Sub

    Private Sub tsmiAnalysisPlot_ADUQuant_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_Plot_ADUQuant.Click
        Dim Disp As New cZEDGraphForm
        Dim PlotData As Generic.Dictionary(Of Long, UInt64) = AstroNET.Statistics.GetQuantizationHisto(CurrentStatistics.MonochromHistogram_Int)
        Dim XAxis As Double() = PlotData.Keys.ToDouble
        Disp.PlotData("Test", New Double() {1, 2, 3, 4}, Color.Red)
        'Plot data
        Disp.Plotter.Clear()
        Disp.Plotter.PlotXvsY("Mono", XAxis, PlotData.Values.ToArray.ToDouble, New cZEDGraphService.sGraphStyle(Color.Black, DB.PlotStyle, 1))
        Disp.Plotter.GridOnOff(True, True)
        Disp.Plotter.ManuallyScaleXAxis(XAxis(0), XAxis(XAxis.GetUpperBound(0)))
        Disp.Plotter.AutoScaleYAxisLog()
        Disp.Plotter.ForceUpdate()
        'Set style of the window
        Disp.Plotter.SetCaptions(String.Empty, "ADU step size", "# found")
        Disp.Plotter.MaximizePlotArea()
        Disp.Hoster.Icon = Me.Icon
    End Sub

    Private Sub tsmiPlateSolve_Click(sender As Object, e As EventArgs) Handles tsmiPlateSolve.Click
        Log("PLATE SOLVE: > ", AstroImageStatistics_Fun.PlateSolve(LastFile, DB.PlateSolve2Path, DB.PlateSolve2HoldOpen))
    End Sub



    Private Sub tsmiFile_FITSGrep_Click(sender As Object, e As EventArgs) Handles tsmiFile_FITSGrep.Click
        Dim X As New frmFITSGrep : X.Show()
    End Sub

    Private Sub tsmiTest_ASCOMDyn_Click(sender As Object, e As EventArgs) Handles tsmiTest_ASCOMDyn.Click
        'Working but NOVAS31 does not ...
        Dim Astrometry As Object = System.Reflection.Assembly.Load("ASCOM.Astrometry").CreateInstance("ASCOM.Astrometry.Transform.Transform")
        Dim dynamicType As Type = Astrometry.GetType
        Dim dynamicObject As Object = Activator.CreateInstance(dynamicType)
        Dim NOVAS31 As Object = System.Reflection.Assembly.Load("ASCOM.Astrometry").CreateInstance("ASCOM.Astrometry.NOVAS.NOVAS31")
        Dim JulianDate As Double = CDbl(dynamicType.InvokeMember("JulianDate", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Type.DefaultBinder, NOVAS31, New Object() {CShort(Now.Year), CShort(Now.Month), CShort(Now.Day), Now.Hour}))
        dynamicType.InvokeMember("JulianDateUTC", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.SetProperty, Type.DefaultBinder, Astrometry, New Object() {(New ASCOM.Astrometry.NOVAS.NOVAS31).JulianDate(CShort(Now.Year), CShort(Now.Month), CShort(Now.Day), Now.Hour)})
        dynamicType.InvokeMember("SetApparent", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Type.DefaultBinder, Astrometry, New Object() {CDbl(1.0), CDbl(2.0)})
        Dim J2000RA As Double = CDbl(dynamicType.GetProperty("RAJ2000").GetValue(Astrometry))
        Dim DecJ2000 As Double = CDbl(dynamicType.GetProperty("DecJ2000").GetValue(Astrometry))
    End Sub

    Private Sub tsmiFile_ClearStatMem_Click(sender As Object, e As EventArgs) Handles tsmiFile_ClearStatMem.Click
        AllFiles.Clear()
    End Sub

    Private Sub tsmiTest_Focus_Click(sender As Object, e As EventArgs) Handles tsmiTest_Focus.Click

        Dim Disp1 As New cZEDGraphForm
        Dim LineGen As New cZEDGraphService.cLineStyleGenerator
        Dim StatHist As New Dictionary(Of Integer, Dictionary(Of Long, ULong))

        'Combine all histograms
        For Each FileName As String In AllFiles.Keys
            'Get the value indicating the focus
            Dim FocusPos As Integer = CInt(System.IO.Path.GetFileNameWithoutExtension(FileName).Split("_"c)(2))
            If StatHist.ContainsKey(FocusPos) = False Then
                StatHist.Add(FocusPos, AllFiles(FileName).Statistics.MonochromHistogram_Int)
            Else
                CombineHisto(StatHist(FocusPos), AllFiles(FileName).Statistics.MonochromHistogram_Int)
            End If
        Next FileName

        'Calculate focus
        For Each FocusPos As Integer In StatHist.Keys
            Dim Plot_X As New List(Of Double)
            Dim Plot_Y As New List(Of Double)
            FocusAnalysis(FocusPos.ValRegIndep, StatHist(FocusPos), Plot_X, Plot_Y)
            Disp1.PlotData("Focus - <" & FocusPos & ">", Plot_X.ToArray, Plot_Y.ToArray, LineGen.GetNextColor)
        Next FocusPos
        Disp1.MakeYAxisLog()

    End Sub

    '''<summary>Combine 2 histograms.</summary>
    Private Sub CombineHisto(ByRef SumHisto As Dictionary(Of Long, ULong), ByVal HistToAdd As Dictionary(Of Long, ULong))
        If IsNothing(SumHisto) = True Then
            'Init sum with HistToAdd
            SumHisto = New Dictionary(Of Long, ULong)
            For Each Entry As Long In HistToAdd.Keys
                SumHisto.Add(Entry, HistToAdd(Entry))
            Next Entry
        Else
            For Each Entry As Long In HistToAdd.Keys
                If SumHisto.ContainsKey(Entry) = False Then
                    SumHisto.Add(Entry, HistToAdd(Entry))
                Else
                    SumHisto(Entry) += HistToAdd(Entry)
                End If
            Next Entry
        End If
        SumHisto = SumHisto.SortDictionary
    End Sub

    '''<summary>Focus analysis based on maximum enery in N percent of the pixel.</summary>
    Private Function FocusAnalysis(ByVal FileName As String, ByVal Hist As Dictionary(Of Long, ULong), ByRef Plot_X As List(Of Double), ByRef Plot_Y As List(Of Double)) As Boolean

        'We assume only positive pixel ADU values (else, the minimum must be added)

        Dim AllPixelValue As List(Of Long) = Hist.Keys.ToList
        Plot_X = New List(Of Double)
        Plot_Y = New List(Of Double)

        '1.) Get the total energy in the image
        Dim TotalEnery As Long = 0
        For Each Entry As Long In AllPixelValue
            TotalEnery += CLng((Entry * Hist(Entry)))
        Next Entry

        '2.) We count from top and get the energy plot
        AllPixelValue.Reverse()
        Dim SumEnergy As Long = 0
        For Each Entry As Long In AllPixelValue
            Plot_X.Add(Entry)
            SumEnergy += CLng((Entry * Hist(Entry)))
            Plot_Y.Add(100 * (SumEnergy / TotalEnery))
        Next Entry

    End Function

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F1 Then
            Dim RTFTextBox As New cRTFTextBox
            Dim AllResources As String() = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames
            For Each Entry As String In AllResources
                If Entry.EndsWith(".HelpContent.rtf") Then
                    RTFTextBox.ShowText(New System.IO.StreamReader(System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(Entry)).ReadToEnd.Trim)
                    Exit For
                End If
            Next Entry
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub tsmiAnalysis_MultiAreaCompare_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_MultiAreaCompare.Click

        Dim TopValues As Double = 99.99

        'Get top 1 percent of the pixel
        Dim SpecialPixels As Dictionary(Of UInt16, List(Of Point)) = Nothing
        Try
            SpecialPixels = CurrentData.DataProcessor_UInt16.GetAbove(CUShort(CurrentStatistics.MonochromHistogram_PctFract(TopValues)))
        Catch ex As Exception
            'Do nothing
        End Try

        'Optional: Get only pixel which also have high values arround (some sort of blur ...)
        'SpecialPixels = AstroImageStatistics_Fun.HighSurrounding(CurrentData, CurrentStatistics, SpecialPixels)

        'Init a new navigator window
        Dim Navigator As New frmNavigator
        Navigator.IPP = DB.IPP
        Try
            Navigator.tbRootFile.Text = System.IO.Path.GetDirectoryName(LastFile)
        Catch ex As Exception
            'Do nut update root file
        End Try

        'Load the list of special pixel values to the navigator
        Navigator.lbSpecialPixel.Items.Clear()
        If IsNothing(SpecialPixels) = False Then
            If SpecialPixels.Count > 0 Then
                For Each PixelValue As UShort In SpecialPixels.Keys
                    For Each Pixel As Point In SpecialPixels(PixelValue)
                        Navigator.lbSpecialPixel.Items.Add(Pixel.X.ToString.Trim & ":" & Pixel.Y.ToString.Trim & ":value=" & PixelValue.ValRegIndep)
                    Next Pixel
                Next PixelValue
            End If
        End If

        'Show the navigator
        Navigator.Show()
        Navigator.ShowMosaik()

    End Sub

    Private Sub tsmiFile_OpenRecent_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenRecent.Click
        With LastOpenedFiles
            .Files.Clear()
            For Each File As String In DB.GetRecentFiles
                .Files.Add(File)
            Next File
            If .ShowDialog = DialogResult.OK Then
                LoadFile(.SelectedFile, CurrentData)
            End If
        End With
    End Sub

    Private Sub MaxImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaxImageToolStripMenuItem.Click
        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Integer
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).Maximum)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(DB.MyPath, "Stacking_Mean.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Int32)
            Process.Start(FileToGenerate)
        End If
    End Sub

    Private Sub SaveAllfilesStatisticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiSaveAllFilesStat.Click

        Dim AddHisto As Boolean = True

        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        'Get combined hist mono X axis (INT only)
        Dim AllADUValues As New List(Of Long)
        Dim FileList As New List(Of String)
        FileList.Add("ADU value")
        For Each SingleFile As String In AllFiles.Keys
            FileList.Add(System.IO.Path.GetFileNameWithoutExtension(SingleFile))
            For Each ADUValue As Long In AllFiles(SingleFile).Statistics.MonochromHistogram_Int.Keys
                If AllADUValues.Contains(ADUValue) = False Then AllADUValues.Add(ADUValue)
            Next ADUValue
        Next SingleFile
        AllADUValues.Sort()

        Using workbook As New ClosedXML.Excel.XLWorkbook

            'Generate data
            Dim XY As New List(Of Object())
            For Each ADUValue As Long In AllADUValues
                Dim Values As New List(Of Object)
                Values.Add(ADUValue)
                For Each SingleFile As String In AllFiles.Keys
                    If AllFiles(SingleFile).Statistics.MonochromHistogram_Int.ContainsKey(ADUValue) Then Values.Add(AllFiles(SingleFile).Statistics.MonochromHistogram_Int(ADUValue)) Else Values.Add(String.Empty)
                Next SingleFile
                XY.Add(Values.ToArray)
            Next ADUValue
            Dim worksheet As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Histogram")
            worksheet.Cell(1, 1).InsertData(FileList, True)                                         'file names
            worksheet.Cell(2, 1).InsertData(XY)                                                     'combined histogram
            For Each col In worksheet.ColumnsUsed
                col.AdjustToContents()
            Next col

            'Save and open
            Dim FileToGenerate As String = IO.Path.Combine(DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Process.Start(FileToGenerate)

        End Using

    End Sub

    Private Sub tsmiTest_ReadNEFFile_Click(sender As Object, e As EventArgs) Handles tsmiTest_ReadNEFFile.Click

        Dim Reader As New cNEFReader
        Reader.Read("\\192.168.100.10\astro\2020_07_20_NeoWise\DSC_0286.NEF")

    End Sub

    Private Sub tsmiSetPixelToValue_Click(sender As Object, e As EventArgs) Handles tsmiSetPixelToValue.Click

        Dim FixedPixelCount As UInt32 = 0
        Dim Limit As UShort = CUShort(InputBox("Upper limit (included)", "65536"))
        Dim SetTo As UShort = CUShort(InputBox("Set to", "0"))
        Running()
        Select Case CurrentData.DataMode
            Case AstroNET.Statistics.eDataMode.UInt16
                With CurrentData.DataProcessor_UInt16.ImageData(0)
                    For Idx1 As Integer = 0 To .NAXIS1 - 1
                        For Idx2 As Integer = 0 To .NAXIS2 - 1
                            If .Data(Idx1, Idx2) >= Limit Then
                                FixedPixelCount += UInt32One
                                .Data(Idx1, Idx2) = SetTo
                            End If
                        Next Idx2
                    Next Idx1
                End With
            Case Else

        End Select
        Log("Fixed " & FixedPixelCount.ValRegIndep & " pixel changed")
        Idle()

    End Sub

    Private Sub tsmiTestCode_UseOpenCV_Click(sender As Object, e As EventArgs) Handles tsmiTestCode_UseOpenCV.Click

        'https://shimat.github.io/opencvsharp_docs/html/d69c29a1-7fb1-4f78-82e9-79be971c3d03.htm

        Dim ImagePath As String = "C:\!Work\Astro\IC5146\rawframes\frame_1.png"

        Using src As New OpenCvSharp.Mat(ImagePath, OpenCvSharp.ImreadModes.Grayscale)

            Dim dst As New OpenCvSharp.Mat
            OpenCvSharp.Cv2.CvtColor(src, dst, OpenCvSharp.ColorConversionCodes.GRAY2BGR)      'Converts image from one color space to another

            CppStyleStarDetector(src, dst) ' C++-style

            'For transformation details see https://docs.opencv.org/master/da/d6e/tutorial_py_geometric_transformations.html
            'Dim Transformer As OpenCvSharp.Mat = OpenCvSharp.Cv2.GetRotationMatrix2D(New OpenCvSharp.Point2f(CSng((src.Width - 1) / 2), CSng((src.Height - 1) / 2)), 45.0, 1.0)

            'Transformer = New  OpenCvSharp.Mat(2, 3, OpenCvSharp.MatType.CV_64FC1, New Double(,) {{1, 2, 3}, {4, 5, 6}})
            'Dim dsize As OpenCvSharp.Size

            'Dim dst As OpenCvSharp.Mat = src.WarpAffine(Transformer, dsize, OpenCvSharp.InterpolationFlags.Lanczos4, OpenCvSharp.BorderTypes.Constant)


            Using w1 As New OpenCvSharp.Window("img", src),
                  w2 As New OpenCvSharp.Window("features", dst)
                OpenCvSharp.Cv2.WaitKey()
            End Using
        End Using

    End Sub

    '''<summary>Extracts keypoints by C++-style code (cv::StarDetector)</summary>
    '''<param name="src"></param>
    '''<param name="dst"></param>
    Private Sub CppStyleStarDetector(ByVal src As OpenCvSharp.Mat, ByVal dst As OpenCvSharp.Mat)

        Dim detector As OpenCvSharp.XFeatures2D.StarDetector = OpenCvSharp.XFeatures2D.StarDetector.Create()
        Dim keypoints() As OpenCvSharp.KeyPoint = detector.Detect(src, Nothing)

        If keypoints IsNot Nothing Then
            For Each kpt As OpenCvSharp.KeyPoint In keypoints
                Dim r As Single = kpt.Size / 2
                Dim a = kpt.Pt
                OpenCvSharp.Cv2.Circle(dst, New OpenCvSharp.Point(kpt.Pt.X, kpt.Pt.Y), CInt(Math.Truncate(r)), New OpenCvSharp.Scalar(0, 255, 0), 1, OpenCvSharp.LineTypes.Link8, 0)
                OpenCvSharp.Cv2.Line(dst, New OpenCvSharp.Point(kpt.Pt.X + r, kpt.Pt.Y + r), New OpenCvSharp.Point(kpt.Pt.X - r, kpt.Pt.Y - r), New OpenCvSharp.Scalar(0, 255, 0), 1, OpenCvSharp.LineTypes.Link8, 0)
                OpenCvSharp.Cv2.Line(dst, New OpenCvSharp.Point(kpt.Pt.X - r, kpt.Pt.Y + r), New OpenCvSharp.Point(kpt.Pt.X + r, kpt.Pt.Y - r), New OpenCvSharp.Scalar(0, 255, 0), 1, OpenCvSharp.LineTypes.Link8, 0)
            Next kpt
        End If

    End Sub

    Private Sub tsmiProcessing_MedianFilter_Click(sender As Object, e As EventArgs) Handles tsmiProcessing_MedianFilter.Click
        Dim Entry As String = InputBox("KSize", "KSize", "3")
        Dim KSize As Integer = -1
        If Integer.TryParse(Entry, KSize) = False Then
            Exit Sub
        Else
            cOpenCvSharp.MedianBlur(CurrentData.DataProcessor_UInt16.ImageData(0).Data, KSize)
            CalculateStatistics(CurrentData, DB.BayerPatternNames, CurrentStatistics)
            If DB.AutoOpenStatGraph = True Then PlotStatistics(LastFile, CurrentStatistics)
        End If
    End Sub

    Private Sub tsmiTools_ALADINCoords_Click(sender As Object, e As EventArgs) Handles tsmiTools_ALADINCoords.Click

        Dim FileToRun As String = LastFile

        'Get the FITS header information
        Dim DataStartPos As Integer = -1
        Dim X As List(Of cFITSHeaderParser.sHeaderElement) = cFITSHeaderChanger.ParseHeader(FileToRun, DataStartPos)
        Dim File_RA_JNow As String = Nothing
        Dim File_Dec_JNow As String = Nothing
        For Each Entry As cFITSHeaderParser.sHeaderElement In X
            If Entry.Keyword = eFITSKeywords.RA Then File_RA_JNow = CStr(Entry.Value).Trim("'"c).Trim.Trim("'"c)
            If Entry.Keyword = eFITSKeywords.DEC Then File_Dec_JNow = CStr(Entry.Value).Trim("'"c).Trim.Trim("'"c)
        Next Entry

        'Data from QHYCapture (10Micron) are in JNow, so convert to J2000 for PlateSolve
        Dim File_RA_J2000 As Double = Double.NaN
        Dim File_Dec_J2000 As Double = Double.NaN
        AstroImageStatistics_Fun.JNowToJ2000(AstroParser.ParseRA(File_RA_JNow), AstroParser.ParseDeclination(File_Dec_JNow), File_RA_J2000, File_Dec_J2000)

        Dim AladinCall As String = Ato.AstroCalc.FormatHMS(File_RA_J2000) & " " & Ato.AstroCalc.Format360Degree(File_Dec_J2000)

        'Possible resolvers:
        'http://tdc-www.harvard.edu/astro.image.html

        Clipboard.Clear()
        Clipboard.SetText(AladinCall)

    End Sub

    Private Sub tsmiAnalysis_ManualColorBalancer_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_ManualColorBalancer.Click
        Dim X As New frmManualAdjust
        X.Show()
    End Sub

    Private Sub tsmiSaveFITSAndStats_Click(sender As Object, e As EventArgs) Handles tsmiSaveFITSAndStats.Click

        Dim AddHisto As Boolean = True

        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        'Generate a list of all FITS keys and statistics including the maximum entry length
        Dim FoundFitsKeywords As New Dictionary(Of eFITSKeywords, Integer)
        Dim FoundStatParameters As New List(Of String)
        For Each FileName As String In AllFiles.Keys
            For Each Key As eFITSKeywords In AllFiles(FileName).Header.Keys
                Dim HeaderValue As String = CStr(AllFiles(FileName).Header(Key))
                If FoundFitsKeywords.ContainsKey(Key) = False Then FoundFitsKeywords.Add(Key, -1)
                If FoundFitsKeywords(Key) < HeaderValue.Length Then
                    FoundFitsKeywords(Key) = HeaderValue.Length
                End If
            Next Key
            For Each StatParameter As String In AllFiles(FileName).Statistics.MonoStatistics_Int.AllStats.Keys
                If FoundStatParameters.Contains(StatParameter) = False Then FoundStatParameters.Add(StatParameter)
            Next StatParameter
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
            For Each FileName As String In AllFiles.Keys
                KeyIdx = 1
                worksheet.Cell(FileIdx, KeyIdx).Value = FileName
                'Add all FITS headers
                For Each Key As eFITSKeywords In FoundFitsKeywords.Keys
                    KeyIdx += 1
                    'Add the found entry or no entry
                    If AllFiles(FileName).Header.ContainsKey(Key) Then
                        worksheet.Cell(FileIdx, KeyIdx).Value = AllFiles(FileName).Header(Key)
                    Else
                        worksheet.Cell(FileIdx, KeyIdx).Value = "XXXXXX"
                    End If
                Next Key
                'Add all statistics values
                For Each Key As String In FoundStatParameters
                    KeyIdx += 1
                    If AllFiles(FileName).Statistics.MonoStatistics_Int.AllStats.ContainsKey(Key) Then
                        worksheet.Cell(FileIdx, KeyIdx).Value = AllFiles(FileName).Statistics.MonoStatistics_Int.AllStats(Key)
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
            Dim FileToGenerate As String = IO.Path.Combine(DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Process.Start(FileToGenerate)

        End Using

    End Sub

    Private Sub tsmiAnalysisVignette_CalcRaw_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_CalcRaw.Click
        CalcVignette()
    End Sub

    Private Sub tsmiAnalysisVignette_Display_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_Display.Click
        If IsNothing(CurrentFileEvalResults.Vig_RawData) = False Then
            If CurrentFileEvalResults.Vig_RawData.Count > 0 Then
                Dim Disp1 As New cZEDGraphForm : Disp1.PlotData("Vignette", CurrentFileEvalResults.Vig_RawData, Color.Red)
            End If
        End If
    End Sub

    '''<summary>Calculate the vignette.</summary>
    Private Sub CalcVignette()
        Running()
        Dim Stopper As New cStopper
        Stopper.Start()
        CurrentFileEvalResults.Vig_RawData = New Dictionary(Of Double, Double)
        Select Case CurrentData.DataMode
            Case AstroNET.Statistics.eDataMode.UInt16
                CurrentFileEvalResults.Vig_RawData = ImageProcessing.Vignette(CurrentData.DataProcessor_UInt16.ImageData(0).Data)
            Case AstroNET.Statistics.eDataMode.UInt32
                CurrentFileEvalResults.Vig_RawData = ImageProcessing.Vignette(CurrentData.DataProcessor_UInt32.ImageData(0).Data)
        End Select
        CurrentFileEvalResults.Vig_RawData = CurrentFileEvalResults.Vig_RawData.SortDictionary
        Log(Stopper.Stamp("Vignette - getting data (" & CurrentFileEvalResults.Vig_RawData.Count & " values"))
        Idle()
    End Sub

    Private Sub tsmiAnalysisVignette_Clear_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_Clear.Click
        CurrentFileEvalResults.Vig_RawData = New Dictionary(Of Double, Double)
    End Sub

    Private Sub tsmiAnalysisVignette_Correct_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_Correct.Click

        Dim Stopper As New cStopper : Stopper.Start()
        Running()

        'Normalize
        DB.IPP.DivC(CurrentFileEvalResults.Vig_Fitting, DB.IPP.Max(CurrentFileEvalResults.Vig_Fitting))
        Dim NormMin As Double = DB.IPP.Min(CurrentFileEvalResults.Vig_Fitting)
        DB.IPP.DivC(CurrentFileEvalResults.Vig_Fitting, NormMin)
        Dim UsedVignette_Correction As New Dictionary(Of Double, Double)
        Dim YPtr As Integer = 0
        For Each Entry As Double In CurrentFileEvalResults.Vig_BinUsedData.Keys
            UsedVignette_Correction.Add(Entry, CurrentFileEvalResults.Vig_Fitting(YPtr))
            YPtr += 1
        Next Entry
        Log(Stopper.Stamp("Vignette - normalizing"))

        'Correct the vignette
        Dim CorrectedValues As Integer = -1
        Select Case CurrentData.DataMode
            Case AstroNET.Statistics.eDataMode.UInt16
                CorrectedValues = ImageProcessing.CorrectVignette(CurrentData.DataProcessor_UInt16.ImageData(0).Data, UsedVignette_Correction)
            Case AstroNET.Statistics.eDataMode.UInt32
                CorrectedValues = ImageProcessing.CorrectVignette(CurrentData.DataProcessor_UInt32.ImageData(0).Data, UsedVignette_Correction)
        End Select
        Log(Stopper.Stamp("Vignette - correction (" & CorrectedValues.ValRegIndep & " values corrected"))

        CalculateStatistics(CurrentData, DB.BayerPatternNames, CurrentStatistics)
        Idle()

    End Sub

    Private Sub tsmiAnalysisVignette_CalcParam_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_CalcParam.Click

        Dim Visualization As New cZEDGraphForm

        'Calculate vignette if not present
        Dim CalcRequired As Boolean = True
        If IsNothing(CurrentFileEvalResults.Vig_RawData) = False Then
            If CurrentFileEvalResults.Vig_RawData.Count > 0 Then
                CalcRequired = False
            End If
        End If
        If CalcRequired = True Then CalcVignette()

        Dim Stopper As New cStopper : Stopper.Start()
        Running()

        'Get only relevant data
        CurrentFileEvalResults.Vig_BinUsedData = New Dictionary(Of Double, Double)
        For Each Distance As Double In CurrentFileEvalResults.Vig_RawData.Keys
            If Distance >= DB.VigStartDistance And Distance <= DB.VigStopDistance Then
                CurrentFileEvalResults.Vig_BinUsedData.Add(Distance, CurrentFileEvalResults.Vig_RawData(Distance))
            End If
        Next Distance
        Dim Min As Double = Double.NaN
        Dim Max As Double = Double.NaN
        CurrentFileEvalResults.Vig_BinUsedData = CurrentFileEvalResults.Vig_BinUsedData.SortDictionary(False, Min, Max)

        'Bin if required
        If DB.VigCalcBins > 0 Then
            'Build a statistics class for each X and Y bin
            Dim VigBin_X(DB.VigCalcBins - 1) As Ato.cSingleValueStatistics
            Dim VigBin_Y(DB.VigCalcBins - 1) As Ato.cSingleValueStatistics
            For InitIdx As Integer = 0 To VigBin_X.GetUpperBound(0)
                VigBin_X(InitIdx) = New Ato.cSingleValueStatistics(False)
                VigBin_Y(InitIdx) = New Ato.cSingleValueStatistics(False)
            Next InitIdx
            'Sweep over all used dictionary entries, calculate bin and add value for X and Y value
            Dim Range As Double = Max - Min
            For Each Distance As Double In CurrentFileEvalResults.Vig_RawData.Keys
                Dim Bin As Double = ((Distance - Min) / Range) * (DB.VigCalcBins - 1)
                Dim BinInt As Integer = CInt(Math.Floor(Bin))
                VigBin_X(BinInt).AddValue(Distance)
                VigBin_Y(BinInt).AddValue(CurrentFileEvalResults.Vig_RawData(Distance))
            Next Distance
            'Use the mean values for each bin in X and Y direction as new support point
            CurrentFileEvalResults.Vig_BinUsedData.Clear()
            For InitIdx As Integer = 0 To VigBin_X.GetUpperBound(0)
                If VigBin_X(InitIdx).ValueCount > 0 Then
                    CurrentFileEvalResults.Vig_BinUsedData.Add(VigBin_X(InitIdx).Mean, VigBin_Y(InitIdx).Mean)
                End If
            Next InitIdx
            Visualization.PlotData("Vignette bin", CurrentFileEvalResults.Vig_BinUsedData, Color.Orange)
        End If

        Log(Stopper.Stamp("Vignette - getting used data"))
        Log("Vignette correction calculation has <" & CurrentFileEvalResults.Vig_BinUsedData.Count.ValRegIndep & "> entries")

        'Calculate the fitting
        If DB.VigPolyOrder = -1 And DB.VigCalcBins > 0 Then
            'Use the binned data
            Log(" ... using (direct) binned data for fitting")
            CurrentFileEvalResults.Vig_Fitting = CurrentFileEvalResults.Vig_BinUsedData.Values.ToArray
        Else
            'Use the polynomial calculation
            Log(" ... using polynomial calcualtion for fitting")
            Dim Polynomial() As Double = {}
            SignalProcessing.RegressPoly(CurrentFileEvalResults.Vig_BinUsedData, DB.VigPolyOrder, Polynomial)
            CurrentFileEvalResults.Vig_Fitting = SignalProcessing.ApplyPoly(CurrentFileEvalResults.Vig_BinUsedData.Keys.ToArray, Polynomial)
        End If

        Log(Stopper.Stamp("Vignette - fitting"))

        Visualization.PlotData("Fitting", CurrentFileEvalResults.Vig_BinUsedData.Keys.ToArray, CurrentFileEvalResults.Vig_Fitting, Color.Green)
        Visualization.PlotData("Vignette raw data", CurrentFileEvalResults.Vig_RawData, Color.Red)

        Idle()

    End Sub

    Private Sub tsmiAnalysisPlot_Replot_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_Plot_Replot.Click
        Running()
        PlotStatistics(LastFile, CurrentStatistics)
        Idle()
    End Sub

    Private Sub tsmiAnalysisPixelMap_SaveFor_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisPixelMap_SaveFor.Click
        Dim Val As Integer = CInt(InputBox("Save coordinates of pixel with a value >= ...", "Criteria", "65536"))
        Running()
        Dim CriteriaPixel As New List(Of String)
        With CurrentData.DataProcessor_UInt16.ImageData(0)
            For Idx1 As Integer = 1 To .NAXIS1 - 2
                For Idx2 As Integer = 1 To .NAXIS2 - 2
                    If .Data(Idx1, Idx2) >= Val Then
                        CriteriaPixel.Add(Format(Idx1, "0000").Trim & ":" & Format(Idx2, "0000") & ">" & .Data(Idx1, Idx2))
                    End If
                Next Idx2
            Next Idx1
        End With
        With sfdMain
            .Filter = "Hot pixel file (*.hotpixel.txt)|*.hotpixel.txt"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With
        System.IO.File.WriteAllLines(sfdMain.FileName, CriteriaPixel.ToArray)
        Log("Found " & CriteriaPixel.Count.ToString.Trim & " pixel")
        Idle()
    End Sub

    Private Sub tsmiAnalysisHotPixel_detect_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisHotPixel_detect.Click
        'For each pixel take the area around and check if the value is significantly too high
        Dim FixedPixelCount As UInt32 = 0
        Dim HotPixelLimit As Double = 5
        Running()
        With CurrentData.DataProcessor_UInt16.ImageData(0)
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
        Log("Fixed " & FixedPixelCount.ValRegIndep & " pixel")
        Idle()
    End Sub

    Private Sub tsmiAnalysisHotPixel_fixfile_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisHotPixel_fixfile.Click
        With ofdMain
            .Filter = "Hot pixel file (*.hotpixel.txt)|*.hotpixel.txt"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With
        Dim HotPixel As String() = System.IO.File.ReadAllLines(ofdMain.FileName)

        Running()
        Dim ReplaceLog As New List(Of String)
        With CurrentData.DataProcessor_UInt16.ImageData(0)
            For Idx As Integer = 0 To HotPixel.GetUpperBound(0)
                Dim X As Integer = CInt(HotPixel(Idx).Substring(0, 4))
                Dim Y As Integer = CInt(HotPixel(Idx).Substring(5, 4))
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
            Next Idx
        End With
        Log(ReplaceLog)
        Log("Fixed " & HotPixel.Count & " pixel")

        CalculateStatistics(CurrentData, DB.BayerPatternNames, CurrentStatistics)
        Idle()

    End Sub

    Private Sub MedianWithinNETToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MedianWithinNETToolStripMenuItem.Click

        Running()

        'Init new image with old one
        Dim NewImage(CurrentData.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(0), CurrentData.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(1)) As UInt16
        DB.IPP.Copy(CurrentData.DataProcessor_UInt16.ImageData(0).Data, NewImage)

        Select Case CurrentData.DataMode
            Case AstroNET.Statistics.eDataMode.UInt16
                With CurrentData.DataProcessor_UInt16.ImageData(0)
                    Dim PixelList As New List(Of UInt16)
                    For Idx1 As Integer = 1 To .NAXIS1 - 2
                        For Idx2 As Integer = 1 To .NAXIS2 - 2
                            PixelList.Clear()
                            PixelList.Add(.Data(Idx1 - 1, Idx2 - 1))
                            PixelList.Add(.Data(Idx1 - 1, Idx2))
                            PixelList.Add(.Data(Idx1 - 1, Idx2 + 1))
                            PixelList.Add(.Data(Idx1, Idx2 - 1))
                            PixelList.Add(.Data(Idx1, Idx2))
                            PixelList.Add(.Data(Idx1, Idx2 + 1))
                            PixelList.Add(.Data(Idx1 + 1, Idx2 - 1))
                            PixelList.Add(.Data(Idx1 + 1, Idx2))
                            PixelList.Add(.Data(Idx1 + 1, Idx2 + 1))
                            PixelList.Sort()
                            NewImage(Idx1, Idx2) = PixelList(4)
                        Next Idx2
                    Next Idx1
                End With
            Case Else

        End Select

        'Replace original image data with new image data
        DB.IPP.Copy(NewImage, CurrentData.DataProcessor_UInt16.ImageData(0).Data)
        CalculateStatistics(CurrentData, DB.BayerPatternNames, CurrentStatistics)
        If DB.AutoOpenStatGraph = True Then PlotStatistics(LastFile, CurrentStatistics)
        Idle()

    End Sub

    Private Sub tsb_Open_Click(sender As Object, e As EventArgs) Handles tsb_Open.Click
        tsmiFile_Open_Click(Nothing, Nothing)
    End Sub

    Private Sub tsb_Display_Click(sender As Object, e As EventArgs) Handles tsb_Display.Click
        Dim ImageDisplay As New frmImageDisplay
        ImageDisplay.FileToDisplay = LastFile
        ImageDisplay.Show()
        ImageDisplay.SingleStatCalc = CurrentData
        ImageDisplay.StatToUsed = CurrentStatistics
        ImageDisplay.MyIPP = DB.IPP
        ImageDisplay.Props.MinCutOff_ADU = CurrentStatistics.MonoStatistics_Int.Min.Key
        ImageDisplay.Props.MaxCutOff_ADU = CurrentStatistics.MonoStatistics_Int.Max.Key
        ImageDisplay.GenerateDisplayImage()
    End Sub

    Private Sub tsmiFile_AstroBinSearch_Click(sender As Object, e As EventArgs) Handles tsmiFile_AstroBinSearch.Click
        Dim AstroBinSearch As New frmAstroBinSearch
        AstroBinSearch.Show()
    End Sub

    Private Sub tsmiFile_Compress2nd_Click(sender As Object, e As EventArgs) Handles tsmiFile_Compress2nd.Click

        'Try to compress a 2nd image by searching all different pixel and only coding them
        'Date are too different, so this may not work out ...

        'Load 2nd file
        Dim Comp2ndFile As AstroNET.Statistics = Nothing
        ofdMain.Filter = "FIT(s) files (FIT/FITS/FTS)|*.FIT;*.FITS;*.FTS"
        If ofdMain.ShowDialog <> DialogResult.OK Then Exit Sub
        LoadFile(ofdMain.FileNames(0), Comp2ndFile)

        'Check if files match together
        If Comp2ndFile.DataProcessor_UInt16.ImageData(0).NAXIS1 <> CurrentData.DataProcessor_UInt16.ImageData(0).NAXIS1 Then
            MsgBox("NAXIS1 missmatch")
            Exit Sub
        End If
        If Comp2ndFile.DataProcessor_UInt16.ImageData(0).NAXIS2 <> CurrentData.DataProcessor_UInt16.ImageData(0).NAXIS2 Then
            MsgBox("NAXIS2 missmatch")
            Exit Sub
        End If

        'Calculate difference data
        Dim UInt32One As UInt32 = 1
        Dim DiffCat As New Dictionary(Of Int32, UInt32)         'All found differences
        Dim PixelDiff As Int32 = 0                              'Diff of the current pixel
        Dim DiffPixelCount As UInt32 = 0                        'Number of different pixel
        Dim TotalPixel As Long = CurrentData.DataProcessor_UInt16.ImageData(0).Length
        With CurrentData.DataProcessor_UInt16.ImageData(0)
            For Idx1 As Integer = 0 To .NAXIS1 - 1
                For Idx2 As Integer = 0 To .NAXIS2 - 1
                    PixelDiff = CInt(Comp2ndFile.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2)) - CInt(CurrentData.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2))
                    If PixelDiff <> 0 Then
                        DiffPixelCount += UInt32One
                        If Not DiffCat.ContainsKey(PixelDiff) Then
                            DiffCat.Add(PixelDiff, 1)
                        Else
                            DiffCat(PixelDiff) += UInt32One
                        End If
                    End If
                Next Idx2
            Next Idx1
        End With

        'Sort by value
        DiffCat = DiffCat.SortDictionary()

        'Output log
        Log("Number of pixel different: " & DiffPixelCount.ValRegIndep)
        Log("Number of pixel identical: " & (TotalPixel - DiffPixelCount).ValRegIndep)
        Log("Different pixel levels: " & DiffCat.Count.ValRegIndep)

        'Generate verbose Excel file
        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        Using workbook As New ClosedXML.Excel.XLWorkbook

            '1.) Generate data
            Dim worksheet As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Difference histogram")
            Dim EntryPtr As Integer = 0
            For Each Entry As Int32 In DiffCat.Keys
                EntryPtr += 1
                worksheet.Cell(EntryPtr, 1).InsertData(New Object() {Entry, DiffCat(Entry)}, True)
            Next Entry
            For Each col In worksheet.ColumnsUsed
                col.AdjustToContents()
            Next col

            '2.) Save and open
            Dim FileToGenerate As String = IO.Path.Combine(DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Process.Start(FileToGenerate)

        End Using

    End Sub

    Private Sub tsmiAnalysis_RawFITSHeader_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_RawFITSHeader.Click
        If System.IO.File.Exists(LastFile) Then
            Dim FileIn As IO.FileStream = System.IO.File.OpenRead(LastFile)
            Dim Lines As New List(Of String)
            Dim ByteBuffer(cFITSReader.HeaderElementLength - 1) As Byte
            For LineIdx As Integer = 1 To 100
                FileIn.Read(ByteBuffer, 0, ByteBuffer.Length)
                Lines.Add(System.Text.Encoding.UTF8.GetString(ByteBuffer))
            Next LineIdx
            Dim HeaderAsIs As New frmHeaderAsIs
            HeaderAsIs.Text = "Raw header of file <" & LastFile & ">"
            HeaderAsIs.tbHeader.Text = Join(Lines.ToArray, System.Environment.NewLine)
            HeaderAsIs.Show()
        End If
    End Sub

    Private Sub tsmiAnalysis_FloatAsIntError_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_FloatAsIntError.Click
        'Calculate if the data are "real" float or just int converted to float
        If CurrentStatistics.DataMode = AstroNET.Statistics.sStatistics.eDataMode.Float Then
            Dim ErrorEnergy As Double = 0.0
            Dim Samples As ULong = 0
            For Each Key As Single In CurrentStatistics.MonochromHistogram_Float32.Keys
                Dim AsInt32 As Int32 = Convert.ToInt32(Key)
                Samples += CurrentStatistics.MonochromHistogram_Float32(Key)
                ErrorEnergy += Math.Abs((AsInt32 - Key) * CurrentStatistics.MonochromHistogram_Float32(Key))
            Next Key
            ErrorEnergy /= Samples
            'Log statistics
            Log("Float as int error: <" & ErrorEnergy.ValRegIndep & ">")
            Log(New String("="c, 109))
        End If
    End Sub

    Private Sub CloudWatcherCombinerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloudWatcherCombinerToolStripMenuItem.Click

        Dim Root As String = "C:\01_Daten\Wetterhistorie"
        Dim FileName As String = "CloudWatcher.csv"

        'Get all files
        Dim AllFiles As New List(Of String)
        For Each Folder As String In System.IO.Directory.GetDirectories(Root)
            Dim SingleFile As String = System.IO.Path.Combine(Folder, FileName)
            If System.IO.File.Exists(SingleFile) = True Then AllFiles.Add(SingleFile)
        Next Folder
        Console.WriteLine("Found <" & AllFiles.Count & "> files.")

        'Max size file
        Dim MaxSize As Long = -1
        Dim MaxSizeFile As String = String.Empty
        For Each SingleFile As String In AllFiles
            Dim FileSize As Long = (New System.IO.FileInfo(SingleFile)).Length
            If FileSize > MaxSize Then
                MaxSize = FileSize
                MaxSizeFile = SingleFile
            End If
        Next SingleFile
        Console.WriteLine("Biggest file: <" & MaxSizeFile & ">")
        Dim BiggestFile As Byte() = System.IO.File.ReadAllBytes(MaxSizeFile)

        'Get all smaller files
        For Each SingleFile As String In AllFiles
            If SingleFile <> MaxSizeFile Then
                Dim Smaller As Byte() = System.IO.File.ReadAllBytes(SingleFile)
                Dim CanBeDeleted As Boolean = StartIsSame(BiggestFile, Smaller)
                If CanBeDeleted Then
                    System.IO.File.Delete(SingleFile)
                    Console.WriteLine("Deleted <" & SingleFile & ">")
                Else
                    Console.WriteLine("DIFFERENT!")
                End If
            End If
        Next SingleFile

        Console.WriteLine("DONE.")
        Console.ReadKey()
    End Sub

    Private Function StartIsSame(ByRef BiggerFile As Byte(), ByRef SmallerFile As Byte()) As Boolean
        For Idx As Integer = 0 To SmallerFile.GetUpperBound(0)
            If BiggerFile(Idx) <> SmallerFile(Idx) Then Return False
        Next Idx
        Return True
    End Function

    Private Sub tsmiTools_ChangeHeader_Click(sender As Object, e As EventArgs) Handles tsmiTools_ChangeHeader.Click
        Dim SrcDst As String = "C:\TEMP\Astro - Change header\ToChange.fits"
        Dim ValuesToChange As New Dictionary(Of String, Object)
        ValuesToChange.Add(FITSKeyword.KeywordString(eFITSKeywords.TELFOC), New Object() {433.2, "Ganz doll ..."})
        ValuesToChange.Add("Ollla", New Object() {Now, "Heute isses schoen"})
        Dim Result As String = cFITSHeaderChanger.ChangeHeader(SrcDst, ValuesToChange)
        MsgBox(Result)
    End Sub

    Private Sub RemoveQHY600OverscanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiTools_RemoveOverscan.Click
        'Remove the overscan area of the given data
        'Normal size is 9600 x 6422
        'Overscan is 24 x 34 pixel
        'Resulting "image only" area is 9576 x 6388
        Dim W As Integer = 9600
        Dim H As Integer = 6422
        Dim OV_X As Integer = 24
        Dim OV_Y As Integer = 34
        Dim EffArea_Width As Integer = W - OV_X
        Dim EffArea_Height As Integer = H - OV_Y
        'Run IPP function (correct)
        Dim ZoomStatCalc As New AstroNET.Statistics(DB.IPP)
        ZoomStatCalc.ResetAllProcessors()
        Dim Status_GetROI As cIntelIPP.IppStatus = DB.IPP.Copy(CurrentData.DataProcessor_UInt16.ImageData(0).Data, ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, CInt(OV_X), CInt(0), CInt(EffArea_Width), CInt(EffArea_Height))
        'Run VB function (may be wrong)
        Dim Data_New(,) As UInt16 = ImgArrayFunction.GetROI(CurrentData.DataProcessor_UInt16.ImageData(0).Data, OV_X, W - 1, 0, H - OV_Y - 2)
        'Compare both ROI's
        Dim DiffCount As Integer = ImgArrayFunction.FindDifferences(ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, Data_New)

        LastFITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.NAXIS1, EffArea_Width))
        LastFITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.NAXIS2, EffArea_Height))
        sfdMain.Filter = "FITS 16-bit fixed|*.fits"
        If sfdMain.ShowDialog = DialogResult.OK Then
            Running()
            cFITSWriter.Write(sfdMain.FileName, ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, cFITSWriter.eBitPix.Int16, LastFITSHeader.GetCardsAsList)
            Idle()
        End If
    End Sub

    Private Sub SpecialTestFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpecialTestFileToolStripMenuItem.Click
        cFITSWriter.WriteTestFile_UInt16_XYCoded(System.IO.Path.Combine(DB.MyPath, "UInt16_XYCoded.fits"))
        Process.Start(DB.MyPath)
    End Sub

    Private Sub CheckROICutoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckROICutoutToolStripMenuItem.Click

        Dim W As Integer = 100
        Dim H As Integer = 50
        Dim OV_X As Integer = 24
        Dim OV_Y As Integer = 34
        Dim EffArea_Width As Integer = W - OV_X
        Dim EffArea_Height As Integer = H - OV_Y
        'Run IPP function (correct)
        Dim ZoomStatCalc As New AstroNET.Statistics(DB.IPP)
        ZoomStatCalc.ResetAllProcessors()
        Dim Status_GetROI As cIntelIPP.IppStatus = DB.IPP.Copy(CurrentData.DataProcessor_UInt16.ImageData(0).Data, ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, CInt(OV_X), CInt(0), CInt(EffArea_Width), CInt(EffArea_Height))
        'Run VB function (may be wrong)
        Dim Data_New(,) As UInt16 = ImgArrayFunction.GetROI(CurrentData.DataProcessor_UInt16.ImageData(0).Data, OV_X, W - 1, 0, H - OV_Y - 1)
        'Compare both ROI's
        Dim DiffCount As Integer = ImgArrayFunction.FindDifferences(ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, Data_New)

    End Sub

    Private Sub tsmiWorkflow_Runner_Click(sender As Object, e As EventArgs) Handles tsmiWorkflow_Runner.Click
        Dim Workflow As New frmWorkflow
        Workflow.SetDB(DB)
        Workflow.Show()
    End Sub

    Private Sub tsmiAnalysis_MultiFile_Open_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_MultiFile_Open.Click
        Dim NewForm As New frmSinglePixelStat
        NewForm.Show()
    End Sub

    Private Sub tsmiAnalysis_MultiFile_LoadAbove_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_MultiFile_LoadAbove.Click

        Dim FixedPixelCount As UInt32 = 0
        Dim LowerLimit As UShort = 0 : UShort.TryParse(InputBox("Lower limit (included)", "Lower limit (included)", "0"), LowerLimit)
        Dim UpperLimit As UShort = 65535 : UShort.TryParse(InputBox("Upper limit (included)", "Upper limit (included)", "65535"), UpperLimit)
        Running()
        Dim CriteriaPixel As New List(Of String)
        Select Case CurrentData.DataMode
            Case AstroNET.Statistics.eDataMode.UInt16
                'Search all criteria pixels
                With CurrentData.DataProcessor_UInt16.ImageData(0)
                    For Idx1 As Integer = 0 To .NAXIS1 - 1
                        For Idx2 As Integer = 0 To .NAXIS2 - 1
                            If (.Data(Idx1, Idx2) >= LowerLimit) And (.Data(Idx1, Idx2) <= UpperLimit) Then
                                FixedPixelCount += UInt32One
                                CriteriaPixel.Add(Format(.Data(Idx1, Idx2), "00000") & ":" & Format(Idx1, "0000").Trim & ":" & Format(Idx2, "0000"))
                            End If
                        Next Idx2
                    Next Idx1
                End With
                'Load to form
                For Each OpenForm As Form In Application.OpenForms
                    If OpenForm.GetType.Name = "frmSinglePixelStat" Then
                        CType(OpenForm, frmSinglePixelStat).lbHotCandidates.Items.AddRange(CriteriaPixel.ToArray)
                        CType(OpenForm, frmSinglePixelStat).Focus()
                    End If
                Next OpenForm
            Case Else
        End Select
        Log("Fixed " & FixedPixelCount.ValRegIndep & " pixel changed")
        Idle()

    End Sub

    Private Sub FixRADECErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FixRADECErrorToolStripMenuItem.Click
        'Fix the error that RA_NOM which comes first is also DEC_NOM
        Dim DirScanner As New Ato.RecursivDirScanner("\\192.168.100.10\astro\2021_02_13 (SH2-155)")
        DirScanner.Scan("*.fit?")
        Dim FileCount As Integer = 0
        Dim CorrectCount As Integer = 0
        For Each File As String In DirScanner.AllFiles
            FileCount += 1
            Dim Pos1 As Integer = 10            '0-based offset
            Dim Pos2 As Integer = 11            '0-based offset
            Dim Buffer1(6) As Byte : Dim Text1 As String = String.Empty
            Dim Buffer2(6) As Byte : Dim Text2 As String = String.Empty
            Using Stream As System.IO.Stream = System.IO.File.OpenRead(File)
                Stream.Seek((Pos1 - 1) * 80, IO.SeekOrigin.Begin) : Stream.Read(Buffer1, 0, Buffer1.Length) : Text1 = System.Text.Encoding.ASCII.GetString(Buffer1)
                Stream.Seek((Pos2 - 1) * 80, IO.SeekOrigin.Begin) : Stream.Read(Buffer2, 0, Buffer2.Length) : Text2 = System.Text.Encoding.ASCII.GetString(Buffer2)
            End Using
            'Error case ...
            Dim RA_NOM As Byte() = System.Text.Encoding.ASCII.GetBytes("RA_NOM ")
            If (Text1 = "DEC_NOM") And (Text2 = "DEC_NOM") Then
                CorrectCount += 1
                Using Stream As System.IO.Stream = System.IO.File.OpenWrite(File)
                    Stream.Seek((Pos1 - 1) * 80, IO.SeekOrigin.Begin) : Stream.Write(RA_NOM, 0, RA_NOM.Length)
                End Using
            End If
        Next File
        MsgBox(FileCount.ValRegIndep & " files scanned, " & CorrectCount.ValRegIndep & " files corrected")
    End Sub

    Private Sub SubtractMedianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubtractMedianToolStripMenuItem.Click
        Dim Entry As String = InputBox("KSize", "KSize", "3")
        Dim KSize As Integer = -1
        If Integer.TryParse(Entry, KSize) = False Then
            Exit Sub
        Else
            Dim NewData(,) As UInt16 = {}
            cOpenCvSharp.RelToMedian(CurrentData.DataProcessor_UInt16.ImageData(0).Data, KSize)
            CalculateStatistics(CurrentData, DB.BayerPatternNames, CurrentStatistics)
            If DB.AutoOpenStatGraph = True Then PlotStatistics(LastFile, CurrentStatistics)
        End If
    End Sub

    Private Sub GrayPNGToFITSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GrayPNGToFITSToolStripMenuItem.Click

        Dim InputFile As String = "\\192.168.100.10\astro_misc\!Support und Probleme\Christian Zier (Sonnenraster)\Sonne_25.04.21-1-1_Frame34.png"
        Dim PNG As Bitmap = Nothing
        Using FileIn As New System.IO.FileStream(InputFile, IO.FileMode.Open)
            PNG = New Bitmap(Image.FromStream(FileIn))
        End Using

        Dim OutputFile As String = "\\192.168.100.10\astro_misc\!Support und Probleme\Christian Zier (Sonnenraster)\Sonne_25.04.21-1-1_Frame34.fits"
        Dim BitPix As Integer = cFITSWriter.eBitPix.Int16
        Dim BaseOut As New System.IO.StreamWriter(OutputFile)
        Dim BytesOut As New System.IO.BinaryWriter(BaseOut.BaseStream)

        'Create converted data
        Dim ImageData(PNG.Width - 1, PNG.Height - 1) As UInt16
        For Idx1 As Integer = 0 To ImageData.GetUpperBound(1)
            For Idx2 As Integer = 0 To ImageData.GetUpperBound(0)
                ImageData(Idx2, Idx1) = PNG.GetPixel(Idx2, Idx1).R
            Next Idx2
        Next Idx1

        'Load all header elements
        Dim Header As New Dictionary(Of eFITSKeywords, Object)
        Header.Add(eFITSKeywords.SIMPLE, "T")
        Header.Add(eFITSKeywords.BITPIX, BitPix)
        Header.Add(eFITSKeywords.NAXIS, 2)
        Header.Add(eFITSKeywords.NAXIS1, ImageData.GetUpperBound(0) + 1)
        Header.Add(eFITSKeywords.NAXIS2, ImageData.GetUpperBound(1) + 1)
        Header.Add(eFITSKeywords.BZERO, 32768)
        Header.Add(eFITSKeywords.BSCALE, 1)

        'Write header
        BaseOut.Write(cFITSWriter.CreateFITSHeader(Header))
        BaseOut.Flush()

        'Write content
        For Idx1 As Integer = 0 To ImageData.GetUpperBound(1)
            For Idx2 As Integer = 0 To ImageData.GetUpperBound(0)
                BytesOut.Write(GetBytes_BitPix16(CType(ImageData(Idx2, Idx1) - 32768, Int16)))
            Next Idx2
        Next Idx1

        'Finish
        BytesOut.Flush()
        BaseOut.Close()

        MsgBox("OK")

    End Sub

    Private Function GetBytes_BitPix16(ByVal Value As Int16) As Byte()
        Dim RetVal As Byte() = BitConverter.GetBytes(Value)
        Return New Byte() {RetVal(1), RetVal(0)}
    End Function

End Class
