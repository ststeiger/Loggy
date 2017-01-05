
Imports System


Namespace COR.web.Modules


    ' http://madskristensen.net/post/Track-your-visitors-using-an-HttpModule.aspx
    Public Class cVisitorTrackingModule
        Implements System.Web.IHttpModule

#Region "Implementation of IHttpModule"


        Public Sub Init(context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
            AddHandler context.PostRequestHandlerExecute, AddressOf context_PostRequestHandlerExecute


            Dim session As SessionStateModule = DirectCast(context.Modules("Session"), SessionStateModule)
            AddHandler session.Start, New EventHandler(AddressOf session_Start)
            AddHandler session.End, New EventHandler(AddressOf session_End)
        End Sub ' Init


        Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
        End Sub ' Dispose

#End Region


        Private Sub session_Start(sender As Object, e As EventArgs)
            Dim context As HttpContext = HttpContext.Current
            Dim visit As New cVisit()
            visit.UserAgent = context.Request.UserAgent
            visit.IpAddress = context.Request.UserHostAddress
            context.Session.Add("visit", visit)
        End Sub


        ' Then every page view is registered after an .aspx page is served.
        Private Sub context_PostRequestHandlerExecute(sender As Object, e As EventArgs)
            Dim context As HttpContext = DirectCast(sender, HttpApplication).Context

            If TypeOf context.CurrentHandler Is Page Then
                Dim visit As cVisit = TryCast(context.Session("visit"), cVisit)
                If visit IsNot Nothing Then
                    Dim action As New cAction()
                    action.Url = context.Request.Url
                    action.Type = "pageview"
                    visit.Action.Add(action)
                End If
            End If
        End Sub


        ' Then the session ends and we need to store the visitor log.
        Private Sub session_End(sender As Object, e As EventArgs)
            Dim context As HttpContext = HttpContext.Current
            Dim Visit As cVisit = TryCast(context.Session("visit"), cVisit)
            ' Log the Visit object to a database
            If Visit IsNot Nothing Then
            End If
        End Sub


        Public Class cVisit
            Public UserAgent As String
            Public IpAddress As String
            Public Action As New List(Of cAction)
        End Class ' cVisit


        Public Class cAction
            Public Url As System.Uri
            Public Type As String
            Public [Date] As DateTime = DateTime.UtcNow
        End Class ' cAction


    End Class 'cVisitorTrackingModule : System.Web.IHttpModule 


End Namespace 'MvcTools.HttpModules
