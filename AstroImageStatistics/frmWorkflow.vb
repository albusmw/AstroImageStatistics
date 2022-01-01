Option Explicit On
Option Strict On

Public Class frmWorkflow

    Private LogContent As New System.Text.StringBuilder
    Private WithEvents DD_Bias As Ato.DragDrop

    Dim ReportIndent As String = "  "

    Private Sub btnBias_Add_Click(sender As Object, e As EventArgs) Handles btnBias_Add.Click
        lbBiasFiles.Items.AddRange(System.IO.Directory.GetFiles(tbBias_Root.Text, tbBias_Filter.Text))
    End Sub

    Private Sub btnMasterBias_Click(sender As Object, e As EventArgs) Handles btnMasterBias.Click

        Dim DataAsUInt32(,) As UInt32 = {}
        Dim DataAsDouble(,) As Double = {}
        Dim PowerSum(,) As Double = {}
        Dim TotalSum(,) As UInt32 = {}
        Dim MaxEvery(,) As UInt32 = {}
        Dim MinEvery(,) As UInt32 = {}
        Dim StatCheck As New Ato.cSingleValueStatistics(True)

        For Idx As Integer = 0 To lbBiasFiles.Items.Count - 1

            Dim FileName As String = CStr(lbBiasFiles.Items.Item(Idx))
            Log("Reading file " & (Idx + 1).ValRegIndep & "/" & lbBiasFiles.Items.Count.ValRegIndep & ":  <" & FileName.Replace(tbBias_Root.Text, String.Empty) & ">")

            'Read the FITS header
            Dim DataStartPos As Integer = 0
            Dim FITSHeader As New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))
            Dim FITSHeaderDict As Dictionary(Of eFITSKeywords, Object) = FITSHeader.GetCardsAsDictionary

            'Read content
            Dim FITSReader As New cFITSReader
            Dim Container As New AstroNET.Statistics(AIS.DB.IPP)
            Container.ResetAllProcessors()
            Container.DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileName, AIS.DB.UseIPP, AIS.DB.ForceDirect)

            'Sum up all data and get total MAX and total MIN
            AIS.DB.IPP.Convert(Container.DataProcessor_UInt16.ImageData(0).Data, DataAsUInt32)
            AIS.DB.IPP.Add(DataAsUInt32, TotalSum)
            If Idx = 0 Then
                AIS.DB.IPP.Copy(DataAsUInt32, MaxEvery)
                AIS.DB.IPP.Copy(DataAsUInt32, MinEvery)
            Else
                AIS.DB.IPP.MaxEvery(DataAsUInt32, MaxEvery)
                AIS.DB.IPP.MinEvery(DataAsUInt32, MinEvery)
            End If

            'Square sum - factor of 2 too much as we add Re^2 + Re^2
            AIS.DB.IPP.Convert(DataAsUInt32, DataAsDouble)
            AIS.DB.IPP.PowerSpectr(DataAsDouble, DataAsDouble, DataAsDouble)
            AIS.DB.IPP.Add(DataAsDouble, PowerSum)

            'Fast single file statistics
            ReportMaxMin(DataAsUInt32)

            'Total stat
            StatCheck.AddValue(Container.DataProcessor_UInt16.ImageData(0).Data(0, 0))

        Next Idx

        'Get sigma of each pixel
        Dim Norm As Double = lbBiasFiles.Items.Count
        Dim Sigma(,) As Double = {}
        Dim SumSum(,) As Double = {}
        AIS.DB.IPP.DivC(PowerSum, 2)            'normiert
        Sigma = AIS.DB.IPP.Copy(PowerSum)       'E[X^2]
        AIS.DB.IPP.Convert(TotalSum, SumSum)    'E[X]
        AIS.DB.IPP.Mul(SumSum, SumSum)          'E^2[X]
        AIS.DB.IPP.DivC(SumSum, Norm)           'E^2[X]/count
        AIS.DB.IPP.Sub(SumSum, PowerSum)        'PowerSum = E[X^2] - E^2[x]
        AIS.DB.IPP.DivC(PowerSum, Norm - 1)
        AIS.DB.IPP.Sqrt(PowerSum)

        'Find peak positions in MaxEvery
        Log("Statistics MaxEvery:")
        ReportMaxMin(MaxEvery)

        'Find peak positions in MinEvery
        Log("Statistics MinEvery:")
        ReportMaxMin(MinEvery)

        'Calculate TotalSum statistics
        Log("Statistics SUM:")
        ReportMaxMin(TotalSum)

        'Calculate MAX-MIN range for each pixel
        Dim MaxMin(,) As UInt32 = {}
        Dim MaxMin_min As UInt32 = UInt32.MaxValue
        Dim MaxMin_max As UInt32 = UInt32.MinValue
        AIS.DB.IPP.Copy(MaxEvery, MaxMin)
        AIS.DB.IPP.Sub(MinEvery, MaxMin)
        AIS.DB.IPP.MinMax(MaxMin, MaxMin_min, MaxMin_max)

        'Calculate statistics for this MAX-MIN range
        Dim Stat_MaxMin As AstroNET.Statistics.sStatistics = Nothing
        Using Container_MaxMin As New AstroNET.Statistics(AIS.DB.IPP)
            Container_MaxMin.ResetAllProcessors()
            Container_MaxMin.DataProcessor_UInt16.LoadImageData(MaxMin)
            Stat_MaxMin = Container_MaxMin.ImageStatistics()
        End Using
        Log("Statistics Max-Min:")
        Log(ReportIndent, Stat_MaxMin.StatisticsReport(True, False).ToArray())
        Log(New String("="c, 109))

        Log("END.")

    End Sub

    Private Sub ReportMaxMin(ByRef Vector(,) As UInt32)
        Dim MaxVal As UInt32 : Dim MinVal As UInt32
        Dim MaxPos As Integer : Dim MinPos As Integer
        AIS.DB.IPP.MinMaxIndx(Vector, MinVal, MinPos, MaxVal, MaxPos)
        Log(ReportIndent, "MIN: " & MinVal.ValRegIndep & " @ " & cIntelIPP.Index2D(Vector, MinPos).ValRegIndep)
        Log(ReportIndent, "MAX: " & MaxVal.ValRegIndep & " @ " & cIntelIPP.Index2D(Vector, MaxPos).ValRegIndep)
    End Sub

    '==============================================================================================

    Private Sub Log(ByVal Text As String)
        Log(Text, False, True)
    End Sub

    Private Sub Log(ByVal Text As List(Of String))
        Log(Text.ToArray)
    End Sub

    Private Sub Log(ByVal Indent As String, ByVal Text As String)
        Log(Indent & Text, False, False)
    End Sub

    Private Sub Log(ByVal Indent As String, ByVal Text() As String)
        For Each Line As String In Text
            Log(Indent & Line, False, False)
        Next Line
        UpdateLog()
    End Sub

    Private Sub Log(ByVal Text() As String)
        For Each Line As String In Text
            Log(Line, False, False)
        Next Line
        UpdateLog()
    End Sub

    Private Sub Log(ByVal Text As String, ByVal LogInStatus As Boolean, ByVal AutoUpdate As Boolean)
        Text = Format(Now, "HH.mm.ss:fff") & "|" & Text
        With LogContent
            If .Length = 0 Then
                .Append(Text)
            Else
                .Append(System.Environment.NewLine & Text)
            End If
            If AutoUpdate = True Then UpdateLog()
            'If LogInStatus = True Then tsslMain.Text = Text
        End With
        DE()
    End Sub

    Private Sub UpdateLog()
        With tbLogOutput
            .Text = LogContent.ToString
            If .Text.Length > 0 Then
                .SelectionStart = .Text.Length - 1
                .SelectionLength = 0
                .ScrollToCaret()
            End If
        End With
    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub lbBiasFiles_KeyUp(sender As Object, e As KeyEventArgs) Handles lbBiasFiles.KeyUp
        If e.KeyCode = Keys.Delete Then
            lbBiasFiles.RemoveSelectedItems
        End If
    End Sub

    Private Sub frmWorkflow_Load(sender As Object, e As EventArgs) Handles Me.Load
        DD_Bias = New Ato.DragDrop(lbBiasFiles, True)       'TODO: configure if double-drop same file is allowed!
    End Sub

End Class