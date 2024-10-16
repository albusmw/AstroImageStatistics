Option Explicit On
Option Strict On

Public Class frmAlignTIFFFiles

    '''<summary>ROI image generator.</summary>
    Private ROIImageGenerator As New cImageFromData

    Private ImageData1(,) As Double
    Private ImageData2(,) As Double
    Private ImageData3(,) As Double

    Private WithEvents DD1 As Ato.DragDrop
    Private WithEvents DD2 As Ato.DragDrop
    Private WithEvents DD3 As Ato.DragDrop

    Private Sub frmAlignTIFFFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        DD1 = New Ato.DragDrop(tbFile1, True)
        DD2 = New Ato.DragDrop(tbFile2, True)
        DD3 = New Ato.DragDrop(tbFile3, True)
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Recalc()
        'Test write ...
        'cFITSWriter.Write("C:\GIT\AstroImageStatistics\AstroImageStatistics\bin\x64\Debug\net7.0-windows8.0\TempTestData.fits", FITSImage1.ImageData(0).Data, cFITSWriter.eBitPix.Int16)
    End Sub

    Private Sub Recalc()

        Dim BaseX As Integer = tbBase_X.Text.ValRegIndepInteger
        Dim BaseY As Integer = tbBase_Y.Text.ValRegIndepInteger
        Dim ROIWidth As Integer = tbROI_width.Text.ValRegIndepInteger
        Dim ROIHeigth As Integer = tbROI_heigth.Text.ValRegIndepInteger

        LoadTIFF(tbFile1.Text, New Rectangle(BaseX + tbFile1_DeltaX.Text.ValRegIndepInteger, BaseY + tbFile1_DeltaY.Text.ValRegIndepInteger, ROIWidth, ROIHeigth), UInt16.MaxValue, ImageData1)
        LoadTIFF(tbFile2.Text, New Rectangle(BaseX + tbFile2_DeltaX.Text.ValRegIndepInteger, BaseY + tbFile2_DeltaY.Text.ValRegIndepInteger, ROIWidth, ROIHeigth), UInt16.MaxValue, ImageData2)
        LoadTIFF(tbFile3.Text, New Rectangle(BaseX + tbFile3_DeltaX.Text.ValRegIndepInteger, BaseY + tbFile3_DeltaY.Text.ValRegIndepInteger, ROIWidth, ROIHeigth), UInt16.MaxValue, ImageData3)

        ROIImageGenerator.CM = cColorMaps.eMaps.None
        ROIImageGenerator.GenerateDisplayImageRGB(ImageData1, ImageData2, ImageData3, AIS.DB.IPP)
        ROIImageGenerator.OutputImage.UnlockBits()
        pbImage.Image = ROIImageGenerator.OutputImage.BitmapToProcess

    End Sub

    '=================================================================================
    ' For class library
    '=================================================================================

    '''<summary>Load data from the given file.</summary>
    '''<param name="TIFFFile">TIFF file to load data from.</param>
    '''<param name="ScalingFactor">Scaling factor to go from TIFF data format to DataContent UInt16 format.</param>
    '''<param name="DataContent">Data content to write data to.</param>
    Private Sub LoadTIFF(ByVal TIFFFile As String, ByVal ReadRect As Rectangle, ByVal ScalingFactor As Double, ByRef DataContent(,) As Double)

        Using File As BitMiracle.LibTiff.Classic.Tiff = BitMiracle.LibTiff.Classic.Tiff.Open(TIFFFile, "r")

            'Read all tags
            Dim AllTags As New Dictionary(Of BitMiracle.LibTiff.Classic.TiffTag, BitMiracle.LibTiff.Classic.FieldValue)
            For Each PossibleTag As BitMiracle.LibTiff.Classic.TiffTag In [Enum].GetValues(Of BitMiracle.LibTiff.Classic.TiffTag)
                Dim TagValue As BitMiracle.LibTiff.Classic.FieldValue() = File.GetField(PossibleTag)
                If IsNothing(TagValue) = False Then
                    AllTags.Add(PossibleTag, TagValue(0))
                End If
            Next PossibleTag

            'Get the relevant tags
            Dim IMAGEWIDTH As Integer = AllTags(BitMiracle.LibTiff.Classic.TiffTag.IMAGEWIDTH).ToInt()
            Dim IMAGELENGTH As Integer = AllTags(BitMiracle.LibTiff.Classic.TiffTag.IMAGELENGTH).ToInt()
            Dim BITSPERSAMPLE As Integer = AllTags(BitMiracle.LibTiff.Classic.TiffTag.BITSPERSAMPLE).ToInt()

            'Routine only works of 32-bit float at the moment
            If BITSPERSAMPLE <> 32 Then Exit Sub

            'Set image data and TIFF related arrays
            ReDim DataContent(ReadRect.Width - 1, ReadRect.Height - 1)                              '1st dimension is width, 2nd dimension is heigth
            Dim scanline As Byte() = New Byte(File.ScanlineSize - 1) {}                             'scanlinesize is bound to the width of the image
            Dim scanline32Bit As Single() = New Single(CInt(File.ScanlineSize / 4 - 1)) {}

            Dim PtrY As Integer = 0
            For Idx2 As Integer = 0 To IMAGELENGTH - 1                                              'scan over the complete heigth
                File.ReadScanline(scanline, Idx2)                                                   'read complete line
                If Idx2 >= ReadRect.Y Then                                                          'if line index is within selected range
                    Buffer.BlockCopy(scanline, 0, scanline32Bit, 0, scanline.Length)
                    For Idx1 As Integer = 0 To ReadRect.Width - 1
                        Dim PixelData As Double = scanline32Bit(ReadRect.X + Idx1)
                        DataContent(Idx1, PtrY) = PixelData
                    Next Idx1
                    PtrY += 1
                    If PtrY >= ReadRect.Height Then Exit For
                End If
            Next Idx2

        End Using

    End Sub

    Private Sub Delta_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbFile1_DeltaX.MouseWheel, tbFile2_DeltaX.MouseWheel, tbFile3_DeltaX.MouseWheel, tbFile1_DeltaY.MouseWheel, tbFile2_DeltaY.MouseWheel, tbFile3_DeltaY.MouseWheel
        Dim Value As Integer = CType(sender, TextBox).Text.ValRegIndepInteger
        If e.Delta > 0 Then Value += tbStepSize_Deltas.Text.ValRegIndepInteger Else Value -= tbStepSize_Deltas.Text.ValRegIndepInteger
        CType(sender, TextBox).Text = Value.ToString.Trim
        Recalc
    End Sub

    Private Sub Size_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbROI_width.MouseWheel, tbROI_heigth.MouseWheel
        Dim Value As Integer = CType(sender, TextBox).Text.ValRegIndepInteger
        If e.Delta > 0 Then Value += tbStepSize_Offset.Text.ValRegIndepInteger Else Value -= tbStepSize_Offset.Text.ValRegIndepInteger
        CType(sender, TextBox).Text = Value.ToString.Trim
        Recalc()
    End Sub

    Private Sub Base_MouseWheel(sender As Object, e As MouseEventArgs) Handles tbBase_X.MouseWheel, tbBase_Y.MouseWheel
        Dim Value As Integer = CType(sender, TextBox).Text.ValRegIndepInteger
        If e.Delta > 0 Then Value += tbStepSize_Offset.Text.ValRegIndepInteger Else Value -= tbStepSize_Offset.Text.ValRegIndepInteger
        CType(sender, TextBox).Text = Value.ToString.Trim
        Recalc
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub

    Private Sub tbFile1_TextChanged(sender As Object, e As EventArgs) Handles tbFile1.TextChanged

    End Sub

End Class