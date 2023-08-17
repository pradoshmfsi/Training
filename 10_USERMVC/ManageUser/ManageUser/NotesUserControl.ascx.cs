using ManageUser.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageUser.Utils.UserDetailModels;
namespace ManageUser
{
    public partial class NotesUserControl : System.Web.UI.UserControl
    {
        public int UserId { get; set; }

        public bool IsAdmin { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack && UserId != 0)
            {
                BindData();
                if (!IsAdmin)
                {
                    ifPrivateCheck.Visible = false;
                    notePrivateHeader.Visible = false;
                    emptyNotePrivateLabel.Visible = false;
                }
            }
            
        }

        public void BindNoteDataList(DataList NoteDataList, int ifPrivate, Label emptyDataLabel)
        {

            NoteDataList.DataSource = UserDetailBusiness.GetUserNotes(UserId, ifPrivate);
            NoteDataList.DataBind();

            if (NoteDataList.Items.Count == 0)
            {
                emptyDataLabel.Visible = true;
            }
            else
            {
                emptyDataLabel.Visible = false;
            }
        }

        public void BindData()
        {

            //NoteDataList.DataSource = dbcontext.userNotes.Where(i => i.userId == userId && ((isAdmin)||i.ifPrivate == 0)).ToList();
            BindNoteDataList(NoteDataList, 0, emptyNoteLabel);
            if (IsAdmin)
            {
                BindNoteDataList(NotePrivateDataList, 1, emptyNotePrivateLabel);
            }

        }

        protected void AddNoteBtn(object sender, EventArgs e)
        {
            if (NoteMessage.Value.Trim() != "")
            {

                UserNote newNote = new UserNote { userId = UserId, noteMessage = NoteMessage.Value, ifPrivate = ifPrivateCheck.Checked ? 1 : 0 };
                if (UserDetailBusiness.AddNotesToDB(newNote))
                {
                    BindData();
                }
               
            }
            NoteMessage.Value = "";
        }
    }
}