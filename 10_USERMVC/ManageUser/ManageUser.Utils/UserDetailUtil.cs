using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace ManageUser.Utils
{
    public class UserDetailUtil
    {
        public static void LogError(Exception ex)
        {

            while (ex.InnerException != null)
            {
                ex= ex.InnerException;
            }

            string logFile = ConfigurationManager.AppSettings.Get("Output_path") + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
            string message = "------------------------------------------------------------------------------------\n";
            message += "User - " + GetSession() +"\n";
            message += "Time - " + DateTime.Now.ToString("hh:mm:ss tt") + "\n";
            message += "Error - \n"; 
            message += ex.ToString();
            File.AppendAllText(logFile, message + "\n\n");
        }
        public static int GetSession()
        {
            return Int32.Parse(HttpContext.Current.Session["user"].ToString());
        }
    }
}
