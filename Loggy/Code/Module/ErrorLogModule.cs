
namespace Loggy
{


    // https://code.tutsplus.com/tutorials/preventing-xss-in-aspnet--cms-21801
    public class ErrorLogModule : System.Web.IHttpModule
    {


        public ErrorLogModule()
		{
			// TODO: Add constructor logic here
		}


        void System.Web.IHttpModule.Dispose()
        {
            // throw new NotImplementedException();
        }


        // http://www.codeguru.com/csharp/.net/net_asp/article.php/c19389/HTTP-Handlers-and-HTTP-Modules-in-ASPNET.htm
        void System.Web.IHttpModule.Init(System.Web.HttpApplication context)
        {
            if (context == null) throw new System.ArgumentNullException("context", "Could not find HttpApplication");
            context.Error += OnError;

            context.BeginRequest += new System.EventHandler(OnBeginRequest);

            // https://stackoverflow.com/questions/1888016/any-way-to-add-httphandler-programatically-in-net
            // is there any way to programatically add an HttpHandler to an ASP.NET website without adding to the web.config?
            // Obviously you probably want to wrap this in some logic to check for things such as verb, requesting url, etc. 
            context.PostMapRequestHandler += new System.EventHandler(OnPostMapRequestHandler);
            
            // throw new NotImplementedException();
        }


        private void OnBeginRequest(object sender, System.EventArgs e)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;


            System.Web.HttpApplication app = ((System.Web.HttpApplication)sender);

            if (app == null)
                return;


            System.Web.HttpContext context = app.Context;
            if (context == null)
                return;

            if (context.Request == null)
                return;

            var nvc = context.Request.QueryString;
            if (nvc == null)
                return;

            string culture = nvc["culture"];
            if (string.IsNullOrEmpty(culture))
                culture = "en-US";


            // context.Request.UserLanguages = new string[] { "a", "b" };

            System.Globalization.CultureInfo cult = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = cult;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cult;


        }


        private void OnPostMapRequestHandler(object sender, System.EventArgs e)
        {
            System.Web.HttpApplication app = ((System.Web.HttpApplication)sender);

            if (app == null)
                return;


            System.Web.HttpContext context = app.Context;
            if (context == null)
                return;


            var appPath = string.Format("{0}://{1}{2}{3}",
                                      context.Request.Url.Scheme,
                                      context.Request.Url.Host,
                                      context.Request.Url.Port == 80
                                          ? string.Empty
                                          : ":" + context.Request.Url.Port,
                                      context.Request.ApplicationPath);
            System.Console.WriteLine(appPath);


            if (context.Request.Url.AbsolutePath == "/foo.ashx")
            {
                // IHttpHandler myHandler = new MyHandler();
                // context.Handler = myHandler;
                context.Handler = new ErrorTemplateHandler();
            }

        }


        protected void onError(object sender, System.EventArgs e)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            System.Web.HttpException tempError = context.Server.GetLastError() as System.Web.HttpException;

