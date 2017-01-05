
Imports System


Namespace COR.web.Modules


    ' http://stackoverflow.com/questions/1172933/using-web-config-to-ban-user-agents
    Public Class cUserAgentBanningModule
        Implements System.Web.IHttpModule


        Protected Shared ReadOnly m_bannedUserAgentsRegex As System.Text.RegularExpressions.Regex = Nothing
        Protected Shared m_strRedirectURL As String = Nothing


        Shared Sub New()
            ' http://eng.eelcowesemann.nl/linux-unix/nginx/nginx-blocking/
            ' http://community.spiceworks.com/how_to/show/1443
            'string regex = @"(libwww-perl|msnbot/1\.1|msnbot|Java/|Purebot|Lipperhey|MaMa CaSpEr|Mail.Ru|gold crawler|MSIE)";//ConfigurationManager.AppSettings["UserAgentBasedRedirecter.UserAgentsRegex"];
            Dim regex As String = "(libwww-perl|libcurl|msnbot|Java/|Purebot|Lipperhey|MaMa CaSpEr|Mail.Ru|gold crawler|MSIE 4.|MSIE 5.|MSIE 6.|MSIE 7.)"
            'ConfigurationManager.AppSettings["UserAgentBasedRedirecter.UserAgentsRegex"];
            System.Web.HttpContext.Current.Application("ProhibitedUserAgents") = regex

            'regex = ".*chrome.*";
            If Not String.IsNullOrEmpty(regex) Then
                m_bannedUserAgentsRegex = New System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Compiled)
            End If ' Not String.IsNullOrEmpty(regex) 

        End Sub ' Constructor


#Region "Implementation of IHttpModule"


        Public Sub Init(context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
            AddHandler context.PreRequestHandlerExecute, AddressOf RedirectMatchedUserAgents
        End Sub ' Init


        Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
        End Sub ' Dispose

#End Region

        
        Public Shared Function isIPLocal(strIP As String) As Boolean
            If String.IsNullOrEmpty(strIP) Then
                Return True
            End If


            Dim ipaIP As System.Net.IPAddress = System.Net.IPAddress.Parse(strIP)

            ' 127.  0.  0.  0 - 127.255.255.255 
            '  10.  0.  0.  0 -  10.255.255.255 
            ' 172. 16.  0.  0 - 172. 31.255.255 
            ' 192.168.  0.  0 - 192.168.255.255

            If strIP.Contains(":") Then
                Return True
            End If
            

            'Dim straryIPAddress As String() = ipaddress.ToString().Split(New String() {"."}, StringSplitOptions.RemoveEmptyEntries)
            Dim straryIPAddress As String() = strIP.Split(New String() {"."}, StringSplitOptions.RemoveEmptyEntries)
            Dim iaryIPAddress As Integer() = New Integer() {Integer.Parse(straryIPAddress(0)), Integer.Parse(straryIPAddress(1)), Integer.Parse(straryIPAddress(2)), Integer.Parse(straryIPAddress(3))}
            If iaryIPAddress(0) = 10 OrElse (iaryIPAddress(0) = 192 AndAlso iaryIPAddress(1) = 168) OrElse (iaryIPAddress(0) = 172 AndAlso (iaryIPAddress(1) >= 16 AndAlso iaryIPAddress(1) <= 31)) Then
                Return True
            Else
                ' IP Address is "probably" public. This doesn't catch some VPN ranges like OpenVPN and Hamachi.
                Return False
            End If
        End Function


        Public Shared Function GetIPAddress() As String
            Dim context As System.Web.HttpContext =
                System.Web.HttpContext.Current
            Dim sIPAddress As String =
                context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(sIPAddress) Then
                Return context.Request.ServerVariables("REMOTE_ADDR")
            Else
                Dim ipArray As String() = sIPAddress.Split(
                    New Char() {","c})
                Return ipArray(0)
            End If
        End Function



        Private Shared Sub RedirectMatchedUserAgents(sender As Object, e As System.EventArgs)
            Dim app As System.Web.HttpApplication = TryCast(sender, System.Web.HttpApplication)

            If isIPLocal(GetIPAddress()) Then
                Return
            End If


            If m_bannedUserAgentsRegex IsNot Nothing AndAlso app IsNot Nothing AndAlso app.Request IsNot Nothing AndAlso Not String.IsNullOrEmpty(app.Request.UserAgent) Then

                If m_bannedUserAgentsRegex.Match(app.Request.UserAgent).Success Then

                    If m_strRedirectURL Is Nothing Then
                        'var cbAppContextBase = new HttpContextWrapper(app.Context);
                        'string strRedirectURL = System.Web.Mvc.UrlHelper.GenerateContentUrl("~/Ban/UserAgentBanned", cbAppContextBase);

                        m_strRedirectURL = COR.ASP.NET.ContentUrl("~/DMS/incompatible.aspx")
                    End If ' (m_strRedirectURL == null)

                    If Not StringComparer.OrdinalIgnoreCase.Equals(m_strRedirectURL, app.Request.RawUrl) Then
                        app.Response.Redirect(m_strRedirectURL)
                    End If ' (!StringComparer.OrdinalIgnoreCase.Equals(m_strRedirectURL, app.Request.Url.LocalPath))

                End If ' (m_bannedUserAgentsRegex.Match(app.Request.UserAgent).Success)

            End If ' (_bannedUserAgentsRegex != null && app != null && app.Request != null && !String.IsNullOrEmpty(app.Request.UserAgent))

        End Sub ' RedirectMatchedUserAgents


    End Class 'cUserAgentBanningModule : System.Web.IHttpModule 


End Namespace 'MvcTools.HttpModules
