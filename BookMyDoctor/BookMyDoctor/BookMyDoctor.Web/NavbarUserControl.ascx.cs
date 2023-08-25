using BookMyDoctor.Business;
using BookMyDoctor.Utils;
using System;

namespace BookMyDoctor.Web
{
    public partial class NavbarUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utilities.IsAuthorized())
            {
                patientNav.Visible= false;
                lblDoctorName.Text =BusinessLogic.GetDoctor(Utilities.GetSessionId()).DoctorName;
            }
            else
            {
                doctorNav.Visible= false;
            }
        }
        protected void LogoutUser(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}