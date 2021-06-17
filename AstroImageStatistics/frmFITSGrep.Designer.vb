<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFITSGrep
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbRootFolder = New System.Windows.Forms.TextBox()
        Me.tbOutput = New System.Windows.Forms.TextBox()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tspbMain = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsslProgress = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslSelectedFiles = New System.Windows.Forms.ToolStripStatusLabel()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.adgvMain = New Zuby.ADGV.AdvancedDataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbFileFilter = New System.Windows.Forms.TextBox()
        Me.tUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_ResetFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbDirFilter = New System.Windows.Forms.TextBox()
        Me.btnBrowseFolder = New System.Windows.Forms.Button()
        Me.ssMain.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        CType(Me.adgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.msMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(1209, 33)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(105, 24)
        Me.btnSearch.TabIndex = 0
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Root folder:"
        '
        'tbRootFolder
        '
        Me.tbRootFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbRootFolder.Location = New System.Drawing.Point(126, 36)
        Me.tbRootFolder.Name = "tbRootFolder"
        Me.tbRootFolder.Size = New System.Drawing.Size(735, 20)
        Me.tbRootFolder.TabIndex = 2
        Me.tbRootFolder.Text = "\\192.168.100.10\astro"
        '
        'tbOutput
        '
        Me.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbOutput.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbOutput.Location = New System.Drawing.Point(0, 0)
        Me.tbOutput.Multiline = True
        Me.tbOutput.Name = "tbOutput"
        Me.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbOutput.Size = New System.Drawing.Size(1299, 149)
        Me.tbOutput.TabIndex = 3
        Me.tbOutput.WordWrap = False
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tspbMain, Me.tsslProgress, Me.tsslMessage, Me.tsslSelectedFiles})
        Me.ssMain.Location = New System.Drawing.Point(0, 652)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1326, 22)
        Me.ssMain.TabIndex = 4
        Me.ssMain.Text = "StatusStrip1"
        '
        'tspbMain
        '
        Me.tspbMain.Name = "tspbMain"
        Me.tspbMain.Size = New System.Drawing.Size(300, 16)
        Me.tspbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'tsslProgress
        '
        Me.tsslProgress.Name = "tsslProgress"
        Me.tsslProgress.Size = New System.Drawing.Size(22, 17)
        Me.tsslProgress.Text = "---"
        '
        'tsslMessage
        '
        Me.tsslMessage.Name = "tsslMessage"
        Me.tsslMessage.Size = New System.Drawing.Size(22, 17)
        Me.tsslMessage.Text = "---"
        '
        'tsslSelectedFiles
        '
        Me.tsslSelectedFiles.Name = "tsslSelectedFiles"
        Me.tsslSelectedFiles.Size = New System.Drawing.Size(77, 17)
        Me.tsslSelectedFiles.Text = "0 files filtered"
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(15, 70)
        Me.scMain.Name = "scMain"
        Me.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.adgvMain)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.tbOutput)
        Me.scMain.Size = New System.Drawing.Size(1299, 575)
        Me.scMain.SplitterDistance = 422
        Me.scMain.TabIndex = 6
        '
        'adgvMain
        '
        Me.adgvMain.AllowUserToAddRows = False
        Me.adgvMain.AllowUserToDeleteRows = False
        Me.adgvMain.AllowUserToOrderColumns = True
        Me.adgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.adgvMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.adgvMain.FilterAndSortEnabled = True
        Me.adgvMain.Location = New System.Drawing.Point(0, 0)
        Me.adgvMain.Name = "adgvMain"
        Me.adgvMain.ReadOnly = True
        Me.adgvMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adgvMain.RowHeadersVisible = False
        Me.adgvMain.ShowEditingIcon = False
        Me.adgvMain.ShowRowErrors = False
        Me.adgvMain.Size = New System.Drawing.Size(1299, 422)
        Me.adgvMain.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(963, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "File Filter:"
        '
        'tbFileFilter
        '
        Me.tbFileFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFileFilter.Location = New System.Drawing.Point(1020, 36)
        Me.tbFileFilter.Name = "tbFileFilter"
        Me.tbFileFilter.Size = New System.Drawing.Size(182, 20)
        Me.tbFileFilter.TabIndex = 8
        Me.tbFileFilter.Text = "*"
        '
        'tUpdate
        '
        Me.tUpdate.Enabled = True
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(1326, 24)
        Me.msMain.TabIndex = 9
        Me.msMain.Text = "MenuStrip1"
        '
        'tsmiFile
        '
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile_ResetFilter})
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Size = New System.Drawing.Size(37, 20)
        Me.tsmiFile.Text = "File"
        '
        'tsmiFile_ResetFilter
        '
        Me.tsmiFile_ResetFilter.Name = "tsmiFile_ResetFilter"
        Me.tsmiFile_ResetFilter.Size = New System.Drawing.Size(134, 22)
        Me.tsmiFile_ResetFilter.Text = "Reset filters"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(867, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(12, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "\"
        '
        'tbDirFilter
        '
        Me.tbDirFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbDirFilter.Location = New System.Drawing.Point(885, 36)
        Me.tbDirFilter.Name = "tbDirFilter"
        Me.tbDirFilter.Size = New System.Drawing.Size(72, 20)
        Me.tbDirFilter.TabIndex = 11
        Me.tbDirFilter.Text = "*"
        '
        'btnBrowseFolder
        '
        Me.btnBrowseFolder.Location = New System.Drawing.Point(80, 34)
        Me.btnBrowseFolder.Name = "btnBrowseFolder"
        Me.btnBrowseFolder.Size = New System.Drawing.Size(40, 24)
        Me.btnBrowseFolder.TabIndex = 12
        Me.btnBrowseFolder.Text = "..."
        Me.btnBrowseFolder.UseVisualStyleBackColor = True
        '
        'frmFITSGrep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 674)
        Me.Controls.Add(Me.btnBrowseFolder)
        Me.Controls.Add(Me.tbDirFilter)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbFileFilter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.msMain)
        Me.Controls.Add(Me.tbRootFolder)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSearch)
        Me.MainMenuStrip = Me.msMain
        Me.Name = "frmFITSGrep"
        Me.Text = "FITS Grep"
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.Panel2.PerformLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        CType(Me.adgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSearch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents tbRootFolder As TextBox
    Friend WithEvents tbOutput As TextBox
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tspbMain As ToolStripProgressBar
    Friend WithEvents tsslMessage As ToolStripStatusLabel
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents Label2 As Label
    Friend WithEvents tbFileFilter As TextBox
    Friend WithEvents tUpdate As Timer
    Friend WithEvents tsslProgress As ToolStripStatusLabel
    Friend WithEvents adgvMain As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents tsmiFile_ResetFilter As ToolStripMenuItem
    Friend WithEvents tsslSelectedFiles As ToolStripStatusLabel
    Friend WithEvents Label3 As Label
    Friend WithEvents tbDirFilter As TextBox
    Friend WithEvents btnBrowseFolder As Button
End Class
