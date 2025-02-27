Option Explicit On
Option Strict On
Imports Microsoft.VisualBasic.Logging

Public Class AIS

    Public Shared DB As New cDB
    Public Shared Config As New cConfig

    '''<summary>Get all (existing) files last loaded.</summary>
    Public Shared Function GetRecentFiles() As List(Of String)
        Dim FileNamesFile As String = System.IO.Path.Combine(DB.MyPath, Config.LastFiles)
        Dim RetVal As New List(Of String)
        If System.IO.File.Exists(FileNamesFile) Then
            For Each Line As String In System.IO.File.ReadAllLines(FileNamesFile)
                If System.IO.File.Exists(Line) Then RetVal.Add(Line)
            Next Line
        End If
        Return RetVal
    End Function

    '''<summary>Get all (existing) files last loaded.</summary>
    Public Shared Sub StoreRecentFile(ByVal NewFile As String)
        Dim FileNamesFile As String = System.IO.Path.Combine(DB.MyPath, Config.LastFiles)
        Dim FileContent As New List(Of String)
        If System.IO.File.Exists(FileNamesFile) Then
            For Each Line As String In System.IO.File.ReadAllLines(FileNamesFile)
                FileContent.Add(Line)
                If System.IO.Path.GetFullPath(Line) = System.IO.Path.GetFullPath(NewFile) Then
                    Exit Sub                'file already in the list ...
                End If
            Next Line
        End If
        FileContent.Insert(0, NewFile)         'insert new file at top
        System.IO.File.WriteAllLines(FileNamesFile, FileContent.ToArray)
    End Sub

    Public Shared Function OpenFile(ByVal FileToOpen As String) As String
        Try
            If System.IO.File.Exists(FileToOpen) Then
                If String.IsNullOrEmpty(AIS.Config.FITSViewer) = True Then
                    Utils.StartWithItsEXE(FileToOpen)
                Else
                    Process.Start(AIS.Config.FITSViewer, Chr(34) & FileToOpen & Chr(34))
                End If
            End If
            Return String.Empty
        Catch ex As Exception
            Return "Error opening <" & FileToOpen & ">: <" & ex.Message & ">"
        End Try
    End Function

End Class

'''<summary>Properties of one loaded files.</summary>
Public Class cFileProps

    '''<summary>Process this file.</summary>
    <System.ComponentModel.DisplayName("Process")>
    Public Property Process As Boolean = True

    '''<summary>File name.</summary>
    <System.ComponentModel.DisplayName("File name")>
    Public ReadOnly Property FileName() As String = String.Empty

    '''<summary>DateTime from the FITS header info.</summary>
    <System.ComponentModel.DisplayName("Date & Time")>
    Public ReadOnly Property DateTime() As String
        Get
            If IsNothing(FITSHeader) Then Return String.Empty
            If FITSHeader.ContainsKey(eFITSKeywords.DATE_OBS) = False Then Return String.Empty
            Dim RetVal As String = CType(FITSHeader(eFITSKeywords.DATE_OBS), String)
            If FITSHeader.ContainsKey(eFITSKeywords.TIME_OBS) = True Then RetVal &= "T" & CType(FITSHeader(eFITSKeywords.TIME_OBS), String)
            Return RetVal
        End Get
    End Property


    '''<summary>Width from the FITS header info.</summary>
    <System.ComponentModel.DisplayName("NAXIS1")>
    Public ReadOnly Property NAXIS1() As Integer
        Get
            If IsNothing(FITSHeader) Then Return -1
            If FITSHeader.ContainsKey(eFITSKeywords.NAXIS1) = False Then Return -1
            Return CType(FITSHeader(eFITSKeywords.NAXIS1), Integer)
        End Get
    End Property

    '''<summary>Width from the FITS header info.</summary>
    <System.ComponentModel.DisplayName("NAXIS2")>
    Public ReadOnly Property NAXIS2() As Integer
        Get
            If IsNothing(FITSHeader) Then Return -1
            If FITSHeader.ContainsKey(eFITSKeywords.NAXIS2) = False Then Return -1
            Return CType(FITSHeader(eFITSKeywords.NAXIS2), Integer)
        End Get
    End Property

    '''<summary>Entered / read DeltaX.</summary>
    <System.ComponentModel.DisplayName("Delta-X")>
    Public Property DeltaX As Double = 0

    '''<summary>Entered / read DeltaY.</summary>
    <System.ComponentModel.DisplayName("Delta-Y")>
    Public Property DeltaY As Double = 0

    <System.ComponentModel.DisplayName("Min")>
    Public ReadOnly Property Min() As Double
        Get
            If IsNothing(Statistics.MonoStatistics_Int) Then Return Double.NaN
            Return Statistics.MonoStatistics_Int.Min.Key
        End Get
    End Property

    <System.ComponentModel.DisplayName("Max")>
    Public ReadOnly Property Max() As Double
        Get
            If IsNothing(Statistics.MonoStatistics_Int) Then Return Double.NaN
            Return Statistics.MonoStatistics_Int.Max.Key
        End Get
    End Property

    <System.ComponentModel.DisplayName("Mean")>
    Public ReadOnly Property Mean() As Double
        Get
            If IsNothing(Statistics.MonoStatistics_Int) Then Return Double.NaN
            Return Statistics.MonoStatistics_Int.Mean
        End Get
    End Property

    <System.ComponentModel.DisplayName("Median")>
    Public ReadOnly Property Median() As Int64
        Get
            If IsNothing(Statistics.MonoStatistics_Int) Then Return 0
            Return Statistics.MonoStatistics_Int.Median
        End Get
    End Property

    '''<summary>Number of stars detected by DSS.</summary>
    <System.ComponentModel.DisplayName("Stars")>
    Public Property NrStars As Integer = 0

    '''<summary>Quality detected by DSS.</summary>
    <System.ComponentModel.DisplayName("Quality")>
    Public Property OverallQuality As Double = Double.NaN

    '''<summary>Sky background detected by DSS.</summary>
    <System.ComponentModel.DisplayName("Sky background")>
    Public Property SkyBackground As Double = Double.NaN

    '═════════════════════════════════════════════════════════════════════════════
    ' Internal properties
    '═════════════════════════════════════════════════════════════════════════════

    Public Sub New(ByVal NewFileName As String)
        Me.FileName = NewFileName
        Using BaseIn As New System.IO.StreamReader(NewFileName)
            Dim FITSHeaderParser As New cFITSHeaderParser(cFITSReader.ReadHeader(BaseIn, DataStartPosition))
        End Using
    End Sub

    '''<summary>Loaded FITS header entries.</summary>
    Public FITSHeader As Dictionary(Of eFITSKeywords, Object)

    '''<summary>Calculated statistics values.</summary>
    Public Statistics As AstroNET.Statistics.sStatistics

    '''<summary>Data start position within the file.</summary>
    <System.ComponentModel.Browsable(False)>
    Public Property DataStartPosition As Integer = -1

