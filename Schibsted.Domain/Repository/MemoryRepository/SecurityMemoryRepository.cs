using Schibsted.Domain.Repository.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schibsted.Domain.Repository.MemoryRepository
{
    public class SecurityMemoryRepository
    {
        #region Attributes
        private string dataBaseName = "Security";
        private IRepository mainRepository;
        private static volatile SecurityMemoryRepository instance;
        private static object syncRoot = new Object();
        private string rolesTableName = "Roles";
        private string usersTableName = "Users";
        #endregion

        #region Properties
        public IRepository Repository
        {
            get
            {
                return instance.mainRepository;
            }
        }

        public IRepositoryTableOperations UserTableOps
        {
            get
            {
                return getTableOps(usersTableName);
            }
        }

        public IRepositoryTableOperations RoleTableOps
        {
            get
            {
                return getTableOps(rolesTableName);
            }
        }
        
        #endregion

        #region Ctor
        private SecurityMemoryRepository() { CreateRepository(); }
        #endregion

        #region Singleton instance

        public static SecurityMemoryRepository Instance
        {
          get 
          {
             if (instance == null) 
             {
                lock (syncRoot) 
                {
                   if (instance == null)
                       instance = new SecurityMemoryRepository();
                }
             }

             return instance;
          }
        }

        #endregion

        #region Repository methods

        protected void CreateRepository()
        {
            mainRepository = new Domain.Repository.Repository();
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();

            var result = mainRepository.SetEngine(Domain.Model.Enums.RepositoryKind.Memory, memoryRepository);

            if (result)
                CreateMemorySecurityDataBase(memoryRepository);
        }

        protected void CreateMemorySecurityDataBase(Domain.Repository.MemoryRepository.MemoryRepository memoRepo)
        {

            if (memoRepo != null)
                memoRepo.CreateDataBase(dataBaseName);

            CreateSecurityMemoryTables(memoRepo);

        }

        protected void CreateSecurityMemoryTables(Domain.Repository.MemoryRepository.MemoryRepository memoRepo)
        {
            MemoryRepositoryDataBase db = memoRepo.DataBase as MemoryRepositoryDataBase;

            db.AddTable(CreateRolesMemoryTable());
            db.AddTable(CreateUsersUsersTable());
        }

        protected MemoryRepositoryTable CreateRolesMemoryTable()
        {
            var rolesTable = CreateMemeoryTable(rolesTableName);

            return rolesTable;
        }

        protected MemoryRepositoryTable CreateUsersUsersTable()
        {
            var usersTable = CreateMemeoryTable(usersTableName);

            return usersTable;
        }

        private MemoryRepositoryTable CreateMemeoryTable(string TableName)
        {
            List<Domain.Model.Base.IRepositoryItem> rows = new List<Domain.Model.Base.IRepositoryItem>();
            var roleTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows);
            var newTable = new MemoryRepositoryTable(TableName, roleTableOps);

            return newTable;
        }

        #endregion

        private IRepositoryTableOperations getTableOps(string TableName)
        {
            var memRepo = Repository.Engine as MemoryRepository;

            var db = memRepo.DataBase as MemoryRepositoryDataBase;

            if (db == null)
                return null;

            var table = db.FindTable(TableName);

            return table.Operations;
        }
    }
}
