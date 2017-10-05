
namespace Loggy.ajax
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für serializeRequest
    /// </summary>
    public class serializeRequest : System.Web.IHttpHandler
    {


        public class ABC
        {
            public string Hello;
            public string World;
        }


        public void ProcessRequest(System.Web.HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            ABC args = HelperArgs.GetArgs<ABC>(context);
            System.Console.WriteLine(args);
        }
        

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    } // End Class serializeRequest : IHttpHandler 


} // End Namespace Loggy.ajax 
