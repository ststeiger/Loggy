
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

            SomeDbOperations.Test();
        }


    }


}
