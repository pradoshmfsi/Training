using ManageUser.Business;
using ManageUser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageUser
{
    /// <summary>
    /// Summary description for ProfilePicUpload
    /// </summary>
    public class ProfilePicUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                try
                {
                    int dotPos = file.FileName.LastIndexOf('.');
                    string fileActualName = file.FileName.Substring(0, dotPos) + DateTime.Now.ToString().Replace(':', '-') + file.FileName.Substring(dotPos);
                    string path = context.Server.MapPath("~/upload/" + fileActualName);
                    file.SaveAs(path);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(fileActualName);
                }
                catch(Exception ex)
                {
                    UserDetailUtil.LogError(ex);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("");
                }
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