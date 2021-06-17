Option Explicit On
Option Strict On

Public Class frmImageDisplay

    '''<summary>Configuration for the display.</summary>
    Public Props As New cImageDisplayProp

    '''<summary>Statistics calculator (where the image data are stored ...).</summary>
    Public SingleStatCalc As AstroNET.Statistics
    '''<summary>Statistics of the passed SingleStatCalc data.</summary>
    Public StatToUsed As AstroNET.Statistics.sStatistics

    Public ZoomStatCalc As AstroNET.Statistics
    Public ZoomStatistics As AstroNET.Statistics.sStatistics

    Public MyIPP As cIntelIPP

    '''<summary>A copy of the image as 1D vector (increases speed).</summary>
    Private ImageData() As UInt16
    Private OutputImage As cLockBitmap32Bit

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

        'Load custom controls - scale image (must be done due to 64-bit IDE limitation)
        pbMainScale = New PictureBoxEx
        scImageAndScale.Panel2.Controls.Add(pbMainScale)
        pbMainScale.Dock = DockStyle.Fill
        pbMainScale.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbMainScale.SizeMode = PictureBoxSizeMode.Zoom
        pbMainScale.BackColor = Color.Purple

        'Load custom controls - zoomed image (must be done due to 64-bit IDE limitation)
        pbZoomed = New PictureBoxEx
        scDetails.Panel2.Controls.Add(pbZoomed)
        pbZoomed.Dock = DockStyle.Fill
        pbZoomed.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbZoomed.SizeMode = PictureBoxSizeMode.Zoom
        pbZoomed.BackColor = Color.Purple

        'Configure GUI in general
        scMain.SplitterDistance = 200
        pgMain.SelectedObject = Props

    End Sub

    '''<summary>Calculated look-up table.</summary>
    Private LUT(UShort.MaxValue) As Int32

    '''<summary>Calculate the image to be displayed.</summary>
    Public Sub GenerateDisplayImage()

        Dim Stopper As New cStopper

        'Build a LUT for all colors present in the picture - the LUT is build as 3 vectors with R/G/B values due to speed reason (dictionary is slow ...)
        Stopper.Tic()
        CalculateLUT()
        Stopper.Toc("Generating LUT for each pixel value in the image")

        'Generate the output picture
        Stopper.Tic()
        InitInternalMemory()
        Stopper.Toc("Prepare image")

        'Calculate output image data
        Stopper.Tic()
        Parallel.For(0, ImageData.GetUpperBound(0), Sub(Ptr As Integer)
                                                        OutputImage.Pixels(Ptr) = LUT(ImageData(Ptr))
                                                    End Sub)
        Dim GenTime As Long = Stopper.Toc("Calculating image (" & ImageData.LongLength.ToString.Trim & " pixel)")
        tsslInfo1.Text = "Speed: " & Format(((ImageData.LongLength * 2) / (1024 * 1024)) / (GenTime / 1000), "0.0") & " MByte/s"

        'Display final image
        Stopper.Tic()
        pbMain.BackColor = Props.BackColor
        pbZoomed.BackColor = Props.BackColor
        OutputImage.UnlockBits()
        pbMain.Image = OutputImage.BitmapToProcess
        Stopper.Toc("Display image")

        ShowDetails()

        'Copy timing information to clipboard
        'Clipboard.SetText(Join(Stopper.GetLog.ToArray, System.Environment.NewLine))

    End Sub

    Private Sub CalculateLUT()

        'Calculate data range and scaling
        Dim Data_Min As Long = StatToUsed.MonoStatistics_Int.Min.Key : If Props.MinMaxPctRescale = True Then Data_Min = Props.MinCutOff_ADU
        Dim Data_Max As Long = StatToUsed.MonoStatistics_Int.Max.Key : If Props.MinMaxPctRescale = True Then Data_Max = Props.MaxCutOff_ADU
        Dim LinOffset As Double = -Data_Min
        Dim ScaleRange1 As Double = 1.0 / (Data_Max - Data_Min)
        Dim ScaleRange255 As Double = 255.0 / (Data_Max - Data_Min)
        Dim MinRangeScaled As Double = Math.Floor((Props.MinCutOff_ADU + LinOffset) * ScaleRange255)
        Dim MaxRangeScaled As Double = Math.Floor((Props.MaxCutOff_ADU + LinOffset) * ScaleRange255)

        'Decide if a full LUT should be generated or only values that are really used should be in ...
        Dim GenerateFullLUT As Boolean = True
        Dim EntriesToGenerate As New List(Of Long)
        If GenerateFullLUT Then
            For Entry As Long = Data_Min To Data_Max
                EntriesToGenerate.Add(Entry)
            Next Entry
        Else
            For Each Entry As Long In StatToUsed.MonochromHistogram_Int.Keys
                EntriesToGenerate.Add(Entry)
            Next Entry
        End If

        'Generate LUT
        For Each Entry As Long In EntriesToGenerate
            Dim ColorToUse As Color
            Select Case Entry
                Case Is < Props.MinCutOff_ADU
                    If Props.CutOffSpecialColor = True Then
                        ColorToUse = Props.MinCutOff_color
                    Else
                        ColorToUse = cColorMaps.ColorByMap(MinRangeScaled, Props.ColorMap)
                    End If
                Case Is > Props.MaxCutOff_ADU
                    If Props.CutOffSpecialColor = True Then
                        ColorToUse = Props.MaxCutOff_color
                    Else
                        ColorToUse = cColorMaps.ColorByMap(MaxRangeScaled, Props.ColorMap)
                    End If
                Case Else
                    If Props.ColorUniMode = True Then
                        ColorToUse = Props.ColorUni
                    Else
                        Dim ScaledToOne As Double = (Entry + LinOffset) * ScaleRange1
                        ColorToUse = cColorMaps.ColorByMap(Math.Floor(255 * (ScaledToOne ^ Props.Gamma)), Props.ColorMap)
                    End If
            End Select
            LUT(CInt(Entry)) = (CInt(ColorToUse.A) << 24) + (CInt(ColorToUse.R) << 16) + (CInt(ColorToUse.G) << 8) + CInt(ColorToUse.B)
        Next Entry

        'Generate LUT image
        ScaleImage = New cLockBitmap32Bit(CInt(Data_Max - Data_Min + 1), 20)
        ScaleImage.LockBits(False)
        Dim CopyPtr As Integer = 0
        For Y As Integer = 0 To ScaleImage.Height - 1
            For X As Integer = 0 To ScaleImage.Width - 1
                ScaleImage.Pixels(CopyPtr) = LUT(CUShort((X / ScaleImage.Width) * UInt16.MaxValue))
                CopyPtr += 1
            Next X
        Next Y
        ScaleImage.UnlockBits()
        pbMainScale.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbMainScale.Image = ScaleImage.BitmapToProcess

    End Sub

    '''<summary>Init the internal memory variables (1D copy of the image and the cLockBitmap32Bit).</summary>
    Private Sub InitInternalMemory()
        Dim InitRequired = False
        If IsNothing(OutputImage) = True Then
            InitRequired = True
        Else
            If (OutputImage.Width <> SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data.GetUpperBound(0) + 1) Or (OutputImage.Height <> SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data.GetUpperBound(1) + 1) Then
                InitRequired = True
            End If
        End If
        If InitRequired = True Then
            OutputImage = New cLockBitmap32Bit(SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data.GetUpperBound(0) + 1, SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data.GetUpperBound(1) + 1)
            ReDim ImageData(CInt(SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data.LongLength - 1))
            Dim CopyPtr As Integer = 0
            For Y As Integer = 0 To OutputImage.Height - 1
                For X As Integer = 0 To OutputImage.Width - 1
                    ImageData(CopyPtr) = SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data(X, Y)
                    CopyPtr += 1
                Next X
            Next Y
        End If
        OutputImage.LockBits(False)
    End Sub

    Private Sub pgMain_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles pgMain.PropertyValueChanged
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "MinCutOff_pct"
                Props.MinCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(Props.MinCutOff_pct)
            Case "MaxCutOff_pct"
                Props.MaxCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(Props.MaxCutOff_pct)
        End Select
        DisplayCurrentProps()
        GenerateDisplayImage()
    End Sub

    Private Sub pgMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pgMain.MouseWheel
        Dim PropToChange As cImageDisplayProp = CType(pgMain.SelectedObject, cImageDisplayProp)
        Select Case pgMain.SelectedGridItem.PropertyDescriptor.Name
            Case "MinCutOff_pct"
                PropToChange.MinCutOff_pct += Math.Sign(e.Delta) * PropToChange.PctStepSize
                If PropToChange.MinCutOff_pct < 0.0 Then PropToChange.MinCutOff_pct = 0.0
                Props.MinCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(Props.MinCutOff_pct)
            Case "MaxCutOff_pct"
                PropToChange.MaxCutOff_pct += Math.Sign(e.Delta) * PropToChange.PctStepSize
                If PropToChange.MaxCutOff_pct > 100.0 Then PropToChange.MaxCutOff_pct = 100.0
                Props.MaxCutOff_ADU = StatToUsed.MonochromHistogram_PctFract(Props.MaxCutOff_pct)
            Case "MinCutOff_ADU"
                If e.Delta < 0 Then
                    Props.MinCutOff_ADU = StatToUsed.MonochromHistogram_NextBelow(Props.MinCutOff_ADU)
                Else
                    Props.MinCutOff_ADU = StatToUsed.MonochromHistogram_NextAbove(Props.MinCutOff_ADU)
                End If
            Case "MaxCutOff_ADU"
                If e.Delta < 0 Then
                    Props.MaxCutOff_ADU = StatToUsed.MonochromHistogram_NextBelow(Props.MaxCutOff_ADU)
                Else
                    Props.MaxCutOff_ADU = StatToUsed.MonochromHistogram_NextAbove(Props.MaxCutOff_ADU)
                End If
            Case "Gamma"
                PropToChange.Gamma += Math.Sign(e.Delta) * PropToChange.PctStepSize
        End Select
        DisplayCurrentProps()
        GenerateDisplayImage()
    End Sub

    Private Sub pbMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pbMain.MouseWheel
        If e.Delta > 0 Then
            Props.ZoomSize -= 2
        Else
            Props.ZoomSize += 2
        End If
        UpdateZoomCenter()
        ShowDetails()
    End Sub

    '''<summary>Moving the mouse changed the point to zoom in.</summary>
    Private Sub pbMain_MouseMove(sender As Object, e As MouseEventArgs) Handles pbMain.MouseMove
        UpdateZoomCenter()
        ShowDetails()
    End Sub

    Private Sub frmImageDisplay_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'Copy zoomed to clipboard
        If e.KeyCode = Keys.C And e.Control = True Then
            Clipboard.Clear()
            Clipboard.SetImage(pbZoomed.Image)
            e.Handled = True
        End If
        If e.Handled = True Then GenerateDisplayImage()
    End Sub

    '''<summary>Calculate the ROI from the zoom.</summary>
    Private Sub UpdateZoomCenter()

        Dim FloatCenter As Drawing.PointF = pbMain.ScreenCoordinatesToImageCoordinates

        'Calculate the zoom area
        Dim X_left As Integer : Dim X_right As Integer : Dim Y_up As Integer : Dim Y_down As Integer
        ROICenter = PictureBoxEx.CenterSizeToXY(FloatCenter, Props.ZoomSize, X_left, X_right, Y_up, Y_down)

        'Set ROI rectangle coordinates
        ROICoord = New Rectangle(X_left, Y_up, X_right - X_left + 1, Y_down - Y_up + 1)
        Props.Zoom_X = ROICenter.X
        Props.Zoom_Y = ROICenter.Y
        pgMain.SelectedObject = Props

    End Sub

    Public Sub ShowDetails()

        'Construct the holder of the zoomed image
        ZoomStatCalc.ResetAllProcessors()
        ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data = ImgArrayFunction.GetROI(SingleStatCalc.DataProcessor_UInt16.ImageData(NAXIS3).Data, ROICoord.Left, ROICoord.Left + ROICoord.Width, ROICoord.Top, ROICoord.Top + ROICoord.Height)
        If ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data.LongLength = 0 Then Exit Sub

        'Calculate statistics
        ZoomStatistics = ZoomStatCalc.ImageStatistics(AstroNET.Statistics.sStatistics.eDataMode.Fixed)
        Dim Report As New List(Of String)
        Report.Add("ROI X  : " & ROICoord.Left.ValRegIndep & " ... [" & ROICenter.X.ValRegIndep & "] ... " & (ROICoord.Left + ROICoord.Width).ValRegIndep)
        Report.Add("ROI Y  : " & ROICoord.Top.ValRegIndep & " ... [" & ROICenter.Y.ValRegIndep & "] ... " & (ROICoord.Top + ROICoord.Height).ValRegIndep)
        Report.Add("Min    : " & ZoomStatistics.MonoStatistics_Int.Min.Key.ValRegIndep)
        Report.Add("Max    : " & ZoomStatistics.MonoStatistics_Int.Max.Key.ValRegIndep)
        Report.Add("Mean   : " & ZoomStatistics.MonoStatistics_Int.Mean.ValRegIndep("0.0"))
        Report.Add("Median : " & ZoomStatistics.MonoStatistics_Int.Median.ValRegIndep)
        Report.Add("1%-PCT : " & ZoomStatistics.MonoStatistics_Int.GetPercentile(1).ValRegIndep)
        Report.Add("99%-PCT: " & ZoomStatistics.MonoStatistics_Int.GetPercentile(99).ValRegIndep)
        tbDetails.Text = String.Join(System.Environment.NewLine, Report)

        'Get the magnified version
        Dim ZoomInAsDisplayed(,) As UInt16 = ImgArrayFunction.RepeatePixel(ZoomStatCalc.DataProcessor_UInt16.ImageData(0).Data, Props.PixelPerRealPixel)

        ZoomInOutputImage = New cLockBitmap32Bit(ZoomInAsDisplayed.GetUpperBound(0) + 1, ZoomInAsDisplayed.GetUpperBound(1) + 1)
        ZoomInOutputImage.LockBits(False)
        Dim CopyPtr As Integer = 0
        For Y As Integer = 0 To ZoomInAsDisplayed.GetUpperBound(1)
            For X As Integer = 0 To ZoomInAsDisplayed.GetUpperBound(0)
                Dim PixelValue As UInt16 = ZoomInAsDisplayed(X, Y)
                ZoomInOutputImage.Pixels(CopyPtr) = LUT(PixelValue)
                CopyPtr += 1
            Next X
        Next Y
        ZoomInOutputImage.UnlockBits()
        pbZoomed.InterpolationMode = Props.ZoomInterpolation
        pbZoomed.Image = ZoomInOutputImage.BitmapToProcess

    End Sub

    Private Sub cms_SetCutOff_Click(sender As Object, e As EventArgs) Handles cms_SetCutOff.Click
        Props.MinCutOff_ADU = ZoomStatistics.MonoStatistics_Int.Min.Key
        Props.MaxCutOff_ADU = ZoomStatistics.MonoStatistics_Int.Max.Key
        DisplayCurrentProps()
        GenerateDisplayImage()
    End Sub

    '''<summary>Display the updated properties.</summary>
    Private Sub DisplayCurrentProps()
        pgMain.SelectedObject = Props
    End Sub

    Private Sub cms_SaveAsSeen_Click(sender As Object, e As EventArgs) Handles cms_SaveAsSeen.Click
        sfdMain.Filter = ImageFileFormatSpecific.SaveImageFileFilters
        If sfdMain.ShowDialog() <> DialogResult.OK Then Exit Sub
        ImageFileFormatSpecific.SaveImageFile(sfdMain.FileName, OutputImage.BitmapToProcess)
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

