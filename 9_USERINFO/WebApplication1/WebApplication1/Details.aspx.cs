using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                using (var dbcontext = new userInfoEntities())
                {

                    GridView1.DataSource = dbcontext.users.Select(s => new {
                        userId=s.userId,
                        fname = s.fname,
                        lname = s.lname,
                        email=s.email,
                        dob=s.dob
                    }).ToList();
                    GridView1.DataBind();
                }
            }
            
        }
        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                string url = "WebForm1?userId=" + userId;
                Response.Redirect(url);
            }
        }
        protected void AddUserBtn(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx");
        }
    }
}