using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication1
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        public int ObjId { get; set; }
        public string ObjType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindData(ObjId);
            }
        }
        public void BindData(int objId)
        {
            using (var dbcontext = new profileapplicationEntities())
            {
                DataList2.DataSource = dbcontext.notes.Where(i => i.objId == objId).Where(i => i.objMode == ObjType).ToList();
                DataList2.DataBind();
            }
        }
        protected void AddMessage(object sender, EventArgs e)
        {
            using (var dbcontext = new profileapplicationEntities())
            {
                var note = dbcontext.notes;

                note newNote = new note { objId = ObjId, objMessage = MessageName.Text, objMode = ObjType };
                dbcontext.notes.Add(newNote);
                dbcontext.SaveChanges();
                BindData(ObjId);
            }

        }


    }
}