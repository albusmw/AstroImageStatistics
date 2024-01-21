Option Explicit On
Option Strict On
Imports System.ComponentModel
Imports AstroImageStatistics.cColorMaps

Partial Public Class frmMultiFileAction

    Public Class cConfig

        Private Const Cat_generic As String = "1.) Generic settings"
        Private Const Cat_processing As String = "2.) Processing steps"
        Private Const Cat_alignment As String = "3.) Alignment"
        Private Const Cat_CutLimitation As String = "4.) Cut limitations"
        Private Const Cat_stack As String = "5.) Stacking"
        Private Const Cat_ROIDisplay As String = "6.) ROI display"
        Private Const Cat_statistics As String = "7.) Statistics"

        Public Enum eROIDisplay
            <Description("Off")>
            Off
            <Description("Selected file only")>
            [Single]
            <Description("All files - stacked - mean per pixel")>
            Stacked_mean
            <Description("All files - stacked - max per pixel")>
            Stacked_max
            <Description("All files - side-by-side")>
            Mosaik
        End Enum

        '=======================================================================================================

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("a) Root working folder")>
        <ComponentModel.Description("Root working folder")>
        Public Property Gen_root As String
            Get
                Return MyGen_root
            End Get
            Set(value As String)
                MyGen_root = value
            End Set
        End Property
        Private MyGen_root As String = AIS.DB.MyPath

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("b) Output file - UInt16")>
        <ComponentModel.Description("Output file")>
        <ComponentModel.DefaultValue("Stacked_UInt16.fits")>
        Public Property Gen_OutputFileUInt16 As String = "Stacked_UInt16.fits"

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("c) Output file - float32")>
        <ComponentModel.Description("Output file")>
        <ComponentModel.DefaultValue("Stacked_float32.fits")>
        Public Property Gen_OutputFilefloat32 As String = "Stacked_float32.fits"

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("d) Output file - sigma-clipped")>
        <ComponentModel.Description("Output file")>
        <ComponentModel.DefaultValue("Stacked_SigmaClip.fits")>
        Public Property Gen_OutputFileSigmaClip As String = "Stacked_SigmaClip.fits"

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("e) Auto-open stack files")>
        <ComponentModel.Description("Open stacked file on finish?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stat_OpenStackedFile As Boolean = True

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Processing steps
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("a) Calculate statistics")>
        <ComponentModel.Description("Calculate statistics for each file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Processing_CalcStatistics As Boolean = True

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("b) Calculate alignment")>
        <ComponentModel.Description("Calculate alignment for each file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Processing_CalcAlignment As Boolean = True

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("c) Calculate stacked file")>
        <ComponentModel.Description("Calculate stacked file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Processing_CalcStackedFile As Boolean = True

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("d) Calculate sigmaclip stack")>
        <ComponentModel.Description("Calculate sigmaclip stacked file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(False)>
        Public Property Processing_CalcSigmaClip As Boolean = False

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("e) Store individual aligned files")>
        <ComponentModel.Description("Store each individual file as new aligned file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(False)>
        Public Property Processing_StoreAlignedFiles As Boolean = False

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("f) Display FITS header")>
        <ComponentModel.Description("Display FITS header on new file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(False)>
        Public Property Processing_DisplayFITSHeader As Boolean = False

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Alignment
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        <ComponentModel.Category(Cat_alignment)>
        <ComponentModel.DisplayName("a) Run Bin2 on input data?")>
        <ComponentModel.Description("Run Bin2 with max removal?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stack_RunBin2 As Boolean = True

        <ComponentModel.Category(Cat_alignment)>
        <ComponentModel.DisplayName("b) XCorr segmentation")>
        <ComponentModel.Description("Segments per smaller axis for XCorr")>
        <ComponentModel.DefaultValue(4)>
        Public Property Stack_XCorrSegmentation As Integer = 4

        <ComponentModel.Category(Cat_alignment)>
        <ComponentModel.DisplayName("c) XCorr tile reduction")>
        <ComponentModel.Description("Pixel to make tile smaller - equals to maximum shift")>
        <ComponentModel.DefaultValue(50)>
        Public Property Stack_TlpReduction As Integer = 50

        <ComponentModel.Category(Cat_alignment)>
        <ComponentModel.DisplayName("d) Shift margin")>
        <ComponentModel.Description("Shift margin for match all ROIs to 1 big image")>
        <ComponentModel.DefaultValue(20)>
        Public Property Stack_ShiftMargin As Integer = 20

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Cut range limitations
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        <ComponentModel.Category(Cat_CutLimitation)>
        <ComponentModel.DisplayName("a) Left limit")>
        <ComponentModel.Description("Left limit")>
        <ComponentModel.DefaultValue(0)>
        Public Property CutLimit_Left As Integer = 0

        <ComponentModel.Category(Cat_CutLimitation)>
        <ComponentModel.DisplayName("b) Right limit")>
        <ComponentModel.Description("Right limit")>
        <ComponentModel.DefaultValue(100000)>
        Public Property CutLimit_Right As Integer = 100000

        <ComponentModel.Category(Cat_CutLimitation)>
        <ComponentModel.DisplayName("c) Top limit")>
        <ComponentModel.Description("Top limit")>
        <ComponentModel.DefaultValue(0)>
        Public Property CutLimit_Top As Integer = 0

        <ComponentModel.Category(Cat_CutLimitation)>
        <ComponentModel.DisplayName("d) Bottom limit")>
        <ComponentModel.Description("Bottom limit")>
        <ComponentModel.DefaultValue(100000)>
        Public Property CutLimit_Bottom As Integer = 100000

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Stacking
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("a) Sigma delta - low bound")>
        <ComponentModel.Description("Values below (mean - <this value>*sigma) are ignored")>
        <ComponentModel.DefaultValue(3.0)>
        Public Property Stack_SigClip_LowBound As Double = 2.0

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("b) Sigma delta - high bound")>
        <ComponentModel.Description("Values above (mean + <this value>*sigma) are ignored")>
        <ComponentModel.DefaultValue(3.0)>
        Public Property Stack_SigClip_HighBound As Double = 2.0

        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' ROI display
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("a) Mode")>
        <ComponentModel.Description("Display the (combined) ROI; deactivate for more performance")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
        <ComponentModel.DefaultValue(eROIDisplay.Off)>
        Public Property ROIDisplay_Mode As eROIDisplay = eROIDisplay.Off

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("b) Use delta XY")>
        <ComponentModel.Description("TRUE to use delta X and Y for ROI correction")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property ROIDisplay_UseDeltaXY As Boolean = True

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("c) Color mode")>
        <ComponentModel.Description("Color mode")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
        <ComponentModel.DefaultValue(eMaps.Hot)>
        Public Property Stack_ROIDisplay_ColorMode As eMaps = eMaps.FalseColor

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("d) Mosaik border width")>
        <ComponentModel.Description("Width [pixel] of the ROI image border between the tiles")>
        <ComponentModel.DefaultValue(3)>
        Public Property ROIDisplay_MosaikBorderWidth As Integer = 3

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("e) Base X")>
        <ComponentModel.Description("Base X")>
        <ComponentModel.DefaultValue(1000)>
        Public Property ROIDisplay_X As Integer = 1000

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("f) Base Y")>
        <ComponentModel.Description("Base Y")>
        <ComponentModel.DefaultValue(1000)>
        Public Property ROIDisplay_Y As Integer = 1000

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("g) ROI display width")>
        <ComponentModel.Description("ROI display - width")>
        <ComponentModel.DefaultValue(200)>
        Public Property ROIDisplay_Width As Integer = 200

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("h) ROI display height")>
        <ComponentModel.Description("ROI display - height")>
        <ComponentModel.DefaultValue(200)>
        Public Property ROIDisplay_Height As Integer = 200

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("i) ROI position and size mouse wheel steps")>
        <ComponentModel.Description("ROI position and size mouse wheel steps")>
        <ComponentModel.DefaultValue(5)>
        Public Property ROIDisplay_PositionAndSize As Integer = 5

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("j) Table DeltaXY mouse wheel step size")>
        <ComponentModel.Description("Table DeltaXY mouse wheel step size")>
        <ComponentModel.DefaultValue(1)>
        Public Property ROIDisplay_DeltaXYStep As Integer = 1



        '˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭˭
        ' Statistics
        '‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗‗

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("a) Calculate histogram?")>
        <ComponentModel.Description("Calculate the total histogram")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stat_CalcHist As Boolean = True

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("b) Statistic image - max of all")>
        <ComponentModel.Description("Store a FITS file with the total max - leave blank to skip storing")>
        Public Property Stat_StatMaxFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_max.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("c) Statistic image - min of all")>
        <ComponentModel.Description("Store a FITS file with the total min - leave blank to skip storing")>
        Public Property Stat_StatMinFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_min.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("d) Statistic image - mean of all")>
        <ComponentModel.Description("Store a FITS file with the total mean - leave blank to skip storing")>
        Public Property Stat_StatMeanFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_mean.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("e) Statistic image - sigma of all")>
        <ComponentModel.Description("Store a FITS file with the total sigma - leave blank to skip storing")>
        Public Property Stat_StatSigmaFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_sigma.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("f) Single pixel statistics - X")>
        <ComponentModel.Description("X coordinate of the single statistics display")>
        Public Property Stat_SinglePixelX As Integer = 0

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("g) Single pixel statistics - Y")>
        <ComponentModel.Description("Y coordinate of the single statistics display")>
        Public Property Stat_SinglePixelY As Integer = 0

    End Class

End Class
