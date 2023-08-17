using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageUser.Utils.UserDetailModels
{
    public class UserDocument
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string documentName { get; set; }
        public string documentNameActual { get; set; }
        public System.DateTime createdOn { get; set; }
        public string createdBy { get; set; }
    }
}
