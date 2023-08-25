using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookMyDoctor.Business;
using BookMyDoctor.Utils;
using BookMyDoctor.Utils.Models;
namespace BookMyDoctor.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utilities.IsAuthorized())
            {
                Response.Redirect("DoctorAppointments.aspx");
            }
        }

        protected void LoginUser(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static StandardPostResponseModel AuthorizeUser(string email, string password)
        {
            var response = Utilities.GetErrorResponse();
            try
            {
                UserViewModel newUser = BusinessLogic.GetUserByEmail(email);
                if (newUser == null)
                {
                    response.Data = "Email doesn't exist";
                }
                else if (newUser.Password != password)
                {
                    response.Data = "Invalid Password";
                }
                else
                {
                    HttpContext.Current.Session["userId"] = newUser.UserId;
                    response.IsSuccess = true;
                    response.Data = "Logged In Successfully";

                }
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
            }
            return response;
        }
    }
}