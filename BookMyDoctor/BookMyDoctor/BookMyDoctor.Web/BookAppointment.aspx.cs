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
    public partial class BookAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["doctorId"] == null)
            {
                Response.Redirect("Patient.aspx");
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static StandardPostResponseModel GetAvailableSlots(int doctorId, string appointmentDate)
        {
            var response = Utilities.GetErrorResponse();
            try
            {
                var reportList = BusinessLogic.GetAvailableSlots(doctorId, appointmentDate);
                if (reportList != null)
                {
                    response.IsSuccess = true;
                    response.Data = reportList;
                }
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
            }
            return response;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static StandardPostResponseModel AddAppointment(AppointmentViewModel appointmentObj)
        {
            var response = Utilities.GetErrorResponse();
            try
            {
                var bookedSlots = BusinessLogic.GetBookedSlots(appointmentObj.DoctorId, appointmentObj.AppointmentDate);
                if (bookedSlots.Contains(appointmentObj.AppointmentTime))
                {
                    response.Data = "Slot already booked!";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Data = "ReportDownload.ashx?type=Appointment&appointmentId=" + BusinessLogic.AddAppointment(appointmentObj);
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