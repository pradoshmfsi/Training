using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace BookMyDoctor.Web
{

    public class FileVersioningHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var Response = context.Response;
                string extension = Path.GetExtension(context.Request.Path);
                string actualFileName = context.Request.Path.Split('/').Last().Split('-')[0] + extension;

                string filePath = context.Server.MapPath(actualFileName);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    if (extension == ".js")
                    {
                        Response.AddHeader("Content-Type", "text/javascript");
                    }
                    else
                    {
                        Response.AddHeader("Content-Type", "text/css");
                    }
                    var refresh = new TimeSpan(365, 0, 0, 0);
                    Response.Cache.SetExpires(DateTime.Now.Add(refresh));
                    Response.Cache.SetMaxAge(refresh);
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                    Response.Cache.SetValidUntilExpires(true);
                    Response.Cache.SetLastModified(DateTime.Now);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.Flush();
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }
            }
            catch(Exception ex)
            {
                Utils.Utilities.LogError(ex);
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