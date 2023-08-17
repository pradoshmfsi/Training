using ManageUser.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUser.Utils.UserDetailModels;
namespace ManageUser.Business
{
    public class UserDetailBusiness
    {
        public static bool IsAdmin(int userId)
        {
            List<int> roleList = GetUserRoleId(userId);
            if (roleList.Contains(4))
            {
                return true;
            }
            return false;
        }

        public static List<int> GetUserRoleId(int userId)
        {
            return UserDetailDA.GetUserRoles(userId).Select(s => s.roleId).ToList();
        }

        public static string GetHobbies(int userId)
        {
            return string.Join(", ", UserDetailDA.GetUserHobbies(userId).Select(s => s.hobby).ToList());
        }

        public static User GetUser(int userId)
        {
            return UserDetailDA.GetUser(userId);
        }

        public static List<User> GetUsersAll()
        {
            return UserDetailDA.GetUsersAll();
        }

        public static List<Role> GetRolesAll()
        {
            return UserDetailDA.GetRolesAll();
        }

        public static List<Country> GetCountriesAll()
        {
            return UserDetailDA.GetCountriesAll();
        }

        public static List<State> GetStates(int countryId)
        {
            return UserDetailDA.GetStates(countryId);
        }

        public static bool IfEmailAlreadyExists(string email)
        {
            return UserDetailDA.GetUserByEmail(email) != null;
        }

        public static AuthUser GetUserByEmail(string email)
        {
            return UserDetailDA.GetUserByEmail(email);
        }
        public static List<UserDocument> GetDocuments(int userId)
        {
            return UserDetailDA.GetDocuments(userId);
        }

        public static bool DeleteUser(int userId)
        {
            return UserDetailDA.DeleteUser(userId);
        }

        public static List<UserNote> GetUserNotes(int userId, int isAdmin)
        {
            return UserDetailDA.GetUserNotes(userId, isAdmin);
        }
        public static bool AddNotesToDB(UserNote newNote)
        {
            return UserDetailDA.AddNotesToDB(newNote);
        }

        public static bool AddDocumentsToDB(UserDocument newDocument)
        {
            return UserDetailDA.AddDocumentsToDB(newDocument);
        }

        public static bool AddUserToDB(UserReceived user)
        {
            return UserDetailDA.AddUserToDB(user);
        }

        public static UserReceived GetUserAJAX(int userId)
        {
            return UserDetailDA.GetUserAJAX(userId);
        }
    }
}
