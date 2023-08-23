using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMyDoctor.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!BasePage.IsAuthorized())
            {
                Response.Redirect("Login.aspx");
            }
        }

        public static bool IsAuthorized()
        {
            if (HttpContext.Current.Session["userId"] == null)
            {
                return false;
            }
            return true;
        }
    }
}