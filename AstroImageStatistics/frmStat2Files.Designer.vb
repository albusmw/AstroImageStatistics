<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStat2Files
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
        pZEDGraph = New Panel()
        gbConfig = New GroupBox()
        pbMain = New ProgressBar()
        tbFile2 = New TextBox()
        tbFile1 = New TextBox()
        btnGo = New Button()
        Label2 = New Label()
        Label1 = New Label()
        gbConfig.SuspendLayout()
        SuspendLayout()
        ' 
        ' pZEDGraph
        ' 
        pZEDGraph.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pZEDGraph.Location = New Point(12, 137)
        pZEDGraph.Name = "pZEDGraph"
        pZEDGraph.Size = New Size(1039, 678)
        pZEDGraph.TabIndex = 0
        ' 
        ' gbConfig
        ' 
        gbConfig.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        gbConfig.Controls.Add(pbMain)
        gbConfig.Controls.Add(tbFile2)
        gbConfig.Controls.Add(tbFile1)
        gbConfig.Controls.Add(btnGo)
        gbConfig.Controls.Add(Label2)
        gbConfig.Controls.Add(Label1)
        gbConfig.Location = New Point(12, 12)
        gbConfig.Name = "gbConfig"
        gbConfig.Size = New Size(1039, 119)
        gbConfig.TabIndex = 1
        gbConfig.TabStop = False
        gbConfig.Text = "Configuration"
        ' 
        ' pbMain
        ' 
        pbMain.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        pbMain.Location = New Point(52, 80)
        pbMain.Name = "pbMain"
        pbMain.Size = New Size(900, 23)
        pbMain.Style = ProgressBarStyle.Continuous
        pbMain.TabIndex = 5
        ' 
        ' tbFile2
        ' 
        tbFile2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbFile2.Location = New Point(52, 51)
        tbFile2.Name = "tbFile2"
        tbFile2.Size = New Size(900, 23)
        tbFile2.TabIndex = 4
        tbFile2.Text = "\\Ds1819\dsc\9999 - Calibration\2025_10_18 - Flat L\Flat L_S4_00009.fits"
        ' 
        ' tbFile1
        ' 
        tbFile1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbFile1.Location = New Point(52, 22)
        tbFile1.Name = "tbFile1"
        tbFile1.Size = New Size(900, 23)
        tbFile1.TabIndex = 3
        tbFile1.Text = "\\Ds1819\dsc\9999 - Calibration\2025_10_18 - Flat L\Flat L_S1_00001.fits"
        ' 
        ' btnGo
        ' 
        btnGo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        btnGo.Location = New Point(958, 14)
        btnGo.Name = "btnGo"
        btnGo.Size = New Size(75, 99)
        btnGo.TabIndex = 2
        btnGo.Text = "Start"
        btnGo.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(6, 54)
        Label2.Name = "Label2"
        Label2.Size = New Size(34, 15)
        Label2.TabIndex = 1
        Label2.Text = "File 2"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 25)
        Label1.Name = "Label1"
        Label1.Size = New Size(34, 15)
        Label1.TabIndex = 0
        Label1.Text = "File 1"
        ' 
        ' frmStat2Files
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1063, 827)
        Controls.Add(gbConfig)
        Controls.Add(pZEDGraph)
        Name = "frmStat2Files"
        Text = "Statistics of 2 files"
        gbConfig.ResumeLayout(False)
        gbConfig.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pZEDGraph As Panel
    Friend WithEvents gbConfig As GroupBox
    Friend WithEvents tbFile2 As TextBox
    Friend WithEvents tbFile1 As TextBox
    Friend WithEvents btnGo As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents pbMain As ProgressBar
End Class
