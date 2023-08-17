using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyDoctor.Utils.Models
{
    public class DoctorTimeModel
    {
        public int DoctorId { get; set; }

        public int AppointmentSlotTime { get; set; }

        public System.TimeSpan DayStartTime { get; set; }

        public System.TimeSpan DayEndTime { get; set; }
    }
}
