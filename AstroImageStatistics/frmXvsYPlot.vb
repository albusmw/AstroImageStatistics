Option Explicit On
Option Strict On

Imports AstroImageStatistics.Ato

Public Class frmXvsYPlot

    '''<summary>The ZED graph control inside the form.</summary>
    Private zgcMain1 As ZedGraph.ZedGraphControl = Nothing
    '''<summary>The ZED graph service (from file ZEDGraphService.vb).</summary>
    Private Plotter1 As cZEDGraph = Nothing
    '''<summary>The ZED graph control inside the form.</summary>
    Private zgcMain2 As ZedGraph.ZedGraphControl = Nothing
    '''<summary>The ZED graph service (from file ZEDGraphService.vb).</summary>
    Private Plotter2 As cZEDGraph = Nothing

    Private Sub frmXvsYPlot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        zgcMain1 = New ZedGraph.ZedGraphControl : zgcMain1.IsShowCursorValues = True
        pPlot1.Controls.Add(zgcMain1)
        zgcMain1.Dock = DockStyle.Fill
        Plotter1 = New cZEDGraph(zgcMain1)
        zgcMain2 = New ZedGraph.ZedGraphControl : zgcMain2.IsShowCursorValues = True
        pPlot2.Controls.Add(zgcMain2)
        zgcMain2.Dock = DockStyle.Fill
        Plotter2 = New cZEDGraph(zgcMain2)
        FillBox(cbX) : cbX.SelectedIndex = 0
        FillBox(cbY) : cbY.SelectedIndex = 1
    End Sub

    Private Sub FillBox(ByRef Box As ComboBox)
        With Box
            .Items.Clear()
            For Each Pattern As String In AIS.Config.BayerPatternNames
                .Items.Add(Pattern)
            Next
        End With
    End Sub

    Private Sub btnPlot_Click(sender As Object, e As EventArgs) Handles btnPlot.Click
        Calculate()
    End Sub

    Private Sub Calculate()

        Dim ModeX As String = cbX.Text
        Dim ModeY As String = cbY.Text
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                Dim VectorX As New List(Of UInt16)
                Dim VectorY As New List(Of UInt16)
                With AIS.DB.LastFile_Data.DataProcessor_UInt16
                    '1st select
                    Dim Offset_1 As Integer() = GetOffsets(cbX.SelectedIndex)
                    For Idx1 As Integer = Offset_1(0) To .ImageData(0).NAXIS1 - 1 Step 2
                        For Idx2 As Integer = Offset_1(1) To .ImageData(0).NAXIS2 - 1 Step 2
                            VectorX.Add(.ImageData(0).Data(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                    '2ns select
                    Dim Offset_2 As Integer() = GetOffsets(cbY.SelectedIndex)
                    For Idx1 As Integer = Offset_2(0) To .ImageData(0).NAXIS1 - 1 Step 2
                        For Idx2 As Integer = Offset_2(1) To .ImageData(0).NAXIS2 - 1 Step 2
                            VectorY.Add(.ImageData(0).Data(Idx1, Idx2))
                        Next Idx2
                    Next Idx1
                    'Calculate histogram data
                    Dim HistData As New Dictionary(Of UInt16, cSingleValueStatistics)
                    For Idx As Integer = 0 To VectorX.Count - 1
                        If HistData.ContainsKey(VectorX(Idx)) = False Then HistData.Add(VectorX(Idx), New cSingleValueStatistics())
                        HistData(VectorX(Idx)).AddValue(VectorY(Idx))
                    Next Idx
                    HistData = HistData.SortDictionary
                    'Prepare data
                    Dim XPoints As Integer = HistData.Keys.Count
                    Dim XAxisUInt16(XPoints - 1) As UInt16
                    Dim XAxis(XPoints - 1) As Double
                    Dim Y_Lin(XPoints - 1) As Double
                    Dim Y_Min(XPoints - 1) As Double
                    Dim Y_Max(XPoints - 1) As Double
                    Dim Y_Mean(XPoints - 1) As Double
                    Dim Y_Median(XPoints - 1) As Double
                    Dim Y_Sigma(XPoints - 1) As Double
                    Dim Y_Count(XPoints - 1) As Double
                    Dim Ptr As Integer = 0
                    For Each Entry As UInt16 In HistData.Keys
                        XAxisUInt16(Ptr) = Entry : Ptr += 1
                    Next Entry
                    For Idx As Integer = 0 To XAxisUInt16.GetUpperBound(0)
                        XAxis(Idx) = XAxisUInt16(Idx)
                        Y_Lin(Idx) = XAxisUInt16(Idx)
                        Y_Min(Idx) = HistData(XAxisUInt16(Idx)).Minimum
                        Y_Max(Idx) = HistData(XAxisUInt16(Idx)).Maximum
                        Y_Mean(Idx) = HistData(XAxisUInt16(Idx)).Mean
                        Y_Median(Idx) = HistData(XAxisUInt16(Idx)).Percentile(50)
                        Y_Sigma(Idx) = HistData(XAxisUInt16(Idx)).Sigma
                        Y_Count(Idx) = HistData(XAxisUInt16(Idx)).ValueCount
                    Next Idx
                    'Calculate curve fit
                    Dim Polynom As Double() = {}
                    SignalProcessing.RegressPoly(XAxis, Y_Mean, 1, Polynom)
                    Dim Y_Regress As Double() = SignalProcessing.ApplyPoly(XAxis, Polynom)
                    'Draw
                    Plotter1.Clear()
                    Plotter1.PlotXvsY("Linear", XAxis, Y_Lin, New cZEDGraph.sGraphStyle(Color.Black, cZEDGraph.eCurveMode.Lines))
                    Plotter1.PlotXvsY("Minimum", XAxis, Y_Min, New cZEDGraph.sGraphStyle(Color.Red, cZEDGraph.eCurveMode.Lines))
                    Plotter1.PlotXvsY("Maximum", XAxis, Y_Max, New cZEDGraph.sGraphStyle(Color.Green, cZEDGraph.eCurveMode.Lines))
                    Plotter1.PlotXvsY("Mean", XAxis, Y_Mean, New cZEDGraph.sGraphStyle(Color.Orange, cZEDGraph.eCurveMode.Lines))
                    Plotter1.PlotXvsY("Median", XAxis, Y_Median, New cZEDGraph.sGraphStyle(Color.Blue, cZEDGraph.eCurveMode.Lines))
                    Plotter1.PlotXvsY("Curve fit (y=" & Polynom(0).ValRegIndep & " + " & Polynom(1).ValRegIndep & "*x", XAxis, Y_Regress, New cZEDGraph.sGraphStyle(Color.LimeGreen, cZEDGraph.eCurveMode.Lines))
                    Plotter1.SetCaptions(cbX.Text & " vs " & cbY.Text, cbX.Text, cbY.Text)
                    Plotter1.ManuallyScaleXAxisLin(XAxisUInt16(0), XAxisUInt16(XAxisUInt16.Count - 1))
                    Plotter1.GridOnOff(True, True)
                    zgcMain1.Invalidate() : zgcMain1.Refresh()
                    Plotter2.Clear()
                    Plotter2.PlotXvsY("Sigma", XAxis, Y_Sigma, New cZEDGraph.sGraphStyle(Color.Blue, cZEDGraph.eCurveMode.Lines))
                    Plotter2.PlotXvsY("# Values", XAxis, Y_Count, New cZEDGraph.sGraphStyle(Color.Red, cZEDGraph.eCurveMode.Lines), True)
                    Plotter2.SetCaptions(cbX.Text & " vs " & cbY.Text, cbX.Text, "Sigma")
                    Plotter2.ManuallyScaleXAxisLin(XAxisUInt16(0), XAxisUInt16(XAxisUInt16.Count - 1))
                    Plotter2.GridOnOff(True, True)
                    zgcMain2.Invalidate() : zgcMain2.Refresh()
                End With
        End Select

    End Sub

    Private Function GetOffsets(ByVal BoxIndex As Integer) As Integer()
        Select Case BoxIndex
            Case 0 : Return New Integer() {0, 0}
            Case 1 : Return New Integer() {0, 1}
            Case 2 : Return New Integer() {1, 0}
            Case 3 : Return New Integer() {1, 1}
            Case Else : Return New Integer() {-1, -1}
        End Select
    End Function

    Private Sub cbX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbX.SelectedIndexChanged, cbY.SelectedIndexChanged
        If (cbX.Text.Length > 0) And (cbY.Text.Length) > 0 Then
            Calculate()
        End If
    End Sub

End Class