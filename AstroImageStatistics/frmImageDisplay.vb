Option Explicit On
Option Strict On

Public Class frmImageDisplay

    '''<summary>Class for data to image conversion.</summary>
    Public ImageFromData As New cImageFromData

    '''<summary>Configuration for the display.</summary>
    'Public Props As New cImageDisplayProp

    '''<summary>Statistics calculator (where the image data are stored ...).</summary>
    Public SingleStatCalc As AstroNET.Statistics
    '''<summary>Statistics of the passed SingleStatCalc data.</summary>
    Public StatToUsed As AstroNET.Statistics.sStatistics

    Public ZoomStatCalc As AstroNET.Statistics
    Public ZoomStatistics As AstroNET.Statistics.sStatistics

    Public MyIPP As cIntelIPP

    Private ROICenter As Point

    Private ScaleImage As cLockBitmap32Bit

    Private ZoomInOutputImage As cLockBitmap32Bit

    'This elements are self-coded and will not work in 64-bit from the toolbox ...
    Private WithEvents pbMain As PictureBoxEx
    'This elements are self-coded and will not work in 64-bit from the toolbox ...
    Private WithEvents pbMainScale As PictureBoxEx
    'This elements are self-coded and will not work in 64-bit from the toolbox ...
    Private WithEvents pbZoomed As PictureBoxEx

    '''<summary>File name of the display to show.</summary>
    Public FileToDisplay As String = String.Empty

    '''<summary>Currently the component only supported mono images so NAXIS3 is static 0.</summary>
    Public NAXIS3 As Integer = 0

    '''<summary>Selected ROI for zoom.</summary>
    Private ROICoord As Rectangle

    '''<summary>Floating-point coordinated of the mouse within the picture.</summary>
    Private FloatCenter As Drawing.PointF

    Private Sub frmImageDisplay_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Text = "Image <" & FileToDisplay & ">"
    End Sub

    Private Sub frmImageDisplay_Load(sender As Object, e As EventArgs) Handles Me.Load

        ZoomStatCalc = New AstroNET.Statistics(MyIPP)

        'Load custom controls - main image (must be done due to 64-bit IDE limitation)
        pbMain = New PictureBoxEx
        scImageAndScale.Panel1.Controls.Add(pbMain)
        pbMain.Dock = DockStyle.Fill
        pbMain.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbMain.SizeMode = PictureBoxSizeMode.Zoom
        pbMain.BackColor = Color.Purple

        'Load custom controls - scale display (must be done due to 64-bit IDE limitation)
        pbMainScale = New PictureBoxEx
        scImageAndScale.Panel2.Controls.Add(pbMainScale)
        pbMainScale.Dock = DockStyle.Fill
        pbMainScale.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbMainScale.SizeMode = PictureBoxSizeMode.StretchImage
        pbMainScale.BackColor = Color.LightGray

        'Load custom controls - zoomed-in image (must be done due to 64-bit IDE limitation)
        pbZoomed = New PictureBoxEx
        scDetails.Panel2.Controls.Add(pbZoomed)
        pbZoomed.Dock = DockStyle.Fill
        pbZoomed.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbZoomed.SizeMode = PictureBoxSizeMode.Zoom
        pbZoomed.BackColor = Color.Purple

        'Configure GUI in general
        scMain.SplitterDistance = 200
        pgMain.SelectedObject = ImageFromData

    End Sub

    '''<summary>Calculate the image to be displayed.</summary>
    Public Sub GenerateDisplayImage()

        Dim Stopper As New cStopper
        Dim ImageDataMin As UInt16 = CUShort(SingleStatCalc.ImageStatistics.MonoStatistics_Int.Min.Key)
        Dim ImageDataMax As UInt16 = CUShort(SingleStatCalc.ImageStatistics.MonoStatistics_Int.Max.Key)
        ImageFromData.GenerateDisplayImage(SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data, ImageDataMin, ImageDataMax, MyIPP)
        DisplayLUTColorBar()

        'Display final image
        Stopper.Tic()
        pbMain.BackColor = ImageFromData.BackColor
        pbZoomed.BackColor = ImageFromData.BackColor
        ImageFromData.OutputImage.UnlockBits()
        pbMain.Image = ImageFromData.OutputImage.BitmapToProcess
        Stopper.Toc("Display image")

        ShowDetails()

        'Copy timing information to clipboard
        'Clipboard.SetText(Join(Stopper.GetLog.ToArray, System.Environment.NewLine))

    End Sub

    '''<summary>Display LUT color bar.</summary>
    Private Sub DisplayLUTColorBar()

        'Generate LUT image
        'Dim ScaleDisplay_Width As Integer = CInt(Data_Max - Data_Min + 1)
        ScaleImage = New cLockBitmap32Bit(800, 20)
        ScaleImage.LockBits(False)
        Dim CopyPtr As Integer = 0
        For Y As Integer = 0 To ScaleImage.Height - 1
            For X As Integer = 0 To ScaleImage.Width - 1
                ScaleImage.Pixels(CopyPtr) = ImageFromData.LUT(CUShort((X / ScaleImage.Width) * UInt16.MaxValue))
                CopyPtr += 1
            Next X
        Next Y
        ScaleImage.UnlockBits()
        pbMainScale.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbMainScale.Image = ScaleImage.BitmapToProcess

    End Sub

    Private Sub pgMain_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgMain.PropertyValueChanged
        'Special handling for certain property changes that need additional calculation
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "MinCutOff_pct"
                ImageFromData.MinCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(ImageFromData.MinCutOff_pct)
            Case "MaxCutOff_pct"
                ImageFromData.MaxCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(ImageFromData.MaxCutOff_pct)
        End Select
        DisplayCurrentProps()
        GenerateDisplayImage()
    End Sub

    Private Sub pgMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgMain.MouseWheel
        Dim PropToChange As cImageFromData = CType(pgMain.SelectedObject, cImageFromData)
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "MinCutOff_pct"
                PropToChange.MinCutOff_pct += Math.Sign(e.Delta) * PropToChange.PctStepSize
                If PropToChange.MinCutOff_pct < 0.0 Then PropToChange.MinCutOff_pct = 0.0
                ImageFromData.MinCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(ImageFromData.MinCutOff_pct)
            Case "MaxCutOff_pct"
                PropToChange.MaxCutOff_pct += Math.Sign(e.Delta) * PropToChange.PctStepSize
                If PropToChange.MaxCutOff_pct > 100.0 Then PropToChange.MaxCutOff_pct = 100.0
                ImageFromData.MaxCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(ImageFromData.MaxCutOff_pct)
            Case "MinCutOff_ADU"
                If e.Delta < 0 Then
                    ImageFromData.MinCutOff_ADU = StatToUsed.MonochromHistogram_NextBelow(ImageFromData.MinCutOff_ADU)
                Else
                    ImageFromData.MinCutOff_ADU = StatToUsed.MonochromHistogram_NextAbove(ImageFromData.MinCutOff_ADU)
                End If
            Case "MaxCutOff_ADU"
                If e.Delta < 0 Then
                    ImageFromData.MaxCutOff_ADU = StatToUsed.MonochromHistogram_NextBelow(ImageFromData.MaxCutOff_ADU)
                Else
                    ImageFromData.MaxCutOff_ADU = StatToUsed.MonochromHistogram_NextAbove(ImageFromData.MaxCutOff_ADU)
                End If
            Case "Gamma"
                PropToChange.Gamma += Math.Sign(e.Delta) * PropToChange.PctStepSize
        End Select
        DisplayCurrentProps()
        GenerateDisplayImage()
    End Sub

    Private Sub pbMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pbMain.MouseWheel
        If e.Delta > 0 Then
            ImageFromData.ZoomSize -= 2
        Else
            ImageFromData.ZoomSize += 2
        End If
        CalculateZoomParameters()
        ShowDetails()
    End Sub

    '''<summary>Moving the mouse changed the point to zoom in.</summary>
    Private Sub pbMain_MouseMove(sender As Object, e As MouseEventArgs) Handles pbMain.MouseMove
        FloatCenter = pbMain.ScreenCoordinatesToImageCoordinates
        CalculateZoomParameters()
        ShowDetails()
    End Sub

    Private Sub frmImageDisplay_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        e.Handled = False
        If e.Control = True Then
            Select Case e.KeyCode
                'ZOOM IMAGE OPERATIONS
                Case Keys.C
                    'Copy zoomed to clipboard
                    Clipboard.Clear()
                    Clipboard.SetImage(pbZoomed.Image)
                    e.Handled = True : tsslInfo1.Text = "Zoomed image copied to clipboard."
                Case Keys.D
                    'Get zoom-in data
                    Dim PadSize As Integer = CInt(IIf(ImageFromData.BinPatternInZoomData, 16, 5))
                    Dim ClipContent_AsIs As New List(Of String)
                    Dim ClipContent_Binary As New List(Of String)
                    For Idx1 As Integer = 0 To ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(0)
                        Dim Line As String = "#" & Format(Idx1, "00").Trim & ":"
                        Dim ClipLine_AsIs As New List(Of String)
                        Dim ClipLine_Binary As New List(Of String)
                        For Idx2 As Integer = 0 To ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(1)
                            Dim Value As UInt16 = ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2)
                            ClipLine_AsIs.Add(Value.ValRegIndep.PadLeft(PadSize))
                            ClipLine_Binary.Add(GetBits(Value))
                        Next Idx2
                        ClipContent_AsIs.Add(Line & Join(ClipLine_AsIs.ToArray, ";"))
                        If ImageFromData.BinPatternInZoomData Then ClipContent_Binary.Add(Line & Join(ClipLine_Binary.ToArray, ";"))
                    Next Idx1
                    ClipContent_AsIs.AddRange(ClipContent_Binary)
                    Clipboard.Clear() : Clipboard.SetText(Join(ClipContent_AsIs.ToArray, System.Environment.NewLine))
                    e.Handled = True : tsslInfo1.Text = "Image pixel values copied to clipboard."
                'MOVE ROI
                Case Keys.A
                    FloatCenter = New PointF(FloatCenter.X - 1, FloatCenter.Y)
                    CalculateZoomParameters()
                    ShowDetails()
                    e.Handled = True
                    tsslInfo1.Text = "Center is now <" & FloatCenter.X.ValRegIndep & "," & FloatCenter.Y.ValRegIndep & ">"
                Case Keys.S
                    FloatCenter = New PointF(FloatCenter.X + 1, FloatCenter.Y)
                    CalculateZoomParameters()
                    ShowDetails()
                    e.Handled = True
                    tsslInfo1.Text = "Center is now <" & FloatCenter.X.ValRegIndep & "," & FloatCenter.Y.ValRegIndep & ">"
                Case Keys.W
                    FloatCenter = New PointF(FloatCenter.X, FloatCenter.Y + 1)
                    CalculateZoomParameters()
                    ShowDetails()
                    e.Handled = True
                    tsslInfo1.Text = "Center is now <" & FloatCenter.X.ValRegIndep & "," & FloatCenter.Y.ValRegIndep & ">"
                Case Keys.Y
                    FloatCenter = New PointF(FloatCenter.X, FloatCenter.Y - 1)
                    CalculateZoomParameters()
                    ShowDetails()
                    e.Handled = True
                    tsslInfo1.Text = "Center is now <" & FloatCenter.X.ValRegIndep & "," & FloatCenter.Y.ValRegIndep & ">"
            End Select
        End If
        ' e.Handled = True Then GenerateDisplayImage()
    End Sub

    Private Function GetBits(ByVal Value As UInt16) As String
        Dim RetVal As String = String.Empty
        Dim HexVal As String = Hex(Value).PadLeft(4, "0"c)
        For HexIdx As Integer = 0 To HexVal.Length - 1
            RetVal &= BitsOfHex(HexVal.Substring(HexIdx, 1))
        Next HexIdx
        Return RetVal
    End Function

    Private Function BitsOfHex(ByVal HexVal As String) As String
        Select Case HexVal
            Case "0" : Return "0000"
            Case "1" : Return "0001"
            Case "2" : Return "0010"
            Case "3" : Return "0011"
            Case "4" : Return "0100"
            Case "5" : Return "0101"
            Case "6" : Return "0110"
            Case "7" : Return "0111"
            Case "8" : Return "1000"
            Case "9" : Return "1001"
            Case "A" : Return "1010"
            Case "B" : Return "1011"
            Case "C" : Return "1100"
            Case "D" : Return "1101"
            Case "E" : Return "1110"
            Case "F" : Return "1111"
            Case Else : Return "    "
        End Select
    End Function

    '''<summary>Calculate the ROI from the zoom.</summary>
    Private Sub CalculateZoomParameters()

        'Calculate the zoom area
        Dim X_left As Integer : Dim X_right As Integer : Dim Y_up As Integer : Dim Y_down As Integer
        ROICenter = PictureBoxEx.CenterSizeToXY(FloatCenter, ImageFromData.ZoomSize, X_left, X_right, Y_up, Y_down)

        'Set ROI rectangle coordinates
        ROICoord = New Rectangle(X_left, Y_up, X_right - X_left + 1, Y_down - Y_up + 1)

    End Sub

    Public Sub ShowDetails()

        'Construct the holder of the zoomed image
        ZoomStatCalc.ResetAllProcessors()
        ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data = (SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data.GetROI(ROICoord.Left, ROICoord.Left + ROICoord.Width - 1, ROICoord.Top, ROICoord.Top + ROICoord.Height - 1))
        If ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data.LongLength < 4 Then Exit Sub

        'Calculate statistics
        ZoomStatistics = ZoomStatCalc.ImageStatistics()
        If ZoomStatistics.DataOK = False Then Exit Sub
        Dim Report As New List(Of String)
        Report.Add("ROI       : " & ROICoord.Width.ValRegIndep & "x" & ROICoord.Height.ValRegIndep)
        Report.Add("ROI - X   : " & ROICoord.Left.ValRegIndep & "...[" & ROICenter.X.ValRegIndep & "]..." & (ROICoord.Left + ROICoord.Width).ValRegIndep)
        Report.Add("ROI - Y   : " & ROICoord.Top.ValRegIndep & "...[" & ROICenter.Y.ValRegIndep & "]..." & (ROICoord.Top + ROICoord.Height).ValRegIndep)
        Report.Add("Min       : " & ZoomStatistics.MonoStatistics_Int.Min.Key.ValRegIndep)
        Report.Add("Max       : " & ZoomStatistics.MonoStatistics_Int.Max.Key.ValRegIndep)
        Report.Add("Mean      : " & ZoomStatistics.MonoStatistics_Int.Mean.ValRegIndep("0.0"))
        Report.Add("Median    : " & ZoomStatistics.MonoStatistics_Int.Median.ValRegIndep)
        Report.Add("1%-PCT    : " & ZoomStatistics.MonoStatistics_Int.GetPercentile(1).ValRegIndep)
        Report.Add("99%-PCT   : " & ZoomStatistics.MonoStatistics_Int.GetPercentile(99).ValRegIndep)
        Report.Add("ADU Values: " & ZoomStatistics.MonoStatistics_Int.DifferentADUValues.ValRegIndep)
        tbDetails.Text = String.Join(System.Environment.NewLine, Report)

        'Get the magnified version
        Dim ZoomInAsDisplayed(,) As UInt16 = ImgArrayFunction.RepeatePixel(ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, ImageFromData.PixelPerRealPixel)

        ZoomInOutputImage = New cLockBitmap32Bit(ZoomInAsDisplayed.GetUpperBound(0) + 1, ZoomInAsDisplayed.GetUpperBound(1) + 1)
        ZoomInOutputImage.LockBits(False)
        Dim CopyPtr As Integer = 0
        For Y As Integer = 0 To ZoomInAsDisplayed.GetUpperBound(1)
            For X As Integer = 0 To ZoomInAsDisplayed.GetUpperBound(0)
                Dim PixelValue As UInt16 = ZoomInAsDisplayed(X, Y)
                ZoomInOutputImage.Pixels(CopyPtr) = ImageFromData.LUT(PixelValue)
                CopyPtr += 1
            Next X
        Next Y
        ZoomInOutputImage.UnlockBits()
        pbZoomed.InterpolationMode = ImageFromData.ZoomInterpolation
        pbZoomed.Image = ZoomInOutputImage.BitmapToProcess

    End Sub

    Private Sub cms_SetCutOff_Click(sender As Object, e As EventArgs) Handles cms_SetCutOff.Click
        ImageFromData.MinCutOff_ADU = ZoomStatistics.MonoStatistics_Int.Min.Key
        ImageFromData.MaxCutOff_ADU = ZoomStatistics.MonoStatistics_Int.Max.Key
        DisplayCurrentProps()
        GenerateDisplayImage()
    End Sub

    '''<summary>Display the updated properties.</summary>
    Private Sub DisplayCurrentProps()
        pgMain.SelectedObject = ImageFromData
    End Sub

    Private Sub cms_SaveAsSeen_Click(sender As Object, e As EventArgs) Handles cms_SaveAsSeen.Click
        sfdMain.Filter = ImageFileFormatSpecific.SaveImageFileFilters
        If sfdMain.ShowDialog() <> DialogResult.OK Then Exit Sub
        ImageFileFormatSpecific.SaveImageFile(sfdMain.FileName, ImageFromData.OutputImage.BitmapToProcess)
    End Sub

    Private Sub pgMain_Click(sender As Object, e As EventArgs) Handles pgMain.Click

    End Sub

    'Private Sub cms_SendToNavigator_Click(sender As Object, e As EventArgs) Handles cms_SendToNavigator.Click
    '    For Each OpenForm As Form In Application.OpenForms
    '        If OpenForm.GetType.Name = "frmNavigator" Then
    '            CType(OpenForm, frmNavigator).tbOffsetX.Text = ROICenter.X.ToString.Trim
    '            CType(OpenForm, frmNavigator).tbOffsetY.Text = ROICenter.Y.ToString.Trim
    '            CType(OpenForm, frmNavigator).Focus()
    '        End If
    '    Next OpenForm
    'End Sub

    'Private Sub cms_SendToSinglePixelStat_Click(sender As Object, e As EventArgs) Handles cms_SendToSinglePixelStat.Click
    '    For Each OpenForm As Form In Application.OpenForms
    '        If OpenForm.GetType.Name = "frmSinglePixelStat" Then
    '            CType(OpenForm, frmSinglePixelStat).tbOffsetX.Text = ROICenter.X.ToString.Trim
    '            CType(OpenForm, frmSinglePixelStat).tbOffsetY.Text = ROICenter.Y.ToString.Trim
    '            CType(OpenForm, frmSinglePixelStat).Focus()
    '        End If
    '    Next OpenForm
    'End Sub

End Class