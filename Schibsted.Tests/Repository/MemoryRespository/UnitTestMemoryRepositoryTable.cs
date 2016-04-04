using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Schibsted.Domain.Repository.MemoryRepository;

namespace Schibsted.Tests.Repository.MemoryRespository
{
    [TestClass]
    public class UnitTestMemoryRepositoryTable
    {
        private MemoryRepositoryTable CreateTable(string TableName)
        {
            List<Domain.Model.Base.IRepositoryItem> rows = new List<Domain.Model.Base.IRepositoryItem>();
            var roleTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows);
            var newTable = new MemoryRepositoryTable(TableName, roleTableOps);

            return newTable;
        }

        [TestMethod]
        public void TestMethodCreateTableRoles()
        {
            var tableName = "Roles";
            var rolesTable = CreateTable(tableName);

            Assert.AreEqual(tableName, rolesTable.TableName);
        }

        [TestMethod]
        public void TestMethodCreateTableUsers()
        {
            var tableName = "Users";
            var rolesTable = CreateTable(tableName);

            Assert.AreEqual(tableName, rolesTable.TableName);
        }

        [TestMethod]
        public void TestMethodTableCreateNewRoleRow()
        {
            var tableName = "Roles";
            var rolesTable = CreateTable(tableName);
            var newRole = new Domain.Model.Security.Roles.Role
            {
                Id = 1,
                Name = "ADMIN"
            };

            var result = rolesTable.Operations.Create(item: newRole);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMethodTableCreateNewUserRow()
        {
            var tableName = "Roles";
            var rolesTable = CreateTable(tableName);
            var newRole = new Domain.Model.Security.Roles.Role
            {
                Id = 1,
                Name = "ADMIN"
            };

            var result = rolesTable.Operations.Create(item: newRole);

            Assert.IsTrue(result);
        }
    }
}
