Option Explicit On
Option Strict On

Public Class frmSinglePixelStat

    Private AllFiles As New List(Of String)
    Private AllDataOffsets As New Dictionary(Of String, Integer)
    Private AllWidths As New Dictionary(Of String, Integer)
    Private LongestFileName As Integer = 0

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        'Run()
    End Sub

    Private Sub ReadAllHeaders()
        AllFiles = New List(Of String)
        AllDataOffsets = New Dictionary(Of String, Integer)
        AllWidths = New Dictionary(Of String, Integer)
        Dim DirScanner As New RecursiveDirScanner(tbRootFolder.Text)
        DirScanner.Scan(tbFilter.Text)
        LongestFileName = 0
        'Read all FITS headers and remember data offset
        pbReading.Maximum = DirScanner.AllFiles.Count + 1
        For Idx As Integer = 0 To DirScanner.AllFiles.Count - 1
            pbReading.Value = Idx
            Dim File As String = DirScanner.AllFiles(Idx)
            Dim FileNameOnly As String = File.Replace(tbRootFolder.Text, String.Empty)
            If FileNameOnly.Length > LongestFileName Then LongestFileName = FileNameOnly.Length                 'maximum filename length for optimum display
            Using BaseIn As New System.IO.StreamReader(File)
                Dim DataStartPos As Integer = 0
                Dim FITSHeaderParser As New cFITSHeaderParser(cFITSReader.ReadHeader(BaseIn, DataStartPos))
                AllFiles.Add(File)
                AllDataOffsets.Add(File, DataStartPos)
                AllWidths.Add(File, FITSHeaderParser.Width)
            End Using
        Next Idx
        pbReading.Value = 0
    End Sub

    Private Sub tbOffsetX_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbOffsetX.MouseWheel
        tbOffsetX.Text = CStr(CInt(tbOffsetX.Text) + Math.Sign(e.Delta)).Trim
    End Sub

    Private Sub tbOffsetY_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbOffsetY.MouseWheel
        tbOffsetY.Text = CStr(CInt(tbOffsetY.Text) + Math.Sign(e.Delta)).Trim
    End Sub

    Private Sub tbOffset_TextChanged(sender As Object, e As EventArgs) Handles tbOffsetX.TextChanged, tbOffsetY.TextChanged
        'Run()
    End Sub

    Private Sub tbRootFolder_TextChanged(sender As Object, e As EventArgs) Handles tbRootFolder.TextChanged
        'Selection is changed -> read all headers again ...
        AllFiles.Clear()
    End Sub

    Private Sub lbHotCandidates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbHotCandidates.SelectedIndexChanged
        Dim Splitted As String() = Split(CStr(lbHotCandidates.SelectedItem).Trim, ":")
        tbOffsetX.Text = Splitted(1)
        tbOffsetY.Text = Splitted(2)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lbHotCandidates.Items.Clear()
    End Sub

End Class