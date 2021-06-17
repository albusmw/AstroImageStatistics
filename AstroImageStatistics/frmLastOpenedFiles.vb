Option Explicit On
Option Strict On

'''<summary>Dialog to handle last-opened-files.</summary>
Public Class frmLastOpenedFiles

    '''<summary>File that was selected.</summary>
    Public ReadOnly Property SelectedFile As String
        Get
            Return MySelectedFile
        End Get
    End Property
    Private MySelectedFile As String = String.Empty

    Public Property Files As New List(Of String)

    Private Sub frmLastOpenedFiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsNothing(Files) = False Then
            lbFiles.Items.Clear()
            Dim Key As Integer = 1
            For Each Entry As String In Files
                If System.IO.File.Exists(Entry) = True Then
                    lbFiles.Items.Add("[" & Key.ToString.Trim & "]:" & Entry)
                    Key += 1
                End If
                If Key = 10 Then Exit For
            Next Entry
        End If
    End Sub

    Private Sub frmLastOpenedFiles_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Try
            'Index is 0-based, so key 1 will become item 0
            MySelectedFile = CStr(lbFiles.Items(CInt(CStr(e.KeyChar)) - 1)).Substring(4)
            e.Handled = True
            Me.DialogResult = DialogResult.OK
        Catch ex As Exception
            e.Handled = False
        End Try
    End Sub

    Private Sub lbFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbFiles.SelectedIndexChanged
        Try
            'Index is 0-based, so key 1 will become item 0
            MySelectedFile = CStr(lbFiles.SelectedItem).Substring(4)
            Me.DialogResult = DialogResult.OK
        Catch ex As Exception
            'Do nothing ...
        End Try
    End Sub

    Private Sub frmLastOpenedFiles_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = DialogResult.Cancel
        End If
    End Sub
End Class