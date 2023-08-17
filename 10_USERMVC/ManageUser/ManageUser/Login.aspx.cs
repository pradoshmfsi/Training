using ManageUser.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManageUser
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginUserBtn(object sender, EventArgs e)
        {
            var user = UserDetailBusiness.GetUserByEmail(email.Value);
            if (user == null)
            {
                loginErrorLabel.Text = "Email doesn't exist";
            }
            else if (user.password != password.Value)
            {
                loginErrorLabel.Text = "Invalid Password";
            }
            else
            {
                Session["user"] = user.userId;
                if (BasePage.IsAdmin(user.userId))
                {
                    Response.Redirect("Details.aspx");
                }
                else
                {
                    Response.Redirect("UserForm.aspx?userId=" + user.userId);
                }
            }
        }
    }
}