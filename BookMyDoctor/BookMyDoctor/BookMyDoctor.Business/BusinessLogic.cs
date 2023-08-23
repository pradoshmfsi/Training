using BookMyDoctor.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyDoctor.DA;
using BookMyDoctor.Utils;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Xml.Linq;
namespace BookMyDoctor.Business
{
    public class BusinessLogic
    {
        public static UserViewModel GetUserByEmail(string email)
        {
            return DataAccess.GetUserByEmail(email);
        }

        public static List<DoctorViewModel> GetDoctorsList()
        {
            return DataAccess.GetDoctorList();
        }

        public static List<TimeSpan> GetBookedSlots(int doctorId, DateTime appointmentDate) {
            var appointments = DataAccess.GetAppointments(doctorId, appointmentDate);
            if (appointments != null)
            {
                return appointments.Select(s => s.AppointmentTime).ToList();
            }
            return new List<TimeSpan>();
        }

        public static List<SlotViewModel> GetAvailableSlots(int doctorId,string appointmentDate)
        {

            var bookedSlots = GetBookedSlots(doctorId, DateTime.Parse(appointmentDate));
            var doctor = DataAccess.GetDoctorFromID(doctorId);

            var startTime = doctor.DayStartTime;
            var endTime = doctor.DayEndTime;
            List<SlotViewModel> slots = new List<SlotViewModel>();

            while (startTime < endTime)
            {
                var tempTime = startTime.Add(new TimeSpan(0, doctor.AppointmentSlotTime, 0));
                var slot = new SlotViewModel
                {
                    SlotStatus = "booked",
                    SlotStartTime = startTime.ToString(),
                    SlotStartTimeUI = Utilities.GetTimeString(startTime),
                    SlotEndTimeUI = Utilities.GetTimeString(tempTime)
                };        
                
                if (!bookedSlots.Contains(startTime))
                    slot.SlotStatus = "open";

                slots.Add(slot);
                startTime = tempTime;

            }
            return slots;
        }

        public static StandardPostResponseModel AddAppointment(AppointmentViewModel appointmentObj)
        {
            var response = new StandardPostResponseModel { IsSuccess = false,Data="Slot already booked" };
            var bookedSlots = GetBookedSlots(appointmentObj.DoctorId, appointmentObj.AppointmentDate);
            if (bookedSlots.Contains(appointmentObj.AppointmentTime))
            {
                return response;
            }

            response.Data = DataAccess.AddAppointment(appointmentObj);

            if (response.Data == 0)
            {                
                response.Data = "Some error occured!";   
            }
            else
            {
                response.IsSuccess = true;
                response.Data = "ReportDownload.ashx?type=Appointment&appointmentId=" + response.Data;
            }
            return response;
        }

        public static int GetDoctorId(int userId)
        {
            return DataAccess.GetDoctor(userId).DoctorId;
        }

        public static DoctorViewModel GetDoctor(int userId)
        {
            return DataAccess.GetDoctor(userId);
        }

        public static AppointmentViewModel GetAppointment(int AppointmentId)
        {
            return DataAccess.GetAppointment(AppointmentId);
        }


        public static List<AppointmentViewModel> GetAppointmentsList(DateTime appointmentDate)
        {
            return DataAccess.GetAppointments(GetDoctorId(Utilities.GetSessionId()), appointmentDate);
        }

        public static bool CloseOrCancelAppointment(int appointmentStatus,int appointmentId)
        {
            return DataAccess.CloseOrCancelAppointment(appointmentStatus,appointmentId);
        }

        public static dynamic GetReportList(string type,DateTime reportMonth)
        {
            return DataAccess.GetReportList(type,GetDoctorId(Utilities.GetSessionId()),reportMonth);
        }

        public static bool InitializeData()
        {
            return DataAccess.InitializeData();
        }


    }
}
