Option Explicit On
Option Strict On

Public Class frmHistogram

    Dim Hist_Left As Double = 5.0
    Dim Hist_Right As Double = 95.0
    Dim Pen_Left As New Pen(Color.Orange, 2.0F)
    Dim Pen_Right As New Pen(Color.Blue, 2.0F)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Using g As Graphics = pbHisto.CreateGraphics
            g.Clear(Color.White)
            VertLine(g, Pen_Left, Hist_Left)
            VertLine(g, Pen_Right, Hist_Right)
        End Using
    End Sub

    '''<summary>Draw a vertical line.</summary>
    Private Sub VertLine(ByRef g As Graphics, ByRef p As Pen, ByVal Percentage As Double)
        Dim Bounds As RectangleF = g.VisibleClipBounds
        Dim Top As New PointF(CSng(Bounds.X + (Bounds.Width * (Percentage / 100))), Bounds.Y + Bounds.Height)
        Dim Bottom As New PointF(CSng(Bounds.X + (Bounds.Width * (Percentage / 100))), Bounds.Y)
        g.DrawLine(p, Top, Bottom)
    End Sub

    Private Sub pbHisto_MouseMove(sender As Object, e As MouseEventArgs) Handles pbHisto.MouseMove
        If e.Button = MouseButtons.Left Then
            Dim PointInHisto As Point = pbHisto.PointToClient(MousePosition)
            Dim PctVal As Double = 100 * (PointInHisto.X / pbHisto.Width)
            If Math.Abs(PctVal - Hist_Left) < Math.Abs(PctVal - Hist_Right) Then
                Hist_Left = PctVal
            Else
                Hist_Right = PctVal
            End If
            Me.Invalidate()
        End If
    End Sub

End Class