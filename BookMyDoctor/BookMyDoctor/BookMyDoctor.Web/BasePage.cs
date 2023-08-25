using BookMyDoctor.Utils;
using System;

namespace BookMyDoctor.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Utilities.IsAuthorized())
            {
                Response.Redirect("Login.aspx");
            }
        }       
    }
}