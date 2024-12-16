Option Explicit On
Option Strict On

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
                    Dim TIFFSave As New ImageFileFormatSpecific.cTIFF
                    If .OneChannelData = True Then
                        TIFFSave.Save_16bppGrayScale(FileName, .ImageData(0).Data)
                        SavedFiles.Add(FileName & " - TIFF 16-bit grayscale")
                    Else
                        TIFFSave.Save_48bppColor(FileName, .ImageData)
                        SavedFiles.Add(FileName & " - TIFF 48-bit color")
                    End If
                Case 4
                    'JPG 8-bit
                    Dim JPEGSave As New ImageFileFormatSpecific.cJPEG
                    JPEGSave.ImageQuality = CInt(tbImageQuality_JPEG.Text)
                    JPEGSave.Save_8bpp(FileName, .ImageData(0).Data)
                    SavedFiles.Add(FileName & " - JPG 8-bit, quality " & JPEGSave.ImageQuality.ValRegIndep)
                Case 5
                    'PNG 8-bit
                    Dim PNGSave As New ImageFileFormatSpecific.cPNG
                    PNGSave.ImageQuality = CInt(tbImageQuality_PNG.Text)
                    PNGSave.Save_8bpp(FileName, .ImageData(0).Data)
                    SavedFiles.Add(FileName & " - PNG 8-bit")
                Case 6
                    'PNG 16-bit
                    Dim PNGSave As New ImageFileFormatSpecific.cPNG
                    PNGSave.Save_16bpp(FileName, .ImageData(0).Data)
                    SavedFiles.Add(FileName & " - PNG 16-bit")
            End Select
        End With
        tbSaveFileName.Text = FileName
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SavedFiles.Count = 0 Then Me.DialogResult = DialogResult.Cancel Else Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class