using BookMyDoctor.Business;
using BookMyDoctor.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookMyDoctor.Web
{
    public partial class Initialize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void InitData(object sender, EventArgs e)
        {
            if (BusinessLogic.InitializeData())
            {
                Response.Redirect("Patient.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage", "alert('Some error occured')", true);
            }
        }
    }
}