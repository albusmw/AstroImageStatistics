Option Explicit On
Option Strict On

'''<summary>Dialog to handle last-opened-files.</summary>
Public Class frmLastOpenedFiles

    Public Property MaxFilesToDisplay As Integer = 10

    Private FullFileName As New List(Of String)
    Private DirNameOnly As New List(Of String)
    Private FileNameOnly As New List(Of String)

    '''<summary>File that was selected.</summary>
    Public ReadOnly Property SelectedFile As String
        Get
            Return MySelectedFile
        End Get
    End Property
    Private MySelectedFile As String = String.Empty

    '''<summary>Load the dialog.</summary>
    '''<param name="FilesToLoad">List of files to show.</param>
    Public Sub New(ByVal FilesToLoad As List(Of String))

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        If IsNothing(FilesToLoad) = False Then
            lbFiles.Items.Clear()
            Dim MaxDirLength As Integer = 0
            For Each Entry As String In FilesToLoad
                If System.IO.File.Exists(Entry) = True Then
                    Dim SingleDirName As String = System.IO.Path.GetDirectoryName(Entry)
                    FullFileName.Add(Entry)
                    DirNameOnly.Add(SingleDirName) : If SingleDirName.Length > MaxDirLength Then MaxDirLength = SingleDirName.Length
                    FileNameOnly.Add(System.IO.Path.GetFileName(Entry))
                End If
                If FilesToLoad.Count = MaxFilesToDisplay Then Exit For
            Next Entry
            For Idx As Integer = 0 To DirNameOnly.Count - 1
                lbFiles.Items.Add(DirNameOnly(Idx).PadRight(MaxDirLength) & "\" & FileNameOnly(Idx))
            Next Idx
        End If

    End Sub

    Private Sub frmLastOpenedFiles_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Try
            'Index is 0-based, so key 1 will become item 0
            MySelectedFile = FullFileName(lbFiles.SelectedIndex)
            e.Handled = True
            Me.DialogResult = DialogResult.OK
        Catch ex As Exception
            e.Handled = False
        End Try
    End Sub

    Private Sub lbFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbFiles.SelectedIndexChanged
        Try
            'Index is 0-based, so key 1 will become item 0
            MySelectedFile = FullFileName(lbFiles.SelectedIndex)
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