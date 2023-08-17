using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyDoctor.Utils.Models;
using BookMyDoctor.Utils;
using System.Linq.Expressions;

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
                        Doctors.Add(new DoctorViewModel
                        {
                            UserId = doctor.UserId,
                            DoctorId = doctor.DoctorId,
                            DoctorName = doctor.DoctorName,
                            AppointmentSlotTime = doctor.AppointmentSlotTime,
                            DayStartTime = new DateTime(doctor.DayStartTime.Ticks).ToString("h:mm tt"),
                            DayEndTime = new DateTime(doctor.DayEndTime.Ticks).ToString("h:mm tt")
                        });
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

        public static DoctorTimeModel GetDoctorTime(int doctorId)
        {
            using(var dbcontext = new BookMyDoctorEntities())
            {
                return dbcontext.Doctors.Select(s=> new DoctorTimeModel
                {
                    DoctorId = s.DoctorId,
                    DayStartTime = s.DayStartTime,
                    DayEndTime = s.DayEndTime,
                    AppointmentSlotTime = s.AppointmentSlotTime,
                }).FirstOrDefault(s=>s.DoctorId == doctorId);
            }
        }

        public static List<AppointmentViewModel> GetAppointments(int doctorId,DateTime appointmentDate)
        {
            try
            {
                using (var dbcontext = new BookMyDoctorEntities())
                {
                    List<Appointment> appointments =  dbcontext.Appointments.Where(s => s.DoctorId == doctorId && s.AppointmentDate == appointmentDate).ToList();
                    List<AppointmentViewModel> appointmentsView = new List<AppointmentViewModel>();
                    foreach (var appointment in appointments)
                    {
                        var newAppointment = new AppointmentViewModel {
                            DoctorId = appointment.DoctorId,
                            PatientEmail = appointment.PatientEmail,
                            PatientName = appointment.PatientName,
                            PatientPhone = appointment.PatientPhone,
                            AppointmentDate = appointment.AppointmentDate,
                            AppointmentDateUI = appointment.AppointmentDate.ToShortDateString(),
                            AppointmentId = appointment.AppointmentId,
                            AppointmentStatus = appointment.AppointmentStatus,
                            AppointmentTime = appointment.AppointmentTime,
                            AppointmentTimeUI = new DateTime(appointment.AppointmentTime.Ticks).ToString("hh:mm tt")
                        };
                        appointmentsView.Add(newAppointment);                       
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

        public static StandardPostResponseModel AddAppointment(AppointmentViewModel appointment)
        {
            var response = new StandardPostResponseModel { IsSuccess = true, Data = "Some error occured" };
            try
            {
                using (var dbcontext = new BookMyDoctorEntities())
                {
                    Appointment newAppointment = new Appointment();
                    newAppointment.AppointmentStatus = appointment.AppointmentStatus;
                    newAppointment.AppointmentTime = appointment.AppointmentTime;
                    newAppointment.AppointmentDate = appointment.AppointmentDate;
                    newAppointment.AppointmentId = appointment.AppointmentId;
                    newAppointment.DoctorId = appointment.DoctorId;
                    newAppointment.PatientName = appointment.PatientName;
                    newAppointment.PatientEmail = appointment.PatientEmail;
                    newAppointment.PatientPhone = appointment.PatientPhone;
                    dbcontext.Appointments.Add(newAppointment);
                    dbcontext.SaveChanges();
                    response.IsSuccess = true;
                    response.Data = "Appointment Saved Successfully";
                }
            }
            catch(Exception ex) { Utilities.LogError(ex);}
            return response;
        }
    }
}
