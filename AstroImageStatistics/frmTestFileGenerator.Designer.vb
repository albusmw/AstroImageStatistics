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
        btnWriteTestFile = New Button()
        tbDimX = New TextBox()
        Label1 = New Label()
        tbDimY = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        tbTestFileName = New TextBox()
        cbOpenAfterWrite = New CheckBox()
        cbTestFileType = New ComboBox()
        Label4 = New Label()
        tbStartValue = New TextBox()
        Label5 = New Label()
        tbStopValue = New TextBox()
        Label6 = New Label()
        msMain = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        tsmiFile_OpenLast = New ToolStripMenuItem()
        tsmiFile_Explorer = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        tsmiFile_Exit = New ToolStripMenuItem()
        tsmiGenerate = New ToolStripMenuItem()
        tsmiGenerate_FITSTestFiles = New ToolStripMenuItem()
        tbStepValue = New TextBox()
        Label7 = New Label()
        tlpSpecialFiles = New TableLayoutPanel()
        cbFile_FITS_BitPix32_Sweep = New CheckBox()
        cbFile_FITS_BitPix32f = New CheckBox()
        cbFile_FITS_BitPix64f = New CheckBox()
        cbFile_FITS_UInt16_Cross_mono = New CheckBox()
        cbFile_FITS_UInt16_Cross_rgb = New CheckBox()
        cbFile_FITS_UInt16_RowColOrder = New CheckBox()
        msMain.SuspendLayout()
        tlpSpecialFiles.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnWriteTestFile
        ' 
        btnWriteTestFile.Location = New Point(14, 237)
        btnWriteTestFile.Margin = New Padding(4, 3, 4, 3)
        btnWriteTestFile.Name = "btnWriteTestFile"
        btnWriteTestFile.Size = New Size(204, 23)
        btnWriteTestFile.TabIndex = 0
        btnWriteTestFile.Text = "Write single test file"
        btnWriteTestFile.UseVisualStyleBackColor = True
        ' 
        ' tbDimX
        ' 
        tbDimX.Location = New Point(146, 31)
        tbDimX.Margin = New Padding(4, 3, 4, 3)
        tbDimX.Name = "tbDimX"
        tbDimX.Size = New Size(72, 23)
        tbDimX.TabIndex = 1
        tbDimX.Text = "1024"
        tbDimX.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(14, 34)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(96, 15)
        Label1.TabIndex = 2
        Label1.Text = "Test image width"
        ' 
        ' tbDimY
        ' 
        tbDimY.Location = New Point(146, 61)
        tbDimY.Margin = New Padding(4, 3, 4, 3)
        tbDimY.Name = "tbDimY"
        tbDimY.Size = New Size(72, 23)
        tbDimY.TabIndex = 3
        tbDimY.Text = "768"
        tbDimY.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(14, 64)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(100, 15)
        Label2.TabIndex = 4
        Label2.Text = "Test image height"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(14, 94)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(106, 15)
        Label3.TabIndex = 5
        Label3.Text = "Test file base name"
        ' 
        ' tbTestFileName
        ' 
        tbTestFileName.Location = New Point(146, 91)
        tbTestFileName.Margin = New Padding(4, 3, 4, 3)
        tbTestFileName.Name = "tbTestFileName"
        tbTestFileName.Size = New Size(139, 23)
        tbTestFileName.TabIndex = 6
        tbTestFileName.Text = "AsImStatTestImage"
        ' 
        ' cbOpenAfterWrite
        ' 
        cbOpenAfterWrite.AutoSize = True
        cbOpenAfterWrite.Checked = True
        cbOpenAfterWrite.CheckState = CheckState.Checked
        cbOpenAfterWrite.Location = New Point(14, 266)
        cbOpenAfterWrite.Margin = New Padding(4, 3, 4, 3)
        cbOpenAfterWrite.Name = "cbOpenAfterWrite"
        cbOpenAfterWrite.Size = New Size(111, 19)
        cbOpenAfterWrite.TabIndex = 7
        cbOpenAfterWrite.Text = "Open after write"
        cbOpenAfterWrite.UseVisualStyleBackColor = True
        ' 
        ' cbTestFileType
        ' 
        cbTestFileType.DropDownStyle = ComboBoxStyle.DropDownList
        cbTestFileType.FormattingEnabled = True
        cbTestFileType.Location = New Point(146, 121)
        cbTestFileType.Margin = New Padding(4, 3, 4, 3)
        cbTestFileType.Name = "cbTestFileType"
        cbTestFileType.Size = New Size(139, 23)
        cbTestFileType.TabIndex = 8
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(14, 154)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(62, 15)
        Label4.TabIndex = 10
        Label4.Text = "Start value"
        ' 
        ' tbStartValue
        ' 
        tbStartValue.Location = New Point(146, 150)
        tbStartValue.Margin = New Padding(4, 3, 4, 3)
        tbStartValue.Name = "tbStartValue"
        tbStartValue.Size = New Size(72, 23)
        tbStartValue.TabIndex = 11
        tbStartValue.Text = "0"
        tbStartValue.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(13, 211)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(62, 15)
        Label5.TabIndex = 12
        Label5.Text = "Stop value"
        ' 
        ' tbStopValue
        ' 
        tbStopValue.Location = New Point(146, 208)
        tbStopValue.Margin = New Padding(4, 3, 4, 3)
        tbStopValue.Name = "tbStopValue"
        tbStopValue.Size = New Size(72, 23)
        tbStopValue.TabIndex = 13
        tbStopValue.Text = "65535"
        tbStopValue.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(14, 124)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(106, 15)
        Label6.TabIndex = 15
        Label6.Text = "Single test file type"
        ' 
        ' msMain
        ' 
        msMain.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, tsmiGenerate})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Size = New Size(518, 24)
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
        tsmiGenerate.DropDownItems.AddRange(New ToolStripItem() {tsmiGenerate_FITSTestFiles})
        tsmiGenerate.Name = "tsmiGenerate"
        tsmiGenerate.Size = New Size(66, 20)
        tsmiGenerate.Text = "Generate"
        ' 
        ' tsmiGenerate_FITSTestFiles
        ' 
        tsmiGenerate_FITSTestFiles.Name = "tsmiGenerate_FITSTestFiles"
        tsmiGenerate_FITSTestFiles.Size = New Size(180, 22)
        tsmiGenerate_FITSTestFiles.Text = "FITS test files"
        ' 
        ' tbStepValue
        ' 
        tbStepValue.Location = New Point(146, 179)
        tbStepValue.Margin = New Padding(4, 3, 4, 3)
        tbStepValue.Name = "tbStepValue"
        tbStepValue.Size = New Size(72, 23)
        tbStepValue.TabIndex = 17
        tbStepValue.Text = "1"
        tbStepValue.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(14, 182)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(61, 15)
        Label7.TabIndex = 18
        Label7.Text = "Step value"
        ' 
        ' tlpSpecialFiles
        ' 
        tlpSpecialFiles.ColumnCount = 1
        tlpSpecialFiles.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpSpecialFiles.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        tlpSpecialFiles.Controls.Add(cbFile_FITS_UInt16_RowColOrder, 0, 5)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_UInt16_Cross_rgb, 0, 4)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_UInt16_Cross_mono, 0, 3)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_BitPix64f, 0, 2)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_BitPix32f, 0, 1)
        tlpSpecialFiles.Controls.Add(cbFile_FITS_BitPix32_Sweep, 0, 0)
        tlpSpecialFiles.Location = New Point(292, 44)
        tlpSpecialFiles.Name = "tlpSpecialFiles"
        tlpSpecialFiles.RowCount = 6
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tlpSpecialFiles.Size = New Size(200, 251)
        tlpSpecialFiles.TabIndex = 19
        ' 
        ' cbFile_FITS_BitPix32_Sweep
        ' 
        cbFile_FITS_BitPix32_Sweep.Anchor = AnchorStyles.Left
        cbFile_FITS_BitPix32_Sweep.AutoSize = True
        cbFile_FITS_BitPix32_Sweep.Checked = True
        cbFile_FITS_BitPix32_Sweep.CheckState = CheckState.Checked
        cbFile_FITS_BitPix32_Sweep.Location = New Point(3, 11)
        cbFile_FITS_BitPix32_Sweep.Name = "cbFile_FITS_BitPix32_Sweep"
        cbFile_FITS_BitPix32_Sweep.Size = New Size(133, 19)
        cbFile_FITS_BitPix32_Sweep.TabIndex = 0
        cbFile_FITS_BitPix32_Sweep.Text = "FITS_BitPix32_Sweep"
        cbFile_FITS_BitPix32_Sweep.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_BitPix32f
        ' 
        cbFile_FITS_BitPix32f.Anchor = AnchorStyles.Left
        cbFile_FITS_BitPix32f.AutoSize = True
        cbFile_FITS_BitPix32f.Checked = True
        cbFile_FITS_BitPix32f.CheckState = CheckState.Checked
        cbFile_FITS_BitPix32f.Location = New Point(3, 52)
        cbFile_FITS_BitPix32f.Name = "cbFile_FITS_BitPix32f"
        cbFile_FITS_BitPix32f.Size = New Size(98, 19)
        cbFile_FITS_BitPix32f.TabIndex = 1
        cbFile_FITS_BitPix32f.Text = "FITS_BitPix32f"
        cbFile_FITS_BitPix32f.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_BitPix64f
        ' 
        cbFile_FITS_BitPix64f.Anchor = AnchorStyles.Left
        cbFile_FITS_BitPix64f.AutoSize = True
        cbFile_FITS_BitPix64f.Checked = True
        cbFile_FITS_BitPix64f.CheckState = CheckState.Checked
        cbFile_FITS_BitPix64f.Location = New Point(3, 93)
        cbFile_FITS_BitPix64f.Name = "cbFile_FITS_BitPix64f"
        cbFile_FITS_BitPix64f.Size = New Size(98, 19)
        cbFile_FITS_BitPix64f.TabIndex = 2
        cbFile_FITS_BitPix64f.Text = "FITS_BitPix64f"
        cbFile_FITS_BitPix64f.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_UInt16_Cross_mono
        ' 
        cbFile_FITS_UInt16_Cross_mono.Anchor = AnchorStyles.Left
        cbFile_FITS_UInt16_Cross_mono.AutoSize = True
        cbFile_FITS_UInt16_Cross_mono.Checked = True
        cbFile_FITS_UInt16_Cross_mono.CheckState = CheckState.Checked
        cbFile_FITS_UInt16_Cross_mono.Location = New Point(3, 134)
        cbFile_FITS_UInt16_Cross_mono.Name = "cbFile_FITS_UInt16_Cross_mono"
        cbFile_FITS_UInt16_Cross_mono.Size = New Size(157, 19)
        cbFile_FITS_UInt16_Cross_mono.TabIndex = 3
        cbFile_FITS_UInt16_Cross_mono.Text = "FITS_UInt16_Cross_mono"
        cbFile_FITS_UInt16_Cross_mono.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_UInt16_Cross_rgb
        ' 
        cbFile_FITS_UInt16_Cross_rgb.Anchor = AnchorStyles.Left
        cbFile_FITS_UInt16_Cross_rgb.AutoSize = True
        cbFile_FITS_UInt16_Cross_rgb.Checked = True
        cbFile_FITS_UInt16_Cross_rgb.CheckState = CheckState.Checked
        cbFile_FITS_UInt16_Cross_rgb.Location = New Point(3, 175)
        cbFile_FITS_UInt16_Cross_rgb.Name = "cbFile_FITS_UInt16_Cross_rgb"
        cbFile_FITS_UInt16_Cross_rgb.Size = New Size(143, 19)
        cbFile_FITS_UInt16_Cross_rgb.TabIndex = 4
        cbFile_FITS_UInt16_Cross_rgb.Text = "FITS_UInt16_Cross_rgb"
        cbFile_FITS_UInt16_Cross_rgb.UseVisualStyleBackColor = True
        ' 
        ' cbFile_FITS_UInt16_RowColOrder
        ' 
        cbFile_FITS_UInt16_RowColOrder.Anchor = AnchorStyles.Left
        cbFile_FITS_UInt16_RowColOrder.AutoSize = True
        cbFile_FITS_UInt16_RowColOrder.Checked = True
        cbFile_FITS_UInt16_RowColOrder.CheckState = CheckState.Checked
        cbFile_FITS_UInt16_RowColOrder.Location = New Point(3, 218)
        cbFile_FITS_UInt16_RowColOrder.Name = "cbFile_FITS_UInt16_RowColOrder"
        cbFile_FITS_UInt16_RowColOrder.Size = New Size(162, 19)
        cbFile_FITS_UInt16_RowColOrder.TabIndex = 5
        cbFile_FITS_UInt16_RowColOrder.Text = "FITS_UInt16_RowColOrder"
        cbFile_FITS_UInt16_RowColOrder.UseVisualStyleBackColor = True
        ' 
        ' frmTestFileGenerator
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(518, 490)
        Controls.Add(tlpSpecialFiles)
        Controls.Add(Label7)
        Controls.Add(tbStepValue)
        Controls.Add(Label6)
        Controls.Add(tbStopValue)
        Controls.Add(Label5)
        Controls.Add(tbStartValue)
        Controls.Add(Label4)
        Controls.Add(cbTestFileType)
        Controls.Add(cbOpenAfterWrite)
        Controls.Add(tbTestFileName)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(tbDimY)
        Controls.Add(Label1)
        Controls.Add(tbDimX)
        Controls.Add(btnWriteTestFile)
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

    Friend WithEvents btnWriteTestFile As Button
    Friend WithEvents tbDimX As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tbDimY As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents tbTestFileName As TextBox
    Friend WithEvents cbOpenAfterWrite As CheckBox
    Friend WithEvents cbTestFileType As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents tbStartValue As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents tbStopValue As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiFile_OpenLast As ToolStripMenuItem
    Friend WithEvents tsmiFile_Explorer As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_Exit As ToolStripMenuItem
    Friend WithEvents tsmiGenerate As ToolStripMenuItem
    Friend WithEvents tsmiGenerate_FITSTestFiles As ToolStripMenuItem
    Friend WithEvents tbStepValue As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents tlpSpecialFiles As TableLayoutPanel
    Friend WithEvents cbFile_FITS_BitPix32_Sweep As CheckBox
    Friend WithEvents cbFile_FITS_UInt16_RowColOrder As CheckBox
    Friend WithEvents cbFile_FITS_UInt16_Cross_rgb As CheckBox
    Friend WithEvents cbFile_FITS_UInt16_Cross_mono As CheckBox
    Friend WithEvents cbFile_FITS_BitPix64f As CheckBox
    Friend WithEvents cbFile_FITS_BitPix32f As CheckBox
End Class
