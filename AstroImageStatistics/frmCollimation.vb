Option Explicit On
Option Strict On

Public Class frmCollimation

    '''<summary>The ZED graph control inside the form.</summary>
    Public zgcHistoDist As ZedGraph.ZedGraphControl = Nothing
    '''<summary>The ZED graph control inside the form.</summary>
    Public zgcHistoCircle As ZedGraph.ZedGraphControl = Nothing
    '''<summary>The ZED graph service (from file ZEDGraphService.vb).</summary>
    Public PlotDist As cZEDGraph = Nothing
    '''<summary>The ZED graph service (from file ZEDGraphService.vb).</summary>
    Public PlotCircle As cZEDGraph = Nothing

    Public Collimation As New cCollimation

    Public Sub New()

        'This call is required by the designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call.
        zgcHistoDist = New ZedGraph.ZedGraphControl : scGraph.Panel1.Controls.Add(zgcHistoDist) : zgcHistoDist.Dock = DockStyle.Fill
        PlotDist = New cZEDGraph(zgcHistoDist) : PlotDist.Clear()
        zgcHistoCircle = New ZedGraph.ZedGraphControl : scGraph.Panel2.Controls.Add(zgcHistoCircle) : zgcHistoCircle.Dock = DockStyle.Fill
        PlotCircle = New cZEDGraph(zgcHistoCircle) : PlotCircle.Clear()

    End Sub

    Private Sub CalcData()

        Collimation.ShowHistoAroundCenter(New Drawing.Point(CInt(tbX.Text), CInt(tbY.Text)), cbXHalf.Checked, cbYHalf.Checked, CInt(tbRadius.Text), Val(tbRadiusCombiner.Text), CInt(tbAngleSegments.Text), PlotDist, PlotCircle)

        PlotDist.GridOnOff(True, True)
        PlotDist.ForceUpdate()
        PlotDist.MaximizePlotArea()
        zgcHistoDist.GraphPane.YAxis.Scale.Min = 0

        PlotCircle.GridOnOff(True, True)
        PlotCircle.ForceUpdate()
        PlotCircle.MaximizePlotArea()
        zgcHistoCircle.GraphPane.XAxis.Scale.Min = -180
        zgcHistoCircle.GraphPane.XAxis.Scale.Max = 180
        zgcHistoCircle.GraphPane.YAxis.Scale.Min = 0


        lRadiusBins.Text = Collimation.HistoBins.ToString.Trim

    End Sub

    Private Sub tbXX_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbX.MouseWheel, tbY.MouseWheel
        Dim OldVal As Integer = CInt(CType(sender, TextBox).Text)
        If e.Delta > 0 Then OldVal += 1 Else OldVal -= 1
        If OldVal < 0 Then OldVal = 0
        CType(sender, TextBox).Text = OldVal.ToString.Trim
        CalcData()
    End Sub

    Private Sub tbRadius_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbRadius.MouseWheel, tbAngleSegments.MouseWheel
        Dim OldVal As Integer = CInt(CType(sender, TextBox).Text)
        If e.Delta > 0 Then OldVal += 1 Else OldVal -= 1
        If OldVal < 0 Then OldVal = 0
        CType(sender, TextBox).Text = OldVal.ToString.Trim
        CalcData()
    End Sub

    Private Sub cbXHalf_CheckedChanged(sender As Object, e As EventArgs) Handles cbXHalf.CheckedChanged, cbXHalf.CheckedChanged
        CalcData()
    End Sub

    Private Sub tbRadiusCombiner_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbRadiusCombiner.MouseWheel
        Dim OldVal As Double = Val(CType(sender, TextBox).Text)
        If e.Delta > 0 Then OldVal += 0.1 Else OldVal -= 0.1
        If OldVal < 1.0 Then OldVal = 1.0
        CType(sender, TextBox).Text = OldVal.ToString.Trim.Replace(",", ".")
        CalcData()
    End Sub

End Class