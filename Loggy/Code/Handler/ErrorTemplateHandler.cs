
// http://www.codeguru.com/csharp/.net/net_asp/article.php/c19389/HTTP-Handlers-and-HTTP-Modules-in-ASPNET.htm
namespace Loggy
{


	/// <summary>
	/// Summary description for NewHandler.
	/// </summary>
    public class ErrorTemplateHandler : System.Web.IHttpHandler
	{


        public ErrorTemplateHandler()
        { } // TODO: Add constructor logic here


		public void ProcessRequest(System.Web.HttpContext context)
		{
            System.Web.HttpResponse response = context.Response;

            response.ClearHeaders();
            response.ClearContent();
            response.Clear();

            response.ContentType = "text/html";
            response.ContentEncoding = System.Text.Encoding.UTF8;


            // <p id="errorMessage">@error.Type: @error.Message</p>
            // <span>@error.StackTrace</span>
            string res = ResourceHelper.GetResource(typeof(ErrorTemplateHandler), "ErrorTemplate.htm");
            response.Write(res);

            // response.Write("<html><body><h1>Hello 15Seconds   Reader ");
            // response.Write("</body></html>");
		} // End Sub ProcessRequest 


		public bool IsReusable
		{
			get
			{
				return true;
			}

        } // End Property IsReusable 


    } // End Class MyHandler : IHttpHandler 


} // End Namespace Loggy 
