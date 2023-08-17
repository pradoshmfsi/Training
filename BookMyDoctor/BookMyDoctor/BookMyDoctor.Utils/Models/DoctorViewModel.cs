using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyDoctor.Utils.Models
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int UserId { get; set; }
        public int AppointmentSlotTime { get; set; }
        public string DayStartTime { get; set; }
        public string DayEndTime { get; set; }
    }
}
