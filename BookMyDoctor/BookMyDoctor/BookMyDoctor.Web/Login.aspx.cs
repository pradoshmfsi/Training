using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookMyDoctor.Business;
using BookMyDoctor.Utils.Models;
namespace BookMyDoctor.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginUser(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static StandardPostResponseModel AuthorizeUser(string email,string password)
        {
            UserViewModel newUser = BusinessLogic.GetUserByEmail(email);
            StandardPostResponseModel response = new StandardPostResponseModel { IsSuccess = false, Data = "Some error occured" };
            if(newUser == null)
            {
                response.Data="Email doesn't exist";
            }
            else if(newUser.Password != password)
            {
                response.Data="Invalid Password";
            }
            else
            {
                HttpContext.Current.Session["userId"] = newUser.UserId;
                response.IsSuccess = true;
                response.Data = "Logged In Successfully";

            }
            return response;
        }
    }
}