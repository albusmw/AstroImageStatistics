<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmNavigator
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
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbMain = New System.Windows.Forms.ToolStripProgressBar()
        Me.cbColorModes = New System.Windows.Forms.ComboBox()
        Me.sfdMain = New System.Windows.Forms.SaveFileDialog()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_AddFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_SaveMosaik = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSel_CheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSel_DeleteAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_CheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSel_UncheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.gbROI = New System.Windows.Forms.GroupBox()
        Me.pgParameter = New System.Windows.Forms.PropertyGrid()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.clbFiles = New System.Windows.Forms.CheckedListBox()
        Me.scLow = New System.Windows.Forms.SplitContainer()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbStatResult = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbSpecialPixel = New System.Windows.Forms.ListBox()
        Me.ssMain.SuspendLayout()
        Me.msMain.SuspendLayout()
        Me.gbROI.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        CType(Me.scLow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scLow.Panel1.SuspendLayout()
        Me.scLow.Panel2.SuspendLayout()
        Me.scLow.SuspendLayout()
        Me.SuspendLayout()
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslStatus, Me.pbMain})
        Me.ssMain.Location = New System.Drawing.Point(0, 1023)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1440, 22)
        Me.ssMain.TabIndex = 13
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(22, 17)
        Me.tsslStatus.Text = "---"
        Me.tsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbMain
        '
        Me.pbMain.Name = "pbMain"
        Me.pbMain.Size = New System.Drawing.Size(100, 16)
        '
        'cbColorModes
        '
        Me.cbColorModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbColorModes.FormattingEnabled = True
        Me.cbColorModes.Items.AddRange(New Object() {"Gray", "Hot", "Jet", "Bone", "False-Color", "False-Color HSL"})
        Me.cbColorModes.Location = New System.Drawing.Point(10, 19)
        Me.cbColorModes.Name = "cbColorModes"
        Me.cbColorModes.Size = New System.Drawing.Size(360, 21)
        Me.cbColorModes.TabIndex = 14
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile, Me.tsmiSel_CheckAll})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(1440, 24)
        Me.msMain.TabIndex = 18
        Me.msMain.Text = "MenuStrip1"
        '
        'tsmiFile
        '
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile_AddFiles, Me.tsmiFile_SaveMosaik, Me.ToolStripMenuItem1, Me.tsmiFile_Exit})
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Size = New System.Drawing.Size(37, 20)
        Me.tsmiFile.Text = "File"
        '
        'tsmiFile_AddFiles
        '
        Me.tsmiFile_AddFiles.Name = "tsmiFile_AddFiles"
        Me.tsmiFile_AddFiles.Size = New System.Drawing.Size(180, 22)
        Me.tsmiFile_AddFiles.Text = "Add files"
        '
        'tsmiFile_SaveMosaik
        '
        Me.tsmiFile_SaveMosaik.Name = "tsmiFile_SaveMosaik"
        Me.tsmiFile_SaveMosaik.Size = New System.Drawing.Size(180, 22)
        Me.tsmiFile_SaveMosaik.Text = "Save mosaik"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(177, 6)
        '
        'tsmiFile_Exit
        '
        Me.tsmiFile_Exit.Name = "tsmiFile_Exit"
        Me.tsmiFile_Exit.Size = New System.Drawing.Size(180, 22)
        Me.tsmiFile_Exit.Text = "Exit"
        '
        'tsmiSel_CheckAll
        '
        Me.tsmiSel_CheckAll.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiSel_DeleteAll, Me.tsmi_CheckAll, Me.tsmiSel_UncheckAll})
        Me.tsmiSel_CheckAll.Name = "tsmiSel_CheckAll"
        Me.tsmiSel_CheckAll.Size = New System.Drawing.Size(67, 20)
        Me.tsmiSel_CheckAll.Text = "Selection"
        '
        'tsmiSel_DeleteAll
        '
        Me.tsmiSel_DeleteAll.Name = "tsmiSel_DeleteAll"
        Me.tsmiSel_DeleteAll.Size = New System.Drawing.Size(146, 22)
        Me.tsmiSel_DeleteAll.Text = "Delete all files"
        '
        'tsmi_CheckAll
        '
        Me.tsmi_CheckAll.Name = "tsmi_CheckAll"
        Me.tsmi_CheckAll.Size = New System.Drawing.Size(146, 22)
        Me.tsmi_CheckAll.Text = "Check all"
        '
        'tsmiSel_UncheckAll
        '
        Me.tsmiSel_UncheckAll.Name = "tsmiSel_UncheckAll"
        Me.tsmiSel_UncheckAll.Size = New System.Drawing.Size(146, 22)
        Me.tsmiSel_UncheckAll.Text = "Uncheck all"
        '
        'gbROI
        '
        Me.gbROI.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbROI.Controls.Add(Me.pgParameter)
        Me.gbROI.Controls.Add(Me.cbColorModes)
        Me.gbROI.Location = New System.Drawing.Point(12, 27)
        Me.gbROI.Name = "gbROI"
        Me.gbROI.Size = New System.Drawing.Size(376, 990)
        Me.gbROI.TabIndex = 19
        Me.gbROI.TabStop = False
        Me.gbROI.Text = "Parameter"
        '
        'pgParameter
        '
        Me.pgParameter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgParameter.Location = New System.Drawing.Point(10, 49)
        Me.pgParameter.Name = "pgParameter"
        Me.pgParameter.Size = New System.Drawing.Size(360, 935)
        Me.pgParameter.TabIndex = 19
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(397, 27)
        Me.scMain.Name = "scMain"
        Me.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.Label6)
        Me.scMain.Panel1.Controls.Add(Me.clbFiles)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.scLow)
        Me.scMain.Size = New System.Drawing.Size(1043, 993)
        Me.scMain.SplitterDistance = 494
        Me.scMain.TabIndex = 28
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "All files:"
        '
        'clbFiles
        '
        Me.clbFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbFiles.FormattingEnabled = True
        Me.clbFiles.IntegralHeight = False
        Me.clbFiles.Location = New System.Drawing.Point(82, 3)
        Me.clbFiles.Name = "clbFiles"
        Me.clbFiles.ScrollAlwaysVisible = True
        Me.clbFiles.Size = New System.Drawing.Size(958, 489)
        Me.clbFiles.TabIndex = 27
        '
        'scLow
        '
        Me.scLow.BackColor = System.Drawing.SystemColors.Control
        Me.scLow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scLow.Location = New System.Drawing.Point(0, 0)
        Me.scLow.Name = "scLow"
        Me.scLow.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scLow.Panel1
        '
        Me.scLow.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.scLow.Panel1.Controls.Add(Me.Label8)
        Me.scLow.Panel1.Controls.Add(Me.tbStatResult)
        '
        'scLow.Panel2
        '
        Me.scLow.Panel2.Controls.Add(Me.Label9)
        Me.scLow.Panel2.Controls.Add(Me.lbSpecialPixel)
        Me.scLow.Size = New System.Drawing.Size(1043, 495)
        Me.scLow.SplitterDistance = 345
        Me.scLow.TabIndex = 28
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Statistics:"
        '
        'tbStatResult
        '
        Me.tbStatResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbStatResult.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbStatResult.Location = New System.Drawing.Point(82, 3)
        Me.tbStatResult.Multiline = True
        Me.tbStatResult.Name = "tbStatResult"
        Me.tbStatResult.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbStatResult.Size = New System.Drawing.Size(958, 339)
        Me.tbStatResult.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Special pixel:"
        '
        'lbSpecialPixel
        '
        Me.lbSpecialPixel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbSpecialPixel.Font = New System.Drawing.Font("Courier New", 8.25!)
        Me.lbSpecialPixel.FormattingEnabled = True
        Me.lbSpecialPixel.IntegralHeight = False
        Me.lbSpecialPixel.ItemHeight = 14
        Me.lbSpecialPixel.Location = New System.Drawing.Point(82, 3)
        Me.lbSpecialPixel.Name = "lbSpecialPixel"
        Me.lbSpecialPixel.ScrollAlwaysVisible = True
        Me.lbSpecialPixel.Size = New System.Drawing.Size(958, 140)
        Me.lbSpecialPixel.TabIndex = 11
        '
        'frmNavigator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1440, 1045)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.gbROI)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.msMain)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.msMain
        Me.Name = "frmNavigator"
        Me.Text = "Navigator"
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.gbROI.ResumeLayout(False)
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel1.PerformLayout()
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.scLow.Panel1.ResumeLayout(False)
        Me.scLow.Panel1.PerformLayout()
        Me.scLow.Panel2.ResumeLayout(False)
        Me.scLow.Panel2.PerformLayout()
        CType(Me.scLow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scLow.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents pbMain As ToolStripProgressBar
    Friend WithEvents cbColorModes As ComboBox
    Friend WithEvents sfdMain As SaveFileDialog
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_SaveMosaik As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents tsmiSel_CheckAll As ToolStripMenuItem
    Friend WithEvents tsmiSel_DeleteAll As ToolStripMenuItem
    Friend WithEvents gbROI As GroupBox
    Friend WithEvents tsmi_CheckAll As ToolStripMenuItem
    Friend WithEvents tsmiSel_UncheckAll As ToolStripMenuItem
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents clbFiles As CheckedListBox
    Friend WithEvents scLow As SplitContainer
    Friend WithEvents tbStatResult As TextBox
    Friend WithEvents lbSpecialPixel As ListBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents pgParameter As PropertyGrid
    Friend WithEvents tsmiFile_AddFiles As ToolStripMenuItem
End Class
