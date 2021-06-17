<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLastOpenedFiles
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
        Me.lbFiles = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lbFiles
        '
        Me.lbFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbFiles.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFiles.FormattingEnabled = True
        Me.lbFiles.IntegralHeight = False
        Me.lbFiles.ItemHeight = 14
        Me.lbFiles.Location = New System.Drawing.Point(12, 12)
        Me.lbFiles.Name = "lbFiles"
        Me.lbFiles.ScrollAlwaysVisible = True
        Me.lbFiles.Size = New System.Drawing.Size(612, 259)
        Me.lbFiles.TabIndex = 0
        '
        'frmLastOpenedFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 283)
        Me.Controls.Add(Me.lbFiles)
        Me.KeyPreview = True
        Me.Name = "frmLastOpenedFiles"
        Me.Text = "Last opened files"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lbFiles As ListBox
End Class
