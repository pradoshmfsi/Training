using BookMyDoctor.Business;
using BookMyDoctor.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookMyDoctor.Utils;
namespace BookMyDoctor.Web
{
    public partial class DoctorAppointments : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static StandardPostResponseModel GetAppointmentsList(DateTime appointmentDate)
        {
            var response = Utilities.GetErrorResponse();
            try
            {
                if (Utilities.IsAuthorized())
                {
                    var appointmentsList = BusinessLogic.GetAppointmentsList(appointmentDate);
                    if (appointmentsList != null)
                    {
                        response.IsSuccess = true;
                        response.Data = appointmentsList;
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
            }
            return response;
        
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static StandardPostResponseModel CloseOrCancelAppointment(int appointmentStatus, int appointmentId)
        {
            var response = Utilities.GetErrorResponse();
            try
            {
                if (Utilities.IsAuthorized())
                {
                    BusinessLogic.CloseOrCancelAppointment(appointmentStatus, appointmentId);
                    response.IsSuccess = true;
                    response.Data = "Success";
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