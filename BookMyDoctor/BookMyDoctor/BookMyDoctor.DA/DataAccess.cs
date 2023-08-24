using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyDoctor.Utils.Models;
using BookMyDoctor.Utils;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Xml.Linq;
using System.Data.Entity;

namespace BookMyDoctor.DA
{
    public class DataAccess
    {
        public static UserViewModel GetUserByEmail(string email)
        {

            using (var dbcontext = new BookMyDoctorEntities())
            {
                return dbcontext.Users.Select(s => new UserViewModel
                {
                    UserId = s.UserId,
                    Email = s.Email,
                    Name = s.Name,
                    Password = s.Password
                }).FirstOrDefault(s => s.Email == email);
            }
        }

        /// <summary>
        /// Maps the entity doctor to an object of DoctorViewModel
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        public static DoctorViewModel MapDoctorFromEntity(Doctor doctor)
        {
            return new DoctorViewModel
            {
                UserId = doctor.UserId,
                DoctorId = doctor.DoctorId,
                DoctorName = doctor.DoctorName,
                AppointmentSlotTime = doctor.AppointmentSlotTime,
                DayStartTime = doctor.DayStartTime,
                DayStartTimeUI = new DateTime(doctor.DayStartTime.Ticks).ToString("h:mm tt"),
                DayEndTime = doctor.DayEndTime,
                DayEndTimeUI = new DateTime(doctor.DayEndTime.Ticks).ToString("h:mm tt")
            };
        }

        /// <summary>
        /// Maps the entity appointment to an object of AppointmentViewModel
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public static AppointmentViewModel MapAppointmentFromEntity(Appointment appointment)
        {
            using (var dbcontext = new BookMyDoctorEntities())
            {
                return new AppointmentViewModel
                {
                    DoctorId = appointment.DoctorId,
                    PatientEmail = appointment.PatientEmail,
                    PatientName = appointment.PatientName,
                    PatientPhone = appointment.PatientPhone,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentDateUI = appointment.AppointmentDate.ToShortDateString(),
                    AppointmentId = appointment.AppointmentId,
                    AppointmentTime = appointment.AppointmentTime,
                    AppointmentStatus = appointment.AppointmentStatus,
                    AppointmentStatusUI = appointment.Status.StatusName,
                    AppointmentTimeUI = new DateTime(appointment.AppointmentTime.Ticks).ToString("hh:mm tt")
                };
            }

        }

        /// <summary>
        /// Returns the particular doctor by the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DoctorViewModel GetDoctor(int userId)
        {
            using (var dbcontext = new BookMyDoctorEntities())
            {
                return MapDoctorFromEntity(dbcontext.Doctors.FirstOrDefault(s => s.UserId == userId));
            }
        }

        /// <summary>
        /// Gets all the doctors present
        /// </summary>
        /// <returns></returns>
        public static List<DoctorViewModel> GetDoctorList()
        {

            using (var dbcontext = new BookMyDoctorEntities())
            {
                List<Doctor> doctors = dbcontext.Doctors.ToList();
                List<DoctorViewModel> Doctors = new List<DoctorViewModel>();
                foreach (var doctor in doctors)
                {
                    Doctors.Add(MapDoctorFromEntity(doctor));
                }
                return Doctors;
            }

        }

        /// <summary>
        /// Returns the particular doctor by the doctorId
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public static DoctorViewModel GetDoctorFromID(int doctorId)
        {
            using (var dbcontext = new BookMyDoctorEntities())
            {
                var doctor = dbcontext.Doctors.FirstOrDefault(s => s.DoctorId == doctorId);
                return MapDoctorFromEntity(doctor);
            }
        }

        /// <summary>
        /// Get a particular appointment from AppointmentId
        /// </summary>
        /// <param name="AppointmentId"></param>
        /// <returns></returns>
        public static AppointmentViewModel GetAppointment(int AppointmentId)
        {
            using (var dbcontext = new BookMyDoctorEntities())
            {
                return MapAppointmentFromEntity(dbcontext.Appointments.FirstOrDefault(s => s.AppointmentId == AppointmentId));
            }
        }

