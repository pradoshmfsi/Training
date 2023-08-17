using ManageUser.Business;
using ManageUser.Utils.UserDetailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManageUser
{
    public partial class Notes : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!BasePage.IsAdmin(Int32.Parse(Session["user"].ToString()))){
                ifPrivateCheck.Visible = false;
            }
        }
    }
}