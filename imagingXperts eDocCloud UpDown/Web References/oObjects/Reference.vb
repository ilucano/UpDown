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
Namespace oObjects
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="OrdersBinding", [Namespace]:="urn:orders"),  _
     System.Xml.Serialization.SoapIncludeAttribute(GetType(ObjectData))>  _
    Partial Public Class Orders
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private GetOrdersOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetBoxesOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetChartsOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.imagingXperts_eDocCloud_UpDown.My.MySettings.Default.imagingXperts_eDocCloud_UpDown_oObjects_Orders
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
        Public Event GetOrdersCompleted As GetOrdersCompletedEventHandler
        
        '''<remarks/>
        Public Event GetBoxesCompleted As GetBoxesCompletedEventHandler
        
        '''<remarks/>
        Public Event GetChartsCompleted As GetChartsCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orderswsdl#GetOrders", RequestNamespace:="urn:orderswsdl", ResponseNamespace:="urn:orderswsdl")>  _
        Public Function GetOrders(ByVal strCompany As String, ByVal token As String) As <System.Xml.Serialization.SoapElementAttribute("return")> ObjectData()
            Dim results() As Object = Me.Invoke("GetOrders", New Object() {strCompany, token})
            Return CType(results(0),ObjectData())
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetOrdersAsync(ByVal strCompany As String, ByVal token As String)
            Me.GetOrdersAsync(strCompany, token, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetOrdersAsync(ByVal strCompany As String, ByVal token As String, ByVal userState As Object)
            If (Me.GetOrdersOperationCompleted Is Nothing) Then
                Me.GetOrdersOperationCompleted = AddressOf Me.OnGetOrdersOperationCompleted
            End If
            Me.InvokeAsync("GetOrders", New Object() {strCompany, token}, Me.GetOrdersOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetOrdersOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetOrdersCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetOrdersCompleted(Me, New GetOrdersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:boxeswsdl#GetBoxes", RequestNamespace:="urn:boxeswsdl", ResponseNamespace:="urn:boxeswsdl")>  _
        Public Function GetBoxes(ByVal strParent As String, ByVal token As String) As <System.Xml.Serialization.SoapElementAttribute("return")> ObjectData()
            Dim results() As Object = Me.Invoke("GetBoxes", New Object() {strParent, token})
            Return CType(results(0),ObjectData())
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetBoxesAsync(ByVal strParent As String, ByVal token As String)
            Me.GetBoxesAsync(strParent, token, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetBoxesAsync(ByVal strParent As String, ByVal token As String, ByVal userState As Object)
            If (Me.GetBoxesOperationCompleted Is Nothing) Then
                Me.GetBoxesOperationCompleted = AddressOf Me.OnGetBoxesOperationCompleted
            End If
            Me.InvokeAsync("GetBoxes", New Object() {strParent, token}, Me.GetBoxesOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetBoxesOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetBoxesCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetBoxesCompleted(Me, New GetBoxesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:boxeswsdl#GetCharts", RequestNamespace:="urn:boxeswsdl", ResponseNamespace:="urn:boxeswsdl")>  _
        Public Function GetCharts(ByVal strParent As String, ByVal token As String) As <System.Xml.Serialization.SoapElementAttribute("return")> ObjectData()
            Dim results() As Object = Me.Invoke("GetCharts", New Object() {strParent, token})
            Return CType(results(0),ObjectData())
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetChartsAsync(ByVal strParent As String, ByVal token As String)
            Me.GetChartsAsync(strParent, token, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetChartsAsync(ByVal strParent As String, ByVal token As String, ByVal userState As Object)
            If (Me.GetChartsOperationCompleted Is Nothing) Then
                Me.GetChartsOperationCompleted = AddressOf Me.OnGetChartsOperationCompleted
            End If
            Me.InvokeAsync("GetCharts", New Object() {strParent, token}, Me.GetChartsOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetChartsOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetChartsCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetChartsCompleted(Me, New GetChartsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
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
     System.Xml.Serialization.SoapTypeAttribute([Namespace]:="urn:orders")>  _
    Partial Public Class ObjectData
        
        Private row_idField As String
        
        Private fk_obj_typeField As String
        
        Private fk_companyField As String
        
        Private f_codeField As String
        
        Private f_nameField As String
        
        Private fk_parentField As String
        
        Private qtyField As String
        
        Private fk_statusField As String
        
        Private invoicedField As String
        
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
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property fk_obj_type() As String
            Get
                Return Me.fk_obj_typeField
            End Get
            Set
                Me.fk_obj_typeField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property fk_company() As String
            Get
                Return Me.fk_companyField
            End Get
            Set
                Me.fk_companyField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property f_code() As String
            Get
                Return Me.f_codeField
            End Get
            Set
                Me.f_codeField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property f_name() As String
            Get
                Return Me.f_nameField
            End Get
            Set
                Me.f_nameField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property fk_parent() As String
            Get
                Return Me.fk_parentField
            End Get
            Set
                Me.fk_parentField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property qty() As String
            Get
                Return Me.qtyField
            End Get
            Set
                Me.qtyField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="integer")>  _
        Public Property fk_status() As String
            Get
                Return Me.fk_statusField
            End Get
            Set
                Me.fk_statusField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property invoiced() As String
            Get
                Return Me.invoicedField
            End Get
            Set
                Me.invoicedField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")>  _
    Public Delegate Sub GetOrdersCompletedEventHandler(ByVal sender As Object, ByVal e As GetOrdersCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetOrdersCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As ObjectData()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),ObjectData())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")>  _
    Public Delegate Sub GetBoxesCompletedEventHandler(ByVal sender As Object, ByVal e As GetBoxesCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetBoxesCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As ObjectData()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),ObjectData())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")>  _
    Public Delegate Sub GetChartsCompletedEventHandler(ByVal sender As Object, ByVal e As GetChartsCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetChartsCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As ObjectData()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),ObjectData())
            End Get
        End Property
    End Class
End Namespace