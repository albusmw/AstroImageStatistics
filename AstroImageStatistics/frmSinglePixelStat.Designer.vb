<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSinglePixelStat
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
        Me.tbRootFolder = New System.Windows.Forms.TextBox()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.tbOffsetX = New System.Windows.Forms.TextBox()
        Me.tbOffsetY = New System.Windows.Forms.TextBox()
        Me.tbValues = New System.Windows.Forms.TextBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.lbHotCandidates = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbFilter = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pbReading = New System.Windows.Forms.ProgressBar()
        Me.btnClear = New System.Windows.Forms.Button()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRootFolder
        '
        Me.tbRootFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbRootFolder.Location = New System.Drawing.Point(106, 12)
        Me.tbRootFolder.Name = "tbRootFolder"
        Me.tbRootFolder.Size = New System.Drawing.Size(266, 20)
        Me.tbRootFolder.TabIndex = 0
        Me.tbRootFolder.Text = "\\192.168.100.10\astro\2021_03_06 (NGC2174)"
        '
        'btnRun
        '
        Me.btnRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRun.Location = New System.Drawing.Point(1600, 38)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(80, 46)
        Me.btnRun.TabIndex = 1
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'tbOffsetX
        '
        Me.tbOffsetX.Location = New System.Drawing.Point(106, 38)
        Me.tbOffsetX.Name = "tbOffsetX"
        Me.tbOffsetX.Size = New System.Drawing.Size(172, 20)
        Me.tbOffsetX.TabIndex = 2
        Me.tbOffsetX.Text = "272"
        '
        'tbOffsetY
        '
        Me.tbOffsetY.Location = New System.Drawing.Point(106, 64)
        Me.tbOffsetY.Name = "tbOffsetY"
        Me.tbOffsetY.Size = New System.Drawing.Size(172, 20)
        Me.tbOffsetY.TabIndex = 3
        Me.tbOffsetY.Text = "115"
        '
        'tbValues
        '
        Me.tbValues.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbValues.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbValues.Location = New System.Drawing.Point(3, 3)
        Me.tbValues.Multiline = True
        Me.tbValues.Name = "tbValues"
        Me.tbValues.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbValues.Size = New System.Drawing.Size(1103, 1024)
        Me.tbValues.TabIndex = 4
        Me.tbValues.Text = "1000"
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(12, 90)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.btnClear)
        Me.scMain.Panel1.Controls.Add(Me.lbHotCandidates)
        Me.scMain.Panel1.Controls.Add(Me.Label5)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.tbValues)
        Me.scMain.Size = New System.Drawing.Size(1668, 1030)
        Me.scMain.SplitterDistance = 555
        Me.scMain.TabIndex = 5
        '
        'lbHotCandidates
        '
        Me.lbHotCandidates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbHotCandidates.Font = New System.Drawing.Font("Courier New", 8.25!)
        Me.lbHotCandidates.FormattingEnabled = True
        Me.lbHotCandidates.HorizontalScrollbar = True
        Me.lbHotCandidates.IntegralHeight = False
        Me.lbHotCandidates.ItemHeight = 14
        Me.lbHotCandidates.Location = New System.Drawing.Point(17, 31)
        Me.lbHotCandidates.Name = "lbHotCandidates"
        Me.lbHotCandidates.ScrollAlwaysVisible = True
        Me.lbHotCandidates.Size = New System.Drawing.Size(535, 965)
        Me.lbHotCandidates.Sorted = True
        Me.lbHotCandidates.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Hot candidates"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Folder to search"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Pixel X position"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Pixel Y position"
        '
        'tbFilter
        '
        Me.tbFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFilter.Location = New System.Drawing.Point(413, 12)
        Me.tbFilter.Name = "tbFilter"
        Me.tbFilter.Size = New System.Drawing.Size(134, 20)
        Me.tbFilter.TabIndex = 9
        Me.tbFilter.Text = "QHY600*.fit?"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(378, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Filter"
        '
        'pbReading
        '
        Me.pbReading.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbReading.Location = New System.Drawing.Point(553, 12)
        Me.pbReading.Name = "pbReading"
        Me.pbReading.Size = New System.Drawing.Size(258, 20)
        Me.pbReading.TabIndex = 11
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(477, 1002)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 14
        Me.btnClear.Text = "Clear list"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmSinglePixelStat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1692, 1132)
        Me.Controls.Add(Me.pbReading)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbFilter)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.tbOffsetY)
        Me.Controls.Add(Me.tbOffsetX)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.tbRootFolder)
        Me.Name = "frmSinglePixelStat"
        Me.Text = "Pixel statistics over multiple files"
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel1.PerformLayout()
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.Panel2.PerformLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbRootFolder As TextBox
    Friend WithEvents btnRun As Button
    Friend WithEvents tbOffsetX As TextBox
    Friend WithEvents tbOffsetY As TextBox
    Friend WithEvents tbValues As TextBox
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents tbFilter As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents pbReading As ProgressBar
    Friend WithEvents Label5 As Label
    Friend WithEvents lbHotCandidates As ListBox
    Friend WithEvents btnClear As Button
End Class
