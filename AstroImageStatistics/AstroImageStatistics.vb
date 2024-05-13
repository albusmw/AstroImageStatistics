Option Explicit On
Option Strict On

'''<summary>Function for astro imaging,</summary>
Public Class AstroImageStatistics_Fun

    '''<summary>Plate solve the passed file,</summary>
    '''<returns>Error code; empty string on no error.</returns>
    <Runtime.Versioning.SupportedOSPlatform("windows")>
    Public Shared Function PlateSolve(ByVal FileToRun As String, ByVal PlateSolve2Path As String, ByVal PlateSolve2HoldOpen As Integer, ByRef SolverLog As String()) As String

        Dim Solver As New cPlateSolve
        Dim Binning As Integer = 1

        'Preconditions
        If System.IO.File.Exists(FileToRun) = False Then Return "Input file <" & FileToRun & "> not found."
        If System.IO.File.Exists(PlateSolve2Path) = False Then Return "PlateSolve2 executable <" & PlateSolve2Path & "> not found."

        'Get the FITS header information
        Dim DataStartPos As Integer = -1
        Dim FITSHeader As List(Of cFITSHeaderParser.sHeaderElement) = cFITSHeaderChanger.ParseHeader(FileToRun, DataStartPos)
        Dim File_RA_JNow_string As String = Nothing
        Dim File_Dec_JNow_string As String = Nothing
        Dim File_FOV1 As Object = Nothing
        Dim File_FOV2 As Object = Nothing
        For Each Entry As cFITSHeaderParser.sHeaderElement In FITSHeader
            If Entry.Keyword = eFITSKeywords.RA Or Entry.Keyword = eFITSKeywords.RA_NOM Then File_RA_JNow_string = CStr(Entry.Value).Trim("'"c).Trim.Trim("'"c)
            If Entry.Keyword = eFITSKeywords.DEC Or Entry.Keyword = eFITSKeywords.DEC_NOM Then File_Dec_JNow_string = CStr(Entry.Value).Trim("'"c).Trim.Trim("'"c)
            If Entry.Keyword = eFITSKeywords.FOV1 Then File_FOV1 = Entry.Value
            If Entry.Keyword = eFITSKeywords.FOV2 Then File_FOV2 = Entry.Value
        Next Entry

        'Exit on no data
        If IsNothing(File_RA_JNow_string) Or IsNothing(File_Dec_JNow_string) Then Return "No RA and/or DEC specified in input file."

        'Data from QHYCapture (10Micron) are in JNow, so convert to J2000 for PlateSolve
        Dim File_RA_JNow As Double = File_RA_JNow_string.ParseRA
        Dim File_Dec_JNow As Double = File_Dec_JNow_string.ParseDegree
        Dim File_RA_J2000 As Double = Double.NaN
        Dim File_Dec_J2000 As Double = Double.NaN
        ASCOMDynamic.JNowToJ2000(File_RA_JNow, File_Dec_JNow, Now, File_RA_J2000, File_Dec_J2000)

        'Run plate solve
        Dim ErrorCode1 As String = String.Empty
        cPlateSolve.PlateSolvePath = PlateSolve2Path
        With Solver
            .SetRA(File_RA_J2000)                                                                       'theoretical position (Wikipedia, J2000.0)
            .SetDec(File_Dec_J2000)                                                                     'theoretical position (Wikipedia, J2000.0)
            .SetDimX(Val(File_FOV1) * cPlateSolve.DegToMin)                                             'constant for system [telescope-camera]
            .SetDimY(Val(File_FOV2) * cPlateSolve.DegToMin)                                             'constant for system [telescope-camera]
            .HoldOpenTime = PlateSolve2HoldOpen
            Dim RawOut As String() = {}
            ErrorCode1 = .Solve(FileToRun, RawOut)
        End With

        'Convert
        Dim RadToH As Double = 12 / Math.PI
        Dim RadToGrad As Double = (180 / Math.PI)
        Dim JNow_RA_solved As Double = Double.NaN
        Dim JNow_Dec_solved As Double = Double.NaN
        ASCOMDynamic.J2000ToJNow(Solver.SolvedRA * RadToH, Solver.SolvedDec * RadToGrad, Now, JNow_RA_solved, JNow_Dec_solved)

        Dim Output As New List(Of String)
        Output.Add("Start with        RA <" & File_RA_JNow.ToHMS & ">, DEC <" & File_Dec_JNow.ToDegMinSec & "> (JNow file string)")
        Output.Add("                  RA <" & File_RA_J2000.ToHMS & ">, DEC <" & File_Dec_J2000.ToDegMinSec & "> (J2000)")
        Output.Add("Solved as       : RA <" & (Solver.SolvedRA * RadToH).ToHMS & ">, DEC <" & (Solver.SolvedDec * RadToGrad).ToDegMinSec & "> (J2000)")
        Output.Add("                  RA <" & JNow_RA_solved.ToHMS & ">, DEC <" & JNow_Dec_solved.ToDegMinSec & "> (JNow)")
        Output.Add("Error           :  RA <" & Solver.ErrorRA.ValRegIndep & " "">, DEC < " & Solver.ErrorDec.ValRegIndep & " "">")
        Output.Add("Error results   :  RA <" & Solver.ErrorRA.ValRegIndep & " "">, DEC < " & Solver.ErrorDec.ValRegIndep & " "">")
        Output.Add(" <Pixel>        :  RA <" & Solver.PixelErrorRA.ValRegIndep & " pixel>, DEC < " & Solver.PixelErrorDec.ValRegIndep & " pixel>")
        Output.Add("Angle           : <" & Solver.RotationAngle.ValRegIndep & ">")

        SolverLog = Output.ToArray

        '----------------------------------------------------------------------------------------------------
        'Code taken from ASCOM_CamTest

        'Dim Solver As New cPlateSolve
        'Dim BasePath As String = "\\DS1819\astro\2019-12-05\IC405\23_57_38"

        'Dim SolverRawOut As String() = {}

        'Dim InitRA As Double = Double.NaN
        'Dim InitDec As Double = Double.NaN

        'For Each File As String In System.IO.Directory.GetFiles(BasePath, "*.fits")

        '    Log(">>> " & File)

        '    Dim Changer As New cFITSHeaderChanger
        '    Dim FITSHeader As New cFITSHeaderParser(cFITSHeaderChanger.ReadHeader(File))

        '    'On first run set assumed position, else set parameters of 1st run
        '    If Double.IsNaN(InitRA) = True Then Solver.SetRA(5, 16, 12) Else Solver.RA = InitRA
        '    If Double.IsNaN(InitDec) = True Then Solver.SetDec(34, 16, 0) Else Solver.Dec = InitDec

        '    'Set X and Y dimensions
        '    Solver.SetDimX(2541, 4.88, FITSHeader.Width)
        '    Solver.SetDimY(2541, 4.88, FITSHeader.Height)
        '    Solver.HoldOpenTime = 0

        '    'Solve
        '    Dim SolverStatus As String = Solver.Solve(File, SolverRawOut)
        '    If Double.IsNaN(InitRA) = True Then InitRA = Solver.SolvedRA
        '    If Double.IsNaN(InitDec) = True Then InitDec = Solver.SolvedDec
        '    Log(Solver.ErrorRA.ValRegIndep & " : " & Solver.ErrorDec.ValRegIndep)

        '    'Set keywords after solving as e.g. http://bf-astro.com/eXcalibrator/excalibrator.htm may need it
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CTYPE1, "'RA---SIN'"))
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CTYPE2, "'DEC--SIN'"))
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CRPIX1, 0.5 * (FITSHeader.Width + 1)))                            'pixels
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CRPIX2, 0.5 * (FITSHeader.Height + 1)))                           'pixels
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CDELT1, (Solver.DimX * Solver.RadToGrad) / FITSHeader.Width))     'degrees/pixel
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CDELT2, (Solver.DimY * Solver.RadToGrad) / FITSHeader.Height))    'degrees/pixel
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CROTA1, 0.0))                                                     'degrees
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CROTA2, 0.0))                                                     'degrees
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CRVAL1, Solver.SolvedRA * Solver.RadToGrad))                      'Right Ascension [degrees]
        '    FITSHeader.Add(New cFITSHeaderParser.sHeaderElement(eFITSKeywords.CRVAL2, Solver.SolvedDec * Solver.RadToGrad))                     'Declination [degrees]

        '    'Store new header elements
        '    Changer.ChangeHeader(File, File & "_SOLVED.fits", FITSHeader.GetCardsAsDictionary)

        'Next File

        Return String.Empty

    End Function

    '''<summary>Filter values that have a high surrounding.</summary>
    '''<param name="Container">Container holding the image data.</param>
    '''<param name="StatCalc">Calculated statistical values.</param>
    '''<param name="SpecialPixels">Pixel with high value.</param>
    '''<returns>List of pixel values and corresponding coordinates for this pixel.</returns>
    Public Shared Function HighSurrounding(ByRef Container As AstroNET.Statistics, ByRef StatCalc As AstroNET.Statistics.sStatistics, ByRef SpecialPixels As Dictionary(Of UInt16, List(Of Point))) As Dictionary(Of UInt16, List(Of Point))

        Dim RetVal As New Dictionary(Of UInt16, List(Of Point))
        With Container.DataProcessor_UInt16.ImageData(0)
            For Each Value As UInt16 In SpecialPixels.Keys
                For Each Pixel As Point In SpecialPixels(Value)
                    Dim Idx1 As Integer = Pixel.X
                    Dim Idx2 As Integer = Pixel.Y
                    Dim Mean As UInt16 = 0
                    'Sum up all values arround
                    Using SurSum As New Ato.cSingleValueStatistics(True)
                        SurSum.AddValue(.Data(Idx1, Idx2))
                        If Idx1 > 0 Then
                            If Idx2 > 0 Then SurSum.AddValue(.Data(Idx1 - 1, Idx2 - 1))
                            SurSum.AddValue(.Data(Idx1 - 1, Idx2))
                            If Idx2 < .Data.GetUpperBound(1) Then SurSum.AddValue(.Data(Idx1 - 1, Idx2 + 1))
                        End If
                        If Idx2 > 0 Then SurSum.AddValue(.Data(Idx1, Idx2 - 1))
                        If Idx2 < .Data.GetUpperBound(1) Then SurSum.AddValue(.Data(Idx1, Idx2 + 1))
                        If Idx1 < .Data.GetUpperBound(0) Then
                            If Idx2 > 0 Then SurSum.AddValue(.Data(Idx1 + 1, Idx2 - 1))
                            SurSum.AddValue(.Data(Idx1 + 1, Idx2))
                            If Idx2 < .Data.GetUpperBound(1) Then SurSum.AddValue(.Data(Idx1 + 1, Idx2 + 1))
                        End If
                        Mean = CType(SurSum.Mean, UInt16)
                    End Using
                    If RetVal.ContainsKey(Mean) = False Then
                        RetVal.Add(Mean, New List(Of Point)({Pixel}))
                    Else
                        RetVal(Mean).Add(Pixel)
                    End If
                Next Pixel
            Next Value
        End With
        Return RetVal.SortDictionaryInverse

    End Function

End Class
