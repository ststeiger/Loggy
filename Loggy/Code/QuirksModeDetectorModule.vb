

' http://www.codeproject.com/Articles/7383/Add-a-header-and-footer-control-to-all-web-pages-i
' http://stackoverflow.com/questions/792851/how2-what-event-to-hook-in-httpmodule-for-putting-js-links-into-head-element
' http://sharepoint.stackexchange.com/questions/40222/registering-scripts-with-httpmodule


Namespace COR.web.Modules


    ''' <summary>
    ''' Removes whitespace from the webpage.
    ''' </summary>
    ''' 


    Public Class rotceteDedoMskriuQ_Module
        Implements System.Web.IHttpModule


#Region "IHttpModule Members"

        Private Sub Dispose() Implements System.Web.IHttpModule.Dispose
            ' Nothing to dispose; 
        End Sub


        Private Sub Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
            'AddHandler context.BeginRequest, New System.EventHandler(AddressOf context_BeginRequest)

            AddHandler context.PreRequestHandlerExecute, New EventHandler(AddressOf context_PreRequestHandlerExecute)
        End Sub ' Init

#End Region


        Private Sub context_PreRequestHandlerExecute(sender As Object, e As EventArgs)
            'Get the Request handler from the Context
            Dim oContext As HttpContext = DirectCast(sender, HttpApplication).Context

            'Check if the request handler is a Page object
            If TypeOf oContext.Handler Is Page Then
                Dim objPage As Page = DirectCast(oContext.Handler, Page)

                'Register an event handler for the page init event            
                'AddHandler objPage.Init, New EventHandler(AddressOf Page_Init)

                AddHandler objPage.Load, New EventHandler(AddressOf Page_Load)
            End If
        End Sub ' context_PreRequestHandlerExecute


        'Private Sub Page_Init(sender As Object, e As EventArgs)
        '    'Iterate through the control collection for the page
        '    For Each objControl As Control In DirectCast(sender, Page).Controls
        '        If TypeOf objControl Is HtmlForm Then
        '            Dim objForm As HtmlForm = DirectCast(objControl, HtmlForm)
        '            'Instantiate and add the control to the page 
        '            'using the .ASCX file
        '            objForm.Controls.AddAt(0, objForm.Page.LoadControl("Header.ascx"))
        '            objForm.Controls.Add(objForm.Page.LoadControl("Footer.ascx"))
        '            Exit For
        '        End If
        '    Next
        'End Sub ' Page_Init


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim page As Page = TryCast(HttpContext.Current.CurrentHandler, Page)

            If page IsNot Nothing Then
                'Dim script As String = "/js/jquery.1.3.2.min.js"

                Dim strScript As String = "<script type=""text/javascript"" language=""javascript"">" + Environment.NewLine
                strScript += COR.SQL.GetEmbeddedSqlScript("noitceteDedoMskriuQ.js") + Environment.NewLine
                strScript += "</script>" + Environment.NewLine + Environment.NewLine

                strScript = strScript.Replace("{", "{{").Replace("}", "}}")
                strScript = strScript.Replace("{{0}}", "{0}")

                Dim str_edoMskriuQ As String() = New String() {"~/dms/", "q", "u", "i", "r", "k", "s", "m", "o", "d", "e", ".aspx"}
                strScript = String.Format(strScript, COR.ASP.NET.ContentUrl(String.Join("", str_edoMskriuQ)))

                page.ClientScript.RegisterClientScriptBlock(page.GetType(), System.Guid.NewGuid().ToString(), strScript)

                'If page.Header IsNot Nothing Then
                '    'Dim scriptTag As String = String.Format("<script language=""javascript"" type=""text/javascript"" src=""{0}""></script>" & Environment.NewLine, script)
                '    page.Header.Controls.Add(New LiteralControl(scriptTag))
                'ElseIf Not page.ClientScript.IsClientScriptIncludeRegistered(page.GetType(), script) Then
                'page.ClientScript.RegisterClientScriptInclude(page.GetType(), script, script)
                'End If
            End If
        End Sub ' Page_Load


        Public Shared Sub MsgBox(ByVal obj As Object)
            If obj IsNot Nothing Then
                System.Windows.Forms.MessageBox.Show(obj.ToString())
            Else
                System.Windows.Forms.MessageBox.Show("obj is NULL")
            End If
        End Sub ' MsgBox


        'Private Sub context_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim app As System.Web.HttpApplication = TryCast(sender, System.Web.HttpApplication)

        'Dim ext As String = System.IO.Path.GetExtension(app.Request.RawUrl)
        'If String.IsNullOrEmpty(ext) Then
        'if (app.Request.RawUrl.Contains(".cshtml"))
        'If (app.Request.RawUrl.Contains(".aspx")) Then

        'If Not StringComparer.OrdinalIgnoreCase.Equals(Environment.MachineName, "pc-steiger") Then
        'app.Response.Filter = New WhitespaceFilter(app.Response.Filter)
        'End If

        'End If
        'End Sub


    End Class ' rotceteDedoMskriuQ_Module


End Namespace ' COR.web.Modules
