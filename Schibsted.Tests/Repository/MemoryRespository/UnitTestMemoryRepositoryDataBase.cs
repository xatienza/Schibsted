using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schibsted.Domain.Repository.MemoryRepository;
using System.Collections.Generic;

namespace Schibsted.Tests.Repository.MemoryRespository
{
    [TestClass]
    public class UnitTestMemoryRepositoryDataBase
    {

        private MemoryRepositoryTable CreateTable(string TableName)
        {
            List<Domain.Model.Base.IRepositoryItem> rows = new List<Domain.Model.Base.IRepositoryItem>();
            var roleTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows);
            var newTable = new MemoryRepositoryTable(TableName, roleTableOps);

            return newTable;
        }

        private MemoryRepositoryDataBase CreateDataBase(string DataBaseName)
        {
            var newDataBase = new MemoryRepositoryDataBase(DataBaseName);

            return newDataBase;
        }

        [TestMethod]
        public void TestMethodCreateDateBase()
        {
            var dataBaseName = "Security";
            var newDataBase = CreateDataBase(dataBaseName);

            Assert.IsNotNull(newDataBase);
        }

        [TestMethod]
        public void TestMethodDateBaseAddRolesTable()
        {
            var tableName = "Roles";
            var dataBaseName = "Security";
            var newDataBase = CreateDataBase(dataBaseName);
            var rolesTable = CreateTable(tableName);

            var result =  newDataBase.AddTable(NewTable: rolesTable);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestMethodDateBaseFindRolesTable()
        {
            var tableName = "Roles";
            var dataBaseName = "Security";
            var newDataBase = CreateDataBase(dataBaseName);
            var rolesTable = CreateTable(tableName);
            var result = false;
            IMemoryRepositoryTable findedTable = null;


            var createResult = newDataBase.AddTable(NewTable: rolesTable);

            if (createResult)
                findedTable = newDataBase.FindTable(TableName: tableName);

            Assert.IsNotNull(findedTable);

        }

        [TestMethod]
        public void TestMethodDateBaseAddUserRowToTable()
        {
            var tableName = "Users";
            var dataBaseName = "Security";
            var newDataBase = CreateDataBase(dataBaseName);
            var rolesTable = CreateTable(tableName);
            var result = false;
            IMemoryRepositoryTable usersTable = null;


            var createResult = newDataBase.AddTable(NewTable: rolesTable);

            if (createResult)
            {
                usersTable = newDataBase.FindTable(TableName: tableName);

                if (usersTable != null)
                {

                    Domain.Model.Base.IRepositoryItem newRole = new Domain.Model.Security.Roles.Role
                    {
                        Id = 1,
                        Name = "ADMIN"
                    };

                    var newUser = new Domain.Model.Security.Users.User
                    {
                        Id = 1,
                        UserName = "xsancheza",
                        Password = "*********",
                        Roles = new List<Domain.Model.Base.IRepositoryItem>()
                    };

                    newUser.Roles.Add(newRole);

                    result = usersTable.Operations.Create(newUser);


                }

            }

            Assert.IsTrue(result);

        }
    }
}
