Option Explicit On
Option Strict On

'''<summary>Class for radial statistics to e.g. support collimation.</summary>
Public Class cCollimation

    Public ImageData As AstroNET.Statistics

    Private RadStat As New Dictionary(Of Integer, List(Of UInt16))

    Public ReadOnly Property HistoBins() As Integer
        Get
            Return RadStat.Count
        End Get
    End Property

    '''<summary>Calculate the histogram around the center (to get star radius, ...).</summary>
    '''<param name="Center">Center coordinate.</param>
    '''<param name="HalfX">TRUE to move X center between 2 pixel.</param>
    '''<param name="HalfY">TRUE to move Y center between 2 pixel.</param>
    '''<param name="Radius">Radius to run calculation for.</param>
    Public Sub ShowHistoAroundCenter(ByVal Center As Point, ByVal HalfX As Boolean, ByVal HalfY As Boolean, ByVal Radius As Integer, ByVal RadiusCombiner As Double, ByVal AngleSegments As Integer, ByRef PlotDist As cZEDGraphService, ByRef PlotCircle As cZEDGraphService)

        Dim RealCenter_X As Double = Center.X + CInt(IIf(HalfX = True, 1 / 2, 0))
        Dim RealCenter_Y As Double = Center.Y + CInt(IIf(HalfY = True, 1 / 2, 0))

        Dim DistPerAngle As New cDistPerAngle

        Dim Radius2 As Integer = Radius * Radius
        Dim X_left As Integer = Center.X - Radius - 1 : If X_left < 0 Then X_left = 0
        Dim Y_left As Integer = Center.Y - Radius - 1 : If Y_left < 0 Then Y_left = 0
        Dim X_right As Integer = Center.X + Radius + 1 : If X_right > ImageData.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(0) Then X_right = ImageData.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(0)
        Dim Y_right As Integer = Center.Y + Radius + 1 : If Y_right > ImageData.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(1) Then Y_right = ImageData.DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(1)

        RadiusCombiner = RadiusCombiner ^ 2

        RadStat.Clear()
        For Pixel_X As Integer = X_left To X_right
            For Pixel_Y As Integer = Y_left To Y_right

                Dim DeltaX As Double = (RealCenter_X - Pixel_X)
                Dim DeltaY As Double = (RealCenter_Y - Pixel_Y)
                Dim Angle As Integer = CInt(Math.Atan2(DeltaX, DeltaY) * (AngleSegments / (2 * Math.PI)))
                Dim Distance As Double = Math.Sqrt((DeltaX * DeltaX) + (DeltaY * DeltaY))
                Dim DistanceBin As Integer = CInt(Distance / RadiusCombiner)

                If Distance <= Radius2 Then

                    Dim PixelValue As UInt16 = ImageData.DataProcessor_UInt16.ImageData(0).Data(Pixel_X, Pixel_Y)
                    If RadStat.ContainsKey(DistanceBin) = False Then RadStat.Add(DistanceBin, New List(Of UInt16))
                    RadStat(DistanceBin).Add(PixelValue)

                    DistPerAngle.Add(Angle, Distance, PixelValue)


                End If

            Next Pixel_Y
        Next Pixel_X

        'Run processing
        DistPerAngle.Process()

        RadStat = RadStat.SortDictionary
        Dim X As New List(Of Double)
        Dim Y_mean As New List(Of Double)
        Dim Y_max As New List(Of Double)
        Dim Y_min As New List(Of Double)
        For Each Entry As Integer In RadStat.Keys
            X.Add(Math.Sqrt(Entry))
            Y_mean.Add((RadStat(Entry).ToArray).Sum / RadStat(Entry).Count)
            Y_max.Add(RadStat(Entry).Max)
            Y_min.Add(RadStat(Entry).Min)
        Next Entry

        PlotDist.SetCaptions("Statistics vs distance", "Distance from center", "Pixel value")
        PlotDist.PlotXvsY("Max vs Radius", X.ToArray, Y_max.ToArray, New cZEDGraphService.sGraphStyle(Color.Red, ZEDGraphUtil.sGraphStyle.eCurveMode.Lines))
        PlotDist.PlotXvsY("Mean vs Radius", X.ToArray, Y_mean.ToArray, New cZEDGraphService.sGraphStyle(Color.Orange, ZEDGraphUtil.sGraphStyle.eCurveMode.LinesAndPoints))
        PlotDist.PlotXvsY("Min vs Radius", X.ToArray, Y_min.ToArray, New cZEDGraphService.sGraphStyle(Color.Green, ZEDGraphUtil.sGraphStyle.eCurveMode.Lines))

        PlotDist.SetCaptions("Statistics vs angle", "Angle [°]", "Value")
        PlotCircle.PlotXvsY("Weighted mean", DistPerAngle.Angles, DistPerAngle.Median, New cZEDGraphService.sGraphStyle(Color.Orange, ZEDGraphUtil.sGraphStyle.eCurveMode.Dots))
        'PlotCircle.PlotXvsY("Pct_10", DistPerAngle.Angles, DistPerAngle.Pct_10, New cZEDGraphService.sGraphStyle(Color.Red, ZEDGraphUtil.sGraphStyle.eCurveMode.Dots))
        'PlotCircle.PlotXvsY("Pct_90", DistPerAngle.Angles, DistPerAngle.Pct_90, New cZEDGraphService.sGraphStyle(Color.Green, ZEDGraphUtil.sGraphStyle.eCurveMode.Dots))
        PlotCircle.PlotXvsY("Pct_width", DistPerAngle.Angles, DistPerAngle.Pct_width, New cZEDGraphService.sGraphStyle(Color.Green, ZEDGraphUtil.sGraphStyle.eCurveMode.Dots))



    End Sub

    Private Class cDistPerAngle

        Private Structure sRadialSample
            Public Distance As Double
            Public PixelValue As Double
            Public Sub New(ByVal NewDistance As Double, ByVal NewPixelVAlue As Double)
                Distance = NewDistance
                PixelValue = NewPixelVAlue
            End Sub
            Public Shared Function SortDistance(ByVal X1 As sRadialSample, ByVal X2 As sRadialSample) As Integer
                Return X1.Distance.CompareTo(X2.Distance)
            End Function
        End Structure

        Private AngleOriented As New Dictionary(Of Integer, List(Of sRadialSample))

        Private AngleSorted As New List(Of Double)
        Private AngleMedian As New List(Of Double)
        Private AnglePct_10 As New List(Of Double)
        Private AnglePct_90 As New List(Of Double)
        Private AnglePct_width As New List(Of Double)

        Public ReadOnly Property Angles As Double()
            Get
                Return AngleSorted.ToArray
            End Get
        End Property

        Public ReadOnly Property Median As Double()
            Get
                Return AngleMedian.ToArray
            End Get
        End Property

        Public ReadOnly Property Pct_10 As Double()
            Get
                Return AnglePct_10.ToArray
            End Get
        End Property

        Public ReadOnly Property Pct_90 As Double()
            Get
                Return AnglePct_90.ToArray
            End Get
        End Property

        Public ReadOnly Property Pct_width As Double()
            Get
                Return AnglePct_width.ToArray
            End Get
        End Property


        Public Sub Add(ByVal Angle As Integer, ByVal Distance As Double, ByVal PixelValue As Double)

            If AngleOriented.ContainsKey(Angle) = False Then
                AngleOriented.Add(Angle, New List(Of sRadialSample))
            End If
            AngleOriented(Angle).Add(New sRadialSample(Distance, PixelValue))

        End Sub

        Public Sub Process()

            AngleSorted = New List(Of Double)
            AngleMedian = New List(Of Double)
            AnglePct_10 = New List(Of Double)

            Dim AllAngles As List(Of Integer) = AngleOriented.Keys.ToList : AllAngles.Sort()
            For AngleIdx As Integer = 0 To AllAngles.Count - 1

                Dim Angle As Integer = AllAngles(AngleIdx)
                Dim ThisAngle As List(Of sRadialSample) = AngleOriented(Angle)
                ThisAngle.Sort(AddressOf sRadialSample.SortDistance)

                AngleSorted.Add(Angle)
                Dim Zaehler As Double = 0
                Dim WeightSum As Double = 0
                For EntryIdx As Integer = 0 To ThisAngle.Count - 1
                    Zaehler += (ThisAngle(EntryIdx).Distance * ThisAngle(EntryIdx).PixelValue)
                    WeightSum += ThisAngle(EntryIdx).PixelValue
                Next EntryIdx
                AngleMedian.Add(Zaehler / WeightSum)
                Dim Pct_10 As Double = WeightSum * (0.5 / 100)
                Dim Pct_90 As Double = WeightSum * (99.5 / 100)
                Dim AccuSum As Double = 0
                Dim Idx As Integer = 0
                Idx = -1
                Do
                    Idx += 1
                    AccuSum += ThisAngle(Idx).PixelValue
                Loop Until AccuSum >= Pct_10
                AnglePct_10.Add(ThisAngle(Idx).Distance)
                Idx = -1
                Do
                    Idx += 1
                    AccuSum += ThisAngle(Idx).PixelValue
                Loop Until AccuSum >= Pct_90
                AnglePct_90.Add(ThisAngle(Idx).Distance)
                AnglePct_width.Add(AnglePct_90(AngleIdx) - AnglePct_10(AngleIdx))
            Next AngleIdx

        End Sub

        'Private Function MeanWeight(ByVal x As List(Of Double), ByVal Weigth As List(Of Double)) As Double
        '    Dim Zaehler As Double = 0
        '    Dim Nenner As Double = 0
        '    For Idx As Integer = 0 To x.Count - 1
        '        Zaehler += (x(Idx) * Weigth(Idx))
        '        Nenner += Weigth(Idx)
        '    Next Idx
        '    Return Zaehler / Nenner
        'End Function

        'Too complicate
        'For Idx_X As Integer = FirstIdx_X To Radius
        '    For Idx_Y As Integer = FirstIdx_Y To Radius
        '        Dim SameRadius As New List(Of String)
        '        Dim Idx_X_range As Integer() = {0} : If Idx_X > 0 Then Idx_X_range = New Integer() {-Idx_X, Idx_X}
        '        Dim Idx_Y_range As Integer() = {0} : If Idx_Y > 0 Then Idx_Y_range = New Integer() {-Idx_Y, Idx_Y}
        '        For Each Idx_X_signed As Integer In Idx_X_range
        '            For Each Idx_Y_signed As Integer In Idx_Y_range

        '                Dim Pixel_X As Double = Center.X + Idx_X_signed
        '                Dim Pixel_Y As Double = Center.Y + Idx_Y_signed
        '                Dim PixelString As String = Pixel_X.ToString.Trim & ":" & Pixel_Y.ToString.Trim
        '                If SameRadius.Contains(PixelString) = False Then SameRadius.Add(PixelString)

        '                If Point.Contains(PixelString) = False Then
        '                    Point.Add(PixelString)
        '                Else
        '                    DoublePoint.Add(PixelString)
        '                End If

        '            Next Idx_Y_signed
        '        Next Idx_X_signed
        '        SameRadiusPoints.Add(Join(SameRadius.ToArray, "|"))
        '    Next Idx_Y
        'Next Idx_X


        'Dim Center_X As Double = ZoomedStatistics.Center.X + StarCenter.X - (ZoomedStatistics.Width / 2)
        'Dim Center_Y As Double = ZoomedStatistics.Center.Y + StarCenter.Y - (ZoomedStatistics.Height / 2)


        'Dim HistData As New cHistData
        'Dim ValuesAdded As New List(Of Integer)

        'If Double.IsNaN(Center.X) = True Then Exit Sub
        'If Double.IsNaN(Center.Y) = True Then Exit Sub

        ''Scan a rectangular region and remove the pixel within the radius specified
        'For IdxX As Integer = CInt(Math.Floor(Center.X - Radius)) To CInt(Math.Ceiling(Center.X + Radius))
        '    If IdxX >= 0 And IdxX <= DB.Channels(DisplayedChannel).ImageData.GetUpperBound(0) Then
        '        Dim DeltaX As Double = ((IdxX - Center.X) * (IdxX - Center.X))
        '        For IdxY As Integer = CInt(Math.Floor(Center.Y - Radius)) To CInt(Math.Ceiling(Center.Y + Radius))
        '            If IdxY >= 0 And IdxY <= DB.Channels(DisplayedChannel).ImageData.GetUpperBound(1) Then
        '                Dim CurrentRadius As Integer = CInt(Math.Sqrt(Math.Floor(DeltaX + ((IdxY - Center.Y) * (IdxY - Center.Y)))) / BinWidth)
        '                HistData.Update(CurrentRadius, DB.Channels(DisplayedChannel).ImageData(IdxX, IdxY))
        '            End If
        '        Next IdxY
        '    End If
        'Next IdxX

        'Dim Plot_X As Double() = {}
        'Dim Plot_Y As Double() = {}

        'HistData.GetXY(Plot_X, Plot_Y, BinWidth, StarRadius, OuterEnergy)

        'ZEDGraphUtil.PlotXvsY(zedHisto, "Power over center distance", Plot_X, Plot_Y, New ZEDGraphUtil.sGraphStyle(Color.Red, ZEDGraphUtil.sGraphStyle.eCurveMode.Dots), Double.NaN, Double.NaN)

    End Class

End Class
