using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"]!=null)
            {
                Label1.Text = "Welcome " + Session["username"].ToString();
                if (!this.IsPostBack)
                {
                    BindGrid();
                }
            }
            else
            {
                Response.Redirect("WebForm4.aspx");
            }
            
        }
        private void BindGrid()
        {
            using (var dbcontext = new profileapplicationEntities())
            {
                GridView1.DataSource = dbcontext.country_table.ToList();
                GridView1.DataBind();
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int countryId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string countryName = (row.FindControl("CountryNameText") as TextBox).Text;

            using (var dbcontext = new profileapplicationEntities())
            {
                var country = dbcontext.country_table.FirstOrDefault(i => i.countryId == countryId);
                country.countryName = countryName;
                dbcontext.SaveChanges();
            }
            GridView1.EditIndex = -1;
            BindGrid();
        }
        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int countryId = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                string url = "States.aspx?countryId="+countryId;
                Response.Redirect(url);
            }
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();

        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int countryId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            using (var dbcontext = new profileapplicationEntities())
            {
                country_table ct = dbcontext.country_table.FirstOrDefault(i => i.countryId == countryId);
                dbcontext.country_table.Remove(ct);
                dbcontext.SaveChanges();
            }
            BindGrid();

        }
        protected void AddCountryOnClick(object sender, EventArgs e)
        {
            using (var dbcontext = new profileapplicationEntities())
            {
                var country = dbcontext.country_table;
          
                country_table ct = new country_table { countryName = CountryName.Text };
                dbcontext.country_table.Add(ct);
                
                dbcontext.SaveChanges();
                Console.WriteLine(ct.countryId);
                BindGrid();
            }
        }
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    //Value of Textbox1 and TectBox2 is assigin on the ViewState
        //    //TextBox1.EnableViewState = false;
        //    ViewState["name"] = TextBox1.Text;
        //    ViewState["password"] = TextBox2.Text;
        //    //after clicking on Button TextBox value Will be Cleared  
        //    TextBox1.Text = TextBox2.Text = string.Empty;
        //}
        //protected void Button3_Click(object sender, EventArgs e)
        //{
        //    //If ViewState Value is not Null then Value of View State is Assign to TextBox  
        //    if (ViewState["name"] != null)
        //    {
        //        TextBox1.Text = ViewState["name"].ToString();
        //    }
        //    if (ViewState["password"] != null)
        //    {
        //        TextBox2.Text = ViewState["password"].ToString();
        //    }
        //}

    }
}