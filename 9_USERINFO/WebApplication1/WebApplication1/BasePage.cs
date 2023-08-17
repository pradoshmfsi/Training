using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public class BasePage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        public int GetUserId()
        {
            return Int32.Parse(Session["user"].ToString());
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
            using (var dbcontext = new userInfoEntities())
            {
                if (dbcontext.userRoles.Where(s => s.userId == userId).Select(i => i.roleId).Contains(4))
                {
                    return true;
                }
            }
            return false;
        }
        public static string GetUserName(int userId)
        {
            using (var dbcontext = new userInfoEntities())
            {
                return dbcontext.users.FirstOrDefault(s => s.userId == userId).fname;
            }
        }
    }
}