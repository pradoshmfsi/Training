using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageUser.Business;
using ManageUser.Utils.UserDetailModels;
namespace WebApplication1
{
    public class DocumentUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                var formData = context.Request.Form;
                int userId = Int32.Parse(formData["userId"]);
                string fname = context.Server.MapPath("~/upload/documents/" + file.FileName);
                file.SaveAs(fname);

                UserDocument newDocument = new UserDocument
                {
                    userId = userId,
                    documentName = file.FileName
                };
                if(UserDetailBusiness.AddDocumentsToDB(newDocument))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(file.FileName);
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Some error occured");
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