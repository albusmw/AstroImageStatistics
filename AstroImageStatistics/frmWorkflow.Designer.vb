<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWorkflow
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
        Me.btnMasterBias = New System.Windows.Forms.Button()
        Me.tbBias_Root = New System.Windows.Forms.TextBox()
        Me.tbBias_Filter = New System.Windows.Forms.TextBox()
        Me.btnBias_Add = New System.Windows.Forms.Button()
        Me.lbBiasFiles = New System.Windows.Forms.ListBox()
        Me.tbLogOutput = New System.Windows.Forms.TextBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnMasterBias
        '
        Me.btnMasterBias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMasterBias.Location = New System.Drawing.Point(6, 1203)
        Me.btnMasterBias.Name = "btnMasterBias"
        Me.btnMasterBias.Size = New System.Drawing.Size(653, 38)
        Me.btnMasterBias.TabIndex = 0
        Me.btnMasterBias.Text = "Master bias"
        Me.btnMasterBias.UseVisualStyleBackColor = True
        '
        'tbBias_Root
        '
        Me.tbBias_Root.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbBias_Root.Location = New System.Drawing.Point(73, 6)
        Me.tbBias_Root.Name = "tbBias_Root"
        Me.tbBias_Root.Size = New System.Drawing.Size(539, 20)
        Me.tbBias_Root.TabIndex = 1
        Me.tbBias_Root.Text = "\\192.168.100.10\astro\2021_03_06 (NGC2174)\Bias"
        '
        'tbBias_Filter
        '
        Me.tbBias_Filter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbBias_Filter.Location = New System.Drawing.Point(73, 32)
        Me.tbBias_Filter.Name = "tbBias_Filter"
        Me.tbBias_Filter.Size = New System.Drawing.Size(539, 20)
        Me.tbBias_Filter.TabIndex = 2
        Me.tbBias_Filter.Text = "QHY600_*.fits"
        '
        'btnBias_Add
        '
        Me.btnBias_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBias_Add.Location = New System.Drawing.Point(618, 6)
        Me.btnBias_Add.Name = "btnBias_Add"
        Me.btnBias_Add.Size = New System.Drawing.Size(41, 46)
        Me.btnBias_Add.TabIndex = 3
        Me.btnBias_Add.Text = "ADD"
        Me.btnBias_Add.UseVisualStyleBackColor = True
        '
        'lbBiasFiles
        '
        Me.lbBiasFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbBiasFiles.Font = New System.Drawing.Font("Courier New", 8.25!)
        Me.lbBiasFiles.FormattingEnabled = True
        Me.lbBiasFiles.IntegralHeight = False
        Me.lbBiasFiles.ItemHeight = 14
        Me.lbBiasFiles.Location = New System.Drawing.Point(6, 58)
        Me.lbBiasFiles.Name = "lbBiasFiles"
        Me.lbBiasFiles.ScrollAlwaysVisible = True
        Me.lbBiasFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lbBiasFiles.Size = New System.Drawing.Size(653, 1139)
        Me.lbBiasFiles.TabIndex = 4
        '
        'tbLogOutput
        '
        Me.tbLogOutput.AllowDrop = True
        Me.tbLogOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLogOutput.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLogOutput.Location = New System.Drawing.Point(3, 3)
        Me.tbLogOutput.Multiline = True
        Me.tbLogOutput.Name = "tbLogOutput"
        Me.tbLogOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLogOutput.Size = New System.Drawing.Size(1350, 1273)
        Me.tbLogOutput.TabIndex = 5
        Me.tbLogOutput.WordWrap = False
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(12, 12)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.tcMain)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.tbLogOutput)
        Me.scMain.Size = New System.Drawing.Size(2039, 1279)
        Me.scMain.SplitterDistance = 679
        Me.scMain.TabIndex = 6
        '
        'tcMain
        '
        Me.tcMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcMain.Controls.Add(Me.TabPage1)
        Me.tcMain.Controls.Add(Me.TabPage2)
        Me.tcMain.Location = New System.Drawing.Point(3, 3)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(673, 1273)
        Me.tcMain.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.tbBias_Root)
        Me.TabPage1.Controls.Add(Me.lbBiasFiles)
        Me.TabPage1.Controls.Add(Me.btnMasterBias)
        Me.TabPage1.Controls.Add(Me.btnBias_Add)
        Me.TabPage1.Controls.Add(Me.tbBias_Filter)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(665, 1247)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Bias"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "File filter"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Root folder"
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(665, 1247)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'frmWorkflow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2063, 1303)
        Me.Controls.Add(Me.scMain)
        Me.Name = "frmWorkflow"
        Me.Text = "Workflow"
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.Panel2.PerformLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.tcMain.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnMasterBias As Button
    Friend WithEvents tbBias_Root As TextBox
    Friend WithEvents tbBias_Filter As TextBox
    Friend WithEvents btnBias_Add As Button
    Friend WithEvents lbBiasFiles As ListBox
    Friend WithEvents tbLogOutput As TextBox
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents tcMain As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TabPage2 As TabPage
End Class
