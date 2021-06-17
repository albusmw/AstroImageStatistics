Option Explicit On
Option Strict On

Public Class AstroImageStatistics_Fun

    Public Shared Function PlateSolve(ByVal FileToRun As String, ByVal PlateSolve2Path As String, ByVal PlateSolve2HoldOpen As Integer) As String()

        Dim Solver As New cPlateSolve
        Dim Binning As Integer = 1

        'Get the FITS header information
        Dim DataStartPos As Integer = -1
        Dim FITSHeader As List(Of cFITSHeaderParser.sHeaderElement) = cFITSHeaderChanger.ParseHeader(FileToRun, DataStartPos)
        Dim File_RA_JNow As String = Nothing
        Dim File_Dec_JNow As String = Nothing
        Dim File_FOV1 As Object = Nothing
        Dim File_FOV2 As Object = Nothing
        For Each Entry As cFITSHeaderParser.sHeaderElement In FITSHeader
            If Entry.Keyword = eFITSKeywords.RA Or Entry.Keyword = eFITSKeywords.RA_NOM Then File_RA_JNow = CStr(Entry.Value).Trim("'"c).Trim.Trim("'"c)
            If Entry.Keyword = eFITSKeywords.DEC Or Entry.Keyword = eFITSKeywords.DEC_NOM Then File_Dec_JNow = CStr(Entry.Value).Trim("'"c).Trim.Trim("'"c)
            If Entry.Keyword = eFITSKeywords.FOV1 Then File_FOV1 = Entry.Value
            If Entry.Keyword = eFITSKeywords.FOV2 Then File_FOV2 = Entry.Value
        Next Entry

        'Exit on no data
        If IsNothing(File_RA_JNow) Or IsNothing(File_Dec_JNow) Then
            Return New String() {}
        End If

        'Data from QHYCapture (10Micron) are in JNow, so convert to J2000 for PlateSolve
        Dim File_RA_J2000 As Double = Double.NaN
        Dim File_Dec_J2000 As Double = Double.NaN
        JNowToJ2000(AstroParser.ParseRA(File_RA_JNow), AstroParser.ParseDeclination(File_Dec_JNow), File_RA_J2000, File_Dec_J2000)

        'Run plate solve
        Dim ErrorCode1 As String = String.Empty
        Dim SolverIn_RA As String() = File_RA_JNow.Trim.Trim("'"c).Split(":"c)
        Dim SolverIn_Dec As String() = File_Dec_JNow.Trim.Trim("'"c).Split(":"c)
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
        J2000ToJNow(Solver.SolvedRA * RadToH, Solver.SolvedDec * RadToGrad, JNow_RA_solved, JNow_Dec_solved)

        Dim Output As New List(Of String)
        Output.Add("Start with        RA <" & File_RA_JNow & ">, DEC <" & File_Dec_JNow & "> (JNow file string)")
        Output.Add("                  RA <" & Ato.AstroCalc.FormatHMS(File_RA_J2000) & ">, DEC <" & Ato.AstroCalc.Format360Degree(File_Dec_J2000) & "> (J2000)")
        Output.Add("Solved as       : RA <" & Ato.AstroCalc.FormatHMS(Solver.SolvedRA * RadToH) & ">, DEC <" & Ato.AstroCalc.Format360Degree(Solver.SolvedDec * RadToGrad) & "> (J2000)")
        Output.Add("                  RA <" & Ato.AstroCalc.FormatHMS(JNow_RA_solved) & ">, DEC <" & Ato.AstroCalc.Format360Degree(JNow_Dec_solved) & "> (JNow)")
        Output.Add("Error           :  RA <" & Solver.ErrorRA.ValRegIndep & " "">, DEC < " & Solver.ErrorDec.ValRegIndep & " "">")
        Output.Add("Error results   :  RA <" & Solver.ErrorRA.ValRegIndep & " "">, DEC < " & Solver.ErrorDec.ValRegIndep & " "">")
        Output.Add(" <Pixel>        :  RA <" & Solver.PixelErrorRA.ValRegIndep & " pixel>, DEC < " & Solver.PixelErrorDec.ValRegIndep & " pixel>")
        Output.Add("Angle           : <" & Solver.RotationAngle.ValRegIndep & ">")

        Return Output.ToArray

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

    '''<summary>Convert JNow to J2000 epoch.</summary>
    '''<param name="JNowRA">RA in apparent co-ordinates [hours].</param>
    '''<param name="JNowDec">DEC in apparent co-ordinates [deg].</param>
    '''<param name="J2000RA">J2000 Right Ascension [hours].</param>
    '''<param name="J2000Dec">J2000 Declination [deg].</param>
    '''<seealso cref="https://ascom-standards.org/Help/Developer/html/T_ASCOM_Astrometry_Transform_Transform.htm"/>
    Public Shared Sub JNowToJ2000(ByVal JNowRA As Double, ByVal JNowDec As Double, ByRef J2000RA As Double, ByRef J2000Dec As Double)
        Dim X As New ASCOM.Astrometry.Transform.Transform
        X.JulianDateUTC = (New ASCOM.Astrometry.NOVAS.NOVAS31).JulianDate(CShort(Now.Year), CShort(Now.Month), CShort(Now.Day), Now.Hour)
        X.SetApparent(JNowRA, JNowDec)
        J2000RA = X.RAJ2000
        J2000Dec = X.DecJ2000
    End Sub

    '''<summary>Convert J2000 to JNow epoch.</summary>
    '''<seealso cref="https://ascom-standards.org/Help/Developer/html/T_ASCOM_Astrometry_Transform_Transform.htm"/>
    Public Shared Sub J2000ToJNow(ByVal J2000RA As Double, ByVal J2000Dec As Double, ByRef JNowRA As Double, ByRef JNowDec As Double)
        Dim X As New ASCOM.Astrometry.Transform.Transform
        X.JulianDateUTC = (New ASCOM.Astrometry.NOVAS.NOVAS31).JulianDate(CShort(Now.Year), CShort(Now.Month), CShort(Now.Day), Now.Hour)
        X.SetJ2000(J2000RA, J2000Dec)
        JNowRA = X.RAApparent
        JNowDec = X.DECApparent
    End Sub

End Class
