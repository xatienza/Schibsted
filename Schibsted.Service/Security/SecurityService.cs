using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Schibsted.Domain.Repository;
using Schibsted.Domain.Repository.MemoryRepository;
using Schibsted.Service.Security.Users;
using Schibsted.Service.Security.Roles;

namespace Schibsted.Service.Security
{
    public class SecurityService : Schibsted.Service.Security.ISecurityService
    {
        private string dataBaseName = "Security";
        private IRepository mainRepository;
        private UserService userService;
        private RoleService rolesService;

        public UserService User
        {
            get { return userService; }
        }

        public RoleService Roles
        {
            get { return rolesService; }
        }

        public SecurityService(IRepository repository)
        {
            mainRepository = repository;

            initService();
        }

        protected void initService()
        {
            var userServiceOps = SecurityMemoryRepository.Instance.UserTableOps;
            userService = new UserService(userServiceOps);

            var roleServiceOps = SecurityMemoryRepository.Instance.RoleTableOps;
            rolesService = new RoleService(roleServiceOps);
        }

        public Domain.Model.Security.Users.User Authenticate(string username, string password)
        {
            var allUsers = User.GetAll();

            if (allUsers == null)
                return null;

            var user = allUsers.Where( u => u.UserName == username && u.Password == password).FirstOrDefault();

            return user;
        }

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


    }
}
