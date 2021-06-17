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
        Me.cbSelectedHisto = New System.Windows.Forms.ComboBox()
        Me.tbCurve_1_Scale = New System.Windows.Forms.TextBox()
        Me.tbCurve_1_Name = New System.Windows.Forms.TextBox()
        Me.tbCurve_1_Offset = New System.Windows.Forms.TextBox()
        Me.tbCurve_2_Name = New System.Windows.Forms.TextBox()
        Me.tbCurve_3_Name = New System.Windows.Forms.TextBox()
        Me.tbCurve_4_Name = New System.Windows.Forms.TextBox()
        Me.tbCurve_2_Scale = New System.Windows.Forms.TextBox()
        Me.tbCurve_3_Scale = New System.Windows.Forms.TextBox()
        Me.tbCurve_4_Scale = New System.Windows.Forms.TextBox()
        Me.tbCurve_2_Offset = New System.Windows.Forms.TextBox()
        Me.tbCurve_3_Offset = New System.Windows.Forms.TextBox()
        Me.tbCurve_4_Offset = New System.Windows.Forms.TextBox()
        Me.tbCurve_1_YMul = New System.Windows.Forms.TextBox()
        Me.tbCurve_2_YMul = New System.Windows.Forms.TextBox()
        Me.tbCurve_3_YMul = New System.Windows.Forms.TextBox()
        Me.tbCurve_4_YMul = New System.Windows.Forms.TextBox()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbCurve_Scale_Step = New System.Windows.Forms.TextBox()
        Me.tbCurve_Offset_Step = New System.Windows.Forms.TextBox()
        Me.tbCurve_YMul_Step = New System.Windows.Forms.TextBox()
        Me.tlpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbSelectedHisto
        '
        Me.cbSelectedHisto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSelectedHisto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSelectedHisto.FormattingEnabled = True
        Me.cbSelectedHisto.Location = New System.Drawing.Point(12, 12)
        Me.cbSelectedHisto.Name = "cbSelectedHisto"
        Me.cbSelectedHisto.Size = New System.Drawing.Size(368, 21)
        Me.cbSelectedHisto.TabIndex = 0
        '
        'tbCurve_1_Scale
        '
        Me.tbCurve_1_Scale.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_1_Scale.Location = New System.Drawing.Point(95, 32)
        Me.tbCurve_1_Scale.Name = "tbCurve_1_Scale"
        Me.tbCurve_1_Scale.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_1_Scale.TabIndex = 1
        Me.tbCurve_1_Scale.Text = "1,00"
        Me.tbCurve_1_Scale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_1_Name
        '
        Me.tbCurve_1_Name.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_1_Name.Location = New System.Drawing.Point(3, 32)
        Me.tbCurve_1_Name.Name = "tbCurve_1_Name"
        Me.tbCurve_1_Name.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_1_Name.TabIndex = 2
        Me.tbCurve_1_Name.Text = "R[0,0]"
        '
        'tbCurve_1_Offset
        '
        Me.tbCurve_1_Offset.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_1_Offset.Location = New System.Drawing.Point(187, 32)
        Me.tbCurve_1_Offset.Name = "tbCurve_1_Offset"
        Me.tbCurve_1_Offset.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_1_Offset.TabIndex = 3
        Me.tbCurve_1_Offset.Text = "0,00"
        Me.tbCurve_1_Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_2_Name
        '
        Me.tbCurve_2_Name.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_2_Name.Location = New System.Drawing.Point(3, 60)
        Me.tbCurve_2_Name.Name = "tbCurve_2_Name"
        Me.tbCurve_2_Name.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_2_Name.TabIndex = 4
        Me.tbCurve_2_Name.Text = "G[0,1]"
        '
        'tbCurve_3_Name
        '
        Me.tbCurve_3_Name.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_3_Name.Location = New System.Drawing.Point(3, 88)
        Me.tbCurve_3_Name.Name = "tbCurve_3_Name"
        Me.tbCurve_3_Name.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_3_Name.TabIndex = 5
        Me.tbCurve_3_Name.Text = "G1[1,0]"
        '
        'tbCurve_4_Name
        '
        Me.tbCurve_4_Name.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_4_Name.Location = New System.Drawing.Point(3, 116)
        Me.tbCurve_4_Name.Name = "tbCurve_4_Name"
        Me.tbCurve_4_Name.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_4_Name.TabIndex = 6
        Me.tbCurve_4_Name.Text = "B[1,1]"
        '
        'tbCurve_2_Scale
        '
        Me.tbCurve_2_Scale.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_2_Scale.Location = New System.Drawing.Point(95, 60)
        Me.tbCurve_2_Scale.Name = "tbCurve_2_Scale"
        Me.tbCurve_2_Scale.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_2_Scale.TabIndex = 7
        Me.tbCurve_2_Scale.Text = "1,00"
        Me.tbCurve_2_Scale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_3_Scale
        '
        Me.tbCurve_3_Scale.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_3_Scale.Location = New System.Drawing.Point(95, 88)
        Me.tbCurve_3_Scale.Name = "tbCurve_3_Scale"
        Me.tbCurve_3_Scale.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_3_Scale.TabIndex = 8
        Me.tbCurve_3_Scale.Text = "1,00"
        Me.tbCurve_3_Scale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_4_Scale
        '
        Me.tbCurve_4_Scale.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_4_Scale.Location = New System.Drawing.Point(95, 116)
        Me.tbCurve_4_Scale.Name = "tbCurve_4_Scale"
        Me.tbCurve_4_Scale.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_4_Scale.TabIndex = 9
        Me.tbCurve_4_Scale.Text = "1,00"
        Me.tbCurve_4_Scale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_2_Offset
        '
        Me.tbCurve_2_Offset.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_2_Offset.Location = New System.Drawing.Point(187, 60)
        Me.tbCurve_2_Offset.Name = "tbCurve_2_Offset"
        Me.tbCurve_2_Offset.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_2_Offset.TabIndex = 10
        Me.tbCurve_2_Offset.Text = "0,00"
        Me.tbCurve_2_Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_3_Offset
        '
        Me.tbCurve_3_Offset.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_3_Offset.Location = New System.Drawing.Point(187, 88)
        Me.tbCurve_3_Offset.Name = "tbCurve_3_Offset"
        Me.tbCurve_3_Offset.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_3_Offset.TabIndex = 11
        Me.tbCurve_3_Offset.Text = "0,00"
        Me.tbCurve_3_Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_4_Offset
        '
        Me.tbCurve_4_Offset.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_4_Offset.Location = New System.Drawing.Point(187, 116)
        Me.tbCurve_4_Offset.Name = "tbCurve_4_Offset"
        Me.tbCurve_4_Offset.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_4_Offset.TabIndex = 12
        Me.tbCurve_4_Offset.Text = "0,00"
        Me.tbCurve_4_Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_1_YMul
        '
        Me.tbCurve_1_YMul.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_1_YMul.Location = New System.Drawing.Point(279, 32)
        Me.tbCurve_1_YMul.Name = "tbCurve_1_YMul"
        Me.tbCurve_1_YMul.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_1_YMul.TabIndex = 13
        Me.tbCurve_1_YMul.Text = "1,00"
        Me.tbCurve_1_YMul.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_2_YMul
        '
        Me.tbCurve_2_YMul.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_2_YMul.Location = New System.Drawing.Point(279, 60)
        Me.tbCurve_2_YMul.Name = "tbCurve_2_YMul"
        Me.tbCurve_2_YMul.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_2_YMul.TabIndex = 14
        Me.tbCurve_2_YMul.Text = "1,00"
        Me.tbCurve_2_YMul.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_3_YMul
        '
        Me.tbCurve_3_YMul.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_3_YMul.Location = New System.Drawing.Point(279, 88)
        Me.tbCurve_3_YMul.Name = "tbCurve_3_YMul"
        Me.tbCurve_3_YMul.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_3_YMul.TabIndex = 15
        Me.tbCurve_3_YMul.Text = "1,00"
        Me.tbCurve_3_YMul.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_4_YMul
        '
        Me.tbCurve_4_YMul.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_4_YMul.Location = New System.Drawing.Point(279, 116)
        Me.tbCurve_4_YMul.Name = "tbCurve_4_YMul"
        Me.tbCurve_4_YMul.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_4_YMul.TabIndex = 16
        Me.tbCurve_4_YMul.Text = "1,00"
        Me.tbCurve_4_YMul.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tlpMain
        '
        Me.tlpMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpMain.ColumnCount = 4
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpMain.Controls.Add(Me.tbCurve_Offset_Step, 0, 5)
        Me.tlpMain.Controls.Add(Me.tbCurve_Scale_Step, 0, 5)
        Me.tlpMain.Controls.Add(Me.tbCurve_1_Name, 0, 1)
        Me.tlpMain.Controls.Add(Me.tbCurve_1_YMul, 3, 1)
        Me.tlpMain.Controls.Add(Me.tbCurve_2_YMul, 3, 2)
        Me.tlpMain.Controls.Add(Me.tbCurve_3_YMul, 3, 3)
        Me.tlpMain.Controls.Add(Me.tbCurve_4_YMul, 3, 4)
        Me.tlpMain.Controls.Add(Me.tbCurve_2_Name, 0, 2)
        Me.tlpMain.Controls.Add(Me.tbCurve_3_Name, 0, 3)
        Me.tlpMain.Controls.Add(Me.tbCurve_4_Name, 0, 4)
        Me.tlpMain.Controls.Add(Me.tbCurve_1_Scale, 1, 1)
        Me.tlpMain.Controls.Add(Me.tbCurve_1_Offset, 2, 1)
        Me.tlpMain.Controls.Add(Me.tbCurve_2_Offset, 2, 2)
        Me.tlpMain.Controls.Add(Me.tbCurve_3_Offset, 2, 3)
        Me.tlpMain.Controls.Add(Me.tbCurve_4_Offset, 2, 4)
        Me.tlpMain.Controls.Add(Me.tbCurve_2_Scale, 1, 2)
        Me.tlpMain.Controls.Add(Me.tbCurve_3_Scale, 1, 3)
        Me.tlpMain.Controls.Add(Me.tbCurve_4_Scale, 1, 4)
        Me.tlpMain.Controls.Add(Me.Label1, 0, 0)
        Me.tlpMain.Controls.Add(Me.Label2, 1, 0)
        Me.tlpMain.Controls.Add(Me.Label3, 2, 0)
        Me.tlpMain.Controls.Add(Me.Label4, 3, 0)
        Me.tlpMain.Controls.Add(Me.tbCurve_YMul_Step, 2, 5)
        Me.tlpMain.Controls.Add(Me.Label5, 0, 5)
        Me.tlpMain.Location = New System.Drawing.Point(12, 39)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 6
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66736!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66319!))
        Me.tlpMain.Size = New System.Drawing.Size(368, 171)
        Me.tlpMain.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 23)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Channel"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(95, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 23)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Multiplier"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(187, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 23)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Offset"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(279, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 23)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Y shift (help)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 23)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Stepping"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbCurve_Scale_Step
        '
        Me.tbCurve_Scale_Step.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_Scale_Step.Location = New System.Drawing.Point(95, 145)
        Me.tbCurve_Scale_Step.Name = "tbCurve_Scale_Step"
        Me.tbCurve_Scale_Step.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_Scale_Step.TabIndex = 22
        Me.tbCurve_Scale_Step.Text = "0,1"
        Me.tbCurve_Scale_Step.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_Offset_Step
        '
        Me.tbCurve_Offset_Step.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_Offset_Step.Location = New System.Drawing.Point(187, 145)
        Me.tbCurve_Offset_Step.Name = "tbCurve_Offset_Step"
        Me.tbCurve_Offset_Step.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_Offset_Step.TabIndex = 23
        Me.tbCurve_Offset_Step.Text = "0,1"
        Me.tbCurve_Offset_Step.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbCurve_YMul_Step
        '
        Me.tbCurve_YMul_Step.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCurve_YMul_Step.Location = New System.Drawing.Point(279, 145)
        Me.tbCurve_YMul_Step.Name = "tbCurve_YMul_Step"
        Me.tbCurve_YMul_Step.Size = New System.Drawing.Size(86, 20)
        Me.tbCurve_YMul_Step.TabIndex = 24
        Me.tbCurve_YMul_Step.Text = "0,1"
        Me.tbCurve_YMul_Step.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmManualAdjust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 222)
        Me.Controls.Add(Me.tlpMain)
        Me.Controls.Add(Me.cbSelectedHisto)
        Me.Name = "frmManualAdjust"
        Me.Text = "Manual balance adjustment"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.ResumeLayout(False)

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
