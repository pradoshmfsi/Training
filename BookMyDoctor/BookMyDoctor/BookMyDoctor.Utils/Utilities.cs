using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Web;
namespace BookMyDoctor.Utils
{
    public class Utilities
    {
        public static int GetSessionId()
        {
            return Int32.Parse(HttpContext.Current.Session["userId"].ToString());
        }

        public static string GetTimeString(TimeSpan time)
        {
            return new DateTime(time.Ticks).ToString("hh:mm tt");
        }
        public static TimeSpan GetTime(string interval)
        {
            string format = interval.Substring(interval.Length - 2);
            string time = interval.Substring(0, interval.Length - 3);
            var timeArr = time.Split(':');
            var hours = Int32.Parse(timeArr[0]);
            var minutes = Int32.Parse(timeArr[1]);
            if (format == "PM" && hours < 12)
            {
                return new TimeSpan(hours + 12, minutes, 0);
            }
            else
            {
                return new TimeSpan(hours, minutes, 0);
            }
        }
        public static void LogError(Exception ex)
        {

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            string logFile = ConfigurationManager.AppSettings.Get("Output_path") + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
            string message = "------------------------------------------------------------------------------------\n";
            message += "Time - " + DateTime.Now.ToString("hh:mm:ss tt") + "\n";
            message += "Error - \n";
            message += ex.ToString();
            File.AppendAllText(logFile, message + "\n\n");
        }
    }
}
