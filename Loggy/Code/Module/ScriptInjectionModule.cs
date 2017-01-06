
namespace Loggy
{


    public class ScriptInjectionModule : System.Web.IHttpModule
    {


        void System.Web.IHttpModule.Dispose()
        {
            // throw new NotImplementedException();
        }


        void System.Web.IHttpModule.Init(System.Web.HttpApplication context)
        {
            context.PreRequestHandlerExecute += new System.EventHandler(context_PreRequestHandlerExecute);
        }


        protected void context_PreRequestHandlerExecute(object sender, System.EventArgs e)
        {
            System.Web.HttpContext oContext = ((System.Web.HttpApplication)sender).Context;

            //Check if the request handler is a Page object
            if (oContext.Handler is System.Web.UI.Page)
            {
                System.Web.UI.Page objPage = (System.Web.UI.Page)oContext.Handler;

                //Register an event handler for the page init event            
                //AddHandler objPage.Init, New EventHandler(AddressOf Page_Init)

                objPage.Load += new System.EventHandler(Page_Load);
            }

        }


        /*
        protected void Page_Init(object sender, System.EventArgs e)
        {
            //Iterate through the control collection for the page
            foreach (System.Web.UI.Control objControl in ((System.Web.UI.Page)sender).Controls)
            {
                if (objControl is System.Web.UI.HtmlControls.HtmlForm)
                {
                    System.Web.UI.HtmlControls.HtmlForm objForm = (System.Web.UI.HtmlControls.HtmlForm)objControl;
                    //Instantiate and add the control to the page 
                    //using the .ASCX file
                    objForm.Controls.AddAt(0, objForm.Page.LoadControl("Header.ascx"));
                    objForm.Controls.Add(objForm.Page.LoadControl("Footer.ascx"));
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
        } // End Sub Page_Init
        */

        protected void Page_Load(object sender, System.EventArgs e)
        {
            System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;

            if (page != null)
            {
                // string script = "/js/jquery.1.3.2.min.js";

                string strScript = "<script type=\"text/javascript\" language=\"javascript\">" + System.Environment.NewLine;
                ///////////////// strScript += COR.SQL.GetEmbeddedSqlScript("noitceteDedoMskriuQ.js") + System.Environment.NewLine;
                strScript += "</script>" + System.Environment.NewLine + System.Environment.NewLine;

                strScript = strScript.Replace("{", "{{").Replace("}", "}}");
                strScript = strScript.Replace("{{0}}", "{0}");

                string[] str_edoMskriuQ = new string[] {
                    "~/dms/",
                    "q",
                    "u",
                    "i",
                    "r",
                    "k",
                    "s",
                    "m",
                    "o",
                    "d",
                    "e",
                    ".aspx"
                };

                //////// strScript = string.Format(strScript, COR.ASP.NET.ContentUrl(string.Join("", str_edoMskriuQ)));

                page.ClientScript.RegisterClientScriptBlock(page.GetType(), System.Guid.NewGuid().ToString(), strScript);

                //If page.Header IsNot Nothing Then
                //    'Dim scriptTag As String = String.Format("<script language=""javascript"" type=""text/javascript"" src=""{0}""></script>" & Environment.NewLine, script)
                //    page.Header.Controls.Add(New LiteralControl(scriptTag))
                //ElseIf Not page.ClientScript.IsClientScriptIncludeRegistered(page.GetType(), script) Then
                //page.ClientScript.RegisterClientScriptInclude(page.GetType(), script, script)
                //End If
            }

        } // End Sub Page_Load


    } // End Class 


} // End Namespace 
