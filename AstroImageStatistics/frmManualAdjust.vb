Option Explicit On
Option Strict On

Public Class frmManualAdjust

    '''<summary>Form selected for modification.</summary>
    Private SelectedForm As cZEDGraphForm = Nothing

    '''<summary>All original curves from the graph.</summary>
    Private OriginalLines As New Dictionary(Of String, ZedGraph.IPointList)

    Private Sub frmManualAdjust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetOpenHistos()
    End Sub

    Private Sub GetOpenHistos()
        cbSelectedHisto.Items.Clear()
        For Each OpenForm As cZEDGraphForm In MyDB.AllPlots
            cbSelectedHisto.Items.Add(OpenForm.Text)
        Next OpenForm
        If cbSelectedHisto.Items.Count = 1 Then
            cbSelectedHisto.SelectedIndex = 0
        End If
    End Sub

    Private Sub tbCurve_Scale_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbCurve_1_Scale.MouseWheel, tbCurve_2_Scale.MouseWheel, tbCurve_3_Scale.MouseWheel, tbCurve_4_Scale.MouseWheel
        CType(sender, TextBox).Text = (Val(CType(sender, TextBox).Text) + ((tbCurve_Scale_Step.Text.ValRegIndep * e.Delta) / (120 * 20))).ValRegIndep
    End Sub

    Private Sub tbCurve_Offset_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbCurve_1_Offset.MouseWheel, tbCurve_2_Offset.MouseWheel, tbCurve_3_Offset.MouseWheel, tbCurve_4_Offset.MouseWheel
        CType(sender, TextBox).Text = (Val(CType(sender, TextBox).Text) + ((tbCurve_Offset_Step.Text.ValRegIndep * e.Delta) / (120))).ValRegIndep
    End Sub

    Private Sub tbCurve_YMul(sender As Object, e As MouseEventArgs) Handles tbCurve_1_YMul.MouseWheel, tbCurve_2_YMul.MouseWheel, tbCurve_3_YMul.MouseWheel, tbCurve_4_YMul.MouseWheel
        CType(sender, TextBox).Text = (Val(CType(sender, TextBox).Text) + ((tbCurve_YMul_Step.Text.ValRegIndep * e.Delta) / (120 * 20))).ValRegIndep
    End Sub

    Private Sub tbCurve_1_Scale_TextChanged(sender As Object, e As EventArgs) Handles tbCurve_1_Scale.TextChanged, tbCurve_1_Offset.TextChanged, tbCurve_1_YMul.TextChanged
        UpdateCurves(tbCurve_1_Name.Text, tbCurve_1_Scale.Text.ValRegIndep, tbCurve_1_Offset.Text.ValRegIndep, tbCurve_1_YMul.Text.ValRegIndep)
    End Sub

    Private Sub tbCurve_2_Scale_TextChanged(sender As Object, e As EventArgs) Handles tbCurve_2_Scale.TextChanged, tbCurve_2_Offset.TextChanged, tbCurve_2_YMul.TextChanged
        UpdateCurves(tbCurve_2_Name.Text, tbCurve_2_Scale.Text.ValRegIndep, tbCurve_2_Offset.Text.ValRegIndep, tbCurve_2_YMul.Text.ValRegIndep)
    End Sub

    Private Sub tbCurve_3_Scale_TextChanged(sender As Object, e As EventArgs) Handles tbCurve_3_Scale.TextChanged, tbCurve_3_Offset.TextChanged, tbCurve_3_YMul.TextChanged
        UpdateCurves(tbCurve_3_Name.Text, tbCurve_3_Scale.Text.ValRegIndep, tbCurve_3_Offset.Text.ValRegIndep, tbCurve_3_YMul.Text.ValRegIndep)
    End Sub

    Private Sub tbCurve_4_Scale_TextChanged(sender As Object, e As EventArgs) Handles tbCurve_4_Scale.TextChanged, tbCurve_4_Offset.TextChanged, tbCurve_4_YMul.TextChanged
        UpdateCurves(tbCurve_4_Name.Text, tbCurve_4_Scale.Text.ValRegIndep, tbCurve_4_Offset.Text.ValRegIndep, tbCurve_4_YMul.Text.ValRegIndep)
    End Sub

    Private Sub cbSelectedHisto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSelectedHisto.SelectedIndexChanged
        For Each OpenForm As cZEDGraphForm In MyDB.AllPlots
            If OpenForm.Text = cbSelectedHisto.SelectedItem.ToString.Trim Then
                SelectedForm = OpenForm
                Exit For
            End If
        Next OpenForm
    End Sub

    Private Function GetXVals(ByVal CurveName As String) As ZedGraph.IPointList
        For Each Item As ZedGraph.LineItem In SelectedForm.zgcMain.GraphPane.CurveList
            If Item.Label.Text = CurveName Then
                Dim RetVal As New ZedGraph.PointPairList
                For PointIdx As Integer = 0 To Item.Points.Count - 1
                    RetVal.Add(New ZedGraph.PointPair(Item.Points.Item(PointIdx).X, Item.Points.Item(PointIdx).Y))
                Next PointIdx
                Return RetVal
            End If
        Next Item
        Return Nothing
    End Function

    Private Sub UpdateCurves(ByVal CurveName As String, ByVal A As Double, ByVal B As Double, ByVal YMul As Double)
        If IsNothing(SelectedForm) = False Then
            If OriginalLines.ContainsKey(CurveName) = False Then OriginalLines.Add(CurveName, GetXVals(CurveName))
            Dim NewList As New ZedGraph.PointPairList
            For PointIdx As Integer = 0 To OriginalLines(CurveName).Count - 1
                NewList.Add(New ZedGraph.PointPair((A * OriginalLines(CurveName).Item(PointIdx).X) + B, OriginalLines(CurveName).Item(PointIdx).Y * YMul))
            Next PointIdx
            For Each Item As ZedGraph.LineItem In SelectedForm.zgcMain.GraphPane.CurveList
                If Item.Label.Text = CurveName Then
                    Item.Points = NewList
                End If
            Next Item
            SelectedForm.zgcMain.Invalidate()
            SelectedForm.zgcMain.Refresh()
        End If
    End Sub

End Class