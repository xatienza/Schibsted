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
            userService = UserService.GetInstance(userServiceOps);

            var roleServiceOps = SecurityMemoryRepository.Instance.RoleTableOps;
            rolesService = RoleService.GetInstance(roleServiceOps);
        }

    }
}
