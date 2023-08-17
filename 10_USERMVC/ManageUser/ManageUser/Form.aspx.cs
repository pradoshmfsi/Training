using ManageUser.Business;
using ManageUser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageUser.Utils.UserDetailModels;
namespace ManageUser
{
    public partial class Form : BasePage
    {
        public bool isAdmin;
        protected void Page_Load(object sender, EventArgs e)
        {
            isAdmin = IsAdmin(UserDetailUtil.GetSession());
            if (!this.IsPostBack)
            {
                BindPropertyOnLoad();
            }
        }
        public void BindPropertyOnLoad()
        {
            AuthorizeRequest();
            if (Request.Params["userId"] != null)
            {
                formHeader.InnerText = "Details";
                submitFormNew.Text = "Edit";
            }
            else
            {
                submitFormNew.Text = "Submit";
                cancelForm.Visible = false;
                deleteForm.Visible = false;
                navContainer.Visible = false;
                notesUserControl.Visible = false;
                documentsUserControl.Visible = false;
            }
            if (!isAdmin)
            {
                cancelForm.Visible = false;
            }
        }

        public void AuthorizeRequest()
        {
            int sessionId = UserDetailUtil.GetSession();
            if (!isAdmin && Request.Params["userId"] != sessionId.ToString())
            {
                Response.Redirect("Form.aspx?userId="+sessionId);
            }
        }

        protected void DeleteBtn(object sender, EventArgs e)
        {
            int userId = Int32.Parse(Request.Params["userId"]);
            if (!UserDetailBusiness.DeleteUser(userId))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage", "alert('Some error occured')", true);
            }
            else
            {
                if (isAdmin)
                {
                    Response.Redirect("Details.aspx");
                }
                Response.Redirect("Login.aspx");
            }

        }

        protected void CancelBtn(object sender, EventArgs e)
        {
            Response.Redirect("Details.aspx");
        }

        [System.Web.Services.WebMethod]
        public static List<Role> GetRoles()
        {
            return UserDetailBusiness.GetRolesAll();
        }

        [System.Web.Services.WebMethod]
        public static List<Country> GetCountries()
        {
            return UserDetailBusiness.GetCountriesAll();
        }

        [System.Web.Services.WebMethod]
        public static List<State> GetStates(int countryId)
        {
            return UserDetailBusiness.GetStates(countryId);
        }

        [System.Web.Services.WebMethod]
        public static UserReceived GetUser(int userId)
        {
            return UserDetailBusiness.GetUserAJAX(userId);
        }
    }
}