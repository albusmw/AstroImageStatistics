Option Explicit On
Option Strict On

Public Class frmStat2Files

    '''<summary>The ZED graph control inside the form.</summary>
    Public zgcPlot As ZedGraph.ZedGraphControl = Nothing
    '''<summary>The ZED graph service (from file ZEDGraphService.vb).</summary>
    Public PlotService As cZEDGraph = Nothing

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        zgcPlot = New ZedGraph.ZedGraphControl : pZEDGraph.Controls.Add(zgcPlot) : zgcPlot.Dock = DockStyle.Fill
        PlotService = New cZEDGraph(zgcPlot) : PlotService.Clear()

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        Dim ULongOne As ULong = 1

        'We take File1 as X axis and File2 as Y axis

        Dim Reader1 As New cFlatEqualizer.cSingleFile(tbFile1.Text)
        Dim Reader2 As New cFlatEqualizer.cSingleFile(tbFile2.Text)

        'Initialize statistical bins for each possible UInt16 value
        Dim StatBins As New Dictionary(Of UInt16, Ato.cSingleValueStatistics)

        Dim File1Hist As New Dictionary(Of Long, ULong)
        Dim File2Hist As New Dictionary(Of Long, ULong)

        'Start reading from each file
        Reader1.StartDataRead()
        Reader2.StartDataRead()

        'Read all lines
        pbMain.Maximum = Reader1.FITSHeader.Width
        For LineIdx As Integer = 0 To Reader1.FITSHeader.Width - 1
            pbMain.Value = LineIdx : DE()
            Reader1.ReadLine()
            Reader2.ReadLine()
            'Use pixel value in file 1 to access the bin and add the value of pixel value in file 2
            For PixelPos As Integer = 0 To Reader1.Pixels.GetUpperBound(0)
                Dim ValX As UInt16 = Reader1.Pixels(PixelPos)
                Dim ValY As UInt16 = Reader2.Pixels(PixelPos)
                If StatBins.ContainsKey(ValX) = False Then StatBins.Add(ValX, New Ato.cSingleValueStatistics(False))
                StatBins(ValX).AddValue(ValY)
                If File1Hist.ContainsKey(ValX) = False Then File1Hist.Add(ValX, 0)
                File1Hist(ValX) = File1Hist(ValX) + ULongOne
                If File2Hist.ContainsKey(ValY) = False Then File2Hist.Add(ValY, 0)
                File2Hist(ValY) = File2Hist(ValY) + ULongOne
            Next PixelPos
        Next LineIdx
        pbMain.Value = 0 : DE()
        StatBins = StatBins.SortDictionary

        'Generate statistics for AXIS1
        Dim File1Stat As AstroNET.Statistics.cSingleChannelStatistics_Int = AstroNET.Statistics.CalcStatValuesFromHisto(File1Hist)
        Dim File2Stat As AstroNET.Statistics.cSingleChannelStatistics_Int = AstroNET.Statistics.CalcStatValuesFromHisto(File2Hist)

        'Calculate the X axis limit where to take the mean value for regression
        Dim LeftLimit As Double = File1Stat.GetPercentile(1)
        Dim RightLimit As Double = File1Stat.GetPercentile(95)

        'Prepare the vectors for the regression
        Dim RegX As New List(Of Double)
        Dim RegY As New List(Of Double)

        'Generate lines to plot and the regression data set
        Dim Plot_X As New List(Of Double)
        Dim Plot_Max As New List(Of Double)
        Dim Plot_Mean As New List(Of Double)
        Dim Plot_Min As New List(Of Double)
        Dim Plot_BinCount As New List(Of Double)
        For Each XVal As UInt16 In StatBins.Keys
            Plot_X.Add(XVal)
            Plot_Max.Add(StatBins(XVal).Maximum)
            Plot_Mean.Add(StatBins(XVal).Mean)
            Plot_Min.Add(StatBins(XVal).Minimum)
            Plot_BinCount.Add(StatBins(XVal).ValueCount)
            If (XVal >= LeftLimit) And (XVal <= RightLimit) Then
                RegX.Add(XVal)
                RegY.Add(StatBins(XVal).Mean)
            End If
        Next XVal

        'Calculate regression
        Dim Polynom() As Double = {}
        SignalProcessing.RegressPoly(RegX.ToArray, RegY.ToArray, 1, Polynom)

        'Plot the line
        PlotService.PlotXvsY("Max", Plot_X.ToArray, Plot_Max.ToArray, New cZEDGraph.sGraphStyle(Color.Red, cZEDGraph.eCurveMode.Dots))
        PlotService.PlotXvsY("Mean", Plot_X.ToArray, Plot_Mean.ToArray, New cZEDGraph.sGraphStyle(Color.Black, cZEDGraph.eCurveMode.Dots))
        PlotService.PlotXvsY("Min", Plot_X.ToArray, Plot_Min.ToArray, New cZEDGraph.sGraphStyle(Color.Green, cZEDGraph.eCurveMode.Dots))
        PlotService.PlotXvsY("#", Plot_X.ToArray, Plot_BinCount.ToArray, New cZEDGraph.sGraphStyle(Color.Blue, cZEDGraph.eCurveMode.Dots), True)
        PlotService.AutoScaleY2AxisLog()

        'Plot the limits - on top we us 95-pct to ignore the stars ...
        PlotService.PlotXvsY("1-pct", New Double() {LeftLimit, LeftLimit}, New Double() {UInt16.MinValue, UInt16.MaxValue}, New cZEDGraph.sGraphStyle(Color.Orange, cZEDGraph.eCurveMode.Lines))
        PlotService.PlotXvsY("95-pct", New Double() {RightLimit, RightLimit}, New Double() {UInt16.MinValue, UInt16.MaxValue}, New cZEDGraph.sGraphStyle(Color.Orange, cZEDGraph.eCurveMode.Lines))

        'Update the display
        PlotService.GridOnOff(True, True)
        PlotService.MaximizePlotArea()
        PlotService.SetCaptions("Fit: y = " & Polynom(0).ValRegIndep & " + " & Polynom(1).ValRegIndep & " * x", "File1", "File2")
        PlotService.ForceUpdate()

    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

End Class