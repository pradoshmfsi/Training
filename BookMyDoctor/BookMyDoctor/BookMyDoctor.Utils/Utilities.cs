using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Web;
using BookMyDoctor.Utils.Models;

namespace BookMyDoctor.Utils
{
    public class Utilities
    {
        /// <summary>
        /// True when the session is not null, i.e, an user is logged in!
        /// False when the session is null, i.e, no user is logged in!
        /// </summary>
        /// <returns></returns>
        public static bool IsAuthorized()
        {
            if (HttpContext.Current.Session["userId"] == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the id that is stored in the session
        /// </summary>
        /// <returns></returns>
        public static int GetSessionId()
        {
            if (IsAuthorized())
            {
                return Int32.Parse(HttpContext.Current.Session["userId"].ToString());
            }
            return 0;
            
        }

        /// <summary>
        /// Returns a Time String with 12hr format
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeString(TimeSpan time)
        {
            return new DateTime(time.Ticks).ToString("hh:mm tt");
        }

        /// <summary>
        /// Logs the error for any exception
        /// </summary>
        /// <param name="ex"></param>
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

        /// <summary>
        /// Changes the file name for versioning, helps to update the client in case of any changes to the static files
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFilePathForHandler(string path)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath(path);
                FileInfo file = new FileInfo(filePath);
                string fileName = path.Substring(0, path.LastIndexOf('.'));
                path = fileName + "-" + file.LastAccessTime.ToString("ddMMyyyyHHmmss") + "-versionTrue" + file.Extension;
                return path;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return "";
            }
        }

        /// <summary>
        /// Returns a standard response error object to initialize an response.
        /// </summary>
        /// <returns></returns>
        public static StandardPostResponseModel GetErrorResponse()
        {
            return new StandardPostResponseModel
            {
                IsSuccess = false,
                Data = "Some error occured!"
            };
        }

    }
}
