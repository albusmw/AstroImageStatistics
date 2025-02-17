<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManualAdjust
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
        cbSelectedHisto = New ComboBox()
        tbCurve_1_Scale = New TextBox()
        tbCurve_1_Name = New TextBox()
        tbCurve_1_Offset = New TextBox()
        tbCurve_2_Name = New TextBox()
        tbCurve_3_Name = New TextBox()
        tbCurve_4_Name = New TextBox()
        tbCurve_2_Scale = New TextBox()
        tbCurve_3_Scale = New TextBox()
        tbCurve_4_Scale = New TextBox()
        tbCurve_2_Offset = New TextBox()
        tbCurve_3_Offset = New TextBox()
        tbCurve_4_Offset = New TextBox()
        tbCurve_1_YMul = New TextBox()
        tbCurve_2_YMul = New TextBox()
        tbCurve_3_YMul = New TextBox()
        tbCurve_4_YMul = New TextBox()
        tlpMain = New TableLayoutPanel()
        tbCurve_Offset_Step = New TextBox()
        tbCurve_Scale_Step = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        tbCurve_YMul_Step = New TextBox()
        Label5 = New Label()
        tlpMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' cbSelectedHisto
        ' 
        cbSelectedHisto.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cbSelectedHisto.DropDownStyle = ComboBoxStyle.DropDownList
        cbSelectedHisto.FormattingEnabled = True
        cbSelectedHisto.Location = New Point(14, 14)
        cbSelectedHisto.Margin = New Padding(4, 3, 4, 3)
        cbSelectedHisto.Name = "cbSelectedHisto"
        cbSelectedHisto.Size = New Size(443, 23)
        cbSelectedHisto.TabIndex = 0
        ' 
        ' tbCurve_1_Scale
        ' 
        tbCurve_1_Scale.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_1_Scale.Location = New Point(114, 41)
        tbCurve_1_Scale.Margin = New Padding(4, 3, 4, 3)
        tbCurve_1_Scale.Name = "tbCurve_1_Scale"
        tbCurve_1_Scale.Size = New Size(102, 23)
        tbCurve_1_Scale.TabIndex = 1
        tbCurve_1_Scale.Text = "1,00"
        tbCurve_1_Scale.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_1_Name
        ' 
        tbCurve_1_Name.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_1_Name.Location = New Point(4, 41)
        tbCurve_1_Name.Margin = New Padding(4, 3, 4, 3)
        tbCurve_1_Name.Name = "tbCurve_1_Name"
        tbCurve_1_Name.Size = New Size(102, 23)
        tbCurve_1_Name.TabIndex = 2
        tbCurve_1_Name.Text = "R[0,0]"
        ' 
        ' tbCurve_1_Offset
        ' 
        tbCurve_1_Offset.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_1_Offset.Location = New Point(224, 41)
        tbCurve_1_Offset.Margin = New Padding(4, 3, 4, 3)
        tbCurve_1_Offset.Name = "tbCurve_1_Offset"
        tbCurve_1_Offset.Size = New Size(102, 23)
        tbCurve_1_Offset.TabIndex = 3
        tbCurve_1_Offset.Text = "0,00"
        tbCurve_1_Offset.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_2_Name
        ' 
        tbCurve_2_Name.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_2_Name.Location = New Point(4, 76)
        tbCurve_2_Name.Margin = New Padding(4, 3, 4, 3)
        tbCurve_2_Name.Name = "tbCurve_2_Name"
        tbCurve_2_Name.Size = New Size(102, 23)
        tbCurve_2_Name.TabIndex = 4
        tbCurve_2_Name.Text = "G[0,1]"
        ' 
        ' tbCurve_3_Name
        ' 
        tbCurve_3_Name.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_3_Name.Location = New Point(4, 111)
        tbCurve_3_Name.Margin = New Padding(4, 3, 4, 3)
        tbCurve_3_Name.Name = "tbCurve_3_Name"
        tbCurve_3_Name.Size = New Size(102, 23)
        tbCurve_3_Name.TabIndex = 5
        tbCurve_3_Name.Text = "G1[1,0]"
        ' 
        ' tbCurve_4_Name
        ' 
        tbCurve_4_Name.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_4_Name.Location = New Point(4, 146)
        tbCurve_4_Name.Margin = New Padding(4, 3, 4, 3)
        tbCurve_4_Name.Name = "tbCurve_4_Name"
        tbCurve_4_Name.Size = New Size(102, 23)
        tbCurve_4_Name.TabIndex = 6
        tbCurve_4_Name.Text = "B[1,1]"
        ' 
        ' tbCurve_2_Scale
        ' 
        tbCurve_2_Scale.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_2_Scale.Location = New Point(114, 76)
        tbCurve_2_Scale.Margin = New Padding(4, 3, 4, 3)
        tbCurve_2_Scale.Name = "tbCurve_2_Scale"
        tbCurve_2_Scale.Size = New Size(102, 23)
        tbCurve_2_Scale.TabIndex = 7
        tbCurve_2_Scale.Text = "1,00"
        tbCurve_2_Scale.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_3_Scale
        ' 
        tbCurve_3_Scale.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_3_Scale.Location = New Point(114, 111)
        tbCurve_3_Scale.Margin = New Padding(4, 3, 4, 3)
        tbCurve_3_Scale.Name = "tbCurve_3_Scale"
        tbCurve_3_Scale.Size = New Size(102, 23)
        tbCurve_3_Scale.TabIndex = 8
        tbCurve_3_Scale.Text = "1,00"
        tbCurve_3_Scale.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_4_Scale
        ' 
        tbCurve_4_Scale.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_4_Scale.Location = New Point(114, 146)
        tbCurve_4_Scale.Margin = New Padding(4, 3, 4, 3)
        tbCurve_4_Scale.Name = "tbCurve_4_Scale"
        tbCurve_4_Scale.Size = New Size(102, 23)
        tbCurve_4_Scale.TabIndex = 9
        tbCurve_4_Scale.Text = "1,00"
        tbCurve_4_Scale.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_2_Offset
        ' 
        tbCurve_2_Offset.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_2_Offset.Location = New Point(224, 76)
        tbCurve_2_Offset.Margin = New Padding(4, 3, 4, 3)
        tbCurve_2_Offset.Name = "tbCurve_2_Offset"
        tbCurve_2_Offset.Size = New Size(102, 23)
        tbCurve_2_Offset.TabIndex = 10
        tbCurve_2_Offset.Text = "0,00"
        tbCurve_2_Offset.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_3_Offset
        ' 
        tbCurve_3_Offset.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_3_Offset.Location = New Point(224, 111)
        tbCurve_3_Offset.Margin = New Padding(4, 3, 4, 3)
        tbCurve_3_Offset.Name = "tbCurve_3_Offset"
        tbCurve_3_Offset.Size = New Size(102, 23)
        tbCurve_3_Offset.TabIndex = 11
        tbCurve_3_Offset.Text = "0,00"
        tbCurve_3_Offset.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_4_Offset
        ' 
        tbCurve_4_Offset.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_4_Offset.Location = New Point(224, 146)
        tbCurve_4_Offset.Margin = New Padding(4, 3, 4, 3)
        tbCurve_4_Offset.Name = "tbCurve_4_Offset"
        tbCurve_4_Offset.Size = New Size(102, 23)
        tbCurve_4_Offset.TabIndex = 12
        tbCurve_4_Offset.Text = "0,00"
        tbCurve_4_Offset.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_1_YMul
        ' 
        tbCurve_1_YMul.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_1_YMul.Location = New Point(334, 41)
        tbCurve_1_YMul.Margin = New Padding(4, 3, 4, 3)
        tbCurve_1_YMul.Name = "tbCurve_1_YMul"
        tbCurve_1_YMul.Size = New Size(105, 23)
        tbCurve_1_YMul.TabIndex = 13
        tbCurve_1_YMul.Text = "1,00"
        tbCurve_1_YMul.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_2_YMul
        ' 
        tbCurve_2_YMul.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_2_YMul.Location = New Point(334, 76)
        tbCurve_2_YMul.Margin = New Padding(4, 3, 4, 3)
        tbCurve_2_YMul.Name = "tbCurve_2_YMul"
        tbCurve_2_YMul.Size = New Size(105, 23)
        tbCurve_2_YMul.TabIndex = 14
        tbCurve_2_YMul.Text = "1,00"
        tbCurve_2_YMul.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_3_YMul
        ' 
        tbCurve_3_YMul.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_3_YMul.Location = New Point(334, 111)
        tbCurve_3_YMul.Margin = New Padding(4, 3, 4, 3)
        tbCurve_3_YMul.Name = "tbCurve_3_YMul"
        tbCurve_3_YMul.Size = New Size(105, 23)
        tbCurve_3_YMul.TabIndex = 15
        tbCurve_3_YMul.Text = "1,00"
        tbCurve_3_YMul.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_4_YMul
        ' 
        tbCurve_4_YMul.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_4_YMul.Location = New Point(334, 146)
        tbCurve_4_YMul.Margin = New Padding(4, 3, 4, 3)
        tbCurve_4_YMul.Name = "tbCurve_4_YMul"
        tbCurve_4_YMul.Size = New Size(105, 23)
        tbCurve_4_YMul.TabIndex = 16
        tbCurve_4_YMul.Text = "1,00"
        tbCurve_4_YMul.TextAlign = HorizontalAlignment.Right
        ' 
        ' tlpMain
        ' 
        tlpMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpMain.ColumnCount = 4
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpMain.Controls.Add(tbCurve_Offset_Step, 0, 5)
        tlpMain.Controls.Add(tbCurve_Scale_Step, 0, 5)
        tlpMain.Controls.Add(tbCurve_1_Name, 0, 1)
        tlpMain.Controls.Add(tbCurve_1_YMul, 3, 1)
        tlpMain.Controls.Add(tbCurve_2_YMul, 3, 2)
        tlpMain.Controls.Add(tbCurve_3_YMul, 3, 3)
        tlpMain.Controls.Add(tbCurve_4_YMul, 3, 4)
        tlpMain.Controls.Add(tbCurve_2_Name, 0, 2)
        tlpMain.Controls.Add(tbCurve_3_Name, 0, 3)
        tlpMain.Controls.Add(tbCurve_4_Name, 0, 4)
        tlpMain.Controls.Add(tbCurve_1_Scale, 1, 1)
        tlpMain.Controls.Add(tbCurve_1_Offset, 2, 1)
        tlpMain.Controls.Add(tbCurve_2_Offset, 2, 2)
        tlpMain.Controls.Add(tbCurve_3_Offset, 2, 3)
        tlpMain.Controls.Add(tbCurve_4_Offset, 2, 4)
        tlpMain.Controls.Add(tbCurve_2_Scale, 1, 2)
        tlpMain.Controls.Add(tbCurve_3_Scale, 1, 3)
        tlpMain.Controls.Add(tbCurve_4_Scale, 1, 4)
        tlpMain.Controls.Add(Label1, 0, 0)
        tlpMain.Controls.Add(Label2, 1, 0)
        tlpMain.Controls.Add(Label3, 2, 0)
        tlpMain.Controls.Add(Label4, 3, 0)
        tlpMain.Controls.Add(tbCurve_YMul_Step, 2, 5)
        tlpMain.Controls.Add(Label5, 0, 5)
        tlpMain.Location = New Point(14, 45)
        tlpMain.Margin = New Padding(4, 3, 4, 3)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 6
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 16.66736F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 16.66736F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 16.66736F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 16.66736F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 16.66736F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 16.66319F))
        tlpMain.Size = New Size(443, 214)
        tlpMain.TabIndex = 17
        ' 
        ' tbCurve_Offset_Step
        ' 
        tbCurve_Offset_Step.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_Offset_Step.Location = New Point(224, 183)
        tbCurve_Offset_Step.Margin = New Padding(4, 3, 4, 3)
        tbCurve_Offset_Step.Name = "tbCurve_Offset_Step"
        tbCurve_Offset_Step.Size = New Size(102, 23)
        tbCurve_Offset_Step.TabIndex = 23
        tbCurve_Offset_Step.Text = "0,1"
        tbCurve_Offset_Step.TextAlign = HorizontalAlignment.Right
        ' 
        ' tbCurve_Scale_Step
        ' 
        tbCurve_Scale_Step.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_Scale_Step.Location = New Point(114, 183)
        tbCurve_Scale_Step.Margin = New Padding(4, 3, 4, 3)
        tbCurve_Scale_Step.Name = "tbCurve_Scale_Step"
        tbCurve_Scale_Step.Size = New Size(102, 23)
        tbCurve_Scale_Step.TabIndex = 22
        tbCurve_Scale_Step.Text = "0,1"
        tbCurve_Scale_Step.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        Label1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(4, 4)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(102, 27)
        Label1.TabIndex = 17
        Label1.Text = "Channel"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        Label2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(114, 4)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(102, 27)
        Label2.TabIndex = 18
        Label2.Text = "Multiplier"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        Label3.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(224, 4)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(102, 27)
        Label3.TabIndex = 19
        Label3.Text = "Offset"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label4
        ' 
        Label4.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        Label4.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(334, 4)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(105, 27)
        Label4.TabIndex = 20
        Label4.Text = "Y shift (help)"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' tbCurve_YMul_Step
        ' 
        tbCurve_YMul_Step.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbCurve_YMul_Step.Location = New Point(334, 183)
        tbCurve_YMul_Step.Margin = New Padding(4, 3, 4, 3)
        tbCurve_YMul_Step.Name = "tbCurve_YMul_Step"
        tbCurve_YMul_Step.Size = New Size(105, 23)
        tbCurve_YMul_Step.TabIndex = 24
        tbCurve_YMul_Step.Text = "0,1"
        tbCurve_YMul_Step.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        Label5.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(4, 181)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(102, 27)
        Label5.TabIndex = 21
        Label5.Text = "Stepping"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' frmManualAdjust
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(481, 273)
        Controls.Add(tlpMain)
        Controls.Add(cbSelectedHisto)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frmManualAdjust"
        Text = "Manual balance adjustment"
        tlpMain.ResumeLayout(False)
        tlpMain.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents cbSelectedHisto As ComboBox
    Friend WithEvents tbCurve_1_Scale As TextBox
    Friend WithEvents tbCurve_1_Name As TextBox
    Friend WithEvents tbCurve_1_Offset As TextBox
    Friend WithEvents tbCurve_2_Name As TextBox
    Friend WithEvents tbCurve_3_Name As TextBox
    Friend WithEvents tbCurve_4_Name As TextBox
    Friend WithEvents tbCurve_2_Scale As TextBox
    Friend WithEvents tbCurve_3_Scale As TextBox
    Friend WithEvents tbCurve_4_Scale As TextBox
    Friend WithEvents tbCurve_2_Offset As TextBox
    Friend WithEvents tbCurve_3_Offset As TextBox
    Friend WithEvents tbCurve_4_Offset As TextBox
    Friend WithEvents tbCurve_1_YMul As TextBox
    Friend WithEvents tbCurve_2_YMul As TextBox
    Friend WithEvents tbCurve_3_YMul As TextBox
    Friend WithEvents tbCurve_4_YMul As TextBox
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents tbCurve_Offset_Step As TextBox
    Friend WithEvents tbCurve_Scale_Step As TextBox
    Friend WithEvents tbCurve_YMul_Step As TextBox
    Friend WithEvents Label5 As Label
End Class