        /// <summary>
        /// Get all appointments of a particular doctor(doctorId) of a certain date(appointmentDate)
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        public static List<AppointmentViewModel> GetAppointments(int doctorId, DateTime appointmentDate)
        {

            using (var dbcontext = new BookMyDoctorEntities())
            {
                List<Appointment> appointments = dbcontext.Appointments.Where(s => s.DoctorId == doctorId && s.AppointmentDate == appointmentDate).OrderBy(s => s.AppointmentTime).ToList();
                List<AppointmentViewModel> appointmentsView = new List<AppointmentViewModel>();
                foreach (var appointment in appointments)
                {
                    appointmentsView.Add(MapAppointmentFromEntity(appointment));
                }
                return appointmentsView;

            }

        }

        /// <summary>
        /// Adds the appointment to DB
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public static int AddAppointment(AppointmentViewModel appointment)
        {

            using (var dbcontext = new BookMyDoctorEntities())
            {
                Appointment newAppointment = new Appointment();
                newAppointment.AppointmentTime = appointment.AppointmentTime;
                newAppointment.AppointmentDate = appointment.AppointmentDate;
                newAppointment.AppointmentId = appointment.AppointmentId;
                newAppointment.DoctorId = appointment.DoctorId;
                newAppointment.PatientName = appointment.PatientName;
                newAppointment.PatientEmail = appointment.PatientEmail;
                newAppointment.PatientPhone = appointment.PatientPhone;
                newAppointment.AppointmentStatus = 1;
                dbcontext.Appointments.Add(newAppointment);
                dbcontext.SaveChanges();
                return newAppointment.AppointmentId;
            }

        }

        /// <summary>
        /// Updates the appointment having appointmentId with the given status
        /// </summary>
        /// <param name="appointmentStatus"></param>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        public static void CloseOrCancelAppointment(int appointmentStatus, int appointmentId)
        {
            using (var dbcontext = new BookMyDoctorEntities())
            {
                var newAppointment = dbcontext.Appointments.FirstOrDefault(s => s.AppointmentId == appointmentId);
                newAppointment.AppointmentStatus = appointmentStatus;
                dbcontext.SaveChanges();
            }
        }

