using System;
using System.Collections.Generic;
using System.Web;

namespace Loggy
{
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

        void IHttpModule.Init(HttpApplication context)
        {
            throw new NotImplementedException();
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

        public class Tuple
        {
            System.Guid First;
            System.Guid Second;
        }



    }
}