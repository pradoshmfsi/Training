using BookMyDoctor.Business;
using BookMyDoctor.Utils;
using BookMyDoctor.Utils.Models;
using System;

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