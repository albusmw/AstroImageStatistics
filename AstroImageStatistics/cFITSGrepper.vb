Option Explicit On
Option Strict On

Public Class cFITSGrepper

    Public Property Progress() As sProgress
        Get
            Return InnerProgress
        End Get
        Set(Value As sProgress)
            InnerProgress = Value
            System.Windows.Forms.Application.DoEvents()
        End Set
    End Property
    Private InnerProgress As sProgress = New sProgress(-1, -1, String.Empty)

    '''<summary>Extension to add for the FITS file search.</summary>
    Public Property FITSFileExtension As String = ".fit|*.fits"

    '''<summary>We do not search for END but read this number of FITS card blocks assuming this is enough.</summary>
    Public Property HeaderBlocks As Integer = 1

    Public Structure sProgress
        Public Current As Integer
        Public Total As Integer
        Public Message As String
        Public Sub New(ByVal NewMessage As String)
            Current = -1
            Total = -1
            Message = NewMessage
        End Sub
        Public Sub New(ByVal NewCurrent As Integer, ByVal NewTotal As Integer, ByVal NewMessage As String)
            Current = NewCurrent
            Total = NewTotal
            Message = NewMessage
        End Sub
    End Structure

    Private WithEvents DirScanner As Ato.RecursivDirScanner

    '''<summary>Report generated during thee grep process.</summary>
    Public Report As New List(Of String)

    '''<summary>Public flag indicating to stop the search.</summary>
    Public StopFlag As Boolean = False

    Public AllFoundKeywordValues As Dictionary(Of eFITSKeywords, List(Of Object))      'keywords found over all search results
    Public NotInAllFiles As List(Of eFITSKeywords)                                     'keywords that are not present in all files
    Public AllFileHeaders As Concurrent.ConcurrentDictionary(Of String, Dictionary(Of eFITSKeywords, Object))

    '''<summary>Run the grep search on the selected folder with the selected filter.</summary>
    '''<param name="RootFolder">Folder to start search in.</param>
    '''<param name="Filter">Filter to apply.</param>
    '''<param name="dgvFiles">DataGridView to fill with the results.</param>
    Public Sub Grep(ByVal RootFolder As String, ByVal DirFilter As String, ByVal FileFilter As String)

        'Init
        Report.Clear()

        '=====================================================================================================================================
        ' Get all files to read the header in
        '=====================================================================================================================================

        'Run search - everything
        ReportSave(New sProgress("Running Everything search ..."))
        Dim AllFoundFiles As New List(Of String)
        AllFoundFiles.AddRange(Everything.GetSearchResult(Chr(34) & RootFolder & Chr(34) & " " & FileFilter & FITSFileExtension))

        'Run "normal" recursive search if no results
        If AllFoundFiles.Count = 0 Then
            ReportSave(New sProgress("Running traditional search ..."))
            DirScanner = New Ato.RecursivDirScanner(RootFolder)
            DirScanner.Scan(DirFilter, FileFilter & ".fit?")
            AllFoundFiles.AddRange(DirScanner.AllFiles)
        End If

        'Report search results and prepare next step
        AllFoundFiles.Sort()
        Report.Add(AllFoundFiles.Count.ToString.Trim & " files found")
        Progress = New sProgress(0, AllFoundFiles.Count + 1, String.Empty)
        If AllFoundFiles.Count = 0 Then Exit Sub

        '=====================================================================================================================================
        ' Read all header bytes from file
        '=====================================================================================================================================

        'Read headers from disc
        ReportSave(New sProgress("Read headers from file"))
        Dim AllHeaders As Concurrent.ConcurrentDictionary(Of String, Byte()) = GetAllHeaders(AllFoundFiles)
        Report.Add(AllHeaders.Count.ToString.Trim & " headers read")
        ReportSave(New sProgress(-1, -1, String.Empty))

        '=====================================================================================================================================
        ' Parse all headers to get all FITS headers
        '=====================================================================================================================================

        'From all headers, get all keywords and all found values for each keyword
        ReportSave(New sProgress("Parse loaded headers"))
        Dim CorruptFiles As Concurrent.ConcurrentBag(Of String) = ParseAllHeaders(AllHeaders)

        Report.Add(AllFileHeaders.Count.ToString.Trim & " headers parsed")
        For Each CorruptFile As String In CorruptFiles
            Report.Add("!! Corrupt file: <" & CorruptFile & ">")
        Next CorruptFile
        ReportSave(New sProgress(-1, -1, String.Empty))

        'Get a list of all keywords found
        AllFoundKeywordValues = New Dictionary(Of eFITSKeywords, List(Of Object))      'keywords found over all search results
        For Each File As String In AllFileHeaders.Keys
            For Each FITSKeyword As eFITSKeywords In AllFileHeaders(File).Keys
                If Not AllFoundKeywordValues.ContainsKey(FITSKeyword) Then AllFoundKeywordValues.Add(FITSKeyword, New List(Of Object))
                Dim KeywordValue As Object = AllFileHeaders(File)(FITSKeyword)
                If AllFoundKeywordValues(FITSKeyword).Contains(KeywordValue) = False Then AllFoundKeywordValues(FITSKeyword).Add(KeywordValue)
            Next FITSKeyword
        Next File
        Report.Add(AllFoundKeywordValues.Count.ToString.Trim & " FITS keywords found in total")

        'Find identical keywords (keyword must be present in each file and must also be the same in each file)
        ReportSave(New sProgress("Finding identical keywords ..."))
        NotInAllFiles = New List(Of eFITSKeywords)
        For Each File As String In AllFileHeaders.Keys
            For Each FITSKeyword As eFITSKeywords In AllFoundKeywordValues.Keys
                If AllFileHeaders(File).ContainsKey(FITSKeyword) = False Then
                    If NotInAllFiles.Contains(FITSKeyword) = False Then NotInAllFiles.Add(FITSKeyword)
                End If
            Next FITSKeyword
        Next File

        'Report keyword summary
        Dim EmptyString As String = "----"
        Dim KeywordSummary As New List(Of String)
        Dim OneForAllFile As String = AllFoundFiles(0)
        Dim HeaderPad As Integer = 22
        For Each Keyword As eFITSKeywords In AllFoundKeywordValues.Keys
            'Value is the same in all files and is also present in all files
            If (AllFoundKeywordValues(Keyword).Count = 1) And (NotInAllFiles.Contains(Keyword) = False) Then
                KeywordSummary.Add("  ALL:".PadRight(HeaderPad) & GetCard(AllFileHeaders(OneForAllFile), Keyword, EmptyString))
            End If
            If (AllFoundKeywordValues(Keyword).Count = 1) And (NotInAllFiles.Contains(Keyword) = True) Then
                KeywordSummary.Add("  ALL (IF PRESENT): ".PadRight(HeaderPad) & GetCard(AllFileHeaders(OneForAllFile), Keyword, EmptyString))
            End If
            If (AllFoundKeywordValues(Keyword).Count > 1) And (NotInAllFiles.Contains(Keyword) = False) Then
                KeywordSummary.Add("  # VALUES: ".PadRight(HeaderPad) & FITSKeyword.KeywordString(Keyword) & ": " & AllFoundKeywordValues(Keyword).Count.ValRegIndep & " values")
            End If
            If (AllFoundKeywordValues(Keyword).Count > 1) And (NotInAllFiles.Contains(Keyword) = False) Then
                KeywordSummary.Add("  # VALUES (IF PRESENT): ".PadRight(HeaderPad) & FITSKeyword.KeywordString(Keyword) & ": " & AllFoundKeywordValues(Keyword).Count.ValRegIndep & " values")
            End If
        Next Keyword
        KeywordSummary.Sort()
        Report.Add("Keyword summary: ")
        Report.AddRange(KeywordSummary)
        ReportSave(New sProgress(-1, -1, String.Empty))

    End Sub

    Private Function ParseAllHeaders(ByVal AllHeaders As Concurrent.ConcurrentDictionary(Of String, Byte())) As Concurrent.ConcurrentBag(Of String)

        Dim CorruptFiles As New Concurrent.ConcurrentBag(Of String)
        AllFileHeaders = New Concurrent.ConcurrentDictionary(Of String, Dictionary(Of eFITSKeywords, Object))
        CorruptFiles = New Concurrent.ConcurrentBag(Of String)
        Dim FilesProcessed As Integer = 0
        Parallel.ForEach(AllHeaders.Keys, Sub(FileName, loopstate)
                                              FilesProcessed += 1
                                              ReportSave(New sProgress(FilesProcessed, AllHeaders.Count, "Parsing <" & FileName & "> parallel"))
                                              Dim DataStartPos As Integer = -1
                                              Dim AllCards As Dictionary(Of eFITSKeywords, Object) = (New cFITSHeaderParser(cFITSHeaderChanger.ParseHeader(AllHeaders(FileName), DataStartPos))).GetCardsAsDictionary
                                              If (AllCards.Count > 0) And (DataStartPos <> -1) Then
                                                  AllFileHeaders.TryAdd(FileName, AllCards)
                                              Else
                                                  CorruptFiles.Add(FileName)
                                              End If
                                          End Sub)
        AllFileHeaders = AllFileHeaders.SortDictionary

        Return CorruptFiles

    End Function

    '''<summary>Build a datatable from the dictionary created.</summary>
    Public Function GetDataTable() As DataTable

        Dim RetVal As New DataTable
        Dim ColumnsToDisplay As New Dictionary(Of eFITSKeywords, Integer)
        Dim ColPtr As Integer = 1                                                                           'we start with 1 as column 0 is the complete file name

        'Check conditions
        If IsNothing(AllFoundKeywordValues) Then Return Nothing

        RetVal.Columns.Add("FileName", GetType(String))
        For Each Keyword As eFITSKeywords In AllFoundKeywordValues.Keys
            If AllFoundKeywordValues(Keyword).Count > 1 Or NotInAllFiles.Contains(Keyword) Then             'if there is more than 1 entry or entries are missing, add
                Dim KeywordString As String = FITSKeyword.KeywordString(Keyword)
                RetVal.Columns.Add(KeywordString, GetKeywordsDataType(AllFoundKeywordValues(Keyword)))      'todo: not all header entries are string ...
                ColumnsToDisplay.Add(Keyword, ColPtr)
                ColPtr += 1
            End If
        Next Keyword

        'Generate all rows (files)
        Dim Ptr As Integer = -1
        For Each FileName As String In AllFileHeaders.Keys
            Dim NewRow(ColumnsToDisplay.Count) As Object
            Ptr += 1
            NewRow(0) = FileName
            For Each Keyword As eFITSKeywords In AllFileHeaders(FileName).Keys
                If ColumnsToDisplay.ContainsKey(Keyword) = True Then
                    Dim ColIdx As Integer = ColumnsToDisplay(Keyword)
                    If AllFileHeaders(FileName).ContainsKey(Keyword) Then
                        NewRow(ColIdx) = AllFileHeaders(FileName)(Keyword)
                    Else
                        NewRow(ColIdx) = "---"
                    End If
                End If
            Next Keyword
            RetVal.Rows.Add(NewRow)
        Next FileName

        Return RetVal

    End Function

    '''<summary>Get the data type of the keyword value list.</summary>
    '''<param name="AllValues">All found values for a certain keyword.</param>
    '''<returns>Type detected - string if multiple types are detected.</returns>
    Private Function GetKeywordsDataType(ByVal AllValues As List(Of Object)) As Type
        Dim DetectedTypes As New List(Of Type)
        For Each Entry As Object In AllValues
            If DetectedTypes.Contains(Entry.GetType) = False Then DetectedTypes.Add(Entry.GetType)
        Next Entry
        If DetectedTypes.Count > 1 Then
            Return GetType(String)
        Else
            Return DetectedTypes(0)
        End If
    End Function

    '''<summary>Get the header bytes for all selected files.</summary>
    '''<param name="AllFoundFiles">List of all files to get the header from.</param>
    '''<returns>Dictionary of files and header bytes.</returns>
    Private Function GetAllHeaders(ByRef AllFoundFiles As List(Of String)) As Concurrent.ConcurrentDictionary(Of String, Byte())
        Dim FileCount As Integer = 0
        Dim NumberOfFiles As Integer = AllFoundFiles.Count
        Dim RetVal As New Concurrent.ConcurrentDictionary(Of String, Byte())
        Parallel.ForEach(AllFoundFiles, Sub(FileName, loopstate)
                                            FileCount += 1
                                            ReportSave(New sProgress(FileCount, NumberOfFiles, "Reading header <" & FileName & ">"))
                                            Dim HeaderBytes((HeaderBlocks * cFITSHeaderChanger.ReadHeaderByteCount) - 1) As Byte
                                            System.IO.File.OpenRead(FileName).Read(HeaderBytes, 0, HeaderBytes.Length)
                                            RetVal.TryAdd(FileName, HeaderBytes)
                                            If StopFlag Then loopstate.Break()
                                        End Sub)
        Return RetVal
    End Function

    Private Function GetCard(ByVal FITSHeader As Dictionary(Of eFITSKeywords, Object), ByVal Keyword As eFITSKeywords, ByVal EmptyString As String) As String
        If FITSHeader.ContainsKey(Keyword) Then
            Return FITSKeyword.KeywordString(Keyword) & "=" & cFITSType.AsString(FITSHeader(Keyword)).Trim
        Else
            Return FITSKeyword.KeywordString(Keyword) & "=" & EmptyString
        End If
    End Function

    Private Sub DirScanner_CurrentlyScanning(DirectoryName As String) Handles DirScanner.CurrentlyScanning
        Progress = New sProgress("Scan <" & DirectoryName & ">")
    End Sub

    Public Sub ReportSave(ByVal value As sProgress)
        Progress = value
    End Sub

End Class
