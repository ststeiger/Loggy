using System;
using System.Collections.Generic;
using System.Web;

namespace Loggy.ajax
{
    /// <summary>
    /// Summary description for json
    /// </summary>
    public class json : IHttpHandler
    {

        public class JsonResult
        {
            public bool error = true;
            public string message = "foo";
            public int status = 123;
        }


        public void ProcessRequest(HttpContext context)
        {
            // context.Response.ContentType = "text/plain"; context.Response.Write("Hello World");

            throw new Exception("omg a bug");


            context.Response.ContentType = "application/json";
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new JsonResult()));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}