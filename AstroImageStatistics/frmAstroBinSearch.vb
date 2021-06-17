Option Explicit On
Option Strict Off

Public Class frmAstroBinSearch

    Dim API_Key As String = "557ecc514617693189a3b30cae4dcf49388edc3e"
    Dim API_secret As String = "7a095792be040799d35119e9d50c8ffe43811061"

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        LogBox.Log("Started ...")
        Dim RequestContent As New List(Of String)
        RequestContent.Add("http://astrobin.com/api/v1/")
        RequestContent.Add("image/")
        RequestContent.Add("?user=equinoxx")
        'RequestContent.Add("?description__icontains=CDK20")
        'RequestContent.Add("?imaging_cameras__icontains=16803")
        RequestContent.Add("&limit=100")
        RequestContent.Add("&api_key=" & API_Key & "&api_secret=" & API_secret & "&format=json")

        LogBox.Log("Running query ...")
        Dim Request As String = Join(RequestContent.ToArray, String.Empty)
        Dim ErrorInfo As String = String.Empty
        Dim Answer As String = RESTQuery(Request, ErrorInfo)

        Dim jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(Answer)
        Dim AllObjects As Newtonsoft.Json.Linq.JArray = jsonResult.Item("objects")

        Dim Counter As Integer = 0
        LogBox.Log("I have loaded " & AllObjects.Count.ToString.Trim & " elements")

        For Each Entry As Object In AllObjects
            Counter += 1
            LogBox.Log("Loading element " & Counter.ToString.Trim)
            Dim Location As Object = Entry.Item("id")
            Dim NormalImage As Object = Entry.Item("url_regular")
            Dim ImageContent As Byte() = ImageQuery(NormalImage, String.Empty)
            If IsNothing(ImageContent) = False Then
                System.IO.File.WriteAllBytes("previews\AstroBinPreview" & Format(Counter, "0000") & ".jpg", ImageContent)
            End If
        Next Entry

        LogBox.Log("OK!")

    End Sub

    Private Function LoadImage(ByVal Number As String) As Byte()
        Dim ThumbQuery As String = "https://www.astrobin.com/" & Number & "/0/rawthumb/thumb"
        Return ImageQuery(ThumbQuery, String.Empty)
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

    Private Function ImageQuery(ByVal Query As String, ByRef ErrorInfo As String) As Byte()
        Try
            Dim request As Net.HttpWebRequest = DirectCast(Net.WebRequest.Create(Query), Net.HttpWebRequest)        'Create the web request  
            Dim response As Net.HttpWebResponse = DirectCast(request.GetResponse(), Net.HttpWebResponse)            'Get response  
            Dim reader As New IO.BinaryReader(response.GetResponseStream())
            Dim RetVal As Byte() = reader.ReadBytes(1000000)
            ErrorInfo = String.Empty
            Return RetVal
        Catch ex As Exception
            ErrorInfo = ex.Message
            Return Nothing
        End Try
    End Function

End Class
