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
    public partial class Report : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }   

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static StandardPostResponseModel GetReportList(string type,DateTime reportMonth)
        {
            var response = Utilities.GetErrorResponse();
            try
            {
                if (Utilities.IsAuthorized())
                {
                    var reportList = BusinessLogic.GetReportList(type, reportMonth);
                    if (reportList != null)
                    {
                        response.IsSuccess = true;
                        response.Data = reportList;
                    }
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