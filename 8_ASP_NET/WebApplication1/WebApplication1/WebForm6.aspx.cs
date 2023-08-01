using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void AddCompanyOnClick(object sender, EventArgs e)
        {
            using (var dbcontext = new profileapplicationEntities())
            {
                var company = dbcontext.companies;

                company c = new company { compName = CompanyName.Text, compRevenue = CompanyRevenue.Text };
                dbcontext.companies.Add(c);
                dbcontext.SaveChanges();
            }
        }
    }
}