using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public class UserDetails
    {
        public string firstName;
        public string lastName;
        public string email;
        public string password;
        public string gender;
        public string dob;
        public List<string> hobbies;

    }
    public partial class UserFormNew : BasePage
    {
        public bool isAdmin;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindPropertyOnLoad();
            }
            BindProperty();
        }

        public void BindPropertyOnLoad()
        {
            AuthorizeRequest();
            BindControlData();            
            if (Request.Params["userId"] != null)
            {              
                formHeader.InnerText = "Details";
                submitForm.Text = "Edit";
                BindDataFromDB();
            }
            else
            {
                submitForm.Text = "Submit";
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
            isAdmin = IsAdmin(GetUserId());
            notesUserControl.IsAdmin = isAdmin;
            if (!isAdmin && Request.Params["userId"] != GetUserId().ToString()) {
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
            else
            {
                notesUserControl.UserId = Int32.Parse(Request.Params["userId"]);
                email.Attributes.Add("isedit", "true");
            }
        }

        public void BindDataFromDB()
        {
            int userId = Int32.Parse(Request.Params["userId"]);
            using (var dbcontext = new userInfoEntities())
            {
                var user = dbcontext.users.FirstOrDefault(i => i.userId == userId);
                if (user == null)
                {
                    Response.Redirect("PageNotFound.aspx");
                }
                firstName.Value = user.fname;
                lastName.Value = user.lname;
                email.Value = user.email;

                date.Value = user.dob.ToString("yyyy-MM-dd");
                if (user.gender == "male")
                {
                    male.Checked = true;
                }
                else
                {
                    female.Checked = true;
                }
                var roles = dbcontext.userRoles
                    .Where(i => i.userId == user.userId)
                    .Select(i => i.roleId).ToList();

                foreach (ListItem item in chklistUserRole.Items)
                {
                    if (roles.Contains(Int32.Parse(item.Value)))
                    {
                        item.Selected = true;
                    }
                }

                var hobbies = dbcontext.userHobbies
                    .Where(i => i.userId == user.userId)
                    .Select(i => i.hobby).ToList();

                hobby.Value = string.Join(", ", hobbies);

                profileImage.Src = "upload/" + user.profilePic;

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
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hobbyPopulate", "$(document).ready(populateHobby());", true);
        }

        public void BindControlData()
        {
            using (var dbcontext = new userInfoEntities())
            {
                chklistUserRole.DataSource = dbcontext.roles.ToList();
                chklistUserRole.DataTextField = "roleName";
                chklistUserRole.DataValueField = "roleId";
                chklistUserRole.DataBind();
                PopulateCountries(presentCountry);
                PopulateCountries(permanentCountry);
            }
        }

        public void PopulateStates(DropDownList country, DropDownList state)
        {
            using (var dbcontext = new userInfoEntities())
            {
                string countryIdString = country.SelectedItem.Value;

                if (countryIdString != "")
                {
                    int countryId = Int32.Parse(countryIdString);
                    state.DataSource = dbcontext.states.Where(i => i.countryId == countryId).ToList();
                    state.DataTextField = "stateName";
                    state.DataValueField = "stateId";
                    state.DataBind();
                }

            }
            state.Items.Insert(0, new ListItem("--Select State--", ""));
        }

        public void PopulateCountries(DropDownList country)
        {
            using (var dbcontext = new userInfoEntities())
            {
                country.DataSource = dbcontext.countries.ToList();
                country.DataTextField = "countryName";
                country.DataValueField = "countryId";
                country.DataBind();
            }
            country.Items.Insert(0, new ListItem("Select Country", ""));

        }

        public void AddUserRoles(userInfoEntities dbcontext, int userId)
        {
            foreach (ListItem item in chklistUserRole.Items)
            {
                if (item.Selected)
                {
                    userRole newUserRole = new userRole
                    {
                        userId = userId,
                        roleId = Int32.Parse(item.Value)
                    };
                    dbcontext.userRoles.Add(newUserRole);
                }
            }
        }

        public void AddUserHobbies(userInfoEntities dbcontext, int userId)
        {
            string[] hobbyList = hobby.Value.Split(',');
            foreach (string hobby in hobbyList)
            {
                userHobby newUserHobby = new userHobby
                {
                    userId = userId,
                    hobby = hobby.Trim()
                };
                dbcontext.userHobbies.Add(newUserHobby);
            }

        }

        public void AddProfilePicImage(HtmlInputFile profilePic, user newUser)
        {
            string SaveLocation = Server.MapPath("upload") + "\\" + profilePic.PostedFile.FileName;
            profilePic.PostedFile.SaveAs(SaveLocation);
            newUser.profilePic = profilePic.PostedFile.FileName;
        }

        public void AddPassword(HtmlInputPassword password, user newUser)
        {
            if (Request.Params["userId"] != null && password.Value == "")
            {
                return;
            }
            newUser.password = password.Value;

        }

        public void AddUserToObject(user newUser)
        {
            newUser.fname = firstName.Value;
            newUser.lname = lastName.Value;
            newUser.email = email.Value;
            AddPassword(password, newUser);
            newUser.gender = male.Checked ? male.Value : female.Value;
            newUser.dob = DateTime.Parse(date.Value); ;

            newUser.presentAddressLine1 = presentAddressLine1.Value;
            newUser.presentAddressLine2 = presentAddressLine2.Value;
            newUser.presentCountryId = Int32.Parse(presentCountry.SelectedItem.Value);
            newUser.presentStateId = Int32.Parse(presentState.SelectedItem.Value); ;
            newUser.presentCity = presentCity.Value;
            newUser.presentPin = presentPin.Value;

            newUser.permanentAddressLine1 = permanentAddressLine1.Value;
            newUser.permanentAddressLine2 = permanentAddressLine2.Value;
            newUser.permanentCountryId = Int32.Parse(permanentCountry.SelectedItem.Value);
            newUser.permanentStateId = Int32.Parse(permanentState.SelectedItem.Value);
            newUser.permanentCity = permanentCity.Value;
            newUser.permanentPin = permanentPin.Value;
        }

        public void SubmitUser()
        {
            using (var dbcontext = new userInfoEntities())
            {
                user newUser = new user();
                AddProfilePicImage(profilePic, newUser);
                AddUserToObject(newUser);

                dbcontext.users.Add(newUser);
                var debugUserId = newUser.userId;
                AddUserRoles(dbcontext, newUser.userId);
                AddUserHobbies(dbcontext, newUser.userId);
                dbcontext.SaveChanges();
            }
            Response.Redirect("Details.aspx");
        }

        public void EditUser()
        {
            int userId = Int32.Parse(Request.Params["userId"]);
            using (var dbcontext = new userInfoEntities())
            {

                var user = dbcontext.users.FirstOrDefault(i => i.userId == userId);
                if ((profilePic.PostedFile != null) && (profilePic.PostedFile.ContentLength > 0))
                {
                    AddProfilePicImage(profilePic, user);
                }
                AddUserToObject(user);
                var userRoleList = dbcontext.userRoles.Where(i => i.userId == userId).ToList();
                dbcontext.userRoles.RemoveRange(userRoleList);
                AddUserRoles(dbcontext, userId);

                var userHobbyList = dbcontext.userHobbies.Where(i => i.userId == userId).ToList();
                dbcontext.userHobbies.RemoveRange(userHobbyList);
                AddUserHobbies(dbcontext, userId);
                dbcontext.SaveChanges();
            }
            if (isAdmin)
            {
                Response.Redirect("Details.aspx");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "updateMessage", "alert('Updated successfully')", true);
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

        protected void SubmitUserBtn(object sender, EventArgs e)
        {
            if (Request.Params["userId"] != null)
            {
                EditUser();
            }
            else
            {
                SubmitUser();
            }
        }

        protected void DeleteBtn(object sender, EventArgs e)
        {
            int userId = Int32.Parse(Request.Params["userId"]);
            using (var dbcontext = new userInfoEntities())
            {
                var user = dbcontext.users.FirstOrDefault(i => i.userId == userId);
                dbcontext.users.Remove(user);
                dbcontext.SaveChanges();
            }
            if (isAdmin)
            {
                Response.Redirect("Details.aspx");
            }
            Response.Redirect("Login.aspx");
        }

        protected void CancelBtn(object sender, EventArgs e)
        {
            Response.Redirect("Details.aspx");
        }

        [System.Web.Services.WebMethod]
        public static string CheckIfEmailAlreadyPresent(string email)
        {
            if (IsAuthorized())
            {
                using (var dbcontext = new userInfoEntities())
                {
                    var users = dbcontext.users.FirstOrDefault(s => s.email == email);
                    if (users == null)
                    {
                        return "";
                    }
                    return "Already exists";
                }
            }
            else
            {
                return null;
            }

        }

        [System.Web.Services.WebMethod]
        public static List<string> GetDocuments(int userId)
        {
            if (IsAuthorized())
            {
                using (var dbcontext = new userInfoEntities())
                {
                    var documents = dbcontext.userDocuments.Where(s => s.userId == userId).Select(s => s.documentName).ToList();
                    return documents;
                }
            }
            else
            {
                return null;
            }
        }

        //[System.Web.Services.WebMethod]
        //public void SubmitUserByAjax(CustomClass Object)
        //{
                
        //}
    }
}