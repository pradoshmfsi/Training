using BookMyDoctor.Business;
using BookMyDoctor.Utils;
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

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static StandardPostResponseModel InitializeData()
        {
            var response = Utilities.GetErrorResponse();
            try
            {
                BusinessLogic.InitializeData();
                response.IsSuccess = true;
                response.Data = "Success";
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
            }
            return response;

        }
    }
}