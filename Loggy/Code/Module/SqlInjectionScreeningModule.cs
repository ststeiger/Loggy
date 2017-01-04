
// http://track.nextmill.net/kb/a72/preventing-sql-injection-attacks-in-asp_net.aspx
namespace Loggy
{


    //https://fullcalendar.io/
    //https://github.com/jarnokurlin/fullcalendar
    //https://taitems.github.io/jQuery.Gantt/
    public class SqlInjectionScreeningModule : System.Web.IHttpModule
    {

        public static string[] blackList = {
	        "'",
            "<",
            ">",
            "<script>",
            "</script>",
            "<script",
            "</script",
            //"--",
            //";--",
            //";",
            //"/*",
            //"*/",
            "@@",
            //"@",
            "char",
            "nchar",
            "varchar",
            "nvarchar",
            "alter",
            "begin",
            "cast",
            "create",
            "cursor",
            "declare",
            "delete",
            "drop",
            //"end",
            "exec",
            "execute",
            "fetch",
            "insert",
            "kill",
            "open",
            "select",
            "sys",
            "sysobjects",
            "syscolumns",
            "table",
            "update"
        };


        void System.Web.IHttpModule.Dispose()
        {  } // End Sub Dispose 


        void System.Web.IHttpModule.Init(System.Web.HttpApplication context)
        {
            if (context == null) throw new System.ArgumentNullException("context", "Could not find HttpApplication");
            

            context.BeginRequest += new System.EventHandler(OnBeginRequest);

            // context.EndRequest += new System.EventHandler(OnEndRequest);
        } // End Sub Init 


        private void OnBeginRequest(object sender, System.EventArgs e)
        {

            System.Web.HttpApplication app = ((System.Web.HttpApplication)sender);

            if (app == null)
                return;


            System.Web.HttpContext context = app.Context;
            if (context == null)
                return;


            string appPath = string.Format("{0}://{1}{2}{3}",
                                      context.Request.Url.Scheme,
                                      context.Request.Url.Host,
                                      context.Request.Url.Port == 80
                                          ? string.Empty
                                          : ":" + context.Request.Url.Port,
                                      context.Request.ApplicationPath);
            System.Console.WriteLine(appPath);


            if (CheckInjection(context.Request))
            {
                System.Web.IHttpHandler mh = new InjectionBlockHandler();
                mh.ProcessRequest(context);
                context.ApplicationInstance.CompleteRequest();
            } // End if (CheckInjection(context.Request)) 

        } // End Sub OnBeginRequest 



        private bool MustNotCheckThisUrl(System.Web.HttpRequest request)
        {
            string strAbsPath = request.Url.AbsolutePath;
            
            if (!string.IsNullOrEmpty(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath))
            {
                strAbsPath = strAbsPath.Substring(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath.Length);
            }
            
            if(strAbsPath.StartsWith("/"))
                strAbsPath = strAbsPath.Substring(1);


            System.Console.WriteLine(strAbsPath);

            System.Collections.Hashtable AllowedUrls = new System.Collections.Hashtable(System.StringComparer.OrdinalIgnoreCase);
            
            AllowedUrls.Add("", null);
            // AllowedUrls.Add("default.aspx", null);
            // AllowedUrls.Add("w8/index.html", null);
            // AllowedUrls.Add("css/w8/Layout.ashx", null);
            // AllowedUrls.Add("js/w8/Script.ashx", null);
            // AllowedUrls.Add("w8/Loading.html", null);

            	
            // "autoStartTest@http://localhost:57566/Scripts/SimpleError.js:3:5\n"
            // <img src="https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png" />
            bool result = AllowedUrls.ContainsKey(strAbsPath);
            return result;
        }


        // System.Web.HttpRequest request
        private bool CheckInjection(System.Web.HttpRequest request)
        {
            if (MustNotCheckThisUrl(request))
                return false;

            string ip = request.UserHostAddress;

            foreach (string key in request.QueryString)
            {
                if (CheckInput(request.QueryString[key]))
                    return true;
            }

            foreach (string key in request.Form)
            {
                if (CheckInput(request.Form[key]))
                    return true;
            }

            foreach (string key in request.Cookies)
            {
                if (CheckInput(request.Cookies[key].Value))
                    return true;
            }

            return false;
        } // End Sub CheckInjection 


        private bool CheckInput(string parameter)
        {
            for (int i = 0; i <= blackList.Length - 1; i++)
            {

                // if ((parameter.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                if (System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(parameter, blackList[i], 0, System.Globalization.CompareOptions.OrdinalIgnoreCase) != -1)
                {
                    return true;
                    //Handle the discovery of suspicious Sql characters here 

                    //generic error page on your site 
                    // System.Web.HttpContext.Current.Response.Redirect("~/Error.aspx");

                    // System.Web.HttpContext.Current.Response.Clear();
                    // System.Web.HttpContext.Current.Response.ClearHeaders();
                    // System.Web.HttpContext.Current.Response.ClearContent();

                    
                    // System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                } // End if (System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(parameter, blackList[i], 0, System.Globalization.CompareOptions.OrdinalIgnoreCase) != -1) 

            } // Next i 

            return false;
        } // End Sub CheckInput 


        private void OnEndRequest(object sender, System.EventArgs e)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
        } // End Sub OnEndRequest 


    } // End Class SqlInjectionScreeningModule : System.Web.IHttpModule 


} // End Namespace Loggy 
