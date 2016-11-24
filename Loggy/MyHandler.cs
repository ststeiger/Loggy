
using System;
using System.Web;

// http://www.codeguru.com/csharp/.net/net_asp/article.php/c19389/HTTP-Handlers-and-HTTP-Modules-in-ASPNET.htm
namespace Loggy
{


	/// <summary>
	/// Summary description for NewHandler.
	/// </summary>
	public class MyHandler : IHttpHandler
	{
        public MyHandler()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public void ProcessRequest(System.Web.HttpContext context)
		{
			HttpResponse response = context.Response ;
            response.Write("<html><body><h1>Hello 15Seconds   Reader ");
            response.Write("</body></html>");
		}


		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
        
	}


}