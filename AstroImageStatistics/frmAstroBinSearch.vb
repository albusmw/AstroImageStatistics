﻿Option Explicit On
Option Strict Off

Public Class frmAstroBinSearch

    Dim API_Key As String = "557ecc514617693189a3b30cae4dcf49388edc3e"
    Dim API_secret As String = "7a095792be040799d35119e9d50c8ffe43811061"
    Dim LB As cLogTextBox
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    Public ReadOnly Property PreviewFolder As String = System.IO.Path.Combine(MyPath, "previews")

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        LB.Clear

        'Compose the query
        Log("Started ...")
        Dim RequestContent As New List(Of String)
        RequestContent.Add("http://astrobin.com/api/v1/")
        RequestContent.Add("image/")
        Dim Filters As New List(Of String)
        If cbFilter_User.Checked = True Then Filters.Add("user=" & tbFilter_User.Text)
        If cbFilter_TitleContains.Checked = True Then Filters.Add("title__icontains=" & tbFilter_TitleContains.Text)
        If cbFilter_DescriptionContains.Checked = True Then Filters.Add("description__icontains=" & tbFilter_DescriptionContains.Text)
        RequestContent.Add("?" & Join(Filters.ToArray, "&"))
        'RequestContent.Add("?imaging_cameras__icontains=16803")
        RequestContent.Add("&limit=" & tbLimit.Text)
        RequestContent.Add("&api_key=" & API_Key & "&api_secret=" & API_secret & "&format=json")

        Log("Running query ...")
        Dim Request As String = Join(RequestContent.ToArray, String.Empty)
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

    Private Sub frmAstroBinSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LB = New cLogTextBox(tbLog)
    End Sub

    Private Sub DE()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub btnQueryToClipboard_Click(sender As Object, e As EventArgs) Handles btnQueryToClipboard.Click
        Clipboard.Clear()
        Clipboard.SetText(tbURL.Text)
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Process.Start(PreviewFolder)
    End Sub

End Class
