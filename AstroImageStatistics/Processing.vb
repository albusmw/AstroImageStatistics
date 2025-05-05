Option Explicit On
Option Strict On

'''<summary>Class to run combined actions (file loading, ...).</summary>
Public Class Processing

    '''<summary>Load the given file.</summary>
    '''<param name="FileName">File to read in.</param>
    '''<param name="SingleStatCalc">Container for data and statistics.</param>
    '''<param name="FITSHeader">FITS header as read in.</param>
    '''<param name="DataStartPosition">Position where the data start.</param>
    '''<returns>Error code or empty string on no error.</returns>
    Public Shared Function LoadFITSFile(ByVal FileName As String, ByRef IPP As cIntelIPP, ByVal ForceDirect As Boolean, ByRef FITSHeader As cFITSHeaderParser, ByRef SingleStatCalc As AstroNET.Statistics, ByRef DataStartPosition As Integer) As String

        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)
        Dim DataStartPos As Integer = 0

        '=========================================================================================================
        'Prepare

        SingleStatCalc = New AstroNET.Statistics(AIS.DB.IPP)
        Dim UseIPP As Boolean = True : If IsNothing(IPP) Then UseIPP = False

        '=========================================================================================================
        'Read fits header

        FITSHeader = New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))

        '=========================================================================================================
        'Read the FITS data

        SingleStatCalc.ResetAllProcessors()
        Select Case FITSHeader.BitPix
            Case 8
                SingleStatCalc.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt8(FileName, UseIPP)
            Case 16
                With SingleStatCalc.DataProcessor_UInt16
                    .ImageData(0).Data = FITSReader.ReadInUInt16(FileName, UseIPP, ForceDirect)
                    If FITSHeader.NAXIS3 > 1 Then
                        For Idx As Integer = 1 To FITSHeader.NAXIS3 - 1
                            DataStartPos += CInt(.ImageData(Idx - 1).Length * FITSHeader.BytesPerSample)        'move to next plane
                            .ImageData(Idx).Data = FITSReader.ReadInUInt16(FileName, DataStartPos, UseIPP, ForceDirect)
                        Next Idx
                    End If
                End With
            Case 32
                SingleStatCalc.DataProcessor_Int32.ImageData = FITSReader.ReadInInt32(FileName, UseIPP)
            Case -32
                With SingleStatCalc.DataProcessor_Float32
                    .ImageData(0).Data = FITSReader.ReadInFloat32(FileName, UseIPP)
                    If FITSHeader.NAXIS3 > 1 Then
                        For Idx As Integer = 1 To FITSHeader.NAXIS3 - 1
                            DataStartPos += CInt(.ImageData(Idx - 1).Length * FITSHeader.BytesPerSample)        'move to next plane
                            .ImageData(Idx).Data = FITSReader.ReadInFloat32(FileName, UseIPP)
                        Next Idx
                    End If
                End With
            Case Else
                Return "!!! File format <" & FITSHeader.BitPix.ToString.Trim & "> not yet supported!"
        End Select

        Return String.Empty

    End Function

    '''<summary>Run the statistics calcuation and display the result.</summary>
    Public Shared Function CalculateStatistics(ByRef Container As AstroNET.Statistics, ByVal ReportMonoStat As Boolean, ByVal ReportBayerStat As Boolean, ByVal ChannelNames As List(Of String), ByRef Stat As AstroNET.Statistics.sStatistics) As List(Of String)
        Dim Indent As String = "  "
        'Calculate statistics
        Stat = Container.ImageStatistics()
        'Log statistics
        Dim RetVal As New List(Of String)
        RetVal.Add("Statistics:")
        For Each Line As String In Container.ImageStatistics.StatisticsReport(ReportMonoStat, ReportBayerStat, ChannelNames)
            RetVal.Add(Indent & Line)
        Next Line
        RetVal.Add(New String("="c, 109))
        Return RetVal
    End Function

End Class
