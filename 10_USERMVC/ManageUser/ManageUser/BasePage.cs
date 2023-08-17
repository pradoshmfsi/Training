using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageUser.Business;
using ManageUser.Utils.UserDetailModels;
namespace ManageUser
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
            if (HttpContext.Current.Session["user"] == null)
            {
                return false;
            }
            return true;
        }

        public static bool IsAdmin(int userId)
        {
            return UserDetailBusiness.IsAdmin(userId);
        }

        public static string GetUserName(int userId)
        {
            return UserDetailBusiness.GetUser(userId).firstName;
        }
    }
}