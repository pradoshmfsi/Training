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
        [System.Web.Services.WebMethod]
        public static StandardPostResponseModel GetAppointmentsList(DateTime appointmentDate)
        {
            var response = new StandardPostResponseModel
            {
                IsSuccess = true,
                Data = BusinessLogic.GetAppointmentsList(appointmentDate)
            };
            if(response.Data == null)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        [System.Web.Services.WebMethod]
        public static StandardPostResponseModel CloseOrCancelAppointment(int appointmentStatus, int appointmentId)
        {
            var response = new StandardPostResponseModel
            {
                IsSuccess = BusinessLogic.CloseOrCancelAppointment(appointmentStatus,appointmentId),
                Data = ""
            };
            if (!response.IsSuccess)
            {
                response.Data = "Some error occured!";
            }
            return response;
        }
    }
}