Option Explicit On
Option Strict On

Public Class frmSinglePixelStat

    Private AllFiles As New List(Of String)
    Private AllDataOffsets As New Dictionary(Of String, Integer)
    Private AllWidths As New Dictionary(Of String, Integer)
    Private LongestFileName As Integer = 0

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        Run()
    End Sub

    Private Sub ReadAllHeaders()
        AllFiles = New List(Of String)
        AllDataOffsets = New Dictionary(Of String, Integer)
        AllWidths = New Dictionary(Of String, Integer)
        Dim DirScanner As New Ato.RecursivDirScanner(tbRootFolder.Text)
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

    Private Sub Run()

        Dim UseIPP As Boolean = True
        tbValues.Text = String.Empty
        System.Windows.Forms.Application.DoEvents()

        'Read all header if not yet loaded
        If AllFiles.Count = 0 Then ReadAllHeaders()

        'Read the same segment from all files and compose a new combined image
        Dim OffsetX As Integer = 0               'X position
        Dim OffsetY As Integer = 0               'Y position

        Try
            OffsetX = CInt(tbOffsetX.Text)
            OffsetY = CInt(tbOffsetY.Text)
        Catch ex As Exception
            Exit Sub
        End Try

        Dim FITSReader As New cFITSReader

        'Get all pixel
        Dim Stat As New Ato.cSingleValueStatistics(True)
        Dim AllPixel As New List(Of UInt16)
        Dim StringValues As New List(Of String)
        For Each File As String In AllDataOffsets.Keys
            Dim FileNameOnly As String = File.Replace(tbRootFolder.Text, String.Empty)
            'Dim PixelValue As UInt16 = FITSReader.ReadInUInt16(File, UseIPP, OffsetX, 1, OffsetY, 1, False)(0, 0)
            Dim PixelValueFast As UInt16 = GetOnePixel(File, AllDataOffsets(File), AllWidths(File), OffsetX, OffsetY)
            AllPixel.Add(PixelValueFast)
            StringValues.Add(FileNameOnly.PadRight(LongestFileName) & ":" & PixelValueFast.ValRegIndep.PadLeft(5))
            Stat.AddValue(PixelValueFast)
        Next File

        StringValues.Insert(0, "Mean: " & Stat.Mean)
        StringValues.Insert(1, "Max: " & Stat.Maximum)
        StringValues.Insert(2, "Min: " & Stat.Minimum)
        StringValues.Insert(3, "Sigma: " & Stat.Sigma)

        tbValues.Text = Join(StringValues.ToArray, System.Environment.NewLine)

    End Sub

    Private Sub tbOffsetX_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbOffsetX.MouseWheel
        tbOffsetX.Text = CStr(CInt(tbOffsetX.Text) + Math.Sign(e.Delta)).Trim
    End Sub

    Private Sub tbOffsetY_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbOffsetY.MouseWheel
        tbOffsetY.Text = CStr(CInt(tbOffsetY.Text) + Math.Sign(e.Delta)).Trim
    End Sub

    Private Sub tbOffset_TextChanged(sender As Object, e As EventArgs) Handles tbOffsetX.TextChanged, tbOffsetY.TextChanged
        Run()
    End Sub

    Private Sub tbRootFolder_TextChanged(sender As Object, e As EventArgs) Handles tbRootFolder.TextChanged
        'Selection is changed -> read all headers again ...
        AllFiles.Clear()
    End Sub

    Private Function GetOnePixel(ByVal FileName As String, ByVal DataStartPosition As Long, ByVal ImageWidth As Integer, ByVal XOffset As Integer, ByVal YOffset As Integer) As UInt16

        Dim BytePerPixel As Integer = 2

        'Open reader and position to start
        Dim DataReader As New System.IO.BinaryReader(System.IO.File.OpenRead(FileName))
        DataReader.BaseStream.Position = DataStartPosition

        'Read only 1 pixel
        Dim PixelOffset As Integer = ((YOffset * ImageWidth) + XOffset)
        DataReader.BaseStream.Position = DataStartPosition + (BytePerPixel * PixelOffset)
        Dim Bytes() As Byte = DataReader.ReadBytes(BytePerPixel)

        'Close data stream
        DataReader.Close()

        Return CUShort(BitConverter.ToInt16({Bytes(1), Bytes(0)}, 0) + 32768)

    End Function

    Private Sub lbHotCandidates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbHotCandidates.SelectedIndexChanged
        Dim Splitted As String() = Split(CStr(lbHotCandidates.SelectedItem).Trim, ":")
        tbOffsetX.Text = Splitted(1)
        tbOffsetY.Text = Splitted(2)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lbHotCandidates.Items.Clear()
    End Sub

End Class