using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class States : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int countryId = Int32.Parse(Request.Params["countryId"]);
            using (var dbcontext = new profileapplicationEntities())
            {
                var state = dbcontext.getStatesFromCountryId(countryId);
                GridViewStates.DataSource = state.ToList();
                GridViewStates.DataBind();
            }

        }
    }
}