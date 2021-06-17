Option Explicit On
Option Strict On

Public Class cDB

    '''<summary>Handle to Intel IPP functions.</summary>
    Public IPP As cIntelIPP

    Private Const Cat_load As String = "1.) Loading"
    Private Const Cat_analysis As String = "2.) Analysis"
    Private Const Cat_plot As String = "3.) Plotting"
    Private Const Cat_Proc_Vignette As String = "4.) Processing - vignette"
    Private Const Cat_log As String = "5.) Logging"
    Private Const Cat_save As String = "6.) Saving"
    Private Const Cat_misc As String = "9.) Misc"

    '''<summary>Location of the EXE.</summary>
    <ComponentModel.Browsable(False)>
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    '''<summary>Location of the EXE.</summary>
    <ComponentModel.Browsable(False)>
    Public ReadOnly Property LastFiles As String = System.IO.Path.Combine(MyPath, "LastOpened.txt")

    <ComponentModel.Category(Cat_load)>
    <ComponentModel.DisplayName("a) Use IPP?")>
    <ComponentModel.Description("Use the Intel IPP for loading and other steps (recommended - speed-up)")>
    <ComponentModel.DefaultValue(True)>
    Public Property UseIPP As Boolean = True

    <ComponentModel.Category(Cat_load)>
    <ComponentModel.DisplayName("b) Force direct read-in")>
    <ComponentModel.Description("Do not apply BZERO or BSCALE - this may help on problems with incorrect scaling coefficients")>
    <ComponentModel.DefaultValue(False)>
    Public Property ForceDirect As Boolean = False

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("a) Mono statistics")>
    <ComponentModel.Description("Calculate the mono statistics (can be of interest if e.g. color balance is applied to a mono image which would be wrong ...)")>
    <ComponentModel.DefaultValue(True)>
    Public Property MonoStatistics As Boolean = True

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("b) Bayer statistics")>
    <ComponentModel.Description("Calculate the bayer statistics (can be of interest if e.g. color balance is applied to a mono image which would be wrong ...)")>
    <ComponentModel.DefaultValue(True)>
    Public Property BayerStatistics As Boolean = True

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("c) Stacking")>
    <ComponentModel.DefaultValue(False)>
    Public Property Stacking As Boolean = False

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("d) PlateSolve2 Path")>
    <ComponentModel.DefaultValue("C:\Bin\PlateSolve2\PlateSolve2.exe")>
    Public Property PlateSolve2Path As String = "C:\Bin\PlateSolve2\PlateSolve2.exe"

    <ComponentModel.Category(Cat_analysis)>
    <ComponentModel.DisplayName("e) PlateSolve2 hold open time")>
    <ComponentModel.DefaultValue(0)>
    Public Property PlateSolve2HoldOpen As Integer = 0

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_plot)>
    <ComponentModel.DisplayName("a) Auto-open graph")>
    <ComponentModel.DefaultValue(True)>
    Public Property AutoOpenStatGraph As Boolean = True

    <ComponentModel.Category(Cat_plot)>
    <ComponentModel.DisplayName("b) Plot style")>
    Public Property PlotStyle As cZEDGraphService.eCurveMode = cZEDGraphService.eCurveMode.LinesAndPoints

    <ComponentModel.Category(Cat_plot)>
    <ComponentModel.DisplayName("c) Stack graphs below form")>
    <ComponentModel.Description("Position graphs below the main window (exact overlay of different graph windows)")>
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
    <ComponentModel.DefaultValue(True)>
    Public Property AutoClearLog As Boolean = True

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_save)>
    <ComponentModel.DisplayName("a) Image quality")>
    <ComponentModel.Description("Image quality parameter to use")>
    <ComponentModel.DefaultValue(80L)>
    Public Property ImageQuality As Int64 = 80L

    '===================================================================================================================================================

    <ComponentModel.Category(Cat_misc)>
    <ComponentModel.DisplayName("a) Bayer pattern")>
    <ComponentModel.Description("Bayer pattern - 1 character for each channel")>
    <ComponentModel.DefaultValue("RGGB")>
    Public Property BayerPattern As String = "RGGB"

    <ComponentModel.Category(Cat_misc)>
    <ComponentModel.DisplayName("b) Used IPP path")>
    Public ReadOnly Property IPPPath As String
        Get
            Return MyIPPPath
        End Get
    End Property
    Public MyIPPPath As String = String.Empty

    '''<summary>Get all (existing) files last loaded.</summary>
    Public Function GetRecentFiles() As List(Of String)
        Dim RetVal As New List(Of String)
        If System.IO.File.Exists(LastFiles) Then
            For Each Line As String In System.IO.File.ReadAllLines(LastFiles)
                If System.IO.File.Exists(Line) Then RetVal.Add(Line)
            Next Line
        End If
        Return RetVal
    End Function

    '''<summary>Get all (existing) files last loaded.</summary>
    Public Sub StoreRecentFile(ByVal NewFile As String)
        Dim FileContent As New List(Of String)
        If System.IO.File.Exists(LastFiles) Then
            For Each Line As String In System.IO.File.ReadAllLines(LastFiles)
                FileContent.Add(Line)
                If System.IO.Path.GetFullPath(Line) = System.IO.Path.GetFullPath(NewFile) Then
                    Exit Sub                'file already in the list ...
                End If
            Next Line
        End If
        FileContent.Insert(0, NewFile)         'insert new file at top
        System.IO.File.WriteAllLines(LastFiles, FileContent.ToArray)
    End Sub

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
