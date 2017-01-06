
Namespace COR.web.Modules


    ''' <summary>
    ''' Removes whitespace from the webpage.
    ''' </summary>
    Public Class ieIntranetModule
        Implements System.Web.IHttpModule

#Region "IHttpModule Members"

        Private Sub System_Web_IHttpModule_Dispose() Implements System.Web.IHttpModule.Dispose
            ' Nothing to dispose; 
        End Sub

        Private Sub System_Web_IHttpModule_Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
            AddHandler context.BeginRequest, New System.EventHandler(AddressOf context_BeginRequest)
        End Sub

#End Region


        Private Function isIPLocal(ipaddress As System.Net.IPAddress) As Boolean
            ' 127.  0.  0.  0 - 127.255.255.255 
            '  10.  0.  0.  0 -  10.255.255.255 
            ' 172. 16.  0.  0 - 172. 31.255.255 
            ' 192.168.  0.  0 - 192.168.255.255

            Dim straryIPAddress As String() = ipaddress.ToString().Split(New String() {"."}, StringSplitOptions.RemoveEmptyEntries)
            Dim iaryIPAddress As Integer() = New Integer() {Integer.Parse(straryIPAddress(0)), Integer.Parse(straryIPAddress(1)), Integer.Parse(straryIPAddress(2)), Integer.Parse(straryIPAddress(3))}
            If iaryIPAddress(0) = 10 OrElse (iaryIPAddress(0) = 192 AndAlso iaryIPAddress(1) = 168) OrElse (iaryIPAddress(0) = 172 AndAlso (iaryIPAddress(1) >= 16 AndAlso iaryIPAddress(1) <= 31)) Then
                Return True
            Else
                ' IP Address is "probably" public. This doesn't catch some VPN ranges like OpenVPN and Hamachi.
                Return False
            End If
        End Function


        Private Sub context_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim app As System.Web.HttpApplication = TryCast(sender, System.Web.HttpApplication)

            'Dim ext As String = System.IO.Path.GetExtension(app.Request.RawUrl)
            'If String.IsNullOrEmpty(ext) Then
            'if (app.Request.RawUrl.Contains(".cshtml"))
            If (app.Request.RawUrl.Contains(".aspx")) Then

                ' http://stackoverflow.com/questions/4091157/httpmodule-to-add-headers-to-request

                'If Not StringComparer.OrdinalIgnoreCase.Equals(Environment.MachineName, "pc-steiger") Then
                'app.Response.Filter = New WhitespaceFilter(app.Response.Filter)
                app.Response.AddHeader("", "")
                'End If

            End If
        End Sub

    End Class


End Namespace
