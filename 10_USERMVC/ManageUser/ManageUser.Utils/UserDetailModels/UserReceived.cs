using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageUser.Utils.UserDetailModels
{
    public class UserReceived
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string profilePic { get; set; }
        public string profilePicActual { get; set; }
        public string presentAddressLine1 { get; set; }
        public string presentAddressLine2 { get; set; }
        public string presentCountry { get; set; }
        public string presentState { get; set; }
        public string presentCity { get; set; }
        public string presentPin { get; set; }
        public string permanentAddressLine1 { get; set; }
        public string permanentAddressLine2 { get; set; }
        public string permanentCountry { get; set; }
        public string permanentState { get; set; }
        public string permanentCity { get; set; }
        public string permanentPin { get; set; }
        public string hobby { get; set; }
        public List<int> userRoles { get; set; }
    }
}