        /// <summary>
        /// Get the report of the given type i.e either summary or detailed, by doctorId and of a particular month
        /// </summary>
        /// <param name="type"></param>
        /// <param name="doctorId"></param>
        /// <param name="reportMonth"></param>
        /// <returns></returns>
        public static dynamic GetReportList(string type, int doctorId, DateTime reportMonth)
        {
            var firstDayOfMonth = new DateTime(reportMonth.Year, reportMonth.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            using (var dbcontext = new BookMyDoctorEntities())
            {
                var groupedAppointments = dbcontext.Appointments.Where(s => s.DoctorId == doctorId && s.AppointmentDate >= firstDayOfMonth && s.AppointmentDate <= lastDayOfMonth)
                    .GroupBy(s => s.AppointmentDate).ToList();
                if (type == "Summary")
                {
                    return GetSummaryReportList(groupedAppointments);
                }
                else
                {
                    return GetDetailedReportList(groupedAppointments);
                }

            }
        }

        /// <summary>
        /// Get the list of all the reports of type "Summary"
        /// </summary>
        /// <param name="groupedAppointments"></param>
        /// <returns></returns>
        public static List<ReportSummaryViewModel> GetSummaryReportList(List<IGrouping<DateTime, Appointment>> groupedAppointments)
        {
            var reports = new List<ReportSummaryViewModel>();
            foreach (var appointment in groupedAppointments)
            {
                reports.Add(new ReportSummaryViewModel
                {
                    Date = appointment.Key.ToShortDateString(),
                    TotalAppointments = appointment.Count(),
                    ClosedAppointments = appointment.Count(s => s.AppointmentStatus == 2),
                    CancelledAppointments = appointment.Count(s => s.AppointmentStatus == 3)
                });
            }
            return reports;
        }

        /// <summary>
        /// Get the list of all the reports of type "Detailed"
        /// </summary>
        /// <param name="groupedAppointments"></param>
        /// <returns></returns>
        public static List<ReportDetailedViewModel> GetDetailedReportList(List<IGrouping<DateTime, Appointment>> groupedAppointments)
        {
            var reports = new List<ReportDetailedViewModel>();
            foreach (var appointment in groupedAppointments)
            {
                var report = new ReportDetailedViewModel();
                report.Date = appointment.Key.ToShortDateString();
                var Appointments = new List<AppointmentViewModel>();
                foreach (var item in appointment)
                {
                    Appointments.Add(new AppointmentViewModel
                    {
                        PatientName = item.PatientName,
                        AppointmentStatusUI = item.Status.StatusName,
                    });

                }
                report.Appointments = Appointments;
                reports.Add(report);
            }
            return reports;
        }

        /// <summary>
        /// Initializes Data for the web.
        /// Removes transactional data like appointments.
        /// Reinitializes non-transactional data like doctors,users etc.
        /// </summary>
        /// <returns></returns>
        public static void InitializeData()
        {

            using (var dbcontext = new BookMyDoctorEntities())
            {

                dbcontext.Database.ExecuteSqlCommand("ALTER TABLE Appointments DROP CONSTRAINT FK_APPOINT_STATUS;" +
                    "ALTER TABLE Appointments DROP CONSTRAINT FK_APPOINT_DOCTOR;" +
                    "ALTER TABLE Doctors DROP CONSTRAINT FK_USER_DOCTOR;" +
                    "TRUNCATE TABLE Appointments;" +
                    "TRUNCATE TABLE Doctors;" +
                    "TRUNCATE TABLE Users;" +
                    "TRUNCATE TABLE Status;" +
                    "INSERT INTO Status(StatusName) VALUES('Open');" +
                    "INSERT INTO Status(StatusName) VALUES('Closed');" +
                    "INSERT INTO Status(StatusName) VALUES('Cancelled');" +
                    "INSERT INTO Users(Name,Email,Password) VALUES('Pramod Kumar','pramod@gmail.com','Pramod@123');" +
                    "INSERT INTO Users(Name,Email,Password) VALUES('Pratyush Sahoo','pratyush@gmail.com','Pratyush@123');" +
                    "INSERT INTO Users(Name,Email,Password) VALUES('BK Jena','bk@gmail.com','bkjena@1233');" +
                    "INSERT INTO Doctors(DoctorName,UserId,AppointmentSlotTime,DayStartTime,DayEndTime) VALUES('Pramod Kumar',1,30,'09:00:00','18:00:00');" +
                    "INSERT INTO Doctors(DoctorName,UserId,AppointmentSlotTime,DayStartTime,DayEndTime) VALUES('Pratyush Sahoo',2,15,'10:00:00','19:00:00');" +
                    "INSERT INTO Doctors(DoctorName,UserId,AppointmentSlotTime,DayStartTime,DayEndTime) VALUES('BK Jena',3,30,'10:00:00','18:00:00');" +
                    "ALTER TABLE Doctors ADD CONSTRAINT FK_USER_DOCTOR FOREIGN KEY (UserId) REFERENCES Users(UserId);" +
                    "ALTER TABLE Appointments ADD CONSTRAINT FK_APPOINT_STATUS FOREIGN KEY (AppointmentStatus) REFERENCES Status(StatusId);" +
                    "ALTER TABLE Appointments ADD CONSTRAINT FK_APPOINT_DOCTOR FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId);");
            }

        }

    }
}
