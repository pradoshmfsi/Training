using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyDoctor.Utils.Models
{
    public class ReportDetailedViewModel
    {
        public string Date { get; set; }

        public List<AppointmentViewModel> Appointments { get; set; }
    }
}
