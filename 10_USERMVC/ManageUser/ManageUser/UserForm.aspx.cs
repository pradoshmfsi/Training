using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using ManageUser.Business;
using ManageUser.Utils.UserDetailModels;
using Microsoft.Ajax.Utilities;
using System.Runtime.Remoting.Contexts;
using ManageUser.Utils;

namespace ManageUser
{
    public partial class UserForm : BasePage
    {
        public bool isAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindProperty();
            if (!this.IsPostBack)
            {
                BindPropertyOnLoad();
            }

        }

        public void BindPropertyOnLoad()
        {
            AuthorizeRequest();
            BindControlData();
            if (Request.Params["userId"] != null)
            {
                formHeader.InnerText = "Details";
                submitFormNew.Text = "Edit";
                BindDataFromDB();
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

            //notesUserControl.IsAdmin = isAdmin;
            if (!isAdmin && Request.Params["userId"] != UserDetailUtil.GetSession().ToString())
            {
                Response.Redirect("PageNotFound.aspx");
            }
        }

        public void BindProperty()
        {
            if (Request.Params["userId"] == null)
            {
                profilePic.Attributes.Add("isrequired", "true");
                password.Attributes.Add("isrequired", "#password|#passwordTitle|#passwordTitle span|.");
                confirmPassword.Attributes.Add("isrequired", "#confirmPassword|#confirmPasswordTitle|#confirmPasswordTitle span|.");
            }
            isAdmin = IsAdmin(UserDetailUtil.GetSession());
        }

        public void BindDataFromDB()
        {
            int userId = Int32.Parse(Request.Params["userId"]);

            User user = UserDetailBusiness.GetUser(userId);
            if (user == null)
            {
                Response.Redirect("PageNotFound.aspx");
            }
            firstName.Value = user.firstName;
            lastName.Value = user.lastName;
            email.Value = user.email;

            dob.Value = user.dob.ToString("yyyy-MM-dd");
            if (user.gender == "male")
            {
                male.Checked = true;
            }
            else
            {
                female.Checked = true;
            }

            var roles = UserDetailBusiness.GetUserRoleId(user.userId);

            foreach (ListItem item in chklistUserRole.Items)
            {
                if (roles.Contains(Int32.Parse(item.Value)))
                {
                    item.Selected = true;
                }
            }

            //var hobbies = dbcontext.userHobbies
            //    .Where(i => i.userId == user.userId)
            //    .Select(i => i.hobby).ToList();

            hobby.Value = UserDetailBusiness.GetHobbies(user.userId);

            profileImage.Src = "upload/" + user.profilePicActual;

            presentAddressLine1.Value = user.presentAddressLine1;
            presentAddressLine2.Value = user.presentAddressLine2;

            presentCountry.SelectedValue = user.presentCountryId.ToString();
            PopulateStates(presentCountry, presentState);
            presentState.SelectedValue = user.presentStateId.ToString();

            presentCity.Value = user.presentCity;
            presentPin.Value = user.presentPin;

            permanentAddressLine1.Value = user.permanentAddressLine1;
            permanentAddressLine2.Value = user.permanentAddressLine2;

            permanentCountry.SelectedValue = user.permanentCountryId.ToString();
            PopulateStates(permanentCountry, permanentState);
            permanentState.SelectedValue = user.permanentStateId.ToString();

            permanentCity.Value = user.permanentCity;
            permanentPin.Value = user.permanentPin;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "hobbyPopulate", "$(document).ready(populateHobby());", true);
        }

        public void BindControlData()
        {
            chklistUserRole.DataSource = UserDetailBusiness.GetRolesAll();
            chklistUserRole.DataTextField = "roleName";
            chklistUserRole.DataValueField = "roleId";
            chklistUserRole.DataBind();
            PopulateCountries(presentCountry);
            PopulateCountries(permanentCountry);

        }

        public void PopulateStates(DropDownList country, DropDownList state)
        {

            string countryIdString = country.SelectedItem.Value;
            if (countryIdString != "")
            {
                int countryId = Int32.Parse(countryIdString);
                state.DataSource = UserDetailBusiness.GetStates(countryId);
                state.DataTextField = "stateName";
                state.DataValueField = "stateId";
                state.DataBind();
            }
            state.Items.Insert(0, new ListItem("--Select State--", ""));
        }

        public void PopulateCountries(DropDownList country)
        {

            country.DataSource = UserDetailBusiness.GetCountriesAll();
            country.DataTextField = "countryName";
            country.DataValueField = "countryId";
            country.DataBind();

            country.Items.Insert(0, new ListItem("Select Country", ""));

        }

        protected void PopulatePermanentAsPresent(object sender, EventArgs e)
        {
            if (ifPresentSameAsPermanent.Checked)
            {
                permanentAddressLine1.Value = presentAddressLine1.Value;
                permanentAddressLine2.Value = presentAddressLine2.Value;
                if (presentCountry.SelectedValue != "")
                {
                    permanentCountry.SelectedValue = presentCountry.SelectedValue;
                    PopulateStates(permanentCountry, permanentState);
                }

                permanentState.SelectedValue = presentState.SelectedValue;
                permanentCity.Value = presentCity.Value;
                permanentPin.Value = presentPin.Value;
            }
        }

        protected void PopulateStatesPresent(object sender, EventArgs e)
        {
            PopulateStates(presentCountry, presentState);
        }

        protected void PopulateStatesPermanent(object sender, EventArgs e)
        {
            PopulateStates(permanentCountry, permanentState);
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
        public static string CheckIfEmailAlreadyPresent(string email,int userId)
        {
            if (IsAuthorized())
            {
                AuthUser user = UserDetailBusiness.GetUserByEmail(email);
                if (user != null)
                {
                    if(userId!=0 && userId == user.userId)
                    {
                        return "";
                    }
                    return "Already Exists";
                }
                else
                {
                    return "";
                }
            }
            return null;
        }

        [System.Web.Services.WebMethod]
        public static List<UserDocument> GetDocuments(int userId)
        {
            if (IsAuthorized())
            {
                var documents = UserDetailBusiness.GetDocuments(userId);
                return documents;
            }
            else
            {
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        public static List<UserNote> GetNotes(int userId)
        {
            if (IsAuthorized())
            {
                int isAdmin = IsAdmin(Int32.Parse(HttpContext.Current.Session["user"].ToString()))?1:0;
                var notes = UserDetailBusiness.GetUserNotes(userId,isAdmin);
                return notes;
            }
            else
            {
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        public static string AddUser(UserReceived user)
        {
            if (IsAuthorized())
            {
                if (UserDetailBusiness.AddUserToDB(user))
                {
                    if (user.userId != 0)
                    {
                        return "User edited successfully";
                    }
                    return "User saved successfully";
                }
                else
                {
                    return "Some error occured";
                }
            }
            else
            {
                HttpContext.Current.Response.StatusCode = 401;
                return null;
            }

        }

        [System.Web.Services.WebMethod]
        public static UserNote AddNoteToDB(UserNote newNote)
        {
            if (IsAuthorized())
            {
                newNote.createdBy = UserDetailBusiness.GetUser(Int32.Parse(HttpContext.Current.Session["user"].ToString())).email;
                newNote.createdOn = DateTime.Now;
                try
                {
                    if (UserDetailBusiness.AddNotesToDB(newNote))
                    {
                        return newNote;
                    }
                }
                catch(Exception ex)
                {
                    UserDetailUtil.LogError(ex);
                }               
            }
            else
            {
                HttpContext.Current.Response.StatusCode = 401;
            }
            return null;
        }
    }
}