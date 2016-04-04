using Schibsted.Domain.Repository.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Service.Security.Roles
{
    public class RoleService
    {
        #region Attributes
        private IRepositoryTableOperations tableOps;
        #endregion

        #region Ctor
        public RoleService(IRepositoryTableOperations TableOps)
        {
            tableOps = TableOps;
        }
        #endregion

        public bool Create(string RoleName)
        {
            var result = false;

            Domain.Model.Security.Roles.Role newItem = new Domain.Model.Security.Roles.Role
            {
                Name = RoleName
            };

            if (tableOps != null)
                result = tableOps.Create(newItem);

            return result;
        }

        public bool Delete(long Id)
        {
            var result = false;

            if (tableOps != null)
                result = tableOps.Delete(Id);

            return result;
        }

        public Domain.Model.Security.Roles.Role Read(long Id)
        {
            Domain.Model.Security.Roles.Role result = null;

            if (tableOps != null)
                result = tableOps.Read(Id) as Domain.Model.Security.Roles.Role;

            return result;
        }

        public List<Domain.Model.Security.Roles.Role> GetAll()
        {
            List<Domain.Model.Security.Roles.Role> userList = null;
            List<Domain.Model.Base.IRepositoryItem> list = null;

            if (tableOps != null)
                list = tableOps.GetAll();

            userList = list.ConvertAll(o => (Domain.Model.Security.Roles.Role)o);

            return userList;
        }
    }
}
