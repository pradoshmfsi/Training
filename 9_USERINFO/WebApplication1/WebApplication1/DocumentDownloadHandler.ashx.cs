using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for DocumentDownloadHandler
    /// </summary>
    public class DocumentDownloadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var Response = context.Response;
            string document = context.Request.QueryString["documentName"];
            string filePath = "D:\\Projects\\Training\\9_USERINFO\\WebApplication1\\WebApplication1\\upload\\documents\\"+ document;
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
 
                Response.Clear();
  
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

                Response.AddHeader("Content-Length", file.Length.ToString());

                Response.Flush();

                Response.TransmitFile(file.FullName);
                Response.End();
            }
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