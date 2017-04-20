
namespace Loggy
{


    // http://stackoverflow.com/questions/14845947/unicode-bug-symbol-that-can-display-inside-browsers
    // http://xahlee.info/comp/unicode_animals.html
    // http://www.fileformat.info/info/unicode/char/1f41b/index.htm
    // http://www.fileformat.info/info/unicode/char/1f41e/index.htm
    // https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRc41xXlE6cGlEZNm-zakIoMPOojxX8m3nDn8z-E0ayplokzX86
    // http://i.stack.imgur.com/xY8bc.jpg
    // http://meta.stackexchange.com/questions/271986/arabic-diacritic-unicode-characters-break-title-links
    // https://www.cnet.com/news/quirky-ios-text-bug-works-in-twitter-snapchat/
    // https://bugtracker.zoho.com/portal/cormanagement
    // http://erraticdev.blogspot.de/2011/01/how-to-correctly-use-ihttpmodule-to.html
    // http://stackoverflow.com/questions/18829677/why-does-this-code-even-compile
    public class Global : System.Web.HttpApplication
    {

        public static System.Collections.Generic.Dictionary<string, ajax.ConnectionInfo>
            ConnectionDix = new System.Collections.Generic.Dictionary<string, ajax.ConnectionInfo>(
                System.StringComparer.OrdinalIgnoreCase);




        // The Application_Start and Application_End methods are special methods that do not represent HttpApplication events. 
        // ASP.NET calls them once for the lifetime of the application domain, not for each HttpApplication instance.
        // IIS uses something called application pools. And each pool can have an arbitrary number of HttpApplication instances. Yes multiple. 
        // Application starting creates all these instances. 
        // Every one of them initializes their own list of modules but only the first one executes the Application_OnStart event handler.
        protected void Application_Start(object sender, System.EventArgs e)
        {
            // moduleError.Init(this);
            System.Console.WriteLine("Redirect");
            System.Console.SetOut(new DebugTextWriter());
            System.Console.SetError(new DebugTextWriter());
            System.Console.WriteLine("Redirected");

            // SomeDbOperations.Test();
        }


        // public static IHttpModule CookieAuth = new ErrorLogModule();
        // public static IHttpModule modul404 = new ErrorLogModule();
        public static System.Web.IHttpModule moduleError = new ErrorLogModule();

        // public static System.Web.IHttpModule moduleSqlScreening = new SqlInjectionScreeningModule();



        // The Application_Start and Application_End methods are special methods that do not represent HttpApplication events. 
        // ASP.NET calls them once for the lifetime of the application domain, not for each HttpApplication instance.
        public override void Init()
        {
            // moduleError.Init(this);
            // moduleSqlScreening.Init(this);
            base.Init();
        }


        protected void Session_Start(object sender, System.EventArgs e)
        {

        }


        protected void Application_BeginRequest(object sender, System.EventArgs e)
        {

        }


        protected void Application_AuthenticateRequest(object sender, System.EventArgs e)
        {

        }


        protected void Application_Error(object sender, System.EventArgs e)
        {

        }


        protected void Session_End(object sender, System.EventArgs e)
        {

        }


        protected void Application_End(object sender, System.EventArgs e)
        {

        }


    } // End Class Global 


} // End Namespace Loggy 