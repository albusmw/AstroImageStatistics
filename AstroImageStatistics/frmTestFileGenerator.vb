Option Explicit On
Option Strict On
Imports FxResources.System

Public Class frmTestFileGenerator

    Public Class cProp

        Public Enum eTestFiles
            <ComponentModel.Description("BITPIX=8, random")>
            Int8
            <ComponentModel.Description("BITPIX=16, random")>
            UInt16
            <ComponentModel.Description("BITPIX=16, sweep over NAXIS1")>
            UInt16_RowColOrder_1
            <ComponentModel.Description("BITPIX=16, sweep over NAXIS2")>
            UInt16_RowColOrder_2
            <ComponentModel.Description("BITPIX=16, single cross - B/W")>
            UInt16_Cross
            <ComponentModel.Description("BITPIX=16, R-G cross, B line showing down")>
            UInt16_Cross_RGB
            Int32
            Float32
            Float64
            UInt16_Box
            <ComponentModel.Description("BITPIX=16, 1000x64 pixel, value codes pixel position")>
            UInt16_XYCoded
        End Enum

        Private Const Cat1 As String = "1. Test image name and dimensions"
        Private Const Cat2 As String = "2. Value ranges"
        Private Const Cat3 As String = "3. Other settings"

        <ComponentModel.Category(Cat1)>
        <ComponentModel.DisplayName("1. Generation folder")>
        Public Property RootFolder As String = AIS.DB.MyPath

        <ComponentModel.Category(Cat1)>
        <ComponentModel.DisplayName("2. Base file name")>
        Public Property BaseFileName As String = "AsImStatTestImage"

        <ComponentModel.Category(Cat1)>
        <ComponentModel.DisplayName("3. Image width")>
        Public Property DimX As Integer = 1024

        <ComponentModel.Category(Cat1)>
        <ComponentModel.DisplayName("4. Image heigth")>
        Public Property DimY As Integer = 768

        <ComponentModel.Category(Cat2)>
        <ComponentModel.DisplayName("1. Single test file type")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.EnumDesciptionConverter))>
        Public Property Val_SingleTestFile As eTestFiles = eTestFiles.UInt16_Cross

        <ComponentModel.Category(Cat2)>
        <ComponentModel.DisplayName("2. Start value")>
        Public Property Val_Start As Decimal = 1

        <ComponentModel.Category(Cat2)>
        <ComponentModel.DisplayName("3. Step value")>
        Public Property Val_Step As Decimal = 1

        <ComponentModel.Category(Cat2)>
        <ComponentModel.DisplayName("4. Stop value")>
        Public Property Val_Stop As Decimal = 65535

        <ComponentModel.Category(Cat2)>
        <ComponentModel.DisplayName("5. Value 1")>
        Public Property Val_1 As Double = 0.0

        <ComponentModel.Category(Cat2)>
        <ComponentModel.DisplayName("6. Value 2")>
        Public Property Val_2 As Double = 50

        <ComponentModel.Category(Cat2)>
        <ComponentModel.DisplayName("7. Value 5")>
        Public Property Val_3 As Double = 200.0


        <ComponentModel.Category(Cat3)>
        <ComponentModel.DisplayName("1. Open test file after write")>
        <ComponentModel.TypeConverter(GetType(ComponentModelEx.BooleanPropertyConverter_YesNo))>
        Public Property OpenFileAfterWrite As Boolean = True

    End Class

    Public Prop As New cProp

    Private Sub frmTestFileGenerator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pgTestFileConfig.SelectedObject = Prop
    End Sub

    Private Sub tsmiGenerate_SingleFile_Click(sender As Object, e As EventArgs) Handles tsmiGenerate_SingleFile.Click
        Dim TestFileName As String = System.IO.Path.Combine(Prop.RootFolder, Prop.BaseFileName & "." & GetExtension())
        GenerateSingleFile(TestFileName, Prop.Val_SingleTestFile)
        If Prop.OpenFileAfterWrite Then Utils.StartWithItsEXE(TestFileName)
    End Sub

    Private Function GetExtension() As String
        Return ".FITS"
        'Select Case cbTestFileType.SelectedIndex
        '    Case 0, 1 : Return "fits"
        '    Case 2, 3 : Return "tiff"
        '    Case Else : Return "unknown"
        'End Select
    End Function

    Private Function GetTestImageData_Byte() As Byte(,)
        Dim ImageData(Prop.DimX - 1, Prop.DimY - 1) As Byte
        Dim Value As Byte = Byte.MinValue
        For Idx1 As Integer = 0 To ImageData.GetUpperBound(1)
            For Idx2 As Integer = 0 To ImageData.GetUpperBound(0)
                ImageData(Idx2, Idx1) = Value
                If Value < Byte.MaxValue Then Value = Value + CType(1, Byte) Else Value = Byte.MinValue
            Next Idx2
        Next Idx1
        Return ImageData
    End Function

    Private Function GetTestImageData_UInt16() As UInt16(,)
        Dim ImageData(Prop.DimX - 1, Prop.DimY - 1) As UInt16
        Dim Value As UInt16 = UInt16.MinValue
        For Idx1 As Integer = 0 To ImageData.GetUpperBound(1)
            For Idx2 As Integer = 0 To ImageData.GetUpperBound(0)
                ImageData(Idx2, Idx1) = Value
                If Value < UInt16.MaxValue Then Value = Value + CType(1, UInt16) Else Value = UInt16.MinValue
            Next Idx2
        Next Idx1
        Return ImageData
    End Function

    Private Function GetFullFilePath(ByVal FileName As String) As String
        Return System.IO.Path.Combine(Prop.RootFolder, FileName)
    End Function

    Private Sub tsmiFile_Explorer_Click(sender As Object, e As EventArgs) Handles tsmiFile_Explorer.Click
        Utils.StartWithItsEXE(Prop.RootFolder)
    End Sub

    Private Sub tsmiFile_Exit_Click(sender As Object, e As EventArgs) Handles tsmiFile_Exit.Click
        Me.Close()
    End Sub

    Private Sub tsmiGenerate_FITSTestFiles_Click(sender As Object, e As EventArgs) Handles tsmiGenerate_FITSTestFiles.Click
        If cbFile_FITS_BitPix32_Sweep.Checked = True Then GenerateSingleFile(GetFullFilePath("FITS_BitPix32f.FITS"), cProp.eTestFiles.Int32)
        If cbFile_FITS_BitPix32f.Checked = True Then GenerateSingleFile(GetFullFilePath("FITS_BitPix64f.FITS"), cProp.eTestFiles.Float32)
        If cbFile_FITS_BitPix64f.Checked = True Then GenerateSingleFile(GetFullFilePath("FITS_BitPix32_Sweep.FITS"), cProp.eTestFiles.Float64)
        If cbFile_FITS_UInt16_Cross_mono.Checked = True Then GenerateSingleFile(GetFullFilePath("FITS_UInt16_Cross_mono.fits"), cProp.eTestFiles.UInt16_Cross)
        If cbFile_FITS_UInt16_Cross_rgb.Checked = True Then GenerateSingleFile(GetFullFilePath("FITS_UInt16_Cross_rgb.fits"), cProp.eTestFiles.UInt16_Cross_RGB)
        If cbFile_FITS_UInt16_RowColOrder.Checked = True Then GenerateSingleFile(GetFullFilePath("FITS_UInt16_RowColOrder_Row.fits"), cProp.eTestFiles.UInt16_RowColOrder_1)
        If cbFile_FITS_UInt16_RowColOrder.Checked = True Then GenerateSingleFile(GetFullFilePath("FITS_UInt16_RowColOrder_Col.fits"), cProp.eTestFiles.UInt16_RowColOrder_2)
    End Sub

    Private Sub GenerateSingleFile(ByVal FileName As String, ByVal Type As cProp.eTestFiles)
        Select Case Type
            Case cProp.eTestFiles.Float32
                cFITSWriter.WriteTestFile.Float32(FileName, Prop.DimX, Prop.DimY, Prop.Val_Start, Prop.Val_Step, Prop.Val_Stop)
            Case cProp.eTestFiles.Float64
                cFITSWriter.WriteTestFile.Float64(FileName, Prop.DimX, Prop.DimY, Prop.Val_Start, Prop.Val_Step, Prop.Val_Stop)
            Case cProp.eTestFiles.Int32
                cFITSWriter.WriteTestFile.Int32(FileName, Prop.DimX, Prop.DimY, CInt(Prop.Val_Start), CInt(Prop.Val_Stop))
            Case cProp.eTestFiles.Int8

            Case cProp.eTestFiles.UInt16

            Case cProp.eTestFiles.UInt16_Box
                cFITSWriter.WriteTestFile.UInt16_Box(FileName, CType(Prop.Val_1, UInt16), CType(Prop.Val_2, UInt16))
            Case cProp.eTestFiles.UInt16_Cross
                cFITSWriter.WriteTestFile.UInt16_Cross(FileName, Prop.DimX, Prop.DimY, CType(Prop.Val_1, UInt16), CType(Prop.Val_2, UInt16), CType(Prop.Val_3, UInt16))
            Case cProp.eTestFiles.UInt16_Cross_RGB
                cFITSWriter.WriteTestFile.UInt16_Cross_RGB(FileName, Prop.DimX, Prop.DimY)
            Case cProp.eTestFiles.UInt16_RowColOrder_1
                cFITSWriter.WriteTestFile.UInt16_RowColOrder(FileName, Prop.DimX, True)
            Case cProp.eTestFiles.UInt16_RowColOrder_2
                cFITSWriter.WriteTestFile.UInt16_RowColOrder(FileName, Prop.DimX, False)
            Case cProp.eTestFiles.UInt16_XYCoded
                cFITSWriter.WriteTestFile.UInt16_XYCoded(FileName)
        End Select
    End Sub

End Class