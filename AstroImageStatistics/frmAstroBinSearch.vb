Option Explicit On
Option Strict Off
Imports AstroImageStatistics.Ato

Public Class frmAstroBinSearch

    Dim AstroBin As cAstroBin

    Dim LB As cLogTextBox
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    Public ReadOnly Property PreviewFolder As String = System.IO.Path.Combine(MyPath, "previews")

    Private Sub frmAstroBinSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LB = New cLogTextBox(tbLog)
        AstroBin = New cAstroBin("557ecc514617693189a3b30cae4dcf49388edc3e", "7a095792be040799d35119e9d50c8ffe43811061")
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        LB.Clear()

        'Compose the query
        Log("Started ...")
        AstroBin.ResultCountLimit = tbLimit.Text.ValRegIndep
        Dim Parameters As New Dictionary(Of cAstroBin.eFilterOptions, String)
        If cbFilter_User.Checked = True Then Parameters.Add(cAstroBin.eFilterOptions.user, tbFilter_User.Text)
        If cbFilter_TitleContains.Checked = True Then Parameters.Add(cAstroBin.eFilterOptions.title__icontains, tbFilter_TitleContains.Text)
        If cbFilter_DescriptionContains.Checked = True Then Parameters.Add(cAstroBin.eFilterOptions.description__icontains, tbFilter_DescriptionContains.Text)

        Log("Running query ...")
        Dim Request As String = AstroBin.GetQueryURL(Parameters)
        tbURL.Text = Request
        Dim ErrorInfo As String = String.Empty
        Dim Answer As String = RESTQuery(Request, ErrorInfo)

        If IsNothing(Answer) = True Then
            Log("No results ...")
            Exit Sub
        End If

        Dim jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(Answer)
        Dim AllObjects As Newtonsoft.Json.Linq.JArray = jsonResult.Item("objects")

        Dim Counter As Integer = 0
        Log("I have loaded " & AllObjects.Count.ToString.Trim & " elements")

        For Each Entry As Newtonsoft.Json.Linq.JToken In AllObjects
            Counter += 1
            Log("Loading element " & Counter.ToString.Trim)
            For Each Element As Newtonsoft.Json.Linq.JToken In Entry
                Log("  " & Element.ToString)
            Next Element

            Dim Location As Object = Entry.Item("id")
                Dim NormalImage As Object = Entry.Item("url_regular")
                Dim ContentType As String = String.Empty
                Dim ImageContent As Byte() = ImageQuery(NormalImage, ContentType, String.Empty)
                If IsNothing(ImageContent) = False Then
                    If System.IO.Directory.Exists(PreviewFolder) = False Then System.IO.Directory.CreateDirectory(PreviewFolder)
                    Dim FileType As String = ContentType.Split("/").Last
                    Dim FileName As String = System.IO.Path.Combine(PreviewFolder, "AstroBinPreview" & Format(Counter, "0000") & "." & FileType)
                    System.IO.File.WriteAllBytes(FileName, ImageContent)
                End If
            Next Entry

            Log("DONE")

    End Sub

    Private Function LoadImage(ByVal Number As String, ByRef ContentType As String) As Byte()
        Dim ThumbQuery As String = "https://www.astrobin.com/" & Number & "/0/rawthumb/thumb"
        Return ImageQuery(ThumbQuery, ContentType, String.Empty)
    End Function

    Private Function RESTQuery(ByVal Query As String, ByRef ErrorInfo As String) As String
        Try
            Dim request As Net.HttpWebRequest = DirectCast(Net.WebRequest.Create(Query), Net.HttpWebRequest)        'Create the web request  
            Dim response As Net.HttpWebResponse = DirectCast(request.GetResponse(), Net.HttpWebResponse)            'Get response  
            Dim reader As New IO.StreamReader(response.GetResponseStream())
            Dim RetVal As String = reader.ReadToEnd
            ErrorInfo = String.Empty
            Return RetVal
        Catch ex As Exception
            ErrorInfo = ex.Message
            Return Nothing
        End Try
    End Function

    Private Function ImageQuery(ByVal Query As String, ByRef ContentType As String, ByRef ErrorInfo As String) As Byte()
        Try
            Dim request As Net.HttpWebRequest = DirectCast(Net.WebRequest.Create(Query), Net.HttpWebRequest)        'Create the web request  
            Dim response As Net.HttpWebResponse = DirectCast(request.GetResponse(), Net.HttpWebResponse)            'Get response  
            Dim reader As New IO.BinaryReader(response.GetResponseStream())
            Dim RetVal As Byte() = reader.ReadBytes(1000000)
            ErrorInfo = String.Empty
            ContentType = response.ContentType
            Return RetVal
        Catch ex As Exception
            ContentType = String.Empty
            ErrorInfo = ex.Message
            Return Nothing
        End Try
    End Function

    Private Sub Log(ByVal Text As String)
        LB.Log(Text)
        DE()
    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub btnQueryToClipboard_Click(sender As Object, e As EventArgs) Handles btnQueryToClipboard.Click
        Clipboard.Clear()
        Clipboard.SetText(tbURL.Text)
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Utils.StartWithItsEXE(PreviewFolder)
    End Sub

End Class
