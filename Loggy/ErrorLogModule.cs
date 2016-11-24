using System;
using System.Collections.Generic;
using System.Web;

namespace Loggy
{

    // https://code.tutsplus.com/tutorials/preventing-xss-in-aspnet--cms-21801

    public class ErrorLogModule : System.Web.IHttpModule
    {
        public ErrorLogModule()
		{
			// TODO: Add constructor logic here

		}


        void IHttpModule.Dispose()
        {
            throw new NotImplementedException();
        }


        // http://www.codeguru.com/csharp/.net/net_asp/article.php/c19389/HTTP-Handlers-and-HTTP-Modules-in-ASPNET.htm
        void System.Web.IHttpModule.Init(HttpApplication context)
        {
            if (context == null) throw new ArgumentNullException("context", "Could not find HttpApplication");
            context.Error += OnError;

            context.BeginRequest += new System.EventHandler(OnBeginRequest);

            // https://stackoverflow.com/questions/1888016/any-way-to-add-httphandler-programatically-in-net
            // is there any way to programatically add an HttpHandler to an ASP.NET website without adding to the web.config?
            // Obviously you probably want to wrap this in some logic to check for things such as verb, requesting url, etc. 
            context.PostMapRequestHandler += new System.EventHandler(OnPostMapRequestHandler);
            

            throw new NotImplementedException();
        }


        private void OnBeginRequest(object sender, System.EventArgs e)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
        }



        private void OnPostMapRequestHandler(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            // IHttpHandler myHandler = new MyHandler();
            // context.Handler = myHandler;
            context.Handler = new MyHandler();
        }




        protected void onError(object sender, System.EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            HttpException tempError = context.Server.GetLastError() as HttpException;

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



        public virtual void OnError(object sender, EventArgs args)
        {
            HttpApplication app = (HttpApplication) sender;
            System.Exception ex = app.Server.GetLastError();



            LogException(ex);
        }   


        public virtual void LogException(System.Exception ex)
        {
            System.Guid errorUID = System.Guid.NewGuid();
            // System.Web.HttpContext.Current.Application.

            // if (ex.Data == null) ex.Data = new Dictionary<string, string>();

            // OnBeforeLog
            // Log SQL-Command with parameters



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
            // string statusCode;

            // QueryKeyValue
            // SessionKeyValue
            // PostKeyValue
            // UserAgentKeyValue

            System.DateTime date = System.DateTime.Now;
            // servervariables
            // machineName

            LogException(ex, errorUID);
            // Trace.WriteLine // echo the error for local debugging
            // OnAfterLog
        }

        
        // Format:
        // ExcetionType: ExceptionMessage; Verdana 13.5, color: rgb(128 0 0) bg: white 
        // Generated: Sat, 17 May 2014 11:30:07 GMT bg: white;

        // Trace Courier New, 10, bg: #FFFFCC [RGB(255, 255, 204)]

        public virtual int LogException(System.Exception ex, System.Guid errorUID)
        {
            int sequence = 0;

            if (ex.InnerException != null)
            {
                sequence = LogException(ex.InnerException, errorUID);
            }

            sequence++;

            System.Guid entryUID = System.Guid.NewGuid();

            string type = ex.GetType().Name;
            string typeFullName = ex.GetType().FullName;
            string message = ex.Message;
            string stackTrace = ex.StackTrace;
            string source = ex.Source;
            System.Collections.IDictionary data = ex.Data;

            return sequence;
        }


        public static void IterativeLogger(System.Exception ex)
        {
            System.Collections.Generic.Stack<Exception> stack = new Stack<Exception>();

            while (ex != null)
            { 
                stack.Push(ex);
                ex = ex.InnerException;
            }


            for (System.Exception ex2 = stack.Pop(); ex2 != null; ex2 = stack.Pop())
            { 
            
            }
            

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