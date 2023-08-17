using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for ProfileImageHandler
    /// </summary>
    public class ProfileImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse r = context.Response;
            r.ContentType = "image/jpeg";
            string file = context.Request.QueryString["fileSrc"];
            r.WriteFile(file);
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