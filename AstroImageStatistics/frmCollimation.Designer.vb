<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCollimation
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
        Me.tbX = New System.Windows.Forms.TextBox()
        Me.tbY = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbRadius = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbXHalf = New System.Windows.Forms.CheckBox()
        Me.cbYHalf = New System.Windows.Forms.CheckBox()
        Me.tbRadiusCombiner = New System.Windows.Forms.TextBox()
        Me.lRadiusBins = New System.Windows.Forms.Label()
        Me.scGraph = New System.Windows.Forms.SplitContainer()
        Me.tbAngleSegments = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.scGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scGraph.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbX
        '
        Me.tbX.Location = New System.Drawing.Point(123, 12)
        Me.tbX.Name = "tbX"
        Me.tbX.Size = New System.Drawing.Size(100, 20)
        Me.tbX.TabIndex = 1
        Me.tbX.Text = "157"
        Me.tbX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbY
        '
        Me.tbY.Location = New System.Drawing.Point(123, 38)
        Me.tbY.Name = "tbY"
        Me.tbY.Size = New System.Drawing.Size(100, 20)
        Me.tbY.TabIndex = 2
        Me.tbY.Text = "162"
        Me.tbY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Center - X"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Center - Y"
        '
        'tbRadius
        '
        Me.tbRadius.Location = New System.Drawing.Point(123, 64)
        Me.tbRadius.Name = "tbRadius"
        Me.tbRadius.Size = New System.Drawing.Size(100, 20)
        Me.tbRadius.TabIndex = 5
        Me.tbRadius.Text = "250"
        Me.tbRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Radius"
        '
        'cbXHalf
        '
        Me.cbXHalf.AutoSize = True
        Me.cbXHalf.Location = New System.Drawing.Point(230, 14)
        Me.cbXHalf.Name = "cbXHalf"
        Me.cbXHalf.Size = New System.Drawing.Size(52, 17)
        Me.cbXHalf.TabIndex = 7
        Me.cbXHalf.Text = "+ 1/2"
        Me.cbXHalf.UseVisualStyleBackColor = True
        '
        'cbYHalf
        '
        Me.cbYHalf.AutoSize = True
        Me.cbYHalf.Location = New System.Drawing.Point(229, 41)
        Me.cbYHalf.Name = "cbYHalf"
        Me.cbYHalf.Size = New System.Drawing.Size(52, 17)
        Me.cbYHalf.TabIndex = 8
        Me.cbYHalf.Text = "+ 1/2"
        Me.cbYHalf.UseVisualStyleBackColor = True
        '
        'tbRadiusCombiner
        '
        Me.tbRadiusCombiner.Location = New System.Drawing.Point(230, 64)
        Me.tbRadiusCombiner.Name = "tbRadiusCombiner"
        Me.tbRadiusCombiner.Size = New System.Drawing.Size(100, 20)
        Me.tbRadiusCombiner.TabIndex = 9
        Me.tbRadiusCombiner.Text = "1.0"
        Me.tbRadiusCombiner.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lRadiusBins
        '
        Me.lRadiusBins.AutoSize = True
        Me.lRadiusBins.Location = New System.Drawing.Point(346, 70)
        Me.lRadiusBins.Name = "lRadiusBins"
        Me.lRadiusBins.Size = New System.Drawing.Size(69, 13)
        Me.lRadiusBins.TabIndex = 10
        Me.lRadiusBins.Text = "Histo Bins: ..."
        '
        'scGraph
        '
        Me.scGraph.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scGraph.Location = New System.Drawing.Point(12, 90)
        Me.scGraph.Name = "scGraph"
        Me.scGraph.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.scGraph.Size = New System.Drawing.Size(933, 675)
        Me.scGraph.SplitterDistance = 335
        Me.scGraph.TabIndex = 11
        '
        'tbAngleSegments
        '
        Me.tbAngleSegments.Location = New System.Drawing.Point(555, 12)
        Me.tbAngleSegments.Name = "tbAngleSegments"
        Me.tbAngleSegments.Size = New System.Drawing.Size(100, 20)
        Me.tbAngleSegments.TabIndex = 12
        Me.tbAngleSegments.Text = "360"
        Me.tbAngleSegments.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(467, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Angle segments"
        '
        'frmCollimation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(957, 777)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbAngleSegments)
        Me.Controls.Add(Me.scGraph)
        Me.Controls.Add(Me.lRadiusBins)
        Me.Controls.Add(Me.tbRadiusCombiner)
        Me.Controls.Add(Me.cbYHalf)
        Me.Controls.Add(Me.cbXHalf)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbRadius)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbY)
        Me.Controls.Add(Me.tbX)
        Me.Name = "frmCollimation"
        Me.Text = "Collimation statistics"
        CType(Me.scGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scGraph.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbX As TextBox
    Friend WithEvents tbY As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents tbRadius As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cbXHalf As CheckBox
    Friend WithEvents cbYHalf As CheckBox
    Friend WithEvents tbRadiusCombiner As TextBox
    Friend WithEvents lRadiusBins As Label
    Friend WithEvents scGraph As SplitContainer
    Friend WithEvents tbAngleSegments As TextBox
    Friend WithEvents Label4 As Label
End Class
