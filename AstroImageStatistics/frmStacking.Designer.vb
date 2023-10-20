<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStacking
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveStatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MaxMinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MeanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StdDevToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SumToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MaxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_Stack = New System.Windows.Forms.ToolStripMenuItem()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        Me.dgvcb_Process = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgv_tbFile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pgMain = New System.Windows.Forms.PropertyGrid()
        Me.MenuStrip1.SuspendLayout()
        Me.ssMain.SuspendLayout()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1308, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveStatisticsToolStripMenuItem, Me.tsmiFile_Stack})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'SaveStatisticsToolStripMenuItem
        '
        Me.SaveStatisticsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MaxMinToolStripMenuItem, Me.MeanToolStripMenuItem, Me.StdDevToolStripMenuItem, Me.SumToolStripMenuItem, Me.MaxToolStripMenuItem})
        Me.SaveStatisticsToolStripMenuItem.Name = "SaveStatisticsToolStripMenuItem"
        Me.SaveStatisticsToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.SaveStatisticsToolStripMenuItem.Text = "Save statistics"
        '
        'MaxMinToolStripMenuItem
        '
        Me.MaxMinToolStripMenuItem.Name = "MaxMinToolStripMenuItem"
        Me.MaxMinToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.MaxMinToolStripMenuItem.Text = "Max-Min"
        '
        'MeanToolStripMenuItem
        '
        Me.MeanToolStripMenuItem.Name = "MeanToolStripMenuItem"
        Me.MeanToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.MeanToolStripMenuItem.Text = "Mean"
        '
        'StdDevToolStripMenuItem
        '
        Me.StdDevToolStripMenuItem.Name = "StdDevToolStripMenuItem"
        Me.StdDevToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.StdDevToolStripMenuItem.Text = "Std Dev"
        '
        'SumToolStripMenuItem
        '
        Me.SumToolStripMenuItem.Name = "SumToolStripMenuItem"
        Me.SumToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.SumToolStripMenuItem.Text = "Sum"
        '
        'MaxToolStripMenuItem
        '
        Me.MaxToolStripMenuItem.Name = "MaxToolStripMenuItem"
        Me.MaxToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.MaxToolStripMenuItem.Text = "Max"
        '
        'tsmiFile_Stack
        '
        Me.tsmiFile_Stack.Name = "tsmiFile_Stack"
        Me.tsmiFile_Stack.Size = New System.Drawing.Size(146, 22)
        Me.tsmiFile_Stack.Text = "Stack"
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslStatus})
        Me.ssMain.Location = New System.Drawing.Point(0, 1002)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1308, 22)
        Me.ssMain.TabIndex = 1
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(22, 17)
        Me.tsslStatus.Text = "---"
        '
        'tbLog
        '
        Me.tbLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLog.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLog.Location = New System.Drawing.Point(12, 859)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLog.Size = New System.Drawing.Size(1284, 131)
        Me.tbLog.TabIndex = 2
        Me.tbLog.WordWrap = False
        '
        'dgvMain
        '
        Me.dgvMain.AllowUserToAddRows = False
        Me.dgvMain.AllowUserToDeleteRows = False
        Me.dgvMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvcb_Process, Me.dgv_tbFile})
        Me.dgvMain.Location = New System.Drawing.Point(317, 27)
        Me.dgvMain.Name = "dgvMain"
        Me.dgvMain.Size = New System.Drawing.Size(979, 359)
        Me.dgvMain.TabIndex = 3
        '
        'dgvcb_Process
        '
        Me.dgvcb_Process.HeaderText = "Process"
        Me.dgvcb_Process.Name = "dgvcb_Process"
        '
        'dgv_tbFile
        '
        Me.dgv_tbFile.HeaderText = "File"
        Me.dgv_tbFile.Name = "dgv_tbFile"
        Me.dgv_tbFile.ReadOnly = True
        '
        'pgMain
        '
        Me.pgMain.Location = New System.Drawing.Point(12, 27)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(299, 359)
        Me.pgMain.TabIndex = 4
        '
        'frmStacking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1308, 1024)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.dgvMain)
        Me.Controls.Add(Me.tbLog)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmStacking"
        Me.Text = "Stacking"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveStatisticsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MaxMinToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MeanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StdDevToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SumToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MaxToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents tbLog As TextBox
    Friend WithEvents dgvMain As DataGridView
    Friend WithEvents tsmiFile_Stack As ToolStripMenuItem
    Friend WithEvents dgvcb_Process As DataGridViewCheckBoxColumn
    Friend WithEvents dgv_tbFile As DataGridViewTextBoxColumn
    Friend WithEvents pgMain As PropertyGrid
End Class
