Option Explicit On
Option Strict On

Public Class frmStacking

    Public Class cSettings
        <ComponentModel.Category("1. Calculation")>
        <ComponentModel.DisplayName("1. Calculate statistics")>
        Public Property Calc_statistics As Boolean = False
        <ComponentModel.Category("1. Calculation")>
        <ComponentModel.DisplayName("2. Store all images")>
        Public Property Calc_StoreAllImages As Boolean = False
    End Class
    Public Settings As New cSettings

    '''<summary>Storage for a simple stack processing.</summary>
    Private StackingStatistics(,) As Ato.cSingleValueStatistics

    Private AllFiles As New List(Of String)

    Private AllImages() As AstroNET.Statistics

    Private ImageStack_UInt32(,) As UInt32

    Private Log As cLog

    Private Stopper As cStopper

    '''<summary>Drag-and-drop handler.</summary>
    Private WithEvents DD As Ato.DragDrop

    Private Sub frmStacking_Load(sender As Object, e As EventArgs) Handles Me.Load
        DD = New Ato.DragDrop(dgvMain)              'Init drap-and-drop
        pgMain.SelectedObject = Settings            'Settings
    End Sub

    Private Sub DD_DropOccured(Files() As String) Handles DD.DropOccured
        'Handle drag-and-drop for all dropped FIT(s) files
        For Each File As String In Files
            AllFiles.Add(File)
        Next File
        ShowAllFiles()
    End Sub

    Private Sub ShowAllFiles()
        dgvMain.Rows.Clear()
        For Each File As String In AllFiles
            dgvMain.Rows.Add(New Object() {True, File})
        Next File
    End Sub

    Private Sub Stack()

        'A per-pixel statistics with full data memory does not work as it is very slow ...
        'So this code can never have worked on full-res QHY600 data

        Stopper = New cStopper
        Dim FITSReader As New cFITSReader(AIS.DB.IPPPath)

        ReDim AllImages(AllFiles.Count - 1)
        For Idx As Integer = 0 To AllFiles.Count - 1

            'Decide where to store the image
            Dim AllImagesIdx As Integer = Idx
            If Settings.Calc_StoreAllImages = True Then
                AllImagesIdx = 0
            End If

            'Init and read header
            Dim FileName As String = AllFiles(Idx)
            Dim FileNameOnly As String = System.IO.Path.GetFileName(FileName)
            Log.Log("Processing file " & (Idx + 1).ToString.Trim & "/" & (AllFiles.Count).ToString.Trim & ": <" & FileName & ">")

            'Read header
            AllImages(AllImagesIdx) = New AstroNET.Statistics(AIS.DB.IPP)
            AllImages(AllImagesIdx).ResetAllProcessors()
            Dim DataStartPos As Integer = -1
            Dim ThisHeader As New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(FileName, DataStartPos))

            'Load file
            Select Case ThisHeader.BitPix
                Case 8
                    AllImages(AllImagesIdx).DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt8(FileName, AIS.Config.UseIPP)
                Case 16
                    AllImages(AllImagesIdx).DataProcessor_UInt16.ImageData(0).Data = FITSReader.ReadInUInt16(FileName, AIS.Config.UseIPP, AIS.Config.ForceDirect)
                Case 32
                    AllImages(AllImagesIdx).DataProcessor_Int32.ImageData = FITSReader.ReadInInt32(FileName, AIS.Config.UseIPP)
            End Select

            'Init memories
            If Idx = 0 Then
                If Settings.Calc_statistics = True Then
                    ReDim StackingStatistics(AllImages(0).NAXIS1 - 1, AllImages(0).NAXIS2 - 1)
                    For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                        For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                            StackingStatistics(Idx1, Idx2) = New Ato.cSingleValueStatistics(False)
                        Next Idx2
                    Next Idx1
                End If
                ReDim ImageStack_UInt32(AllImages(0).NAXIS1 - 1, AllImages(0).NAXIS2 - 1)
            End If

            'Stack
            For Idx1 As Integer = 0 To AllImages(AllImagesIdx).DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(0)
                For Idx2 As Integer = 0 To AllImages(AllImagesIdx).DataProcessor_UInt16.ImageData(0).Data.GetUpperBound(1)
                    ImageStack_UInt32(Idx1, Idx2) += (AllImages(AllImagesIdx).DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2))
                Next Idx2
            Next Idx1

            'Add up statistics if dimension is matching
            If Settings.Calc_statistics = True Then
                If StackingStatistics.GetUpperBound(0) = AllImages(Idx).NAXIS1 - 1 And StackingStatistics.GetUpperBound(1) = AllImages(Idx).NAXIS2 - 1 Then
                    Select Case ThisHeader.BitPix
                        Case 8, 16
                            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                                    StackingStatistics(Idx1, Idx2).AddValue(AllImages(Idx).DataProcessor_UInt16.ImageData(0).Data(Idx1, Idx2))
                                Next Idx2
                            Next Idx1
                        Case 32
                            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                                    StackingStatistics(Idx1, Idx2).AddValue(AllImages(Idx).DataProcessor_Int32.ImageData(Idx1, Idx2))
                                Next Idx2
                            Next Idx1
                    End Select
                Else
                    Log.Log("!!! Dimension mismatch between the different images!")
                End If
            End If

            Stopper.Stamp(FileNameOnly & ": Stacking")

        Next Idx

        'Load new image
        Dim NewImage(AllImages(0).NAXIS1 - 1, AllImages(0).NAXIS2 - 1) As UInt16
        For Idx1 As Integer = 0 To ImageStack_UInt32.GetUpperBound(0)
            For Idx2 As Integer = 0 To ImageStack_UInt32.GetUpperBound(1)
                NewImage(Idx1, Idx2) = CUShort(ImageStack_UInt32(Idx1, Idx2) / AllFiles.Count)
            Next Idx2
        Next Idx1
        AIS.DB.LastFile_Data.DataProcessor_UInt16.LoadImageData(NewImage)

    End Sub

    Private Sub MaxMinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaxMinToolStripMenuItem.Click

        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Integer
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).MaxMin)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(AIS.DB.MyPath, "Stacking_MaxMin.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Int32)
            Process.Start(FileToGenerate)
        End If

    End Sub

    Private Function StackedStatPresent() As Boolean
        If IsNothing(StackingStatistics) = True Then Return False
        If StackingStatistics.LongLength = 0 Then Return False
        Return True
    End Function

    Private Sub MeanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeanToolStripMenuItem.Click

        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Integer
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).Mean)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(AIS.DB.MyPath, "Stacking_Mean.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Int32)
            Process.Start(FileToGenerate)
        End If
    End Sub

    Private Sub StdDevToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StdDevToolStripMenuItem.Click

        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Integer
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).Sigma)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(AIS.DB.MyPath, "Stacking_StdDev.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Int32)
            Process.Start(FileToGenerate)
        End If

    End Sub

    Private Sub SumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SumToolStripMenuItem.Click

        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Double
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).Mean * StackingStatistics(Idx1, Idx2).ValueCount)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(AIS.DB.MyPath, "Stacking_Sum.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Double)
            Process.Start(FileToGenerate)
        End If

    End Sub

    Private Sub MaxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaxToolStripMenuItem.Click

        If StackedStatPresent() = True Then
            Dim ImageData(StackingStatistics.GetUpperBound(0), StackingStatistics.GetUpperBound(1)) As Integer
            For Idx1 As Integer = 0 To StackingStatistics.GetUpperBound(0)
                For Idx2 As Integer = 0 To StackingStatistics.GetUpperBound(1)
                    ImageData(Idx1, Idx2) = CInt(StackingStatistics(Idx1, Idx2).Maximum)
                Next Idx2
            Next Idx1
            Dim FileToGenerate As String = System.IO.Path.Combine(AIS.DB.MyPath, "Stacking_Mean.fits")
            cFITSWriter.Write(FileToGenerate, ImageData, cFITSWriter.eBitPix.Int32)
            Process.Start(FileToGenerate)
        End If

    End Sub

    Private Sub frmStacking_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Log = New cLog(tbLog, tsslStatus)
    End Sub

    Private Sub tsmiFile_Stack_Click(sender As Object, e As EventArgs) Handles tsmiFile_Stack.Click
        Stack()
    End Sub

End Class