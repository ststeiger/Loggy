
// using System.Net.Mail.


namespace Loggy
{

    public class LocalLogon
    {
        public static void TestPassword()
        {
            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                LocalAuth_Linux.PasswordValid("usr", "dom", "pw");
            else
                LocalAuth_NativeMethods_Win32.PasswordValid("usn", "domain", "pw");
        }
    }


    public class LocalAuth_Linux
    {
        // TODO
        public static bool PasswordValid(string userName, string domain, string password)
        {
            return false;
        }
    }


    // http://www.pinvoke.net/default.aspx/advapi32.logonuser
    public class LocalAuth_NativeMethods_Win32
    {
        public static bool PasswordValid(string userName, string domain, string password)
        {
            System.IntPtr tokenHandle = new System.IntPtr();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378184(v=vs.85).aspx
            // Use LOGON32_LOGON_NETWORK if you just want to verify the username/password and don't need to impersonate them or anything.
            // If the function succeeds, the function returns nonzero.
            // If the function fails, it returns zero. To get extended error information, call GetLastError.
            return LogonUser(userName, domain, password,
                LogonType.LOGON32_LOGON_NETWORK, LogonProvider.LOGON32_PROVIDER_DEFAULT,
                ref tokenHandle);
        }



        public enum LogonType
        {
            /// <summary>
            /// This logon type is intended for users who will be interactively using the computer, such as a user being logged on  
            /// by a terminal server, remote shell, or similar process.
            /// This logon type has the additional expense of caching logon information for disconnected operations;
            /// therefore, it is inappropriate for some client/server applications,
            /// such as a mail server.
            /// </summary>
            LOGON32_LOGON_INTERACTIVE = 2,

            /// <summary>
            /// This logon type is intended for high performance servers to authenticate plaintext passwords.

            /// The LogonUser function does not cache credentials for this logon type.
            /// </summary>
            LOGON32_LOGON_NETWORK = 3,

            /// <summary>
            /// This logon type is intended for batch servers, where processes may be executing on behalf of a user without
            /// their direct intervention. This type is also for higher performance servers that process many plaintext
            /// authentication attempts at a time, such as mail or Web servers.
            /// The LogonUser function does not cache credentials for this logon type.
            /// </summary>
            LOGON32_LOGON_BATCH = 4,

            /// <summary>
            /// Indicates a service-type logon. The account provided must have the service privilege enabled.
            /// </summary>
            LOGON32_LOGON_SERVICE = 5,

            /// <summary>
            /// This logon type is for GINA DLLs that log on users who will be interactively using the computer.
            /// This logon type can generate a unique audit record that shows when the workstation was unlocked.
            /// </summary>
            LOGON32_LOGON_UNLOCK = 7,

            /// <summary>
            /// This logon type preserves the name and password in the authentication package, which allows the server to make
            /// connections to other network servers while impersonating the client. A server can accept plaintext credentials
            /// from a client, call LogonUser, verify that the user can access the system across the network, and still
            /// communicate with other servers.
            /// NOTE: Windows NT:  This value is not supported.
            /// </summary>
            LOGON32_LOGON_NETWORK_CLEARTEXT = 8,

            /// <summary>
            /// This logon type allows the caller to clone its current token and specify new credentials for outbound connections.
            /// The new logon session has the same local identifier but uses different credentials for other network connections.
            /// NOTE: This logon type is supported only by the LOGON32_PROVIDER_WINNT50 logon provider.
            /// NOTE: Windows NT:  This value is not supported.
            /// </summary>
            LOGON32_LOGON_NEW_CREDENTIALS = 9,
        }

        public enum LogonProvider
        {
            /// <summary>
            /// Use the standard logon provider for the system.
            /// The default security provider is negotiate, unless you pass NULL for the domain name and the user name
            /// is not in UPN format. In this case, the default provider is NTLM.
            /// NOTE: Windows 2000/NT:   The default security provider is NTLM.
            /// </summary>
            LOGON32_PROVIDER_DEFAULT = 0,
            LOGON32_PROVIDER_WINNT35 = 1,
            LOGON32_PROVIDER_WINNT40 = 2,
            LOGON32_PROVIDER_WINNT50 = 3
        }

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        internal static extern bool LogonUser(
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pszUserName,
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pszDomain,
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pszPassword,
            LogonType dwLogonType,
            LogonProvider dwLogonProvider,
            ref System.IntPtr phToken);

    }


    
    public sealed class ErrorHandler
    {

        public class Logging
        {
            public static string LogFilePath = "";
        }


