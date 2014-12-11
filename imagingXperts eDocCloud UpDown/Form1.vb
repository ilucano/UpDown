Imports System.Net
Imports System.IO
Imports MySql.Data
Imports MySql.Data.MySqlClient

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
            reqFTP = DirectCast(FtpWebRequest.Create(New Uri("ftp://" + FTPSettings.IP + "/etc/eDocCloud/" + RemoteFile)), FtpWebRequest)
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
End Class


