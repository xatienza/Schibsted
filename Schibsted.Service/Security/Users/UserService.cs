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
        #region Attributes
        private IRepositoryTableOperations tableOps;
        #endregion

        #region Ctor
        public UserService(IRepositoryTableOperations TableOps)
        {
            tableOps = TableOps;
        }
        #endregion

        public bool Create(string UserName, string Password, string Role)
        {
            var result = false;

            var role = new Domain.Model.Security.Roles.Role
            {
                Name = Role
            };

            var roles = new List<Domain.Model.Base.IRepositoryItem>();
            roles.Add(role);

            Domain.Model.Security.Users.User newUser = new Domain.Model.Security.Users.User{
                UserName = UserName,
                Password = Password,
                Roles = roles
            };

            if (tableOps != null)
                result = tableOps.Create(newUser);

            return result;
        }

        public bool Update(long Id, string Password, string Role)
        {
            var result = false;
            Domain.Model.Security.Users.User user = null;

            if (tableOps != null)
            {
                user = tableOps.Read(Id) as Domain.Model.Security.Users.User;

                if (user != null)
                {
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
    }
}
