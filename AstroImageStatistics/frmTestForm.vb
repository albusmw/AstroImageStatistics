Option Explicit On
Option Strict On

Public Class frmTestForm

    Private Class cElements
        Public Property A As String = String.Empty
        Public Property B As Integer = 0
        Public Property C As Double = Double.NaN
        Public Sub New(ByVal MyA As String, ByVal MyB As Integer, ByVal MyC As Double)
            A = MyA
            B = MyB
            C = MyC
        End Sub
    End Class

    Private Content As New List(Of cElements)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        adgvMain.DataSource = Content
        Content.Add(New cElements("Teste", 1, 2))
        Content.Add(New cElements("Hurz", 5, 77))
        'adgvMain.DataSource = Nothing
        'adgvMain.DataSource = Content
    End Sub

End Class