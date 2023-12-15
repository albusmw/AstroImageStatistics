<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_Open = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_OpenRecent = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_OpenLastFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_FITSGrep = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_AstroBinSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_Compress2nd = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_SaveLastStat = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSaveAllFilesStat = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSaveFITSAndStats = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSaveImageData = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem14 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_ClearStatMem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenEXELocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_RowColStat = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_Plot = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_Plot_Replot = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_Plot_ADUQuant = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisHotPixel = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisHotPixel_detect = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisHotPixel_fixfile = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiAnalysis_MultiAreaCompare = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_ManualColorBalancer = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisVignette = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisVignette_CalcRaw = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisVignette_CalcParam = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisVignette_Correct = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisVignette_Display = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiAnalysisVignette_Clear = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisPixelMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysisPixelMap_SaveFor = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_RawFITSHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_FloatAsIntError = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_MultiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_MultiFile_Open = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_MultiFile_LoadAbove = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_XvsYPlot = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalysis_FindStars = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProcessing = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProcessing_AdjustRGB = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiStretch = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiPlateSolve = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSetPixelToValue = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProcessing_MedianFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.SubtractMedianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProcessing_LSBMSB = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProcessing_Specials = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProcessing_Specials_NINAFix = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProcessing_Stack = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiHotPixelFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProc_Bin2OpenCV = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProc_Bin2Median = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiProc_Bin2MaxOut = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.AfiineTranslateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiTest_Focus = New System.Windows.Forms.ToolStripMenuItem()
        Me.tssmRadCollimation = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTest_ReadNEFFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTest_ippiXCorr = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiTestCode_UseOpenCV = New System.Windows.Forms.ToolStripMenuItem()
        Me.MedianWithinNETToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripSeparator()
        Me.CodeBelowIsNotForHereToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloudWatcherCombinerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GrayPNGToFITSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTest_AstrometryQuery = New System.Windows.Forms.ToolStripMenuItem()
        Me.frmTest_HistoInteractive = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTestCode_StreamDeck = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsTest_LibRaw = New System.Windows.Forms.ToolStripMenuItem()
        Me.SigmaClipToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTools_ALADINCoords = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTools_ChangeHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTools_RemoveOverscan = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiTools_TestFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckROICutoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WorkflowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MultifileActionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripSeparator()
        Me.FixRADECErrorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiWorkflow_Runner = New System.Windows.Forms.ToolStripMenuItem()
        Me.ofdMain = New System.Windows.Forms.OpenFileDialog()
        Me.tbLogOutput = New System.Windows.Forms.TextBox()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslRunning = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslMain = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbMain = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbMultiFile = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsslMultiFile = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pgMain = New System.Windows.Forms.PropertyGrid()
        Me.sfdMain = New System.Windows.Forms.SaveFileDialog()
        Me.gbDetails = New System.Windows.Forms.GroupBox()
        Me.tbDetails = New System.Windows.Forms.TextBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.scLeft = New System.Windows.Forms.SplitContainer()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.tsb_Open = New System.Windows.Forms.ToolStripButton()
        Me.tsb_Display = New System.Windows.Forms.ToolStripButton()
        Me.tsmiFile_ConvertTo16BitFITS = New System.Windows.Forms.ToolStripMenuItem()
        Me.msMain.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.gbDetails.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        CType(Me.scLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scLeft.Panel1.SuspendLayout()
        Me.scLeft.Panel2.SuspendLayout()
        Me.scLeft.SuspendLayout()
        Me.tsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'msMain
        '
        Me.msMain.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile, Me.tsmiAnalysis, Me.tsmiProcessing, Me.tsmiTest, Me.tsmiTools, Me.WorkflowToolStripMenuItem})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.msMain.Size = New System.Drawing.Size(1692, 24)
        Me.msMain.TabIndex = 0
        Me.msMain.Text = "MenuStrip1"
        '
        'tsmiFile
        '
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile_Open, Me.tsmiFile_OpenRecent, Me.tsmiFile_OpenLastFile, Me.ToolStripMenuItem8, Me.tsmiFile_FITSGrep, Me.tsmiFile_AstroBinSearch, Me.ToolStripMenuItem11, Me.tsmiFile_Compress2nd, Me.ToolStripMenuItem2, Me.tsmiFile_SaveLastStat, Me.tsmiSaveAllFilesStat, Me.tsmiSaveFITSAndStats, Me.tsmiSaveImageData, Me.tsmiFile_ConvertTo16BitFITS, Me.ToolStripMenuItem14, Me.tsmiFile_ClearStatMem, Me.ToolStripMenuItem1, Me.OpenEXELocationToolStripMenuItem, Me.ToolStripMenuItem5, Me.ExitToolStripMenuItem})
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Size = New System.Drawing.Size(37, 22)
        Me.tsmiFile.Text = "File"
        '
        'tsmiFile_Open
        '
        Me.tsmiFile_Open.Name = "tsmiFile_Open"
        Me.tsmiFile_Open.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.tsmiFile_Open.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_Open.Text = "Open file(s) to analyse"
        Me.tsmiFile_Open.ToolTipText = "Open one or more files for analysis"
        '
        'tsmiFile_OpenRecent
        '
        Me.tsmiFile_OpenRecent.Name = "tsmiFile_OpenRecent"
        Me.tsmiFile_OpenRecent.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_OpenRecent.Text = "Open recent files ..."
        Me.tsmiFile_OpenRecent.ToolTipText = "Open recent used files"
        '
        'tsmiFile_OpenLastFile
        '
        Me.tsmiFile_OpenLastFile.Name = "tsmiFile_OpenLastFile"
        Me.tsmiFile_OpenLastFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.tsmiFile_OpenLastFile.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_OpenLastFile.Text = "Open last file processed in assoociated software"
        Me.tsmiFile_OpenLastFile.ToolTipText = "Use the standard configured viewer to open the last opened file (e.g. with FITSWo" &
    "rk)"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(363, 6)
        '
        'tsmiFile_FITSGrep
        '
        Me.tsmiFile_FITSGrep.Name = "tsmiFile_FITSGrep"
        Me.tsmiFile_FITSGrep.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_FITSGrep.Text = "FITS Grep"
        '
        'tsmiFile_AstroBinSearch
        '
        Me.tsmiFile_AstroBinSearch.Name = "tsmiFile_AstroBinSearch"
        Me.tsmiFile_AstroBinSearch.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_AstroBinSearch.Text = "AstroBin search"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(363, 6)
        '
        'tsmiFile_Compress2nd
        '
        Me.tsmiFile_Compress2nd.Name = "tsmiFile_Compress2nd"
        Me.tsmiFile_Compress2nd.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_Compress2nd.Text = "Compress 2nd file"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(363, 6)
        '
        'tsmiFile_SaveLastStat
        '
        Me.tsmiFile_SaveLastStat.Name = "tsmiFile_SaveLastStat"
        Me.tsmiFile_SaveLastStat.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.tsmiFile_SaveLastStat.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_SaveLastStat.Text = "Save last image statistics (EXCEL)"
        '
        'tsmiSaveAllFilesStat
        '
        Me.tsmiSaveAllFilesStat.Name = "tsmiSaveAllFilesStat"
        Me.tsmiSaveAllFilesStat.Size = New System.Drawing.Size(366, 22)
        Me.tsmiSaveAllFilesStat.Text = "Save all-files statistics"
        '
        'tsmiSaveFITSAndStats
        '
        Me.tsmiSaveFITSAndStats.Name = "tsmiSaveFITSAndStats"
        Me.tsmiSaveFITSAndStats.Size = New System.Drawing.Size(366, 22)
        Me.tsmiSaveFITSAndStats.Text = "Save FITS and statistics summary"
        '
        'tsmiSaveImageData
        '
        Me.tsmiSaveImageData.Name = "tsmiSaveImageData"
        Me.tsmiSaveImageData.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.tsmiSaveImageData.Size = New System.Drawing.Size(366, 22)
        Me.tsmiSaveImageData.Text = "Save current image"
        '
        'ToolStripMenuItem14
        '
        Me.ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        Me.ToolStripMenuItem14.Size = New System.Drawing.Size(363, 6)
        '
        'tsmiFile_ClearStatMem
        '
        Me.tsmiFile_ClearStatMem.Name = "tsmiFile_ClearStatMem"
        Me.tsmiFile_ClearStatMem.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_ClearStatMem.Text = "Clear statistics memory"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(363, 6)
        '
        'OpenEXELocationToolStripMenuItem
        '
        Me.OpenEXELocationToolStripMenuItem.Name = "OpenEXELocationToolStripMenuItem"
        Me.OpenEXELocationToolStripMenuItem.Size = New System.Drawing.Size(366, 22)
        Me.OpenEXELocationToolStripMenuItem.Text = "Open EXE location"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(363, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(366, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'tsmiAnalysis
        '
        Me.tsmiAnalysis.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAnalysis_RowColStat, Me.tsmiAnalysis_Plot, Me.tsmiAnalysisHotPixel, Me.ToolStripMenuItem7, Me.tsmiAnalysis_MultiAreaCompare, Me.tsmiAnalysis_ManualColorBalancer, Me.tsmiAnalysisVignette, Me.tsmiAnalysisPixelMap, Me.tsmiAnalysis_RawFITSHeader, Me.tsmiAnalysis_FloatAsIntError, Me.tsmiAnalysis_MultiFile, Me.tsmiAnalysis_XvsYPlot, Me.tsmiAnalysis_FindStars})
        Me.tsmiAnalysis.Name = "tsmiAnalysis"
        Me.tsmiAnalysis.Size = New System.Drawing.Size(62, 22)
        Me.tsmiAnalysis.Text = "Analysis"
        '
        'tsmiAnalysis_RowColStat
        '
        Me.tsmiAnalysis_RowColStat.Name = "tsmiAnalysis_RowColStat"
        Me.tsmiAnalysis_RowColStat.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_RowColStat.Text = "Row and column statistics"
        '
        'tsmiAnalysis_Plot
        '
        Me.tsmiAnalysis_Plot.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAnalysis_Plot_Replot, Me.tsmiAnalysis_Plot_ADUQuant})
        Me.tsmiAnalysis_Plot.Name = "tsmiAnalysis_Plot"
        Me.tsmiAnalysis_Plot.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.tsmiAnalysis_Plot.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_Plot.Text = "Plot"
        '
        'tsmiAnalysis_Plot_Replot
        '
        Me.tsmiAnalysis_Plot_Replot.Name = "tsmiAnalysis_Plot_Replot"
        Me.tsmiAnalysis_Plot_Replot.Size = New System.Drawing.Size(167, 22)
        Me.tsmiAnalysis_Plot_Replot.Text = "Re-plot statistics"
        '
        'tsmiAnalysis_Plot_ADUQuant
        '
        Me.tsmiAnalysis_Plot_ADUQuant.Name = "tsmiAnalysis_Plot_ADUQuant"
        Me.tsmiAnalysis_Plot_ADUQuant.Size = New System.Drawing.Size(167, 22)
        Me.tsmiAnalysis_Plot_ADUQuant.Text = "ADU quantization"
        '
        'tsmiAnalysisHotPixel
        '
        Me.tsmiAnalysisHotPixel.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAnalysisHotPixel_detect, Me.tsmiAnalysisHotPixel_fixfile})
        Me.tsmiAnalysisHotPixel.Name = "tsmiAnalysisHotPixel"
        Me.tsmiAnalysisHotPixel.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysisHotPixel.Text = "Hot pixel"
        '
        'tsmiAnalysisHotPixel_detect
        '
        Me.tsmiAnalysisHotPixel_detect.Name = "tsmiAnalysisHotPixel_detect"
        Me.tsmiAnalysisHotPixel_detect.Size = New System.Drawing.Size(159, 22)
        Me.tsmiAnalysisHotPixel_detect.Text = "Detect"
        '
        'tsmiAnalysisHotPixel_fixfile
        '
        Me.tsmiAnalysisHotPixel_fixfile.Name = "tsmiAnalysisHotPixel_fixfile"
        Me.tsmiAnalysisHotPixel_fixfile.Size = New System.Drawing.Size(159, 22)
        Me.tsmiAnalysisHotPixel_fixfile.Text = "Fix based on file"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(258, 6)
        '
        'tsmiAnalysis_MultiAreaCompare
        '
        Me.tsmiAnalysis_MultiAreaCompare.Name = "tsmiAnalysis_MultiAreaCompare"
        Me.tsmiAnalysis_MultiAreaCompare.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_MultiAreaCompare.Text = "Multi-file area compare (Navigator)"
        '
        'tsmiAnalysis_ManualColorBalancer
        '
        Me.tsmiAnalysis_ManualColorBalancer.Name = "tsmiAnalysis_ManualColorBalancer"
        Me.tsmiAnalysis_ManualColorBalancer.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_ManualColorBalancer.Text = "Manual color balancer"
        '
        'tsmiAnalysisVignette
        '
        Me.tsmiAnalysisVignette.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAnalysisVignette_CalcRaw, Me.tsmiAnalysisVignette_CalcParam, Me.tsmiAnalysisVignette_Correct, Me.tsmiAnalysisVignette_Display, Me.ToolStripMenuItem10, Me.tsmiAnalysisVignette_Clear})
        Me.tsmiAnalysisVignette.Name = "tsmiAnalysisVignette"
        Me.tsmiAnalysisVignette.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysisVignette.Text = "Vignette"
        '
        'tsmiAnalysisVignette_CalcRaw
        '
        Me.tsmiAnalysisVignette_CalcRaw.Name = "tsmiAnalysisVignette_CalcRaw"
        Me.tsmiAnalysisVignette_CalcRaw.Size = New System.Drawing.Size(240, 22)
        Me.tsmiAnalysisVignette_CalcRaw.Text = "Calculate raw data (no binning)"
        '
        'tsmiAnalysisVignette_CalcParam
        '
        Me.tsmiAnalysisVignette_CalcParam.Name = "tsmiAnalysisVignette_CalcParam"
        Me.tsmiAnalysisVignette_CalcParam.Size = New System.Drawing.Size(240, 22)
        Me.tsmiAnalysisVignette_CalcParam.Text = "Calculate fitting parameters"
        '
        'tsmiAnalysisVignette_Correct
        '
        Me.tsmiAnalysisVignette_Correct.Name = "tsmiAnalysisVignette_Correct"
        Me.tsmiAnalysisVignette_Correct.Size = New System.Drawing.Size(240, 22)
        Me.tsmiAnalysisVignette_Correct.Text = "Correct (only for BIN=0)"
        '
        'tsmiAnalysisVignette_Display
        '
        Me.tsmiAnalysisVignette_Display.Name = "tsmiAnalysisVignette_Display"
        Me.tsmiAnalysisVignette_Display.Size = New System.Drawing.Size(240, 22)
        Me.tsmiAnalysisVignette_Display.Text = "Display"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(237, 6)
        '
        'tsmiAnalysisVignette_Clear
        '
        Me.tsmiAnalysisVignette_Clear.Name = "tsmiAnalysisVignette_Clear"
        Me.tsmiAnalysisVignette_Clear.Size = New System.Drawing.Size(240, 22)
        Me.tsmiAnalysisVignette_Clear.Text = "Clear"
        '
        'tsmiAnalysisPixelMap
        '
        Me.tsmiAnalysisPixelMap.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAnalysisPixelMap_SaveFor})
        Me.tsmiAnalysisPixelMap.Name = "tsmiAnalysisPixelMap"
        Me.tsmiAnalysisPixelMap.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysisPixelMap.Text = "Pixel map files"
        '
        'tsmiAnalysisPixelMap_SaveFor
        '
        Me.tsmiAnalysisPixelMap_SaveFor.Name = "tsmiAnalysisPixelMap_SaveFor"
        Me.tsmiAnalysisPixelMap_SaveFor.Size = New System.Drawing.Size(221, 22)
        Me.tsmiAnalysisPixelMap_SaveFor.Text = "Save pixel coordinates for ..."
        '
        'tsmiAnalysis_RawFITSHeader
        '
        Me.tsmiAnalysis_RawFITSHeader.Name = "tsmiAnalysis_RawFITSHeader"
        Me.tsmiAnalysis_RawFITSHeader.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_RawFITSHeader.Text = "Raw FITS Header"
        '
        'tsmiAnalysis_FloatAsIntError
        '
        Me.tsmiAnalysis_FloatAsIntError.Name = "tsmiAnalysis_FloatAsIntError"
        Me.tsmiAnalysis_FloatAsIntError.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_FloatAsIntError.Text = "Float error to int"
        '
        'tsmiAnalysis_MultiFile
        '
        Me.tsmiAnalysis_MultiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAnalysis_MultiFile_Open, Me.tsmiAnalysis_MultiFile_LoadAbove})
        Me.tsmiAnalysis_MultiFile.Name = "tsmiAnalysis_MultiFile"
        Me.tsmiAnalysis_MultiFile.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_MultiFile.Text = "Multi-file pixelwise statistics"
        '
        'tsmiAnalysis_MultiFile_Open
        '
        Me.tsmiAnalysis_MultiFile_Open.Name = "tsmiAnalysis_MultiFile_Open"
        Me.tsmiAnalysis_MultiFile_Open.Size = New System.Drawing.Size(195, 22)
        Me.tsmiAnalysis_MultiFile_Open.Text = "Open analysis  window"
        '
        'tsmiAnalysis_MultiFile_LoadAbove
        '
        Me.tsmiAnalysis_MultiFile_LoadAbove.Name = "tsmiAnalysis_MultiFile_LoadAbove"
        Me.tsmiAnalysis_MultiFile_LoadAbove.Size = New System.Drawing.Size(195, 22)
        Me.tsmiAnalysis_MultiFile_LoadAbove.Text = "Load certain pixel ..."
        '
        'tsmiAnalysis_XvsYPlot
        '
        Me.tsmiAnalysis_XvsYPlot.Name = "tsmiAnalysis_XvsYPlot"
        Me.tsmiAnalysis_XvsYPlot.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_XvsYPlot.Text = "X-vs-Y plots"
        '
        'tsmiAnalysis_FindStars
        '
        Me.tsmiAnalysis_FindStars.Name = "tsmiAnalysis_FindStars"
        Me.tsmiAnalysis_FindStars.Size = New System.Drawing.Size(261, 22)
        Me.tsmiAnalysis_FindStars.Text = "Find stars"
        '
        'tsmiProcessing
        '
        Me.tsmiProcessing.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiProcessing_AdjustRGB, Me.tsmiStretch, Me.tsmiPlateSolve, Me.tsmiSetPixelToValue, Me.tsmiProcessing_MedianFilter, Me.SubtractMedianToolStripMenuItem, Me.tsmiProcessing_LSBMSB, Me.tsmiProcessing_Specials, Me.tsmiProcessing_Stack, Me.tsmiHotPixelFilter, Me.tsmiProc_Bin2OpenCV, Me.tsmiProc_Bin2Median, Me.tsmiProc_Bin2MaxOut})
        Me.tsmiProcessing.Name = "tsmiProcessing"
        Me.tsmiProcessing.Size = New System.Drawing.Size(76, 22)
        Me.tsmiProcessing.Text = "Processing"
        '
        'tsmiProcessing_AdjustRGB
        '
        Me.tsmiProcessing_AdjustRGB.Name = "tsmiProcessing_AdjustRGB"
        Me.tsmiProcessing_AdjustRGB.Size = New System.Drawing.Size(301, 22)
        Me.tsmiProcessing_AdjustRGB.Text = "Adjust RGB channels (using modus)"
        '
        'tsmiStretch
        '
        Me.tsmiStretch.Name = "tsmiStretch"
        Me.tsmiStretch.Size = New System.Drawing.Size(301, 22)
        Me.tsmiStretch.Text = "Stretcher histogramm over complete range"
        '
        'tsmiPlateSolve
        '
        Me.tsmiPlateSolve.Name = "tsmiPlateSolve"
        Me.tsmiPlateSolve.Size = New System.Drawing.Size(301, 22)
        Me.tsmiPlateSolve.Text = "Plate solve image"
        '
        'tsmiSetPixelToValue
        '
        Me.tsmiSetPixelToValue.Name = "tsmiSetPixelToValue"
        Me.tsmiSetPixelToValue.Size = New System.Drawing.Size(301, 22)
        Me.tsmiSetPixelToValue.Text = "Set pixel above to certain value"
        '
        'tsmiProcessing_MedianFilter
        '
        Me.tsmiProcessing_MedianFilter.Name = "tsmiProcessing_MedianFilter"
        Me.tsmiProcessing_MedianFilter.Size = New System.Drawing.Size(301, 22)
        Me.tsmiProcessing_MedianFilter.Text = "Median filter"
        '
        'SubtractMedianToolStripMenuItem
        '
        Me.SubtractMedianToolStripMenuItem.Name = "SubtractMedianToolStripMenuItem"
        Me.SubtractMedianToolStripMenuItem.Size = New System.Drawing.Size(301, 22)
        Me.SubtractMedianToolStripMenuItem.Text = "Subtract median"
        '
        'tsmiProcessing_LSBMSB
        '
        Me.tsmiProcessing_LSBMSB.Name = "tsmiProcessing_LSBMSB"
        Me.tsmiProcessing_LSBMSB.Size = New System.Drawing.Size(301, 22)
        Me.tsmiProcessing_LSBMSB.Text = "LSB-MSB swap"
        '
        'tsmiProcessing_Specials
        '
        Me.tsmiProcessing_Specials.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiProcessing_Specials_NINAFix})
        Me.tsmiProcessing_Specials.Name = "tsmiProcessing_Specials"
        Me.tsmiProcessing_Specials.Size = New System.Drawing.Size(301, 22)
        Me.tsmiProcessing_Specials.Text = "Specials"
        '
        'tsmiProcessing_Specials_NINAFix
        '
        Me.tsmiProcessing_Specials_NINAFix.Name = "tsmiProcessing_Specials_NINAFix"
        Me.tsmiProcessing_Specials_NINAFix.Size = New System.Drawing.Size(128, 22)
        Me.tsmiProcessing_Specials_NINAFix.Text = "NINA fix 1"
        '
        'tsmiProcessing_Stack
        '
        Me.tsmiProcessing_Stack.Name = "tsmiProcessing_Stack"
        Me.tsmiProcessing_Stack.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.tsmiProcessing_Stack.Size = New System.Drawing.Size(301, 22)
        Me.tsmiProcessing_Stack.Text = "Stacking multiple files"
        '
        'tsmiHotPixelFilter
        '
        Me.tsmiHotPixelFilter.Name = "tsmiHotPixelFilter"
        Me.tsmiHotPixelFilter.Size = New System.Drawing.Size(301, 22)
        Me.tsmiHotPixelFilter.Text = "Special hotpixel filter - EXPERIMENTAL"
        '
        'tsmiProc_Bin2OpenCV
        '
        Me.tsmiProc_Bin2OpenCV.Name = "tsmiProc_Bin2OpenCV"
        Me.tsmiProc_Bin2OpenCV.Size = New System.Drawing.Size(301, 22)
        Me.tsmiProc_Bin2OpenCV.Text = "Bin 2 with OpenCV"
        '
        'tsmiProc_Bin2Median
        '
        Me.tsmiProc_Bin2Median.Name = "tsmiProc_Bin2Median"
        Me.tsmiProc_Bin2Median.Size = New System.Drawing.Size(301, 22)
        Me.tsmiProc_Bin2Median.Text = "Bin 2 with median"
        '
        'tsmiProc_Bin2MaxOut
        '
        Me.tsmiProc_Bin2MaxOut.Name = "tsmiProc_Bin2MaxOut"
        Me.tsmiProc_Bin2MaxOut.Size = New System.Drawing.Size(301, 22)
        Me.tsmiProc_Bin2MaxOut.Text = "Bin 2 with max removal"
        '
        'tsmiTest
        '
        Me.tsmiTest.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AfiineTranslateToolStripMenuItem, Me.ToolStripMenuItem6, Me.tsmiTest_Focus, Me.tssmRadCollimation, Me.tsmiTest_ReadNEFFile, Me.tsmiTest_ippiXCorr, Me.ToolStripMenuItem9, Me.tsmiTestCode_UseOpenCV, Me.MedianWithinNETToolStripMenuItem, Me.ToolStripMenuItem12, Me.CodeBelowIsNotForHereToolStripMenuItem, Me.CloudWatcherCombinerToolStripMenuItem, Me.GrayPNGToFITSToolStripMenuItem, Me.tsmiTest_AstrometryQuery, Me.frmTest_HistoInteractive, Me.tsmiTestCode_StreamDeck, Me.tsTest_LibRaw, Me.SigmaClipToolStripMenuItem})
        Me.tsmiTest.Name = "tsmiTest"
        Me.tsmiTest.Size = New System.Drawing.Size(68, 22)
        Me.tsmiTest.Text = "Test code"
        '
        'AfiineTranslateToolStripMenuItem
        '
        Me.AfiineTranslateToolStripMenuItem.Name = "AfiineTranslateToolStripMenuItem"
        Me.AfiineTranslateToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.AfiineTranslateToolStripMenuItem.Text = "Afiine translate"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(271, 6)
        '
        'tsmiTest_Focus
        '
        Me.tsmiTest_Focus.Name = "tsmiTest_Focus"
        Me.tsmiTest_Focus.Size = New System.Drawing.Size(274, 22)
        Me.tsmiTest_Focus.Text = "Focus"
        '
        'tssmRadCollimation
        '
        Me.tssmRadCollimation.Name = "tssmRadCollimation"
        Me.tssmRadCollimation.Size = New System.Drawing.Size(274, 22)
        Me.tssmRadCollimation.Text = "Radial statistics and collimation"
        '
        'tsmiTest_ReadNEFFile
        '
        Me.tsmiTest_ReadNEFFile.Name = "tsmiTest_ReadNEFFile"
        Me.tsmiTest_ReadNEFFile.Size = New System.Drawing.Size(274, 22)
        Me.tsmiTest_ReadNEFFile.Text = "NEF reading"
        '
        'tsmiTest_ippiXCorr
        '
        Me.tsmiTest_ippiXCorr.Name = "tsmiTest_ippiXCorr"
        Me.tsmiTest_ippiXCorr.Size = New System.Drawing.Size(274, 22)
        Me.tsmiTest_ippiXCorr.Text = "ippi XCorr"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(271, 6)
        '
        'tsmiTestCode_UseOpenCV
        '
        Me.tsmiTestCode_UseOpenCV.Name = "tsmiTestCode_UseOpenCV"
        Me.tsmiTestCode_UseOpenCV.Size = New System.Drawing.Size(274, 22)
        Me.tsmiTestCode_UseOpenCV.Text = "Use OpenCV"
        '
        'MedianWithinNETToolStripMenuItem
        '
        Me.MedianWithinNETToolStripMenuItem.Name = "MedianWithinNETToolStripMenuItem"
        Me.MedianWithinNETToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.MedianWithinNETToolStripMenuItem.Text = ".NET Median Filter (for reference only)"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(271, 6)
        '
        'CodeBelowIsNotForHereToolStripMenuItem
        '
        Me.CodeBelowIsNotForHereToolStripMenuItem.Name = "CodeBelowIsNotForHereToolStripMenuItem"
        Me.CodeBelowIsNotForHereToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.CodeBelowIsNotForHereToolStripMenuItem.Text = "(Code below is not for here ...)"
        '
        'CloudWatcherCombinerToolStripMenuItem
        '
        Me.CloudWatcherCombinerToolStripMenuItem.Name = "CloudWatcherCombinerToolStripMenuItem"
        Me.CloudWatcherCombinerToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.CloudWatcherCombinerToolStripMenuItem.Text = "CloudWatcher combiner"
        '
        'GrayPNGToFITSToolStripMenuItem
        '
        Me.GrayPNGToFITSToolStripMenuItem.Name = "GrayPNGToFITSToolStripMenuItem"
        Me.GrayPNGToFITSToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.GrayPNGToFITSToolStripMenuItem.Text = "Gray PNG to FITS"
        '
        'tsmiTest_AstrometryQuery
        '
        Me.tsmiTest_AstrometryQuery.Name = "tsmiTest_AstrometryQuery"
        Me.tsmiTest_AstrometryQuery.Size = New System.Drawing.Size(274, 22)
        Me.tsmiTest_AstrometryQuery.Text = "Astrometry Batch Query"
        '
        'frmTest_HistoInteractive
        '
        Me.frmTest_HistoInteractive.Name = "frmTest_HistoInteractive"
        Me.frmTest_HistoInteractive.Size = New System.Drawing.Size(274, 22)
        Me.frmTest_HistoInteractive.Text = "Interactive Histogram"
        '
        'tsmiTestCode_StreamDeck
        '
        Me.tsmiTestCode_StreamDeck.Name = "tsmiTestCode_StreamDeck"
        Me.tsmiTestCode_StreamDeck.Size = New System.Drawing.Size(274, 22)
        Me.tsmiTestCode_StreamDeck.Text = "StreamDeck"
        '
        'tsTest_LibRaw
        '
        Me.tsTest_LibRaw.Name = "tsTest_LibRaw"
        Me.tsTest_LibRaw.Size = New System.Drawing.Size(274, 22)
        Me.tsTest_LibRaw.Text = "LibRaw access"
        '
        'SigmaClipToolStripMenuItem
        '
        Me.SigmaClipToolStripMenuItem.Name = "SigmaClipToolStripMenuItem"
        Me.SigmaClipToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.SigmaClipToolStripMenuItem.Text = "Sigma clip"
        '
        'tsmiTools
        '
        Me.tsmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiTools_ALADINCoords, Me.tsmiTools_ChangeHeader, Me.tsmiTools_RemoveOverscan, Me.tsmiTools_TestFile, Me.CheckROICutoutToolStripMenuItem})
        Me.tsmiTools.Name = "tsmiTools"
        Me.tsmiTools.Size = New System.Drawing.Size(46, 22)
        Me.tsmiTools.Text = "Tools"
        '
        'tsmiTools_ALADINCoords
        '
        Me.tsmiTools_ALADINCoords.Name = "tsmiTools_ALADINCoords"
        Me.tsmiTools_ALADINCoords.Size = New System.Drawing.Size(215, 22)
        Me.tsmiTools_ALADINCoords.Text = "Coords for ALADIN call"
        '
        'tsmiTools_ChangeHeader
        '
        Me.tsmiTools_ChangeHeader.Name = "tsmiTools_ChangeHeader"
        Me.tsmiTools_ChangeHeader.Size = New System.Drawing.Size(215, 22)
        Me.tsmiTools_ChangeHeader.Text = "Change header"
        '
        'tsmiTools_RemoveOverscan
        '
        Me.tsmiTools_RemoveOverscan.Name = "tsmiTools_RemoveOverscan"
        Me.tsmiTools_RemoveOverscan.Size = New System.Drawing.Size(215, 22)
        Me.tsmiTools_RemoveOverscan.Text = "Remove QHY600 Overscan"
        '
        'tsmiTools_TestFile
        '
        Me.tsmiTools_TestFile.Name = "tsmiTools_TestFile"
        Me.tsmiTools_TestFile.Size = New System.Drawing.Size(215, 22)
        Me.tsmiTools_TestFile.Text = "Special test file"
        '
        'CheckROICutoutToolStripMenuItem
        '
        Me.CheckROICutoutToolStripMenuItem.Name = "CheckROICutoutToolStripMenuItem"
        Me.CheckROICutoutToolStripMenuItem.Size = New System.Drawing.Size(215, 22)
        Me.CheckROICutoutToolStripMenuItem.Text = "Check ROI cut-out"
        '
        'WorkflowToolStripMenuItem
        '
        Me.WorkflowToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MultifileActionToolStripMenuItem, Me.ToolStripMenuItem13, Me.FixRADECErrorToolStripMenuItem, Me.tsmiWorkflow_Runner})
        Me.WorkflowToolStripMenuItem.Name = "WorkflowToolStripMenuItem"
        Me.WorkflowToolStripMenuItem.Size = New System.Drawing.Size(70, 22)
        Me.WorkflowToolStripMenuItem.Text = "Workflow"
        '
        'MultifileActionToolStripMenuItem
        '
        Me.MultifileActionToolStripMenuItem.Name = "MultifileActionToolStripMenuItem"
        Me.MultifileActionToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.MultifileActionToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.MultifileActionToolStripMenuItem.Text = "Multi-file action"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(201, 6)
        '
        'FixRADECErrorToolStripMenuItem
        '
        Me.FixRADECErrorToolStripMenuItem.Name = "FixRADECErrorToolStripMenuItem"
        Me.FixRADECErrorToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.FixRADECErrorToolStripMenuItem.Text = "Fix RA_DEC error"
        '
        'tsmiWorkflow_Runner
        '
        Me.tsmiWorkflow_Runner.Name = "tsmiWorkflow_Runner"
        Me.tsmiWorkflow_Runner.Size = New System.Drawing.Size(204, 22)
        Me.tsmiWorkflow_Runner.Text = "Open runner"
        '
        'ofdMain
        '
        Me.ofdMain.Multiselect = True
        '
        'tbLogOutput
        '
        Me.tbLogOutput.AllowDrop = True
        Me.tbLogOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLogOutput.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLogOutput.Location = New System.Drawing.Point(3, 3)
        Me.tbLogOutput.Multiline = True
        Me.tbLogOutput.Name = "tbLogOutput"
        Me.tbLogOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLogOutput.Size = New System.Drawing.Size(1289, 1116)
        Me.tbLogOutput.TabIndex = 3
        Me.tbLogOutput.WordWrap = False
        '
        'ssMain
        '
        Me.ssMain.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslRunning, Me.tsslMain, Me.tspbMain, Me.ToolStripStatusLabel1, Me.tspbMultiFile, Me.tsslMultiFile})
        Me.ssMain.Location = New System.Drawing.Point(0, 1177)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Padding = New System.Windows.Forms.Padding(1, 0, 9, 0)
        Me.ssMain.Size = New System.Drawing.Size(1692, 22)
        Me.ssMain.TabIndex = 4
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslRunning
        '
        Me.tsslRunning.Font = New System.Drawing.Font("Wingdings", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.tsslRunning.ForeColor = System.Drawing.Color.Silver
        Me.tsslRunning.Name = "tsslRunning"
        Me.tsslRunning.Size = New System.Drawing.Size(17, 17)
        Me.tsslRunning.Text = "l"
        '
        'tsslMain
        '
        Me.tsslMain.Name = "tsslMain"
        Me.tsslMain.Size = New System.Drawing.Size(22, 17)
        Me.tsslMain.Text = "---"
        '
        'tspbMain
        '
        Me.tspbMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tspbMain.Name = "tspbMain"
        Me.tspbMain.Size = New System.Drawing.Size(100, 16)
        Me.tspbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(10, 17)
        Me.ToolStripStatusLabel1.Text = "|"
        '
        'tspbMultiFile
        '
        Me.tspbMultiFile.Name = "tspbMultiFile"
        Me.tspbMultiFile.Size = New System.Drawing.Size(100, 16)
        '
        'tsslMultiFile
        '
        Me.tsslMultiFile.Name = "tsslMultiFile"
        Me.tsslMultiFile.Size = New System.Drawing.Size(22, 17)
        Me.tsslMultiFile.Text = "---"
        '
        'pgMain
        '
        Me.pgMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgMain.Location = New System.Drawing.Point(3, 3)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(363, 559)
        Me.pgMain.TabIndex = 5
        Me.pgMain.ToolbarVisible = False
        '
        'gbDetails
        '
        Me.gbDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDetails.Controls.Add(Me.tbDetails)
        Me.gbDetails.Location = New System.Drawing.Point(3, 3)
        Me.gbDetails.Name = "gbDetails"
        Me.gbDetails.Size = New System.Drawing.Size(363, 547)
        Me.gbDetails.TabIndex = 6
        Me.gbDetails.TabStop = False
        Me.gbDetails.Text = "Details"
        '
        'tbDetails
        '
        Me.tbDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbDetails.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDetails.Location = New System.Drawing.Point(6, 19)
        Me.tbDetails.Multiline = True
        Me.tbDetails.Name = "tbDetails"
        Me.tbDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbDetails.Size = New System.Drawing.Size(351, 522)
        Me.tbDetails.TabIndex = 0
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(12, 52)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.scLeft)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.tbLogOutput)
        Me.scMain.Size = New System.Drawing.Size(1668, 1122)
        Me.scMain.SplitterDistance = 369
        Me.scMain.TabIndex = 7
        '
        'scLeft
        '
        Me.scLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scLeft.Location = New System.Drawing.Point(0, 0)
        Me.scLeft.Name = "scLeft"
        Me.scLeft.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scLeft.Panel1
        '
        Me.scLeft.Panel1.Controls.Add(Me.pgMain)
        '
        'scLeft.Panel2
        '
        Me.scLeft.Panel2.Controls.Add(Me.gbDetails)
        Me.scLeft.Size = New System.Drawing.Size(369, 1122)
        Me.scLeft.SplitterDistance = 565
        Me.scLeft.TabIndex = 0
        '
        'tsMain
        '
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_Open, Me.tsb_Display})
        Me.tsMain.Location = New System.Drawing.Point(0, 24)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Size = New System.Drawing.Size(1692, 25)
        Me.tsMain.TabIndex = 8
        Me.tsMain.Text = "ToolStrip1"
        '
        'tsb_Open
        '
        Me.tsb_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsb_Open.Image = CType(resources.GetObject("tsb_Open.Image"), System.Drawing.Image)
        Me.tsb_Open.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Open.Name = "tsb_Open"
        Me.tsb_Open.Size = New System.Drawing.Size(52, 22)
        Me.tsb_Open.Text = "Open ..."
        '
        'tsb_Display
        '
        Me.tsb_Display.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsb_Display.Image = CType(resources.GetObject("tsb_Display.Image"), System.Drawing.Image)
        Me.tsb_Display.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Display.Name = "tsb_Display"
        Me.tsb_Display.Size = New System.Drawing.Size(49, 22)
        Me.tsb_Display.Text = "Display"
        '
        'tsmiFile_ConvertTo16BitFITS
        '
        Me.tsmiFile_ConvertTo16BitFITS.Name = "tsmiFile_ConvertTo16BitFITS"
        Me.tsmiFile_ConvertTo16BitFITS.Size = New System.Drawing.Size(366, 22)
        Me.tsmiFile_ConvertTo16BitFITS.Text = "Make file 16-bit FITS file"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1692, 1199)
        Me.Controls.Add(Me.tsMain)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.msMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.msMain
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MainForm"
        Me.Text = "Astro Image Statistics Version 0.3"
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.gbDetails.ResumeLayout(False)
        Me.gbDetails.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.Panel2.PerformLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.scLeft.Panel1.ResumeLayout(False)
        Me.scLeft.Panel2.ResumeLayout(False)
        CType(Me.scLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scLeft.ResumeLayout(False)
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents tsmiAnalysis_MultiAreaCompare As ToolStripMenuItem
    Friend WithEvents tspbMain As ToolStripProgressBar
    Friend WithEvents tsmiFile_OpenRecent As ToolStripMenuItem
    Friend WithEvents gbDetails As GroupBox
    Friend WithEvents tbDetails As TextBox
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents scLeft As SplitContainer
    Friend WithEvents ToolStripMenuItem8 As ToolStripSeparator
    Friend WithEvents tsmiSaveAllFilesStat As ToolStripMenuItem
    Friend WithEvents tsmiTest_ReadNEFFile As ToolStripMenuItem
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
    Friend WithEvents tsmiAnalysisHotPixel_detect As ToolStripMenuItem
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
    Friend WithEvents GrayPNGToFITSToolStripMenuItem As ToolStripMenuItem
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
    Friend WithEvents tsTest_LibRaw As ToolStripMenuItem
    Friend WithEvents tsmiProcessing_Stack As ToolStripMenuItem
    Friend WithEvents tssmRadCollimation As ToolStripMenuItem
    Friend WithEvents MultifileActionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiHotPixelFilter As ToolStripMenuItem
    Friend WithEvents tsmiProc_Bin2Median As ToolStripMenuItem
    Friend WithEvents tsmiProc_Bin2MaxOut As ToolStripMenuItem
    Friend WithEvents tsmiAnalysis_FindStars As ToolStripMenuItem
    Friend WithEvents tsmiProc_Bin2OpenCV As ToolStripMenuItem
    Friend WithEvents tsmiTest_ippiXCorr As ToolStripMenuItem
    Friend WithEvents SigmaClipToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiFile_ConvertTo16BitFITS As ToolStripMenuItem
End Class
