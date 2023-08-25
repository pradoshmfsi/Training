using BookMyDoctor.Business;
using BookMyDoctor.Utils;
using BookMyDoctor.Utils.Models;
using System;

namespace BookMyDoctor.Web
{
    public partial class Patient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static StandardPostResponseModel GetDoctorsList()
        {
            var response = Utilities.GetErrorResponse() ;
            try
            {

                var doctorList = BusinessLogic.GetDoctorsList();
                if (doctorList != null)
                {
                    response.IsSuccess = true;
                    response.Data = doctorList;
                }
            
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
            }
            return response;
        }        
    }
}