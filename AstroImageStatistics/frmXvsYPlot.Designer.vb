<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmXvsYPlot
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbX = New System.Windows.Forms.ComboBox()
        Me.cbY = New System.Windows.Forms.ComboBox()
        Me.btnPlot = New System.Windows.Forms.Button()
        Me.pPlot1 = New System.Windows.Forms.Panel()
        Me.cbAutoPlot = New System.Windows.Forms.CheckBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.pPlot2 = New System.Windows.Forms.Panel()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "X axis"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Y axis"
        '
        'cbX
        '
        Me.cbX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbX.FormattingEnabled = True
        Me.cbX.Location = New System.Drawing.Point(53, 12)
        Me.cbX.Name = "cbX"
        Me.cbX.Size = New System.Drawing.Size(59, 21)
        Me.cbX.TabIndex = 2
        '
        'cbY
        '
        Me.cbY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbY.FormattingEnabled = True
        Me.cbY.Location = New System.Drawing.Point(53, 39)
        Me.cbY.Name = "cbY"
        Me.cbY.Size = New System.Drawing.Size(59, 21)
        Me.cbY.TabIndex = 3
        '
        'btnPlot
        '
        Me.btnPlot.Location = New System.Drawing.Point(12, 94)
        Me.btnPlot.Name = "btnPlot"
        Me.btnPlot.Size = New System.Drawing.Size(100, 40)
        Me.btnPlot.TabIndex = 4
        Me.btnPlot.Text = "Plot"
        Me.btnPlot.UseVisualStyleBackColor = True
        '
        'pPlot1
        '
        Me.pPlot1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pPlot1.Location = New System.Drawing.Point(0, 0)
        Me.pPlot1.Name = "pPlot1"
        Me.pPlot1.Size = New System.Drawing.Size(1160, 473)
        Me.pPlot1.TabIndex = 5
        '
        'cbAutoPlot
        '
        Me.cbAutoPlot.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbAutoPlot.Checked = True
        Me.cbAutoPlot.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAutoPlot.Location = New System.Drawing.Point(15, 66)
        Me.cbAutoPlot.Name = "cbAutoPlot"
        Me.cbAutoPlot.Size = New System.Drawing.Size(97, 24)
        Me.cbAutoPlot.TabIndex = 6
        Me.cbAutoPlot.Text = "Auto-plot"
        Me.cbAutoPlot.UseVisualStyleBackColor = True
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(118, 15)
        Me.scMain.Name = "scMain"
        Me.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.pPlot1)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.pPlot2)
        Me.scMain.Size = New System.Drawing.Size(1160, 947)
        Me.scMain.SplitterDistance = 473
        Me.scMain.TabIndex = 7
        '
        'pPlot2
        '
        Me.pPlot2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pPlot2.Location = New System.Drawing.Point(0, 0)
        Me.pPlot2.Name = "pPlot2"
        Me.pPlot2.Size = New System.Drawing.Size(1160, 470)
        Me.pPlot2.TabIndex = 6
        '
        'frmXvsYPlot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1290, 974)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.cbAutoPlot)
        Me.Controls.Add(Me.btnPlot)
        Me.Controls.Add(Me.cbY)
        Me.Controls.Add(Me.cbX)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmXvsYPlot"
        Me.Text = "X-vs-Y plot"
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cbX As ComboBox
    Friend WithEvents cbY As ComboBox
    Friend WithEvents btnPlot As Button
    Friend WithEvents pPlot1 As Panel
    Friend WithEvents cbAutoPlot As CheckBox
    Friend WithEvents scMain As SplitContainer
    Friend WithEvents pPlot2 As Panel
End Class
