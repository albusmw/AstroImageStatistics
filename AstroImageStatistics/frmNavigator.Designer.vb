<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNavigator
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tbRootFile = New System.Windows.Forms.TextBox()
        Me.tbFilterString = New System.Windows.Forms.TextBox()
        Me.tbOffsetX = New System.Windows.Forms.TextBox()
        Me.tbOffsetY = New System.Windows.Forms.TextBox()
        Me.tbTileSize = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbMain = New System.Windows.Forms.ToolStripProgressBar()
        Me.cbColorModes = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbSelected = New System.Windows.Forms.TextBox()
        Me.bntAddRange = New System.Windows.Forms.Button()
        Me.sfdMain = New System.Windows.Forms.SaveFileDialog()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_SaveMosaik = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSel_CheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSel_DeleteAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmi_CheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSel_UncheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.gbROI = New System.Windows.Forms.GroupBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.clbFiles = New System.Windows.Forms.CheckedListBox()
        Me.scLow = New System.Windows.Forms.SplitContainer()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbStatResult = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbSpecialPixel = New System.Windows.Forms.ListBox()
        Me.tbBlur = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tbTileBoarder = New System.Windows.Forms.TextBox()
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
        'tbRootFile
        '
        Me.tbRootFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbRootFile.Location = New System.Drawing.Point(465, 47)
        Me.tbRootFile.Name = "tbRootFile"
        Me.tbRootFile.Size = New System.Drawing.Size(827, 20)
        Me.tbRootFile.TabIndex = 0
        '
        'tbFilterString
        '
        Me.tbFilterString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFilterString.Location = New System.Drawing.Point(465, 73)
        Me.tbFilterString.Name = "tbFilterString"
        Me.tbFilterString.Size = New System.Drawing.Size(827, 20)
        Me.tbFilterString.TabIndex = 1
        Me.tbFilterString.Text = "QHY600*.fit*"
        '
        'tbOffsetX
        '
        Me.tbOffsetX.Location = New System.Drawing.Point(98, 19)
        Me.tbOffsetX.Name = "tbOffsetX"
        Me.tbOffsetX.Size = New System.Drawing.Size(66, 20)
        Me.tbOffsetX.TabIndex = 3
        Me.tbOffsetX.Text = "2000"
        Me.tbOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbOffsetY
        '
        Me.tbOffsetY.Location = New System.Drawing.Point(98, 45)
        Me.tbOffsetY.Name = "tbOffsetY"
        Me.tbOffsetY.Size = New System.Drawing.Size(66, 20)
        Me.tbOffsetY.TabIndex = 4
        Me.tbOffsetY.Text = "3000"
        Me.tbOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbTileSize
        '
        Me.tbTileSize.Location = New System.Drawing.Point(98, 71)
        Me.tbTileSize.Name = "tbTileSize"
        Me.tbTileSize.Size = New System.Drawing.Size(66, 20)
        Me.tbTileSize.TabIndex = 5
        Me.tbTileSize.Text = "100"
        Me.tbTileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(394, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Root file"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(394, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "File Filter"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "ROI Offset X"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "ROI Offset Y"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "ROI Tile size"
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
        Me.cbColorModes.Location = New System.Drawing.Point(207, 19)
        Me.cbColorModes.Name = "cbColorModes"
        Me.cbColorModes.Size = New System.Drawing.Size(144, 21)
        Me.cbColorModes.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(394, 102)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Selected file"
        '
        'tbSelected
        '
        Me.tbSelected.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSelected.Location = New System.Drawing.Point(465, 99)
        Me.tbSelected.Name = "tbSelected"
        Me.tbSelected.ReadOnly = True
        Me.tbSelected.Size = New System.Drawing.Size(827, 20)
        Me.tbSelected.TabIndex = 23
        '
        'bntAddRange
        '
        Me.bntAddRange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bntAddRange.Location = New System.Drawing.Point(1298, 46)
        Me.bntAddRange.Name = "bntAddRange"
        Me.bntAddRange.Size = New System.Drawing.Size(130, 46)
        Me.bntAddRange.TabIndex = 22
        Me.bntAddRange.Text = "Add range"
        Me.bntAddRange.UseVisualStyleBackColor = True
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
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile_SaveMosaik, Me.ToolStripMenuItem1, Me.tsmiFile_Exit})
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Size = New System.Drawing.Size(37, 20)
        Me.tsmiFile.Text = "File"
        '
        'tsmiFile_SaveMosaik
        '
        Me.tsmiFile_SaveMosaik.Name = "tsmiFile_SaveMosaik"
        Me.tsmiFile_SaveMosaik.Size = New System.Drawing.Size(139, 22)
        Me.tsmiFile_SaveMosaik.Text = "Save mosaik"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(136, 6)
        '
        'tsmiFile_Exit
        '
        Me.tsmiFile_Exit.Name = "tsmiFile_Exit"
        Me.tsmiFile_Exit.Size = New System.Drawing.Size(139, 22)
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
        Me.gbROI.Controls.Add(Me.tbTileBoarder)
        Me.gbROI.Controls.Add(Me.Label11)
        Me.gbROI.Controls.Add(Me.Label10)
        Me.gbROI.Controls.Add(Me.tbBlur)
        Me.gbROI.Controls.Add(Me.Label3)
        Me.gbROI.Controls.Add(Me.cbColorModes)
        Me.gbROI.Controls.Add(Me.tbOffsetY)
        Me.gbROI.Controls.Add(Me.Label5)
        Me.gbROI.Controls.Add(Me.tbTileSize)
        Me.gbROI.Controls.Add(Me.tbOffsetX)
        Me.gbROI.Controls.Add(Me.Label4)
        Me.gbROI.Location = New System.Drawing.Point(12, 27)
        Me.gbROI.Name = "gbROI"
        Me.gbROI.Size = New System.Drawing.Size(376, 98)
        Me.gbROI.TabIndex = 19
        Me.gbROI.TabStop = False
        Me.gbROI.Text = "ROI"
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(12, 131)
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
        Me.scMain.Size = New System.Drawing.Size(1428, 889)
        Me.scMain.SplitterDistance = 443
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
        Me.clbFiles.Size = New System.Drawing.Size(1343, 438)
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
        Me.scLow.Size = New System.Drawing.Size(1428, 442)
        Me.scLow.SplitterDistance = 309
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
        Me.tbStatResult.Size = New System.Drawing.Size(1343, 303)
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
        Me.lbSpecialPixel.Size = New System.Drawing.Size(1343, 123)
        Me.lbSpecialPixel.TabIndex = 11
        '
        'tbBlur
        '
        Me.tbBlur.Location = New System.Drawing.Point(285, 47)
        Me.tbBlur.Name = "tbBlur"
        Me.tbBlur.Size = New System.Drawing.Size(66, 20)
        Me.tbBlur.TabIndex = 15
        Me.tbBlur.Text = "1"
        Me.tbBlur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(204, 52)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(25, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Blur"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(204, 74)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Tile boarder"
        '
        'tbTileBoarder
        '
        Me.tbTileBoarder.Location = New System.Drawing.Point(285, 73)
        Me.tbTileBoarder.Name = "tbTileBoarder"
        Me.tbTileBoarder.Size = New System.Drawing.Size(66, 20)
        Me.tbTileBoarder.TabIndex = 18
        Me.tbTileBoarder.Text = "1"
        Me.tbTileBoarder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmNavigator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1440, 1045)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.gbROI)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.tbSelected)
        Me.Controls.Add(Me.bntAddRange)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.msMain)
        Me.Controls.Add(Me.tbRootFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbFilterString)
        Me.Controls.Add(Me.Label2)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.msMain
        Me.Name = "frmNavigator"
        Me.Text = "Navigator"
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.gbROI.ResumeLayout(False)
        Me.gbROI.PerformLayout()
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

    Friend WithEvents tbRootFile As TextBox
    Friend WithEvents tbFilterString As TextBox
    Friend WithEvents tbTileSize As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents pbMain As ToolStripProgressBar
    Friend WithEvents cbColorModes As ComboBox
    Friend WithEvents bntAddRange As Button
    Friend WithEvents sfdMain As SaveFileDialog
    Friend WithEvents Label7 As Label
    Friend WithEvents tbSelected As TextBox
    Public WithEvents tbOffsetX As TextBox
    Public WithEvents tbOffsetY As TextBox
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
    Friend WithEvents Label10 As Label
    Friend WithEvents tbBlur As TextBox
    Friend WithEvents tbTileBoarder As TextBox
    Friend WithEvents Label11 As Label
End Class
