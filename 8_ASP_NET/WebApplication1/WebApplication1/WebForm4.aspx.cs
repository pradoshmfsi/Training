using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            if (TextBox2.Text == "123")
            {
                Session["username"] = username;
                Response.Redirect("WebForm3.aspx");
            }
        }
    }
}