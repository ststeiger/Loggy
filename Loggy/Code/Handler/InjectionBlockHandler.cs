
// http://www.codeguru.com/csharp/.net/net_asp/article.php/c19389/HTTP-Handlers-and-HTTP-Modules-in-ASPNET.htm
namespace Loggy
{


	/// <summary>
	/// Summary description for NewHandler.
	/// </summary>
    public class InjectionBlockHandler : System.Web.IHttpHandler
	{


        public InjectionBlockHandler()
        { } // TODO: Add constructor logic here
		

		public void ProcessRequest(System.Web.HttpContext context)
		{
            System.Web.HttpResponse response = context.Response;

            response.ClearHeaders();
            response.ClearContent();
            response.Clear();

            response.ContentType = "text/html";
            response.ContentEncoding = System.Text.Encoding.UTF8;

            string html = @"<!DOCTYPE html>
<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en"">
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />

    <meta http-equiv=""cache-control"" content=""max-age=0"" />
    <meta http-equiv=""cache-control"" content=""no-cache"" />
    <meta http-equiv=""expires"" content=""0"" />
    <meta http-equiv=""expires"" content=""Tue, 01 Jan 1980 1:00:00 GMT"" />
    <meta http-equiv=""pragma"" content=""no-cache"" />

    <meta charset=""utf-8"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />

    <meta http-equiv=""Content-Language"" content=""en"" />
    <meta name=""viewport"" content=""width=device-width,initial-scale=1"" />


    <!--
    <meta name=""author"" content=""name"" />
    <meta name=""description"" content=""description here"" />
    <meta name=""keywords"" content=""keywords,here"" />

    <link rel=""shortcut icon"" href=""favicon.ico"" type=""image/vnd.microsoft.icon"" />
    <link rel=""stylesheet"" href=""stylesheet.css"" type=""text/css"" />
    -->

    <title>Title</title>

    <style type=""text/css"" media=""all"">
        body
        {
            background-color: #0c70b4;
            color: #546775;
            font: normal 400 18px ""PT Sans"", sans-serif;
            -webkit-font-smoothing: antialiased;
        }
    </style>


    <script type=""text/javascript"">
        
    </script>
    
</head>
<body>
    <h1>Hello 15Seconds Reader</h1>
</body>
</html>
";
            html = ResourceHelper.GetResource(typeof(InjectionBlockHandler), "BlackErrorTemplate_Injection_DE.htm");
            response.Write(html);

            context.ApplicationInstance.CompleteRequest();
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
