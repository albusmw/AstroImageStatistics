Option Explicit On
Option Strict On

Public Class frmXvsYPlot

    Private MyDisplay As cZEDGraphForm

    Private Sub frmXvsYPlot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillBox(cbX)
        FillBox(cbY)
    End Sub

    Private Sub FillBox(ByRef Box As ComboBox)
        With Box
            .Items.Clear()
            .Items.Add("R[0,0]")
            .Items.Add("G[0,1]")
            .Items.Add("G[1,0]")
            .Items.Add("B[1,1]")
            For Each Pattern As String In AIS.DB.BayerPatternNames
                .Items.Add(Pattern)
            Next
        End With
    End Sub

    Private Sub btnPlot_Click(sender As Object, e As EventArgs) Handles btnPlot.Click
        Dim VectorX As New List(Of Double)
        Dim VectorY As New List(Of Double)
        Dim ModeX As String = cbX.Text
        Dim ModeY As String = cbY.Text
        Select Case AIS.DB.LastFile_Data.DataType
            Case AstroNET.Statistics.eDataType.UInt16
                With AIS.DB.LastFile_Data.DataProcessor_UInt16
                    Select Case ModeX
                        Case "R[0,0]", "G[0,1]", "G[1,0]", "B[1,1]"
                            Dim OffsetX As Integer = CInt(ModeX.Substring(ModeX.Length - 4, 1))
                            Dim OffsetY As Integer = CInt(ModeX.Substring(ModeX.Length - 2, 1))
                            For Idx1 As Integer = OffsetX To .ImageData(0).NAXIS1 - 1 Step 2
                                For Idx2 As Integer = OffsetY To .ImageData(0).NAXIS2 - 1 Step 2
                                    VectorX.Add(.ImageData(0).Data(Idx1, Idx2))
                                Next Idx2
                            Next Idx1
                    End Select
                    Select Case ModeY
                        Case "R[0,0]", "G[0,1]", "G[1,0]", "B[1,1]"
                            Dim OffsetX As Integer = CInt(ModeY.Substring(ModeY.Length - 4, 1))
                            Dim OffsetY As Integer = CInt(ModeY.Substring(ModeY.Length - 2, 1))
                            For Idx1 As Integer = OffsetX To .ImageData(0).NAXIS1 - 1 Step 2
                                For Idx2 As Integer = OffsetY To .ImageData(0).NAXIS2 - 1 Step 2
                                    VectorY.Add(.ImageData(0).Data(Idx1, Idx2))
                                Next Idx2
                            Next Idx1
                    End Select
                End With
        End Select
        If IsNothing(MyDisplay) Then MyDisplay = New cZEDGraphForm
        If MyDisplay.HostForm.IsDisposed Then MyDisplay = New cZEDGraphForm
        MyDisplay.PlotData("XvsY", VectorX.ToArray, VectorY.ToArray, New ZEDGraphUtil.sGraphStyle(Color.Red, ZEDGraphUtil.sGraphStyle.eCurveMode.Dots))
    End Sub

End Class