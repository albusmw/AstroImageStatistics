﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_Open = New ToolStripMenuItem()
        tsmiFile_OpenRecent = New ToolStripMenuItem()
        tsmiFile_OpenLastFile = New ToolStripMenuItem()
        tsmiFile_QHY600Preview = New ToolStripMenuItem()
        ToolStripMenuItem8 = New ToolStripSeparator()
        tsmiFile_FITSGrep = New ToolStripMenuItem()
        tsmiFile_AstroBinSearch = New ToolStripMenuItem()
        ToolStripMenuItem11 = New ToolStripSeparator()
        tsmiFile_Compress2nd = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        tsmiFile_SaveLastStat = New ToolStripMenuItem()
        tsmiSaveAllFilesStat = New ToolStripMenuItem()
        tsmiSaveFITSAndStats = New ToolStripMenuItem()
        tsmiSaveImageData = New ToolStripMenuItem()
        tsmiFile_ConvertTo16BitFITS = New ToolStripMenuItem()
        ToolStripMenuItem14 = New ToolStripSeparator()
        tsmiFile_ClearStatMem = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        OpenEXELocationToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem5 = New ToolStripSeparator()
        ExitToolStripMenuItem = New ToolStripMenuItem()
        tsmiAnalysis = New ToolStripMenuItem()
        tsmiAnalysis_RowColStat = New ToolStripMenuItem()
        tsmiAnalysis_Plot = New ToolStripMenuItem()
        tsmiAnalysis_Plot_Replot = New ToolStripMenuItem()
        tsmiAnalysis_Plot_ADUQuant = New ToolStripMenuItem()
        tsmiAnalysisHotPixel = New ToolStripMenuItem()
        tsmiAnalysisHotPixel_fixfile = New ToolStripMenuItem()
        ToolStripMenuItem7 = New ToolStripSeparator()
        tsmiAnalysis_ManualColorBalancer = New ToolStripMenuItem()
        tsmiAnalysisVignette = New ToolStripMenuItem()
        tsmiAnalysisVignette_CalcRaw = New ToolStripMenuItem()
        tsmiAnalysisVignette_CalcParam = New ToolStripMenuItem()
        tsmiAnalysisVignette_Correct = New ToolStripMenuItem()
        tsmiAnalysisVignette_Display = New ToolStripMenuItem()
        ToolStripMenuItem10 = New ToolStripSeparator()
        tsmiAnalysisVignette_Clear = New ToolStripMenuItem()
        tsmiAnalysisPixelMap = New ToolStripMenuItem()
        tsmiAnalysisPixelMap_SaveFor = New ToolStripMenuItem()
        tsmiAnalysis_RawFITSHeader = New ToolStripMenuItem()
        tsmiAnalysis_FloatAsIntError = New ToolStripMenuItem()
        tsmiAnalysis_MultiFile = New ToolStripMenuItem()
        tsmiAnalysis_MultiFile_Open = New ToolStripMenuItem()
        tsmiAnalysis_MultiFile_LoadAbove = New ToolStripMenuItem()
        tsmiAnalysis_XvsYPlot = New ToolStripMenuItem()
        tsmiAnalysis_FindStars = New ToolStripMenuItem()
        tsmiProcessing = New ToolStripMenuItem()
        tsmiProcessing_AdjustRGB = New ToolStripMenuItem()
        tsmiStretch = New ToolStripMenuItem()
        tsmiPlateSolve = New ToolStripMenuItem()
        tsmiSetPixelToValue = New ToolStripMenuItem()
        tsmiProcessing_MedianFilter = New ToolStripMenuItem()
        SubtractMedianToolStripMenuItem = New ToolStripMenuItem()
        tsmiProcessing_LSBMSB = New ToolStripMenuItem()
        tsmiProcessing_Specials = New ToolStripMenuItem()
        tsmiProcessing_Specials_NINAFix = New ToolStripMenuItem()
        tsmiProcessing_Stack = New ToolStripMenuItem()
        tsmiHotPixelFilter = New ToolStripMenuItem()
        tsmiProc_Bin2OpenCV = New ToolStripMenuItem()
        tsmiProc_Bin2Median = New ToolStripMenuItem()
        tsmiProc_Bin2MaxOut = New ToolStripMenuItem()
        tsmiProcessing_AlignTIFFFiles = New ToolStripMenuItem()
        tsmiTools = New ToolStripMenuItem()
        tsmiTools_ALADINCoords = New ToolStripMenuItem()
        tsmiTools_ChangeHeader = New ToolStripMenuItem()
        tsmiTools_RemoveOverscan = New ToolStripMenuItem()
        tsmiTools_TestFile = New ToolStripMenuItem()
        CheckROICutoutToolStripMenuItem = New ToolStripMenuItem()
        WorkflowToolStripMenuItem = New ToolStripMenuItem()
        MultifileActionToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem13 = New ToolStripSeparator()
        FixRADECErrorToolStripMenuItem = New ToolStripMenuItem()
        tsmiWorkflow_Runner = New ToolStripMenuItem()
        tsmiTest = New ToolStripMenuItem()
        AfiineTranslateToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem6 = New ToolStripSeparator()
        tsmiTest_Focus = New ToolStripMenuItem()
        tssmRadCollimation = New ToolStripMenuItem()
        tsmiTest_ippiXCorr = New ToolStripMenuItem()
        ToolStripMenuItem9 = New ToolStripSeparator()
        tsmiTestCode_UseOpenCV = New ToolStripMenuItem()
        MedianWithinNETToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem12 = New ToolStripSeparator()
        CodeBelowIsNotForHereToolStripMenuItem = New ToolStripMenuItem()
        CloudWatcherCombinerToolStripMenuItem = New ToolStripMenuItem()
        tsmiTest_AstrometryQuery = New ToolStripMenuItem()
        frmTest_HistoInteractive = New ToolStripMenuItem()
        tsmiTestCode_StreamDeck = New ToolStripMenuItem()
        tsmiTest_SigmaClip = New ToolStripMenuItem()
        tsmiTest_FITSReadSpeed = New ToolStripMenuItem()
        DataGridViewDataSourceToolStripMenuItem = New ToolStripMenuItem()
        tsmiTest_RAWReader = New ToolStripMenuItem()
        tsmiTest_RAWReader_NEF = New ToolStripMenuItem()
        tsmiTest_RAWReader_LibRawDLL = New ToolStripMenuItem()
        tsmiTest_RAWReader_GrayPNGToFits = New ToolStripMenuItem()
        tsmiTest_Shannon = New ToolStripMenuItem()
        ofdMain = New OpenFileDialog()
        tbLogOutput = New TextBox()
        ssMain = New StatusStrip()
        tsslRunning = New ToolStripStatusLabel()
        tsslMain = New ToolStripStatusLabel()
        tspbMain = New ToolStripProgressBar()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        tspbMultiFile = New ToolStripProgressBar()
        tsslMultiFile = New ToolStripStatusLabel()
        pgMain = New PropertyGrid()
        sfdMain = New SaveFileDialog()
        gbDetails = New GroupBox()
        tbDetails = New TextBox()
        scMain = New SplitContainer()
        scLeft = New SplitContainer()
        tsMain = New ToolStrip()
        tsb_Open = New ToolStripButton()
        tsb_Display = New ToolStripButton()
        msMain.SuspendLayout()
        ssMain.SuspendLayout()
        gbDetails.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        CType(scLeft, ComponentModel.ISupportInitialize).BeginInit()
        scLeft.Panel1.SuspendLayout()
        scLeft.Panel2.SuspendLayout()
        scLeft.SuspendLayout()
        tsMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' msMain
        ' 
        msMain.ImageScalingSize = New Size(24, 24)
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiAnalysis, tsmiProcessing, tsmiTools, WorkflowToolStripMenuItem, tsmiTest})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Padding = New Padding(6, 1, 0, 1)
        msMain.Size = New Size(2256, 26)
        msMain.TabIndex = 0
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_Open, tsmiFile_OpenRecent, tsmiFile_OpenLastFile, tsmiFile_QHY600Preview, ToolStripMenuItem8, tsmiFile_FITSGrep, tsmiFile_AstroBinSearch, ToolStripMenuItem11, tsmiFile_Compress2nd, ToolStripMenuItem2, tsmiFile_SaveLastStat, tsmiSaveAllFilesStat, tsmiSaveFITSAndStats, tsmiSaveImageData, tsmiFile_ConvertTo16BitFITS, ToolStripMenuItem14, tsmiFile_ClearStatMem, ToolStripMenuItem1, OpenEXELocationToolStripMenuItem, ToolStripMenuItem5, ExitToolStripMenuItem})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(46, 24)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_Open
        ' 
        tsmiFile_Open.Name = "tsmiFile_Open"
        tsmiFile_Open.ShortcutKeys = Keys.Control Or Keys.O
        tsmiFile_Open.Size = New Size(466, 26)
        tsmiFile_Open.Text = "Open file to analyse"
        tsmiFile_Open.ToolTipText = "Open one or more files for analysis"
        ' 
        ' tsmiFile_OpenRecent
        ' 
        tsmiFile_OpenRecent.Name = "tsmiFile_OpenRecent"
        tsmiFile_OpenRecent.Size = New Size(466, 26)
        tsmiFile_OpenRecent.Text = "Open recent files ..."
        tsmiFile_OpenRecent.ToolTipText = "Open recent used files"
        ' 
        ' tsmiFile_OpenLastFile
        ' 
        tsmiFile_OpenLastFile.Name = "tsmiFile_OpenLastFile"
        tsmiFile_OpenLastFile.ShortcutKeys = Keys.Control Or Keys.R
        tsmiFile_OpenLastFile.Size = New Size(466, 26)
        tsmiFile_OpenLastFile.Text = "Open last file processed in assoociated software"
        tsmiFile_OpenLastFile.ToolTipText = "Use the standard configured viewer to open the last opened file (e.g. with FITSWork)"
        ' 
        ' tsmiFile_QHY600Preview
        ' 
        tsmiFile_QHY600Preview.Name = "tsmiFile_QHY600Preview"
        tsmiFile_QHY600Preview.Size = New Size(466, 26)
        tsmiFile_QHY600Preview.Text = "Preview processing QHY600M"
        ' 
        ' ToolStripMenuItem8
        ' 
        ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        ToolStripMenuItem8.Size = New Size(463, 6)
        ' 
        ' tsmiFile_FITSGrep
        ' 
        tsmiFile_FITSGrep.Name = "tsmiFile_FITSGrep"
        tsmiFile_FITSGrep.Size = New Size(466, 26)
        tsmiFile_FITSGrep.Text = "FITS Grep"
        ' 
        ' tsmiFile_AstroBinSearch
        ' 
        tsmiFile_AstroBinSearch.Name = "tsmiFile_AstroBinSearch"
        tsmiFile_AstroBinSearch.Size = New Size(466, 26)
        tsmiFile_AstroBinSearch.Text = "AstroBin search"
        ' 
        ' ToolStripMenuItem11
        ' 
        ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        ToolStripMenuItem11.Size = New Size(463, 6)
        ' 
        ' tsmiFile_Compress2nd
        ' 
        tsmiFile_Compress2nd.Name = "tsmiFile_Compress2nd"
        tsmiFile_Compress2nd.Size = New Size(466, 26)
        tsmiFile_Compress2nd.Text = "Compress 2nd file"
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(463, 6)
        ' 
        ' tsmiFile_SaveLastStat
        ' 
        tsmiFile_SaveLastStat.Name = "tsmiFile_SaveLastStat"
        tsmiFile_SaveLastStat.ShortcutKeys = Keys.Control Or Keys.E
        tsmiFile_SaveLastStat.Size = New Size(466, 26)
        tsmiFile_SaveLastStat.Text = "Save last image statistics (EXCEL)"
        ' 
        ' tsmiSaveAllFilesStat
        ' 
        tsmiSaveAllFilesStat.Name = "tsmiSaveAllFilesStat"
        tsmiSaveAllFilesStat.Size = New Size(466, 26)
        tsmiSaveAllFilesStat.Text = "Save all-files statistics"
        ' 
        ' tsmiSaveFITSAndStats
        ' 
        tsmiSaveFITSAndStats.Name = "tsmiSaveFITSAndStats"
        tsmiSaveFITSAndStats.Size = New Size(466, 26)
        tsmiSaveFITSAndStats.Text = "Save FITS and statistics summary"
        ' 
        ' tsmiSaveImageData
        ' 
        tsmiSaveImageData.Name = "tsmiSaveImageData"
        tsmiSaveImageData.ShortcutKeys = Keys.Control Or Keys.S
        tsmiSaveImageData.Size = New Size(466, 26)
        tsmiSaveImageData.Text = "Save current image"
        ' 
        ' tsmiFile_ConvertTo16BitFITS
        ' 
        tsmiFile_ConvertTo16BitFITS.Name = "tsmiFile_ConvertTo16BitFITS"
        tsmiFile_ConvertTo16BitFITS.Size = New Size(466, 26)
        tsmiFile_ConvertTo16BitFITS.Text = "Make file 16-bit FITS file"
        ' 
        ' ToolStripMenuItem14
        ' 
        ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        ToolStripMenuItem14.Size = New Size(463, 6)
        ' 
        ' tsmiFile_ClearStatMem
        ' 
        tsmiFile_ClearStatMem.Name = "tsmiFile_ClearStatMem"
        tsmiFile_ClearStatMem.Size = New Size(466, 26)
        tsmiFile_ClearStatMem.Text = "Clear statistics memory"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(463, 6)
        ' 
        ' OpenEXELocationToolStripMenuItem
        ' 
        OpenEXELocationToolStripMenuItem.Name = "OpenEXELocationToolStripMenuItem"
        OpenEXELocationToolStripMenuItem.Size = New Size(466, 26)
        OpenEXELocationToolStripMenuItem.Text = "Open EXE location"
        ' 
        ' ToolStripMenuItem5
        ' 
        ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        ToolStripMenuItem5.Size = New Size(463, 6)
        ' 
        ' ExitToolStripMenuItem
        ' 
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ExitToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.X
        ExitToolStripMenuItem.Size = New Size(466, 26)
        ExitToolStripMenuItem.Text = "Exit"
        ' 
        ' tsmiAnalysis
        ' 
        tsmiAnalysis.DropDownItems.AddRange(New ToolStripItem() {tsmiAnalysis_RowColStat, tsmiAnalysis_Plot, tsmiAnalysisHotPixel, ToolStripMenuItem7, tsmiAnalysis_ManualColorBalancer, tsmiAnalysisVignette, tsmiAnalysisPixelMap, tsmiAnalysis_RawFITSHeader, tsmiAnalysis_FloatAsIntError, tsmiAnalysis_MultiFile, tsmiAnalysis_XvsYPlot, tsmiAnalysis_FindStars})
        tsmiAnalysis.Name = "tsmiAnalysis"
        tsmiAnalysis.Size = New Size(76, 24)
        tsmiAnalysis.Text = "Analysis"
        ' 
        ' tsmiAnalysis_RowColStat
        ' 
        tsmiAnalysis_RowColStat.Name = "tsmiAnalysis_RowColStat"
        tsmiAnalysis_RowColStat.Size = New Size(278, 26)
        tsmiAnalysis_RowColStat.Text = "Row and column statistics"
        ' 
        ' tsmiAnalysis_Plot
        ' 
        tsmiAnalysis_Plot.DropDownItems.AddRange(New ToolStripItem() {tsmiAnalysis_Plot_Replot, tsmiAnalysis_Plot_ADUQuant})
        tsmiAnalysis_Plot.Name = "tsmiAnalysis_Plot"
        tsmiAnalysis_Plot.ShortcutKeys = Keys.Control Or Keys.P
        tsmiAnalysis_Plot.Size = New Size(278, 26)
        tsmiAnalysis_Plot.Text = "Plot"
        ' 
        ' tsmiAnalysis_Plot_Replot
        ' 
        tsmiAnalysis_Plot_Replot.Name = "tsmiAnalysis_Plot_Replot"
        tsmiAnalysis_Plot_Replot.Size = New Size(210, 26)
        tsmiAnalysis_Plot_Replot.Text = "Re-plot statistics"
        ' 
        ' tsmiAnalysis_Plot_ADUQuant
        ' 
        tsmiAnalysis_Plot_ADUQuant.Name = "tsmiAnalysis_Plot_ADUQuant"
        tsmiAnalysis_Plot_ADUQuant.Size = New Size(210, 26)
        tsmiAnalysis_Plot_ADUQuant.Text = "ADU quantization"
        ' 
        ' tsmiAnalysisHotPixel
        ' 
        tsmiAnalysisHotPixel.DropDownItems.AddRange(New ToolStripItem() {tsmiAnalysisHotPixel_fixfile})
        tsmiAnalysisHotPixel.Name = "tsmiAnalysisHotPixel"
        tsmiAnalysisHotPixel.Size = New Size(278, 26)
        tsmiAnalysisHotPixel.Text = "Hot pixel"
        ' 
        ' tsmiAnalysisHotPixel_fixfile
        ' 
        tsmiAnalysisHotPixel_fixfile.Name = "tsmiAnalysisHotPixel_fixfile"
        tsmiAnalysisHotPixel_fixfile.Size = New Size(200, 26)
        tsmiAnalysisHotPixel_fixfile.Text = "Fix based on file"
        ' 
        ' ToolStripMenuItem7
        ' 
        ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        ToolStripMenuItem7.Size = New Size(275, 6)
        ' 
        ' tsmiAnalysis_ManualColorBalancer
        ' 
        tsmiAnalysis_ManualColorBalancer.Name = "tsmiAnalysis_ManualColorBalancer"
        tsmiAnalysis_ManualColorBalancer.Size = New Size(278, 26)
        tsmiAnalysis_ManualColorBalancer.Text = "Manual color balancer"
        ' 
        ' tsmiAnalysisVignette
        ' 
        tsmiAnalysisVignette.DropDownItems.AddRange(New ToolStripItem() {tsmiAnalysisVignette_CalcRaw, tsmiAnalysisVignette_CalcParam, tsmiAnalysisVignette_Correct, tsmiAnalysisVignette_Display, ToolStripMenuItem10, tsmiAnalysisVignette_Clear})
        tsmiAnalysisVignette.Name = "tsmiAnalysisVignette"
        tsmiAnalysisVignette.Size = New Size(278, 26)
        tsmiAnalysisVignette.Text = "Vignette"
        ' 
        ' tsmiAnalysisVignette_CalcRaw
        ' 
        tsmiAnalysisVignette_CalcRaw.Name = "tsmiAnalysisVignette_CalcRaw"
        tsmiAnalysisVignette_CalcRaw.Size = New Size(300, 26)
        tsmiAnalysisVignette_CalcRaw.Text = "Calculate raw data (no binning)"
        ' 
        ' tsmiAnalysisVignette_CalcParam
        ' 
        tsmiAnalysisVignette_CalcParam.Name = "tsmiAnalysisVignette_CalcParam"
        tsmiAnalysisVignette_CalcParam.Size = New Size(300, 26)
        tsmiAnalysisVignette_CalcParam.Text = "Calculate fitting parameters"
        ' 
        ' tsmiAnalysisVignette_Correct
        ' 
        tsmiAnalysisVignette_Correct.Name = "tsmiAnalysisVignette_Correct"
        tsmiAnalysisVignette_Correct.Size = New Size(300, 26)
        tsmiAnalysisVignette_Correct.Text = "Correct (only for BIN=0)"
        ' 
        ' tsmiAnalysisVignette_Display
        ' 
        tsmiAnalysisVignette_Display.Name = "tsmiAnalysisVignette_Display"
        tsmiAnalysisVignette_Display.Size = New Size(300, 26)
        tsmiAnalysisVignette_Display.Text = "Display"
        ' 
        ' ToolStripMenuItem10
        ' 
        ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        ToolStripMenuItem10.Size = New Size(297, 6)
        ' 
        ' tsmiAnalysisVignette_Clear
        ' 
        tsmiAnalysisVignette_Clear.Name = "tsmiAnalysisVignette_Clear"
        tsmiAnalysisVignette_Clear.Size = New Size(300, 26)
        tsmiAnalysisVignette_Clear.Text = "Clear"
        ' 
        ' tsmiAnalysisPixelMap
        ' 
        tsmiAnalysisPixelMap.DropDownItems.AddRange(New ToolStripItem() {tsmiAnalysisPixelMap_SaveFor})
        tsmiAnalysisPixelMap.Name = "tsmiAnalysisPixelMap"
        tsmiAnalysisPixelMap.Size = New Size(278, 26)
        tsmiAnalysisPixelMap.Text = "Pixel map files"
        ' 
        ' tsmiAnalysisPixelMap_SaveFor
        ' 
        tsmiAnalysisPixelMap_SaveFor.Name = "tsmiAnalysisPixelMap_SaveFor"
        tsmiAnalysisPixelMap_SaveFor.Size = New Size(277, 26)
        tsmiAnalysisPixelMap_SaveFor.Text = "Save pixel coordinates for ..."
        ' 
        ' tsmiAnalysis_RawFITSHeader
        ' 
        tsmiAnalysis_RawFITSHeader.Name = "tsmiAnalysis_RawFITSHeader"
        tsmiAnalysis_RawFITSHeader.Size = New Size(278, 26)
        tsmiAnalysis_RawFITSHeader.Text = "Raw FITS Header"
        ' 
        ' tsmiAnalysis_FloatAsIntError
        ' 
        tsmiAnalysis_FloatAsIntError.Name = "tsmiAnalysis_FloatAsIntError"
        tsmiAnalysis_FloatAsIntError.Size = New Size(278, 26)
        tsmiAnalysis_FloatAsIntError.Text = "Float error to int"
        ' 
        ' tsmiAnalysis_MultiFile
        ' 
        tsmiAnalysis_MultiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiAnalysis_MultiFile_Open, tsmiAnalysis_MultiFile_LoadAbove})
        tsmiAnalysis_MultiFile.Name = "tsmiAnalysis_MultiFile"
        tsmiAnalysis_MultiFile.Size = New Size(278, 26)
        tsmiAnalysis_MultiFile.Text = "Multi-file pixelwise statistics"
        ' 
        ' tsmiAnalysis_MultiFile_Open
        ' 
        tsmiAnalysis_MultiFile_Open.Name = "tsmiAnalysis_MultiFile_Open"
        tsmiAnalysis_MultiFile_Open.Size = New Size(243, 26)
        tsmiAnalysis_MultiFile_Open.Text = "Open analysis  window"
        ' 
        ' tsmiAnalysis_MultiFile_LoadAbove
        ' 
        tsmiAnalysis_MultiFile_LoadAbove.Name = "tsmiAnalysis_MultiFile_LoadAbove"
        tsmiAnalysis_MultiFile_LoadAbove.Size = New Size(243, 26)
        tsmiAnalysis_MultiFile_LoadAbove.Text = "Load certain pixel ..."
        ' 
        ' tsmiAnalysis_XvsYPlot
        ' 
        tsmiAnalysis_XvsYPlot.Name = "tsmiAnalysis_XvsYPlot"
        tsmiAnalysis_XvsYPlot.Size = New Size(278, 26)
        tsmiAnalysis_XvsYPlot.Text = "X-vs-Y plots"
        ' 
        ' tsmiAnalysis_FindStars
        ' 
        tsmiAnalysis_FindStars.Name = "tsmiAnalysis_FindStars"
        tsmiAnalysis_FindStars.Size = New Size(278, 26)
        tsmiAnalysis_FindStars.Text = "Find stars"
        ' 
        ' tsmiProcessing
        ' 
        tsmiProcessing.DropDownItems.AddRange(New ToolStripItem() {tsmiProcessing_AdjustRGB, tsmiStretch, tsmiPlateSolve, tsmiSetPixelToValue, tsmiProcessing_MedianFilter, SubtractMedianToolStripMenuItem, tsmiProcessing_LSBMSB, tsmiProcessing_Specials, tsmiProcessing_Stack, tsmiHotPixelFilter, tsmiProc_Bin2OpenCV, tsmiProc_Bin2Median, tsmiProc_Bin2MaxOut, tsmiProcessing_AlignTIFFFiles})
        tsmiProcessing.Name = "tsmiProcessing"
        tsmiProcessing.Size = New Size(93, 24)
        tsmiProcessing.Text = "Processing"
        ' 
        ' tsmiProcessing_AdjustRGB
        ' 
        tsmiProcessing_AdjustRGB.Name = "tsmiProcessing_AdjustRGB"
        tsmiProcessing_AdjustRGB.Size = New Size(377, 26)
        tsmiProcessing_AdjustRGB.Text = "Adjust RGB channels (using modus)"
        ' 
        ' tsmiStretch
        ' 
        tsmiStretch.Name = "tsmiStretch"
        tsmiStretch.Size = New Size(377, 26)
        tsmiStretch.Text = "Stretcher histogramm over complete range"
        ' 
        ' tsmiPlateSolve
        ' 
        tsmiPlateSolve.Name = "tsmiPlateSolve"
        tsmiPlateSolve.Size = New Size(377, 26)
        tsmiPlateSolve.Text = "Plate solve image"
        ' 
        ' tsmiSetPixelToValue
        ' 
        tsmiSetPixelToValue.Name = "tsmiSetPixelToValue"
        tsmiSetPixelToValue.Size = New Size(377, 26)
        tsmiSetPixelToValue.Text = "Set pixel above to certain value"
        ' 
        ' tsmiProcessing_MedianFilter
        ' 
        tsmiProcessing_MedianFilter.Name = "tsmiProcessing_MedianFilter"
        tsmiProcessing_MedianFilter.Size = New Size(377, 26)
        tsmiProcessing_MedianFilter.Text = "Median filter"
        ' 
        ' SubtractMedianToolStripMenuItem
        ' 
        SubtractMedianToolStripMenuItem.Name = "SubtractMedianToolStripMenuItem"
        SubtractMedianToolStripMenuItem.Size = New Size(377, 26)
        SubtractMedianToolStripMenuItem.Text = "Subtract median"
        ' 
        ' tsmiProcessing_LSBMSB
        ' 
        tsmiProcessing_LSBMSB.Name = "tsmiProcessing_LSBMSB"
        tsmiProcessing_LSBMSB.Size = New Size(377, 26)
        tsmiProcessing_LSBMSB.Text = "LSB-MSB swap"
        ' 
        ' tsmiProcessing_Specials
        ' 
        tsmiProcessing_Specials.DropDownItems.AddRange(New ToolStripItem() {tsmiProcessing_Specials_NINAFix})
        tsmiProcessing_Specials.Name = "tsmiProcessing_Specials"
        tsmiProcessing_Specials.Size = New Size(377, 26)
        tsmiProcessing_Specials.Text = "Specials"
        ' 
        ' tsmiProcessing_Specials_NINAFix
        ' 
        tsmiProcessing_Specials_NINAFix.Name = "tsmiProcessing_Specials_NINAFix"
        tsmiProcessing_Specials_NINAFix.Size = New Size(160, 26)
        tsmiProcessing_Specials_NINAFix.Text = "NINA fix 1"
        ' 
        ' tsmiProcessing_Stack
        ' 
        tsmiProcessing_Stack.Name = "tsmiProcessing_Stack"
        tsmiProcessing_Stack.ShortcutKeys = Keys.Alt Or Keys.S
        tsmiProcessing_Stack.Size = New Size(377, 26)
        tsmiProcessing_Stack.Text = "Stacking multiple files"
        ' 
        ' tsmiHotPixelFilter
        ' 
        tsmiHotPixelFilter.Name = "tsmiHotPixelFilter"
        tsmiHotPixelFilter.Size = New Size(377, 26)
        tsmiHotPixelFilter.Text = "Special hotpixel filter - EXPERIMENTAL"
        ' 
        ' tsmiProc_Bin2OpenCV
        ' 
        tsmiProc_Bin2OpenCV.Name = "tsmiProc_Bin2OpenCV"
        tsmiProc_Bin2OpenCV.Size = New Size(377, 26)
        tsmiProc_Bin2OpenCV.Text = "Bin 2 with OpenCV"
        ' 
        ' tsmiProc_Bin2Median
        ' 
        tsmiProc_Bin2Median.Name = "tsmiProc_Bin2Median"
        tsmiProc_Bin2Median.Size = New Size(377, 26)
        tsmiProc_Bin2Median.Text = "Bin 2 with median"
        ' 
        ' tsmiProc_Bin2MaxOut
        ' 
        tsmiProc_Bin2MaxOut.Name = "tsmiProc_Bin2MaxOut"
        tsmiProc_Bin2MaxOut.Size = New Size(377, 26)
        tsmiProc_Bin2MaxOut.Text = "Bin 2 with max removal"
        ' 
        ' tsmiProcessing_AlignTIFFFiles
        ' 
        tsmiProcessing_AlignTIFFFiles.Name = "tsmiProcessing_AlignTIFFFiles"
        tsmiProcessing_AlignTIFFFiles.Size = New Size(377, 26)
        tsmiProcessing_AlignTIFFFiles.Text = "Align TIFF files (from DSS)"
        ' 
        ' tsmiTools
        ' 
        tsmiTools.DropDownItems.AddRange(New ToolStripItem() {tsmiTools_ALADINCoords, tsmiTools_ChangeHeader, tsmiTools_RemoveOverscan, tsmiTools_TestFile, CheckROICutoutToolStripMenuItem})
        tsmiTools.Name = "tsmiTools"
        tsmiTools.Size = New Size(58, 24)
        tsmiTools.Text = "Tools"
        ' 
        ' tsmiTools_ALADINCoords
        ' 
        tsmiTools_ALADINCoords.Name = "tsmiTools_ALADINCoords"
        tsmiTools_ALADINCoords.Size = New Size(268, 26)
        tsmiTools_ALADINCoords.Text = "Coords for ALADIN call"
        ' 
        ' tsmiTools_ChangeHeader
        ' 
        tsmiTools_ChangeHeader.Name = "tsmiTools_ChangeHeader"
        tsmiTools_ChangeHeader.Size = New Size(268, 26)
        tsmiTools_ChangeHeader.Text = "Change header"
        ' 
        ' tsmiTools_RemoveOverscan
        ' 
        tsmiTools_RemoveOverscan.Name = "tsmiTools_RemoveOverscan"
        tsmiTools_RemoveOverscan.Size = New Size(268, 26)
        tsmiTools_RemoveOverscan.Text = "Remove QHY600 Overscan"
        ' 
        ' tsmiTools_TestFile
        ' 
        tsmiTools_TestFile.Name = "tsmiTools_TestFile"
        tsmiTools_TestFile.Size = New Size(268, 26)
        tsmiTools_TestFile.Text = "Test file generator"
        ' 
        ' CheckROICutoutToolStripMenuItem
        ' 
        CheckROICutoutToolStripMenuItem.Name = "CheckROICutoutToolStripMenuItem"
        CheckROICutoutToolStripMenuItem.Size = New Size(268, 26)
        CheckROICutoutToolStripMenuItem.Text = "Check ROI cut-out"
        ' 
        ' WorkflowToolStripMenuItem
        ' 
        WorkflowToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {MultifileActionToolStripMenuItem, ToolStripMenuItem13, FixRADECErrorToolStripMenuItem, tsmiWorkflow_Runner})
        WorkflowToolStripMenuItem.Name = "WorkflowToolStripMenuItem"
        WorkflowToolStripMenuItem.Size = New Size(86, 24)
        WorkflowToolStripMenuItem.Text = "Workflow"
        ' 
        ' MultifileActionToolStripMenuItem
        ' 
        MultifileActionToolStripMenuItem.Name = "MultifileActionToolStripMenuItem"
        MultifileActionToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.M
        MultifileActionToolStripMenuItem.Size = New Size(257, 26)
        MultifileActionToolStripMenuItem.Text = "Multi-file action"
        ' 
        ' ToolStripMenuItem13
        ' 
        ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        ToolStripMenuItem13.Size = New Size(254, 6)
        ' 
        ' FixRADECErrorToolStripMenuItem
        ' 
        FixRADECErrorToolStripMenuItem.Name = "FixRADECErrorToolStripMenuItem"
        FixRADECErrorToolStripMenuItem.Size = New Size(257, 26)
        FixRADECErrorToolStripMenuItem.Text = "Fix RA_DEC error"
        ' 
        ' tsmiWorkflow_Runner
        ' 
        tsmiWorkflow_Runner.Name = "tsmiWorkflow_Runner"
        tsmiWorkflow_Runner.Size = New Size(257, 26)
        tsmiWorkflow_Runner.Text = "Open runner"
        ' 
        ' tsmiTest
        ' 
        tsmiTest.DropDownItems.AddRange(New ToolStripItem() {AfiineTranslateToolStripMenuItem, ToolStripMenuItem6, tsmiTest_Focus, tssmRadCollimation, tsmiTest_ippiXCorr, ToolStripMenuItem9, tsmiTestCode_UseOpenCV, MedianWithinNETToolStripMenuItem, ToolStripMenuItem12, CodeBelowIsNotForHereToolStripMenuItem, CloudWatcherCombinerToolStripMenuItem, tsmiTest_AstrometryQuery, frmTest_HistoInteractive, tsmiTestCode_StreamDeck, tsmiTest_SigmaClip, tsmiTest_FITSReadSpeed, DataGridViewDataSourceToolStripMenuItem, tsmiTest_RAWReader, tsmiTest_Shannon})
        tsmiTest.Name = "tsmiTest"
        tsmiTest.Size = New Size(86, 24)
        tsmiTest.Text = "Test code"
        ' 
        ' AfiineTranslateToolStripMenuItem
        ' 
        AfiineTranslateToolStripMenuItem.Name = "AfiineTranslateToolStripMenuItem"
        AfiineTranslateToolStripMenuItem.Size = New Size(344, 26)
        AfiineTranslateToolStripMenuItem.Text = "Afiine translate"
        ' 
        ' ToolStripMenuItem6
        ' 
        ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        ToolStripMenuItem6.Size = New Size(341, 6)
        ' 
        ' tsmiTest_Focus
        ' 
        tsmiTest_Focus.Name = "tsmiTest_Focus"
        tsmiTest_Focus.Size = New Size(344, 26)
        tsmiTest_Focus.Text = "Focus"
        ' 
        ' tssmRadCollimation
        ' 
        tssmRadCollimation.Name = "tssmRadCollimation"
        tssmRadCollimation.Size = New Size(344, 26)
        tssmRadCollimation.Text = "Radial statistics and collimation"
        ' 
        ' tsmiTest_ippiXCorr
        ' 
        tsmiTest_ippiXCorr.Name = "tsmiTest_ippiXCorr"
        tsmiTest_ippiXCorr.Size = New Size(344, 26)
        tsmiTest_ippiXCorr.Text = "ippi XCorr"
        ' 
        ' ToolStripMenuItem9
        ' 
        ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        ToolStripMenuItem9.Size = New Size(341, 6)
        ' 
        ' tsmiTestCode_UseOpenCV
        ' 
        tsmiTestCode_UseOpenCV.Name = "tsmiTestCode_UseOpenCV"
        tsmiTestCode_UseOpenCV.Size = New Size(344, 26)
        tsmiTestCode_UseOpenCV.Text = "Use OpenCV"
        ' 
        ' MedianWithinNETToolStripMenuItem
        ' 
        MedianWithinNETToolStripMenuItem.Name = "MedianWithinNETToolStripMenuItem"
        MedianWithinNETToolStripMenuItem.Size = New Size(344, 26)
        MedianWithinNETToolStripMenuItem.Text = ".NET Median Filter (for reference only)"
        ' 
        ' ToolStripMenuItem12
        ' 
        ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        ToolStripMenuItem12.Size = New Size(341, 6)
        ' 
        ' CodeBelowIsNotForHereToolStripMenuItem
        ' 
        CodeBelowIsNotForHereToolStripMenuItem.Name = "CodeBelowIsNotForHereToolStripMenuItem"
        CodeBelowIsNotForHereToolStripMenuItem.Size = New Size(344, 26)
        CodeBelowIsNotForHereToolStripMenuItem.Text = "(Code below is not for here ...)"
        ' 
        ' CloudWatcherCombinerToolStripMenuItem
        ' 
        CloudWatcherCombinerToolStripMenuItem.Name = "CloudWatcherCombinerToolStripMenuItem"
        CloudWatcherCombinerToolStripMenuItem.Size = New Size(344, 26)
        CloudWatcherCombinerToolStripMenuItem.Text = "CloudWatcher combiner"
        ' 
        ' tsmiTest_AstrometryQuery
        ' 
        tsmiTest_AstrometryQuery.Name = "tsmiTest_AstrometryQuery"
        tsmiTest_AstrometryQuery.Size = New Size(344, 26)
        tsmiTest_AstrometryQuery.Text = "Astrometry Batch Query"
        ' 
        ' frmTest_HistoInteractive
        ' 
        frmTest_HistoInteractive.Name = "frmTest_HistoInteractive"
        frmTest_HistoInteractive.Size = New Size(344, 26)
        frmTest_HistoInteractive.Text = "Interactive Histogram"
        ' 
        ' tsmiTestCode_StreamDeck
        ' 
        tsmiTestCode_StreamDeck.Name = "tsmiTestCode_StreamDeck"
        tsmiTestCode_StreamDeck.Size = New Size(344, 26)
        tsmiTestCode_StreamDeck.Text = "StreamDeck"
        ' 
        ' tsmiTest_SigmaClip
        ' 
        tsmiTest_SigmaClip.Name = "tsmiTest_SigmaClip"
        tsmiTest_SigmaClip.Size = New Size(344, 26)
        tsmiTest_SigmaClip.Text = "Sigma clip"
        ' 
        ' tsmiTest_FITSReadSpeed
        ' 
        tsmiTest_FITSReadSpeed.Name = "tsmiTest_FITSReadSpeed"
        tsmiTest_FITSReadSpeed.Size = New Size(344, 26)
        tsmiTest_FITSReadSpeed.Text = "FITS read speed"
        ' 
        ' DataGridViewDataSourceToolStripMenuItem
        ' 
        DataGridViewDataSourceToolStripMenuItem.Name = "DataGridViewDataSourceToolStripMenuItem"
        DataGridViewDataSourceToolStripMenuItem.Size = New Size(344, 26)
        DataGridViewDataSourceToolStripMenuItem.Text = "DataGridView data source"
        ' 
        ' tsmiTest_RAWReader
        ' 
        tsmiTest_RAWReader.DropDownItems.AddRange(New ToolStripItem() {tsmiTest_RAWReader_NEF, tsmiTest_RAWReader_LibRawDLL, tsmiTest_RAWReader_GrayPNGToFits})
        tsmiTest_RAWReader.Name = "tsmiTest_RAWReader"
        tsmiTest_RAWReader.Size = New Size(344, 26)
        tsmiTest_RAWReader.Text = "File format converter and RAW reader"
        ' 
        ' tsmiTest_RAWReader_NEF
        ' 
        tsmiTest_RAWReader_NEF.Name = "tsmiTest_RAWReader_NEF"
        tsmiTest_RAWReader_NEF.Size = New Size(204, 26)
        tsmiTest_RAWReader_NEF.Text = "NEF reading"
        ' 
        ' tsmiTest_RAWReader_LibRawDLL
        ' 
        tsmiTest_RAWReader_LibRawDLL.Name = "tsmiTest_RAWReader_LibRawDLL"
        tsmiTest_RAWReader_LibRawDLL.Size = New Size(204, 26)
        tsmiTest_RAWReader_LibRawDLL.Text = "LibRaw DLL "
        ' 
        ' tsmiTest_RAWReader_GrayPNGToFits
        ' 
        tsmiTest_RAWReader_GrayPNGToFits.Name = "tsmiTest_RAWReader_GrayPNGToFits"
        tsmiTest_RAWReader_GrayPNGToFits.Size = New Size(204, 26)
        tsmiTest_RAWReader_GrayPNGToFits.Text = "Gray PNG to FITS"
        ' 
        ' tsmiTest_Shannon
        ' 
        tsmiTest_Shannon.Name = "tsmiTest_Shannon"
        tsmiTest_Shannon.Size = New Size(344, 26)
        tsmiTest_Shannon.Text = "Shannon-Fano compression"
        ' 
        ' ofdMain
        ' 
        ofdMain.Multiselect = True
        ' 
        ' tbLogOutput
        ' 
        tbLogOutput.AllowDrop = True
        tbLogOutput.Dock = DockStyle.Fill
        tbLogOutput.Font = New Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbLogOutput.Location = New Point(0, 0)
        tbLogOutput.Margin = New Padding(5, 4, 5, 4)
        tbLogOutput.Multiline = True
        tbLogOutput.Name = "tbLogOutput"
        tbLogOutput.ScrollBars = ScrollBars.Both
        tbLogOutput.Size = New Size(1728, 1299)
        tbLogOutput.TabIndex = 3
        tbLogOutput.WordWrap = False
        ' 
        ' ssMain
        ' 
        ssMain.ImageScalingSize = New Size(24, 24)
        ssMain.Items.AddRange(New ToolStripItem() {tsslRunning, tsslMain, tspbMain, ToolStripStatusLabel1, tspbMultiFile, tsslMultiFile})
        ssMain.Location = New Point(0, 1388)
        ssMain.Name = "ssMain"
        ssMain.Padding = New Padding(1, 0, 11, 0)
        ssMain.Size = New Size(2256, 28)
        ssMain.TabIndex = 4
        ssMain.Text = "StatusStrip1"
        ' 
        ' tsslRunning
        ' 
        tsslRunning.Font = New Font("Wingdings", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(2))
        tsslRunning.ForeColor = Color.Silver
        tsslRunning.Name = "tsslRunning"
        tsslRunning.Size = New Size(19, 22)
        tsslRunning.Text = "l"
        ' 
        ' tsslMain
        ' 
        tsslMain.Name = "tsslMain"
        tsslMain.Size = New Size(27, 22)
        tsslMain.Text = "---"
        ' 
        ' tspbMain
        ' 
        tspbMain.ForeColor = Color.FromArgb(CByte(0), CByte(192), CByte(0))
        tspbMain.Name = "tspbMain"
        tspbMain.Size = New Size(134, 20)
        tspbMain.Style = ProgressBarStyle.Continuous
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(13, 22)
        ToolStripStatusLabel1.Text = "|"
        ' 
        ' tspbMultiFile
        ' 
        tspbMultiFile.Name = "tspbMultiFile"
        tspbMultiFile.Size = New Size(134, 20)
        ' 
        ' tsslMultiFile
        ' 
        tsslMultiFile.Name = "tsslMultiFile"
        tsslMultiFile.Size = New Size(27, 22)
        tsslMultiFile.Text = "---"
        ' 
        ' pgMain
        ' 
        pgMain.Dock = DockStyle.Fill
        pgMain.Location = New Point(0, 0)
        pgMain.Margin = New Padding(5, 4, 5, 4)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(490, 652)
        pgMain.TabIndex = 5
        pgMain.ToolbarVisible = False
        ' 
        ' gbDetails
        ' 
        gbDetails.Controls.Add(tbDetails)
        gbDetails.Dock = DockStyle.Fill
        gbDetails.Location = New Point(0, 0)
        gbDetails.Margin = New Padding(5, 4, 5, 4)
        gbDetails.Name = "gbDetails"
        gbDetails.Padding = New Padding(5, 4, 5, 4)
        gbDetails.Size = New Size(490, 640)
        gbDetails.TabIndex = 6
        gbDetails.TabStop = False
        gbDetails.Text = "Details"
        ' 
        ' tbDetails
        ' 
        tbDetails.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbDetails.Font = New Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbDetails.Location = New Point(8, 29)
        tbDetails.Margin = New Padding(5, 4, 5, 4)
        tbDetails.Multiline = True
        tbDetails.Name = "tbDetails"
        tbDetails.ScrollBars = ScrollBars.Both
        tbDetails.Size = New Size(473, 600)
        tbDetails.TabIndex = 0
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(16, 80)
        scMain.Margin = New Padding(5, 4, 5, 4)
        scMain.Name = "scMain"
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(scLeft)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(tbLogOutput)
        scMain.Size = New Size(2224, 1299)
        scMain.SplitterDistance = 490
        scMain.SplitterWidth = 6
        scMain.TabIndex = 7
        ' 
        ' scLeft
        ' 
        scLeft.Dock = DockStyle.Fill
        scLeft.Location = New Point(0, 0)
        scLeft.Margin = New Padding(5, 4, 5, 4)
        scLeft.Name = "scLeft"
        scLeft.Orientation = Orientation.Horizontal
        ' 
        ' scLeft.Panel1
        ' 
        scLeft.Panel1.Controls.Add(pgMain)
        ' 
        ' scLeft.Panel2
        ' 
        scLeft.Panel2.Controls.Add(gbDetails)
        scLeft.Size = New Size(490, 1299)
        scLeft.SplitterDistance = 652
        scLeft.SplitterWidth = 7
        scLeft.TabIndex = 0
        ' 
        ' tsMain
        ' 
        tsMain.ImageScalingSize = New Size(24, 24)
        tsMain.Items.AddRange(New ToolStripItem() {tsb_Open, tsb_Display})
        tsMain.Location = New Point(0, 26)
        tsMain.Name = "tsMain"
        tsMain.Padding = New Padding(0, 0, 2, 0)
        tsMain.Size = New Size(2256, 27)
        tsMain.TabIndex = 8
        tsMain.Text = "ToolStrip1"
        ' 
        ' tsb_Open
        ' 
        tsb_Open.DisplayStyle = ToolStripItemDisplayStyle.Text
        tsb_Open.Image = CType(resources.GetObject("tsb_Open.Image"), Image)
        tsb_Open.ImageTransparentColor = Color.Magenta
        tsb_Open.Name = "tsb_Open"
        tsb_Open.Size = New Size(62, 24)
        tsb_Open.Text = "Open ..."
        ' 
        ' tsb_Display
        ' 
        tsb_Display.DisplayStyle = ToolStripItemDisplayStyle.Text
        tsb_Display.Image = CType(resources.GetObject("tsb_Display.Image"), Image)
        tsb_Display.ImageTransparentColor = Color.Magenta
        tsb_Display.Name = "tsb_Display"
        tsb_Display.Size = New Size(62, 24)
        tsb_Display.Text = "Display"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(2256, 1416)
        Controls.Add(tsMain)
        Controls.Add(scMain)
        Controls.Add(ssMain)
        Controls.Add(msMain)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        MainMenuStrip = msMain
        Margin = New Padding(2, 3, 2, 3)
        Name = "MainForm"
        Text = "Astro Image Statistics Version 0.3"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        gbDetails.ResumeLayout(False)
        gbDetails.PerformLayout()
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        scMain.Panel2.PerformLayout()
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        scLeft.Panel1.ResumeLayout(False)
        scLeft.Panel2.ResumeLayout(False)
        CType(scLeft, ComponentModel.ISupportInitialize).EndInit()
        scLeft.ResumeLayout(False)
        tsMain.ResumeLayout(False)
        tsMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_Open As ToolStripMenuItem
    Friend WithEvents ofdMain As OpenFileDialog
    Friend WithEvents tbLogOutput As TextBox
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslMain As ToolStripStatusLabel
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_OpenLastFile As ToolStripMenuItem
    Friend WithEvents tsmiTest As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenEXELocationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents tsmiAnalysis As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_RowColStat As ToolStripMenuItem
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents tsmiAnalysis_Plot As ToolStripMenuItem
    Friend WithEvents AfiineTranslateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiFile_SaveLastStat As ToolStripMenuItem
    Friend WithEvents tsmiProcessing As ToolStripMenuItem
    Friend WithEvents tsmiProcessing_AdjustRGB As ToolStripMenuItem
    Friend WithEvents tsmiSaveImageData As ToolStripMenuItem
    Friend WithEvents sfdMain As SaveFileDialog
    Friend WithEvents tsmiStretch As ToolStripMenuItem
    Friend WithEvents tsslRunning As ToolStripStatusLabel
    Friend WithEvents tsmiPlateSolve As ToolStripMenuItem
    Friend WithEvents tsmiFile_FITSGrep As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripSeparator
    Friend WithEvents tsmiFile_ClearStatMem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As ToolStripSeparator
    Friend WithEvents tsmiTest_Focus As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisHotPixel As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As ToolStripSeparator
    Friend WithEvents tspbMain As ToolStripProgressBar
    Friend WithEvents tsmiFile_OpenRecent As ToolStripMenuItem
    Friend WithEvents gbDetails As GroupBox
    Friend WithEvents tbDetails As TextBox
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents scLeft As SplitContainer
    Friend WithEvents ToolStripMenuItem8 As ToolStripSeparator
    Friend WithEvents tsmiSaveAllFilesStat As ToolStripMenuItem
    Friend WithEvents tsmiSetPixelToValue As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As ToolStripSeparator
    Friend WithEvents tsmiTestCode_UseOpenCV As ToolStripMenuItem
    Friend WithEvents tsmiTools As ToolStripMenuItem
    Friend WithEvents tsmiTools_ALADINCoords As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_ManualColorBalancer As ToolStripMenuItem
    Friend WithEvents tsmiSaveFITSAndStats As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisVignette As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisVignette_CalcRaw As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisVignette_Display As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As ToolStripSeparator
    Friend WithEvents tsmiAnalysisVignette_Clear As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisVignette_Correct As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisVignette_CalcParam As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_Plot_Replot As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_Plot_ADUQuant As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisPixelMap As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisPixelMap_SaveFor As ToolStripMenuItem
    Friend WithEvents tsmiAnalysisHotPixel_fixfile As ToolStripMenuItem
    Friend WithEvents MedianWithinNETToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiProcessing_MedianFilter As ToolStripMenuItem
    Friend WithEvents tsMain As ToolStrip
    Friend WithEvents tsb_Open As ToolStripButton
    Friend WithEvents tsb_Display As ToolStripButton
    Friend WithEvents tsmiFile_AstroBinSearch As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As ToolStripSeparator
    Friend WithEvents tsmiFile_Compress2nd As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_RawFITSHeader As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_FloatAsIntError As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As ToolStripSeparator
    Friend WithEvents CodeBelowIsNotForHereToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloudWatcherCombinerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiTools_ChangeHeader As ToolStripMenuItem
    Friend WithEvents tsmiTools_RemoveOverscan As ToolStripMenuItem
    Friend WithEvents tsmiTools_TestFile As ToolStripMenuItem
    Friend WithEvents CheckROICutoutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WorkflowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiWorkflow_Runner As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_MultiFile As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_MultiFile_Open As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_MultiFile_LoadAbove As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As ToolStripSeparator
    Friend WithEvents FixRADECErrorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SubtractMedianToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiTest_AstrometryQuery As ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents tspbMultiFile As ToolStripProgressBar
    Friend WithEvents tsslMultiFile As ToolStripStatusLabel
    Friend WithEvents tsmiProcessing_LSBMSB As ToolStripMenuItem
    Friend WithEvents tsmiProcessing_Specials As ToolStripMenuItem
    Friend WithEvents tsmiProcessing_Specials_NINAFix As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_XvsYPlot As ToolStripMenuItem
    Friend WithEvents frmTest_HistoInteractive As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem14 As ToolStripSeparator
    Friend WithEvents tsmiTestCode_StreamDeck As ToolStripMenuItem
    Friend WithEvents tsmiProcessing_Stack As ToolStripMenuItem
    Friend WithEvents tssmRadCollimation As ToolStripMenuItem
    Friend WithEvents MultifileActionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiHotPixelFilter As ToolStripMenuItem
    Friend WithEvents tsmiProc_Bin2Median As ToolStripMenuItem
    Friend WithEvents tsmiProc_Bin2MaxOut As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_FindStars As ToolStripMenuItem
    Friend WithEvents tsmiProc_Bin2OpenCV As ToolStripMenuItem
    Friend WithEvents tsmiTest_ippiXCorr As ToolStripMenuItem
    Friend WithEvents tsmiTest_SigmaClip As ToolStripMenuItem
    Friend WithEvents tsmiFile_ConvertTo16BitFITS As ToolStripMenuItem
    Friend WithEvents tsmiTest_FITSReadSpeed As ToolStripMenuItem
    Friend WithEvents DataGridViewDataSourceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiTest_RAWReader As ToolStripMenuItem
    Friend WithEvents tsmiTest_RAWReader_NEF As ToolStripMenuItem
    Friend WithEvents tsmiTest_RAWReader_LibRawDLL As ToolStripMenuItem
    Friend WithEvents tsmiTest_RAWReader_GrayPNGToFits As ToolStripMenuItem
    Friend WithEvents tsmiFile_QHY600Preview As ToolStripMenuItem
    Friend WithEvents tsmiProcessing_AlignTIFFFiles As ToolStripMenuItem
    Friend WithEvents tsmiTest_Shannon As ToolStripMenuItem
End Class
