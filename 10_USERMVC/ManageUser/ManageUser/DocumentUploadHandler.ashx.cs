using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageUser.Business;
using ManageUser.Utils.UserDetailModels;
using System.Web.SessionState;
namespace ManageUser
{
    public class DocumentUploadHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];

                var formData = context.Request.Form;
                int userId = Int32.Parse(formData["userId"]);

                string fileNameActual = file.FileName + DateTime.Now.ToString().Replace(':', '-');
                string fname = context.Server.MapPath("~/upload/documents/" + fileNameActual);
                file.SaveAs(fname);

                string email = UserDetailBusiness.GetUser(Int32.Parse(context.Session["user"].ToString())).email;
                UserDocument newDocument = new UserDocument
                {
                    userId = userId,
                    documentName = file.FileName,
                    documentNameActual = fileNameActual,
                    createdOn = DateTime.Now,
                    createdBy = email,
                };

                if(UserDetailBusiness.AddDocumentsToDB(newDocument))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(fileNameActual);
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(null);
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