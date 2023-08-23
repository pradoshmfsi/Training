using BookMyDoctor.Business;
using BookMyDoctor.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookMyDoctor.Web
{
    public partial class Patient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static StandardPostResponseModel GetDoctorsList()
        {
            var response = new StandardPostResponseModel
            {
                IsSuccess = true,
                Data = BusinessLogic.GetDoctorsList()
            };
            if (response.Data == null)
            {
                response.IsSuccess = false;
                response.Data = "Some error occured";
            }
            return response;
        }

        
    }
}