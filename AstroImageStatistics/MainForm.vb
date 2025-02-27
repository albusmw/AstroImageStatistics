Option Explicit On
Option Strict On

Public Class MainForm

    Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal pDst As IntPtr,
                                                                 ByVal pSrc As IntPtr,
                                                                 ByVal ByteLen As Long)

    Const UInt32One As UInt32 = 1

    Private Log As cLog

    '''<summary>Drag-and-drop handler.</summary>
    Private WithEvents DD As Ato.DragDrop

    Private MyStreamDeck As OpenMacroBoard.SDK.IMacroBoard

    '''<summary>Menu - File - Open.</summary>
    Private Sub tsmiFile_Open_Click(sender As Object, e As EventArgs) Handles tsmiFile_Open.Click
        ofdMain.Filter = "FIT(s) files (FIT/FITS/FTS)|*.FIT;*.FITS;*.FTS"
        ofdMain.Multiselect = False
        If ofdMain.ShowDialog <> DialogResult.OK Then Exit Sub
        LoadFile(ofdMain.FileName, AIS.DB.LastFile_Data)
    End Sub

    '''<summary>Load the given file.</summary>
    '''<param name="FileName">File to read in.</param>
    '''<param name="Container">Container for data and statistics.</param>
    '''<returns>Position where the data start.</returns>
    Private Function LoadFileDataOnly(ByVal FileName As String, ByRef Container As AstroNET.Statistics) As cFileProps

        Dim RetVal As New cFileProps(FileName)
        Dim FileNameOnly As String = System.IO.Path.GetFileName(FileName)
        Running()

        If AIS.Config.AutoClearLog = True Then Log.Clear()


        If AIS.Config.UseIPP Then
            Processing.LoadFITSFile(FileName, AIS.DB.IPP, AIS.Config.ForceDirect, AIS.DB.LastFile_FITSHeader, Container, RetVal.DataStartPosition)
        Else
            Processing.LoadFITSFile(FileName, Nothing, AIS.Config.ForceDirect, AIS.DB.LastFile_FITSHeader, Container, RetVal.DataStartPosition)
        End If

        'End
        Idle()
        Return RetVal

    End Function

    '''<summary>Load the given file.</summary>
    '''<param name="FileName">File to read in.</param>
    '''<param name="Container">Container for data and statistics.</param>
    '''<returns>Position where the data start.</returns>
    Private Function LoadFile(ByVal FileName As String) As cFileProps
        Return LoadFile(FileName, AIS.DB.LastFile_Data)
    End Function

    '''<summary>Load the given file.</summary>
    '''<param name="FileName">File to read in.</param>
    '''<param name="Container">Container for data and statistics.</param>
    '''<returns>Position where the data start.</returns>
    Private Function LoadFile(ByVal FileName As String, ByRef Container As AstroNET.Statistics) As cFileProps

        Dim RetVal As New cFileProps(FileName)
        Dim FileNameOnly As String = System.IO.Path.GetFileName(FileName)
        Running()

        If AIS.Config.AutoClearLog = True Then Log.Clear()

        If AIS.Config.UseIPP Then
            Processing.LoadFITSFile(FileName, AIS.DB.IPP, AIS.Config.ForceDirect, AIS.DB.LastFile_FITSHeader, Container, RetVal.DataStartPosition)
        Else
            Processing.LoadFITSFile(FileName, Nothing, AIS.Config.ForceDirect, AIS.DB.LastFile_FITSHeader, Container, RetVal.DataStartPosition)
        End If

        RetVal.FITSHeader = AIS.DB.LastFile_FITSHeader.GetCardsAsDictionary

        '=========================================================================================================
        'Display fits header

        AIS.DB.LastFile_Name = FileName
        Log.Log("Loading file <" & FileName & "> ...")
        Log.Log("  -> <" & System.IO.Path.GetFileNameWithoutExtension(FileName) & ">")
        Log.Log("FITS header:")
        Dim ContentToPrint As List(Of String) = cFITSHeaderParser.GetListToDisplay(RetVal.FITSHeader)
        Log.Log(ContentToPrint)
        Log.Log(New String("-"c, 107))

        '=========================================================================================================
        'Calculate the statistics

        Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(Container, AIS.Config.CalcStat_Mono, AIS.Config.CalcStat_Bayer, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
        Log.Log(StatisticsReport)

        'Record statistics
        RetVal.Statistics = AIS.DB.LastFile_Statistics

        '=========================================================================================================
        'Plot statistics and remember this file as last processed file
        If AIS.Config.AutoOpenStatGraph = True Then PlotStatistics(FileName, AIS.DB.LastFile_Statistics)
        'Me.Focus()

        'Store recent file
        AIS.StoreRecentFile(FileName)

        Idle()
        Return RetVal

    End Function

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
        With AIS.DB.LastFile_Statistics
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
        AIS.DB.AllPlots.Add(New cZEDGraphForm)
        Dim Disp As cZEDGraphForm = AIS.DB.AllPlots.Last
        With Disp
            AddHandler .PointValueHandler, AddressOf PointValueHandler
            .PlotData("Test", New Double() {1, 2, 3, 4}, Color.Red)
            Dim XAxisMargin As Integer = 128                                    'axis margin to see the most outer values
            Select Case Stats.DataMode
                Case AstroNET.Statistics.eDataMode.Fixed
                    'Plot histogram
                    .Plotter.Clear()
                    If IsNothing(Stats.BayerHistograms_Int) = False And AIS.Config.CalcStat_Bayer Then
                        .Plotter.PlotXvsY(AIS.Config.BayerPatternName(0) & "[0,0]", Stats.BayerHistograms_Int(0, 0), 1, New cZEDGraph.sGraphStyle(Color.Red, AIS.Config.PlotStyle, 1))
                        .Plotter.PlotXvsY(AIS.Config.BayerPatternName(1) & "[0,1]", Stats.BayerHistograms_Int(0, 1), 1, New cZEDGraph.sGraphStyle(Color.LightGreen, AIS.Config.PlotStyle, 1))
                        .Plotter.PlotXvsY(AIS.Config.BayerPatternName(2) & "[1,0]", Stats.BayerHistograms_Int(1, 0), 1, New cZEDGraph.sGraphStyle(Color.Green, AIS.Config.PlotStyle, 1))
                        .Plotter.PlotXvsY(AIS.Config.BayerPatternName(3) & "[1,1]", Stats.BayerHistograms_Int(1, 1), 1, New cZEDGraph.sGraphStyle(Color.Blue, AIS.Config.PlotStyle, 1))
                    End If
                    If IsNothing(Stats.MonochromHistogram_Int) = False And AIS.Config.CalcStat_Mono Then
                        .Plotter.PlotXvsY("Mono histo", Stats.MonochromHistogram_Int, 1, New cZEDGraph.sGraphStyle(Color.Black, AIS.Config.PlotStyle, 1))
                    End If
                    .Plotter.ManuallyScaleXAxisLin(Stats.MonoStatistics_Int.Min.Key - XAxisMargin, Stats.MonoStatistics_Int.Max.Key + XAxisMargin)
                Case AstroNET.Statistics.eDataMode.Float
                    'Plot histogram
                    .Plotter.Clear()
                    If IsNothing(Stats.BayerHistograms_Float32) = False And AIS.Config.CalcStat_Bayer Then
                        .Plotter.PlotXvsY(AIS.Config.BayerPatternName(0) & "[0,0]", Stats.BayerHistograms_Float32(0, 0), 1, New cZEDGraph.sGraphStyle(Color.Red, AIS.Config.PlotStyle, 1))
                        .Plotter.PlotXvsY(AIS.Config.BayerPatternName(1) & "[0,1]", Stats.BayerHistograms_Float32(0, 1), 1, New cZEDGraph.sGraphStyle(Color.LightGreen, AIS.Config.PlotStyle, 1))
                        .Plotter.PlotXvsY(AIS.Config.BayerPatternName(2) & "[1,0]", Stats.BayerHistograms_Float32(1, 0), 1, New cZEDGraph.sGraphStyle(Color.Green, AIS.Config.PlotStyle, 1))
                        .Plotter.PlotXvsY(AIS.Config.BayerPatternName(3) & "[1,1]", Stats.BayerHistograms_Float32(1, 1), 1, New cZEDGraph.sGraphStyle(Color.Blue, AIS.Config.PlotStyle, 1))
                    End If
                    If IsNothing(Stats.MonochromHistogram_Float32) = False And AIS.Config.CalcStat_Mono Then
                        .Plotter.PlotXvsY("Mono histo", Stats.MonochromHistogram_Float32, 1, New cZEDGraph.sGraphStyle(Color.Black, AIS.Config.PlotStyle, 1))
                    End If
                    .Plotter.ManuallyScaleXAxisLin(Stats.MonoStatistics_Int.Min.Key - XAxisMargin, Stats.MonoStatistics_Int.Max.Key + XAxisMargin)
            End Select
            .Plotter.AutoScaleYAxisLog()
            .Plotter.GridOnOff(True, True)
            .Plotter.ForceUpdate()
            .Plotter.MaximizePlotArea()
            'Set style of the window
            .Plotter.SetCaptions(String.Empty, "Pixel value", "# of pixel with this value")
            .HostForm.Text = FileName
            .HostForm.Icon = Me.Icon
            .Tag = "Statistics"
            'Position window below the main window
            If AIS.Config.StackGraphs = True Then
                .HostForm.Left = Me.Left
                .HostForm.Top = Me.Top + Me.Height
                .HostForm.Height = Me.Height
                .HostForm.Width = Me.Width
            End If
        End With
    End Sub

    Private Sub PlotStatistics(ByVal FileName As String, ByRef Stats() As Ato.cSingleValueStatistics)
        Dim Disp As New cZEDGraphForm
        Disp.PlotData("Test", New Double() {1, 2, 3, 4}, Color.Red)
        'Plot data
        Dim XAxis() As Double = Ato.cSingleValueStatistics.GetAspectVectorXAxis(Stats)
        Disp.Plotter.Clear()
        Disp.Plotter.PlotXvsY("Mean", XAxis, Ato.cSingleValueStatistics.GetAspectVector(Stats, Ato.cSingleValueStatistics.eAspects.Mean), New cZEDGraph.sGraphStyle(Color.Black, AIS.Config.PlotStyle, 1))
        Disp.Plotter.PlotXvsY("Max", XAxis, Ato.cSingleValueStatistics.GetAspectVector(Stats, Ato.cSingleValueStatistics.eAspects.Maximum), New cZEDGraph.sGraphStyle(Color.Red, AIS.Config.PlotStyle, 1))
        Disp.Plotter.PlotXvsY("Min", XAxis, Ato.cSingleValueStatistics.GetAspectVector(Stats, Ato.cSingleValueStatistics.eAspects.Minimum), New cZEDGraph.sGraphStyle(Color.Green, AIS.Config.PlotStyle, 1))
        Disp.Plotter.PlotXvsY("Sigma", XAxis, Ato.cSingleValueStatistics.GetAspectVector(Stats, Ato.cSingleValueStatistics.eAspects.Sigma), New cZEDGraph.sGraphStyle(Color.Orange, AIS.Config.PlotStyle, 1), True)
        Disp.Plotter.ManuallyScaleXAxisLin(XAxis(0), XAxis(XAxis.GetUpperBound(0)))
        Disp.Plotter.GridOnOff(True, True)
        Disp.Plotter.ForceUpdate()
        'Set style of the window
        Disp.Plotter.SetCaptions(String.Empty, "Pixel index", "Statistics value")
        Disp.Plotter.MaximizePlotArea()
        Disp.HostForm.Text = FileName
        Disp.HostForm.Icon = Me.Icon
        'Position window below the main window
        Disp.HostForm.Left = Me.Left
        Disp.HostForm.Top = Me.Top + Me.Height
        Disp.HostForm.Height = Me.Height
        Disp.HostForm.Width = Me.Width
    End Sub

    Private Sub PlotStatistics(ByVal PlotName As String, ByRef Trace As Dictionary(Of UInt16, UInt32), ByVal XNorm As Double)
        Dim Disp As New cZEDGraphForm
        With Disp
            .PlotData("Test", New Double() {1, 2, 3, 4}, Color.Red)
            'Plot data
            .Plotter.Clear()
            .Plotter.PlotXvsY("Data", Trace, XNorm, 1, New cZEDGraph.sGraphStyle(Color.Black, AIS.Config.PlotStyle, 1))
            .Plotter.ManuallyScaleXAxisLin(Trace.Keys.First, Trace.Keys.Last)
            .Plotter.AutoScaleYAxisLog()
            .Plotter.GridOnOff(True, True)
            .Plotter.ForceUpdate()
            'Set style of the window
            .Plotter.SetCaptions(String.Empty, "X", "Y")
            .Plotter.MaximizePlotArea()
            .HostForm.Text = PlotName
            .HostForm.Icon = Me.Icon
            'Position window below the main window
            .HostForm.Left = Me.Left
            .HostForm.Top = Me.Top + Me.Height
            .HostForm.Height = Me.Height
            .HostForm.Width = Me.Width
        End With
    End Sub

    Private Sub PlotStatistics(ByVal FileName As String, ByRef Stats As Dictionary(Of Double, AstroImageStatistics.AstroNET.Statistics.cSingleChannelStatistics_Int))
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
        Disp.Plotter.PlotXvsY("StdDev", XAxis.ToArray, YAxis.ToArray, New cZEDGraph.sGraphStyle(Color.Black, cZEDGraph.eCurveMode.Dots, 1))
        Disp.Plotter.GridOnOff(True, True)
        Disp.Plotter.ForceUpdate()
        'Set style of the window
        Disp.Plotter.SetCaptions(String.Empty, "Gain", "StdDev")
        Disp.Plotter.MaximizePlotArea()
        Disp.HostForm.Text = FileName
        Disp.HostForm.Icon = Me.Icon
        'Position window below the main window
        Disp.HostForm.Left = Me.Left
        Disp.HostForm.Top = Me.Top + Me.Height
        Disp.HostForm.Height = Me.Height
        Disp.HostForm.Width = Me.Width
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Get build data
        Me.Text = GetBuildDateTime.GetMainformTitle

        'Load IPP
        Dim IPPLoadError = String.Empty
        Dim IPPPathToUse = cIntelIPP.SearchDLLToUse(cIntelIPP.PossiblePaths(AIS.DB.MyPath).ToArray, IPPLoadError)
        If String.IsNullOrEmpty(IPPLoadError) = True Then
            AIS.DB.IPP = New cIntelIPP(IPPPathToUse)
            cFITSWriter.UseIPPForWriting = True
        Else
            cFITSWriter.UseIPPForWriting = False
        End If
        cFITSWriter.IPPPath = AIS.DB.IPP.IPPPath
        'cFITSReader.IPPPath = AIS.DB.IPP.IPPPath

        'Set FITS viewer
        Dim FileName As String = "FITSWork4.exe"
        Dim Locations As List(Of String) = Everything.GetExactMatch(FileName, Everything.GetSearchResult(FileName))
        If Locations.Count > 0 Then AIS.Config.FITSViewer = Locations(0)

        'Init drap-and-drop
        DD = New Ato.DragDrop(tbLogOutput, False)

        'Test
        Dim X As New ASCOMDynamic
        X.CallTest()

    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'Init GUI
        pgMain.SelectedObject = AIS.Config
        Log = New cLog(tbLogOutput, tsslMain)

        'If a file is droped to the EXE (icon), use this as filename
        With My.Application
            If .CommandLineArgs.Count > 0 Then
                Dim FileName = .CommandLineArgs.Item(0)
                If IO.File.Exists(FileName) Then LoadFile(FileName, AIS.DB.LastFile_Data)
            End If
        End With

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub OpenEXELocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenEXELocationToolStripMenuItem.Click
        Utils.StartWithItsEXE(AIS.DB.MyPath)
    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub DD_DropOccured(Files() As String) Handles DD.DropOccured
        'Handle drag-and-drop for the first dropped FIT(s) file
        Dim AllFiles As New List(Of String)
        For Each File As String In Files
            If System.IO.Path.GetExtension(File).ToUpper.StartsWith(".FIT") Then
                LoadFile(File)
                Exit Sub
            End If
        Next File
    End Sub

    Private Sub tsmiFile_OpenLastFile_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenLastFile.Click
        Dim OpenError As String = AIS.OpenFile(AIS.DB.LastFile_Name)
        If String.IsNullOrEmpty(OpenError) = False Then Log.Log(OpenError)
    End Sub

    Private Sub RowAndColumnStatisticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_RowColStat.Click

        Running()

        Dim DataProcessed As Boolean = False

        '1. Load data
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                With AIS.DB.LastFile_Data.DataProcessor_UInt16
                    ReDim AIS.DB.LastFile_EvalResults.StatPerRow(.ImageData(0).Data.GetUpperBound(1)) : InitStat(AIS.DB.LastFile_EvalResults.StatPerRow)
                    ReDim AIS.DB.LastFile_EvalResults.StatPerCol(.ImageData(0).Data.GetUpperBound(0)) : InitStat(AIS.DB.LastFile_EvalResults.StatPerCol)
                    For Idx1 As Integer = 0 To .ImageData(0).Data.GetUpperBound(0)
                        For Idx2 As Integer = 0 To .ImageData(0).Data.GetUpperBound(1)
                            AIS.DB.LastFile_EvalResults.StatPerRow(Idx2).AddValue(.ImageData(0).Data(Idx1, Idx2))
                            AIS.DB.LastFile_EvalResults.StatPerCol(Idx1).AddValue(.ImageData(0).Data(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                    DataProcessed = True
                End With
            Case AstroNET.Statistics.eDataType.UInt32
                With AIS.DB.LastFile_Data.DataProcessor_UInt32
                    ReDim AIS.DB.LastFile_EvalResults.StatPerRow(.ImageData(0).Data.GetUpperBound(1)) : InitStat(AIS.DB.LastFile_EvalResults.StatPerRow)
                    ReDim AIS.DB.LastFile_EvalResults.StatPerCol(.ImageData(0).Data.GetUpperBound(0)) : InitStat(AIS.DB.LastFile_EvalResults.StatPerCol)
                    For Idx1 As Integer = 0 To .ImageData(0).Data.GetUpperBound(0)
                        For Idx2 As Integer = 0 To .ImageData(0).Data.GetUpperBound(1)
                            AIS.DB.LastFile_EvalResults.StatPerRow(Idx2).AddValue(.ImageData(0).Data(Idx1, Idx2))
                            AIS.DB.LastFile_EvalResults.StatPerCol(Idx1).AddValue(.ImageData(0).Data(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                    DataProcessed = True
                End With
            Case AstroNET.Statistics.eDataType.Int32
                With AIS.DB.LastFile_Data.DataProcessor_Int32
                    ReDim AIS.DB.LastFile_EvalResults.StatPerRow(.ImageData.GetUpperBound(1)) : InitStat(AIS.DB.LastFile_EvalResults.StatPerRow)
                    ReDim AIS.DB.LastFile_EvalResults.StatPerCol(.ImageData.GetUpperBound(0)) : InitStat(AIS.DB.LastFile_EvalResults.StatPerCol)
                    For Idx1 As Integer = 0 To .ImageData.GetUpperBound(0)
                        For Idx2 As Integer = 0 To .ImageData.GetUpperBound(1)
                            AIS.DB.LastFile_EvalResults.StatPerRow(Idx2).AddValue(.ImageData(Idx1, Idx2))
                            AIS.DB.LastFile_EvalResults.StatPerCol(Idx1).AddValue(.ImageData(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                    DataProcessed = True
                End With
        End Select

        '2. Plot data
        If DataProcessed = True Then
            PlotStatistics(AIS.DB.LastFile_Name & " - ROW STAT", AIS.DB.LastFile_EvalResults.StatPerRow)
            PlotStatistics(AIS.DB.LastFile_Name & " - COL STAT", AIS.DB.LastFile_EvalResults.StatPerCol)
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

    Private Sub tsmiFile_SaveLastStat_Click(sender As Object, e As EventArgs) Handles tsmiFile_SaveLastStat.Click

        Dim AddHisto As Boolean = True

        With sfdMain
            .Filter = "EXCEL file (*.xlsx)|*.xlsx"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        Using workbook As New ClosedXML.Excel.XLWorkbook

            '1.) Histogram
            If AddHisto = True Then
                Dim XY As New List(Of Object())
                For Each Key As Long In AIS.DB.LastFile_Statistics.MonochromHistogram_Int.Keys
                    Dim Values As New List(Of Object)
                    Values.Add(Key)
                    Values.Add(AIS.DB.LastFile_Statistics.MonochromHistogram_Int(Key))
                    If AIS.DB.LastFile_Statistics.BayerHistograms_Int(0, 0).ContainsKey(Key) Then Values.Add(AIS.DB.LastFile_Statistics.BayerHistograms_Int(0, 0)(Key)) Else Values.Add(String.Empty)
                    If AIS.DB.LastFile_Statistics.BayerHistograms_Int(0, 1).ContainsKey(Key) Then Values.Add(AIS.DB.LastFile_Statistics.BayerHistograms_Int(0, 1)(Key)) Else Values.Add(String.Empty)
                    If AIS.DB.LastFile_Statistics.BayerHistograms_Int(1, 0).ContainsKey(Key) Then Values.Add(AIS.DB.LastFile_Statistics.BayerHistograms_Int(1, 0)(Key)) Else Values.Add(String.Empty)
                    If AIS.DB.LastFile_Statistics.BayerHistograms_Int(1, 1).ContainsKey(Key) Then Values.Add(AIS.DB.LastFile_Statistics.BayerHistograms_Int(1, 1)(Key)) Else Values.Add(String.Empty)
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
            For Each Key As UInteger In AIS.DB.LastFile_Statistics.MonoStatistics_Int.HistXDist.Keys
                HistDens.Add(New Object() {Key, AIS.DB.LastFile_Statistics.MonoStatistics_Int.HistXDist(Key)})
            Next Key
            Dim worksheet2 As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Histogram Density")
            worksheet2.Cell(1, 1).InsertData(New List(Of String)({"Step size", "Count"}), True)
            worksheet2.Cell(2, 1).InsertData(HistDens)
            For Each col In worksheet2.ColumnsUsed
                col.AdjustToContents()
            Next col

            '3.) Row and column
            If IsNothing(AIS.DB.LastFile_EvalResults.StatPerRow) = False Then
                With New cSingleValStatToXLS
                    .Save(workbook.Worksheets.Add("Row Statistics"), AIS.DB.LastFile_EvalResults.StatPerRow)
                End With
            End If
            If IsNothing(AIS.DB.LastFile_EvalResults.StatPerCol) = False Then
                With New cSingleValStatToXLS
                    .Save(workbook.Worksheets.Add("Column Statistics"), AIS.DB.LastFile_EvalResults.StatPerCol)
                End With
            End If

            '4) Save and open
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Utils.StartWithItsEXE(FileToGenerate)

        End Using

    End Sub

    Private Sub tsmiProcessing_AdjustRGB_Click(sender As Object, e As EventArgs) Handles tsmiProcessing_AdjustRGB.Click

        'Calculate the maximum modus (the most propable value in the channel) and normalize all channels to this channel
        Running()
        Dim ClipCount(1, 1) As Integer
        Dim Norm(1, 1) As Double
        If AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Length > 0 Then
            Dim ModusRef As Long = Long.MinValue
            For BayerIdx1 As Integer = 0 To 1
                For BayerIdx2 As Integer = 0 To 1
                    ModusRef = Math.Max(ModusRef, AIS.DB.LastFile_Statistics.BayerStatistics_Int(BayerIdx1, BayerIdx2).Modus.Key)
                Next BayerIdx2
            Next BayerIdx1
            For BayerIdx1 As Integer = 0 To 1
                For BayerIdx2 As Integer = 0 To 1
                    ClipCount(BayerIdx1, BayerIdx2) = 0
                    Norm(BayerIdx1, BayerIdx2) = ModusRef / AIS.DB.LastFile_Statistics.BayerStatistics_Int(BayerIdx1, BayerIdx2).Modus.Key
                    If ModusRef <> AIS.DB.LastFile_Statistics.BayerStatistics_Int(BayerIdx1, BayerIdx2).Modus.Key Then                                                        'skip channels that do not need a change
                        For PixelIdx1 As Integer = BayerIdx1 To AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(0) Step 2
                            For PixelIdx2 As Integer = BayerIdx2 To AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(1) Step 2
                                Dim NewValue As Double = Math.Round(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data(PixelIdx1, PixelIdx2) * Norm(BayerIdx1, BayerIdx2))
                                If NewValue > UInt16.MaxValue Then
                                    AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data(PixelIdx1, PixelIdx2) = UInt16.MaxValue
                                    ClipCount(BayerIdx1, BayerIdx2) += 1
                                Else
                                    AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data(PixelIdx1, PixelIdx2) = CUShort(NewValue)
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
                Log.Log("Channel [" & BayerIdx1.ValRegIndep & ":" & BayerIdx2.ValRegIndep & "]: Norm <" & Norm(BayerIdx1, BayerIdx2).ToString.Trim & ">, clip count " & ClipCount(BayerIdx1, BayerIdx2).ValRegIndep)
            Next BayerIdx2
        Next BayerIdx1

        Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
        Log.Log(StatisticsReport)
        Idle()

    End Sub

    Private Sub tsmiSaveImageData_Click(sender As Object, e As EventArgs) Handles tsmiSaveImageData.Click
        Dim MyDialog As New frmSaveFile
        Running()
        Dim Result As DialogResult = MyDialog.ShowDialog()
        If Result = DialogResult.OK Then
            For Each entry As String In MyDialog.SavedFiles
                Log.Log("Saved as <" & entry & ">")
            Next entry
        End If
        Idle()
    End Sub

    Private Sub tsmiStretch_Click(sender As Object, e As EventArgs) Handles tsmiStretch.Click
        Running()
        ImageProcessing.MakeHistoStraight(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data)
        Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
        Log.Log(StatisticsReport)
        Idle()
    End Sub

    '''<summary>Processing running.</summary>
    Private Sub Running()
        tsslRunning.ForeColor = Drawing.Color.Red
        DE()
    End Sub

    '''<summary>Processing running.</summary>
    Private Function Running(ByVal RunningStep As String) As Stopwatch
        tsslRunning.ForeColor = Drawing.Color.Red
        Log.Log("Started <" & RunningStep & ">")
        DE()
        Dim RetVal As New Stopwatch
        RetVal.Reset() : RetVal.Start()
        Return RetVal
    End Function

    '''<summary>Processing running.</summary>
    Private Sub Running(ByRef Stopper As Stopwatch)
        tsslRunning.ForeColor = Drawing.Color.Red
        DE()
        Stopper.Restart() : Stopper.Start()
    End Sub

    '''<summary>Processing idle.</summary>
    Private Sub Idle()
        tsslRunning.ForeColor = Drawing.Color.Silver
        DE()
    End Sub

    '''<summary>Processing idle.</summary>
    Private Sub Idle(ByRef Stopper As Stopwatch, ByVal Message As String)
        Stopper.Stop()
        tsslRunning.ForeColor = Drawing.Color.Silver
        Log.Log("Finished <" & Message & ">, elapsed time: " & Stopper.ElapsedMilliseconds.ToString.Trim & " ms")
        DE()
    End Sub

    Private Sub tsmiAnalysisPlot_ADUQuant_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_Plot_ADUQuant.Click
        Dim Disp As New cZEDGraphForm
        Dim PlotData As Generic.Dictionary(Of Long, UInt64) = AstroNET.Statistics.GetQuantizationHisto(AIS.DB.LastFile_Statistics.MonochromHistogram_Int)
        Dim XAxis As Double() = PlotData.Keys.ToDouble
        Disp.PlotData("Test", New Double() {1, 2, 3, 4}, Color.Red)
        'Plot data
        Disp.Plotter.Clear()
        Disp.Plotter.PlotXvsY("Mono", XAxis, PlotData.Values.ToArray.ToDouble, New cZEDGraph.sGraphStyle(Color.Black, AIS.Config.PlotStyle, 1))
        Disp.Plotter.GridOnOff(True, True)
        Disp.Plotter.ManuallyScaleXAxisLin(XAxis(0), XAxis(XAxis.GetUpperBound(0)))
        Disp.Plotter.AutoScaleYAxisLog()
        Disp.Plotter.ForceUpdate()
        'Set style of the window
        Disp.Plotter.SetCaptions(String.Empty, "ADU step size", "# found")
        Disp.Plotter.MaximizePlotArea()
        Disp.HostForm.Icon = Me.Icon
    End Sub

    Private Sub tsmiPlateSolve_Click(sender As Object, e As EventArgs) Handles tsmiPlateSolve.Click
        Dim SolverLog As String() = {}
        Dim ErrorCode As String = AstroImageStatistics_Fun.PlateSolve(AIS.DB.LastFile_Name, AIS.Config.PlateSolve2Path, AIS.Config.PlateSolve2HoldOpen, SolverLog)
        If String.IsNullOrEmpty(ErrorCode) = True Then
            Log.Log("Plate solve results: > ", SolverLog)
        Else
            Log.Log("Plate solve FAILED: <" & ErrorCode & ">")
        End If
    End Sub

    Private Sub tsmiFile_FITSGrep_Click(sender As Object, e As EventArgs) Handles tsmiFile_FITSGrep.Click
        Dim X As New frmFITSGrep : X.Show()
    End Sub

    'Private Sub tsmiTest_Focus_Click(sender As Object, e As EventArgs) Handles tsmiTest_Focus.Click

    '    'Focus analysis test code
    '    'Test files: \\192.168.100.10\astro\2021_02_12 (Focus)\Small


    '    Dim StatFocusPoint As New Dictionary(Of Integer, Dictionary(Of Long, ULong))

    '    'Select EXCEL file name
    '    With sfdMain
    '        .Filter = "EXCEL file (*.xlsx)|*.xlsx"
    '        .FileName = "FocusAnalysis.xlsx"
    '        If .ShowDialog <> DialogResult.OK Then Exit Sub
    '    End With

    '    '====================================================================================================
    '    'Calculate combined statistics for all equal focus positions
    '    For Each FileName As String In AllFilesEverRead.Keys

    '        Dim FileNameOnly As String = System.IO.Path.GetFileNameWithoutExtension(FileName)
    '        Dim FocusPos As Integer = FocusFromFileName(FileNameOnly)

    '        If StatFocusPoint.ContainsKey(FocusPos) = False Then
    '            StatFocusPoint.Add(FocusPos, AllFilesEverRead(FileName).Statistics.MonochromHistogram_Int.Clone)
    '        Else
    '            AstroNET.Statistics.CombineHisto(StatFocusPoint(FocusPos), AllFilesEverRead(FileName).Statistics.MonochromHistogram_Int)
    '        End If

    '    Next FileName

    '    '====================================================================================================
    '    'Generate the EXCEL output
    '    Dim ExcelRow As Integer = 0
    '    Using workbook As New ClosedXML.Excel.XLWorkbook

    '        Dim WorkSheet_Single As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("File Statistics")
    '        WorkSheet_Single.Cell(1, 1).InsertData(New List(Of String)({"Filename", "Focus position", "Total Energy", "Focus Quality Indicator"}), True)
    '        ExcelRow = 2

    '        For Each FileName As String In AllFilesEverRead.Keys

    '            Dim FileNameOnly As String = System.IO.Path.GetFileNameWithoutExtension(FileName)
    '            Dim FocusPos As Integer = FocusFromFileName(FileNameOnly)

    '            'Save raw data
    '            WorkSheet_Single.Cell(ExcelRow, 1).Value = FileNameOnly
    '            WorkSheet_Single.Cell(ExcelRow, 2).Value = FocusPos

    '            'Calculate total energy
    '            WorkSheet_Single.Cell(ExcelRow, 3).Value = AstroNET.Statistics.TotalEnergy(AllFilesEverRead(FileName).Statistics.MonochromHistogram_Int)
    '            WorkSheet_Single.Cell(ExcelRow, 4).Value = AstroNET.Statistics.FocusQualityIndicator(AllFilesEverRead(FileName).Statistics.MonochromHistogram_Int, 5.0)

    '            ExcelRow += 1

    '        Next FileName

    '        'Calculate sum statistics
    '        Dim WorkSheet_Sum As ClosedXML.Excel.IXLWorksheet = workbook.Worksheets.Add("Focus points")
    '        WorkSheet_Sum.Cell(1, 1).InsertData(New List(Of String)({"Focus position", "Total Energy", "95-Percentile", "Modus", "Focus Quality Indicator"}), True)
    '        ExcelRow = 2

    '        Dim FocusPoint As New List(Of Integer)(StatFocusPoint.Keys)
    '        For Each FocusPos As Integer In FocusPoint

    '            WorkSheet_Sum.Cell(ExcelRow, 1).Value = FocusPos
    '            WorkSheet_Sum.Cell(ExcelRow, 2).Value = AstroNET.Statistics.TotalEnergy(StatFocusPoint(FocusPos))
    '            Dim Results As AstroNET.Statistics.cSingleChannelStatistics_Int = AstroNET.Statistics.CalcStatValuesFromHisto(StatFocusPoint(FocusPos))
    '            WorkSheet_Sum.Cell(ExcelRow, 3).Value = Results.Percentile(95)
    '            WorkSheet_Sum.Cell(ExcelRow, 4).Value = Results.Modus.Key
    '            WorkSheet_Sum.Cell(ExcelRow, 5).Value = AstroNET.Statistics.FocusQualityIndicator(StatFocusPoint(FocusPos), 5.0)
    '            ExcelRow += 1

    '        Next FocusPos

    '        '4) Save and open
    '        For Each col In WorkSheet_Single.ColumnsUsed
    '            col.AdjustToContents()
    '        Next col
    '        For Each col In WorkSheet_Sum.ColumnsUsed
    '            col.AdjustToContents()
    '        Next col
    '        Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, sfdMain.FileName)
    '        workbook.SaveAs(FileToGenerate)
    '        Process.Start(FileToGenerate)

    '    End Using

    '    '====================================================================================================
    '    'Create focus plot
    '    Dim Disp1 As New cZEDGraphForm
    '    Dim LineGen As New cZEDGraphService.cLineStyleGenerator
    '    Dim AllFocusPoints As New List(Of Integer) : AllFocusPoints.AddRange(StatFocusPoint.Keys)
    '    For Each FocusPos As Integer In AllFocusPoints
    '        'Dim Plot_X As New List(Of Double)
    '        'Dim Plot_Y As New List(Of Double)
    '        'FocusAnalysis( StatFocusPoint(FocusPos), Plot_X, Plot_Y)
    '        Dim Plot_X As Double() = {}
    '        Dim Plot_Y As Double() = {}
    '        'Dim Plot_Y As New List(Of Double)
    '        AstroNET.Statistics.EnergyCCDF(StatFocusPoint(FocusPos), 0.01 * AstroNET.Statistics.TotalEnergy(StatFocusPoint(FocusPos)), Plot_X, Plot_Y)
    '        Disp1.PlotData("Focus - <" & FocusPos & ">", Plot_X, Plot_Y, LineGen.GetNextColor)
    '    Next FocusPos
    '    Disp1.MakeYAxisLog()

    'End Sub

    Private Function FocusFromFileName(ByVal FileNameOnly As String) As Integer
        Return CInt(FileNameOnly.Split("_"c)(0))
    End Function



    '''<summary>Focus analysis based on maximum enery in N percent of the pixel.</summary>
    Private Function FocusAnalysis(ByVal Hist As Dictionary(Of Long, ULong), ByRef Plot_X As List(Of Double), ByRef Plot_Y As List(Of Double)) As Boolean

        Dim PlotHistDirect As Boolean = True

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
            If PlotHistDirect Then
                Plot_Y.Add(Hist(Entry))
            Else
                SumEnergy += CLng((Entry * Hist(Entry)))
                Plot_Y.Add(100 * (SumEnergy / TotalEnery))
            End If
        Next Entry

        Return True

    End Function

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.F1 Then
            Dim RTFTextBox As New cRTFTextBox
            Dim AllResources = Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames
            For Each Entry In AllResources
                If Entry.EndsWith(".HelpContent.rtf") Then
                    RTFTextBox.ShowText(New IO.StreamReader(Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(Entry)).ReadToEnd.Trim)
                    Exit For
                End If
            Next Entry
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub tsmiFile_OpenRecent_Click(sender As Object, e As EventArgs) Handles tsmiFile_OpenRecent.Click
        Using LastOpenedFiles As New frmLastOpenedFiles(AIS.GetRecentFiles)
            If LastOpenedFiles.ShowDialog <> DialogResult.OK Then Exit Sub
            LoadFile(LastOpenedFiles.SelectedFile)
        End Using
    End Sub

    Private Sub tsmiSetPixelToValue_Click(sender As Object, e As EventArgs) Handles tsmiSetPixelToValue.Click

        Dim FixedPixelCount As UInt32 = 0
        Dim Limit As UShort = CUShort(InputBox("Upper limit (included)", "65536"))
        Dim SetTo As UShort = CUShort(InputBox("Set to", "0"))
        Running()
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
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
        Log.Log("Fixed " & FixedPixelCount.ValRegIndep & " pixel changed")
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
            cOpenCvSharp.MedianBlur(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data, KSize)
            Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
            Log.Log(StatisticsReport)
            If AIS.Config.AutoOpenStatGraph = True Then PlotStatistics(AIS.DB.LastFile_Name, AIS.DB.LastFile_Statistics)
        End If
    End Sub

    Private Sub tsmiTools_ALADINCoords_Click(sender As Object, e As EventArgs) Handles tsmiTools_ALADINCoords.Click

        Dim FileToRun As String = AIS.DB.LastFile_Name

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
        ASCOMDynamic.JNowToJ2000(File_RA_JNow.ParseRA, File_Dec_JNow.ParseDegree, File_RA_J2000, File_Dec_J2000)

        Dim AladinCall As String = File_RA_J2000.ToHMS & " " & File_Dec_J2000.ToDegMinSec

        'Possible resolvers:
        'http://tdc-www.harvard.edu/astro.image.html

        Clipboard.Clear()
        Clipboard.SetText(AladinCall)

    End Sub

    Private Sub tsmiAnalysis_ManualColorBalancer_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_ManualColorBalancer.Click
        Dim X As New frmManualAdjust
        X.Show()
    End Sub

    Private Sub tsmiAnalysisVignette_CalcRaw_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_CalcRaw.Click
        CalcVignette()
    End Sub

    Private Sub tsmiAnalysisVignette_Display_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_Display.Click
        If IsNothing(AIS.DB.LastFile_EvalResults.Vig_RawData) = False Then
            If AIS.DB.LastFile_EvalResults.Vig_RawData.Count > 0 Then
                Dim Disp1 As New cZEDGraphForm : Disp1.PlotData("Vignette", AIS.DB.LastFile_EvalResults.Vig_RawData, Color.Red)
            End If
        End If
    End Sub

    '''<summary>Calculate the vignette.</summary>
    Private Sub CalcVignette()
        Running()
        Dim Stopper As New cStopper
        Stopper.Start()
        AIS.DB.LastFile_EvalResults.Vig_RawData = New Dictionary(Of Double, Double)
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                AIS.DB.LastFile_EvalResults.Vig_RawData = ImageProcessing.Vignette(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data)
            Case AstroNET.Statistics.eDataType.UInt32
                AIS.DB.LastFile_EvalResults.Vig_RawData = ImageProcessing.Vignette(AIS.DB.LastFile_Data.DataProcessor_UInt32.ImageData(0).Data)
        End Select
        AIS.DB.LastFile_EvalResults.Vig_RawData = AIS.DB.LastFile_EvalResults.Vig_RawData.SortDictionary
        Log.Log(Stopper.Stamp("Vignette - getting data (" & AIS.DB.LastFile_EvalResults.Vig_RawData.Count & " values"))
        Idle()
    End Sub

    Private Sub tsmiAnalysisVignette_Clear_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_Clear.Click
        AIS.DB.LastFile_EvalResults.Vig_RawData = New Dictionary(Of Double, Double)
    End Sub

    Private Sub tsmiAnalysisVignette_Correct_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_Correct.Click

        Dim Stopper As New cStopper : Stopper.Start()
        Running()

        'Normalize
        AIS.DB.IPP.DivC(AIS.DB.LastFile_EvalResults.Vig_Fitting, AIS.DB.IPP.Max(AIS.DB.LastFile_EvalResults.Vig_Fitting))
        Dim NormMin As Double = AIS.DB.IPP.Min(AIS.DB.LastFile_EvalResults.Vig_Fitting)
        AIS.DB.IPP.DivC(AIS.DB.LastFile_EvalResults.Vig_Fitting, NormMin)
        Dim UsedVignette_Correction As New Dictionary(Of Double, Double)
        Dim YPtr As Integer = 0
        For Each Entry As Double In AIS.DB.LastFile_EvalResults.Vig_BinUsedData.Keys
            UsedVignette_Correction.Add(Entry, AIS.DB.LastFile_EvalResults.Vig_Fitting(YPtr))
            YPtr += 1
        Next Entry
        Log.Log(Stopper.Stamp("Vignette - normalizing"))

        'Correct the vignette
        Dim CorrectedValues As Integer = -1
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                CorrectedValues = ImageProcessing.CorrectVignette(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data, UsedVignette_Correction)
            Case AstroNET.Statistics.eDataType.UInt32
                CorrectedValues = ImageProcessing.CorrectVignette(AIS.DB.LastFile_Data.DataProcessor_UInt32.ImageData(0).Data, UsedVignette_Correction)
        End Select
        Log.Log(Stopper.Stamp("Vignette - correction (" & CorrectedValues.ValRegIndep & " values corrected"))

        Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
        Log.Log(StatisticsReport)
        Idle()

    End Sub

    Private Sub tsmiAnalysisVignette_CalcParam_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisVignette_CalcParam.Click

        Dim Visualization As New cZEDGraphForm

        'Calculate vignette if not present
        Dim CalcRequired As Boolean = True
        If IsNothing(AIS.DB.LastFile_EvalResults.Vig_RawData) = False Then
            If AIS.DB.LastFile_EvalResults.Vig_RawData.Count > 0 Then
                CalcRequired = False
            End If
        End If
        If CalcRequired = True Then CalcVignette()

        Dim Stopper As New cStopper : Stopper.Start()
        Running()

        'Get only relevant data
        AIS.DB.LastFile_EvalResults.Vig_BinUsedData = New Dictionary(Of Double, Double)
        For Each Distance As Double In AIS.DB.LastFile_EvalResults.Vig_RawData.Keys
            If Distance >= AIS.Config.VigStartDistance And Distance <= AIS.Config.VigStopDistance Then
                AIS.DB.LastFile_EvalResults.Vig_BinUsedData.Add(Distance, AIS.DB.LastFile_EvalResults.Vig_RawData(Distance))
            End If
        Next Distance
        Dim Min As Double = Double.NaN
        Dim Max As Double = Double.NaN
        AIS.DB.LastFile_EvalResults.Vig_BinUsedData = AIS.DB.LastFile_EvalResults.Vig_BinUsedData.SortDictionary(False, Min, Max)

        'Bin if required
        If AIS.Config.VigCalcBins > 0 Then
            'Build a statistics class for each X and Y bin
            Dim VigBin_X(AIS.Config.VigCalcBins - 1) As Ato.cSingleValueStatistics
            Dim VigBin_Y(AIS.Config.VigCalcBins - 1) As Ato.cSingleValueStatistics
            For InitIdx As Integer = 0 To VigBin_X.GetUpperBound(0)
                VigBin_X(InitIdx) = New Ato.cSingleValueStatistics(False)
                VigBin_Y(InitIdx) = New Ato.cSingleValueStatistics(False)
            Next InitIdx
            'Sweep over all used dictionary entries, calculate bin and add value for X and Y value
            Dim Range As Double = Max - Min
            For Each Distance As Double In AIS.DB.LastFile_EvalResults.Vig_RawData.Keys
                Dim Bin As Double = ((Distance - Min) / Range) * (AIS.Config.VigCalcBins - 1)
                Dim BinInt As Integer = CInt(Math.Floor(Bin))
                VigBin_X(BinInt).AddValue(Distance)
                VigBin_Y(BinInt).AddValue(AIS.DB.LastFile_EvalResults.Vig_RawData(Distance))
            Next Distance
            'Use the mean values for each bin in X and Y direction as new support point
            AIS.DB.LastFile_EvalResults.Vig_BinUsedData.Clear()
            For InitIdx As Integer = 0 To VigBin_X.GetUpperBound(0)
                If VigBin_X(InitIdx).ValueCount > 0 Then
                    AIS.DB.LastFile_EvalResults.Vig_BinUsedData.Add(VigBin_X(InitIdx).Mean, VigBin_Y(InitIdx).Mean)
                End If
            Next InitIdx
            Visualization.PlotData("Vignette bin", AIS.DB.LastFile_EvalResults.Vig_BinUsedData, Color.Orange)
        End If

        Log.Log(Stopper.Stamp("Vignette - getting used data"))
        Log.Log("Vignette correction calculation has <" & AIS.DB.LastFile_EvalResults.Vig_BinUsedData.Count.ValRegIndep & "> entries")

        'Calculate the fitting
        If AIS.Config.VigPolyOrder = -1 And AIS.Config.VigCalcBins > 0 Then
            'Use the binned data
            Log.Log(" ... using (direct) binned data for fitting")
            AIS.DB.LastFile_EvalResults.Vig_Fitting = AIS.DB.LastFile_EvalResults.Vig_BinUsedData.Values.ToArray
        Else
            'Use the polynomial calculation
            Log.Log(" ... using polynomial calcualtion for fitting")
            Dim Polynomial() As Double = {}
            SignalProcessing.RegressPoly(AIS.DB.LastFile_EvalResults.Vig_BinUsedData, AIS.Config.VigPolyOrder, Polynomial)
            AIS.DB.LastFile_EvalResults.Vig_Fitting = SignalProcessing.ApplyPoly(AIS.DB.LastFile_EvalResults.Vig_BinUsedData.Keys.ToArray, Polynomial)
        End If

        Log.Log(Stopper.Stamp("Vignette - fitting"))

        Visualization.PlotData("Fitting", AIS.DB.LastFile_EvalResults.Vig_BinUsedData.Keys.ToArray, AIS.DB.LastFile_EvalResults.Vig_Fitting, Color.Green)
        Visualization.PlotData("Vignette raw data", AIS.DB.LastFile_EvalResults.Vig_RawData, Color.Red)

        Idle()

    End Sub

    Private Sub tsmiAnalysisPlot_Replot_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_Plot_Replot.Click
        Running()
        PlotStatistics(AIS.DB.LastFile_Name, AIS.DB.LastFile_Statistics)
        Idle()
    End Sub

    Private Sub tsmiAnalysisPixelMap_SaveFor_Click(sender As Object, e As EventArgs) Handles tsmiAnalysisPixelMap_SaveFor.Click
        Dim Val As Integer = CInt(InputBox("Save coordinates of pixel with a value >= ...", "Criteria", "65536"))
        Running()
        Dim CriteriaPixel As New List(Of String)
        With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
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
        Log.Log("Found " & CriteriaPixel.Count.ToString.Trim & " pixel")
        Idle()
    End Sub

    Private Sub MedianWithinNETToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MedianWithinNETToolStripMenuItem.Click

        Running()

        'Init new image with old one
        Dim NewImage(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(0), AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(1)) As UInt16
        AIS.DB.IPP.Copy(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data, NewImage)

        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
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
        AIS.DB.IPP.Copy(NewImage, AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data)
        Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
        Log.Log(StatisticsReport)
        If AIS.Config.AutoOpenStatGraph = True Then PlotStatistics(AIS.DB.LastFile_Name, AIS.DB.LastFile_Statistics)
        Idle()

    End Sub

    Private Sub tsb_Open_Click(sender As Object, e As EventArgs) Handles tsb_Open.Click
        tsmiFile_Open_Click(Nothing, Nothing)
    End Sub

    Private Sub tsb_Display_Click(sender As Object, e As EventArgs) Handles tsb_Display.Click
        Dim ImageDisplay As New frmImageDisplay
        ImageDisplay.FileToDisplay = AIS.DB.LastFile_Name
        ImageDisplay.Show()
        ImageDisplay.SingleStatCalc = AIS.DB.LastFile_Data
        ImageDisplay.StatToUsed = AIS.DB.LastFile_Statistics
        ImageDisplay.MyIPP = AIS.DB.IPP
        ImageDisplay.ImageFromData.CM_LowerEnd_Absolute = AIS.DB.LastFile_Statistics.MonoStatistics_Int.Min.Key
        ImageDisplay.ImageFromData.CM_UpperEnd_Absolute = AIS.DB.LastFile_Statistics.MonoStatistics_Int.Max.Key
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
        If Comp2ndFile.DataProcessor_UInt16.ImageData(0).NAXIS1 <> AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).NAXIS1 Then
            MsgBox("NAXIS1 missmatch")
            Exit Sub
        End If
        If Comp2ndFile.DataProcessor_UInt16.ImageData(0).NAXIS2 <> AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).NAXIS2 Then
            MsgBox("NAXIS2 missmatch")
            Exit Sub
        End If

        'Calculate difference data
        Dim UInt32One As UInt32 = 1
        Dim DiffCat As New Dictionary(Of Int32, UInt32)         'All found differences
        Dim PixelDiff As Int32 = 0                              'Diff of the current pixel
        Dim DiffPixelCount As UInt32 = 0                        'Number of different pixel
        Dim TotalPixel As Long = AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Length
        With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
            For Idx1 As Integer = 0 To .NAXIS1 - 1
                For Idx2 As Integer = 0 To .NAXIS2 - 1
                    PixelDiff = CInt(Comp2ndFile.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2)) - CInt(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2))
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
        Log.Log("Number of pixel different: " & DiffPixelCount.ValRegIndep)
        Log.Log("Number of pixel identical: " & (TotalPixel - DiffPixelCount).ValRegIndep)
        Log.Log("Different pixel levels: " & DiffCat.Count.ValRegIndep)

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
            Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, sfdMain.FileName)
            workbook.SaveAs(FileToGenerate)
            Process.Start(FileToGenerate)

        End Using

    End Sub

    Private Sub tsmiAnalysis_RawFITSHeader_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_RawFITSHeader.Click
        If System.IO.File.Exists(AIS.DB.LastFile_Name) Then
            Dim FileIn As IO.FileStream = System.IO.File.OpenRead(AIS.DB.LastFile_Name)
            Dim Lines As New List(Of String)
            Dim ByteBuffer(cFITSReader.HeaderElementLength - 1) As Byte
            For LineIdx As Integer = 1 To 100
                FileIn.Read(ByteBuffer, 0, ByteBuffer.Length)
                Lines.Add(System.Text.Encoding.UTF8.GetString(ByteBuffer))
            Next LineIdx
            Dim HeaderAsIs As New frmHeaderAsIs
            HeaderAsIs.Text = "Raw header of file <" & AIS.DB.LastFile_Name & ">"
            HeaderAsIs.tbHeader.Text = Join(Lines.ToArray, System.Environment.NewLine)
            HeaderAsIs.Show()
        End If
    End Sub

    Private Sub tsmiAnalysis_FloatAsIntError_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_FloatAsIntError.Click
        'Calculate if the data are "real" float or just int converted to float
        If AIS.DB.LastFile_Statistics.DataMode = AstroNET.Statistics.eDataMode.Float Then
            Dim ErrorEnergy As Double = 0.0
            Dim Samples As ULong = 0
            For Each Key As Single In AIS.DB.LastFile_Statistics.MonochromHistogram_Float32.Keys
                Dim AsInt32 As Int32 = Convert.ToInt32(Key)
                Samples += AIS.DB.LastFile_Statistics.MonochromHistogram_Float32(Key)
                ErrorEnergy += Math.Abs((AsInt32 - Key) * AIS.DB.LastFile_Statistics.MonochromHistogram_Float32(Key))
            Next Key
            ErrorEnergy /= Samples
            'Log statistics
            Log.Log("Float as int error: <" & ErrorEnergy.ValRegIndep & ">")
            Log.Log(New String("="c, 109))
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
        Dim ZoomStatCalc As New AstroNET.Statistics(AIS.DB.IPP)
        ZoomStatCalc.ResetAllProcessors()
        Dim Status_GetROI As cIntelIPP.IppStatus = AIS.DB.IPP.Copy(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data, ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, CInt(OV_X), CInt(0), CInt(EffArea_Width), CInt(EffArea_Height))
        'Run VB function (may be wrong)
        Dim Data_New(,) As UInt16 = AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data.GetROI(OV_X, W - 1, 0, H - OV_Y - 2)
        'Compare both ROI's
        Dim DiffCount As Long = ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data.FindDifferences(Data_New)

        AIS.DB.LastFile_FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.NAXIS1, EffArea_Width))
        AIS.DB.LastFile_FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.NAXIS2, EffArea_Height))
        sfdMain.Filter = "FITS 16-bit fixed|*.fits"
        If sfdMain.ShowDialog = DialogResult.OK Then
            Running()
            cFITSWriter.Write(sfdMain.FileName, ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, cFITSWriter.eBitPix.Int16, AIS.DB.LastFile_FITSHeader.GetCardsAsList)
            Idle()
        End If
    End Sub

    Private Sub CheckROICutoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckROICutoutToolStripMenuItem.Click

        Dim W As Integer = 100
        Dim H As Integer = 50
        Dim OV_X As Integer = 24
        Dim OV_Y As Integer = 34
        Dim EffArea_Width As Integer = W - OV_X
        Dim EffArea_Height As Integer = H - OV_Y
        'Run IPP function (correct)
        Dim ZoomStatCalc As New AstroNET.Statistics(AIS.DB.IPP)
        ZoomStatCalc.ResetAllProcessors()
        Dim Status_GetROI As cIntelIPP.IppStatus = AIS.DB.IPP.Copy(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data, ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, CInt(OV_X), CInt(0), CInt(EffArea_Width), CInt(EffArea_Height))
        'Run VB function (may be wrong)
        Dim Data_New(,) As UInt16 = AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data.GetROI(OV_X, W - 1, 0, H - OV_Y - 1)
        'Compare both ROI's
        Dim DiffCount As Long = ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data.FindDifferences(Data_New)

    End Sub

    Private Sub tsmiWorkflow_Runner_Click(sender As Object, e As EventArgs) Handles tsmiWorkflow_Runner.Click
        Dim Workflow As New frmWorkflow
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
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                'Search all criteria pixels
                With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
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
        Log.Log("Fixed " & FixedPixelCount.ValRegIndep & " pixel changed")
        Idle()

    End Sub

    Private Sub FixRADECErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FixRADECErrorToolStripMenuItem.Click
        'Fix the error that RA_NOM which comes first is also DEC_NOM
        Dim DirScanner As New Ato.RecursiveDirScanner("\\192.168.100.10\astro\2021_02_13 (SH2-155)")
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
            cOpenCvSharp.RelToMedian(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data, KSize)
            Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
            Log.Log(StatisticsReport)
            If AIS.Config.AutoOpenStatGraph = True Then PlotStatistics(AIS.DB.LastFile_Name, AIS.DB.LastFile_Statistics)
        End If
    End Sub

    Private Function GetBytes_BitPix16(ByVal Value As Int16) As Byte()
        Dim RetVal As Byte() = BitConverter.GetBytes(Value)
        Return New Byte() {RetVal(1), RetVal(0)}
    End Function

    Private Sub tsmiTest_AstrometryQuery_Click(sender As Object, e As EventArgs) Handles tsmiTest_AstrometryQuery.Click
        'https://nova.astrometry.net/api/jobs/4720338/info/
    End Sub

    Private Sub tsmiProcessing_LSBMSB_Click(sender As Object, e As EventArgs) Handles tsmiProcessing_LSBMSB.Click

        Running()
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                AIS.DB.IPP.SwapBytes(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data)
                Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
                Log.Log(StatisticsReport)
                If AIS.Config.AutoOpenStatGraph = True Then PlotStatistics(AIS.DB.LastFile_Name, AIS.DB.LastFile_Statistics)
            Case Else

        End Select
        Log.Log("Swap done.")
        Idle()

    End Sub

    Private Sub tsmiProcessing_Specials_NINAFix_Click(sender As Object, e As EventArgs) Handles tsmiProcessing_Specials_NINAFix.Click

        Running()

        'Create LUT

        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
                    For Idx1 As Integer = 0 To .NAXIS1 - 1
                        For Idx2 As Integer = 0 To .NAXIS2 - 1
                            .Data(Idx1, Idx2) = cBitByte.BitsReverse(.Data(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                End With
                Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(AIS.DB.LastFile_Data, True, True, AIS.Config.BayerPatternNames, AIS.DB.LastFile_Statistics)
                Log.Log(StatisticsReport)
                If AIS.Config.AutoOpenStatGraph = True Then PlotStatistics(AIS.DB.LastFile_Name, AIS.DB.LastFile_Statistics)
            Case Else

        End Select
        Log.Log("Swap done.")
        Idle()

    End Sub

    Private Sub tsmiAnalysis_XvsYPlot_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_XvsYPlot.Click
        Dim XvsYPlot As New frmXvsYPlot
        XvsYPlot.Show()
    End Sub

    Private Sub frmTest_HistoInteractive_Click(sender As Object, e As EventArgs) Handles frmTest_HistoInteractive.Click
        Dim X As New frmHistogram
        X.Show()
    End Sub

    Private Sub tsmiTestCode_StreamDeck_Click(sender As Object, e As EventArgs) Handles tsmiTestCode_StreamDeck.Click

        'Test code to set Stream Deck keys

        'https://github.com/OpenMacroBoard/StreamDeckSharp

        If IsNothing(MyStreamDeck) = True Then
            MyStreamDeck = StreamDeckSharp.StreamDeck.OpenDevice
            AddHandler MyStreamDeck.KeyStateChanged, AddressOf StreamDeckHandler
        End If
        Dim KeyToAdd As OpenMacroBoard.SDK.KeyBitmap = Nothing '= OpenMacroBoard.SDK.KeyBitmap.Create.FromRgb(255, 0, 0)
        MyStreamDeck.SetBrightness(100)
        MyStreamDeck.SetKeyBitmap(0, KeyToAdd)
        MyStreamDeck.SetKeyBitmap(1, KeyToAdd)
        MyStreamDeck.SetKeyBitmap(2, KeyToAdd)
        MyStreamDeck.SetKeyBitmap(3, KeyToAdd)
        MyStreamDeck.SetKeyBitmap(4, KeyToAdd)
        MyStreamDeck.SetKeyBitmap(5, KeyToAdd)

    End Sub

    Private Function GetUniColorKey(ByVal Value As Color) As Byte()
        Dim RawByteSize As Integer = 72 * 72 * 3
        Dim RawBitmapData(RawByteSize - 1) As Byte
        For Idx As Integer = 0 To RawBitmapData.GetUpperBound(0) Step 3
            RawBitmapData(Idx + 2) = Value.R
            RawBitmapData(Idx + 1) = Value.G
            RawBitmapData(Idx) = Value.B
        Next Idx
        Return RawBitmapData
    End Function

    Private Sub StreamDeckHandler(sender As Object, e As OpenMacroBoard.SDK.KeyEventArgs)
        MsgBox("Press <" & e.Key.ToString.Trim & "> " & CStr(IIf(e.IsDown = True, "DOWN", "UP")))
    End Sub

    Private Sub tsmiTools_TestFile_Click(sender As Object, e As EventArgs) Handles tsmiTools_TestFile.Click
        Dim TestFileGenerator As New frmTestFileGenerator
        TestFileGenerator.Show()
    End Sub

    Private Sub tsmiProcessing_Stack_Click(sender As Object, e As EventArgs) Handles tsmiProcessing_Stack.Click
        Dim Stacking As New frmStacking
        Stacking.Show()
    End Sub

    Private Sub tssmRadCollimation_Click(sender As Object, e As EventArgs) Handles tssmRadCollimation.Click

        Dim ColForm As New frmCollimation
        If MsgBox("Bad Signal", vbYesNo) = MsgBoxResult.Yes Then
            LoadFile("C:\Users\albus\Dropbox\Astro\!WORK\TestImages\Blau_von_Collimation_Bad.fit", ColForm.Collimation.ImageData)
            ColForm.tbX.Text = "152"
            ColForm.tbY.Text = "165"
            ColForm.tbRadius.Text = "230"
            ColForm.Show()
        Else
            LoadFile("C:\Users\albus\Dropbox\Astro\!WORK\TestImages\Blau_von_Collimation_OK.fit", ColForm.Collimation.ImageData)
            ColForm.tbX.Text = "155"
            ColForm.tbY.Text = "148"
            ColForm.tbRadius.Text = "230"
            ColForm.Show()
        End If

    End Sub

    Private Sub MultifileActionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultifileActionToolStripMenuItem.Click
        Dim X As New frmMultiFileAction
        X.Show()
    End Sub

    Private Sub tsmiHotPixelFilter_Click(sender As Object, e As EventArgs) Handles tsmiHotPixelFilter.Click

        'Tries to fix the special hot-pixel on the QHY600

        Dim AroundStat As New Ato.cSingleValueStatistics
        Dim FixedPixelCount As UInt32 = 0
        Dim Limit As UShort = 65534
        Dim SetTo As UShort = CUShort(InputBox("Set to", "0"))
        Running()
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
                    For Idx1 As Integer = 1 To .NAXIS1 - 2
                        For Idx2 As Integer = 1 To .NAXIS2 - 2
                            If .Data(Idx1, Idx2) = Limit Then
                                AroundStat.Clear()
                                For DeltaX As Integer = -1 To 1
                                    For DeltaY As Integer = -1 To 1
                                        If (DeltaX <> 0) And (DeltaY <> 0) Then
                                            AroundStat.AddValue(.Data(Idx1 + DeltaX, Idx2 + DeltaY))
                                        End If
                                    Next DeltaY
                                Next DeltaX
                                If AroundStat.Mean < Limit / 2 Then
                                    FixedPixelCount += UInt32One
                                    .Data(Idx1, Idx2) = SetTo
                                End If
                            End If
                        Next Idx2
                    Next Idx1
                End With
                Log.Log("Fixed " & FixedPixelCount.ValRegIndep & " pixel changed")
            Case Else
                MsgBox("Data type not supported!")
        End Select
        Idle()

    End Sub

    Private Sub tsmiProc_Bin2Median_Click(sender As Object, e As EventArgs) Handles tsmiProc_Bin2Median.Click

        Running()
        AIS.DB.LastFile_Data.DataProcessor_UInt16.LoadImageData(ImageProcessing.Binning.Bin2_Inner_UInt16(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data))
        Log.Log("Bin2 with median executed.")
        Idle()

    End Sub

    Private Sub tsmiProc_Bin2MaxOut_Click(sender As Object, e As EventArgs) Handles tsmiProc_Bin2MaxOut.Click

        Dim StepName As String = "BIN2, max removal"
        Dim Stopper As Stopwatch = Running(StepName)
        Dim Bin2_2(,) As UInt16 = ImageProcessing.Binning.Bin2_Inner_UInt16(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data)
        Idle(Stopper, StepName)

        'Load new image
        AIS.DB.LastFile_Data.DataProcessor_UInt16.LoadImageData(Bin2_2)

    End Sub

    Private Function TestMatrix(ByVal Rows As Integer, ByVal Cols As Integer) As UInt16(,)
        Dim RetVal(Rows - 1, Cols - 1) As UInt16
        Dim RndGen As New Random
        For Idx1 As Integer = 0 To Rows - 1
            For Idx2 As Integer = 0 To Cols - 1
                RetVal(Idx1, Idx2) = CUShort(RndGen.Next(UInt16.MinValue, UInt16.MaxValue + 1))
            Next Idx2
        Next Idx1
        Return RetVal
    End Function

    '''<summary>Bin2 with removal of the maximum value, additional statistics.</summary>
    Private Sub RunBin2MaxOut()

        'We run a 2x2 bin and remove the maximum value
        Dim Stopper As New Stopwatch
        Running()
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                'Calculate the new image and remember the maximum spike value
                Dim MaxSpike As Double = Double.NaN
                Dim NewImage(,) As UInt16 = Nothing
                Stopper.Start()
                Bin2MaxOut(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0), MaxSpike, NewImage)
                Stopper.Stop()
                'Get the spike value distribution
                Dim SpikeStat As Dictionary(Of UInt16, UInt32) = Bin2MaxOut(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0), MaxSpike, NewImage)
                'Load new image
                AIS.DB.LastFile_Data.DataProcessor_UInt16.LoadImageData(NewImage)
                'Finish
                PlotStatistics("Spike Statistics", SpikeStat, UInt16.MaxValue / MaxSpike)
                Log.Log("Bin2 with max removal executed.", Stopper.Elapsed)
            Case Else
                MsgBox("Data type not supported!")
        End Select
        Idle()

    End Sub

    Private Function Bin2MaxOut(ByRef OriginalData As cStatMultiThread_UInt16.sImgData_UInt16, ByRef MaxSpikeToUse As Double, ByRef BinnedData(,) As UInt16) As Dictionary(Of UInt16, UInt32)
        Dim RetVal As New Dictionary(Of UInt16, UInt32)
        Dim UInt16_one As UInt16 = 1
        Dim MaxSpikeFound As Double = Double.MinValue
        With OriginalData
            ReDim BinnedData((.NAXIS1 \ 2) - 1, (.NAXIS2 \ 2) - 1)
            Dim NewX As Integer = 0
            Dim AllX As New List(Of Integer)
            For OrigX As Integer = 0 To .NAXIS1 - 1 Step 2
                AllX.Add(OrigX)
            Next OrigX
            For Each OrigX As Integer In AllX
                Dim NewY As Integer = 0
                For OrigY As Integer = 0 To .NAXIS2 - 1 Step 2
                    Dim BinPixel As New List(Of UInt32)({ .Data(OrigX, OrigY), .Data(OrigX + 1, OrigY), .Data(OrigX, OrigY + 1), .Data(OrigX + 1, OrigY + 1)})
                    BinPixel.Sort()
                    Dim PixelSum As UInt32 = (BinPixel(0) + BinPixel(1) + BinPixel(2))
                    Dim Spike As Double = BinPixel(3) / (PixelSum / 3)
                    If Double.IsNaN(MaxSpikeToUse) = True Then
                        'Remember the spike data
                        If Spike > MaxSpikeFound Then MaxSpikeFound = Spike
                    Else
                        'Calculate the statistics
                        Dim StatBin As UInt16 = CType((Spike / MaxSpikeToUse) * UInt16.MaxValue, UInt16)
                        If RetVal.ContainsKey(StatBin) = False Then
                            RetVal.Add(StatBin, 1)
                        Else
                            RetVal(StatBin) = RetVal(StatBin) + UInt16_one
                        End If
                        If Spike = MaxSpikeToUse Then
                            Dim X As Integer = 1
                        End If
                    End If
                    BinnedData(NewX, NewY) = CType(PixelSum \ 3, UInt16)
                    If BinnedData(NewX, NewY) = 0 Then MsgBox("!!!!")
                    NewY += 1
                Next OrigY
                NewX += 1
            Next OrigX
        End With
        If Double.IsNaN(MaxSpikeToUse) = True Then MaxSpikeToUse = MaxSpikeFound
        Return RetVal.SortDictionary
    End Function

    Private Sub tsmiAnalysis_FindStars_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_FindStars.Click

        'We run a 2x2 bin and remove the maximum value
        Dim Stopper As New Stopwatch : Stopper.Start()
        Running()
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                Dim DetectionImage(,) As UInt16 = AIS.DB.IPP.Copy(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data)
                Dim BlurOK As String = cOpenCvSharp.MedianBlur(DetectionImage, 5)
                If String.IsNullOrEmpty(BlurOK) Then
                    AIS.DB.Stars = New List(Of cDB.sSpecialPoint)
                    Dim MaxVal As UInt16 = UInt16.MinValue
                    Dim MaxPos1 As Integer = -1
                    Dim MaxPos2 As Integer = -1
                    Do
                        AIS.DB.IPP.MaxIndx(DetectionImage, MaxVal, MaxPos1, MaxPos2)      'find peak
                        Dim Y As UInt16 = DetectionImage(MaxPos1, MaxPos2)
                        Dim MeanEnery As Double = SetROIToValue(DetectionImage, MaxPos1, MaxPos2, 5, UInt16.MinValue)
                        AIS.DB.Stars.Add(New cDB.sSpecialPoint(MaxPos1, MaxPos2, MaxVal, MeanEnery))
                    Loop Until AIS.DB.Stars.Count = 250
                End If

                'Calculate the new image and remember the maximum spike value
                'Dim MaxSpike As Double = Double.NaN
                'Dim NewImage(,) As UInt16 = Nothing
                'Bin2MaxOut(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0), MaxSpike, NewImage)
                ''Get the spike value distribution
                'Dim SpikeStat As Dictionary(Of UInt16, UInt32) = Bin2MaxOut(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0), MaxSpike, NewImage)
                ''Load new image
                'AIS.DB.LastFile_Data.DataProcessor_UInt16.LoadImageData(NewImage)
                ''Finish
                'Stopper.Stop()
                'PlotStatistics("Spike Statistics", SpikeStat, UInt16.MaxValue / MaxSpike)
                'Log.Log("Bin2 with max removal executed.", Stopper.Elapsed)
            Case Else
                MsgBox("Data type not supported!")
        End Select
        Idle()

    End Sub

    '''<summary>Set a certain ROI in the data to a defined number.</summary>
    '''<param name="Data">Data to change.</param>
    '''<param name="Center1">Center in 1st axis.</param>
    '''<param name="Center2">Center in 2nd axis.</param>
    '''<param name="ROISize">ROI - delta to left, right, up and down in pixel.</param>
    '''<param name="ValueToSet">Value to set in the ROI.</param>
    '''<returns>Mean energy per pixel in the ROI.</returns>
    Private Function SetROIToValue(ByRef Data(,) As UInt16, ByVal Center1 As Integer, ByVal Center2 As Integer, ByVal ROISize As Integer, ByVal ValueToSet As UInt16) As Double
        Dim Center1_Left As Integer = Center1 - ROISize : If Center1_Left < 0 Then Center1_Left = 0
        Dim Center1_Right As Integer = Center1 + ROISize : If Center1_Right > Data.GetUpperBound(0) Then Center1_Right = Data.GetUpperBound(0)
        Dim Center2_Left As Integer = Center2 - ROISize : If Center2_Left < 0 Then Center2_Left = 0
        Dim Center2_Right As Integer = Center2 + ROISize : If Center2_Right > Data.GetUpperBound(1) Then Center2_Right = Data.GetUpperBound(1)
        Dim RetVal As UInt64 = 0
        Dim Pixel As Integer = 0
        For Idx1 As Integer = Center1_Left To Center1_Right
            For Idx2 As Integer = Center2_Left To Center2_Right
                Pixel += 1
                RetVal += Data(Idx1, Idx2) : Data(Idx1, Idx2) = ValueToSet
            Next Idx2
        Next Idx1
        Return RetVal / Pixel
    End Function

    Private Sub tsmiProc_Bin2OpenCV_Click(sender As Object, e As EventArgs) Handles tsmiProc_Bin2OpenCV.Click

        Running()
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                With AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0)
                    Dim NewData(,) As UInt16 = {}
                    cOpenCvSharp.pyrDown(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data, NewData)
                    AIS.DB.LastFile_Data.DataProcessor_UInt16.LoadImageData(NewData)
                End With
                Log.Log("Bin2 with pyrDown executed.")
            Case Else
                MsgBox("Data type not supported!")
        End Select
        Idle()

    End Sub

    Private Sub tsmiTest_ippiXCorr_Click(sender As Object, e As EventArgs) Handles tsmiTest_ippiXCorr.Click
        Dim MyForm As New frmXCorr
        MyForm.Show()
    End Sub

    Private Sub SigmaClipToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiTest_SigmaClip.Click
        Dim TestClass As New AstroDSP.cSigmaClipped
        Dim Samples(999) As UInt16
        Dim RndGen As New Random
        For Idx As Integer = 0 To Samples.GetUpperBound(0)
            Samples(Idx) = CType(RndGen.Next(1000, 2000), UInt16)
        Next Idx
        Samples(4) = 20000
        Samples(66) = 0
        For LoopIdx As Integer = 1 To 10000
            Dim PixelRemoved As UInt16 = 0
            Dim NewMean As Double = TestClass.SigmaClipped_mean(Samples, PixelRemoved)
        Next LoopIdx
        MsgBox("OK")
    End Sub

    Private Sub tsmiFile_ConvertTo16BitFITS_Click(sender As Object, e As EventArgs) Handles tsmiFile_ConvertTo16BitFITS.Click

        'Select file
        ofdMain.Filter = "FIT(s) files (FIT/FITS/FTS)|*.FIT;*.FITS;*.FTS"
        If ofdMain.ShowDialog <> DialogResult.OK Then Exit Sub

        'Load data
        Dim FileContainer As AstroNET.Statistics = Nothing
        LoadFileDataOnly(ofdMain.FileNames(0), FileContainer)

        Dim X As String = FileContainer.Dimensions
        Select Case FileContainer.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                'Is already UInt16 - do nothing
                MsgBox("File is already 16-bit fixed point!")
            Case AstroNET.Statistics.eDataType.UInt32
                'Downscale with full range
                If sfdMain.ShowDialog <> DialogResult.OK Then Exit Sub
                Dim Data_Min As UInt32 = UInt32.MaxValue
                Dim Data_Max As UInt32 = UInt32.MinValue
                Dim NewRange_Min As UInt16 = UInt16.MinValue
                Dim NewRange_Max As UInt16 = UInt16.MaxValue
                AIS.DB.IPP.MinMax(FileContainer.DataProcessor_UInt32.ImageData(0).Data, Data_Min, Data_Max)
                Dim A As Double = (NewRange_Min - NewRange_Max) / (Data_Min - Data_Max)
                Dim B As Double = NewRange_Max - (A * Data_Max)
                Dim NewData(,) As UInt16 = FileContainer.DataProcessor_UInt32.ImageData(0).Data.ToUInt16(A, B)
                cFITSWriter.Write(sfdMain.FileName, NewData, cFITSWriter.eBitPix.Int16)
            Case AstroNET.Statistics.eDataType.Float32
                'Downscale with full range
                If sfdMain.ShowDialog <> DialogResult.OK Then Exit Sub
                Dim Data_Min As Single = Single.NaN
                Dim Data_Max As Single = Single.NaN
                Dim NewRange_Min As Double = UInt16.MinValue
                Dim NewRange_Max As Double = UInt16.MaxValue
                AIS.DB.IPP.MinMax(FileContainer.DataProcessor_Float32.ImageData(0).Data, Data_Min, Data_Max)
                Dim A As Double = (NewRange_Min - NewRange_Max) / (Data_Min - Data_Max)
                Dim B As Double = NewRange_Max - (A * Data_Max)
                Dim NewData(,) As UInt16 = FileContainer.DataProcessor_Float32.ImageData(0).Data.ToUInt16(A, B)
                cFITSWriter.Write(sfdMain.FileName, NewData, cFITSWriter.eBitPix.Int16)
        End Select

    End Sub

    Private Sub tsmiTest_FITSReadSpeed_Click(sender As Object, e As EventArgs) Handles tsmiTest_FITSReadSpeed.Click

        Dim FileName As String = "C:\GIT\AstroImageStatistics\AstroImageStatistics\bin\x64\Debug\AsImStatTestImage.fits"
        'Dim FileName As String = "\\192.168.100.10\astro_misc\TestData\TestImage_UInt16_20000_19000.fits"
        Dim ROI As New Drawing.Rectangle(1, 1, 20000 - 2, 19000 - 2)
        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)
        Dim Log As New List(Of String)
        'cFITSReader.IPPPath = AIS.DB.IPP.IPPPath
        Dim Stopper As New Stopwatch
        Dim Data(,) As UInt16 = {}

        'Load reference data
        FITSReader.UseExperimentalModes = False
        Stopper.Reset() : Stopper.Start()
        Dim RefData(,) As UInt16 = FITSReader.ReadInUInt16(FileName, True, True)
        Stopper.Stop()
        Log.Add("Load def data (IPP, direct, all): " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")

        'Speed test
        Stopper.Reset() : Stopper.Start()
        'Data = FITSReader.ReadDataContentUInt16_NEW(FileName, 2880, True, 0, 20000, 0, 19000, True)
        Stopper.Stop()
        Log.Add("Load with IPP - max speed and low memory mode: " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")

        Dim Differences As Long = RefData.FindDifferences(Data)
        Log.Add("Differences: " & Differences.ValRegIndep)

        'Load with IPP - full
        'Data = {} : FITSReader.UseExperimentalModes = True
        'Stopper.Reset() : Stopper.Start()
        'Data = FITSReader.ReadInUInt16(FileName, True, True)
        'Stopper.Stop()
        'Log.Add("Load with IPP - full in max speed mode: " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")



        'Load without IPP - full parallel
        'Data = {} : FITSReader.UseExperimentalModes = True
        'Stopper.Reset() : Stopper.Start()
        'Data = FITSReader.ReadInUInt16(FileName, False, False)
        'Stopper.Stop()
        'Log.Add("Load without IPP - full parallel: " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")

        ''Load without IPP - full non-parallel
        'Data = {} : FITSReader.UseExperimentalModes = False
        'Stopper.Reset() : Stopper.Start()
        'Data = FITSReader.ReadInUInt16(FileName, False, False)
        'Stopper.Stop()
        'Log.Add("Load without IPP - full non-parallel: " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")

        ''Load with IPP - ROI
        'Data = {} : FITSReader.UseExperimentalModes = True
        'Stopper.Reset() : Stopper.Start()
        'Data = FITSReader.ReadInUInt16(FileName, True, ROI, True)
        'Stopper.Stop()
        'Log.Add("Load with IPP - ROI: " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")

        ''Load without IPP - ROI parallel
        'Data = {} : FITSReader.UseExperimentalModes = True
        'Stopper.Reset() : Stopper.Start()
        'Data = FITSReader.ReadInUInt16(FileName, False, ROI, False)
        'Stopper.Stop()
        'Log.Add("Load without IPP - ROI parallel: " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")

        ''Load without IPP - ROI non-parallel
        'Data = {} : FITSReader.UseExperimentalModes = False
        'Stopper.Reset() : Stopper.Start()
        'Data = FITSReader.ReadInUInt16(FileName, False, ROI, False)
        'Stopper.Stop()
        'Log.Add("Load without IPP - ROI non-parallel: " & Stopper.ElapsedMilliseconds.ValRegIndep & " ms")

        MsgBox(Join(Log.ToArray, System.Environment.NewLine))

    End Sub

    Private Sub DataGridViewDataSourceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataGridViewDataSourceToolStripMenuItem.Click
        Dim X As New frmTestForm
        X.Show()
    End Sub

    Private Sub tsmiTest_RAWReader_NEF_Click(sender As Object, e As EventArgs) Handles tsmiTest_RAWReader_NEF.Click
        With ofdMain
            .Filter = "Nikon RAW file (*.NEF)|*.NEF"
            .Multiselect = False
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With
        Dim Reader As New cNEFReader
        Dim ReturnArgument As String = Reader.Read(ofdMain.FileName)
        MsgBox(ReturnArgument)
    End Sub

    Private Sub tsmiTest_RAWReader_LibRawDLL_Click(sender As Object, e As EventArgs) Handles tsmiTest_RAWReader_LibRawDLL.Click
        'https://github.com/laheller/SharpLibraw/blob/master/LibRAWDemo/Program.cs
        With ofdMain
            .Filter = "Nikon RAW file (*.NEF)|*.NEF"
            .Multiselect = False
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With
        Dim DLLPtr As IntPtr = cLibRaw.libraw_init(cLibRaw.LibRaw_init_flags.LIBRAW_OPTIONS_NONE)
        MsgBox(System.Runtime.InteropServices.Marshal.PtrToStringAnsi(cLibRaw.libraw_version))
        Dim AllCam As List(Of String) = cLibRaw.GetAllSupportedCameras
        Dim OpenError As cLibRaw.LibRaw_errors = cLibRaw.libraw_open_file(DLLPtr, ofdMain.FileName)
        MsgBox(Join(cLibRaw.DisplayCommonInfo(DLLPtr).ToArray, System.Environment.NewLine))
        Dim param As cLibRaw.libraw_iparams_t = System.Runtime.InteropServices.Marshal.PtrToStructure(Of cLibRaw.libraw_iparams_t)(cLibRaw.libraw_get_iparams(DLLPtr))
        Dim paramEx As cLibRaw.libraw_imgother_t = System.Runtime.InteropServices.Marshal.PtrToStructure(Of cLibRaw.libraw_imgother_t)(cLibRaw.libraw_get_imgother(DLLPtr))
        cLibRaw.libraw_close(DLLPtr)
    End Sub

    Private Sub tsmiTest_RAWReader_GrayPNGToFits_Click(sender As Object, e As EventArgs) Handles tsmiTest_RAWReader_GrayPNGToFits.Click

        With ofdMain
            .Filter = "PNG files (*.png)|*.png"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        Dim PNG As Bitmap = Nothing
        Using FileIn As New System.IO.FileStream(ofdMain.FileName, IO.FileMode.Open)
            PNG = New Bitmap(Image.FromStream(FileIn))
        End Using

        'Create converted data
        Dim BitPix As Integer = cFITSWriter.eBitPix.Int16
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

        With sfdMain
            .Filter = "FITS file (*.fits)|*.FITS"
            If .ShowDialog <> DialogResult.OK Then Exit Sub
        End With

        Dim BaseOut As New System.IO.StreamWriter(sfdMain.FileName)
        Dim BytesOut As New System.IO.BinaryWriter(BaseOut.BaseStream)

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

    Private Sub tsmiFile_QHY600Preview_Click(sender As Object, e As EventArgs) Handles tsmiFile_QHY600Preview.Click

        Dim InputFileName As String = "\\192.168.100.10\dsc\2024_02_19 - Test full area\QHY600_H_alpha_300_000_050_001_010_Photographic.fits"
        Dim DataStartPosition As Integer = -1
        Dim Container As New AstroNET.Statistics(AIS.DB.IPP)
        Dim Statistics As New AstroNET.Statistics.sStatistics

        'Load the raw data
        Processing.LoadFITSFile(InputFileName, AIS.DB.IPP, AIS.Config.ForceDirect, AIS.DB.LastFile_FITSHeader, Container, DataStartPosition)

        With Container.DataProcessor_UInt16

            'Cut dark area
            ' - Original data  : 9600 x 6422 mit Overscan
            ' - Image as stored: 9576 x 6388
            Dim ROI As New Rectangle(9600 - 9576, 0, 9576, 6388)
            .LoadImageData(.ImageData(0).Data.GetROI(ROI))

            'Median filter
            .LoadImageData(ImageProcessing.Binning.Bin2_Inner_UInt16(Container.DataProcessor_UInt16.ImageData(0).Data))

            'Calculate statistics
            Dim StatisticsReport As List(Of String) = Processing.CalculateStatistics(Container, True, False, Nothing, Statistics)

            'Save data
            Dim PreviewFileName As String = "C:\!Work\Preview.fits"
            cFITSWriter.Write(PreviewFileName, .ImageData(0).Data, cFITSWriter.eBitPix.Int16)
            'FileIO.PNG16Bit(.ImageData(0).Data, PreviewFileName)

        End With

        MsgBox("OK!")


    End Sub

    Private Sub tsmiProcessing_AlignTIFFFiles_Click(sender As Object, e As EventArgs) Handles tsmiProcessing_AlignTIFFFiles.Click
        Dim NewForm As New frmAlignTIFFFiles
        NewForm.Show()
    End Sub

    Private Sub tsmiTest_Shannon_Click(sender As Object, e As EventArgs) Handles tsmiTest_Shannon.Click
        Dim TestFile As String = "C:\Users\albus\OneDrive\Transfer_Kevin_Morefield\QHY600_L_300_025_020_003_060_ExtendFullwell.fits"
        LoadFile(TestFile)
        Dim Dic As Dictionary(Of Long, ULong) = AIS.DB.LastFile_Statistics.MonochromHistogram_Int
        Dim Gene As New cShanFano(Of Long)
        Gene.GenCodeBook(Dic)
    End Sub

    Private Sub tsmiTest_FlatsEqualizer_Click(sender As Object, e As EventArgs) Handles tsmiTest_FlatsEqualizer.Click

        Dim AllFiles As New List(Of String) : AllFiles.AddRange(System.IO.Directory.GetFiles("\\192.168.100.10\dsc\2024_11_13\Flats\06_28_12", "Flats_000*.fits"))

        Dim FileToGenerate As String = IO.Path.Combine(AIS.DB.MyPath, "PolyFactorSummary.xlsx")
        Dim FlatEqualizer As New cFlatEqualizer
        FlatEqualizer.FlatFile1 = AllFiles.First
        FlatEqualizer.PowerReduction = 5
        FlatEqualizer.Config_CalcHeatMap = False
        FlatEqualizer.Config_StoreXLS = False
        Dim EQFlats As New Dictionary(Of String, Double())
        Using WB As New ClosedXML.Excel.XLWorkbook
            Dim WS As ClosedXML.Excel.IXLWorksheet = WB.Worksheets.Add("Polyfactors")
            WS.Cell(1, 1).InsertData(New List(Of String)({"MatrixPoly_0", "MatrixPoly_1", "StatXYPoly_0", "StatXYPoly_1"}), True)
            Dim RowPtr As Integer = 1
            For Each FlatFile As String In AllFiles
                If FlatFile <> FlatEqualizer.FlatFile1 Then
                    RowPtr += 1
                    Log.Log(RowPtr.ValRegIndep & "/" & 20.ValRegIndep)
                    FlatEqualizer.FlatFile2 = FlatFile
                    FlatEqualizer.CalcPolyFactors()
                    WS.Cell(RowPtr, 1).Value = FlatEqualizer.MatrixPoly(0)
                    WS.Cell(RowPtr, 2).Value = FlatEqualizer.MatrixPoly(1)
                    WS.Cell(RowPtr, 3).Value = FlatEqualizer.StatXYPoly(0)
                    WS.Cell(RowPtr, 4).Value = FlatEqualizer.StatXYPoly(1)
                    EQFlats.Add(FlatEqualizer.FlatFile2, New Double() {FlatEqualizer.StatXYPoly(0), FlatEqualizer.StatXYPoly(1)})
                End If
                'If RowPtr = 5 Then Exit For                    'break for test purpose
            Next FlatFile
            WB.SaveAs(FileToGenerate)
        End Using
        Log.Log(" Combining ...")
        FlatEqualizer.CombineFlats(FlatEqualizer.FlatFile1, EQFlats)
        Log.Log(" DONE")
        Media.SystemSounds.Beep.Play()
    End Sub

    Private Sub tsmiAnalysis_Plot_RelPixDist_Click(sender As Object, e As EventArgs) Handles tsmiAnalysis_Plot_RelPixDist.Click

        'We take the image data and run a BIN2 with outer removal.
        'Then we generate a statistics on this.
        Dim Bin2Data As New AstroNET.Statistics(AIS.DB.IPP)
        Bin2Data.ResetAllProcessors()
        'Bin2Data.DataProcessor_UInt16.ImageData(0).Data = ImageProcessing.Binning.Bin2_Inner_UInt16(AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data)
        Bin2Data.DataProcessor_UInt16.ImageData(0).Data = AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data
        Dim Bin2Stat As AstroNET.Statistics.sStatistics = Bin2Data.ImageStatistics()

        'Now we order the histogram by the occurence of ADU values
        Dim ADUValStat As Dictionary(Of Long, ULong) = Bin2Stat.MonochromHistogram_Int.SortDictionaryByValue(True)

        Dim MaxTrace As New Dictionary(Of ULong, Long)
        Dim MinTrace As New Dictionary(Of ULong, Long)
        Dim ADUMax As Long = Long.MinValue
        Dim ADUMin As Long = Long.MaxValue
        Dim Ptr As Integer = 0
        Dim TotalSamplesConsidered As ULong = 0
        Dim SamplesInOriginal As Long = AIS.DB.LastFile_Data.DataProcessor_UInt16.ImageData(0).Data.LongLength

        For Each Item As KeyValuePair(Of Long, ULong) In ADUValStat
            TotalSamplesConsidered += Item.Value
            If Item.Key > ADUMax Then
                ADUMax = Item.Key
                MaxTrace.Add(TotalSamplesConsidered, ADUMax)
            End If
            If Item.Key < ADUMin Then
                ADUMin = Item.Key
                MinTrace.Add(TotalSamplesConsidered, ADUMin)
            End If
            Ptr += 1
        Next Item

        Dim Disp As New cZEDGraphForm
        Disp.Plotter.Clear()
        Disp.Plotter.PlotXvsY("Max trace", MaxTrace, New cZEDGraph.sGraphStyle(Color.Black, AIS.Config.PlotStyle, 1))
        Disp.Plotter.PlotXvsY("Min trace", MinTrace, New cZEDGraph.sGraphStyle(Color.Black, AIS.Config.PlotStyle, 1))
        Disp.Plotter.GridOnOff(True, True)
        Disp.Plotter.AutoScaleXAxis()
        Disp.Plotter.ManuallyScaleYAxisLin(0, 100)
        Disp.Plotter.ForceUpdate()
        'Set style of the window
        Disp.Plotter.SetCaptions(String.Empty, "Samples considered [%]", "Range of samples up to this point")
        Disp.Plotter.MaximizePlotArea()
        Disp.HostForm.Icon = Me.Icon

    End Sub

End Class
