using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindControlData();
                if (Request.Params["userId"] != null)
                {
                    int userId = Int32.Parse(Request.Params["userId"]);
                    submitForm.Text = "Edit";
                    BindData();
                }
                else
                {
                    submitForm.Text = "Submit";
                    cancelForm.Visible = false;
                    deleteForm.Visible = false;
                }
            }
            IsEditOrSubmit();
        }
        public void IsEditOrSubmit()
        {
            if (Request.Params["userId"] != null)
            {
                submitForm.Click += new EventHandler(this.EditBtn);
            }
            else
            {
                submitForm.Click += new EventHandler(this.SubmitBtn);
                dp.Attributes.Add("isrequired", "true");
            }
        }

        public void BindData()
        {
            int userId = Int32.Parse(Request.Params["userId"]);
            using (var dbcontext = new userInfoEntities())
            {
                var user = dbcontext.users.FirstOrDefault(i => i.userId == userId);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hobbyPopulate", "$(document).ready(populateHobby());", true);

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
        protected void PopulatePermanentAsPresent(object sender, EventArgs e)
        {
            if (ifPresentSameAsPermanent.Checked)
            {
                permanentAddressLine1.Value = presentAddressLine1.Value;
                permanentAddressLine2.Value = presentAddressLine2.Value;
                if (presentCountry.SelectedValue!="")
                {
                    permanentCountry.SelectedValue = presentCountry.SelectedValue;
                    PopulateStates(permanentCountry, permanentState);
                }
                
                permanentState.SelectedValue = presentState.SelectedValue;
                permanentCity.Value = presentCity.Value;
                permanentPin.Value = presentPin.Value;
            }
        }
        public void PopulateStates(DropDownList country, DropDownList state)
        {
            using (var dbcontext = new userInfoEntities())
            {
                string countryString = country.SelectedItem.Value;

                if (countryString != "")
                {
                    int countryId = Int32.Parse(countryString);
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
        protected void PopulateStatesPresent(object sender, EventArgs e)
        {
            PopulateStates(presentCountry, presentState);
        }
        protected void PopulateStatesPermanent(object sender, EventArgs e)
        {
            PopulateStates(permanentCountry, permanentState);
        }

        protected void SubmitBtn(object sender, EventArgs e)
        {
            using (var dbcontext = new userInfoEntities())
            {
                var user = dbcontext.users;
                string genderValue = "";
                if (male.Checked)
                {
                    genderValue = male.Value;
                }
                else
                {
                    genderValue = female.Value;
                }

                string fname = firstName.Value;

                int presentCountryId = Int32.Parse(presentCountry.SelectedItem.Value);
                int presentStateId = Int32.Parse(presentState.SelectedItem.Value);

                int permanentCountryId = Int32.Parse(permanentCountry.SelectedItem.Value);
                int permanentStateId = Int32.Parse(permanentState.SelectedItem.Value);

                string fn = System.IO.Path.GetFileName(dp.PostedFile.FileName);
                string SaveLocation = Server.MapPath("upload") + "\\" + fn;
                dp.PostedFile.SaveAs(SaveLocation);

                DateTime dobValue = DateTime.Parse(date.Value);

                user u = new user
                {
                    fname = firstName.Value,
                    lname = lastName.Value,
                    email = email.Value,
                    gender = genderValue,
                    dob = dobValue,
                    profilePic = dp.PostedFile.FileName,
                    presentAddressLine1 = presentAddressLine1.Value,
                    presentAddressLine2 = presentAddressLine2.Value,
                    presentCountryId = presentCountryId,
                    presentStateId = presentStateId,
                    presentCity = presentCity.Value,
                    presentPin = presentPin.Value,
                    permanentAddressLine1 = permanentAddressLine1.Value,
                    permanentAddressLine2 = permanentAddressLine2.Value,
                    permanentCountryId = permanentCountryId,
                    permanentStateId = permanentStateId,
                    permanentCity = permanentCity.Value,
                    permanentPin = permanentPin.Value,
                };

                dbcontext.users.Add(u);
                

                foreach (ListItem item in chklistUserRole.Items)
                {
                    if (item.Selected)
                    {
                        var userRole = dbcontext.userRoles;

                        userRole newUserRole = new userRole
                        {
                            userId = u.userId,
                            roleId = Int32.Parse(item.Value)
                        };
                        dbcontext.userRoles.Add(newUserRole);
                    }

                }
                string[] hobbyList = hobby.Value.Split(',');
                foreach (string hobby in hobbyList)
                {
                    var userHobby = dbcontext.userHobbies;

                    userHobby newUserHobby = new userHobby
                    {
                        userId = u.userId,
                        hobby = hobby
                    };
                    dbcontext.userHobbies.Add(newUserHobby);
                }
                dbcontext.SaveChanges();
            }
            Response.Redirect("Details.aspx");


        }
        protected void EditBtn(object sender, EventArgs e)
        {
            int userId = Int32.Parse(Request.Params["userId"]);
            using (var dbcontext = new userInfoEntities())
            {
                var user = dbcontext.users.FirstOrDefault(i => i.userId == userId);
                string genderValue = "";
                if (male.Checked)
                {
                    genderValue = male.Value;
                }
                else
                {
                    genderValue = female.Value;
                }

                string fname = firstName.Value;

                int presentCountryId = Int32.Parse(presentCountry.SelectedItem.Value);

                int presentStateId = Int32.Parse(presentState.SelectedItem.Value);


                int permanentCountryId = Int32.Parse(permanentCountry.SelectedItem.Value);
                int permanentStateId = Int32.Parse(permanentState.SelectedItem.Value);

                if ((dp.PostedFile != null) && (dp.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(dp.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("upload") + "\\" + fn;

                    dp.PostedFile.SaveAs(SaveLocation);
                    user.profilePic = dp.PostedFile.FileName;
                }



                DateTime dobValue = DateTime.Parse(date.Value);
                user.fname = firstName.Value;
                user.lname = lastName.Value;
                user.email = email.Value;
                user.gender = genderValue;
                user.dob = dobValue;
                
                user.presentAddressLine1 = presentAddressLine1.Value;
                user.presentAddressLine2 = presentAddressLine2.Value;
                user.presentCountryId = presentCountryId;
                user.presentStateId = presentStateId;
                user.presentCity = presentCity.Value;
                user.presentPin = presentPin.Value;
                user.permanentAddressLine1 = permanentAddressLine1.Value;
                user.permanentAddressLine2 = permanentAddressLine2.Value;
                user.permanentCountryId = permanentCountryId;
                user.permanentStateId = permanentStateId;
                user.permanentCity = permanentCity.Value;
                user.permanentPin = permanentPin.Value;

                var userRoleList = dbcontext.userRoles.Where(i => i.userId == userId).ToList();
                dbcontext.userRoles.RemoveRange(userRoleList);

                foreach (ListItem item in chklistUserRole.Items)
                {
                    if (item.Selected)
                    {
                        var userRole = dbcontext.userRoles;

                        userRole newUserRole = new userRole
                        {
                            userId = userId,
                            roleId = Int32.Parse(item.Value)
                        };
                        dbcontext.userRoles.Add(newUserRole);
                    }

                }

                var userHobbyList = dbcontext.userHobbies.Where(i => i.userId == userId).ToList();
                dbcontext.userHobbies.RemoveRange(userHobbyList);

                string[] hobbyList = hobby.Value.Split(',');
                foreach (string hobby in hobbyList)
                {
                    var userHobby = dbcontext.userHobbies;

                    userHobby newUserHobby = new userHobby
                    {
                        userId = userId,
                        hobby = hobby
                    };
                    dbcontext.userHobbies.Add(newUserHobby);
                }

                dbcontext.SaveChanges();

            }
            BindData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "updateMessage", "alert('Updated successfully')", true);
            Response.Redirect("Details.aspx");
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
            Response.Redirect("Details.aspx");
        }
        protected void CancelBtn(object sender, EventArgs e)
        {
            Response.Redirect("Details.aspx");
        }
    }
}