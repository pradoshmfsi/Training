using BookMyDoctor.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyDoctor.DA;
using BookMyDoctor.Utils;

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
            DoctorTimeModel doctorTimeModel = DataAccess.GetDoctorTime(doctorId);

            var startTime = doctorTimeModel.DayStartTime;
            var endTime = doctorTimeModel.DayEndTime;
            List<SlotViewModel> availableSlots = new List<SlotViewModel>();

            while (startTime < endTime)
            {
                var tempTime = startTime.Add(new TimeSpan(0, doctorTimeModel.AppointmentSlotTime, 0));
                var slot = new SlotViewModel
                {
                    SlotStatus = "booked",
                    SlotTime = startTime.ToString(),
                    SlotStartTime = Utilities.GetTimeString(startTime),
                    SlotEndTime = Utilities.GetTimeString(tempTime)
                };        
                
                if (!bookedSlots.Contains(startTime))
                    slot.SlotStatus = "open";

                availableSlots.Add(slot);
                startTime = tempTime;

            }
            return availableSlots;
        }

        public static StandardPostResponseModel AddAppointment(AppointmentViewModel appointmentObj)
        {
            var response = new StandardPostResponseModel { IsSuccess = false,Data="Slot already booked" };
            var bookedSlots = GetBookedSlots(appointmentObj.DoctorId, appointmentObj.AppointmentDate);
            if (bookedSlots.Contains(appointmentObj.AppointmentTime))
            {
                return response;
            }
            return DataAccess.AddAppointment(appointmentObj);
        }
    }
}
