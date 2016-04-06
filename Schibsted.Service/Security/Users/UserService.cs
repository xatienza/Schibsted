using Schibsted.Domain.Repository.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Service.Security.Users
{
    public class UserService
    {
        #region Singleton instance

        public static UserService GetInstance(IRepositoryTableOperations TableOps)
        {
        if (instance == null)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new UserService(TableOps);
            }
        }

        return instance;

        }

        #endregion

        #region Attributes
        private IRepositoryTableOperations tableOps;
        private static volatile UserService instance;
        private static object syncRoot = new Object();
        #endregion

        #region Ctor
        private UserService(IRepositoryTableOperations TableOps) { tableOps = TableOps; }

        #endregion

        public bool IsUserAdmin(Domain.Model.Security.Users.User user)
        {

            if (user == null)
                return false;

            if (user.Roles == null || user.Roles.Count() == 0)
                return false;

            if (user.Roles.ConvertAll(o => (Domain.Model.Security.Roles.Role)o)
                .Where(x => x.Name == "ADMIN").Count() == 0)
                return false;
            else
                return true;
        }

        public bool Create(string UserName, string Password, Domain.Model.Security.Roles.Role Role)
        {
            var result = false;

            if (ExistsUserName(UserName))
                return false;

            var roles = new List<Domain.Model.Base.IRepositoryItem>();
            roles.Add(Role);

            Domain.Model.Security.Users.User newUser = new Domain.Model.Security.Users.User{
                UserName = UserName,
                Password = Password,
                Roles = roles
            };

            if (tableOps != null)
                result = tableOps.Create(newUser);

            return result;
        }

        public bool Update(long Id, string UserName, string Password, string Role)
        {
            var result = false;
            Domain.Model.Security.Users.User user = null;

            if (tableOps != null)
            {
                user = tableOps.Read(Id) as Domain.Model.Security.Users.User;

                if (user != null)
                {
                    user.UserName = UserName;
                    user.Password = Password;

                    var role = new Domain.Model.Security.Roles.Role
                    {
                        Name = Role
                    };

                    user.Roles.Clear();
                    user.Roles.Add(role);

                    result = tableOps.Update(user);

                }
            }

            return result;
        }

        public bool Delete(long Id)
        {
            var result = false;

            if (tableOps != null)
                result = tableOps.Delete(Id);

            return result;
        }

        public bool ExistsUserName(string UserName)
        {
            bool result = false;

            if (tableOps != null)
            {
                var allUsers = GetAll();

                if (allUsers != null)
                    result = allUsers.Exists(u => u.UserName == UserName);
            }

            return result;
        }

        public Domain.Model.Security.Users.User Read(long Id)
        {
            Domain.Model.Security.Users.User result = null;

            if (tableOps != null)
                result = tableOps.Read(Id) as Domain.Model.Security.Users.User;

            return result;
        }

        public List<Domain.Model.Security.Users.User> GetAll()
        {
            List<Domain.Model.Security.Users.User> userList = null;
            List<Domain.Model.Base.IRepositoryItem> list = null;

            if (tableOps != null)
                list = tableOps.GetAll();

            userList =  list.ConvertAll(o => (Domain.Model.Security.Users.User)o);

            return userList;
        }

        public Domain.Model.Security.Users.User Authenticate(string username, string password)
        {
            var allUsers = GetAll();

            if (allUsers == null)
                return null;

            var user = allUsers.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();

            return user;
        }
    }
}
