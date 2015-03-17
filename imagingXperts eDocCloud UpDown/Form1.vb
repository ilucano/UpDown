Imports System.Net
Imports System.IO
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports NUnit.Framework

Public Class frmMain


    'Dim conn As MySqlConnection
    Const cnStr As String = "server=nlapi.edoccloud.com;user id=shaman;password=pampita1280;database=ilucano_edoccloud"
    Dim fUserID As String = "ftpuser"
    Dim fPassword As String = "Zr;:F+7.9gm=D+m"
    Private Const ERR_CODE As String = "-1"

    ' Autenticacion
    Private eToken As String

    Private m_OldSelectNode As TreeNode

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'SWITCH TO API: OK
        Dim lngRC As Long
        lngRC = WritePrivateProfileString("Download", "DownloadPath", txtFolder.Text, "appinfo.ini")
    End Sub
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SWITCH TO API: OK
        Dim rcl As Integer
        Dim strWork As String

        strWork = New String(Chr(0), 200)

        'Pongo el Output
        rcl = GetPrivateProfileString("Download", "DownloadPath", "C:\TEST", strWork, 200, "appinfo.ini")
        If rcl > 0 Then
            txtFolder.Text = strWork.Substring(0, rcl)
        Else
            txtFolder.Text = "C:\TEST"
        End If

        Dim oAu As New oAuth.Authenticate
        eToken = oAu.GetToken("caringpeople", "test123", "36f13093ac392f9e92d8376d0542ca70")

        RefreshList()
        RefreshListWF()
    End Sub
    Private Sub RefreshList()
        'SWITCH TO API: OK
        tView.Nodes.Clear()

        Dim oC As New oCompanies.Company
        Dim sRet() As oCompanies.CompanyType

        oC.GetCompanies(eToken)

        sRet = oC.GetCompanies(eToken)
        If sRet(0).row_id <> ERR_CODE Then
            For i = 0 To (sRet.Count - 1)
                Dim li As New TreeNode
                li.ImageIndex = 0
                li.SelectedImageIndex = 0
                li.Tag = sRet(i).row_id
                li.Text = sRet(i).company_name
                tView.Nodes.Add(li)
                GetOrders(li)
            Next
        End If

    End Sub
    Private Sub RefreshListWF()
        lView.Items.Clear()

        Dim oP As New oPickup.Pickup
        Dim sRet() As oPickup.BoxData

        sRet = oP.GetBoxbyStatus("12", eToken)

        If sRet(0).row_id <> ERR_CODE Then
            For i = 0 To (sRet.Count - 1)
                Dim li As New ListViewItem
                'li.ImageIndex = 0
                li.Tag = sRet(i).row_id
                li.Text = sRet(i).wf_id
                lView.Items.Add(li)
            Next
        End If

    End Sub
    Private Sub GetOrders(pli As TreeNode)
        'SWITCH TO API: OK
        Try
            Dim oOb As New oObjects.Orders
            Dim sRet() As oObjects.ObjectData

            sRet = oOb.GetOrders(pli.Tag.ToString, eToken)
            If sRet(0).row_id <> ERR_CODE Then
                For i = 0 To (sRet.Count - 1)
                    Dim li As New TreeNode
                    li.ImageIndex = 1
                    li.SelectedImageIndex = 1
                    li.Tag = sRet(i).row_id
                    li.Text = sRet(i).f_name
                    pli.Nodes.Add(li)
                    GetBoxes(li)
                Next
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub GetBoxes(pli As TreeNode)
        'SWITCH TO API: OK
        Try
            Dim oOb As New oObjects.Orders
            Dim sRet() As oObjects.ObjectData

            sRet = oOb.GetBoxes(pli.Tag.ToString, eToken)
            If sRet(0).row_id <> ERR_CODE Then
                For i = 0 To (sRet.Count - 1)
                    Dim li As New TreeNode
                    li.ImageIndex = 2
                    li.SelectedImageIndex = 2
                    li.Tag = sRet(i).row_id
                    li.Text = sRet(i).f_code & " - " & sRet(i).f_name  ' & " (" & reader.GetString(4) & ")"
                    pli.Nodes.Add(li)
                    'GetOrders(li)
                Next
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub btnRefreshList_Click_1(sender As Object, e As EventArgs) Handles btnRefreshList.Click
        'SWITCH TO API: OK
        RefreshList()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        'SWITCH TO API: OK
        ' Primero Verifico si tiene Algo
        Dim node As TreeNode = tView.SelectedNode

        DownloadBox(node)
    End Sub
    Private Sub tView_MouseUp(sender As Object, e As MouseEventArgs) Handles tView.MouseUp
        'SWITCH TO API: OK
        If e.Button = MouseButtons.Right Then

            ' Point where mouse is clicked
            Dim p As Point = New Point(e.X, e.Y)

            ' Go to the node that the user clicked
            Dim node As TreeNode = tView.GetNodeAt(p)
            If Not node Is Nothing Then

                ' Highlight the node that the user clicked.
                ' The node is highlighted until the Menu is displayed on the screen
                'm_OldSelectNode = tView.SelectedNode
                tView.SelectedNode = node

                ' Find the appropriate ContextMenu based on the highlighted node
                Select Case node.Level
                    Case 2
                        cntMnu.Show(tView, p)
                End Select

                ' Highlight the selected node
                'tView.SelectedNode = m_OldSelectNode
                'm_OldSelectNode = Nothing

            End If
        End If

    End Sub
    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        'SWITCH TO API: OK
        If Not tView.SelectedNode Is Nothing Then
            If tView.SelectedNode.Level = 2 Then
                DownloadBox(tView.SelectedNode)
            End If
        End If
    End Sub
    Public Sub DownloadBox(node As TreeNode)
        'SWITCH TO API: OK
        If My.Computer.FileSystem.DirectoryExists(txtFolder.Text) Then
            DisableForm()
            Try
                Dim oOb As New oObjects.Orders
                Dim sRet() As oObjects.ObjectData

                sRet = oOb.GetCharts(node.Tag.ToString, eToken)
                If sRet(0).row_id <> ERR_CODE Then
                    For i = 0 To (sRet.Count - 1)
                        ' Tiene Chars, los bajo
                        ' Primero creo la carpeta de Box
                        Dim strBox As String = txtFolder.Text & "\" & node.Tag & " - " & node.Text
                        If Not My.Computer.FileSystem.DirectoryExists(strBox) Then
                            My.Computer.FileSystem.CreateDirectory(strBox)
                        End If

                        ' Ahora creo la carpeta de Char
                        Dim strChart As String = strBox & "\"
                        If IsDBNull(sRet(i).f_code) Or sRet(i).f_code = "" Then
                            strChart = strBox & "\" & sRet(i).f_name
                        Else
                            strChart = strBox & "\" & sRet(i).f_code & " - " & sRet(i).f_name
                        End If
                        If Not My.Computer.FileSystem.DirectoryExists(strChart) Then
                            My.Computer.FileSystem.CreateDirectory(strChart)
                        End If

                        DownloadChars(sRet(i).row_id, strChart)

                    Next
                End If

                EnableForm()
                MessageBox.Show("Box Downloadaed Correctly", "imagingXperts LLC", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                EnableForm()
                MessageBox.Show(ex.Message, "imagingXperts LLC", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Public Sub DisableForm()
        'SWITCH TO API: OK
        ToolStrip1.Enabled = False
        tView.Enabled = False
        tabGen.Enabled = False
    End Sub
    Public Sub EnableForm()
        'SWITCH TO API: OK
        ToolStrip1.Enabled = True
        tView.Enabled = True
        tabGen.Enabled = True
    End Sub
    Public Sub DownloadChars(rowId As Integer, strChart As String)
        'SWITCH TO API: OK
        Try
            Dim oOb As New oFiles.Files
            Dim sRet() As oFiles.FilesData

            sRet = oOb.GetFiles(rowId.ToString, eToken)
            If sRet(0).row_id <> ERR_CODE Then
                For i = 0 To (sRet.Count - 1)
                    If My.Computer.FileSystem.FileExists(strChart & "\" & sRet(i).filename) Then

                    Else
                        DownloadFile(strChart, sRet(i).filename, "" & sRet(0).path)
                    End If
                Next
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        'SWITCH TO API: OK
        fldBrowse.SelectedPath = txtFolder.Text
        If fldBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtFolder.Text = fldBrowse.SelectedPath
        End If
    End Sub
    Private Sub DownloadFile(ByVal SaveFilePath As String, ByVal SaveFileName As String, ByVal RemoteFile As String)

        If ftpCli.IsConnected = False Then
            ftpCli.ServerAddress = "ftp.edoccloud.com"
            ftpCli.UserName = fUserID
            ftpCli.Password = fPassword
            ftpCli.LogLevel = EnterpriseDT.Util.Debug.LogLevel.Debug
            ftpCli.LogToConsole = True
            ftpCli.ConnectMode = EnterpriseDT.Net.Ftp.FTPConnectMode.PASV
            ftpCli.Connect()
        End If
        

        Try
            
            Dim strPath As String = Uri.EscapeUriString("ftp://ftp.edoccloud.com/opt/eDocCloud/files/" + RemoteFile)
            'Dim strPath As String = "ftp://ftp.edoccloud.com/opt/eDocCloud/files/" + RemoteFile
            Dim strFile As String = Path.GetFileName(RemoteFile)
            Dim strFolder As String = RemoteFile.Replace(strFile, "")
            If My.Computer.FileSystem.FileExists(SaveFilePath & "\" & strFile) = False Then
                ftpCli.ChangeWorkingDirectory("/opt/eDocCloud/files/" & strFolder)
                ftpCli.DownloadFile((SaveFilePath & "\").Replace("\", "\\"), strFile)
            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public NotInheritable Class FTPSettings
        Private Sub New()
        End Sub
        Public Shared Property IP() As String
            Get
                Return m_IP
            End Get
            Set(ByVal value As String)
                m_IP = Value
            End Set
        End Property
        Private Shared m_IP As String
        Public Shared Property UserID() As String
            Get
                Return m_UserID
            End Get
            Set(ByVal value As String)
                m_UserID = Value
            End Set
        End Property
        Private Shared m_UserID As String
        Public Shared Property Password() As String
            Get
                Return m_Password
            End Get
            Set(ByVal value As String)
                m_Password = Value
            End Set
        End Property
        Private Shared m_Password As String
    End Class

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        'SWITCH TO API: OK
        RefreshListWF()
    End Sub
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        ' Primero Busco la Caja la obtengo del WF
        If lView.SelectedItems.Count > 0 Then
            fldBrowse.SelectedPath = "C:\"
            If fldBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If My.Computer.FileSystem.DirectoryExists(fldBrowse.SelectedPath) Then
                    UploadBox(lView.SelectedItems(0).Text, fldBrowse.SelectedPath)


                    Dim cnStr As String = "server=nlapi.edoccloud.com;user id=shaman;password=pampita1280;database=ilucano_edoccloud"
                    Dim strRet As String = "-1"

                    Try
                        Dim conn As MySqlConnection
                        conn = New MySqlConnection()
                        conn.ConnectionString = cnStr
                        Try
                            conn.Open()
                        Catch myerror As MySqlException
                            MsgBox("Connection to the Database Failed")
                            GoTo a
                        End Try

                        Dim query As String = "UPDATE workflow SET fk_status = 15 WHERE wf_id = '" & lView.SelectedItems(0).Text & "'"

                        Dim connection As New MySqlConnection(cnStr)
                        Dim cmd As New MySqlCommand(query, connection)
                        connection.Open()
                        cmd.ExecuteNonQuery()

                        conn.Close()

                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

a:

                    MessageBox.Show("Box Uploaded Correctly", "imagingXperts LLC", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    prg.Value = 0
                Else
                    MessageBox.Show("Directory is incorrect", "imagingXperts LLC", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Else
            MessageBox.Show("No item selected", "imagingXperts LLC", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Public Sub UploadBox(wfId As String, dirFold As String)
        'SWITCH TO API: OK
        Dim oP As New oPickup.Pickup
        Dim sRet() As oPickup.PickupData

        sRet = oP.GetPickupbyBcd(wfId, eToken)

        If sRet(0).row_id <> ERR_CODE Then
            For i = 0 To (sRet.Count - 1)
                FindCharts(sRet(i).fk_box, dirFold, sRet(i).fk_company)
            Next
        End If

    End Sub
    Private Sub FindCharts(boxId As String, strFolder As String, strCompany As Integer)
        'SWITCH TO API: OK
        ' Cargo todos los directorios de la carpeta en el objects
        Dim di As New DirectoryInfo(strFolder)
        ' Get a reference to each file in that directory.
        Dim fiArr As DirectoryInfo() = di.GetDirectories
        ' Display the names of the files.
        Dim fri As DirectoryInfo

        prg.Minimum = 0
        prg.Maximum = fiArr.Count

        Dim strText1 As String = "/arc/"
        Dim strYear As String = Now.Year.ToString
        Dim miFolder As String

        miFolder = "/opt/eDocCloud/files" & strText1 & strYear & _
            "/" & strCompany.ToString & "/" & boxId.ToString

        ftpCli.ServerAddress = "www.edoccloud.com"
        ftpCli.UserName = fUserID
        ftpCli.Password = fPassword
        ftpCli.Connect()

        Try
            ftpCli.CreateDirectory(miFolder)
        Catch ex As Exception

        End Try

        ftpCli.ServerDirectory = miFolder

        For Each fri In fiArr
            prg.Increment(1)
            Application.DoEvents()
            If Not (My.Computer.FileSystem.FileExists(fri.FullName & "\OK")) Then
                Dim intChart As String = CreateChart(strCompany, fri.Name, boxId)
                If GetEachFile(fri.FullName, strCompany, boxId, intChart) Then
                    File.Create(fri.FullName & "\OK").Dispose()
                End If
            End If
        Next fri

        ftpCli.Close()
        ftpCli.Dispose()
    End Sub
    Private Function CreateChart(strCompany As Integer, strFolder As String, boxId As String) As String
        Dim retVal As String = "-1"
        Try
            Dim query As String = "INSERT INTO objects (fk_obj_type, fk_company, f_name, fk_parent" & _
                ", creation, fk_status) VALUES (3,'" & _
                strCompany & "','" & strFolder & "'," & boxId & _
                ",'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "',5)"

            Dim conn As New MySqlConnection(cnStr)
            Dim cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed. Chart Creation")
            End Try
            cmd.ExecuteNonQuery()

            cmd.CommandText = "SELECT  LAST_INSERT_ID()   FROM objects"
            retVal = cmd.ExecuteScalar()
            conn.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return retVal
    End Function
    Private Sub CreateFile(strFile As String, intEmpresa As Integer, intParent As Integer, strPath As String, strChart As String)
        Dim intPages As Integer
        Dim intFileSize As Long
        Dim strVersion, strRemotePath, strOtro As String
        Try
            Dim oReader As New iTextSharp.text.pdf.PdfReader(strPath & "\" & strFile)
            Dim sOut = ""
            intPages = oReader.NumberOfPages
            intFileSize = oReader.FileLength
            strVersion = oReader.PdfVersion

            'arc/2014/3/838/1321/
            ' arc <year> <company> <order> <parent>
            Dim strText1 As String = "/arc/"
            Dim strText2 As String = "arc/"
            Dim strYear As String = Now.Year.ToString
            Dim miFolder As String

            miFolder = "/opt/eDocCloud/files" & strText1 & strYear & _
                "/" & intEmpresa.ToString & "/" & intParent.ToString & "/" & strChart
            strRemotePath = miFolder & "/" & strFile
            strOtro = strText2 & strYear & _
                "/" & intEmpresa.ToString & "/" & intParent.ToString & "/" & strChart & "/" & strFile

            If UploadFile(strPath & "\" & strFile, strRemotePath, miFolder) Then

                For i = 1 To intPages
                    Dim its As New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy

                    Try
                        sOut &= iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(oReader, i, its)
                    Catch ex As Exception

                    End Try

                Next

                Dim query As String = "INSERT INTO files (filename, creadate, moddate, pages" & _
                    ", filesize, pdf_version, fk_empresa, parent_id, texto, path) VALUES (@filename, @creadate," & _
                    "@moddate, @pages, @filesize, @pdf_version, @fk_empresa, @parent_id, @texto, @path)"

                Dim conn As New MySqlConnection(cnStr)
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@filename", strFile)
                cmd.Parameters.AddWithValue("@creadate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("@moddate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("@pages", intPages)
                cmd.Parameters.AddWithValue("@filesize", intFileSize)
                cmd.Parameters.AddWithValue("@pdf_version", strVersion)
                cmd.Parameters.AddWithValue("@fk_empresa", intEmpresa)
                cmd.Parameters.AddWithValue("@parent_id", strChart)
                cmd.Parameters.AddWithValue("@texto", sOut)
                strRemotePath = strRemotePath.Replace("ftp://www.edoccloud.com//opt/eDocCloud/files/", "")
                cmd.Parameters.AddWithValue("@path", strOtro)
                Try
                    conn.Open()
                Catch myerror As MySqlException
                    MsgBox("Connection to the Database Failed. File Creation")
                End Try
                cmd.ExecuteNonQuery()
                conn.Close()
            Else
                MsgBox("Connection to the Database Failed. Upload")
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            ' ftpCli.Close()
        End Try
    End Sub
    Private Function GetEachFile(strDir As String, intEmpresa As Integer, intParent As Integer, strChart As String) As Boolean
        Dim strRet As Boolean = False

        ' Cargo todos los documentos del directorio
        Dim di As New DirectoryInfo(strDir)
        ' Get a reference to each file in that directory.
        Dim fiArr As FileInfo() = di.GetFiles()
        ' Display the names of the files.
        Dim fri As FileInfo
        Dim intCnt As Integer = 0

        For Each fri In fiArr
            If Not (My.Computer.FileSystem.FileExists(fri.FullName & "\OK")) Then
                If intCnt = 0 Then
                    Dim strText1 As String = "/arc/"
                    Dim strYear As String = Now.Year.ToString
                    Dim miFolder As String

                    miFolder = "ftp://www.edoccloud.com//opt/eDocCloud/files" & strText1 & strYear & _
                        "/" & intEmpresa.ToString & "/" & intParent.ToString & "/" & strChart

                    Dim FTPReq As System.Net.FtpWebRequest = CType(WebRequest.Create(miFolder), FtpWebRequest)
                    FTPReq.Credentials = New NetworkCredential(fUserID, fPassword)
                    FTPReq.Method = WebRequestMethods.Ftp.MakeDirectory

                    Dim FTPRes As FtpWebResponse
                    Try
                        FTPRes = CType(FTPReq.GetResponse, FtpWebResponse)
                    Catch ex As Exception

                    End Try
                    intCnt = 1
                End If
                CreateFile(fri.Name, intEmpresa, intParent, fri.DirectoryName, strChart)
            End If
        Next fri
        strRet = True

        Return strRet
    End Function
    Public Shared Function GetTextFromPDF(PdfFileName As String) As String
        'SWITCH TO API: OK
        Dim oReader As New iTextSharp.text.pdf.PdfReader(PdfFileName)

        Dim sOut = ""

        For i = 1 To oReader.NumberOfPages
            Dim its As New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy

            sOut &= iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(oReader, i, its)
        Next

        Return sOut
    End Function
    Private Function UploadFile(ByVal LocalFileName As String, ByVal miUri As String, miFolder As String) As Boolean
        'SWITCH TO API: OK
        Dim strRet As Boolean = False

        'Dim miRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(miUri), System.Net.FtpWebRequest)
        'miRequest.Credentials = New Net.NetworkCredential(fUserID, fPassword)
        'miRequest.Method = Net.WebRequestMethods.Ftp.UploadFile

        Try
            ftpCli.UploadFile(LocalFileName, miUri)
            'ftpCli.ConnectMode = EnterpriseDT.Net.Ftp.FTPConnectMode.ACTIVE
            ''My.Computer.Network.UploadFile(LocalFileName, miUri, FTPSettings.UserID, FTPSettings.Password)
            'Dim bFile() As Byte = System.IO.File.ReadAllBytes(LocalFileName)
            Application.DoEvents()
            'Dim miStream As System.IO.Stream = miRequest.GetRequestStream()
            'miStream.Write(bFile, 0, bFile.Length)
            'miStream.Close()
            'miStream.Dispose()
            strRet = True
        Catch ex As Exception
            Try
                ftpCli.UploadFile(LocalFileName, miUri)
                strRet = True
            Catch ex2 As Exception
                strRet = False
            End Try

        End Try
        Return strRet
    End Function

End Class


