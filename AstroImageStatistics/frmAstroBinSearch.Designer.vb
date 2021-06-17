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
        Me.LogBox = New cLogTextBox()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(839, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(168, 107)
        Me.btnSearch.TabIndex = 0
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'LogBox
        '
        Me.LogBox.Font = New System.Drawing.Font("Courier New", 8.0!)
        Me.LogBox.Location = New System.Drawing.Point(12, 508)
        Me.LogBox.Multiline = True
        Me.LogBox.Name = "LogBox"
        Me.LogBox.ReadOnly = True
        Me.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.LogBox.Size = New System.Drawing.Size(995, 429)
        Me.LogBox.TabIndex = 1
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 949)
        Me.Controls.Add(Me.LogBox)
        Me.Controls.Add(Me.btnSearch)
        Me.Name = "MainForm"
        Me.Text = "AstroBin search"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSearch As Button
    Friend WithEvents LogBox As AstroImageStatistics.cLogTextBox

End Class
