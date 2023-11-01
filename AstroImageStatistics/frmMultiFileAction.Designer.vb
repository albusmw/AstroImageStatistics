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
        Me.adgvMain = New Zuby.ADGV.AdvancedDataGridView()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.scButtom = New System.Windows.Forms.SplitContainer()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pgMain = New System.Windows.Forms.PropertyGrid()
        Me.btnStackCorr = New System.Windows.Forms.Button()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.btnClearLog = New System.Windows.Forms.Button()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_OpenStackDir = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.MenuStrip1.SuspendLayout()
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
        Me.adgvMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.adgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.adgvMain.FilterAndSortEnabled = True
        Me.adgvMain.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.adgvMain.Location = New System.Drawing.Point(7, 3)
        Me.adgvMain.Name = "adgvMain"
        Me.adgvMain.ReadOnly = True
        Me.adgvMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adgvMain.RowHeadersVisible = False
        Me.adgvMain.ShowEditingIcon = False
        Me.adgvMain.ShowRowErrors = False
        Me.adgvMain.Size = New System.Drawing.Size(1323, 325)
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
        Me.scMain.Size = New System.Drawing.Size(1333, 956)
        Me.scMain.SplitterDistance = 331
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
        Me.scButtom.Panel2.Controls.Add(Me.btnStackCorr)
        Me.scButtom.Panel2.Controls.Add(Me.btnRun)
        Me.scButtom.Panel2.Controls.Add(Me.btnClearLog)
        Me.scButtom.Panel2.Controls.Add(Me.tbLog)
        Me.scButtom.Size = New System.Drawing.Size(1330, 618)
        Me.scButtom.SplitterDistance = 399
        Me.scButtom.TabIndex = 1
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.TabPage2)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(399, 618)
        Me.tcMain.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pgMain)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(391, 592)
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
        Me.pgMain.Size = New System.Drawing.Size(379, 580)
        Me.pgMain.TabIndex = 0
        '
        'btnStackCorr
        '
        Me.btnStackCorr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStackCorr.Location = New System.Drawing.Point(96, 587)
        Me.btnStackCorr.Name = "btnStackCorr"
        Me.btnStackCorr.Size = New System.Drawing.Size(108, 23)
        Me.btnStackCorr.TabIndex = 2
        Me.btnStackCorr.Text = "Correlate and stack"
        Me.btnStackCorr.UseVisualStyleBackColor = True
        '
        'btnRun
        '
        Me.btnRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRun.Location = New System.Drawing.Point(15, 587)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 0
        Me.btnRun.Text = "Just stack"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'btnClearLog
        '
        Me.btnClearLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearLog.Location = New System.Drawing.Point(849, 585)
        Me.btnClearLog.Name = "btnClearLog"
        Me.btnClearLog.Size = New System.Drawing.Size(75, 23)
        Me.btnClearLog.TabIndex = 1
        Me.btnClearLog.Text = "Clear"
        Me.btnClearLog.UseVisualStyleBackColor = True
        '
        'tbLog
        '
        Me.tbLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLog.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLog.Location = New System.Drawing.Point(3, 22)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLog.Size = New System.Drawing.Size(921, 559)
        Me.tbLog.TabIndex = 0
        Me.tbLog.WordWrap = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.FileListToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1357, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
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
        'frmMultiFileAction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1357, 995)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
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
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents adgvMain As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents scButtom As SplitContainer
    Friend WithEvents tcMain As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents tbLog As TextBox
    Friend WithEvents btnRun As Button
    Friend WithEvents btnClearLog As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents btnStackCorr As Button
    Friend WithEvents FileListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenStackDir As ToolStripMenuItem
End Class
