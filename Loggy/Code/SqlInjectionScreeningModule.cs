
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
    "<script>",
    "</script>",
    "<script",
    "</script",
    //"--",
    //";--",
    //";",
    //"/*",
    //"*/",
    //"@@",
    //"@",
    //"char",
    //"nchar",
    //"varchar",
    //"nvarchar",
    //"alter",
    //"begin",
    //"cast",
    //"create",
    //"cursor",
    //"declare",
    //"delete",
    //"drop",
    //"end",
    //"exec",
    //"execute",
    //"fetch",
    //"insert",
    //"kill",
    //"open",
    //"select",
    //"sys",
    //"sysobjects",
    //"syscolumns",
    //"table",
    //"update"
};


        void System.Web.IHttpModule.Dispose()
        { }


        void System.Web.IHttpModule.Init(System.Web.HttpApplication context)
        {
            if (context == null) throw new System.ArgumentNullException("context", "Could not find HttpApplication");
            

            context.BeginRequest += new System.EventHandler(OnBeginRequest);

            // https://stackoverflow.com/questions/1888016/any-way-to-add-httphandler-programatically-in-net
            // is there any way to programatically add an HttpHandler to an ASP.NET website without adding to the web.config?
            // Obviously you probably want to wrap this in some logic to check for things such as verb, requesting url, etc. 
            context.PostMapRequestHandler += new System.EventHandler(OnPostMapRequestHandler);


            // context.EndRequest += new System.EventHandler(OnEndRequest);

        }


        private void OnBeginRequest(object sender, System.EventArgs e)
        {
        }


        // System.Web.HttpRequest request
        private void CheckInjection(System.Web.HttpRequest request)
        {
            string ip = request.UserHostAddress;


            foreach (string key in request.QueryString)
            {
                CheckInput(request.QueryString[key]);
            }

            foreach (string key in request.Form)
            {
                CheckInput(request.Form[key]);
            }

            foreach (string key in request.Cookies)
            {
                CheckInput(request.Cookies[key].Value);
            }

        }


        private void CheckInput(string parameter)
        {
            for (int i = 0; i <= blackList.Length - 1; i++)
            {

                // if ((parameter.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                if (System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(parameter, blackList[i], 0, System.Globalization.CompareOptions.OrdinalIgnoreCase) != -1)
                {
                    //Handle the discovery of suspicious Sql characters here 

                    //generic error page on your site 
                    // System.Web.HttpContext.Current.Response.Redirect("~/Error.aspx");

                    // System.Web.HttpContext.Current.Response.Clear();
                    // System.Web.HttpContext.Current.Response.ClearHeaders();
                    // System.Web.HttpContext.Current.Response.ClearContent();

                    System.Web.HttpContext.Current.Handler = new MyHandler();
                    // System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
            }
        }


        private void OnEndRequest(object sender, System.EventArgs e)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
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


            CheckInjection(context.Request);


            if (context.Request.Url.AbsolutePath == "/foo.ashx")
            {
                // IHttpHandler myHandler = new MyHandler();
                // context.Handler = myHandler;
                context.Handler = new MyHandler();
            }

        }


    }


}
