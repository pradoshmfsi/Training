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
    public partial class Appointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["doctorId"]==null)
            {
                Response.Redirect("Patient.aspx");
            }
        }
        [System.Web.Services.WebMethod]
        public static StandardPostResponseModel GetAvailableSlots(int doctorId,string appointmentDate)
        {
            var response = new StandardPostResponseModel { IsSuccess=false,Data=new List<string>()};
            var availableSlots = BusinessLogic.GetAvailableSlots(doctorId, appointmentDate);
            if(availableSlots != null)
            {
                response.IsSuccess = true;
                response.Data = availableSlots;
            }
            return response;
        }

        [System.Web.Services.WebMethod]
        public static StandardPostResponseModel AddAppointment(AppointmentViewModel appointmentObj)
        {
            return BusinessLogic.AddAppointment(appointmentObj);
        }
    }
}