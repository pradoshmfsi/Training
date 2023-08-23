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
    public partial class Report : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }   
        [System.Web.Services.WebMethod]
        public static StandardPostResponseModel GetReportSummaryList(DateTime reportMonth)
        {
            var response = new StandardPostResponseModel
            {
                IsSuccess = true,
                Data = BusinessLogic.GetReportList("Summary",reportMonth)
            };
            if (response.Data == null)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        [System.Web.Services.WebMethod]
        public static StandardPostResponseModel GetReportDetailedList(DateTime reportMonth)
        {
            var response = new StandardPostResponseModel
            {
                IsSuccess = true,
                Data = BusinessLogic.GetReportList("Detailed", reportMonth)
            };
            if (response.Data == null)
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}