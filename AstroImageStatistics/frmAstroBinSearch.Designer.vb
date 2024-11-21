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
        btnSearch = New Button()
        tbLog = New TextBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        tbFilter_TitleContains = New TextBox()
        cbFilter_User = New CheckBox()
        tbFilter_User = New TextBox()
        cbFilter_TitleContains = New CheckBox()
        Label1 = New Label()
        tbLimit = New TextBox()
        tbFilter_DescriptionContains = New TextBox()
        cbFilter_DescriptionContains = New CheckBox()
        tbURL = New TextBox()
        btnQueryToClipboard = New Button()
        btnOpenFolder = New Button()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnSearch
        ' 
        btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSearch.Location = New Point(957, 9)
        btnSearch.Margin = New Padding(2)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(131, 57)
        btnSearch.TabIndex = 0
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' tbLog
        ' 
        tbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tbLog.Location = New Point(14, 167)
        tbLog.Margin = New Padding(4, 3, 4, 3)
        tbLog.Multiline = True
        tbLog.Name = "tbLog"
        tbLog.Size = New Size(1068, 738)
        tbLog.TabIndex = 1
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Controls.Add(tbFilter_TitleContains, 1, 1)
        TableLayoutPanel1.Controls.Add(cbFilter_User, 0, 0)
        TableLayoutPanel1.Controls.Add(tbFilter_User, 1, 0)
        TableLayoutPanel1.Controls.Add(cbFilter_TitleContains, 0, 1)
        TableLayoutPanel1.Controls.Add(Label1, 0, 3)
        TableLayoutPanel1.Controls.Add(tbLimit, 1, 3)
        TableLayoutPanel1.Controls.Add(tbFilter_DescriptionContains, 1, 2)
        TableLayoutPanel1.Controls.Add(cbFilter_DescriptionContains, 0, 2)
        TableLayoutPanel1.Location = New Point(14, 9)
        TableLayoutPanel1.Margin = New Padding(4, 3, 4, 3)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 4
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25.00062F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25.00063F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25.00062F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 24.99813F))
        TableLayoutPanel1.Size = New Size(580, 123)
        TableLayoutPanel1.TabIndex = 2
        ' 
        ' tbFilter_TitleContains
        ' 
        tbFilter_TitleContains.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbFilter_TitleContains.Location = New Point(294, 33)
        tbFilter_TitleContains.Margin = New Padding(4, 3, 4, 3)
        tbFilter_TitleContains.Name = "tbFilter_TitleContains"
        tbFilter_TitleContains.Size = New Size(282, 23)
        tbFilter_TitleContains.TabIndex = 4
        tbFilter_TitleContains.Text = "Helix"
        ' 
        ' cbFilter_User
        ' 
        cbFilter_User.Anchor = AnchorStyles.Left
        cbFilter_User.AutoSize = True
        cbFilter_User.Location = New Point(4, 5)
        cbFilter_User.Margin = New Padding(4, 3, 4, 3)
        cbFilter_User.Name = "cbFilter_User"
        cbFilter_User.Size = New Size(49, 19)
        cbFilter_User.TabIndex = 0
        cbFilter_User.Text = "User"
        cbFilter_User.UseVisualStyleBackColor = True
        ' 
        ' tbFilter_User
        ' 
        tbFilter_User.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbFilter_User.Location = New Point(294, 3)
        tbFilter_User.Margin = New Padding(4, 3, 4, 3)
        tbFilter_User.Name = "tbFilter_User"
        tbFilter_User.Size = New Size(282, 23)
        tbFilter_User.TabIndex = 2
        tbFilter_User.Text = "equinoxx"
        ' 
        ' cbFilter_TitleContains
        ' 
        cbFilter_TitleContains.Anchor = AnchorStyles.Left
        cbFilter_TitleContains.AutoSize = True
        cbFilter_TitleContains.Location = New Point(4, 35)
        cbFilter_TitleContains.Margin = New Padding(4, 3, 4, 3)
        cbFilter_TitleContains.Name = "cbFilter_TitleContains"
        cbFilter_TitleContains.Size = New Size(98, 19)
        cbFilter_TitleContains.TabIndex = 4
        cbFilter_TitleContains.Text = "Title Contains"
        cbFilter_TitleContains.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Left
        Label1.AutoSize = True
        Label1.Location = New Point(4, 99)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(101, 15)
        Label1.TabIndex = 5
        Label1.Text = "Limit return items"
        ' 
        ' tbLimit
        ' 
        tbLimit.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbLimit.Location = New Point(294, 95)
        tbLimit.Margin = New Padding(4, 3, 4, 3)
        tbLimit.Name = "tbLimit"
        tbLimit.Size = New Size(282, 23)
        tbLimit.TabIndex = 4
        tbLimit.Text = "2"
        ' 
        ' tbFilter_DescriptionContains
        ' 
        tbFilter_DescriptionContains.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbFilter_DescriptionContains.Location = New Point(294, 63)
        tbFilter_DescriptionContains.Margin = New Padding(4, 3, 4, 3)
        tbFilter_DescriptionContains.Name = "tbFilter_DescriptionContains"
        tbFilter_DescriptionContains.Size = New Size(282, 23)
        tbFilter_DescriptionContains.TabIndex = 3
        tbFilter_DescriptionContains.Text = "CDK20"
        ' 
        ' cbFilter_DescriptionContains
        ' 
        cbFilter_DescriptionContains.Anchor = AnchorStyles.Left
        cbFilter_DescriptionContains.AutoSize = True
        cbFilter_DescriptionContains.Location = New Point(4, 65)
        cbFilter_DescriptionContains.Margin = New Padding(4, 3, 4, 3)
        cbFilter_DescriptionContains.Name = "cbFilter_DescriptionContains"
        cbFilter_DescriptionContains.Size = New Size(136, 19)
        cbFilter_DescriptionContains.TabIndex = 1
        cbFilter_DescriptionContains.Text = "Description Contains"
        cbFilter_DescriptionContains.UseVisualStyleBackColor = True
        ' 
        ' tbURL
        ' 
        tbURL.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tbURL.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(128))
        tbURL.Location = New Point(14, 133)
        tbURL.Margin = New Padding(4, 3, 4, 3)
        tbURL.Name = "tbURL"
        tbURL.ReadOnly = True
        tbURL.Size = New Size(932, 23)
        tbURL.TabIndex = 3
        ' 
        ' btnQueryToClipboard
        ' 
        btnQueryToClipboard.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnQueryToClipboard.Location = New Point(957, 133)
        btnQueryToClipboard.Margin = New Padding(2)
        btnQueryToClipboard.Name = "btnQueryToClipboard"
        btnQueryToClipboard.Size = New Size(131, 24)
        btnQueryToClipboard.TabIndex = 4
        btnQueryToClipboard.Text = "Copy to clipboard"
        btnQueryToClipboard.UseVisualStyleBackColor = True
        ' 
        ' btnOpenFolder
        ' 
        btnOpenFolder.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnOpenFolder.Location = New Point(957, 70)
        btnOpenFolder.Margin = New Padding(2)
        btnOpenFolder.Name = "btnOpenFolder"
        btnOpenFolder.Size = New Size(131, 57)
        btnOpenFolder.TabIndex = 5
        btnOpenFolder.Text = "Open preview folder"
        btnOpenFolder.UseVisualStyleBackColor = True
        ' 
        ' frmAstroBinSearch
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1097, 919)
        Controls.Add(btnOpenFolder)
        Controls.Add(btnQueryToClipboard)
        Controls.Add(tbURL)
        Controls.Add(TableLayoutPanel1)
        Controls.Add(tbLog)
        Controls.Add(btnSearch)
        Margin = New Padding(2)
        Name = "frmAstroBinSearch"
        Text = "AstroBin search"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

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
