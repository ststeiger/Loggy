
namespace Loggy.ajax
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für jsErrorLog
    /// </summary>
    public class jsErrorLog : System.Web.IHttpHandler
    {

        class PostData
        {
            public string e;
            public string msg;
            public string name;
            public string filename;
            public string columnNumber;
            public string lineNumber;
            public string stack;
        }


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

            System.Console.WriteLine(refe);
            System.Console.WriteLine(postData);

            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }


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
