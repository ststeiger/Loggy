
namespace Loggy
{


    public class ApplicationInfo
    {
        public string ApplicationName;
        public string AssemblyVersion;
        public string PublicIP;
        public string LocalIP;

        public string MachineName;
        public string InstallPath;
        public string FrameworkVersion;
        public string PipeLineMode;
        public string TrustLevel;
        public int Bitness;


        public string UserName;
        public string Domain;


        public string OS;
        public string OS_Version;
        public string ApplicationServer;
        public string Processor;

        public string[] Shares;
        public string[] DriveList;


        public System.Collections.Specialized.NameValueCollection EnvironmentVariables;
    }


    public class DbInfo
    {
        public string DbType;
        public string DbVersion;
        public string ConnectionString;
    }


    public class HttpInfo
    {
        public string Method;
        public string Url;
        public string Path; // Host + VirtDir
        public string Query; // Host + VirtDir
        public string UserAgent; // Screen-size
        public string Languages; // Culture

        public string Cookies;
        public string Sessions;

        public System.Collections.Specialized.NameValueCollection Session;



        public class CookieValues
        {
            public string Name;
            public string Value;
            public string Path;
            public System.DateTime Expires;

            public bool Secure;
            public bool HttpOnly;


            public CookieValues(System.Web.HttpCookie c)
            {

                this.Name = c.Name;
                this.Value = c.Value;
                this.Path = c.Path;
                this.Expires = c.Expires;
                this.Secure = c.Secure;
                this.HttpOnly = c.HttpOnly;

            }


        }



        public static void GatherCookieData(System.Web.HttpContext context)
        {
            System.Collections.Generic.Dictionary<string, CookieValues> dict = new System.Collections.Generic.Dictionary<string, CookieValues>(System.StringComparer.OrdinalIgnoreCase);

            foreach (System.Web.HttpCookie thisCookie in context.Request.Cookies)
            {
                dict.Add(c.Name, new CookieValues(thisCookie));
            } // Next thisCookie 

        } // End Function GatherCookieData 


        public static System.Collections.Generic.Dictionary<string, object> GatherUserSessionInfo()
        {
            System.Collections.Generic.Dictionary<string, object> Session =
                new System.Collections.Generic.Dictionary<string, object>();


            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.DataSource = System.Environment.MachineName;
            csb.InitialCatalog = "COR_Basic";
            
            csb.IntegratedSecurity = true;
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "";
                csb.Password = "";
            }
            csb.ApplicationName = "Loggy";
            // csb.CurrentLanguage = "";
            csb.PacketSize = 4096;
            csb.PersistSecurityInfo = false;
            csb.Pooling = true;
            csb.MaxPoolSize = 5;
            csb.WorkstationID = System.Environment.MachineName;

            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "SELECT TOP 2 * FROM T_Benutzer";
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sql, csb.ConnectionString);
            da.Fill(dt);

            System.Web.HttpContext.Current.Session["foo"] = dt;
            System.Web.HttpContext.Current.Session["bar"] = dt.Rows[0];



            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null)
            {
                foreach (string strSessionKey in System.Web.HttpContext.Current.Session.Keys)
                {
                    Session[strSessionKey] = System.Web.HttpContext.Current.Session[strSessionKey];
                } // Next strSessionKey 

            } // End if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null)


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Session, Newtonsoft.Json.Formatting.Indented);
            System.Console.WriteLine(json);

            return Session;
        } // End Function GatherUserSessionInfo 

        public static void foo(System.Web.HttpContext context)
        {

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(context.Request.Headers, Newtonsoft.Json.Formatting.Indented);
            System.Console.WriteLine(json);
            // context.Request.RequestType
            // context.Request.ServerVariables
            // context.Request.Url
            // context.Request.UrlReferrer;
            // context.Request.UserAgent;
            // context.Request.UserHostAddress;
            // context.Request.UserHostAddress;
            // context.Request.UserLanguages;

            // context.Application.AllKeys
            // context.ApplicationInstance.all
            // context.ApplicationInstance.Site
            // context.Cache
            foreach (object x in context.Cache)
            {
                System.Console.WriteLine(x.GetType());
            }
            context.Request.PathInfo
            //context.Request.InputStream
            context.Request.ContentType
            context.Request.ContentEncoding
            context.Request.Browser
            context.Request.ApplicationPath

            foreach (string header in context.Request.Headers.AllKeys)
            {
                string value = context.Request.Headers[header];
            }


            foreach (string serverVariable in context.Request.ServerVariables.AllKeys)
            {
                string value = context.Request.ServerVariables[serverVariable];
            }
        }




        public string ClientIP;
        public string ForwardedClientIP;


        public System.Collections.Specialized.NameValueCollection RequestHeaders;
        public System.Collections.Specialized.NameValueCollection ResponseHeaders; // Server:Microsoft-IIS/7.5


        public System.Collections.Specialized.NameValueCollection GetParameters;
        public System.Collections.Specialized.NameValueCollection PostParameters;


        // InputStream

        public string FormsUserId;
        public string FormsUserName;
    }


    public class ExceptionInfo
    {
        public string Type;
        public string Source;
        public string StackTrace;
    }


    public class ErrorInfo
    {
        public System.DateTime When;
        public ApplicationInfo App;
        public DbInfo Db;
        public HttpInfo Http;
        public ExceptionInfo Exception;
    }


}
