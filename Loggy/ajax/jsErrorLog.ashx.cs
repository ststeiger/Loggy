
namespace Loggy.ajax
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für jsErrorLog
    /// </summary>
    public class jsErrorLog : System.Web.IHttpHandler
    {


        public class PostData
        {
            public string msg; // Message
            public string name; // Type 
            public string filename;
            public string columnNumber;
            public string lineNumber;
            public string stack; // stacktrace

            public string href;
            public string referrer;

        }


        public class JavaScriptException : System.Exception
        {

            public JavaScriptException() : base() 
            { }


            public JavaScriptException(string message)
                : base(message)
            {
                this.m_Message = message;
            }


            public JavaScriptException(string message, System.Exception innerException)
                : base(message, innerException)
            {
                this.m_Message = message;
            }


            public JavaScriptException(PostData data)
                : base(data.msg)
            {
                this.m_Message = data.msg;
                this.m_StackTrace =
                    "Referer:       " + data.referrer + System.Environment.NewLine +
                    "URL:           " + data.href + System.Environment.NewLine +
                    "Type:          " + data.name + System.Environment.NewLine +
                    "File-Name:     " + data.filename + System.Environment.NewLine +
                    "Line-Number:   " + data.lineNumber + System.Environment.NewLine +
                    "Column-Number: " + data.columnNumber + System.Environment.NewLine +
                    System.Environment.NewLine +
                    "Stacktrace:    " + data.stack + System.Environment.NewLine
                ;
                
                this.m_Source = "JavaScript";
                this.m_Data = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase);
            }



            // protected JavaScriptException(SerializationInfo info, StreamingContext context);


            protected System.Collections.IDictionary m_Data;
            public override System.Collections.IDictionary Data
            {
                get
                {
                    return m_Data;
                }
            }

            protected string m_HelpLink;
            public override string HelpLink
            {

                get
                {
                    return this.m_HelpLink;
                }

                set
                {
                    this.m_HelpLink = value;
                }
            }

            protected string m_Message;
            public override string Message
            {

                get
                {
                    return m_Message;
                }

            }

            protected string m_Source;
            public override string Source
            {

                get
                {
                    return m_Source;
                }

                set
                {
                    this.m_Source = value;
                }
            }

            protected string m_StackTrace;
            public override string StackTrace
            {

                get
                {
                    return m_StackTrace;
                }

            }

            // public virtual Exception GetBaseException();
            // public virtual void GetObjectData(SerializationInfo info, StreamingContext context);

            public override string ToString()
            {
                return null;
            } // End Function ToString 

        } // End Class JavaScriptException 



        public void ProcessRequest(System.Web.HttpContext context)
        {
            // referer is html document.
            string refe = context.Request.UrlReferrer.OriginalString;
            string postData = "Referrer: " + refe + System.Environment.NewLine;

            if (context.Request.Form != null)
            {

                foreach (string key in context.Request.Form.AllKeys)
                {
                    postData += key + ": " + context.Request.Form[key] + System.Environment.NewLine;
                } // Next key 

            } // End if (context.Request.Form != null)

            System.Console.WriteLine(postData);

            if (context.Request.InputStream != null)
            {

                using (System.IO.StreamReader stream = new System.IO.StreamReader(context.Request.InputStream))
                {
                    string json = stream.ReadToEnd();
                    json = JsonPrettyPrinter.Format(json);

                    PostData data = Newtonsoft.Json.JsonConvert.DeserializeObject<PostData>(json);
                    JavaScriptException exJS = new JavaScriptException(data);
                    System.Console.WriteLine(data);
                    System.Console.WriteLine(exJS);
                } // End Using stream 

            } // End if (context.Request.InputStream != null) 

            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        } // End Sub ProcessRequest 


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    } // End Class jsErrorLog : System.Web.IHttpHandler 


} // End Namespace Loggy 


/*

“ ”

https://www.redmine.org/boards/2/topics/22354

/var/lib/redmine/default/files

a (installdir)/apps/redmine/files to something else like a network path.
No configuration in redmine, but you can change the files directory 
to a symlink pointing to the other directory you want your files in.

\\corinet02\Webs$\Servicedesk\files\2016\12





new Http.Json("SaveUpdateSibe.ashx", JSON.stringify(SaveInfo))
        .success(function (r) {
        console.log("saved");
        console.log(r);
    })
    .send();





document.body.insertAdjacentHTML("beforeend", this.sibeTableTemplate);
var onSuccess = function (r) 
{
    r = JSON.parse(r);
    COR.Basic.SIBE.Populate(r.data);
    // r.data[i].SIBE_UID
    // console.log(r.data[0].SIBE_Anlage);
};

// trTitleAnually.style.display = "none";
console.log(COR);
// trTitleAnually.style.display = "none";
var req = new Http.Post("../ajax/Getdatatable.ashx?resource=.Quartalsreport.QREP_SIBE_Liste.sql")
    .success(onSuccess)
    .send()
 ;





new Http.Post("../ajax/GetDataTable.ashx", postData)
    .success(onSuccess)
    .always(cbAlways)
    .send()
;





new Http.RequestChain().add(req1)
        .whenDone(
            function ()
            {
                this.fetchDataIfExists();
            }.bind(this)
        )
        .process()
;


*/
