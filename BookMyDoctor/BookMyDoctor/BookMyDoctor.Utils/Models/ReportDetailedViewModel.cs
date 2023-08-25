using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyDoctor.Utils.Models
{
    public class ReportDetailedViewModel
    {
        public DateTime Date { get; set; }

        public string DateUI { get; set; }

        public List<AppointmentViewModel> Appointments { get; set; }
    }
}
