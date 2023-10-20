Option Explicit On
Option Strict On

Public Class cLog

    Private LogContent As New System.Text.StringBuilder
    Private MyTextBox As TextBox = Nothing
    Private MyToolStripLabel As ToolStripLabel = Nothing

    Public Sub New(ByRef TB As TextBox, ByRef TSL As ToolStripStatusLabel)
        MyTextBox = TB
        MyToolStripLabel = TSL
    End Sub

    Public Sub Clear()
        LogContent = New System.Text.StringBuilder
        If IsNothing(MyTextBox) = False Then MyTextBox.Text = String.Empty
        If IsNothing(MyToolStripLabel) = False Then MyToolStripLabel.Text = String.Empty
    End Sub

    Public Sub Log(ByVal Text As String)
        Log(Text, False, True)
    End Sub

    Public Sub Log(ByVal Text As String, ByVal Time As TimeSpan)
        Log(Text & " (" & CInt(Time.TotalMilliseconds) & " ms)", False, True)
    End Sub

    Public Sub Log(ByVal Text As List(Of String))
        Log(Text.ToArray)
    End Sub

    Public Sub Log(ByVal Indent As String, ByVal Text As String)
        Log(Indent & Text, False, False)
    End Sub

    Public Sub Log(ByVal Indent As String, ByVal Text() As String)
        For Each Line As String In Text
            Log(Indent & Line, False, False)
        Next Line
        UpdateLogBox()
    End Sub

    Public Sub Log(ByVal Text() As String)
        For Each Line As String In Text
            Log(Line, False, False)
        Next Line
        UpdateLogBox()
    End Sub

    Public Sub Log(ByVal Text As String, ByVal LogInStatus As Boolean, ByVal LogInBox As Boolean)
        Text = Format(Now, "HH.mm.ss:fff") & "|" & Text
        With LogContent
            If .Length = 0 Then
                .Append(Text)
            Else
                .Append(System.Environment.NewLine & Text)
            End If
        End With
        If LogInBox = True Then UpdateLogBox()
        If (LogInStatus = True) And (IsNothing(MyToolStripLabel) = False) Then MyToolStripLabel.Text = Text
        If (LogInBox = True) Or (LogInStatus = True) Then System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub UpdateLogBox()
        If IsNothing(MyTextBox) = False Then
            With MyTextBox
                .Text = LogContent.ToString
                If .Text.Length > 0 Then
                    .SelectionStart = .Text.Length - 1
                    .SelectionLength = 0
                    .ScrollToCaret()
                End If
            End With
        End If
    End Sub

End Class
