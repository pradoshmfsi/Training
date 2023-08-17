using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public class CustomUser
    {
        public int userId;
        public string firstName;
        public string lastName;
        public string email;
        public string userRole;
        public string userHobby;
    }
    public partial class UserDetailsDiv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void AddUserBtn(object sender, EventArgs e)
        {
            Response.Redirect("UserForm.aspx");
        }

        [System.Web.Services.WebMethod]
        public static List<CustomUser> GetUsers()
        {
            List<CustomUser> users = null;
            using (var dbcontext = new userInfoEntities())
            {
                users = dbcontext.users.Select(s => new CustomUser{ userId = s.userId, firstName = s.fname,lastName = s.lname,email=s.email }).ToList();
                foreach(CustomUser user in users)
                {
                    user.userRole = string.Join(", ", dbcontext.userRoles.Where(s => s.userId == user.userId).Select(s => s.role.roleName).ToList());
                    user.userHobby = string.Join(", ", dbcontext.userHobbies.Where(s => s.userId == user.userId).Select(s => s.hobby).ToList());
                }
            }

            return users;
        }
    }
}