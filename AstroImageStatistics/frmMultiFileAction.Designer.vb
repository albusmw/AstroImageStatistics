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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_OpenStackDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAction_Run = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ClearLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAction_Stack = New System.Windows.Forms.ToolStripMenuItem()
        Me.ofdMain = New System.Windows.Forms.OpenFileDialog()
        Me.pbImage = New AstroImageStatistics.PictureBoxEx()
        Me.SetModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StoreAlignedFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.msMain.SuspendLayout()
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.adgvMain.Size = New System.Drawing.Size(1310, 302)
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
        Me.scMain.Size = New System.Drawing.Size(1320, 892)
        Me.scMain.SplitterDistance = 308
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
        Me.scButtom.Size = New System.Drawing.Size(1317, 577)
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
        Me.tcMain.Size = New System.Drawing.Size(395, 577)
        Me.tcMain.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pgMain)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(387, 551)
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
        Me.pgMain.Size = New System.Drawing.Size(375, 539)
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
        Me.gbAspects.Size = New System.Drawing.Size(912, 389)
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
        Me.tcAspect.Location = New System.Drawing.Point(6, 19)
        Me.tcAspect.Name = "tcAspect"
        Me.tcAspect.SelectedIndex = 0
        Me.tcAspect.Size = New System.Drawing.Size(906, 364)
        Me.tcAspect.TabIndex = 0
        '
        'tpFITSHeader
        '
        Me.tpFITSHeader.BackColor = System.Drawing.SystemColors.Control
        Me.tpFITSHeader.Controls.Add(Me.tbFITSHeader)
        Me.tpFITSHeader.Location = New System.Drawing.Point(4, 22)
        Me.tpFITSHeader.Name = "tpFITSHeader"
        Me.tpFITSHeader.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFITSHeader.Size = New System.Drawing.Size(898, 338)
        Me.tpFITSHeader.TabIndex = 0
        Me.tpFITSHeader.Text = "FITS header"
        '
        'tbFITSHeader
        '
        Me.tbFITSHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFITSHeader.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFITSHeader.Location = New System.Drawing.Point(2, 3)
        Me.tbFITSHeader.Multiline = True
        Me.tbFITSHeader.Name = "tbFITSHeader"
        Me.tbFITSHeader.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbFITSHeader.Size = New System.Drawing.Size(890, 332)
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
        Me.tbCombinedROI.Size = New System.Drawing.Size(898, 338)
        Me.tbCombinedROI.TabIndex = 1
        Me.tbCombinedROI.Text = "Combined ROI"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 395)
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
        Me.tbLog.Location = New System.Drawing.Point(3, 411)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLog.Size = New System.Drawing.Size(912, 162)
        Me.tbLog.TabIndex = 0
        Me.tbLog.WordWrap = False
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.FileListToolStripMenuItem, Me.ActionsToolStripMenuItem})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(1344, 24)
        Me.msMain.TabIndex = 3
        Me.msMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddFilesToolStripMenuItem, Me.tsmiFile_OpenStackDir, Me.ToolStripMenuItem1, Me.tsmiFile_Exit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'AddFilesToolStripMenuItem
        '
        Me.AddFilesToolStripMenuItem.Name = "AddFilesToolStripMenuItem"
        Me.AddFilesToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.AddFilesToolStripMenuItem.Text = "Add file(s)"
        '
        'tsmiFile_OpenStackDir
        '
        Me.tsmiFile_OpenStackDir.Name = "tsmiFile_OpenStackDir"
        Me.tsmiFile_OpenStackDir.Size = New System.Drawing.Size(200, 22)
        Me.tsmiFile_OpenStackDir.Text = "Open stacking directory"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(197, 6)
        '
        'tsmiFile_Exit
        '
        Me.tsmiFile_Exit.Name = "tsmiFile_Exit"
        Me.tsmiFile_Exit.Size = New System.Drawing.Size(200, 22)
        Me.tsmiFile_Exit.Text = "Exit"
        '
        'FileListToolStripMenuItem
        '
        Me.FileListToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClearListToolStripMenuItem})
        Me.FileListToolStripMenuItem.Name = "FileListToolStripMenuItem"
        Me.FileListToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.FileListToolStripMenuItem.Text = "File list"
        '
        'ClearListToolStripMenuItem
        '
        Me.ClearListToolStripMenuItem.Name = "ClearListToolStripMenuItem"
        Me.ClearListToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.ClearListToolStripMenuItem.Text = "Clear list"
        '
        'ActionsToolStripMenuItem
        '
        Me.ActionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAction_Run, Me.ToolStripMenuItem2, Me.ClearLogToolStripMenuItem, Me.tsmiAction_Stack, Me.SetModeToolStripMenuItem})
        Me.ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem"
        Me.ActionsToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.ActionsToolStripMenuItem.Text = "Actions"
        '
        'tsmiAction_Run
        '
        Me.tsmiAction_Run.Name = "tsmiAction_Run"
        Me.tsmiAction_Run.Size = New System.Drawing.Size(180, 22)
        Me.tsmiAction_Run.Text = "Run"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(177, 6)
        '
        'ClearLogToolStripMenuItem
        '
        Me.ClearLogToolStripMenuItem.Name = "ClearLogToolStripMenuItem"
        Me.ClearLogToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ClearLogToolStripMenuItem.Text = "Clear log"
        '
        'tsmiAction_Stack
        '
        Me.tsmiAction_Stack.Name = "tsmiAction_Stack"
        Me.tsmiAction_Stack.Size = New System.Drawing.Size(180, 22)
        Me.tsmiAction_Stack.Text = "Stack (special code)"
        '
        'ofdMain
        '
        Me.ofdMain.FileName = "OpenFileDialog1"
        '
        'pbImage
        '
        Me.pbImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.pbImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
        Me.pbImage.Location = New System.Drawing.Point(3, 3)
        Me.pbImage.Name = "pbImage"
        Me.pbImage.Size = New System.Drawing.Size(889, 332)
        Me.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbImage.TabIndex = 5
        Me.pbImage.TabStop = False
        '
        'SetModeToolStripMenuItem
        '
        Me.SetModeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StoreAlignedFilesToolStripMenuItem})
        Me.SetModeToolStripMenuItem.Name = "SetModeToolStripMenuItem"
        Me.SetModeToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SetModeToolStripMenuItem.Text = "Set mode"
        '
        'StoreAlignedFilesToolStripMenuItem
        '
        Me.StoreAlignedFilesToolStripMenuItem.Name = "StoreAlignedFilesToolStripMenuItem"
        Me.StoreAlignedFilesToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.StoreAlignedFilesToolStripMenuItem.Text = "Store aligned files"
        '
        'frmMultiFileAction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 931)
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
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents FileListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenStackDir As ToolStripMenuItem
    Friend WithEvents ActionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiAction_Stack As ToolStripMenuItem
    Friend WithEvents tsmiAction_Run As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents ClearLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents ofdMain As OpenFileDialog
    Friend WithEvents tbFITSHeader As TextBox
    Friend WithEvents pbImage As PictureBoxEx
    Friend WithEvents gbAspects As GroupBox
    Friend WithEvents tcAspect As TabControl
    Friend WithEvents tpFITSHeader As TabPage
    Friend WithEvents tbCombinedROI As TabPage
    Friend WithEvents SetModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StoreAlignedFilesToolStripMenuItem As ToolStripMenuItem
End Class
