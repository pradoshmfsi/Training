using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ShowDetailsCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int objId = Int32.Parse(Request.Params["compId"]);
            messageControl.ObjId = objId;
            BindData(objId);
        }
        public void BindData(int objId)
        {

            using (var dbcontext = new profileapplicationEntities())
            {
                GridView1.DataSource = dbcontext.companies.Where(i => i.compId == objId).ToList();
                GridView1.DataBind();
            }
        }

    }
}