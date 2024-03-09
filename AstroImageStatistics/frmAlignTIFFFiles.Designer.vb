<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlignTIFFFiles
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
        tbFile1 = New TextBox()
        tbFile2 = New TextBox()
        tbFile3 = New TextBox()
        btnLoad = New Button()
        tbFile1_DeltaX = New TextBox()
        tbFile1_DeltaY = New TextBox()
        tbFile2_DeltaX = New TextBox()
        tbFile2_DeltaY = New TextBox()
        tbFile3_DeltaX = New TextBox()
        tbFile3_DeltaY = New TextBox()
        tbBase_X = New TextBox()
        tbBase_Y = New TextBox()
        pbImage = New PictureBoxEx()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        tbStepSize_Offset = New TextBox()
        Label7 = New Label()
        tbStepSize_Deltas = New TextBox()
        Label8 = New Label()
        tbROI_width = New TextBox()
        Label9 = New Label()
        tbROI_heigth = New TextBox()
        Label10 = New Label()
        Label11 = New Label()
        btnSave = New Button()
        CType(pbImage, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' tbFile1
        ' 
        tbFile1.Location = New Point(52, 43)
        tbFile1.Name = "tbFile1"
        tbFile1.Size = New Size(413, 23)
        tbFile1.TabIndex = 0
        tbFile1.Text = "\\192.168.100.10\dsc\2024_03_03 - NGC3324 Ha\Autosave.tif"
        ' 
        ' tbFile2
        ' 
        tbFile2.Location = New Point(52, 72)
        tbFile2.Name = "tbFile2"
        tbFile2.Size = New Size(413, 23)
        tbFile2.TabIndex = 1
        tbFile2.Text = "\\192.168.100.10\dsc\2024_03_03 - NGC3324 O-III\Autosave.tif"
        ' 
        ' tbFile3
        ' 
        tbFile3.Location = New Point(52, 101)
        tbFile3.Name = "tbFile3"
        tbFile3.Size = New Size(413, 23)
        tbFile3.TabIndex = 2
        tbFile3.Text = "\\192.168.100.10\dsc\2024_03_06 - NGC3324 S-II\Autosave.tif"
        ' 
        ' btnLoad
        ' 
        btnLoad.Location = New Point(12, 12)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(453, 23)
        btnLoad.TabIndex = 3
        btnLoad.Text = "Load Files"
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' tbFile1_DeltaX
        ' 
        tbFile1_DeltaX.Location = New Point(471, 44)
        tbFile1_DeltaX.Name = "tbFile1_DeltaX"
        tbFile1_DeltaX.Size = New Size(64, 23)
        tbFile1_DeltaX.TabIndex = 5
        tbFile1_DeltaX.Text = "-27"
        tbFile1_DeltaX.TextAlign = HorizontalAlignment.Center
        ' 
        ' tbFile1_DeltaY
        ' 
        tbFile1_DeltaY.Location = New Point(541, 44)
        tbFile1_DeltaY.Name = "tbFile1_DeltaY"
        tbFile1_DeltaY.Size = New Size(64, 23)
        tbFile1_DeltaY.TabIndex = 6
        tbFile1_DeltaY.Text = "-9"
        tbFile1_DeltaY.TextAlign = HorizontalAlignment.Center
        ' 
        ' tbFile2_DeltaX
        ' 
        tbFile2_DeltaX.Location = New Point(471, 73)
        tbFile2_DeltaX.Name = "tbFile2_DeltaX"
        tbFile2_DeltaX.Size = New Size(64, 23)
        tbFile2_DeltaX.TabIndex = 7
        tbFile2_DeltaX.Text = "5"
        tbFile2_DeltaX.TextAlign = HorizontalAlignment.Center
        ' 
        ' tbFile2_DeltaY
        ' 
        tbFile2_DeltaY.Location = New Point(541, 73)
        tbFile2_DeltaY.Name = "tbFile2_DeltaY"
        tbFile2_DeltaY.Size = New Size(64, 23)
        tbFile2_DeltaY.TabIndex = 8
        tbFile2_DeltaY.Text = "-3"
        tbFile2_DeltaY.TextAlign = HorizontalAlignment.Center
        ' 
        ' tbFile3_DeltaX
        ' 
        tbFile3_DeltaX.Location = New Point(471, 102)
        tbFile3_DeltaX.Name = "tbFile3_DeltaX"
        tbFile3_DeltaX.Size = New Size(64, 23)
        tbFile3_DeltaX.TabIndex = 9
        tbFile3_DeltaX.Text = "0"
        tbFile3_DeltaX.TextAlign = HorizontalAlignment.Center
        ' 
        ' tbFile3_DeltaY
        ' 
        tbFile3_DeltaY.Location = New Point(541, 102)
        tbFile3_DeltaY.Name = "tbFile3_DeltaY"
        tbFile3_DeltaY.Size = New Size(64, 23)
        tbFile3_DeltaY.TabIndex = 10
        tbFile3_DeltaY.Text = "0"
        tbFile3_DeltaY.TextAlign = HorizontalAlignment.Center
        ' 
        ' tbBase_X
        ' 
        tbBase_X.Location = New Point(98, 130)
        tbBase_X.Name = "tbBase_X"
        tbBase_X.Size = New Size(64, 23)
        tbBase_X.TabIndex = 11
        tbBase_X.Text = "100"
        tbBase_X.TextAlign = HorizontalAlignment.Center
        ' 
        ' tbBase_Y
        ' 
        tbBase_Y.Location = New Point(98, 159)
        tbBase_Y.Name = "tbBase_Y"
        tbBase_Y.Size = New Size(64, 23)
        tbBase_Y.TabIndex = 12
        tbBase_Y.Text = "100"
        tbBase_Y.TextAlign = HorizontalAlignment.Center
        ' 
        ' pbImage
        ' 
        pbImage.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pbImage.BackColor = Color.Lime
        pbImage.InterpolationMode = Drawing2D.InterpolationMode.Default
        pbImage.Location = New Point(12, 189)
        pbImage.Name = "pbImage"
        pbImage.Size = New Size(1059, 836)
        pbImage.SizeMode = PictureBoxSizeMode.Zoom
        pbImage.TabIndex = 13
        pbImage.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 46)
        Label1.Name = "Label1"
        Label1.Size = New Size(34, 15)
        Label1.TabIndex = 14
        Label1.Text = "File 1"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 75)
        Label2.Name = "Label2"
        Label2.Size = New Size(34, 15)
        Label2.TabIndex = 15
        Label2.Text = "File 2"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 104)
        Label3.Name = "Label3"
        Label3.Size = New Size(34, 15)
        Label3.TabIndex = 16
        Label3.Text = "File 3"
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(12, 133)
        Label4.Name = "Label4"
        Label4.Size = New Size(80, 20)
        Label4.TabIndex = 17
        Label4.Text = "Base X Offset"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(12, 159)
        Label5.Name = "Label5"
        Label5.Size = New Size(80, 20)
        Label5.TabIndex = 18
        Label5.Text = "Base Y Offset"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(343, 134)
        Label6.Name = "Label6"
        Label6.Size = New Size(122, 15)
        Label6.TabIndex = 19
        Label6.Text = "Stepping - base offset"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tbStepSize_Offset
        ' 
        tbStepSize_Offset.Location = New Point(471, 131)
        tbStepSize_Offset.Name = "tbStepSize_Offset"
        tbStepSize_Offset.Size = New Size(64, 23)
        tbStepSize_Offset.TabIndex = 20
        tbStepSize_Offset.Text = "20"
        tbStepSize_Offset.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(343, 163)
        Label7.Name = "Label7"
        Label7.Size = New Size(96, 15)
        Label7.TabIndex = 21
        Label7.Text = "Stepping - deltas"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tbStepSize_Deltas
        ' 
        tbStepSize_Deltas.Location = New Point(471, 160)
        tbStepSize_Deltas.Name = "tbStepSize_Deltas"
        tbStepSize_Deltas.Size = New Size(64, 23)
        tbStepSize_Deltas.TabIndex = 22
        tbStepSize_Deltas.Text = "1"
        tbStepSize_Deltas.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label8
        ' 
        Label8.Location = New Point(168, 133)
        Label8.Name = "Label8"
        Label8.Size = New Size(80, 20)
        Label8.TabIndex = 23
        Label8.Text = "ROI width"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tbROI_width
        ' 
        tbROI_width.Location = New Point(254, 130)
        tbROI_width.Name = "tbROI_width"
        tbROI_width.Size = New Size(64, 23)
        tbROI_width.TabIndex = 24
        tbROI_width.Text = "100"
        tbROI_width.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label9
        ' 
        Label9.Location = New Point(168, 159)
        Label9.Name = "Label9"
        Label9.Size = New Size(80, 20)
        Label9.TabIndex = 25
        Label9.Text = "ROI height"
        Label9.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tbROI_heigth
        ' 
        tbROI_heigth.Location = New Point(254, 159)
        tbROI_heigth.Name = "tbROI_heigth"
        tbROI_heigth.Size = New Size(64, 23)
        tbROI_heigth.TabIndex = 26
        tbROI_heigth.Text = "100"
        tbROI_heigth.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label10
        ' 
        Label10.Location = New Point(471, 13)
        Label10.Name = "Label10"
        Label10.Size = New Size(64, 20)
        Label10.TabIndex = 27
        Label10.Text = "Delta X"
        Label10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label11
        ' 
        Label11.Location = New Point(541, 13)
        Label11.Name = "Label11"
        Label11.Size = New Size(64, 20)
        Label11.TabIndex = 28
        Label11.Text = "Delta Y"
        Label11.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnSave
        ' 
        btnSave.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnSave.Location = New Point(12, 1031)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(1059, 23)
        btnSave.TabIndex = 29
        btnSave.Text = "Save aligned files"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' frmAlignTIFFFiles
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1083, 1066)
        Controls.Add(btnSave)
        Controls.Add(Label11)
        Controls.Add(Label10)
        Controls.Add(tbROI_heigth)
        Controls.Add(Label9)
        Controls.Add(tbROI_width)
        Controls.Add(Label8)
        Controls.Add(tbStepSize_Deltas)
        Controls.Add(Label7)
        Controls.Add(tbStepSize_Offset)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(pbImage)
        Controls.Add(tbBase_Y)
        Controls.Add(tbBase_X)
        Controls.Add(tbFile3_DeltaY)
        Controls.Add(tbFile3_DeltaX)
        Controls.Add(tbFile2_DeltaY)
        Controls.Add(tbFile2_DeltaX)
        Controls.Add(tbFile1_DeltaY)
        Controls.Add(tbFile1_DeltaX)
        Controls.Add(btnLoad)
        Controls.Add(tbFile3)
        Controls.Add(tbFile2)
        Controls.Add(tbFile1)
        Name = "frmAlignTIFFFiles"
        Text = "frmAlignTIFFFiles"
        CType(pbImage, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tbFile1 As TextBox
    Friend WithEvents tbFile2 As TextBox
    Friend WithEvents tbFile3 As TextBox
    Friend WithEvents btnLoad As Button
    Friend WithEvents tbFile1_DeltaX As TextBox
    Friend WithEvents tbFile1_DeltaY As TextBox
    Friend WithEvents tbFile2_DeltaX As TextBox
    Friend WithEvents tbFile2_DeltaY As TextBox
    Friend WithEvents tbFile3_DeltaX As TextBox
    Friend WithEvents tbFile3_DeltaY As TextBox
    Friend WithEvents tbBase_X As TextBox
    Friend WithEvents tbBase_Y As TextBox
    Friend WithEvents pbImage As PictureBoxEx
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents tbStepSize_Offset As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents tbStepSize_Deltas As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents tbROI_width As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents tbROI_heigth As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents btnSave As Button
End Class
