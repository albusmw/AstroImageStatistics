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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMultiFileAction))
        Me.adgvMain = New Zuby.ADGV.AdvancedDataGridView()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.scButtom = New System.Windows.Forms.SplitContainer()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pgMain = New System.Windows.Forms.PropertyGrid()
        Me.gbAspects = New System.Windows.Forms.GroupBox()
        Me.tcAspect = New System.Windows.Forms.TabControl()
        Me.tpFITSHeader = New System.Windows.Forms.TabPage()
        Me.tbFITSHeader = New System.Windows.Forms.TextBox()
        Me.tbCombinedROI = New System.Windows.Forms.TabPage()
        Me.pbImage = New AstroImageStatistics.PictureBoxEx()
        Me.tbSinglePixelStat = New System.Windows.Forms.TabPage()
        Me.tbPixelStat = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_AddFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_OpenWorkingDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFileList = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFileList_ClearList = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAction = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAction_Run = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiAction_ClearLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAction_StackSpecial = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAction_Mode = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAction_Mode_StoreAlignedFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.ofdMain = New System.Windows.Forms.OpenFileDialog()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tspbMain = New System.Windows.Forms.ToolStripProgressBar()
        CType(Me.adgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        CType(Me.scButtom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scButtom.Panel1.SuspendLayout()
        Me.scButtom.Panel2.SuspendLayout()
        Me.scButtom.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.gbAspects.SuspendLayout()
        Me.tcAspect.SuspendLayout()
        Me.tpFITSHeader.SuspendLayout()
        Me.tbCombinedROI.SuspendLayout()
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbSinglePixelStat.SuspendLayout()
        Me.msMain.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'adgvMain
        '
        Me.adgvMain.AllowDrop = True
        Me.adgvMain.AllowUserToAddRows = False
        Me.adgvMain.AllowUserToDeleteRows = False
        Me.adgvMain.AllowUserToOrderColumns = True
        Me.adgvMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.adgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.adgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.adgvMain.FilterAndSortEnabled = True
        Me.adgvMain.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.adgvMain.Location = New System.Drawing.Point(7, 3)
        Me.adgvMain.MultiSelect = False
        Me.adgvMain.Name = "adgvMain"
        Me.adgvMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adgvMain.RowHeadersVisible = False
        Me.adgvMain.ShowEditingIcon = False
        Me.adgvMain.ShowRowErrors = False
        Me.adgvMain.Size = New System.Drawing.Size(1310, 293)
        Me.adgvMain.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.adgvMain.TabIndex = 1
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(12, 27)
        Me.scMain.Name = "scMain"
        Me.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.adgvMain)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.scButtom)
        Me.scMain.Size = New System.Drawing.Size(1320, 868)
        Me.scMain.SplitterDistance = 299
        Me.scMain.TabIndex = 2
        '
        'scButtom
        '
        Me.scButtom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scButtom.Location = New System.Drawing.Point(3, 3)
        Me.scButtom.Name = "scButtom"
        '
        'scButtom.Panel1
        '
        Me.scButtom.Panel1.Controls.Add(Me.tcMain)
        '
        'scButtom.Panel2
        '
        Me.scButtom.Panel2.Controls.Add(Me.gbAspects)
        Me.scButtom.Panel2.Controls.Add(Me.Label1)
        Me.scButtom.Panel2.Controls.Add(Me.tbLog)
        Me.scButtom.Size = New System.Drawing.Size(1317, 562)
        Me.scButtom.SplitterDistance = 395
        Me.scButtom.TabIndex = 1
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.TabPage2)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(395, 562)
        Me.tcMain.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pgMain)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(387, 536)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Configure"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pgMain
        '
        Me.pgMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgMain.Location = New System.Drawing.Point(6, 6)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(375, 524)
        Me.pgMain.TabIndex = 0
        '
        'gbAspects
        '
        Me.gbAspects.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbAspects.Controls.Add(Me.tcAspect)
        Me.gbAspects.Location = New System.Drawing.Point(3, 3)
        Me.gbAspects.Name = "gbAspects"
        Me.gbAspects.Size = New System.Drawing.Size(912, 374)
        Me.gbAspects.TabIndex = 6
        Me.gbAspects.TabStop = False
        Me.gbAspects.Text = "Aspect"
        '
        'tcAspect
        '
        Me.tcAspect.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcAspect.Controls.Add(Me.tpFITSHeader)
        Me.tcAspect.Controls.Add(Me.tbCombinedROI)
        Me.tcAspect.Controls.Add(Me.tbSinglePixelStat)
        Me.tcAspect.Location = New System.Drawing.Point(6, 19)
        Me.tcAspect.Name = "tcAspect"
        Me.tcAspect.SelectedIndex = 0
        Me.tcAspect.Size = New System.Drawing.Size(906, 349)
        Me.tcAspect.TabIndex = 0
        '
        'tpFITSHeader
        '
        Me.tpFITSHeader.BackColor = System.Drawing.SystemColors.Control
        Me.tpFITSHeader.Controls.Add(Me.tbFITSHeader)
        Me.tpFITSHeader.Location = New System.Drawing.Point(4, 22)
        Me.tpFITSHeader.Name = "tpFITSHeader"
        Me.tpFITSHeader.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFITSHeader.Size = New System.Drawing.Size(898, 323)
        Me.tpFITSHeader.TabIndex = 0
        Me.tpFITSHeader.Text = "FITS header"
        '
        'tbFITSHeader
        '
        Me.tbFITSHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFITSHeader.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFITSHeader.Location = New System.Drawing.Point(2, 6)
        Me.tbFITSHeader.Multiline = True
        Me.tbFITSHeader.Name = "tbFITSHeader"
        Me.tbFITSHeader.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbFITSHeader.Size = New System.Drawing.Size(890, 314)
        Me.tbFITSHeader.TabIndex = 2
        Me.tbFITSHeader.WordWrap = False
        '
        'tbCombinedROI
        '
        Me.tbCombinedROI.BackColor = System.Drawing.SystemColors.Control
        Me.tbCombinedROI.Controls.Add(Me.pbImage)
        Me.tbCombinedROI.Location = New System.Drawing.Point(4, 22)
        Me.tbCombinedROI.Name = "tbCombinedROI"
        Me.tbCombinedROI.Padding = New System.Windows.Forms.Padding(3)
        Me.tbCombinedROI.Size = New System.Drawing.Size(898, 323)
        Me.tbCombinedROI.TabIndex = 1
        Me.tbCombinedROI.Text = "(Combined) ROI"
        '
        'pbImage
        '
        Me.pbImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.pbImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
        Me.pbImage.Location = New System.Drawing.Point(3, 6)
        Me.pbImage.Name = "pbImage"
        Me.pbImage.Size = New System.Drawing.Size(889, 314)
        Me.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbImage.TabIndex = 5
        Me.pbImage.TabStop = False
        '
        'tbSinglePixelStat
        '
        Me.tbSinglePixelStat.BackColor = System.Drawing.SystemColors.Control
        Me.tbSinglePixelStat.Controls.Add(Me.tbPixelStat)
        Me.tbSinglePixelStat.Location = New System.Drawing.Point(4, 22)
        Me.tbSinglePixelStat.Name = "tbSinglePixelStat"
        Me.tbSinglePixelStat.Padding = New System.Windows.Forms.Padding(3)
        Me.tbSinglePixelStat.Size = New System.Drawing.Size(898, 323)
        Me.tbSinglePixelStat.TabIndex = 2
        Me.tbSinglePixelStat.Text = "Single pixel statistics"
        '
        'tbPixelStat
        '
        Me.tbPixelStat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbPixelStat.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPixelStat.Location = New System.Drawing.Point(6, 6)
        Me.tbPixelStat.Multiline = True
        Me.tbPixelStat.Name = "tbPixelStat"
        Me.tbPixelStat.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbPixelStat.Size = New System.Drawing.Size(886, 311)
        Me.tbPixelStat.TabIndex = 4
        Me.tbPixelStat.WordWrap = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 380)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Log"
        '
        'tbLog
        '
        Me.tbLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLog.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLog.Location = New System.Drawing.Point(3, 396)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLog.Size = New System.Drawing.Size(912, 162)
        Me.tbLog.TabIndex = 0
        Me.tbLog.WordWrap = False
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile, Me.tsmiFileList, Me.tsmiAction})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(1344, 24)
        Me.msMain.TabIndex = 3
        Me.msMain.Text = "MenuStrip1"
        '
        'tsmiFile
        '
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile_AddFiles, Me.tsmiFile_OpenWorkingDir, Me.ToolStripMenuItem1, Me.tsmiFile_Exit})
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Size = New System.Drawing.Size(37, 20)
        Me.tsmiFile.Text = "File"
        '
        'tsmiFile_AddFiles
        '
        Me.tsmiFile_AddFiles.Name = "tsmiFile_AddFiles"
        Me.tsmiFile_AddFiles.Size = New System.Drawing.Size(199, 22)
        Me.tsmiFile_AddFiles.Text = "Add file(s)"
        '
        'tsmiFile_OpenWorkingDir
        '
        Me.tsmiFile_OpenWorkingDir.Name = "tsmiFile_OpenWorkingDir"
        Me.tsmiFile_OpenWorkingDir.Size = New System.Drawing.Size(199, 22)
        Me.tsmiFile_OpenWorkingDir.Text = "Open working directory"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(196, 6)
        '
        'tsmiFile_Exit
        '
        Me.tsmiFile_Exit.Name = "tsmiFile_Exit"
        Me.tsmiFile_Exit.Size = New System.Drawing.Size(199, 22)
        Me.tsmiFile_Exit.Text = "Exit"
        '
        'tsmiFileList
        '
        Me.tsmiFileList.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFileList_ClearList})
        Me.tsmiFileList.Name = "tsmiFileList"
        Me.tsmiFileList.Size = New System.Drawing.Size(55, 20)
        Me.tsmiFileList.Text = "File list"
        '
        'tsmiFileList_ClearList
        '
        Me.tsmiFileList_ClearList.Name = "tsmiFileList_ClearList"
        Me.tsmiFileList_ClearList.Size = New System.Drawing.Size(119, 22)
        Me.tsmiFileList_ClearList.Text = "Clear list"
        '
        'tsmiAction
        '
        Me.tsmiAction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAction_Run, Me.ToolStripMenuItem2, Me.tsmiAction_ClearLog, Me.tsmiAction_StackSpecial, Me.tsmiAction_Mode})
        Me.tsmiAction.Name = "tsmiAction"
        Me.tsmiAction.Size = New System.Drawing.Size(59, 20)
        Me.tsmiAction.Text = "Actions"
        '
        'tsmiAction_Run
        '
        Me.tsmiAction_Run.Name = "tsmiAction_Run"
        Me.tsmiAction_Run.Size = New System.Drawing.Size(178, 22)
        Me.tsmiAction_Run.Text = "Run"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(175, 6)
        '
        'tsmiAction_ClearLog
        '
        Me.tsmiAction_ClearLog.Name = "tsmiAction_ClearLog"
        Me.tsmiAction_ClearLog.Size = New System.Drawing.Size(178, 22)
        Me.tsmiAction_ClearLog.Text = "Clear log"
        '
        'tsmiAction_StackSpecial
        '
        Me.tsmiAction_StackSpecial.Name = "tsmiAction_StackSpecial"
        Me.tsmiAction_StackSpecial.Size = New System.Drawing.Size(178, 22)
        Me.tsmiAction_StackSpecial.Text = "Stack (special code)"
        '
        'tsmiAction_Mode
        '
        Me.tsmiAction_Mode.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAction_Mode_StoreAlignedFiles})
        Me.tsmiAction_Mode.Name = "tsmiAction_Mode"
        Me.tsmiAction_Mode.Size = New System.Drawing.Size(178, 22)
        Me.tsmiAction_Mode.Text = "Set mode"
        '
        'tsmiAction_Mode_StoreAlignedFiles
        '
        Me.tsmiAction_Mode_StoreAlignedFiles.Name = "tsmiAction_Mode_StoreAlignedFiles"
        Me.tsmiAction_Mode_StoreAlignedFiles.Size = New System.Drawing.Size(167, 22)
        Me.tsmiAction_Mode_StoreAlignedFiles.Text = "Store aligned files"
        '
        'ofdMain
        '
        Me.ofdMain.FileName = "OpenFileDialog1"
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tspbMain})
        Me.ssMain.Location = New System.Drawing.Point(0, 909)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1344, 22)
        Me.ssMain.TabIndex = 4
        Me.ssMain.Text = "StatusStrip1"
        '
        'tspbMain
        '
        Me.tspbMain.Name = "tspbMain"
        Me.tspbMain.Size = New System.Drawing.Size(100, 16)
        '
        'frmMultiFileAction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 931)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.msMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.msMain
        Me.Name = "frmMultiFileAction"
        Me.Text = "Multi-file action"
        CType(Me.adgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.scButtom.Panel1.ResumeLayout(False)
        Me.scButtom.Panel2.ResumeLayout(False)
        Me.scButtom.Panel2.PerformLayout()
        CType(Me.scButtom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scButtom.ResumeLayout(False)
        Me.tcMain.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.gbAspects.ResumeLayout(False)
        Me.tcAspect.ResumeLayout(False)
        Me.tpFITSHeader.ResumeLayout(False)
        Me.tpFITSHeader.PerformLayout()
        Me.tbCombinedROI.ResumeLayout(False)
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbSinglePixelStat.ResumeLayout(False)
        Me.tbSinglePixelStat.PerformLayout()
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents adgvMain As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents scButtom As SplitContainer
    Friend WithEvents tcMain As TabControl
    Friend WithEvents TabPage2 As TabPage
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
End Class
