using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyDoctor.Utils.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentDateUI { get; set; }
        public System.TimeSpan AppointmentTime { get; set; }
        public string AppointmentTimeUI { get; set; }
        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhone { get; set; }
        public int AppointmentStatus { get; set; }
        public string AppointmentStatusUI { get; set; }
    }
}
