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
        Me.cbX.FormattingEnabled = True
        Me.cbX.Location = New System.Drawing.Point(53, 12)
        Me.cbX.Name = "cbX"
        Me.cbX.Size = New System.Drawing.Size(177, 21)
        Me.cbX.TabIndex = 2
        '
        'cbY
        '
        Me.cbY.FormattingEnabled = True
        Me.cbY.Location = New System.Drawing.Point(53, 39)
        Me.cbY.Name = "cbY"
        Me.cbY.Size = New System.Drawing.Size(177, 21)
        Me.cbY.TabIndex = 3
        '
        'btnPlot
        '
        Me.btnPlot.Location = New System.Drawing.Point(12, 66)
        Me.btnPlot.Name = "btnPlot"
        Me.btnPlot.Size = New System.Drawing.Size(218, 40)
        Me.btnPlot.TabIndex = 4
        Me.btnPlot.Text = "Plot"
        Me.btnPlot.UseVisualStyleBackColor = True
        '
        'frmXvsYPlot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(242, 114)
        Me.Controls.Add(Me.btnPlot)
        Me.Controls.Add(Me.cbY)
        Me.Controls.Add(Me.cbX)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmXvsYPlot"
        Me.Text = "X-vs-Y plot"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cbX As ComboBox
    Friend WithEvents cbY As ComboBox
    Friend WithEvents btnPlot As Button
End Class
