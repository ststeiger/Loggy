﻿
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Loggy
{


    public partial class _Default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("Hello");
            System.Diagnostics.Debug.WriteLine("world");

            SomeDbOperations.Test();
        }


    }


}
