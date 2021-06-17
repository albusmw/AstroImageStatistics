Option Explicit On
Option Strict On

Public Class IntelIPP_NewCode

    ''' <summary>Test code for image translation</summary>
    ''' <param name="File">File to translate.</param>
    Public Shared Sub Translate(ByRef FileName As String)

        Dim FileNameOnly As String = System.IO.Path.GetFileName(FileName)
        Dim DataStartPos As Integer = -1
        Dim FITSHeader As New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))
        Dim FITSHeaderDict As Dictionary(Of eFITSKeywords, Object) = FITSHeader.GetCardsAsDictionary
        Dim FITSReader As New cFITSReader
        Dim ImageData(,) As UInt16 = FITSReader.ReadInUInt16(FileName, True, False)

        Dim StatusRecord As New List(Of cIntelIPP.IppStatus)

        'Get rotation and transformation parameters
        Dim MyIPP As New cIntelIPP(cFITSReader.IPPPath)
        Dim coeffs(,) As Double = {}
        Dim Angle As Double = 100
        Dim ShiftX As Double = 20
        Dim ShiftY As Double = 30
        StatusRecord.Add(MyIPP.GetRotateTransform(Angle, ShiftX, ShiftY, coeffs))

        'Prepare call
        Dim SizeSource As New cIntelIPP.IppiSize(ImageData.GetUpperBound(0) + 1, ImageData.GetUpperBound(1) + 1)
        Dim SizeDest As New cIntelIPP.IppiSize(SizeSource.Width, SizeSource.Height)
        Dim DataType As cIntelIPP.IppDataType = cIntelIPP.IppDataType.ipp8u
        Dim Interpolation As cIntelIPP.IppiInterpolationType = cIntelIPP.IppiInterpolationType.ippLinear
        Dim Direction As cIntelIPP.IppiWarpDirection = cIntelIPP.IppiWarpDirection.ippWarpForward
        Dim BorderType As cIntelIPP.IppiBorderType = cIntelIPP.IppiBorderType.ippBorderConst

        Dim pSpecSize As Integer = -1
        Dim pInitBufSize As Integer = -1
        Dim boarderValue As Double = Double.NaN
        Dim Spec() As Byte

        StatusRecord.Add(MyIPP.WarpAffineGetSize(SizeSource, SizeDest, DataType, coeffs, Interpolation, Direction, BorderType, pSpecSize, pInitBufSize))
        ReDim Spec(pInitBufSize - 1)
        StatusRecord.Add(MyIPP.WarpAffineLinearInit(SizeSource, SizeDest, DataType, coeffs, Direction, 1, BorderType, boarderValue, 0, Spec))

        MsgBox("OK")

    End Sub

End Class
