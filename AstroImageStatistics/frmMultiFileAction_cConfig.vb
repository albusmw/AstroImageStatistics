Option Explicit On
Option Strict On

Partial Public Class frmMultiFileAction

    Public Class cConfig

        Private Const Cat_generic As String = "1.) Generic settings"
        Private Const Cat_processing As String = "2.) Processing"
        Private Const Cat_stack As String = "3.) Stacking and alignment"
        Private Const Cat_ROIDisplay As String = "4.) ROI display"
        Private Const Cat_statistics As String = "5.) Statistics"

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
        Public Property Gen_OutputFileUInt16 As String
            Get
                Return MyGen_OutputFileUInt16
            End Get
            Set(value As String)
                MyGen_OutputFileUInt16 = value
            End Set
        End Property
        Private MyGen_OutputFileUInt16 As String = "Stacked_UInt16.fits"

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("c) Output file - float32")>
        <ComponentModel.Description("Output file")>
        <ComponentModel.DefaultValue("Stacked_float32.fits")>
        Public Property Gen_OutputFilefloat32 As String
            Get
                Return MyGen_OutputFilefloat32
            End Get
            Set(value As String)
                MyGen_OutputFilefloat32 = value
            End Set
        End Property
        Private MyGen_OutputFilefloat32 As String = "Stacked_float32.fits"

        <ComponentModel.Category(Cat_generic)>
        <ComponentModel.DisplayName("d) Auto-open stack files")>
        <ComponentModel.Description("Open stacked file on finish?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stat_OpenStackedFile As Boolean
            Get
                Return MyStat_OpenStackedFile
            End Get
            Set(value As Boolean)
                MyStat_OpenStackedFile = value
            End Set
        End Property
        Private MyStat_OpenStackedFile As Boolean = True

        '=======================================================================================================

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("a) Calculate statistics")>
        <ComponentModel.Description("Calculate statistics for each file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Processing_CalcStatistics As Boolean
            Get
                Return MyProcessing_CalcStatistics
            End Get
            Set(value As Boolean)
                MyProcessing_CalcStatistics = value
            End Set
        End Property
        Private MyProcessing_CalcStatistics As Boolean = True

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("b) Calculate alignment")>
        <ComponentModel.Description("Calculate alignment for each file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Processing_CalcAlignment As Boolean
            Get
                Return MyProcessing_CalcAlignment
            End Get
            Set(value As Boolean)
                MyProcessing_CalcAlignment = value
            End Set
        End Property
        Private MyProcessing_CalcAlignment As Boolean = True

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("c) Calculate stacked file")>
        <ComponentModel.Description("Calculate stacked file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Processing_CalcStackedFile As Boolean
            Get
                Return MyProcessing_CalcStackedFile
            End Get
            Set(value As Boolean)
                MyProcessing_CalcStackedFile = value
            End Set
        End Property
        Private MyProcessing_CalcStackedFile As Boolean = True

        <ComponentModel.Category(Cat_processing)>
        <ComponentModel.DisplayName("d) Store aligned files")>
        <ComponentModel.Description("Store each file as new aligned file?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(False)>
        Public Property Processing_StoreAlignedFiles As Boolean
            Get
                Return MyProcessing_StoreAlignedFiles
            End Get
            Set(value As Boolean)
                MyProcessing_StoreAlignedFiles = value
            End Set
        End Property
        Private MyProcessing_StoreAlignedFiles As Boolean = False

        '=======================================================================================================

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("a) Run Bin2 on input data?")>
        <ComponentModel.Description("Run Bin2 with max removal?")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stack_RunBin2 As Boolean
            Get
                Return MyStack_RunBin2
            End Get
            Set(value As Boolean)
                MyStack_RunBin2 = value
            End Set
        End Property
        Private MyStack_RunBin2 As Boolean = True

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("b) XCorr segmentation")>
        <ComponentModel.Description("Segments per smaller axis for XCorr")>
        <ComponentModel.DefaultValue(4)>
        Public Property Stack_XCorrSegmentation As Integer
            Get
                Return MyStack_XCorrSegmentation
            End Get
            Set(value As Integer)
                MyStack_XCorrSegmentation = value
            End Set
        End Property
        Private MyStack_XCorrSegmentation As Integer = 4

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("c) XCorr tile reduction")>
        <ComponentModel.Description("Pixel to make tile smaller - equals to maximum shift")>
        <ComponentModel.DefaultValue(50)>
        Public Property Stack_TlpReduction As Integer
            Get
                Return MyStack_TlpReduction
            End Get
            Set(value As Integer)
                MyStack_TlpReduction = value
            End Set
        End Property
        Private MyStack_TlpReduction As Integer = 50

        <ComponentModel.Category(Cat_stack)>
        <ComponentModel.DisplayName("d) Shift margin")>
        <ComponentModel.Description("Shift margin for match all ROIs to 1 big image")>
        <ComponentModel.DefaultValue(20)>
        Public Property Stack_ShiftMargin As Integer
            Get
                Return MyStack_ShiftMargin
            End Get
            Set(value As Integer)
                MyStack_ShiftMargin = value
            End Set
        End Property
        Private MyStack_ShiftMargin As Integer = 20

        '=======================================================================================================

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("a) Base X")>
        <ComponentModel.Description("Base X")>
        <ComponentModel.DefaultValue(1000)>
        Public Property ROIDisplay_X As Integer
            Get
                Return MyROIDisplay_X
            End Get
            Set(value As Integer)
                MyROIDisplay_X = value
            End Set
        End Property
        Private MyROIDisplay_X As Integer = 1000

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("b) Base Y")>
        <ComponentModel.Description("Base Y")>
        <ComponentModel.DefaultValue(1000)>
        Public Property ROIDisplay_Y As Integer
            Get
                Return MyROIDisplay_Y
            End Get
            Set(value As Integer)
                MyROIDisplay_Y = value
            End Set
        End Property
        Private MyROIDisplay_Y As Integer = 1000

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("c) ROI display width")>
        <ComponentModel.Description("ROI display - width")>
        <ComponentModel.DefaultValue(200)>
        Public Property ROIDisplay_Width As Integer
            Get
                Return MyROIDisplay_Width
            End Get
            Set(value As Integer)
                MyROIDisplay_Width = value
            End Set
        End Property
        Private MyROIDisplay_Width As Integer = 200

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("d) ROI display height")>
        <ComponentModel.Description("ROI display - height")>
        <ComponentModel.DefaultValue(200)>
        Public Property ROIDisplay_Height As Integer
            Get
                Return MyROIDisplay_Height
            End Get
            Set(value As Integer)
                MyROIDisplay_Height = value
            End Set
        End Property
        Private MyROIDisplay_Height As Integer = 200

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("e) ROI display max mode")>
        <ComponentModel.Description("TRUE to get max of all ROI's, FALSE to get sum")>
        <ComponentModel.DefaultValue(True)>
        Public Property ROIDisplay_MaxMode As Boolean
            Get
                Return MyROIDisplay_MaxMode
            End Get
            Set(value As Boolean)
                MyROIDisplay_MaxMode = value
            End Set
        End Property
        Private MyROIDisplay_MaxMode As Boolean = True

        <ComponentModel.Category(Cat_ROIDisplay)>
        <ComponentModel.DisplayName("f) ROI shift mouse wheel steps")>
        <ComponentModel.Description("ROI shift mouse wheel steps")>
        <ComponentModel.DefaultValue(5)>
        Public Property Stack_ROIDisplay_MouseWheelSteps As Integer
            Get
                Return MyStack_ROIDisplay_MouseWheelSteps
            End Get
            Set(value As Integer)
                MyStack_ROIDisplay_MouseWheelSteps = value
            End Set
        End Property
        Private MyStack_ROIDisplay_MouseWheelSteps As Integer = 5

        '=======================================================================================================

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("a) Calculate histogram?")>
        <ComponentModel.Description("Calculate the total histogram")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        <ComponentModel.DefaultValue(True)>
        Public Property Stat_CalcHist As Boolean
            Get
                Return MyStat_CalcHist
            End Get
            Set(value As Boolean)
                MyStat_CalcHist = value
            End Set
        End Property
        Private MyStat_CalcHist As Boolean = True

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("b) Statistic image - max of all")>
        <ComponentModel.Description("Store a FITS file with the total max - leave blank to skip storing")>
        Public Property Stat_StatMaxFile As String
            Get
                Return MyStat_StatMaxFile
            End Get
            Set(value As String)
                MyStat_StatMaxFile = value
            End Set
        End Property
        Private MyStat_StatMaxFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_max.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("c) Statistic image - min of all")>
        <ComponentModel.Description("Store a FITS file with the total min - leave blank to skip storing")>
        Public Property Stat_StatMinFile As String
            Get
                Return MyStat_StatMinFile
            End Get
            Set(value As String)
                MyStat_StatMinFile = value
            End Set
        End Property
        Private MyStat_StatMinFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_min.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("d) Statistic image - mean of all")>
        <ComponentModel.Description("Store a FITS file with the total mean - leave blank to skip storing")>
        Public Property Stat_StatMeanFile As String
            Get
                Return MyStat_StatMeanFile
            End Get
            Set(value As String)
                MyStat_StatMeanFile = value
            End Set
        End Property
        Private MyStat_StatMeanFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_mean.fits")

        <ComponentModel.Category(Cat_statistics)>
        <ComponentModel.DisplayName("e) Statistic image - sigma of all")>
        <ComponentModel.Description("Store a FITS file with the total sigma - leave blank to skip storing")>
        Public Property Stat_StatSigmaFile As String
            Get
                Return MyStat_StatSigmaFile
            End Get
            Set(value As String)
                MyStat_StatSigmaFile = value
            End Set
        End Property
        Private MyStat_StatSigmaFile As String = IO.Path.Combine(AIS.DB.MyPath, "Stack_sigma.fits")

    End Class

End Class