            if (tempError != null && tempError.GetHttpCode() == 404)
            {
                // this.saveLog(context);
                
                //' R.L.: Ignores costum error property due to split 404 and error handling.
                 
                if (context.Request.RawUrl.Contains(".aspx"))
                {
                    /*
                    context.Response.StatusCode = 404;


                    Configuration.CustomErrorsSection tempConfig = (Configuration.CustomErrorsSection)Configuration.WebConfigurationManager.GetSection("system.web/customErrors");
                    string tempFile = string.IsNullOrEmpty(tempConfig.DefaultRedirect) ? "~/noavailability.aspx" : tempConfig.DefaultRedirect;
                    if (tempConfig.Errors("404") != null)
                        tempFile = tempConfig.Errors("404").Redirect;
                    if (!tempFile.Contains("?"))
                        tempFile = tempFile + "?404=true";

                    tempFile += "&aspxerrorpath=" + context.Request.RawUrl.Split('?')(0);

                    if (!this.queryString(context).Equals(string.Empty))
                    {
                        tempFile += "&" + this.queryString(context);
                    }

                    try
                    {
                        context.Server.ClearError();
                        context.Server.Transfer(tempFile);
                    }
                    catch (Exception tempException)
                    {
                        context.Response.Write(tempException.Message + ": " + tempException.StackTrace);
                        context.Response.Flush();
                        context.Response.End();
                    }
                    */
                }
                
                
            }
        }


        public virtual void OnError(object sender, System.EventArgs args)
        {
            System.Web.HttpApplication app = (System.Web.HttpApplication)sender;
            System.Exception ex = app.Server.GetLastError();

            LogException(ex);

            // app.Server.ClearError();

        } // End Sub OnError 


        public virtual void LogException(System.Exception ex)
        {
            System.Guid errorUID = System.Guid.NewGuid();
            // System.Web.HttpContext.Current.Application.

            // OnBeforeLog
            // Log SQL-Command with parameters
            IterativeExceptionLogger("ErrorLogModule.cs", 0, ex, null, null);
            // Trace.WriteLine // echo the error for local debugging
            // OnAfterLog
        } // End Sub LogException 

        
        // Format:
        // ExcetionType: ExceptionMessage; Verdana 13.5, color: rgb(128 0 0) bg: white 
        // Generated: Sat, 17 May 2014 11:30:07 GMT bg: white;

        // Trace Courier New, 10, bg: #FFFFCC [RGB(255, 255, 204)]
        public static void IterativeExceptionLogger(string origin, int level, System.Exception exceptionToLog, System.Data.Common.DbCommand cmd, string logMessage)
        {
            System.Collections.Generic.Stack<System.Exception> stack = new System.Collections.Generic.Stack<System.Exception>();

            exceptionToLog = new System.Exception("foo", exceptionToLog);

            while (exceptionToLog != null)
            {
                stack.Push(exceptionToLog);
                exceptionToLog = exceptionToLog.InnerException;
            }

            int sequence = 0;


            string headers;
            string url;
            string postParams;
            string ipAddress;
            string referer;
            string applicationName;
            string implementationScript;
            string SQL;
            string userName;
            string sessionData;
            string userAgent;

            System.DateTime date = System.DateTime.Now;

            // http://stackoverflow.com/questions/1654797/get-iis-site-name-from-for-an-asp-net-website
            // string siteName = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();




            // System.Web.Hosting.HostingEnvironment.IsHosted
            System.Console.WriteLine(System.Web.Hosting.HostingEnvironment.ApplicationID);
            System.Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName); // /LM/W3SVC/178/ROOT-1-131245590641962041

            // System.Console.WriteLine(System.Web.Hosting.HostingEnvironment.ApplicationHost);
            System.Console.WriteLine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath);
            System.Console.WriteLine(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);



            



            // servervariables
            // machineName


            // string IISserverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];

            // string statusCode;
            // QueryKeyValue
            // SessionKeyValue
            // PostKeyValue
            // UserAgentKeyValue

            // if (ex.Data == null) ex.Data = new Dictionary<string, string>();

            while (stack.Count > 0)
            {
                sequence++;

                System.Exception ex = stack.Pop();
                System.Console.WriteLine(ex);



                System.Guid entryUID = System.Guid.NewGuid();

                string type = ex.GetType().Name;
                string typeFullName = ex.GetType().FullName;
                string message = ex.Message;
                string stackTrace = ex.StackTrace;
                string source = ex.Source;
                System.Collections.IDictionary data = ex.Data;

            }

            System.Console.WriteLine(exceptionToLog);
            

        }


        // http://www.codeproject.com/Articles/429167/Activity-Logging-and-Error-Logging-in-ASP-NET
        // http://nlog-project.org/
        // https://github.com/nlog/nlog/wiki/Tutorial
        // NLog supports the following log levels:
        //Trace - very detailed logs, which may include high-volume information such as protocol payloads. This log level is typically only enabled during development
        //Debug - debugging information, less detailed than trace, typically not enabled in production environment.
        //Info - information messages, which are normally enabled in production environment
        //Warn - warning messages, typically for non-critical issues, which can be recovered or which are temporary failures
        //Error - error messages - most of the time these are Exceptions
        //Fatal - very serious errors!
    }


}