End Class

'''<summary>Detailed evaluation reults.</summary>
Public Structure sFileEvalResults
    '''<summary>Statistics for pixel with identical Y value.</summary>
    Dim StatPerRow() As Ato.cSingleValueStatistics
    '''<summary>Statistics for pixel with identical X value.</summary>
    Dim StatPerCol() As Ato.cSingleValueStatistics
    '''<summary>Raw vignette.</summary>
    Public Vig_RawData As Dictionary(Of Double, Double)
    '''<summary>Reduced and binned vignette.</summary>
    Public Vig_BinUsedData As Dictionary(Of Double, Double)
    '''<summary>Fitted vignette.</summary>
    Public Vig_Fitting() As Double
End Structure

Public Class cDB

    '''<summary>All open plots.</summary>
    Public AllPlots As New List(Of cZEDGraphForm)

    '''<summary>Last file opened.</summary>
    Public LastFile_Name As String = String.Empty
    '''<summary>Last file opened -  FITS header.</summary>
    Public LastFile_FITSHeader As cFITSHeaderParser
    '''<summary>Last file opened - statistics processor.</summary>
    Public LastFile_Data As AstroNET.Statistics
    '''<summary>Last file opened - statistics.</summary>
    Public LastFile_Statistics As AstroNET.Statistics.sStatistics
    '''<summary>Last file opened - detailed evaluation reults.</summary>
    Public LastFile_EvalResults As sFileEvalResults

    Public Structure sSpecialPoint
        Public Coord As System.Drawing.Point
        Public Value1 As Double
        Public Value2 As Double
        Public Sub New(ByVal X As Integer, ByVal Y As Integer, ByVal NewValue1 As Double, ByVal NewValue2 As Double)
            Coord.X = X
            Coord.Y = Y
            Value1 = NewValue1
            Value2 = NewValue2
        End Sub
    End Structure

    Public Stars As New List(Of sSpecialPoint)

    '''<summary>Handle to Intel IPP functions.</summary>
    Public IPP As cIntelIPP

    Public ReadOnly Property IPPPath As String
        Get
            Return IPP.IPPPath
        End Get
    End Property

    '''<summary>Location of the EXE.</summary>
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

End Class

