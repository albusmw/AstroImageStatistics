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
        Me.btnWriteTestFile = New System.Windows.Forms.Button()
        Me.tbDimX = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbDimY = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbTestFileName = New System.Windows.Forms.TextBox()
        Me.cbOpenAfterWrite = New System.Windows.Forms.CheckBox()
        Me.cbTestFileType = New System.Windows.Forms.ComboBox()
        Me.btnOpenExplorer = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbInt16StartValue = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbInt16StopValue = New System.Windows.Forms.TextBox()
        Me.btnWriteAllTestFiles = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnWriteTestFile
        '
        Me.btnWriteTestFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWriteTestFile.Location = New System.Drawing.Point(353, 250)
        Me.btnWriteTestFile.Name = "btnWriteTestFile"
        Me.btnWriteTestFile.Size = New System.Drawing.Size(117, 33)
        Me.btnWriteTestFile.TabIndex = 0
        Me.btnWriteTestFile.Text = "Write test file"
        Me.btnWriteTestFile.UseVisualStyleBackColor = True
        '
        'tbDimX
        '
        Me.tbDimX.Location = New System.Drawing.Point(125, 12)
        Me.tbDimX.Name = "tbDimX"
        Me.tbDimX.Size = New System.Drawing.Size(62, 20)
        Me.tbDimX.TabIndex = 1
        Me.tbDimX.Text = "1024"
        Me.tbDimX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Test image width"
        '
        'tbDimY
        '
        Me.tbDimY.Location = New System.Drawing.Point(125, 38)
        Me.tbDimY.Name = "tbDimY"
        Me.tbDimY.Size = New System.Drawing.Size(62, 20)
        Me.tbDimY.TabIndex = 3
        Me.tbDimY.Text = "768"
        Me.tbDimY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Test image height"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Test file name"
        '
        'tbTestFileName
        '
        Me.tbTestFileName.Location = New System.Drawing.Point(125, 64)
        Me.tbTestFileName.Name = "tbTestFileName"
        Me.tbTestFileName.Size = New System.Drawing.Size(222, 20)
        Me.tbTestFileName.TabIndex = 6
        Me.tbTestFileName.Text = "AsImStatTestImage"
        '
        'cbOpenAfterWrite
        '
        Me.cbOpenAfterWrite.AutoSize = True
        Me.cbOpenAfterWrite.Checked = True
        Me.cbOpenAfterWrite.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbOpenAfterWrite.Location = New System.Drawing.Point(353, 66)
        Me.cbOpenAfterWrite.Name = "cbOpenAfterWrite"
        Me.cbOpenAfterWrite.Size = New System.Drawing.Size(101, 17)
        Me.cbOpenAfterWrite.TabIndex = 7
        Me.cbOpenAfterWrite.Text = "Open after write"
        Me.cbOpenAfterWrite.UseVisualStyleBackColor = True
        '
        'cbTestFileType
        '
        Me.cbTestFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTestFileType.FormattingEnabled = True
        Me.cbTestFileType.Location = New System.Drawing.Point(125, 90)
        Me.cbTestFileType.Name = "cbTestFileType"
        Me.cbTestFileType.Size = New System.Drawing.Size(222, 21)
        Me.cbTestFileType.TabIndex = 8
        '
        'btnOpenExplorer
        '
        Me.btnOpenExplorer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenExplorer.Location = New System.Drawing.Point(230, 250)
        Me.btnOpenExplorer.Name = "btnOpenExplorer"
        Me.btnOpenExplorer.Size = New System.Drawing.Size(117, 33)
        Me.btnOpenExplorer.TabIndex = 9
        Me.btnOpenExplorer.Text = "Open explorer"
        Me.btnOpenExplorer.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Int16 start value"
        '
        'tbInt16StartValue
        '
        Me.tbInt16StartValue.Location = New System.Drawing.Point(125, 125)
        Me.tbInt16StartValue.Name = "tbInt16StartValue"
        Me.tbInt16StartValue.Size = New System.Drawing.Size(62, 20)
        Me.tbInt16StartValue.TabIndex = 11
        Me.tbInt16StartValue.Text = "0"
        Me.tbInt16StartValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Int16 stop value"
        '
        'tbInt16StopValue
        '
        Me.tbInt16StopValue.Location = New System.Drawing.Point(125, 151)
        Me.tbInt16StopValue.Name = "tbInt16StopValue"
        Me.tbInt16StopValue.Size = New System.Drawing.Size(62, 20)
        Me.tbInt16StopValue.TabIndex = 13
        Me.tbInt16StopValue.Text = "65535"
        Me.tbInt16StopValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnWriteAllTestFiles
        '
        Me.btnWriteAllTestFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnWriteAllTestFiles.Location = New System.Drawing.Point(15, 250)
        Me.btnWriteAllTestFiles.Name = "btnWriteAllTestFiles"
        Me.btnWriteAllTestFiles.Size = New System.Drawing.Size(117, 33)
        Me.btnWriteAllTestFiles.TabIndex = 14
        Me.btnWriteAllTestFiles.Text = "Write all test file"
        Me.btnWriteAllTestFiles.UseVisualStyleBackColor = True
        '
        'frmTestFileGenerator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 295)
        Me.Controls.Add(Me.btnWriteAllTestFiles)
        Me.Controls.Add(Me.tbInt16StopValue)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbInt16StartValue)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnOpenExplorer)
        Me.Controls.Add(Me.cbTestFileType)
        Me.Controls.Add(Me.cbOpenAfterWrite)
        Me.Controls.Add(Me.tbTestFileName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbDimY)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbDimX)
        Me.Controls.Add(Me.btnWriteTestFile)
        Me.Name = "frmTestFileGenerator"
        Me.Text = "Test file generator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents btnOpenExplorer As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents tbInt16StartValue As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents tbInt16StopValue As TextBox
    Friend WithEvents btnWriteAllTestFiles As Button
End Class
