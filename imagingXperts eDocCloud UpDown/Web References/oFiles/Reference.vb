﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.34209
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.34209.
'
Namespace oFiles
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="FilesBinding", [Namespace]:="urn:files"),  _
     System.Xml.Serialization.SoapIncludeAttribute(GetType(FilesData))>  _
    Partial Public Class Files
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private GetFilesOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.imagingXperts_eDocCloud_UpDown.My.MySettings.Default.imagingXperts_eDocCloud_UpDown_oFiles_Files
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event GetFilesCompleted As GetFilesCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:fileswsdl#GetFiles", RequestNamespace:="urn:fileswsdl", ResponseNamespace:="urn:fileswsdl")>  _
        Public Function GetFiles(ByVal strParent As String, ByVal token As String) As <System.Xml.Serialization.SoapElementAttribute("return")> FilesData()
            Dim results() As Object = Me.Invoke("GetFiles", New Object() {strParent, token})
            Return CType(results(0),FilesData())
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetFilesAsync(ByVal strParent As String, ByVal token As String)
            Me.GetFilesAsync(strParent, token, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetFilesAsync(ByVal strParent As String, ByVal token As String, ByVal userState As Object)
            If (Me.GetFilesOperationCompleted Is Nothing) Then
                Me.GetFilesOperationCompleted = AddressOf Me.OnGetFilesOperationCompleted
            End If
            Me.InvokeAsync("GetFiles", New Object() {strParent, token}, Me.GetFilesOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetFilesOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetFilesCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetFilesCompleted(Me, New GetFilesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.SoapTypeAttribute([Namespace]:="urn:files")>  _
    Partial Public Class FilesData
        
        Private row_idField As String
        
        Private filenameField As String
        
        Private pagesField As String
        
        Private filesizeField As String
        
        Private fk_empresaField As String
        
        Private parent_idField As String
        
        Private pathField As String
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property row_id() As String
            Get
                Return Me.row_idField
            End Get
            Set
                Me.row_idField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property filename() As String
            Get
                Return Me.filenameField
            End Get
            Set
                Me.filenameField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property pages() As String
            Get
                Return Me.pagesField
            End Get
            Set
                Me.pagesField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property filesize() As String
            Get
                Return Me.filesizeField
            End Get
            Set
                Me.filesizeField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property fk_empresa() As String
            Get
                Return Me.fk_empresaField
            End Get
            Set
                Me.fk_empresaField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property parent_id() As String
            Get
                Return Me.parent_idField
            End Get
            Set
                Me.parent_idField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property path() As String
            Get
                Return Me.pathField
            End Get
            Set
                Me.pathField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")>  _
    Public Delegate Sub GetFilesCompletedEventHandler(ByVal sender As Object, ByVal e As GetFilesCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetFilesCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As FilesData()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),FilesData())
            End Get
        End Property
    End Class
End Namespace
