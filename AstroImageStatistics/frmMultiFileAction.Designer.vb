<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMultiFileAction
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMultiFileAction))
        adgvMain = New Zuby.ADGV.AdvancedDataGridView()
        cmsTable = New ContextMenuStrip(components)
        cmsTable_OpenFile = New ToolStripMenuItem()
        scMain = New SplitContainer()
        scButtom = New SplitContainer()
        pgMain = New PropertyGrid()
        scBottomRight = New SplitContainer()
        gbAspects = New GroupBox()
        tcAspect = New TabControl()
        tpFITSHeader = New TabPage()
        tbFITSHeader = New TextBox()
        tbCombinedROI = New TabPage()
        pbImage = New PictureBoxEx()
        cmsMain = New ContextMenuStrip(components)
        cmsMain_ToClipboard = New ToolStripMenuItem()
        tbSinglePixelStat = New TabPage()
        tbPixelStat = New TextBox()
        tbLog = New TextBox()
        Label1 = New Label()
        msMain = New MenuStrip()
        tsmiFile = New ToolStripMenuItem()
        tsmiFile_AddFiles = New ToolStripMenuItem()
        tsmiFile_OpenWorkingDir = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiFile_SaveAllFilesHisto = New ToolStripMenuItem()
        tsmiFile_SaveFITSandStats = New ToolStripMenuItem()
        ToolStripMenuItem3 = New ToolStripSeparator()
        tsmiFile_Exit = New ToolStripMenuItem()
        tsmiFileList = New ToolStripMenuItem()
        tsmiFileList_ClearList = New ToolStripMenuItem()
        tsmiAction = New ToolStripMenuItem()
        tsmiAction_Run = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        tsmiAction_ClearLog = New ToolStripMenuItem()
        tsmiAction_StackSpecial = New ToolStripMenuItem()
        tsmiAction_Mode = New ToolStripMenuItem()
        tsmiAction_Mode_StoreAlignedFiles = New ToolStripMenuItem()
        ToolStripMenuItem4 = New ToolStripSeparator()
        tsmiAction_SelectAll = New ToolStripMenuItem()
        tsmiAction_DeSelectAll = New ToolStripMenuItem()
        tsmiAction_DSSParam = New ToolStripMenuItem()
        ofdMain = New OpenFileDialog()
        ssMain = New StatusStrip()
        tspbMain = New ToolStripProgressBar()
        sfdMain = New SaveFileDialog()
        CType(adgvMain, ComponentModel.ISupportInitialize).BeginInit()
        cmsTable.SuspendLayout()
        CType(scMain, ComponentModel.ISupportInitialize).BeginInit()
        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        scMain.SuspendLayout()
        CType(scButtom, ComponentModel.ISupportInitialize).BeginInit()
        scButtom.Panel1.SuspendLayout()
        scButtom.Panel2.SuspendLayout()
        scButtom.SuspendLayout()
        CType(scBottomRight, ComponentModel.ISupportInitialize).BeginInit()
        scBottomRight.Panel1.SuspendLayout()
        scBottomRight.Panel2.SuspendLayout()
        scBottomRight.SuspendLayout()
        gbAspects.SuspendLayout()
        tcAspect.SuspendLayout()
        tpFITSHeader.SuspendLayout()
        tbCombinedROI.SuspendLayout()
        CType(pbImage, ComponentModel.ISupportInitialize).BeginInit()
        cmsMain.SuspendLayout()
        tbSinglePixelStat.SuspendLayout()
        msMain.SuspendLayout()
        ssMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' adgvMain
        ' 
        adgvMain.AllowDrop = True
        adgvMain.AllowUserToAddRows = False
        adgvMain.AllowUserToDeleteRows = False
        adgvMain.AllowUserToOrderColumns = True
        adgvMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        adgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        adgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        adgvMain.ContextMenuStrip = cmsTable
        adgvMain.FilterAndSortEnabled = True
        adgvMain.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        adgvMain.Location = New Point(4, 3)
        adgvMain.Margin = New Padding(4, 3, 4, 3)
        adgvMain.MultiSelect = False
        adgvMain.Name = "adgvMain"
        adgvMain.RightToLeft = RightToLeft.No
        adgvMain.RowHeadersVisible = False
        adgvMain.ShowEditingIcon = False
        adgvMain.ShowRowErrors = False
        adgvMain.Size = New Size(1530, 338)
        adgvMain.SortStringChangedInvokeBeforeDatasourceUpdate = True
        adgvMain.TabIndex = 1
        adgvMain.VirtualMode = True
        ' 
        ' cmsTable
        ' 
        cmsTable.Items.AddRange(New ToolStripItem() {cmsTable_OpenFile})
        cmsTable.Name = "cmsTable"
        cmsTable.Size = New Size(123, 26)
        ' 
        ' cmsTable_OpenFile
        ' 
        cmsTable_OpenFile.Name = "cmsTable_OpenFile"
        cmsTable_OpenFile.Size = New Size(122, 22)
        cmsTable_OpenFile.Text = "Open file"
        ' 
        ' scMain
        ' 
        scMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scMain.Location = New Point(14, 31)
        scMain.Margin = New Padding(4, 3, 4, 3)
        scMain.Name = "scMain"
        scMain.Orientation = Orientation.Horizontal
        ' 
        ' scMain.Panel1
        ' 
        scMain.Panel1.Controls.Add(adgvMain)
        ' 
        ' scMain.Panel2
        ' 
        scMain.Panel2.Controls.Add(scButtom)
        scMain.Size = New Size(1540, 1002)
        scMain.SplitterDistance = 345
        scMain.SplitterWidth = 5
        scMain.TabIndex = 2
        ' 
        ' scButtom
        ' 
        scButtom.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scButtom.Location = New Point(4, 3)
        scButtom.Margin = New Padding(4, 3, 4, 3)
        scButtom.Name = "scButtom"
        ' 
        ' scButtom.Panel1
        ' 
        scButtom.Panel1.Controls.Add(pgMain)
        ' 
        ' scButtom.Panel2
        ' 
        scButtom.Panel2.Controls.Add(scBottomRight)
        scButtom.Size = New Size(1536, 644)
        scButtom.SplitterDistance = 460
        scButtom.SplitterWidth = 5
        scButtom.TabIndex = 1
        ' 
        ' pgMain
        ' 
        pgMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgMain.Location = New Point(4, 3)
        pgMain.Margin = New Padding(4, 3, 4, 3)
        pgMain.Name = "pgMain"
        pgMain.Size = New Size(452, 638)
        pgMain.TabIndex = 0
        ' 
        ' scBottomRight
        ' 
        scBottomRight.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        scBottomRight.Location = New Point(3, 3)
        scBottomRight.Name = "scBottomRight"
        scBottomRight.Orientation = Orientation.Horizontal
        ' 
        ' scBottomRight.Panel1
        ' 
        scBottomRight.Panel1.Controls.Add(gbAspects)
        ' 
        ' scBottomRight.Panel2
        ' 
        scBottomRight.Panel2.Controls.Add(tbLog)
        scBottomRight.Panel2.Controls.Add(Label1)
        scBottomRight.Size = New Size(1062, 638)
        scBottomRight.SplitterDistance = 318
        scBottomRight.TabIndex = 7
        ' 
        ' gbAspects
        ' 
        gbAspects.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gbAspects.Controls.Add(tcAspect)
        gbAspects.Location = New Point(5, 3)
        gbAspects.Margin = New Padding(4, 3, 4, 3)
        gbAspects.Name = "gbAspects"
        gbAspects.Padding = New Padding(4, 3, 4, 3)
        gbAspects.Size = New Size(1053, 312)
        gbAspects.TabIndex = 6
        gbAspects.TabStop = False
        gbAspects.Text = "Aspect"
        ' 
        ' tcAspect
        ' 
        tcAspect.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tcAspect.Controls.Add(tpFITSHeader)
        tcAspect.Controls.Add(tbCombinedROI)
        tcAspect.Controls.Add(tbSinglePixelStat)
        tcAspect.Location = New Point(7, 22)
        tcAspect.Margin = New Padding(4, 3, 4, 3)
        tcAspect.Name = "tcAspect"
        tcAspect.SelectedIndex = 0
        tcAspect.Size = New Size(1046, 283)
        tcAspect.TabIndex = 0
        ' 
        ' tpFITSHeader
        ' 
        tpFITSHeader.BackColor = SystemColors.Control
        tpFITSHeader.Controls.Add(tbFITSHeader)
        tpFITSHeader.Location = New Point(4, 24)
        tpFITSHeader.Margin = New Padding(4, 3, 4, 3)
        tpFITSHeader.Name = "tpFITSHeader"
        tpFITSHeader.Padding = New Padding(4, 3, 4, 3)
        tpFITSHeader.Size = New Size(1038, 255)
        tpFITSHeader.TabIndex = 0
        tpFITSHeader.Text = "FITS header"
        ' 
        ' tbFITSHeader
        ' 
        tbFITSHeader.Dock = DockStyle.Fill
        tbFITSHeader.Font = New Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbFITSHeader.Location = New Point(4, 3)
        tbFITSHeader.Margin = New Padding(4, 3, 4, 3)
        tbFITSHeader.Multiline = True
        tbFITSHeader.Name = "tbFITSHeader"
        tbFITSHeader.ScrollBars = ScrollBars.Both
        tbFITSHeader.Size = New Size(1030, 249)
        tbFITSHeader.TabIndex = 2
        tbFITSHeader.WordWrap = False
        ' 
        ' tbCombinedROI
        ' 
        tbCombinedROI.BackColor = SystemColors.Control
        tbCombinedROI.Controls.Add(pbImage)
        tbCombinedROI.Location = New Point(4, 24)
        tbCombinedROI.Margin = New Padding(4, 3, 4, 3)
        tbCombinedROI.Name = "tbCombinedROI"
        tbCombinedROI.Padding = New Padding(4, 3, 4, 3)
        tbCombinedROI.Size = New Size(1039, 256)
        tbCombinedROI.TabIndex = 1
        tbCombinedROI.Text = "(Combined) ROI"
        ' 
        ' pbImage
        ' 
        pbImage.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pbImage.BackColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        pbImage.ContextMenuStrip = cmsMain
        pbImage.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        pbImage.Location = New Point(4, 7)
        pbImage.Margin = New Padding(4, 3, 4, 3)
        pbImage.Name = "pbImage"
        pbImage.Size = New Size(1029, 245)
        pbImage.SizeMode = PictureBoxSizeMode.Zoom
        pbImage.TabIndex = 5
        pbImage.TabStop = False
        ' 
        ' cmsMain
        ' 
        cmsMain.Items.AddRange(New ToolStripItem() {cmsMain_ToClipboard})
        cmsMain.Name = "cmsMain"
        cmsMain.Size = New Size(170, 26)
        ' 
        ' cmsMain_ToClipboard
        ' 
        cmsMain_ToClipboard.Name = "cmsMain_ToClipboard"
        cmsMain_ToClipboard.Size = New Size(169, 22)
        cmsMain_ToClipboard.Text = "Copy to clipboard"
        ' 
        ' tbSinglePixelStat
        ' 
        tbSinglePixelStat.BackColor = SystemColors.Control
        tbSinglePixelStat.Controls.Add(tbPixelStat)
        tbSinglePixelStat.Location = New Point(4, 24)
        tbSinglePixelStat.Margin = New Padding(4, 3, 4, 3)
        tbSinglePixelStat.Name = "tbSinglePixelStat"
        tbSinglePixelStat.Padding = New Padding(4, 3, 4, 3)
        tbSinglePixelStat.Size = New Size(1039, 256)
        tbSinglePixelStat.TabIndex = 2
        tbSinglePixelStat.Text = "Single pixel statistics"
        ' 
        ' tbPixelStat
        ' 
        tbPixelStat.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbPixelStat.Font = New Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbPixelStat.Location = New Point(7, 7)
        tbPixelStat.Margin = New Padding(4, 3, 4, 3)
        tbPixelStat.Multiline = True
        tbPixelStat.Name = "tbPixelStat"
        tbPixelStat.ScrollBars = ScrollBars.Both
        tbPixelStat.Size = New Size(1025, 241)
        tbPixelStat.TabIndex = 4
        tbPixelStat.WordWrap = False
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Font = New Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbLog.Location = New Point(4, 24)
        tbLog.Margin = New Padding(4, 3, 4, 3)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.ScrollBars = ScrollBars.Both
        tbLog.Size = New Size(1054, 289)
        tbLog.TabIndex = 0
        tbLog.WordWrap = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(5, 6)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(27, 15)
        Label1.TabIndex = 1
        Label1.Text = "Log"
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {tsmiFile, tsmiFileList, tsmiAction})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Padding = New Padding(7, 2, 0, 2)
        msMain.Size = New Size(1568, 24)
        msMain.TabIndex = 3
        msMain.Text = "MenuStrip1"
        ' 
        ' tsmiFile
        ' 
        tsmiFile.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_AddFiles, tsmiFile_OpenWorkingDir, ToolStripMenuItem1, tsmiFile_SaveAllFilesHisto, tsmiFile_SaveFITSandStats, ToolStripMenuItem3, tsmiFile_Exit})
        tsmiFile.Name = "tsmiFile"
        tsmiFile.Size = New Size(37, 20)
        tsmiFile.Text = "File"
        ' 
        ' tsmiFile_AddFiles
        ' 
        tsmiFile_AddFiles.Name = "tsmiFile_AddFiles"
        tsmiFile_AddFiles.Size = New Size(226, 22)
        tsmiFile_AddFiles.Text = "Add file(s)"
        ' 
        ' tsmiFile_OpenWorkingDir
        ' 
        tsmiFile_OpenWorkingDir.Name = "tsmiFile_OpenWorkingDir"
        tsmiFile_OpenWorkingDir.Size = New Size(226, 22)
        tsmiFile_OpenWorkingDir.Text = "Open working directory"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(223, 6)
        ' 
        ' tsmiFile_SaveAllFilesHisto
        ' 
        tsmiFile_SaveAllFilesHisto.Name = "tsmiFile_SaveAllFilesHisto"
        tsmiFile_SaveAllFilesHisto.Size = New Size(226, 22)
        tsmiFile_SaveAllFilesHisto.Text = "Save all-files histogram (XLS)"
        ' 
        ' tsmiFile_SaveFITSandStats
        ' 
        tsmiFile_SaveFITSandStats.Name = "tsmiFile_SaveFITSandStats"
        tsmiFile_SaveFITSandStats.Size = New Size(226, 22)
        tsmiFile_SaveFITSandStats.Text = "Save FITS and statistics (XLS)"
        ' 
        ' ToolStripMenuItem3
        ' 
        ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        ToolStripMenuItem3.Size = New Size(223, 6)
        ' 
        ' tsmiFile_Exit
        ' 
        tsmiFile_Exit.Name = "tsmiFile_Exit"
        tsmiFile_Exit.Size = New Size(226, 22)
        tsmiFile_Exit.Text = "Exit"
        ' 
        ' tsmiFileList
        ' 
        tsmiFileList.DropDownItems.AddRange(New ToolStripItem() {tsmiFileList_ClearList})
        tsmiFileList.Name = "tsmiFileList"
        tsmiFileList.Size = New Size(55, 20)
        tsmiFileList.Text = "File list"
        ' 
        ' tsmiFileList_ClearList
        ' 
        tsmiFileList_ClearList.Name = "tsmiFileList_ClearList"
        tsmiFileList_ClearList.Size = New Size(119, 22)
        tsmiFileList_ClearList.Text = "Clear list"
        ' 
        ' tsmiAction
        ' 
        tsmiAction.DropDownItems.AddRange(New ToolStripItem() {tsmiAction_Run, ToolStripMenuItem2, tsmiAction_ClearLog, tsmiAction_StackSpecial, tsmiAction_Mode, ToolStripMenuItem4, tsmiAction_SelectAll, tsmiAction_DeSelectAll, tsmiAction_DSSParam})
        tsmiAction.Name = "tsmiAction"
        tsmiAction.Size = New Size(59, 20)
        tsmiAction.Text = "Actions"
        ' 
        ' tsmiAction_Run
        ' 
        tsmiAction_Run.Name = "tsmiAction_Run"
        tsmiAction_Run.Size = New Size(185, 22)
        tsmiAction_Run.Text = "Run"
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(182, 6)
        ' 
        ' tsmiAction_ClearLog
        ' 
        tsmiAction_ClearLog.Name = "tsmiAction_ClearLog"
        tsmiAction_ClearLog.Size = New Size(185, 22)
        tsmiAction_ClearLog.Text = "Clear log"
        ' 
        ' tsmiAction_StackSpecial
        ' 
        tsmiAction_StackSpecial.Name = "tsmiAction_StackSpecial"
        tsmiAction_StackSpecial.Size = New Size(185, 22)
        tsmiAction_StackSpecial.Text = "Stack (special code)"
        ' 
        ' tsmiAction_Mode
        ' 
        tsmiAction_Mode.DropDownItems.AddRange(New ToolStripItem() {tsmiAction_Mode_StoreAlignedFiles})
        tsmiAction_Mode.Name = "tsmiAction_Mode"
        tsmiAction_Mode.Size = New Size(185, 22)
        tsmiAction_Mode.Text = "Set mode"
        ' 
        ' tsmiAction_Mode_StoreAlignedFiles
        ' 
        tsmiAction_Mode_StoreAlignedFiles.Name = "tsmiAction_Mode_StoreAlignedFiles"
        tsmiAction_Mode_StoreAlignedFiles.Size = New Size(167, 22)
        tsmiAction_Mode_StoreAlignedFiles.Text = "Store aligned files"
        ' 
        ' ToolStripMenuItem4
        ' 
        ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        ToolStripMenuItem4.Size = New Size(182, 6)
        ' 
        ' tsmiAction_SelectAll
        ' 
        tsmiAction_SelectAll.Name = "tsmiAction_SelectAll"
        tsmiAction_SelectAll.Size = New Size(185, 22)
        tsmiAction_SelectAll.Text = "Select all files"
        ' 
        ' tsmiAction_DeSelectAll
        ' 
        tsmiAction_DeSelectAll.Name = "tsmiAction_DeSelectAll"
        tsmiAction_DeSelectAll.Size = New Size(185, 22)
        tsmiAction_DeSelectAll.Text = "Deselect all files"
        ' 
        ' tsmiAction_DSSParam
        ' 
        tsmiAction_DSSParam.Name = "tsmiAction_DSSParam"
        tsmiAction_DSSParam.Size = New Size(185, 22)
        tsmiAction_DSSParam.Text = "Read DSS parameters"
        ' 
        ' ofdMain
        ' 
        ofdMain.FileName = "OpenFileDialog1"
        ' 
        ' ssMain
        ' 
        ssMain.Items.AddRange(New ToolStripItem() {tspbMain})
        ssMain.Location = New Point(0, 1050)
        ssMain.Name = "ssMain"
        ssMain.Padding = New Padding(1, 0, 16, 0)
        ssMain.Size = New Size(1568, 24)
        ssMain.TabIndex = 4
        ssMain.Text = "StatusStrip1"
        ' 
        ' tspbMain
        ' 
        tspbMain.Name = "tspbMain"
        tspbMain.Size = New Size(117, 18)
        ' 
        ' frmMultiFileAction
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1568, 1074)
        Controls.Add(ssMain)
        Controls.Add(scMain)
        Controls.Add(msMain)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = msMain
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmMultiFileAction"
        Text = "Multi-file action"
        CType(adgvMain, ComponentModel.ISupportInitialize).EndInit()
        cmsTable.ResumeLayout(False)
        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        CType(scMain, ComponentModel.ISupportInitialize).EndInit()
        scMain.ResumeLayout(False)
        scButtom.Panel1.ResumeLayout(False)
        scButtom.Panel2.ResumeLayout(False)
        CType(scButtom, ComponentModel.ISupportInitialize).EndInit()
        scButtom.ResumeLayout(False)
        scBottomRight.Panel1.ResumeLayout(False)
        scBottomRight.Panel2.ResumeLayout(False)
        scBottomRight.Panel2.PerformLayout()
        CType(scBottomRight, ComponentModel.ISupportInitialize).EndInit()
        scBottomRight.ResumeLayout(False)
        gbAspects.ResumeLayout(False)
        tcAspect.ResumeLayout(False)
        tpFITSHeader.ResumeLayout(False)
        tpFITSHeader.PerformLayout()
        tbCombinedROI.ResumeLayout(False)
        CType(pbImage, ComponentModel.ISupportInitialize).EndInit()
        cmsMain.ResumeLayout(False)
        tbSinglePixelStat.ResumeLayout(False)
        tbSinglePixelStat.PerformLayout()
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ssMain.ResumeLayout(False)
        ssMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents adgvMain As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents scButtom As SplitContainer
    Friend WithEvents tbLog As TextBox
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_AddFiles As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents tsmiFileList As ToolStripMenuItem
    Friend WithEvents tsmiFileList_ClearList As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenWorkingDir As ToolStripMenuItem
    Friend WithEvents tsmiAction As ToolStripMenuItem
    Friend WithEvents tsmiAction_StackSpecial As ToolStripMenuItem
    Friend WithEvents tsmiAction_Run As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents tsmiAction_ClearLog As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents ofdMain As OpenFileDialog
    Friend WithEvents tbFITSHeader As TextBox
    Friend WithEvents pbImage As PictureBoxEx
    Friend WithEvents gbAspects As GroupBox
    Friend WithEvents tcAspect As TabControl
    Friend WithEvents tpFITSHeader As TabPage
    Friend WithEvents tbCombinedROI As TabPage
    Friend WithEvents tsmiAction_Mode As ToolStripMenuItem
    Friend WithEvents tsmiAction_Mode_StoreAlignedFiles As ToolStripMenuItem
    Friend WithEvents tbSinglePixelStat As TabPage
    Friend WithEvents tbPixelStat As TextBox
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tspbMain As ToolStripProgressBar
    Friend WithEvents tsmiFile_SaveAllFilesHisto As ToolStripMenuItem
    Friend WithEvents sfdMain As SaveFileDialog
    Friend WithEvents tsmiFile_SaveFITSandStats As ToolStripMenuItem
    Friend WithEvents cmsMain As ContextMenuStrip
    Friend WithEvents cmsMain_ToClipboard As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
    Friend WithEvents tsmiAction_SelectAll As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripSeparator
    Friend WithEvents tsmiAction_DeSelectAll As ToolStripMenuItem
    Friend WithEvents tsmiAction_DSSParam As ToolStripMenuItem
    Friend WithEvents scBottomRight As SplitContainer
    Friend WithEvents cmsTable As ContextMenuStrip
    Friend WithEvents cmsTable_OpenFile As ToolStripMenuItem
End Class