        public static void SendMail(string strErrorMessage)
        {
            string sender = "cor.ErrorLog@smtp.riz-itmotion.de";
            string recipient = "COR_SwissRe_Postfach@cor-management.ch";
            string copyRecipient = "";
            string blindCopyRecipient = "";
            System.Net.Mail.MailPriority MailPriority = System.Net.Mail.MailPriority.Normal;



            string strMachineInfo = "Machine: " + System.Environment.MachineName;
            strMachineInfo += System.Environment.NewLine;
            strMachineInfo += "Domain: " + System.Environment.UserDomainName;
            strMachineInfo += System.Environment.NewLine;
            strMachineInfo += "User: " + System.Environment.UserName;
            strMachineInfo += System.Environment.NewLine;
            strMachineInfo += "Time: " + System.DateTime.Now.ToString("dddd, dd.MM.yyyy HH:mm:ss");
            strMachineInfo += System.Environment.NewLine;
            strMachineInfo += "Bitness: " + (System.IntPtr.Size * 8).ToString();
            strMachineInfo += System.Environment.NewLine;
            strMachineInfo += "OS: " + System.Environment.OSVersion.ToString();
            strMachineInfo += System.Environment.NewLine;
            strMachineInfo += "CLR Version: " + System.Environment.Version.ToString();
            strMachineInfo += System.Environment.NewLine;

            strMachineInfo += System.Environment.NewLine;
            strMachineInfo += System.Environment.NewLine;


            strErrorMessage = strMachineInfo + strErrorMessage;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.Priority = MailPriority;


            mail.From = new System.Net.Mail.MailAddress(sender);
            mail.To.Add(recipient);

            if (copyRecipient.Length > 0)
            {
                mail.CC.Add(copyRecipient);
            }

            if (blindCopyRecipient.Length > 0)
            {
                mail.Bcc.Add(blindCopyRecipient);
            }


            string strAssemblyName = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            mail.Subject = strAssemblyName + " Error";
            mail.Body = strErrorMessage;
            mail.IsBodyHtml = false;

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            if (!string.IsNullOrEmpty(Logging.LogFilePath))
            {
                if (System.IO.File.Exists(Logging.LogFilePath))
                {
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(Logging.LogFilePath);
                    mail.Attachments.Add(attach);
                }

            }


            //mail.ReplyTo = New System.Net.Mail.MailAddress("bimbo@example.com")

            //mail.DeliveryNotificationOptions = DeliveryNotificationOptions.None

            //Dim ysodAttachment As New System.Net.Mail.Attachment("filename", New System.Net.Mime.ContentType(""))
            //Dim ysodAttachment As New System.Net.Mail.Attachment("filename", "mediatype")
            //mail.Attachments.Add(ysodAttachment)

            InternalSendMail(mail);
        }
        // SendMail


        protected static void InternalSendMail(System.Net.Mail.MailMessage mail)
        {
            string SmtpServer = "smtp.riz-itmotion.de";
            int SmtpPort = 25;
            string AuthUserName = "";
            string AuthPassword = "";
            bool UseSsl = false;



            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            string host = SmtpServer ?? string.Empty;

            if (host.Length > 0)
            {
                client.Host = host;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            }

            int port = SmtpPort;
            if (port > 0)
            {
                client.Port = port;
            }

            string userName = AuthUserName ?? string.Empty;
            string password = AuthPassword ?? string.Empty;

            if (userName.Length > 0 && password.Length > 0)
            {
                client.Credentials = new System.Net.NetworkCredential(userName, password);
            }
            else
            {
                //client.Credentials = System.Net.CredentialCache.DefaultCredentials
                client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            }

            client.EnableSsl = UseSsl;



            if (UseSsl)
            {
                // http://stackoverflow.com/questions/1301127/how-to-ignore-a-certificate-error-with-c-2-0-webclient-without-the-certificate
                //InitiateSSLTrust()
                try
                {
                    //Change SSL checks so that all checks pass
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback( delegate { return true; });
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(InternalCallback);
                    
                    //System.Net.ServicePointManager.CertificatePolicy = New AcceptAllCertificatePolicy()
                }
                catch (System.Exception ex)
                {
                }

            }

            client.Send(mail);
        }
        // InternalSendMail


        //system.security.cryptography.x509certificates.

        private static bool InternalCallback(object sender
            , System.Security.Cryptography.X509Certificates.X509Certificate certificate
            , System.Security.Cryptography.X509Certificates.X509Chain chain
            , System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            System.Net.WebRequest request = sender as System.Net.WebRequest;

            if (request != null)
            {

                //if (request == this.request)
                //{
                //    if (this.callback != null)
                //    {
                //        return this.callback(sender, certificate, chain, sslPolicyErrors);
                //    }
                //}
            }

            return true;
        }


        //private static void UnhandledConsoleException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    string str = "";

        //    if (e != null)
        //    {
        //        if (e.ExceptionObject != null)
        //        {
        //            str += e.ExceptionObject.ToString() + Environment.NewLine;
        //        }
        //    }

        //    ReportApplicationCrashModal(str);
        //    //Console.WriteLine(str)
        //    //Console.WriteLine("Press Enter to continue")
        //    //Console.ReadLine()
        //    //Environment.Exit(1)
        //}
        //// UnhandledConsoleException


        //public static void UnhandedGuiException(object sender, System.Threading.ThreadExceptionEventArgs e)
        //{
        //    string str = "";

