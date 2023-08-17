using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageUser.Utils.UserDetailModels;
using ManageUser.Business;
using ManageUser.Utils;

namespace ManageUser
{
    public partial class Details : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!IsAdmin(UserDetailUtil.GetSession()))
                {
                    Response.Redirect("PageNotFound.aspx");
                }

                UserDetailsGrid.DataSource = UserDetailBusiness.GetUsersAll().Select(s => new
                {
                    UserId = s.userId,
                    FirstName = s.firstName,
                    LastName = s.lastName,
                    Email = s.email,
                    DOB = s.dob
                }).ToList();
                UserDetailsGrid.DataBind();
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
        protected void SortCommand(Object sender, GridViewSortEventArgs e)
        {
            
        }
        protected void AddUserBtn(object sender, EventArgs e)
        {
            Response.Redirect("UserForm.aspx");
        }
    }
}