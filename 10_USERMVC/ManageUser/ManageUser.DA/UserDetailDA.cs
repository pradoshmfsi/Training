using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using ManageUser.Utils;
using ManageUser.Utils.UserDetailModels;
namespace ManageUser.DA
{
    public class UserDetailDA
    {
        public static List<UserRole> GetUserRoles(int userId)
        {
            using (var dbcontext = new userInfoEntities())
            {
                return dbcontext.userRoles.Where(s => s.userId == userId).Select(s => new UserRole
                {
                    id = s.id,
                    userId = s.userId,
                    roleId = s.roleId,
                }).ToList();
            }
        }

        public static List<UserHobby> GetUserHobbies(int userId)
        {
            using (var dbcontext = new userInfoEntities())
            {
                return dbcontext.userHobbies
                    .Where(i => i.userId == userId)
                    .Select(s => new UserHobby
                    {
                        id = s.id,
                        userId = s.userId,
                        hobby = s.hobby
                    }).ToList();

            }
        }

        public static User GetUser(int userId)
        {
            user getUser = GetUserEntity(userId);
            if (getUser == null) return null;
            return MapUser(new User(), getUser); ;
        }

        public static List<Role> GetRolesAll()
        {
            using (var dbcontext = new userInfoEntities())
            {
                return dbcontext.roles.Select(s => new Role
                {
                    roleId = s.roleId,
                    roleName = s.roleName
                }).ToList();
            }
        }

        public static List<User> GetUsersAll()
        {
            using (var dbcontext = new userInfoEntities())
            {
                var users =  dbcontext.users.ToList();
                List<User> NewUsers = new List<User>();
                foreach(var user in users)
                {
                    NewUsers.Add(MapUser(new User(),user));
                }
                return NewUsers;
            }
        }

        public static User MapUser( User NewUser ,user user)
        {
            NewUser.userId = user.userId;
            NewUser.firstName = user.firstName;
            NewUser.lastName = user.lastName;
            NewUser.email = user.email;
            NewUser.password = user.password;
            NewUser.gender = user.gender;
            NewUser.dob = user.dob;
            NewUser.profilePic = user.profilePic;
            NewUser.profilePicActual = user.profilePicActual;
            NewUser.presentAddressLine1 = user.presentAddressLine1;
            NewUser.presentAddressLine2 = user.presentAddressLine2;
            NewUser.presentCountryId = user.presentCountryId;
            NewUser.presentStateId = user.presentStateId;
            NewUser.presentCity = user.presentCity;
            NewUser.presentPin = user.presentPin;
            NewUser.permanentAddressLine1 = user.permanentAddressLine1;
            NewUser.permanentAddressLine2 = user.permanentAddressLine2;
            NewUser.permanentCountryId = user.permanentCountryId;
            NewUser.permanentStateId = user.permanentStateId;
            NewUser.permanentCity = user.permanentCity;
            NewUser.permanentPin = user.permanentPin;

            return NewUser;
        }

        public static List<Country> GetCountriesAll()
        {
            using (var dbcontext = new userInfoEntities())
            {
                return dbcontext.countries.Select(s => new Country
                {
                    countryId = s.countryId,
                    countryName = s.countryName
                }).ToList();
            }
        }

        public static List<State> GetStates(int countryId)
        {
            using (var dbcontext = new userInfoEntities())
            {
                return dbcontext.states.Where(s => s.countryId == countryId).Select(s => new State
                {
                    stateId = s.stateId,
                    stateName = s.stateName
                }).ToList();
            }
        }

        public static AuthUser GetUserByEmail(string email)
        {
            using (var dbcontext = new userInfoEntities())
            {
                return dbcontext.users.Select(s => new AuthUser
                {
                    userId = s.userId,
                    email = s.email,
                    password = s.password
                }).FirstOrDefault(s => s.email == email);
            }
        }

