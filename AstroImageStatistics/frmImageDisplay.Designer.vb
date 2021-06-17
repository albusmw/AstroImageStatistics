<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImageDisplay
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
        Me.components = New System.ComponentModel.Container()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.scLeft = New System.Windows.Forms.SplitContainer()
        Me.pgMain = New System.Windows.Forms.PropertyGrid()
        Me.scDetails = New System.Windows.Forms.SplitContainer()
        Me.tbDetails = New System.Windows.Forms.TextBox()
        Me.cmsImage = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cms_SetCutOff = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_SendToNavigator = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_SendToSinglePixelStat = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cms_SaveAsSeen = New System.Windows.Forms.ToolStripMenuItem()
        Me.scImageAndScale = New System.Windows.Forms.SplitContainer()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslInfo1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sfdMain = New System.Windows.Forms.SaveFileDialog()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        CType(Me.scLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scLeft.Panel1.SuspendLayout()
        Me.scLeft.Panel2.SuspendLayout()
        Me.scLeft.SuspendLayout()
        CType(Me.scDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scDetails.Panel1.SuspendLayout()
        Me.scDetails.SuspendLayout()
        Me.cmsImage.SuspendLayout()
        CType(Me.scImageAndScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scImageAndScale.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.Location = New System.Drawing.Point(4, 0)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.scLeft)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.ContextMenuStrip = Me.cmsImage
        Me.scMain.Panel2.Controls.Add(Me.scImageAndScale)
        Me.scMain.Size = New System.Drawing.Size(1122, 974)
        Me.scMain.SplitterDistance = 284
        Me.scMain.TabIndex = 0
        '
        'scLeft
        '
        Me.scLeft.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scLeft.Location = New System.Drawing.Point(0, 0)
        Me.scLeft.Name = "scLeft"
        Me.scLeft.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scLeft.Panel1
        '
        Me.scLeft.Panel1.Controls.Add(Me.pgMain)
        '
        'scLeft.Panel2
        '
        Me.scLeft.Panel2.Controls.Add(Me.scDetails)
        Me.scLeft.Size = New System.Drawing.Size(284, 974)
        Me.scLeft.SplitterDistance = 412
        Me.scLeft.TabIndex = 1
        '
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgMain.Location = New System.Drawing.Point(0, 0)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(284, 412)
        Me.pgMain.TabIndex = 0
        '
        'scDetails
        '
        Me.scDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scDetails.Location = New System.Drawing.Point(0, 0)
        Me.scDetails.Name = "scDetails"
        Me.scDetails.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scDetails.Panel1
        '
        Me.scDetails.Panel1.Controls.Add(Me.tbDetails)
        Me.scDetails.Size = New System.Drawing.Size(284, 558)
        Me.scDetails.SplitterDistance = 213
        Me.scDetails.TabIndex = 0
        '
        'tbDetails
        '
        Me.tbDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbDetails.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDetails.Location = New System.Drawing.Point(0, 0)
        Me.tbDetails.Multiline = True
        Me.tbDetails.Name = "tbDetails"
        Me.tbDetails.ReadOnly = True
        Me.tbDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbDetails.Size = New System.Drawing.Size(284, 213)
        Me.tbDetails.TabIndex = 0
        '
        'cmsImage
        '
        Me.cmsImage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cms_SetCutOff, Me.cms_SendToNavigator, Me.cms_SendToSinglePixelStat, Me.ToolStripMenuItem1, Me.cms_SaveAsSeen})
        Me.cmsImage.Name = "cmsImage"
        Me.cmsImage.Size = New System.Drawing.Size(230, 98)
        '
        'cms_SetCutOff
        '
        Me.cms_SetCutOff.Name = "cms_SetCutOff"
        Me.cms_SetCutOff.Size = New System.Drawing.Size(229, 22)
        Me.cms_SetCutOff.Text = "Set zoom min-max as cut-off"
        '
        'cms_SendToNavigator
        '
        Me.cms_SendToNavigator.Name = "cms_SendToNavigator"
        Me.cms_SendToNavigator.Size = New System.Drawing.Size(229, 22)
        Me.cms_SendToNavigator.Text = "Send to navigator"
        '
        'cms_SendToSinglePixelStat
        '
        Me.cms_SendToSinglePixelStat.Name = "cms_SendToSinglePixelStat"
        Me.cms_SendToSinglePixelStat.Size = New System.Drawing.Size(229, 22)
        Me.cms_SendToSinglePixelStat.Text = "Send to single pixel stat"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(226, 6)
        '
        'cms_SaveAsSeen
        '
        Me.cms_SaveAsSeen.Name = "cms_SaveAsSeen"
        Me.cms_SaveAsSeen.Size = New System.Drawing.Size(229, 22)
        Me.cms_SaveAsSeen.Text = "Save as seen"
        '
        'scImageAndScale
        '
        Me.scImageAndScale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scImageAndScale.Location = New System.Drawing.Point(0, 0)
        Me.scImageAndScale.Name = "scImageAndScale"
        Me.scImageAndScale.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.scImageAndScale.Size = New System.Drawing.Size(834, 974)
        Me.scImageAndScale.SplitterDistance = 945
        Me.scImageAndScale.TabIndex = 0
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslInfo1})
        Me.ssMain.Location = New System.Drawing.Point(0, 977)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1134, 22)
        Me.ssMain.TabIndex = 1
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslInfo1
        '
        Me.tsslInfo1.Name = "tsslInfo1"
        Me.tsslInfo1.Size = New System.Drawing.Size(22, 17)
        Me.tsslInfo1.Text = "---"
        '
        'frmImageDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1134, 999)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.scMain)
        Me.KeyPreview = True
        Me.Name = "frmImageDisplay"
        Me.Text = "Image display"
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.scLeft.Panel1.ResumeLayout(False)
        Me.scLeft.Panel2.ResumeLayout(False)
        CType(Me.scLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scLeft.ResumeLayout(False)
        Me.scDetails.Panel1.ResumeLayout(False)
        Me.scDetails.Panel1.PerformLayout()
        CType(Me.scDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scDetails.ResumeLayout(False)
        Me.cmsImage.ResumeLayout(False)
        CType(Me.scImageAndScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scImageAndScale.ResumeLayout(False)
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents scMain As SplitContainer
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslInfo1 As ToolStripStatusLabel
    Friend WithEvents scLeft As SplitContainer
    Friend WithEvents scDetails As SplitContainer
    Friend WithEvents tbDetails As TextBox
    Friend WithEvents cmsImage As ContextMenuStrip
    Friend WithEvents cms_SetCutOff As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents cms_SaveAsSeen As ToolStripMenuItem
    Friend WithEvents sfdMain As SaveFileDialog
    Friend WithEvents scImageAndScale As SplitContainer
    Friend WithEvents cms_SendToNavigator As ToolStripMenuItem
    Friend WithEvents cms_SendToSinglePixelStat As ToolStripMenuItem
End Class
