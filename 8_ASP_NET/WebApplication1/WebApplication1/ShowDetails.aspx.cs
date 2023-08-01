using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ShowDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int objId = Int32.Parse(Request.Params["userId"]);
            messageControl.ObjId = objId;
            if (!this.IsPostBack)
            {
                BindData(objId);
            }
            
        }
        public void BindData(int objId)
        {
            using (var dbcontext = new profileapplicationEntities())
            {

                GridView1.DataSource = dbcontext.users.Where(i => i.userId == objId).ToList();
                
                GridView1.DataBind();
            }
        }



    }
}