Public Class cImageDisplayProp

    Const Cat_Range As String = "1.) Range limitation"
    Const Cat_Color As String = "2.) Color conversion"
    Const Cat_Detail As String = "3.) Zoomed details"

    '''<summary>Values below this percentage value are special colored.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.1) Lower cut-off [%]")>
    <ComponentModel.Description("Values below this percentage value are special colored")>
    <ComponentModel.DefaultValue(0.0)>
    Public Property MinCutOff_pct As Double = 0.0

    '''<summary>Stop display color conversion at given percentage value.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.1) Upper cut-off [%]")>
    <ComponentModel.Description("Values above this percentage value are special colored")>
    <ComponentModel.DefaultValue(100.0)>
    Public Property MaxCutOff_pct As Double = 100.0

    '''<summary>Values below this percentage value are special colored.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.2) Lower cut-off [ADU]")>
    <ComponentModel.Description("Values below this ADU value are special colored")>
    <ComponentModel.DefaultValue(0)>
    Public Property MinCutOff_ADU As System.Int64 = System.Int64.MinValue

    '''<summary>Stop display color conversion at given percentage value.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.2) Upper cut-off [ADU]")>
    <ComponentModel.Description("Values above this ADU value are special colored")>
    <ComponentModel.DefaultValue(0)>
    Public Property MaxCutOff_ADU As System.Int64 = System.Int64.MaxValue

    '''<summary>Color to use if value is below display range.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.3) Lower cut-off color")>
    <ComponentModel.Description("Color for values below this percentage value")>
    Public Property MinCutOff_color As Color = Color.Blue

    '''<summary>Color to use if value is above display range.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.3) Upper cut-off color")>
    <ComponentModel.Description("Color for values above this percentage value")>
    Public Property MaxCutOff_color As Color = Color.Red

    '''<summary>Color to use if value is above display range.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.4) Use special cut-off color")>
    <ComponentModel.Description("TRUE to use special colors for cut-off, FALSE to use limit color")>
    <ComponentModel.DefaultValue(False)>
    Public Property CutOffSpecialColor As Boolean = False

    '''<summary>True to use full color range between min and max pct.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.5) Full range between cut-offs")>
    <ComponentModel.Description("True to use full color range within the cut-off range")>
    <ComponentModel.DefaultValue(True)>
    Public Property MinMaxPctRescale As Boolean = True

    '''<summary>Back color for non-image areas.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.6) Back color")>
    <ComponentModel.Description("Back color for non-image areas")>
    Public Property BackColor As Color = Color.DarkGray

    '''<summary>Step size for mouse-wheel change.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.7) Mouse wheel step size")>
    <ComponentModel.Description("Step size for mouse-wheel change")>
    <ComponentModel.DefaultValue(0.05)>
    Public Property PctStepSize As Double = 0.05

    '''<summary>Step size for mouse-wheel change.</summary>
    <ComponentModel.Category(Cat_Range)>
    <ComponentModel.DisplayName("1.8) Gamma")>
    <ComponentModel.Description("Gamma value")>
    <ComponentModel.DefaultValue(1.0)>
    Public Property Gamma As Double = 1.0

    '==========================================================================================================

    '''<summary>True to use full color range between min and max pct.</summary>
    <ComponentModel.Category(Cat_Color)>
    <ComponentModel.DisplayName("a) Color mode")>
    <ComponentModel.Description("Color conversion mode")>
    <ComponentModel.DefaultValue(cColorMaps.eMaps.Hot)>
    Public Property ColorMap As cColorMaps.eMaps = cColorMaps.eMaps.Hot

    '''<summary>Within range, display all values in in upper cut-off color.</summary>
    <ComponentModel.Category(Cat_Color)>
    <ComponentModel.DisplayName("b) Uni-color mode")>
    <ComponentModel.Description("Within range, display all values in Uni-color")>
    <ComponentModel.DefaultValue(False)>
    Public Property ColorUniMode As Boolean = False

    '''<summary>Within range, display all values in in upper cut-off color.</summary>
    <ComponentModel.Category(Cat_Color)>
    <ComponentModel.DisplayName("c) Uni-color")>
    <ComponentModel.Description("Uni-color for Uni-color mode")>
    <ComponentModel.DefaultValue(False)>
    Public Property ColorUni As Color = Color.Purple

    '==========================================================================================================

    '''<summary>Center of zoom - X.</summary>
    <ComponentModel.Category(Cat_Detail)>
    <ComponentModel.DisplayName("a) Center - X")>
    <ComponentModel.Description("Center of zoom - X")>
    <ComponentModel.DefaultValue(0)>
    Public Property Zoom_X As Integer = 0

    '''<summary>Center of zoom - Y.</summary>
    <ComponentModel.Category(Cat_Detail)>
    <ComponentModel.DisplayName("b) Center - Y")>
    <ComponentModel.Description("Center of zoom - Y")>
    <ComponentModel.DefaultValue(0)>
    Public Property Zoom_Y As Integer = 0

    '''<summary>Color to use if value is below display range.</summary>
    <ComponentModel.Category(Cat_Detail)>
    <ComponentModel.DisplayName("c) Zoom size [pixel]")>
    <ComponentModel.Description("Size of the zoomed area")>
    <ComponentModel.DefaultValue(20)>
    Public Property ZoomSize As Integer = 20

    '''<summary>Color to use if value is below display range.</summary>
    <ComponentModel.Category(Cat_Detail)>
    <ComponentModel.DisplayName("d) Pixel per real pixel")>
    <ComponentModel.Description("Size of the zoomed area")>
    <ComponentModel.DefaultValue(1)>
    Public Property PixelPerRealPixel As Integer = 1

    '''<summary>Color to use if value is below display range.</summary>
    <ComponentModel.Category(Cat_Detail)>
    <ComponentModel.DisplayName("e) Zoom interpolation")>
    <ComponentModel.Description("Interpolation mode of the zoomed area")>
    <ComponentModel.DefaultValue(4)>
    Public Property ZoomInterpolation As Drawing.Drawing2D.InterpolationMode = Drawing.Drawing2D.InterpolationMode.NearestNeighbor

End Class