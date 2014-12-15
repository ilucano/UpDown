Imports System.Net
Imports System.IO
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser

Public Class frmMain

    'Dim conn As MySqlConnection
    Const cnStr As String = "server=www.edoccloud.com;user id=shaman;password=pampita1280;database=ilucano_edoccloud"
    Private m_OldSelectNode As TreeNode

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim lngRC As Long
        lngRC = WritePrivateProfileString("Download", "DownloadPath", txtFolder.Text, "appinfo.ini")
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        RefreshList()
        RefreshListWF()
    End Sub
    Private Sub RefreshList()
        tView.Nodes.Clear()

        Try
            
            Dim query As String = "SELECT * FROM companies"

            Dim conn As New MySqlConnection(cnStr)
            Dim cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed")
            End Try
            Dim reader As MySqlDataReader
            reader = cmd.ExecuteReader()

            While reader.Read()
                Dim li As New TreeNode
                li.ImageIndex = 0
                li.SelectedImageIndex = 0
                li.Tag = reader.GetValue(0)
                li.Text = reader.GetString(1)
                tView.Nodes.Add(li)
                GetOrders(li)
            End While

            conn.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub
    Private Sub RefreshListWF()
        lView.Items.Clear()

        Try

            Dim query As String = "SELECT * FROM workflow WHERE fk_status = 14"

            Dim conn As New MySqlConnection(cnStr)
            Dim cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed")
            End Try
            Dim reader As MySqlDataReader
            reader = cmd.ExecuteReader()

            While reader.Read()
                Dim li As New ListViewItem
                'li.ImageIndex = 0
                li.Tag = reader.GetValue(0)
                li.Text = reader.GetString(1)
                lView.Items.Add(li)
            End While

            conn.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub
    Private Sub GetOrders(pli As TreeNode)
        Try
            Dim query As String = "SELECT * FROM objects WHERE fk_obj_type = 1 AND fk_company = " & pli.Tag

            Dim conn As New MySqlConnection(cnStr)
            Dim cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed")
            End Try
            Dim reader As MySqlDataReader
            reader = cmd.ExecuteReader()

            While reader.Read()
                Dim li As New TreeNode
                li.ImageIndex = 1
                li.SelectedImageIndex = 1
                li.Tag = reader.GetValue(0)
                li.Text = reader.GetString(4)
                pli.Nodes.Add(li)
                GetBoxes(li)
            End While

            conn.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub GetBoxes(pli As TreeNode)
        Try
            Dim query As String = "SELECT * FROM objects WHERE fk_obj_type = 2 AND fk_parent = " & pli.Tag & " ORDER BY f_code ASC"

            Dim conn As New MySqlConnection(cnStr)
            Dim cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed")
            End Try
            Dim reader As MySqlDataReader
            reader = cmd.ExecuteReader()

            While reader.Read()
                Dim li As New TreeNode
                li.ImageIndex = 2
                li.SelectedImageIndex = 2
                li.Tag = reader.GetValue(0)
                li.Text = reader.GetString(3) & " - " & reader.GetString(4) ' & " (" & reader.GetString(4) & ")"
                pli.Nodes.Add(li)
                'GetOrders(li)
            End While

            conn.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub btnRefreshList_Click_1(sender As Object, e As EventArgs) Handles btnRefreshList.Click
        RefreshList()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        ' Primero Verifico si tiene Algo
        Dim node As TreeNode = tView.SelectedNode

        DownloadBox(node)
    End Sub
    Private Sub tView_MouseUp(sender As Object, e As MouseEventArgs) Handles tView.MouseUp
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
        If Not tView.SelectedNode Is Nothing Then
            If tView.SelectedNode.Level = 2 Then
                DownloadBox(tView.SelectedNode)
            End If
        End If
    End Sub
    Public Sub DownloadBox(node As TreeNode)
        If My.Computer.FileSystem.DirectoryExists(txtFolder.Text) Then
            Try
                Dim query As String = "SELECT * FROM objects WHERE fk_obj_type = 3 AND fk_parent = " & node.Tag & " ORDER BY f_code ASC"

                Dim conn As New MySqlConnection(cnStr)
                Dim cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                Catch myerror As MySqlException
                    MsgBox("Connection to the Database Failed")
                End Try
                Dim reader As MySqlDataReader
                reader = cmd.ExecuteReader()

                While reader.Read()
                    ' Tiene Chars, los bajo
                    ' Primero creo la carpeta de Box
                    Dim strBox As String = txtFolder.Text & "\" & node.Tag & " - " & node.Text
                    If Not My.Computer.FileSystem.DirectoryExists(strBox) Then
                        My.Computer.FileSystem.CreateDirectory(strBox)
                    End If

                    ' Ahora creo la carpeta de Char
                    Dim strChart As String = strBox & "\"
                    If IsDBNull(reader.GetValue(3)) Or reader.GetValue(3) = "" Then
                        strChart = strBox & "\" & reader.GetValue(4)
                    Else
                        strChart = strBox & "\" & reader.GetValue(3) & " - " & reader.GetValue(4)
                    End If
                    If Not My.Computer.FileSystem.DirectoryExists(strChart) Then
                        My.Computer.FileSystem.CreateDirectory(strChart)
                    End If

                    DownloadChars(reader.GetValue(0), strChart)

                End While

                conn.Close()

            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End If
    End Sub
    Public Sub DownloadChars(rowId As Integer, strChart As String)
        Try
            Dim query As String = "SELECT * FROM files WHERE parent_id = " & rowId

            Dim conn As New MySqlConnection(cnStr)
            Dim cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed")
            End Try
            Dim reader As MySqlDataReader
            reader = cmd.ExecuteReader()

            While reader.Read()
                
                If My.Computer.FileSystem.FileExists(strChart & "\" & reader.GetValue(1)) Then

                Else
                    DownloadFile(strChart, reader.GetValue(1), "" & reader.GetValue(10))
                End If

            End While

            conn.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        fldBrowse.SelectedPath = txtFolder.Text
        If fldBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtFolder.Text = fldBrowse.SelectedPath
        End If
    End Sub
    Private Sub DownloadFile(ByVal SaveFilePath As String, ByVal SaveFileName As String, ByVal RemoteFile As String)
        FTPSettings.IP = "www.edoccloud.com"
        FTPSettings.UserID = "ftpuser"
        FTPSettings.Password = "Zr;:F+7.9gm=D+m"
        Dim reqFTP As FtpWebRequest = Nothing
        Dim ftpStream As Stream = Nothing
        Try
            Dim outputStream As New FileStream(SaveFilePath + "\" + SaveFileName, FileMode.Create)
            Dim strPath As String = Uri.EscapeUriString("ftp://" + FTPSettings.IP + "/opt/eDocCloud/files/" + RemoteFile)
            reqFTP = DirectCast(FtpWebRequest.Create(strPath), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(FTPSettings.UserID, FTPSettings.Password)
            Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
            ftpStream = response.GetResponseStream()
            Dim cl As Long = response.ContentLength
            Dim bufferSize As Integer = 2048
            Dim readCount As Integer
            Dim buffer As Byte() = New Byte(bufferSize - 1) {}

            readCount = ftpStream.Read(buffer, 0, bufferSize)
            While readCount > 0
                outputStream.Write(buffer, 0, readCount)
                readCount = ftpStream.Read(buffer, 0, bufferSize)
            End While

            ftpStream.Close()
            outputStream.Close()
            response.Close()
        Catch ex As Exception
            If ftpStream IsNot Nothing Then
                ftpStream.Close()
                ftpStream.Dispose()
            End If
            Throw New Exception(ex.Message.ToString())
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
        RefreshListWF()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        ' Primero Busco la Caja la obtengo del WF
        If lView.SelectedItems.Count > 0 Then
            fldBrowse.SelectedPath = "C:\"
            If fldBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If My.Computer.FileSystem.DirectoryExists(fldBrowse.SelectedPath) Then
                    UploadBox(lView.SelectedItems(0).Text, fldBrowse.SelectedPath)
                Else
                    MessageBox.Show("Directory is incorrect", "imagingXperts LLC", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Else
            MessageBox.Show("No item selected", "imagingXperts LLC", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Public Sub UploadBox(wfId As String, dirFold As String)

        Try
            Dim query As String = "SELECT * FROM pickup WHERE fk_barcode = " & wfId

            Dim conn As New MySqlConnection(cnStr)
            Dim cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed")
            End Try
            Dim reader As MySqlDataReader
            reader = cmd.ExecuteReader()

            While reader.Read()
                ' Box: reader.GetValue(11)
                FindCharts(reader.GetValue(11), dirFold, reader.GetValue(2))
            End While

            conn.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub FindCharts(boxId As String, strFolder As String, strCompany As Integer)
        ' Cargo todos los directorios de la carpeta en el objects
        Dim di As New DirectoryInfo(strFolder)
        ' Get a reference to each file in that directory.
        Dim fiArr As DirectoryInfo() = di.GetDirectories
        ' Display the names of the files.
        Dim fri As DirectoryInfo

        prg.Minimum = 0
        prg.Maximum = fiArr.Count

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
    End Sub
    Private Function CreateChart(strCompany As Integer, strFolder As String, boxId As String) As String
        Dim retVal As String = "-1"
        Try
            Dim query As String = "INSERT INTO objects (fk_obj_type, fk_company, f_name, fk_parent" & _
                ", creation, fk_status) VALUES (3,'" & _
                strCompany & "','" & strFolder & "'," & boxId & _
                ",'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "',3)"

            Dim conn As New MySqlConnection(cnStr)
            Dim cmd As New MySqlCommand(query, conn)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed")
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
        Dim strVersion, strRemotePath As String
        Try
            Dim oReader As New iTextSharp.text.pdf.PdfReader(strPath & "\" & strFile)
            Dim sOut = ""
            intPages = oReader.NumberOfPages
            intFileSize = oReader.FileLength
            strVersion = oReader.PdfVersion

            'arc/2014/3/838/1321/
            ' arc <year> <company> <order> <parent>
            Dim strText1 As String = "/arc/"
            Dim strYear As String = Now.Year.ToString
            Dim miFolder As String

            miFolder = "ftp://www.edoccloud.com//opt/eDocCloud/files" & strText1 & strYear & _
                "/" & intEmpresa.ToString & "/" & intParent.ToString & "/" & strChart
            strRemotePath = miFolder & "/" & strFile

            UploadFile(strPath & "\" & strFile, strRemotePath, miFolder)

            For i = 1 To intPages
                Dim its As New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy

                sOut &= iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(oReader, i, its)
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
            cmd.Parameters.AddWithValue("@path", strRemotePath)
            Try
                conn.Open()
            Catch myerror As MySqlException
                MsgBox("Connection to the Database Failed")
            End Try
            cmd.ExecuteNonQuery()
            conn.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)
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

        For Each fri In fiArr
            If Not (My.Computer.FileSystem.FileExists(fri.FullName & "\OK")) Then
                CreateFile(fri.Name, intEmpresa, intParent, fri.DirectoryName, strChart)
                'If GetEachFile(fri.FullName) Then
                '    File.Create(fri.FullName & "\OK").Dispose()
                'End If
            End If
        Next fri

        Return strRet
    End Function
    Public Shared Function GetTextFromPDF(PdfFileName As String) As String
        Dim oReader As New iTextSharp.text.pdf.PdfReader(PdfFileName)

        Dim sOut = ""

        For i = 1 To oReader.NumberOfPages
            Dim its As New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy

            sOut &= iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(oReader, i, its)
        Next

        Return sOut
    End Function
    Private Function UploadFile(ByVal LocalFileName As String, ByVal miUri As String, miFolder As String) As Boolean
        Dim strRet As Boolean = False
        FTPSettings.UserID = "ftpuser"
        FTPSettings.Password = "Zr;:F+7.9gm=D+m"

        'Dim miRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(miUri), System.Net.FtpWebRequest)
        'miRequest.Credentials = New Net.NetworkCredential(FTPSettings.UserID, FTPSettings.Password)
        'miRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
        Try
            If Not WebRequestMethods.Ftp.ListDirectoryDetails.Contains(miFolder) Then

                Dim FTPReq As System.Net.FtpWebRequest = CType(WebRequest.Create(miFolder), FtpWebRequest)
                FTPReq.Credentials = New NetworkCredential(FTPSettings.UserID, FTPSettings.Password)
                FTPReq.Method = WebRequestMethods.Ftp.MakeDirectory

                Dim FTPRes As FtpWebResponse
                Try
                    FTPRes = CType(FTPReq.GetResponse, FtpWebResponse)
                Catch ex As Exception

                End Try

            End If

            My.Computer.Network.UploadFile(LocalFileName, miUri, FTPSettings.UserID, FTPSettings.Password)
            'Dim bFile() As Byte = System.IO.File.ReadAllBytes(LocalFileName)
            'Dim miStream As System.IO.Stream = miRequest.GetRequestStream()
            'miStream.Write(bFile, 0, bFile.Length)
            'miStream.Close()
            'miStream.Dispose()
            'strRet = True
        Catch ex As Exception
            Throw New Exception(ex.Message & ". File cannot be snded.")
        End Try
        Return strRet
    End Function

    Private Sub lView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lView.SelectedIndexChanged

    End Sub
End Class


