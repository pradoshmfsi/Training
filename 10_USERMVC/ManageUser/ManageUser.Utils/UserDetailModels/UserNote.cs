using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageUser.Utils.UserDetailModels
{
    public class UserNote
    {
        public int noteId { get; set; }
        public int userId { get; set; }
        public string noteMessage { get; set; }
        public int ifPrivate { get; set; }
        public string createdBy { get; set; }
        public System.DateTime createdOn { get; set; }
    }
}
