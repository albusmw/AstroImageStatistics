Option Explicit On
Option Strict On

Public Class frmXCorr

    Dim CalcCount As Integer = 0
    Dim Log As New List(Of String)
    Dim Ref_data(,) As Single
    Dim Tlp_data(,) As Single
    Dim Corr(,) As Single

    Private Sub RunCorrelation()

        Dim Ref_X As Integer = Integer.MinValue
        Dim Ref_Y As Integer = Integer.MinValue
        Dim Tpl_X As Integer = Integer.MinValue
        Dim Tpl_Y As Integer = Integer.MinValue

        Dim Ref_Xsize As Integer = Integer.MinValue
        Dim Ref_Ysize As Integer = Integer.MinValue
        Dim Tpl_Xsize As Integer = Integer.MinValue
        Dim Tpl_Ysize As Integer = Integer.MinValue

        Dim RefROI As Drawing.Rectangle = Nothing
        Dim TplROI As Drawing.Rectangle = Nothing

        Log.Clear()

        Try
            Ref_X = CInt(tbRef_X.Text) : Ref_Xsize = CInt(tbRef_Xsize.Text)
            Ref_Y = CInt(tbRef_Y.Text) : Ref_Ysize = CInt(tbRef_Ysize.Text)
            Tpl_X = CInt(tbTpl_X.Text) : Tpl_Xsize = CInt(tbTpl_Xsize.Text)
            Tpl_Y = CInt(tbTpl_Y.Text) : Tpl_Ysize = CInt(tbTpl_Ysize.Text)
        Catch ex As Exception
            Log.Add("!!!!ROI coordinates can not be transformed")
            Exit Sub
        End Try

        Try
            RefROI = New Drawing.Rectangle(Ref_X, Ref_Y, Ref_Xsize, Ref_Ysize)
            TplROI = New Drawing.Rectangle(Tpl_X, Tpl_Y, Tpl_Xsize, Tpl_Ysize)
        Catch ex As Exception
            Log.Add("!!!!ROI coordinates can not be set")
            Exit Sub
        End Try

        Dim Stopper As New Stopwatch
        Stopper.Restart() : Stopper.Start()
        CalcCount += 1
        Dim RefImage(,) As Single = Ref_data.GetROI(RefROI)
        Dim TemplateImage(,) As Single = Tlp_data.GetROI(TplROI)
        If (IsNothing(RefImage) = True) Or (IsNothing(TemplateImage) = True) Then
            Log.Add("!!!!Empty data")
            Exit Sub
        End If
        Corr = cRegistration.Correlate(RefImage, TemplateImage)
        If (IsNothing(Corr) = True) Then
            Log.Add("!!!!FUNCTION ERROR!!!!!")
            Exit Sub
        End If
        'ShowCorrResults(Corr)
        Stopper.Stop()

        Log.Add("Calculation # " & CalcCount.ToString.Trim.PadLeft(10, " "c))
        Log.Add("Speed: " & Stopper.ElapsedMilliseconds.ToString.Trim & " ms")

    End Sub

    Private Sub ShowCorrResults(ByVal ShiftX As Integer, ByVal ShiftY As Integer, ByVal CorrVal As Double)
        Log.Add("Calculated Shift: <" & ShiftX.ValRegIndep & ":" & ShiftY.ValRegIndep & ">, value: " & CorrVal)
    End Sub



    Private Sub ippiXCorrTestCode()
        'https://www.intel.com/content/www/us/en/docs/ipp/developer-reference/2021-7/crosscorrnorm-002.html
        Dim SizeOfSingle As Integer = 4
        Dim Src(,) As Single = {{1.0, 2.0, 1.5, 4.1, 3.6}, {0.2, 3.2, 2.5, 1.5, 10.0}, {5.0, 6.8, 0.5, 4.1, 1.1}, {7.1, 4.2, 2.2, 8.7, 10.0}}
        Dim Tpl(,) As Single = {{2.1, 3.5, 7.7}, {0.4, 2.3, 5.5}, {1.4, 2.8, 3.1}}
        Dim Dst(Src.GetUpperBound(0), Src.GetUpperBound(1)) As Single
        Dim srcRoiSize As New cIntelIPP.IppiSize(5, 4)
        Dim tplRoiSize As New cIntelIPP.IppiSize(3, 3)
        Dim dstRoiSize As New cIntelIPP.IppiSize(5, 4)
        Dim srcStep As Integer = srcRoiSize.Width * SizeOfSingle
        Dim tplStep As Integer = tplRoiSize.Width * SizeOfSingle
        Dim dstStep As Integer = dstRoiSize.Width * SizeOfSingle
        Dim funcfg As Integer = cIntelIPP.IppAlgType.ippAlgAuto Or cIntelIPP.eIppiROIShape.ippiROIValid Or cIntelIPP.eIppiNormOp.ippiNorm
        Dim BufferSize As Integer = -1
        Dim Status1 As cIntelIPP.IppStatus = AIS.DB.IPP.CrossCorrNormGetBufferSize(srcRoiSize, tplRoiSize, funcfg, BufferSize)
        Dim Buffer(BufferSize - 1) As Byte
        Dim Status2 As cIntelIPP.IppStatus = AIS.DB.IPP.CrossCorrNorm(Src, srcStep, srcRoiSize, Tpl, tplStep, tplRoiSize, Dst, dstStep, funcfg, Buffer)
        '0.53 0.54 0.58 0.50 0.30
        '0.68 0.62 0.68 0.83 0.38
        '0.77 0.55 0.60 0.81 0.42
        '0.81 0.46 0.70 0.62 0.24
    End Sub

    Private Sub ROI_TextChanged(sender As Object, e As EventArgs) Handles tbRef_X.TextChanged, tbRef_Y.TextChanged, tbTpl_X.TextChanged, tbTpl_Y.TextChanged, tbRef_Xsize.TextChanged, tbRef_Ysize.TextChanged, tbTpl_Xsize.TextChanged, tbTpl_Ysize.TextChanged
        RunCorrelation()
    End Sub

    Private Sub btnStoreAndOpen_Click(sender As Object, e As EventArgs) Handles btnStoreAndOpen.Click
        Dim CorrFile As String = "C:\!Work\XCorr.fits"
        cFITSWriter.Write(CorrFile, Corr, cFITSWriter.eBitPix.Single)
        AstroImageStatistics.Ato.Utils.StartWithItsEXE(CorrFile)
    End Sub

    Private Sub btnLoadRef_Click(sender As Object, e As EventArgs) Handles btnLoadRef.Click
        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)
        Ref_data = ImageProcessing.Binning.Bin2_Inner_Single(FITSReader.ReadInUInt16(tbRefFile.Text, True, True))
        tbRefFile.BackColor = Color.LimeGreen
        RunCorrelation()
    End Sub

    Private Sub btnLoadMatch_Click(sender As Object, e As EventArgs) Handles btnLoadTemplate.Click
        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)
        Tlp_data = ImageProcessing.Binning.Bin2_Inner_Single(FITSReader.ReadInUInt16(tbTemplateFile.Text, True, True))
        tbTemplateFile.BackColor = Color.LimeGreen
        RunCorrelation()
    End Sub

    Private Sub tShowResults_Tick(sender As Object, e As EventArgs) Handles tShowResults.Tick
        tbResults.Text = Join(Log.ToArray, System.Environment.NewLine)
    End Sub

    Private Sub MouseWheelChange(sender As Object, e As MouseEventArgs) Handles tbRef_X.MouseWheel, tbRef_Y.MouseWheel, tbTpl_X.MouseWheel, tbTpl_Y.MouseWheel, tbRef_Xsize.MouseWheel, tbRef_Ysize.MouseWheel, tbTpl_Xsize.MouseWheel, tbTpl_Ysize.MouseWheel
        Dim OldVal As Integer = CInt(CType(sender, TextBox).Text)
        If e.Delta > 0 Then OldVal += 1 Else OldVal -= 1
        If OldVal < 0 Then OldVal = 0
        CType(sender, TextBox).Text = OldVal.ToString.Trim
    End Sub

    Private Sub tbRefFile_TextChanged(sender As Object, e As EventArgs) Handles tbRefFile.TextChanged
        tbRefFile.BackColor = Color.Red
    End Sub

    Private Sub tbTemplateFile_TextChanged(sender As Object, e As EventArgs) Handles tbTemplateFile.TextChanged
        tbTemplateFile.BackColor = Color.Red
    End Sub

    Private Sub btnMultiAreaXCorr_Click(sender As Object, e As EventArgs) Handles btnMultiAreaXCorr.Click


    End Sub

End Class