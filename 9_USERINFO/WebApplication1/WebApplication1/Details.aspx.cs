using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Details : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)   
            {
                if (!IsAdmin(GetUserId()))
                {
                    Response.Redirect("PageNotFound.aspx");
                }
                using (var dbcontext = new userInfoEntities())
                {
                    UserDetailsGrid.DataSource = dbcontext.users.Select(s => new {
                        UserId=s.userId,
                        FirstName = s.fname,
                        LastName = s.lname,
                        Email=s.email,
                        DOB=s.dob
                    }).ToList();
                    UserDetailsGrid.DataBind();
                }
            }           
        }
        protected void EditRowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(UserDetailsGrid.DataKeys[index].Values[0]);
                string url = "UserForm?userId=" + userId;
                Response.Redirect(url);
            }
        }
        protected void AddUserBtn(object sender, EventArgs e)
        {
            Response.Redirect("UserForm.aspx");
        }
    }
}