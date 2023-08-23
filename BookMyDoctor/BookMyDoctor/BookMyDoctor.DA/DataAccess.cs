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
            try
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
            catch (Exception ex)
            {
                Utilities.LogError(ex);
                return null;
            }
        }

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

        public static AppointmentViewModel MapAppointmentFromEntity(Appointment appointment)
        {
            using(var dbcontext = new BookMyDoctorEntities())
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
        
        public static DoctorViewModel GetDoctor(int userId)
        {
            try
            {
                using (var dbcontext = new BookMyDoctorEntities())
                {
                    return MapDoctorFromEntity(dbcontext.Doctors.FirstOrDefault(s => s.UserId == userId));
                }
            }
            catch (Exception ex) { Utilities.LogError(ex); return null; }

        }

        public static List<DoctorViewModel> GetDoctorList()
        {
            try
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
            catch (Exception ex)
            {
                Utilities.LogError(ex);
                return null;
            }
        }

        public static DoctorViewModel GetDoctorFromID(int doctorId)
        {
            try
            {
                using (var dbcontext = new BookMyDoctorEntities())
                {
                    var doctor = dbcontext.Doctors.FirstOrDefault(s => s.DoctorId == doctorId);
                    return MapDoctorFromEntity(doctor);
                }
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
                return null;
            }

        }

        public static AppointmentViewModel GetAppointment(int AppointmentId)
        {
            using(var dbcontext = new BookMyDoctorEntities())
            {
                return MapAppointmentFromEntity(dbcontext.Appointments.FirstOrDefault(s => s.AppointmentId == AppointmentId));
            }
        }

        public static List<AppointmentViewModel> GetAppointments(int doctorId, DateTime appointmentDate)
        {
            try
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
            catch (Exception ex)
            {
                Utilities.LogError(ex);
                return null;
            }
        }

        public static int AddAppointment(AppointmentViewModel appointment)
        {
            try
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
            catch (Exception ex) { Utilities.LogError(ex); }
            return 0;
        }

        public static bool CloseOrCancelAppointment(int appointmentStatus, int appointmentId)
        {
            try
            {
                using (var dbcontext = new BookMyDoctorEntities())
                {
                    var newAppointment = dbcontext.Appointments.FirstOrDefault(s => s.AppointmentId == appointmentId);
                    newAppointment.AppointmentStatus = appointmentStatus;
                    dbcontext.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Utilities.LogError(e);
                return false;
            }
        }

        public static dynamic GetReportList(string type, int doctorId, DateTime reportMonth)
        {
            try
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
            catch (Exception ex)
            {
                Utilities.LogError(ex);
                return null;
            }
        }

        public static List<ReportSummaryViewModel> GetSummaryReportList(List<IGrouping<DateTime, Appointment>> groupedAppointments)
        {
            var reports = new List<ReportSummaryViewModel>();
            foreach (var appointment in groupedAppointments)
            {
                reports.Add(new ReportSummaryViewModel
                {
                    Date = appointment.Key.ToShortDateString(),
                    TotalAppointments = appointment.Count(),
                    ClosedAppointments = appointment.Where(s => s.AppointmentStatus == 2).Count(),
                    CancelledAppointments = appointment.Where(s => s.AppointmentStatus == 3).Count()
                });
            }
            return reports;
        }

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

        public static bool InitializeData()
        {
            try
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
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
            }
            return false;
        }

    }
}
