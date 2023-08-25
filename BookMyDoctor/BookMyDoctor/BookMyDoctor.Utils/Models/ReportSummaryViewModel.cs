using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyDoctor.Utils.Models
{
    public class ReportSummaryViewModel
    {
        public DateTime Date { get; set; }

        public string DateUI { get; set; }

        public int TotalAppointments { get; set; }

        public int ClosedAppointments { get; set;}

        public int CancelledAppointments { get; set; }
    }
}
