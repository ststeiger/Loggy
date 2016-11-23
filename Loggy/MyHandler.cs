
using System;
using System.Web;


namespace Loggy
{


	/// <summary>
	/// Summary description for NewHandler.
	/// </summary>
	public class NewHandler : IHttpHandler
	{
		public NewHandler()
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