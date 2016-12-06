
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Loggy
{


    public partial class _Default : System.Web.UI.Page
    {

        protected override void InitializeCulture()
        {
            // string sprache = "de-CH";
            // System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(sprache);
            // System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(sprache);

            base.InitializeCulture();
        }


        protected void Page_Load(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("Hello");
            System.Diagnostics.Debug.WriteLine("world");


            // http://www.useragentstring.com/pages/useragentstring.php
            // http://www.user-agents.org/
            string ua = System.Web.HttpContext.Current.Request.UserAgent;
            ua = @"Googlebot/2.1 (+http://www.google.com/bot.html)";
            ua = @"Googlebot-Image/1.0";
            ua = @"Baiduspider+(+http://www.baidu.com/search/spider.htm)";
            

            UAParser.Parser parser = UAParser.Parser.GetDefault();
            UAParser.Device dev = parser.ParseDevice(ua);
            UAParser.OS os = parser.ParseOS(ua);
            UAParser.UserAgent pua = parser.ParseUserAgent(ua);
            UAParser.ClientInfo cli = parser.Parse(ua);

            string strua = pua.ToString();
            System.Console.WriteLine(strua);


            System.Console.WriteLine(dev);
            System.Console.WriteLine(os);
            System.Console.WriteLine(pua);
            System.Console.WriteLine(cli);




            
            // System.Web.HttpContext.Current.Request.Browser.IsMobileDevice
            

            // SomeDbOperations.Test();
        }


    }


}
