<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestFileGenerator
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
        msMain = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        tsmiFile_OpenLast = New ToolStripMenuItem()
        tsmiFile_Explorer = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiFile_Exit = New ToolStripMenuItem()
        tsmiGenerate = New ToolStripMenuItem()
        tsmiGenerate_FITSTestFiles = New ToolStripMenuItem()
        tsmiGenerate_SingleFile = New ToolStripMenuItem()
        tlpSpecialFiles = New TableLayoutPanel()
        cbFile_FITS_UInt16_RowColOrder = New CheckBox()
        cbFile_FITS_UInt16_Cross_rgb = New CheckBox()
        cbFile_FITS_UInt16_Cross_mono = New CheckBox()
        cbFile_FITS_BitPix64f = New CheckBox()
        cbFile_FITS_BitPix32f = New CheckBox()
        cbFile_FITS_BitPix32_Sweep = New CheckBox()
        pgTestFileConfig = New PropertyGrid()
        msMain.SuspendLayout()
        tlpSpecialFiles.SuspendLayout()
        SuspendLayout()
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, tsmiGenerate})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(682, 24)
        msMain.TabIndex = 16
        msMain.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {tsmiFile_OpenLast, tsmiFile_Explorer, ToolStripMenuItem1, tsmiFile_Exit})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(37, 20)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' tsmiFile_OpenLast
        ' 
        tsmiFile_OpenLast.Name = "tsmiFile_OpenLast"
        tsmiFile_OpenLast.Size = New Size(199, 22)
        tsmiFile_OpenLast.Text = "Open last generated file"
        ' 
        ' tsmiFile_Explorer
        ' 
        tsmiFile_Explorer.Name = "tsmiFile_Explorer"
        tsmiFile_Explorer.Size = New Size(199, 22)
        tsmiFile_Explorer.Text = "Open explorer"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(196, 6)
        ' 
        ' tsmiFile_Exit
        ' 
        tsmiFile_Exit.Name = "tsmiFile_Exit"
        tsmiFile_Exit.Size = New Size(199, 22)
        tsmiFile_Exit.Text = "Exit"
        ' 
        ' tsmiGenerate
        ' 
        tsmiGenerate.DropDownItems.AddRange(New ToolStripItem() {tsmiGenerate_FITSTestFiles, tsmiGenerate_SingleFile})
        tsmiGenerate.Name = "tsmiGenerate"
        tsmiGenerate.Size = New Size(66, 20)
        tsmiGenerate.Text = "Generate"
        ' 
        ' tsmiGenerate_FITSTestFiles
        ' 
        tsmiGenerate_FITSTestFiles.Name = "tsmiGenerate_FITSTestFiles"
        tsmiGenerate_FITSTestFiles.Size = New Size(147, 22)
        tsmiGenerate_FITSTestFiles.Text = "FITS test files"
        ' 
        ' tsmiGenerate_SingleFile
        ' 
        tsmiGenerate_SingleFile.Name = "tsmiGenerate_SingleFile"
        tsmiGenerate_SingleFile.Size = New Size(147, 22)
        tsmiGenerate_SingleFile.Text = "Single test file"
        ' 
        ' tlpSpecialFiles
        ' 
        tlpSpecialFiles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        tlpSpecialFiles.ColumnCount = 1
        tlpSpecialFiles.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpSpecialFiles.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        tlpSpecialFiles.Controls.Add(cbFile_FITS_UInt16_RowColOrder, 0, 5)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_UInt16_Cross_rgb, 0, 4)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_UInt16_Cross_mono, 0, 3)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_BitPix64f, 0, 2)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_BitPix32f, 0, 1)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_BitPix32_Sweep, 0, 0)
        tlpSpecialFiles.Location = New Point(482, 27)
        tlpSpecialFiles.Name = "tlpSpecialFiles"
        tlpSpecialFiles.RowCount = 6
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.Size = New Size(200, 628)
        tlpSpecialFiles.TabIndex = 19
        ' 
        ' cbFile_FITS_UInt16_RowColOrder
        ' 
        cbFile_FITS_UInt16_RowColOrder.Anchor = AnchorStyles.Left
        cbFile_FITS_UInt16_RowColOrder.AutoSize = True
        cbFile_FITS_UInt16_RowColOrder.Checked = True
        cbFile_FITS_UInt16_RowColOrder.CheckState = CheckState.Checked
        cbFile_FITS_UInt16_RowColOrder.Location = New Point(3, 564)
        cbFile_FITS_UInt16_RowColOrder.Name = "cbFile_FITS_UInt16_RowColOrder"
        cbFile_FITS_UInt16_RowColOrder.Size = New Size(162, 19)
        cbFile_FITS_UInt16_RowColOrder.TabIndex = 5
        cbFile_FITS_UInt16_RowColOrder.Text = "FITS_UInt16_RowColOrder"
        cbFile_FITS_UInt16_RowColOrder.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_UInt16_Cross_rgb
        ' 
        cbFile_FITS_UInt16_Cross_rgb.Anchor = AnchorStyles.Left
        cbFile_FITS_UInt16_Cross_rgb.AutoSize = True
        cbFile_FITS_UInt16_Cross_rgb.Checked = True
        cbFile_FITS_UInt16_Cross_rgb.CheckState = CheckState.Checked
        cbFile_FITS_UInt16_Cross_rgb.Location = New Point(3, 458)
        cbFile_FITS_UInt16_Cross_rgb.Name = "cbFile_FITS_UInt16_Cross_rgb"
        cbFile_FITS_UInt16_Cross_rgb.Size = New Size(143, 19)
        cbFile_FITS_UInt16_Cross_rgb.TabIndex = 4
        cbFile_FITS_UInt16_Cross_rgb.Text = "FITS_UInt16_Cross_rgb"
        cbFile_FITS_UInt16_Cross_rgb.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_UInt16_Cross_mono
        ' 
        cbFile_FITS_UInt16_Cross_mono.Anchor = AnchorStyles.Left
        cbFile_FITS_UInt16_Cross_mono.AutoSize = True
        cbFile_FITS_UInt16_Cross_mono.Checked = True
        cbFile_FITS_UInt16_Cross_mono.CheckState = CheckState.Checked
        cbFile_FITS_UInt16_Cross_mono.Location = New Point(3, 354)
        cbFile_FITS_UInt16_Cross_mono.Name = "cbFile_FITS_UInt16_Cross_mono"
        cbFile_FITS_UInt16_Cross_mono.Size = New Size(157, 19)
        cbFile_FITS_UInt16_Cross_mono.TabIndex = 3
        cbFile_FITS_UInt16_Cross_mono.Text = "FITS_UInt16_Cross_mono"
        cbFile_FITS_UInt16_Cross_mono.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_BitPix64f
        ' 
        cbFile_FITS_BitPix64f.Anchor = AnchorStyles.Left
        cbFile_FITS_BitPix64f.AutoSize = True
        cbFile_FITS_BitPix64f.Checked = True
        cbFile_FITS_BitPix64f.CheckState = CheckState.Checked
        cbFile_FITS_BitPix64f.Location = New Point(3, 250)
        cbFile_FITS_BitPix64f.Name = "cbFile_FITS_BitPix64f"
        cbFile_FITS_BitPix64f.Size = New Size(98, 19)
        cbFile_FITS_BitPix64f.TabIndex = 2
        cbFile_FITS_BitPix64f.Text = "FITS_BitPix64f"
        cbFile_FITS_BitPix64f.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_BitPix32f
        ' 
        cbFile_FITS_BitPix32f.Anchor = AnchorStyles.Left
        cbFile_FITS_BitPix32f.AutoSize = True
        cbFile_FITS_BitPix32f.Checked = True
        cbFile_FITS_BitPix32f.CheckState = CheckState.Checked
        cbFile_FITS_BitPix32f.Location = New Point(3, 146)
        cbFile_FITS_BitPix32f.Name = "cbFile_FITS_BitPix32f"
        cbFile_FITS_BitPix32f.Size = New Size(98, 19)
        cbFile_FITS_BitPix32f.TabIndex = 1
        cbFile_FITS_BitPix32f.Text = "FITS_BitPix32f"
        cbFile_FITS_BitPix32f.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_BitPix32_Sweep
        ' 
        cbFile_FITS_BitPix32_Sweep.Anchor = AnchorStyles.Left
        cbFile_FITS_BitPix32_Sweep.AutoSize = True
        cbFile_FITS_BitPix32_Sweep.Checked = True
        cbFile_FITS_BitPix32_Sweep.CheckState = CheckState.Checked
        cbFile_FITS_BitPix32_Sweep.Location = New Point(3, 42)
        cbFile_FITS_BitPix32_Sweep.Name = "cbFile_FITS_BitPix32_Sweep"
        cbFile_FITS_BitPix32_Sweep.Size = New Size(133, 19)
        cbFile_FITS_BitPix32_Sweep.TabIndex = 0
        cbFile_FITS_BitPix32_Sweep.Text = "FITS_BitPix32_Sweep"
        cbFile_FITS_BitPix32_Sweep.UseVisualStyleBackColor = True
        ' 
        ' pgTestFileConfig
        ' 
        pgTestFileConfig.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pgTestFileConfig.Location = New Point(14, 27)
        pgTestFileConfig.Name = "pgTestFileConfig"
        pgTestFileConfig.Size = New Size(462, 628)
        pgTestFileConfig.TabIndex = 20
        ' 
        ' frmTestFileGenerator
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(682, 667)
        Controls.Add(pgTestFileConfig)
        Controls.Add(tlpSpecialFiles)
        Controls.Add(msMain)
        MainMenuStrip = msMain
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmTestFileGenerator"
        Text = "Test file generator"
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        tlpSpecialFiles.ResumeLayout(False)
        tlpSpecialFiles.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenLast As ToolStripMenuItem
    Friend WithEvents tsmiFile_Explorer As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents tsmiGenerate As ToolStripMenuItem
    Friend WithEvents tsmiGenerate_FITSTestFiles As ToolStripMenuItem
    Friend WithEvents tlpSpecialFiles As TableLayoutPanel
    Friend WithEvents cbFile_FITS_BitPix32_Sweep As CheckBox
    Friend WithEvents cbFile_FITS_UInt16_RowColOrder As CheckBox
    Friend WithEvents cbFile_FITS_UInt16_Cross_rgb As CheckBox
    Friend WithEvents cbFile_FITS_UInt16_Cross_mono As CheckBox
    Friend WithEvents cbFile_FITS_BitPix64f As CheckBox
    Friend WithEvents cbFile_FITS_BitPix32f As CheckBox
    Friend WithEvents pgTestFileConfig As PropertyGrid
    Friend WithEvents tsmiGenerate_SingleFile As ToolStripMenuItem
End Class
