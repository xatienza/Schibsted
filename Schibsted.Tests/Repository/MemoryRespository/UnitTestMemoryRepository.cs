using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schibsted.Domain.Repository.MemoryRepository;
using System.Collections.Generic;

namespace Schibsted.Tests.Repository.MemoryRespository
{
    [TestClass]
    public class UnitTestMemoryRepository
    {
        private MemoryRepositoryTable CreateTable(string TableName)
        {
            List<Domain.Model.Base.IRepositoryItem> rows = new List<Domain.Model.Base.IRepositoryItem>();
            var roleTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows);
            var newTable = new MemoryRepositoryTable(TableName, roleTableOps);

            return newTable;
        }

        [TestMethod]
        public void TestMethodMemoryRepositoryCreate()
        {
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();

            Assert.IsNotNull(memoryRepository);
        }

        [TestMethod]
        public void TestMethodMemoryRepositoryCreateDataBase()
        {
            var dataBaseName = "Security";
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();

            var result = memoryRepository.CreateDataBase(dataBaseName);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMethodMemoryRepositoryHasDataBase()
        {
            var dataBaseName = "Security";
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();

            memoryRepository.CreateDataBase(dataBaseName);

            var result = memoryRepository.HasDataBase;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMethodMemoryRepositoryAddTableToDataBase()
        {
            var tableName = "Roles";
            var dataBaseName = "Security";
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();

            memoryRepository.CreateDataBase(dataBaseName);
            var rolesTable = CreateTable(tableName);

            var result = memoryRepository.DataBase.AddTable(NewTable: rolesTable);

            Assert.IsTrue(result);
        }
    }
}
