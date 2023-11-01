<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaveFile
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbFormat = New System.Windows.Forms.ComboBox()
        Me.tbFileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.sfdMain = New System.Windows.Forms.SaveFileDialog()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbSaveFileName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbImageQuality_JPEG = New System.Windows.Forms.TextBox()
        Me.tbImageQuality_PNG = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Format"
        '
        'cbFormat
        '
        Me.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFormat.FormattingEnabled = True
        Me.cbFormat.Location = New System.Drawing.Point(123, 39)
        Me.cbFormat.Name = "cbFormat"
        Me.cbFormat.Size = New System.Drawing.Size(208, 21)
        Me.cbFormat.TabIndex = 1
        '
        'tbFileName
        '
        Me.tbFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFileName.Location = New System.Drawing.Point(123, 12)
        Me.tbFileName.Name = "tbFileName"
        Me.tbFileName.Size = New System.Drawing.Size(525, 20)
        Me.tbFileName.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 218)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Saved as:"
        '
        'btnBrowse
        '
        Me.btnBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowse.Location = New System.Drawing.Point(654, 10)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(36, 23)
        Me.btnBrowse.TabIndex = 4
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(615, 184)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(615, 213)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "File name"
        '
        'tbSaveFileName
        '
        Me.tbSaveFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSaveFileName.Location = New System.Drawing.Point(73, 215)
        Me.tbSaveFileName.Name = "tbSaveFileName"
        Me.tbSaveFileName.ReadOnly = True
        Me.tbSaveFileName.Size = New System.Drawing.Size(536, 20)
        Me.tbSaveFileName.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Image quality - JPEG"
        '
        'tbImageQuality_JPEG
        '
        Me.tbImageQuality_JPEG.Location = New System.Drawing.Point(123, 66)
        Me.tbImageQuality_JPEG.Name = "tbImageQuality_JPEG"
        Me.tbImageQuality_JPEG.Size = New System.Drawing.Size(47, 20)
        Me.tbImageQuality_JPEG.TabIndex = 10
        Me.tbImageQuality_JPEG.Text = "90"
        '
        'tbImageQuality_PNG
        '
        Me.tbImageQuality_PNG.Location = New System.Drawing.Point(123, 92)
        Me.tbImageQuality_PNG.Name = "tbImageQuality_PNG"
        Me.tbImageQuality_PNG.Size = New System.Drawing.Size(47, 20)
        Me.tbImageQuality_PNG.TabIndex = 11
        Me.tbImageQuality_PNG.Text = "90"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Image quality - PNG"
        '
        'frmSaveFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 248)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbImageQuality_PNG)
        Me.Controls.Add(Me.tbImageQuality_JPEG)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbSaveFileName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbFileName)
        Me.Controls.Add(Me.cbFormat)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmSaveFile"
        Me.Text = "Save file"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cbFormat As ComboBox
    Friend WithEvents tbFileName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnBrowse As Button
    Friend WithEvents sfdMain As SaveFileDialog
    Friend WithEvents btnSave As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents tbSaveFileName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents tbImageQuality_JPEG As TextBox
    Friend WithEvents tbImageQuality_PNG As TextBox
    Friend WithEvents Label5 As Label
End Class