        //    if (e != null)
        //    {
        //        if (e.Exception != null)
        //        {
        //            str += e.Exception.ToString() + Environment.NewLine;
        //        }
        //    }

        //    ReportApplicationCrashModal(str);
        //}
        //// UnhandedGuiException


        //public static void ReportApplicationCrashModal(string strError)
        //{
        //    frmError ErrorReportingForm = new frmError(strError);

        //    ErrorReportingForm.ShowDialog();
        //}
        //// ReportApplicationCrashModal


        //public static void ShowErrorModal(string strError)
        //{
        //    ShowErrorModal(strError, true);
        //}
        //// ShowErrorModal


        //public static void ShowErrorModal(string strError, bool bTerminate)
        //{
        //    //MsgBox(strError)

        //    frmErrorDetails ErrorForm = new frmErrorDetails();
        //    ErrorForm.AddErrorText(strError);
        //    ErrorForm.ShowDialog();

        //    if (bTerminate)
        //    {
        //        System.Environment.Exit(0);
        //    }
        //} // ShowErrorModal


        //public static void InitErrorHandling()
        //{
        //    //System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        //    System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(UnhandedGuiException);
        //    AppDomain.CurrentDomain.UnhandledException += UnhandledConsoleException;
        //} // InitErrorHandling


    } // ErrorHandler



    public class MailSender
    {


        /*
        public static void send(string tFrom, string tTo, string tSubject, string tBody)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(tFrom, tTo, tSubject, tBody);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            string smtpserver = System.Web.HttpContext.Current.Application["smtp_server"];
            string smtpport = System.Web.HttpContext.Current.Application["smtp_port"];
            smtp.Host = smtpserver;
            smtp.Port = IsInteger(smtpport) ? Convert.ToInt32(smtpport) : 25;

            string smtpauthenticate = System.Web.HttpContext.Current.Application["smtp_authenticate"];
            if (!string.IsNullOrEmpty(smtpauthenticate) && smtpauthenticate.ToLower.Equals("true"))
            {
                string smtpuser = System.Web.HttpContext.Current.Application["smtp_user"];
                string smtppasswort = System.Web.HttpContext.Current.Application["smtp_passwort"];
                if (!string.IsNullOrEmpty(smtppasswort))
                    smtppasswort = CryptStrings.DeCrypt(smtppasswort);

                smtp.Credentials = new System.Net.NetworkCredential(smtpuser, smtppasswort);
            }

            smtp.Send(message);
        }
        */


        public void SendMail(string mailto, string mailtext)
        {
            //IniFile objIniFile = new IniFile(Directory.GetCurrentDirectory + "\\" + "COR_Vertragsverwaltung_Sendmail.ini");
            string smtp = ""; // objIniFile.GetString("Mailing", "SMTP", "");
            int port = 25; // objIniFile.GetString("Mailing", "Port", "");
            int auth = 1; //objIniFile.GetInteger("Mailing", "auth", 0);
            string authUser = "";  // objIniFile.GetString("Mailing", "authUser", "");
            string authPassword = ""; // objIniFile.GetString("Mailing", "authPassword", "");
            string sender = ""; // objIniFile.GetString("Mailing", "absender", "");
            string subject = ""; // objIniFile.GetString("Mailing", "subject", "");


            // SMTP-Server
            System.Net.Mail.SmtpClient oSMTP = new System.Net.Mail.SmtpClient();
            var _with1 = oSMTP;
            // Mailserver
            _with1.Host = System.Convert.ToString(smtp);
            _with1.Port = port;

            // Erweiterte Mail-Einstellungen
            _with1.UseDefaultCredentials = false;
            _with1.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            // SMTP-AUTH mit UserName und Kennwort
            if (auth == 1)
            {
                _with1.Credentials = new System.Net.NetworkCredential(authUser, authPassword);
            }

            System.Net.Mail.MailMessage oMail = new System.Net.Mail.MailMessage();
            var _with2 = oMail;
            // Betreff
            _with2.From = new System.Net.Mail.MailAddress(sender);
            _with2.To.Add(new System.Net.Mail.MailAddress(mailto));

            _with2.Subject = System.Convert.ToString(subject);

            // Nachricht (kein HTML)
            _with2.IsBodyHtml = false;
            _with2.Body = System.Convert.ToString(mailtext);

            // ggf. Kopie-Empfänger hinzufügen
            // _with2.CC.Add(new System.Net.Mail.MailAddress("email address"));


            // ggf. BCC-Empfänger hinzufügen
            // _with2.Bcc.Add(new System.Net.Mail.MailAddress("email address"));


            // Anlagen hinzufügen
            // _with2.Attachments.Add( new System.Net.Mail.Attachment("path and filename")  );

            // Priorität einstellen
            _with2.Priority = System.Net.Mail.MailPriority.Normal;

            try
            {
                // Nachricht senden
                oSMTP.Send(oMail);

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Fehler: " + ex.Message);
            }

        }


    }


}