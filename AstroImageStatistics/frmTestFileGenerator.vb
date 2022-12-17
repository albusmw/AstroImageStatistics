Option Explicit On
Option Strict On

Public Class frmTestFileGenerator

    Private Sub btnWriteTestFile_Click(sender As Object, e As EventArgs) Handles btnWriteTestFile.Click

        CType(sender, Button).Enabled = False : System.Windows.Forms.Application.DoEvents()

        Dim TestFileName As String = System.IO.Path.Combine(AIS.DB.MyPath, tbTestFileName.Text & "." & GetExtension())
        Dim DimX As Integer = CInt(tbDimX.Text)
        Dim DimY As Integer = CInt(tbDimY.Text)

        Select Case cbTestFileType.SelectedIndex
            Case 0
                cFITSWriter.WriteTestFile_Int8(TestFileName, DimX, DimY)
            Case 1
                cFITSWriter.WriteTestFile_Int16(TestFileName, DimX, DimY)
            Case 2
                ImageFileFormatSpecific.SaveTIFF_Format8bppGrayScale(TestFileName, GetTestImageData_Byte)
            Case 3
                ImageFileFormatSpecific.SaveTIFF_Format16bppGrayScale(TestFileName, GetTestImageData_UInt16)
        End Select

        'cFITSWriter.WriteTestFile_Int32(WriteToEXEFolder("FITS_BitPix32.FITS"))
        'cFITSWriter.WriteTestFile_Float32(WriteToEXEFolder("FITS_BitPix32f.FITS"))
        'cFITSWriter.WriteTestFile_Float64(WriteToEXEFolder("FITS_BitPix64f.FITS"))
        'cFITSWriter.WriteTestFile_UInt16_Cross(WriteToEXEFolder("UInt16_Cross_mono.fits"))
        'cFITSWriter.WriteTestFile_UInt16_Cross_RGB(WriteToEXEFolder("UInt16_Cross_rgb.fits"))
        'cFITSWriter.WriteTestFile_UInt16_XYIdent(WriteToEXEFolder("UInt16_XYIdent.fits"))

        If cbOpenAfterWrite.Checked Then Ato.Utils.StartWithItsEXE(TestFileName)

        CType(sender, Button).Enabled = True : System.Windows.Forms.Application.DoEvents()

    End Sub

    Private Sub frmTestFileGenerator_Load(sender As Object, e As EventArgs) Handles Me.Load
        With cbTestFileType
            .Items.Clear()
            .Items.Add("FITS, 8 Bit")
            .Items.Add("FITS, 16 Bit")
            .Items.Add("TIFF, 8 Bit")
            .Items.Add("TIFF, 16 Bit")
            .SelectedIndex = 0
        End With
    End Sub

    Private Function GetExtension() As String
        Select Case cbTestFileType.SelectedIndex
            Case 0, 1 : Return "fits"
            Case 2, 3 : Return "tiff"
            Case Else : Return "unknown"
        End Select
    End Function

    Private Function GetTestImageData_Byte() As Byte(,)
        Dim ImageData(CInt(tbDimX.Text) - 1, CInt(tbDimY.Text) - 1) As Byte
        Dim Value As Byte = Byte.MinValue
        For Idx1 As Integer = 0 To ImageData.GetUpperBound(1)
            For Idx2 As Integer = 0 To ImageData.GetUpperBound(0)
                ImageData(Idx2, Idx1) = Value
                If Value < Byte.MaxValue Then Value = Value + CType(1, Byte) Else Value = Byte.MinValue
            Next Idx2
        Next Idx1
        Return ImageData
    End Function

    Private Function GetTestImageData_UInt16() As UInt16(,)
        Dim ImageData(CInt(tbDimX.Text) - 1, CInt(tbDimY.Text) - 1) As UInt16
        Dim Value As UInt16 = UInt16.MinValue
        For Idx1 As Integer = 0 To ImageData.GetUpperBound(1)
            For Idx2 As Integer = 0 To ImageData.GetUpperBound(0)
                ImageData(Idx2, Idx1) = Value
                If Value < UInt16.MaxValue Then Value = Value + CType(1, UInt16) Else Value = UInt16.MinValue
            Next Idx2
        Next Idx1
        Return ImageData
    End Function

    Private Sub btnOpenExplorer_Click(sender As Object, e As EventArgs) Handles btnOpenExplorer.Click
        Process.Start(AIS.DB.MyPath)
    End Sub

End Class