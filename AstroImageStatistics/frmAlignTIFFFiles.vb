Option Explicit On
Option Strict On

Public Class frmAlignTIFFFiles

    '''<summary>ROI image generator.</summary>
    Private ROIImageGenerator As New cImageFromData

    Private ImageData1(,) As Double
    Private ImageData2(,) As Double
    Private ImageData3(,) As Double

    Private WithEvents DD1 As cDragDrop
    Private WithEvents DD2 As cDragDrop
    Private WithEvents DD3 As cDragDrop

    Private Sub frmAlignTIFFFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        DD1 = New cDragDrop(tbFile1, True)
        DD2 = New cDragDrop(tbFile2, True)
        DD3 = New cDragDrop(tbFile3, True)
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

        Dim ROI1 As New Rectangle(BaseX + tbFile1_DeltaX.Text.ValRegIndepInteger, BaseY + tbFile1_DeltaY.Text.ValRegIndepInteger, ROIWidth, ROIHeigth)
        Dim ROI2 As New Rectangle(BaseX + tbFile2_DeltaX.Text.ValRegIndepInteger, BaseY + tbFile2_DeltaY.Text.ValRegIndepInteger, ROIWidth, ROIHeigth)
        Dim ROI3 As New Rectangle(BaseX + tbFile3_DeltaX.Text.ValRegIndepInteger, BaseY + tbFile3_DeltaY.Text.ValRegIndepInteger, ROIWidth, ROIHeigth)

        Dim TIFF_Load As New ImageFileFormatSpecific.cTIFF
        TIFF_Load.Load(tbFile1.Text, ROI1, ImageData1)
        TIFF_Load.Load(tbFile2.Text, ROI2, ImageData2)
        TIFF_Load.Load(tbFile3.Text, ROI3, ImageData3)

        ROIImageGenerator.CM = cColorMaps.eMaps.None
        ROIImageGenerator.GenerateDisplayImageRGB(ImageData1, ImageData2, ImageData3, AIS.DB.IPP)
        ROIImageGenerator.OutputImage.UnlockBits()
        pbImage.Image = ROIImageGenerator.OutputImage.BitmapToProcess

    End Sub

    '=================================================================================
    ' For class library
    '=================================================================================

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

End Class