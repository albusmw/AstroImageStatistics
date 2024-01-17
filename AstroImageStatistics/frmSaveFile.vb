Option Explicit On
Option Strict On
Imports System.Windows
Imports System.Windows.Media.Imaging

Public Class frmSaveFile

    Dim FileEndings As String() = {"fits", "fits", "fits", "tif", "jpg", "png", "png"}

    '''<summary>List to report stored files to the calling form.</summary>
    Public SavedFiles As New List(Of String)

    Private Sub frmSaveFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        With cbFormat
            .Items.Clear()
            .Items.Add("FITS 16-bit fixed")
            .Items.Add("FITS 32-bit fixed")
            .Items.Add("FITS 32-bit float")
            .Items.Add("TIFF 16-bit")
            .Items.Add("JPG 8-bit")
            .Items.Add("PNG 8-bit")
            .Items.Add("PNG 16-bit")
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        With sfdMain
            If String.IsNullOrEmpty(tbFileName.Text) = False Then
                Dim InitDir As String = System.IO.Path.GetDirectoryName(tbFileName.Text)
                If System.IO.Directory.Exists(InitDir) Then .InitialDirectory = InitDir
            End If
            .OverwritePrompt = False
            If .ShowDialog = DialogResult.OK Then
                tbFileName.Text = System.IO.Path.ChangeExtension(.FileName, String.Empty).TrimEnd("."c)
            End If
        End With
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'TODO: Save also non-UInt16 data
        If IsNothing(AIS.DB.LastFile_Data) Then Exit Sub
        Dim FileName As String = System.IO.Path.ChangeExtension(tbFileName.Text, String.Empty).TrimEnd("."c) & "." & FileEndings(cbFormat.SelectedIndex)
        If System.IO.File.Exists(FileName) Then
            If MsgBox("File <" & FileName & "> already exists - overwrite?", vbCritical Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        tbSaveFileName.Text = "saving as <" & FileName & ">"
        With AIS.DB.LastFile_Data.DataProcessor_UInt16
            Select Case cbFormat.SelectedIndex
                Case 0
                    'FITS 16 bit fixed
                    cFITSWriter.Write(FileName, .ImageData(0).Data, cFITSWriter.eBitPix.Int16, AIS.DB.LastFile_FITSHeader.GetCardsAsList)
                    SavedFiles.Add(FileName & " - FITS 16 bit fixed")
                Case 1
                    'FITS 32 bit fixed
                    cFITSWriter.Write(FileName, .ImageData(0).Data, cFITSWriter.eBitPix.Int32, AIS.DB.LastFile_FITSHeader.GetCardsAsList)
                    SavedFiles.Add(FileName & " - FITS 32 bit fixed")
                Case 2
                    'FITS 32 bit float
                    cFITSWriter.Write(FileName, .ImageData(0).Data, cFITSWriter.eBitPix.Single, AIS.DB.LastFile_FITSHeader.GetCardsAsList)
                    SavedFiles.Add(FileName & " - FITS 32 bit float")
                Case 3
                    'TIFF 16-bit
                    If .OneChannelData = True Then
                        ImageFileFormatSpecific.SaveTIFF_Format16bppGrayScale(FileName, .ImageData(0).Data)
                        SavedFiles.Add(FileName & " - TIFF 16-bit grayscale")
                    Else
                        ImageFileFormatSpecific.SaveTIFF_Format48bppColor(FileName, .ImageData)
                        SavedFiles.Add(FileName & " - TIFF 48-bit color")
                    End If
                Case 4
                    'JPG 8-bit
                    Dim ImageQuality As Integer = CInt(tbImageQuality_JPEG.Text)
                    Dim myEncoderParameters As New System.Drawing.Imaging.EncoderParameters(1)
                    myEncoderParameters.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ImageQuality)
                    Dim BMP8BitToSave As Bitmap = cLockBitmap.Get8BitGrayscaleImage(.ImageData(0).Data, AIS.DB.LastFile_Statistics.MonoStatistics_Int.Max.Key).BitmapToProcess
                    BMP8BitToSave.Save(FileName, GetEncoderInfo("image/jpeg"), myEncoderParameters)
                    SavedFiles.Add(FileName & " - JPG 8-bit, quality " & ImageQuality.ValRegIndep)
                Case 5
                    'PNG 8-bit
                    Dim ImageQuality As Integer = CInt(tbImageQuality_PNG.Text)
                    Dim myEncoderParameters As New System.Drawing.Imaging.EncoderParameters(2)
                    myEncoderParameters.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ImageQuality)
                    myEncoderParameters.Param(1) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, 8)
                    Dim BMP8BitToSave As Bitmap = cLockBitmap.Get8BitGrayscaleImage(.ImageData(0).Data, AIS.DB.LastFile_Statistics.MonoStatistics_Int.Max.Key).BitmapToProcess
                    BMP8BitToSave.Save(FileName, GetEncoderInfo("image/png"), myEncoderParameters)
                    SavedFiles.Add(FileName & " - PNG 8-bit")
                Case 6
                    'PNG 16-bit
                    PNG16Bit(.ImageData(0).Data, FileName)
                    SavedFiles.Add(FileName & " - PNG 16-bit")
            End Select
        End With
        tbSaveFileName.Text = FileName
    End Sub

    '''<summary>Save the passed data as 16-bit PNG grayscale</summary>
    Private Sub PNG16Bit(ByRef Data(,) As UInt16, ByVal FileName As String)
        'Works and tested
        'https://stackoverflow.com/questions/9588367/creating-16-bit-grayscale-images-in-wpf
        Dim Width As Integer = Data.GetUpperBound(0) + 1
        Dim Height As Integer = Data.GetUpperBound(1) + 1
        Dim RetVal As New WriteableBitmap(Width, Height, 96, 96, System.Windows.Media.PixelFormats.Gray16, Nothing)
        Dim ROI As New Int32Rect(0, 0, Width, Height)
        Dim Stride As Integer = (Width * 2)
        RetVal.WritePixels(ROI, Data.Transpose, Stride, 0)
        Dim Encoder As New System.Windows.Media.Imaging.PngBitmapEncoder
        Encoder.Frames.Add(BitmapFrame.Create(RetVal))
        Using Writter As System.IO.Stream = System.IO.File.Create(FileName)
            Encoder.Save(Writter)
        End Using
    End Sub

    '''<summary>Get an encoder by its MIME name</summary>
    '''<param name="mimeType"></param>
    '''<returns></returns>
    Private Function GetEncoderInfo(ByVal mimeType As String) As Imaging.ImageCodecInfo
        Dim RetVal As Imaging.ImageCodecInfo = Nothing
        Dim AllEncoder As New List(Of String)
        For Each Encoder As Imaging.ImageCodecInfo In Imaging.ImageCodecInfo.GetImageEncoders
            If Encoder.MimeType = mimeType Then RetVal = Encoder
            AllEncoder.Add(Encoder.MimeType)
        Next Encoder
        Return RetVal
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SavedFiles.Count = 0 Then Me.DialogResult = DialogResult.Cancel Else Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class