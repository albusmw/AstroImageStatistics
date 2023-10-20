<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMultiFileAction
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.adgvMain = New Zuby.ADGV.AdvancedDataGridView()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.scButtom = New System.Windows.Forms.SplitContainer()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.tbCombine = New System.Windows.Forms.TabPage()
        Me.cbSaveMax = New System.Windows.Forms.CheckBox()
        Me.cbSaveMin = New System.Windows.Forms.CheckBox()
        Me.cbSaveSigma = New System.Windows.Forms.CheckBox()
        Me.cbSaveMean = New System.Windows.Forms.CheckBox()
        Me.cbCalcHisto = New System.Windows.Forms.CheckBox()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnClearLog = New System.Windows.Forms.Button()
        Me.tbLog = New System.Windows.Forms.TextBox()
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
        Me.tbCombine.SuspendLayout()
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
        Me.adgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.adgvMain.FilterAndSortEnabled = True
        Me.adgvMain.Location = New System.Drawing.Point(7, 3)
        Me.adgvMain.Name = "adgvMain"
        Me.adgvMain.ReadOnly = True
        Me.adgvMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adgvMain.RowHeadersVisible = False
        Me.adgvMain.ShowEditingIcon = False
        Me.adgvMain.ShowRowErrors = False
        Me.adgvMain.Size = New System.Drawing.Size(1319, 326)
        Me.adgvMain.TabIndex = 1
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(12, 12)
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
        Me.scMain.Size = New System.Drawing.Size(1329, 955)
        Me.scMain.SplitterDistance = 332
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
        Me.scButtom.Panel2.Controls.Add(Me.btnClearLog)
        Me.scButtom.Panel2.Controls.Add(Me.tbLog)
        Me.scButtom.Size = New System.Drawing.Size(1326, 616)
        Me.scButtom.SplitterDistance = 442
        Me.scButtom.TabIndex = 1
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tbCombine)
        Me.tcMain.Controls.Add(Me.TabPage2)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(442, 616)
        Me.tcMain.TabIndex = 0
        '
        'tbCombine
        '
        Me.tbCombine.BackColor = System.Drawing.SystemColors.Control
        Me.tbCombine.Controls.Add(Me.cbSaveMax)
        Me.tbCombine.Controls.Add(Me.cbSaveMin)
        Me.tbCombine.Controls.Add(Me.cbSaveSigma)
        Me.tbCombine.Controls.Add(Me.cbSaveMean)
        Me.tbCombine.Controls.Add(Me.cbCalcHisto)
        Me.tbCombine.Controls.Add(Me.btnRun)
        Me.tbCombine.Location = New System.Drawing.Point(4, 22)
        Me.tbCombine.Name = "tbCombine"
        Me.tbCombine.Padding = New System.Windows.Forms.Padding(3)
        Me.tbCombine.Size = New System.Drawing.Size(434, 590)
        Me.tbCombine.TabIndex = 0
        Me.tbCombine.Text = "Combine"
        '
        'cbSaveMax
        '
        Me.cbSaveMax.AutoSize = True
        Me.cbSaveMax.Checked = True
        Me.cbSaveMax.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSaveMax.Location = New System.Drawing.Point(17, 109)
        Me.cbSaveMax.Name = "cbSaveMax"
        Me.cbSaveMax.Size = New System.Drawing.Size(46, 17)
        Me.cbSaveMax.TabIndex = 5
        Me.cbSaveMax.Text = "Max"
        Me.cbSaveMax.UseVisualStyleBackColor = True
        '
        'cbSaveMin
        '
        Me.cbSaveMin.AutoSize = True
        Me.cbSaveMin.Checked = True
        Me.cbSaveMin.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSaveMin.Location = New System.Drawing.Point(17, 86)
        Me.cbSaveMin.Name = "cbSaveMin"
        Me.cbSaveMin.Size = New System.Drawing.Size(43, 17)
        Me.cbSaveMin.TabIndex = 4
        Me.cbSaveMin.Text = "Min"
        Me.cbSaveMin.UseVisualStyleBackColor = True
        '
        'cbSaveSigma
        '
        Me.cbSaveSigma.AutoSize = True
        Me.cbSaveSigma.Checked = True
        Me.cbSaveSigma.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSaveSigma.Location = New System.Drawing.Point(17, 63)
        Me.cbSaveSigma.Name = "cbSaveSigma"
        Me.cbSaveSigma.Size = New System.Drawing.Size(55, 17)
        Me.cbSaveSigma.TabIndex = 3
        Me.cbSaveSigma.Text = "Sigma"
        Me.cbSaveSigma.UseVisualStyleBackColor = True
        '
        'cbSaveMean
        '
        Me.cbSaveMean.AutoSize = True
        Me.cbSaveMean.Checked = True
        Me.cbSaveMean.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSaveMean.Location = New System.Drawing.Point(17, 40)
        Me.cbSaveMean.Name = "cbSaveMean"
        Me.cbSaveMean.Size = New System.Drawing.Size(53, 17)
        Me.cbSaveMean.TabIndex = 2
        Me.cbSaveMean.Text = "Mean"
        Me.cbSaveMean.UseVisualStyleBackColor = True
        '
        'cbCalcHisto
        '
        Me.cbCalcHisto.AutoSize = True
        Me.cbCalcHisto.Checked = True
        Me.cbCalcHisto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbCalcHisto.Location = New System.Drawing.Point(17, 17)
        Me.cbCalcHisto.Name = "cbCalcHisto"
        Me.cbCalcHisto.Size = New System.Drawing.Size(118, 17)
        Me.cbCalcHisto.TabIndex = 1
        Me.cbCalcHisto.Text = "Calculate histogram"
        Me.cbCalcHisto.UseVisualStyleBackColor = True
        '
        'btnRun
        '
        Me.btnRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRun.Location = New System.Drawing.Point(353, 561)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 0
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(434, 590)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnClearLog
        '
        Me.btnClearLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearLog.Location = New System.Drawing.Point(802, 583)
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
        Me.tbLog.Size = New System.Drawing.Size(874, 557)
        Me.tbLog.TabIndex = 0
        Me.tbLog.WordWrap = False
        '
        'frmMultiFileAction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1353, 979)
        Me.Controls.Add(Me.scMain)
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
        Me.tbCombine.ResumeLayout(False)
        Me.tbCombine.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents adgvMain As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents scButtom As SplitContainer
    Friend WithEvents tcMain As TabControl
    Friend WithEvents tbCombine As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents tbLog As TextBox
    Friend WithEvents btnRun As Button
    Friend WithEvents btnClearLog As Button
    Friend WithEvents cbCalcHisto As CheckBox
    Friend WithEvents cbSaveMean As CheckBox
    Friend WithEvents cbSaveSigma As CheckBox
    Friend WithEvents cbSaveMax As CheckBox
    Friend WithEvents cbSaveMin As CheckBox
End Class