        public static List<UserDocument> GetDocuments(int userId)
        {
            try{
                using (var dbcontext = new userInfoEntities())
                {
                    return dbcontext.userDocuments.Where(s => s.userId == userId).Select(s => new UserDocument
                    {
                        id = s.id,
                        userId = s.userId,
                        documentName = s.documentName,
                        createdBy = s.createdBy,
                        createdOn = s.createdOn,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                UserDetailUtil.LogError(ex);
                return null;
            }

        }

        public static bool DeleteUser(int userId)
        {
            try
            {
                using (var dbcontext = new userInfoEntities())
                {
                    var user = dbcontext.users.FirstOrDefault(i => i.userId == userId);
                    dbcontext.users.Remove(user);
                    dbcontext.SaveChanges();
                    return true;
                }
            }
            catch (EntityException ex)
            {
                UserDetailUtil.LogError(ex);
                return false;
            }
        }

        public static List<UserNote> GetUserNotes(int userId, int isAdmin)
        {
            using (var dbcontext = new userInfoEntities())
            {
                return dbcontext.userNotes.Where(i => i.userId == userId && ((isAdmin==1) || i.ifPrivate == 0)).Select(s => new UserNote
                {
                    noteId = s.noteId,
                    userId = s.userId,
                    noteMessage = s.noteMessage,
                    createdBy = s.createdBy,
                    createdOn = s.createdOn,
                    ifPrivate = s.ifPrivate
                }).ToList();
            }
        }

        public static bool AddNotesToDB(UserNote newNote)
        {
            try
            {
                using (var dbcontext = new userInfoEntities())
                {
                    userNote userNoteEntity = new userNote
                    {
                        userId = newNote.userId,
                        noteMessage = newNote.noteMessage,
                        createdBy = newNote.createdBy,
                        createdOn = newNote.createdOn,
                        ifPrivate = newNote.ifPrivate
                    };
                    dbcontext.userNotes.Add(userNoteEntity);
                    dbcontext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                UserDetailUtil.LogError(ex);
                return false;
            }
        }

        public static bool AddDocumentsToDB(UserDocument newDocument)
        {
            try
            {
                using (var dbcontext = new userInfoEntities())
                {
                    userDocument userDocumentEntity = new userDocument
                    {
                        userId = newDocument.userId,
                        documentName = newDocument.documentName,
                        documentNameActual = newDocument.documentNameActual,
                        createdBy = newDocument.createdBy,
                        createdOn = newDocument.createdOn
                    };
                    dbcontext.userDocuments.Add(userDocumentEntity);
                    dbcontext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                UserDetailUtil.LogError(ex);
                return false;
            }
        }

        public static user GetUserEntity(int userId)
        {
            using(var dbcontext = new userInfoEntities())
            {
                return dbcontext.users.FirstOrDefault(s => s.userId == userId);
            }
            
        }

        public static bool AddUserToDB(UserReceived user)
        {
            user NewUser = null;
            try
            {
                using (var dbcontext = new userInfoEntities())
                {
                    NewUser = user.userId != 0 ? dbcontext.users.FirstOrDefault(s => s.userId == user.userId) : new user();
                    NewUser.firstName = user.firstName;
                    NewUser.lastName = user.lastName;
                    NewUser.email = user.email;
                    if (user.password!=null)
                    {
                        NewUser.password = user.password;
                    }                 
                    NewUser.gender = user.gender;
                    NewUser.dob = DateTime.Parse(user.dob);

                    if(user.profilePic!=null)
                    {
                        NewUser.profilePic = user.profilePic;
                        NewUser.profilePicActual = user.profilePicActual;
                    }
                    
                    NewUser.presentAddressLine1 = user.presentAddressLine1;
                    NewUser.presentAddressLine2 = user.presentAddressLine2;
                    NewUser.presentCountryId = Int32.Parse(user.presentCountry);
                    NewUser.presentStateId = Int32.Parse(user.presentState);
                    NewUser.presentCity = user.presentCity;
                    NewUser.presentPin = user.presentPin;

                    NewUser.permanentAddressLine1 = user.permanentAddressLine1;
                    NewUser.permanentAddressLine2 = user.permanentAddressLine2;
                    NewUser.permanentCountryId = Int32.Parse(user.permanentCountry);
                    NewUser.permanentStateId = Int32.Parse(user.permanentState);
                    NewUser.permanentCity = user.permanentCity;
                    NewUser.permanentPin = user.permanentPin;

                    if (user.userId == 0)
                    {
                        dbcontext.users.Add(NewUser);
                    }
                    else
                    {
                        var userRoleList = dbcontext.userRoles.Where(i => i.userId == user.userId).ToList();
                        dbcontext.userRoles.RemoveRange(userRoleList);

                        var userHobbyList = dbcontext.userHobbies.Where(i => i.userId == user.userId).ToList();
                        dbcontext.userHobbies.RemoveRange(userHobbyList);
                    }

                    user.userRoles.ForEach((roleId) =>
                    {
                        userRole NewUserRole = new userRole
                        {
                            userId = NewUser.userId,
                            roleId = roleId
                        };
                        dbcontext.userRoles.Add(NewUserRole);
                    });

                    var hobbyList = user.hobby.Split(',');
                    foreach (var hobby in hobbyList)
                    {
                        userHobby NewHobby = new userHobby
                        {
                            userId = NewUser.userId,
                            hobby = hobby.Trim()
                        };
                        dbcontext.userHobbies.Add(NewHobby);
                    }

                    dbcontext.SaveChanges();
                }
                return true;
                
            }
            catch (Exception ex)
            {
                UserDetailUtil.LogError(ex);
                return false;
            }
                     
        }

        public static UserReceived GetUserAJAX(int userId)
        {
            user user = GetUserEntity(userId);
            UserReceived NewUser = new UserReceived();
            NewUser.userId = user.userId;
            NewUser.firstName = user.firstName;
            NewUser.lastName = user.lastName;
            NewUser.email = user.email;
            NewUser.password = user.password;
            NewUser.gender = user.gender;
            NewUser.dob = user.dob.ToString("yyyy-MM-dd");
            NewUser.profilePic = user.profilePic;
            NewUser.profilePicActual = user.profilePicActual;

            NewUser.presentAddressLine1 = user.presentAddressLine1;
            NewUser.presentAddressLine2 = user.presentAddressLine2;
            NewUser.presentCountry = user.presentCountryId.ToString();
            NewUser.presentState = user.presentStateId.ToString();
            NewUser.presentCity = user.presentCity;
            NewUser.presentPin = user.presentPin;

            NewUser.permanentAddressLine1 = user.permanentAddressLine1;
            NewUser.permanentAddressLine2 = user.permanentAddressLine2;
            NewUser.permanentCountry = user.permanentCountryId.ToString();
            NewUser.permanentState = user.permanentStateId.ToString();
            NewUser.permanentCity = user.permanentCity;
            NewUser.permanentPin = user.permanentPin;
            NewUser.userRoles = GetUserRoles(userId).Select(s=>s.roleId).ToList();
            NewUser.hobby = String.Join(", ", GetUserHobbies(userId).Select(s => s.hobby).ToList());

            return NewUser;
        }
    }
}
