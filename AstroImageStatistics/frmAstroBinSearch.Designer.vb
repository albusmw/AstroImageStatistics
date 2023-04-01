<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAstroBinSearch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbFilter_TitleContains = New System.Windows.Forms.TextBox()
        Me.cbFilter_User = New System.Windows.Forms.CheckBox()
        Me.tbFilter_User = New System.Windows.Forms.TextBox()
        Me.cbFilter_TitleContains = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbLimit = New System.Windows.Forms.TextBox()
        Me.tbFilter_DescriptionContains = New System.Windows.Forms.TextBox()
        Me.cbFilter_DescriptionContains = New System.Windows.Forms.CheckBox()
        Me.tbURL = New System.Windows.Forms.TextBox()
        Me.btnQueryToClipboard = New System.Windows.Forms.Button()
        Me.btnOpenFolder = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(696, 8)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(112, 49)
        Me.btnSearch.TabIndex = 0
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'tbLog
        '
        Me.tbLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLog.Location = New System.Drawing.Point(12, 186)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.Size = New System.Drawing.Size(792, 339)
        Me.tbLog.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.tbFilter_TitleContains, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cbFilter_User, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.tbFilter_User, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cbFilter_TitleContains, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tbLimit, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tbFilter_DescriptionContains, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cbFilter_DescriptionContains, 0, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 8)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(497, 107)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'tbFilter_TitleContains
        '
        Me.tbFilter_TitleContains.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFilter_TitleContains.Location = New System.Drawing.Point(251, 29)
        Me.tbFilter_TitleContains.Name = "tbFilter_TitleContains"
        Me.tbFilter_TitleContains.Size = New System.Drawing.Size(243, 20)
        Me.tbFilter_TitleContains.TabIndex = 4
        Me.tbFilter_TitleContains.Text = "Helix"
        '
        'cbFilter_User
        '
        Me.cbFilter_User.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbFilter_User.AutoSize = True
        Me.cbFilter_User.Checked = True
        Me.cbFilter_User.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbFilter_User.Location = New System.Drawing.Point(3, 4)
        Me.cbFilter_User.Name = "cbFilter_User"
        Me.cbFilter_User.Size = New System.Drawing.Size(48, 17)
        Me.cbFilter_User.TabIndex = 0
        Me.cbFilter_User.Text = "User"
        Me.cbFilter_User.UseVisualStyleBackColor = True
        '
        'tbFilter_User
        '
        Me.tbFilter_User.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFilter_User.Location = New System.Drawing.Point(251, 3)
        Me.tbFilter_User.Name = "tbFilter_User"
        Me.tbFilter_User.Size = New System.Drawing.Size(243, 20)
        Me.tbFilter_User.TabIndex = 2
        Me.tbFilter_User.Text = "equinoxx"
        '
        'cbFilter_TitleContains
        '
        Me.cbFilter_TitleContains.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbFilter_TitleContains.AutoSize = True
        Me.cbFilter_TitleContains.Location = New System.Drawing.Point(3, 30)
        Me.cbFilter_TitleContains.Name = "cbFilter_TitleContains"
        Me.cbFilter_TitleContains.Size = New System.Drawing.Size(90, 17)
        Me.cbFilter_TitleContains.TabIndex = 4
        Me.cbFilter_TitleContains.Text = "Title Contains"
        Me.cbFilter_TitleContains.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Limit return items"
        '
        'tbLimit
        '
        Me.tbLimit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLimit.Location = New System.Drawing.Point(251, 82)
        Me.tbLimit.Name = "tbLimit"
        Me.tbLimit.Size = New System.Drawing.Size(243, 20)
        Me.tbLimit.TabIndex = 4
        Me.tbLimit.Text = "10"
        '
        'tbFilter_DescriptionContains
        '
        Me.tbFilter_DescriptionContains.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFilter_DescriptionContains.Location = New System.Drawing.Point(251, 55)
        Me.tbFilter_DescriptionContains.Name = "tbFilter_DescriptionContains"
        Me.tbFilter_DescriptionContains.Size = New System.Drawing.Size(243, 20)
        Me.tbFilter_DescriptionContains.TabIndex = 3
        Me.tbFilter_DescriptionContains.Text = "CDK20"
        '
        'cbFilter_DescriptionContains
        '
        Me.cbFilter_DescriptionContains.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbFilter_DescriptionContains.AutoSize = True
        Me.cbFilter_DescriptionContains.Location = New System.Drawing.Point(3, 56)
        Me.cbFilter_DescriptionContains.Name = "cbFilter_DescriptionContains"
        Me.cbFilter_DescriptionContains.Size = New System.Drawing.Size(123, 17)
        Me.cbFilter_DescriptionContains.TabIndex = 1
        Me.cbFilter_DescriptionContains.Text = "Description Contains"
        Me.cbFilter_DescriptionContains.UseVisualStyleBackColor = True
        '
        'tbURL
        '
        Me.tbURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbURL.Location = New System.Drawing.Point(12, 160)
        Me.tbURL.Name = "tbURL"
        Me.tbURL.ReadOnly = True
        Me.tbURL.Size = New System.Drawing.Size(675, 20)
        Me.tbURL.TabIndex = 3
        '
        'btnQueryToClipboard
        '
        Me.btnQueryToClipboard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQueryToClipboard.Location = New System.Drawing.Point(692, 160)
        Me.btnQueryToClipboard.Margin = New System.Windows.Forms.Padding(2)
        Me.btnQueryToClipboard.Name = "btnQueryToClipboard"
        Me.btnQueryToClipboard.Size = New System.Drawing.Size(112, 21)
        Me.btnQueryToClipboard.TabIndex = 4
        Me.btnQueryToClipboard.Text = "Copy to clipboard"
        Me.btnQueryToClipboard.UseVisualStyleBackColor = True
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenFolder.Location = New System.Drawing.Point(696, 61)
        Me.btnOpenFolder.Margin = New System.Windows.Forms.Padding(2)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(112, 49)
        Me.btnOpenFolder.TabIndex = 5
        Me.btnOpenFolder.Text = "Open preview folder"
        Me.btnOpenFolder.UseVisualStyleBackColor = True
        '
        'frmAstroBinSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(816, 537)
        Me.Controls.Add(Me.btnOpenFolder)
        Me.Controls.Add(Me.btnQueryToClipboard)
        Me.Controls.Add(Me.tbURL)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.tbLog)
        Me.Controls.Add(Me.btnSearch)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmAstroBinSearch"
        Me.Text = "AstroBin search"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSearch As Button
    Friend WithEvents tbLog As TextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents tbFilter_DescriptionContains As TextBox
    Friend WithEvents cbFilter_User As CheckBox
    Friend WithEvents cbFilter_DescriptionContains As CheckBox
    Friend WithEvents tbFilter_User As TextBox
    Friend WithEvents tbURL As TextBox
    Friend WithEvents tbLimit As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tbFilter_TitleContains As TextBox
    Friend WithEvents cbFilter_TitleContains As CheckBox
    Friend WithEvents btnQueryToClipboard As Button
    Friend WithEvents btnOpenFolder As Button
    ' Friend WithEvents LogBox As cLogTextBox

End Class
