using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginUserBtn(object sender, EventArgs e)
        {

            using (var dbcontext = new userInfoEntities())
            {
                var user = dbcontext.users.FirstOrDefault(s => s.email == email.Value);
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
}