Public Class cConfig

    Private Const Cat_load As String = "1.) Loading"
    Private Const Cat_analysis As String = "2.) Analysis"
    Private Const Cat_plot As String = "3.) Plotting"
    Private Const Cat_Proc_Vignette As String = "4.) Processing - vignette"
    Private Const Cat_log As String = "5.) Logging"
    Private Const Cat_misc As String = "9.) Misc"

    <ComponentModel.Category(Cat_load)>
    <ComponentModel.DisplayName("a) Use IPP?")>
    <ComponentModel.Description("Use the Intel IPP for loading and other steps (recommended - speed-up)")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property UseIPP As Boolean = True

    <ComponentModel.Category(Cat_load)>
    <ComponentModel.DisplayName("b) Force direct read-in")>
    <ComponentModel.Description("Do not apply BZERO or BSCALE - this may help on problems with incorrect scaling coefficients")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(False)>
    Public Property ForceDirect As Boolean = False

    '''<summary>File to hold last opened files.</summary>
    <ComponentModel.Category(Cat_load)>
    <ComponentModel.DisplayName("c) Last opened files")>
    <ComponentModel.Description("File to hold last opened files.")>
    Public ReadOnly Property LastFiles As String = "LastOpened.txt"

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("a) Mono statistics")>
    <ComponentModel.Description("Calculate the mono statistics (can be of interest if e.g. color balance is applied to a mono image which would be wrong ...)")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property CalcStat_Mono As Boolean = True

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("b) Bayer statistics")>
    <ComponentModel.Description("Calculate the bayer statistics (can be of interest if e.g. color balance is applied to a mono image which would be wrong ...)")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property CalcStat_Bayer As Boolean = True

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("c) PlateSolve2 Path")>
    <ComponentModel.DefaultValue("C:\Bin\PlateSolve2\PlateSolve2.exe")>
    Public Property PlateSolve2Path As String = "C:\Bin\PlateSolve2\PlateSolve2.exe"

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("d) PlateSolve2 hold open time")>
    <ComponentModel.DefaultValue(0)>
    Public Property PlateSolve2HoldOpen As Integer = 0

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_plot)>
    <ComponentModel.DisplayName("a) Auto-open graph")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property AutoOpenStatGraph As Boolean = True

    <ComponentModel.Category(Cat_plot)>
    <ComponentModel.DisplayName("b) Plot style")>
    Public Property PlotStyle As cZEDGraph.eCurveMode = cZEDGraph.eCurveMode.LinesAndPoints

    <ComponentModel.Category(Cat_plot)>
    <ComponentModel.DisplayName("c) Stack graphs below form")>
    <ComponentModel.Description("Position graphs below the main window (exact overlay of different graph windows)")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(False)>
    Public Property StackGraphs As Boolean = False

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_Proc_Vignette)>
    <ComponentModel.DisplayName("a) Vignette calculation bins")>
    <ComponentModel.Description("Number of bins for the vignette calculation - use 0 for infinit (no bin) resolution")>
    <ComponentModel.DefaultValue(1000)>
    Public Property VigCalcBins As Integer = 1000

    <ComponentModel.Category(Cat_Proc_Vignette)>
    <ComponentModel.DisplayName("b) Vignette polynomial order")>
    <ComponentModel.Description("Order of the fitting vignette")>
    <ComponentModel.DefaultValue(19)>
    Public Property VigPolyOrder As Integer = 19

    <ComponentModel.Category(Cat_Proc_Vignette)>
    <ComponentModel.DisplayName("c) Vignette correction start distance")>
    <ComponentModel.Description("Distance below are ignored for correction")>
    <ComponentModel.DefaultValue(0)>
    Public Property VigStartDistance As Integer = 0

    <ComponentModel.Category(Cat_Proc_Vignette)>
    <ComponentModel.DisplayName("d) Vignette correction stop distance")>
    <ComponentModel.Description("Distance below are ignored for correction")>
    <ComponentModel.DefaultValue(10000)>
    Public Property VigStopDistance As Integer = 10000

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_log)>
    <ComponentModel.DisplayName("a) Clean log on any analysis?")>
    <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
    <ComponentModel.DefaultValue(True)>
    Public Property AutoClearLog As Boolean = True

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_misc)>
    <ComponentModel.DisplayName("a) Bayer pattern")>
    <ComponentModel.Description("Bayer pattern - 1 character for each channel")>
    <ComponentModel.DefaultValue("RGGB")>
    Public Property BayerPattern As String = "RGGB"

    <ComponentModel.Category(Cat_misc)>
    <ComponentModel.DisplayName("b) FITS viewer")>
    <ComponentModel.Description("Viewer for stored FITS files - delete to use default viewer")>
    Public Property FITSViewer As String = String.Empty

    '''<summary>Get the channel name of the bayer pattern index.</summary>
    '''<param name="Idx">0-based index.</param>
    '''<returns>Channel name - if there are more channels with the same letter a number is added beginning with the 2nd channel.</returns>
    Public Function BayerPatternName(ByVal PatIdx As Integer) As String
        If PatIdx > BayerPattern.Length - 1 Then Return "?"
        Dim Dict As New Dictionary(Of String, Integer)
        Dim ColorName As String = String.Empty
        For Idx As Integer = 0 To PatIdx
            ColorName = BayerPattern.Substring(Idx, 1)
            If Dict.ContainsKey(ColorName) = False Then
                Dict.Add(ColorName, 0)
            Else
                Dict(ColorName) += 1
            End If
        Next Idx
        If Dict(ColorName) > 0 Then
            Return ColorName & Dict(ColorName).ValRegIndep
        Else
            Return ColorName
        End If
    End Function

    '''<summary>Get the channel name of all bayer pattern index.</summary>
    '''<param name="Idx">0-based index.</param>
    '''<returns>Channel name.</returns>
    Public Function BayerPatternNames() As List(Of String)
        Dim RetVal As New List(Of String)
        For Idx As Integer = 0 To BayerPattern.Length - 1
            RetVal.Add(BayerPatternName(Idx))
        Next Idx
        Return RetVal
    End Function

End Class
