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
        /// <summary>
        /// Gets the particular row having the given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static UserViewModel GetUserByEmail(string email)
        {
            return DataAccess.GetUserByEmail(email);
        }

        /// <summary>
        /// Gets all the doctos present
        /// </summary>
        /// <returns></returns>
        public static List<DoctorViewModel> GetDoctorsList()
        {
            return DataAccess.GetDoctorList();
        }

        /// <summary>
        /// Gets the list having the starting time of all booked slots of a particular doctor on a particular date
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        public static List<TimeSpan> GetBookedSlots(int doctorId, DateTime appointmentDate) {
            var appointments = DataAccess.GetAppointments(doctorId, appointmentDate);
            if (appointments != null)
            {
                return appointments.Select(s => s.AppointmentTime).ToList();
            }
            return new List<TimeSpan>();
        }

        /// <summary>
        /// Gets all the available slots of a particular doctor on a particular date
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds the appointment to DB and return the Id of the added appointment.
        /// </summary>
        /// <param name="appointmentObj"></param>
        /// <returns></returns>
        public static int AddAppointment(AppointmentViewModel appointmentObj)
        {
            return DataAccess.AddAppointment(appointmentObj);
        }

        /// <summary>
        /// Gets the ID particular doctor having the given userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetDoctorId(int userId)
        {
            return DataAccess.GetDoctor(userId).DoctorId;
        }

        /// <summary>
        /// Gets the particular doctor having a particular userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DoctorViewModel GetDoctor(int userId)
        {
            return DataAccess.GetDoctor(userId);
        }

        /// <summary>
        /// Gets the particuar appointment having the given appointmentId
        /// </summary>
        /// <param name="AppointmentId"></param>
        /// <returns></returns>
        public static AppointmentViewModel GetAppointment(int AppointmentId)
        {
            return DataAccess.GetAppointment(AppointmentId);
        }

        /// <summary>
        /// Gets all the appointments of a particular month of the logged doctor.
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        public static List<AppointmentViewModel> GetAppointmentsList(DateTime appointmentDate)
        {
            return DataAccess.GetAppointments(GetDoctorId(Utilities.GetSessionId()), appointmentDate);
        }

        /// <summary>
        /// Updates the status of a particuar appointment having the given appointmentId with the given status.
        /// </summary>
        /// <param name="appointmentStatus"></param>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        public static void CloseOrCancelAppointment(int appointmentStatus,int appointmentId)
        {
            DataAccess.CloseOrCancelAppointment(appointmentStatus,appointmentId);
        }

        /// <summary>
        /// Get the report of the given type of a particular month of the logged doctor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reportMonth"></param>
        /// <returns></returns>
        public static dynamic GetReportList(string type,DateTime reportMonth)
        {
            return DataAccess.GetReportList(type,GetDoctorId(Utilities.GetSessionId()),reportMonth);
        }

        /// <summary>
        /// Initializes data, removes transactional data, and reinitializes non-transactional data
        /// </summary>
        /// <returns></returns>
        public static void InitializeData()
        {
            DataAccess.InitializeData();
        }

    }
}
