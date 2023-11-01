<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmXCorr
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
        Me.components = New System.ComponentModel.Container()
        Me.tbRefFile = New System.Windows.Forms.TextBox()
        Me.tbTemplateFile = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnLoadRef = New System.Windows.Forms.Button()
        Me.btnLoadTemplate = New System.Windows.Forms.Button()
        Me.tbRef_X = New System.Windows.Forms.TextBox()
        Me.tbRef_Y = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnStoreAndOpen = New System.Windows.Forms.Button()
        Me.tbResults = New System.Windows.Forms.TextBox()
        Me.tShowResults = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbTpl_X = New System.Windows.Forms.TextBox()
        Me.tbTpl_Y = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbRef_Xsize = New System.Windows.Forms.TextBox()
        Me.tbRef_Ysize = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbTpl_Xsize = New System.Windows.Forms.TextBox()
        Me.tbTpl_Ysize = New System.Windows.Forms.TextBox()
        Me.btnMultiAreaXCorr = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'tbRefFile
        '
        Me.tbRefFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbRefFile.BackColor = System.Drawing.Color.Red
        Me.tbRefFile.Location = New System.Drawing.Point(134, 12)
        Me.tbRefFile.Name = "tbRefFile"
        Me.tbRefFile.Size = New System.Drawing.Size(725, 20)
        Me.tbRefFile.TabIndex = 0
        Me.tbRefFile.Text = "\\192.168.100.10\astro\2023_10_11 (NGC7380 - QHY600M)\QHY600_H_alpha_240_100_50_0" &
    "01_015_Photographic.fits"
        '
        'tbTemplateFile
        '
        Me.tbTemplateFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbTemplateFile.BackColor = System.Drawing.Color.Red
        Me.tbTemplateFile.Location = New System.Drawing.Point(134, 38)
        Me.tbTemplateFile.Name = "tbTemplateFile"
        Me.tbTemplateFile.Size = New System.Drawing.Size(725, 20)
        Me.tbTemplateFile.TabIndex = 1
        Me.tbTemplateFile.Text = "\\192.168.100.10\astro\2023_10_11 (NGC7380 - QHY600M)\QHY600_H_alpha_240_100_50_0" &
    "15_015_Photographic.fits"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Reference file"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Template file"
        '
        'btnLoadRef
        '
        Me.btnLoadRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadRef.Location = New System.Drawing.Point(865, 10)
        Me.btnLoadRef.Name = "btnLoadRef"
        Me.btnLoadRef.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadRef.TabIndex = 4
        Me.btnLoadRef.Text = "Load"
        Me.btnLoadRef.UseVisualStyleBackColor = True
        '
        'btnLoadTemplate
        '
        Me.btnLoadTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadTemplate.Location = New System.Drawing.Point(865, 36)
        Me.btnLoadTemplate.Name = "btnLoadTemplate"
        Me.btnLoadTemplate.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadTemplate.TabIndex = 5
        Me.btnLoadTemplate.Text = "Load"
        Me.btnLoadTemplate.UseVisualStyleBackColor = True
        '
        'tbRef_X
        '
        Me.tbRef_X.Location = New System.Drawing.Point(134, 64)
        Me.tbRef_X.Name = "tbRef_X"
        Me.tbRef_X.Size = New System.Drawing.Size(89, 20)
        Me.tbRef_X.TabIndex = 6
        Me.tbRef_X.Text = "500"
        '
        'tbRef_Y
        '
        Me.tbRef_Y.Location = New System.Drawing.Point(229, 64)
        Me.tbRef_Y.Name = "tbRef_Y"
        Me.tbRef_Y.Size = New System.Drawing.Size(89, 20)
        Me.tbRef_Y.TabIndex = 7
        Me.tbRef_Y.Text = "500"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Ref ROI corner"
        '
        'btnStoreAndOpen
        '
        Me.btnStoreAndOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStoreAndOpen.Location = New System.Drawing.Point(12, 551)
        Me.btnStoreAndOpen.Name = "btnStoreAndOpen"
        Me.btnStoreAndOpen.Size = New System.Drawing.Size(147, 31)
        Me.btnStoreAndOpen.TabIndex = 9
        Me.btnStoreAndOpen.Text = "Store and open XCorr"
        Me.btnStoreAndOpen.UseVisualStyleBackColor = True
        '
        'tbResults
        '
        Me.tbResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbResults.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbResults.Location = New System.Drawing.Point(15, 116)
        Me.tbResults.Multiline = True
        Me.tbResults.Name = "tbResults"
        Me.tbResults.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbResults.Size = New System.Drawing.Size(925, 429)
        Me.tbResults.TabIndex = 10
        '
        'tShowResults
        '
        Me.tShowResults.Enabled = True
        Me.tShowResults.Interval = 500
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Template ROI corner"
        '
        'tbTpl_X
        '
        Me.tbTpl_X.Location = New System.Drawing.Point(134, 90)
        Me.tbTpl_X.Name = "tbTpl_X"
        Me.tbTpl_X.Size = New System.Drawing.Size(89, 20)
        Me.tbTpl_X.TabIndex = 12
        Me.tbTpl_X.Text = "800"
        '
        'tbTpl_Y
        '
        Me.tbTpl_Y.Location = New System.Drawing.Point(229, 90)
        Me.tbTpl_Y.Name = "tbTpl_Y"
        Me.tbTpl_Y.Size = New System.Drawing.Size(89, 20)
        Me.tbTpl_Y.TabIndex = 13
        Me.tbTpl_Y.Text = "800"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(324, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Ref ROI size"
        '
        'tbRef_Xsize
        '
        Me.tbRef_Xsize.Location = New System.Drawing.Point(439, 64)
        Me.tbRef_Xsize.Name = "tbRef_Xsize"
        Me.tbRef_Xsize.Size = New System.Drawing.Size(89, 20)
        Me.tbRef_Xsize.TabIndex = 15
        Me.tbRef_Xsize.Text = "1500"
        '
        'tbRef_Ysize
        '
        Me.tbRef_Ysize.Location = New System.Drawing.Point(534, 64)
        Me.tbRef_Ysize.Name = "tbRef_Ysize"
        Me.tbRef_Ysize.Size = New System.Drawing.Size(89, 20)
        Me.tbRef_Ysize.TabIndex = 16
        Me.tbRef_Ysize.Text = "1500"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(324, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Template ROI size"
        '
        'tbTpl_Xsize
        '
        Me.tbTpl_Xsize.Location = New System.Drawing.Point(439, 90)
        Me.tbTpl_Xsize.Name = "tbTpl_Xsize"
        Me.tbTpl_Xsize.Size = New System.Drawing.Size(89, 20)
        Me.tbTpl_Xsize.TabIndex = 18
        Me.tbTpl_Xsize.Text = "200"
        '
        'tbTpl_Ysize
        '
        Me.tbTpl_Ysize.Location = New System.Drawing.Point(534, 90)
        Me.tbTpl_Ysize.Name = "tbTpl_Ysize"
        Me.tbTpl_Ysize.Size = New System.Drawing.Size(89, 20)
        Me.tbTpl_Ysize.TabIndex = 19
        Me.tbTpl_Ysize.Text = "200"
        '
        'btnMultiAreaXCorr
        '
        Me.btnMultiAreaXCorr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMultiAreaXCorr.Location = New System.Drawing.Point(793, 65)
        Me.btnMultiAreaXCorr.Name = "btnMultiAreaXCorr"
        Me.btnMultiAreaXCorr.Size = New System.Drawing.Size(147, 31)
        Me.btnMultiAreaXCorr.TabIndex = 20
        Me.btnMultiAreaXCorr.Text = "Multi-area XCorr"
        Me.btnMultiAreaXCorr.UseVisualStyleBackColor = True
        '
        'frmXCorr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(952, 594)
        Me.Controls.Add(Me.btnMultiAreaXCorr)
        Me.Controls.Add(Me.tbTpl_Ysize)
        Me.Controls.Add(Me.tbTpl_Xsize)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tbRef_Ysize)
        Me.Controls.Add(Me.tbRef_Xsize)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbTpl_Y)
        Me.Controls.Add(Me.tbTpl_X)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbResults)
        Me.Controls.Add(Me.btnStoreAndOpen)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbRef_Y)
        Me.Controls.Add(Me.tbRef_X)
        Me.Controls.Add(Me.btnLoadTemplate)
        Me.Controls.Add(Me.btnLoadRef)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbTemplateFile)
        Me.Controls.Add(Me.tbRefFile)
        Me.Name = "frmXCorr"
        Me.Text = "frmXCorr"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbRefFile As TextBox
    Friend WithEvents tbTemplateFile As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnLoadRef As Button
    Friend WithEvents btnLoadTemplate As Button
    Friend WithEvents tbRef_X As TextBox
    Friend WithEvents tbRef_Y As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnStoreAndOpen As Button
    Friend WithEvents tbResults As TextBox
    Friend WithEvents tShowResults As Timer
    Friend WithEvents Label4 As Label
    Friend WithEvents tbTpl_X As TextBox
    Friend WithEvents tbTpl_Y As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents tbRef_Xsize As TextBox
    Friend WithEvents tbRef_Ysize As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents tbTpl_Xsize As TextBox
    Friend WithEvents tbTpl_Ysize As TextBox
    Friend WithEvents btnMultiAreaXCorr As Button
End Class
