<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestForm
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
        adgvMain = New Zuby.ADGV.AdvancedDataGridView()
        Button1 = New Button()
        CType(adgvMain, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' adgvMain
        ' 
        adgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        adgvMain.FilterAndSortEnabled = True
        adgvMain.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        adgvMain.Location = New Point(12, 12)
        adgvMain.Name = "adgvMain"
        adgvMain.RightToLeft = RightToLeft.No
        adgvMain.RowTemplate.Height = 25
        adgvMain.Size = New Size(776, 382)
        adgvMain.SortStringChangedInvokeBeforeDatasourceUpdate = True
        adgvMain.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(12, 400)
        Button1.Name = "Button1"
        Button1.Size = New Size(89, 38)
        Button1.TabIndex = 1
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' frmTestForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button1)
        Controls.Add(adgvMain)
        Name = "frmTestForm"
        Text = "frmTestForm"
        CType(adgvMain, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents adgvMain As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents Button1 As Button
End Class
