Option Explicit On
Option Strict On
Imports System.ComponentModel

'''<summary>Form to run everything DLL and display some header information.</summary>
Public Class frmFITSGrep

    Private WithEvents FITSGrepper As New cFITSGrepper
    Private MyTable As New DataTable
    Private WithEvents MyData As New BindingSource

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim MyButton As Button = CType(sender, Button)
        If MyButton.Text = "Search" Then
            MyButton.Text = "Stop"
            FITSGrepper.StopFlag = False
            FITSGrepper.Grep(tbRootFolder.Text, tbDirFilter.Text, tbFileFilter.Text)
            MyData.DataSource = FITSGrepper.GetDataTable
            If IsNothing(MyData.DataSource) = False Then
                adgvMain.DataSource = MyData
                adgvMain.SortASC(adgvMain.Columns(0))
                'adgvMain.AutoResizeColumns()
            Else
                adgvMain.DataSource = Nothing
            End If
            tbOutput.Text &= Join(FITSGrepper.Report.ToArray, System.Environment.NewLine)
            tspbMain.Value = 0
            MyButton.Text = "Search"
        Else
            FITSGrepper.StopFlag = True
            MyButton.Text = "Search"
        End If
    End Sub

    Private Sub tbRootFolder_KeyUp(sender As Object, e As KeyEventArgs) Handles tbRootFolder.KeyUp
        If e.KeyCode = Keys.Enter And btnSearch.Enabled = True Then btnSearch_Click(btnSearch, Nothing)
    End Sub

    Private Sub tUpdate_Tick(sender As Object, e As EventArgs) Handles tUpdate.Tick
        If FITSGrepper.Progress.Total > -1 Then
            tspbMain.Maximum = FITSGrepper.Progress.Total
            tspbMain.Value = FITSGrepper.Progress.Current
            tsslProgress.Text = FITSGrepper.Progress.Current.ValRegIndep & "/" & FITSGrepper.Progress.Total.ValRegIndep
        Else
            tsslProgress.Text = "---/---"
            tspbMain.Value = 0
        End If
        tsslMessage.Text = FITSGrepper.Progress.Message
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub adgvMain_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles adgvMain.FilterStringChanged
        MyData.Filter = adgvMain.FilterString
    End Sub

    Private Sub adgvMain_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs) Handles adgvMain.SortStringChanged
        MyData.Sort = adgvMain.SortString
    End Sub

    Private Sub adgvMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles adgvMain.CellContentClick
        'If the file column is selected, open file if file exists
        If adgvMain.Columns.Item(e.ColumnIndex).HeaderText = "FileName" Then
            Dim FileName As String = CStr(adgvMain.SelectedCells.Item(0).Value)
            If System.IO.File.Exists(FileName) Then Process.Start(FileName)
        End If
    End Sub

    Private Sub tsmiFile_ResetFilter_Click(sender As Object, e As EventArgs) Handles tsmiFile_ResetFilter.Click
        adgvMain.CleanFilter(True)
    End Sub

    Private Sub MyData_ListChanged(sender As Object, e As ListChangedEventArgs) Handles MyData.ListChanged
        tsslSelectedFiles.Text = MyData.List.Count & " files filtered"
    End Sub

    Private Sub btnBrowseFolder_Click(sender As Object, e As EventArgs) Handles btnBrowseFolder.Click
        If System.IO.Directory.Exists(tbRootFolder.Text) Then Process.Start(tbRootFolder.Text)
    End Sub

End Class