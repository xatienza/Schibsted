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
        #region Singleton instance

        public static RoleService GetInstance(IRepositoryTableOperations TableOps)
        {
        if (instance == null)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new RoleService(TableOps);
            }
        }

        return instance;

        }

        #endregion

        #region Attributes
        private IRepositoryTableOperations tableOps;
        private static volatile RoleService instance;
        private static object syncRoot = new Object();
        #endregion

        #region Ctor
        private RoleService(IRepositoryTableOperations TableOps) { tableOps = TableOps; }

        #endregion

        public bool Create(string RoleName)
        {
            var result = false;

            if (Exists(RoleName))
                return false;

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

        public bool Exists(string RoleName)
        {
            bool result = false;

            if (tableOps != null)
            {
                var allRoles = GetAll();

                if (allRoles != null)
                    result = allRoles.Exists(u => u.Name == RoleName);
            }

            return result; 
        }

        public Domain.Model.Security.Roles.Role GetByName(string RoleName)
        {
            Domain.Model.Security.Roles.Role result = null;

            if (tableOps != null)
            {
                var allRoles = GetAll();

                if (allRoles != null)
                    result = allRoles.Where(r => r.Name == RoleName).FirstOrDefault();
            }

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
