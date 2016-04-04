using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schibsted.Domain.Repository.MemoryRepository;
using System.Collections.Generic;

namespace Schibsted.Tests.Repository
{
    [TestClass]
    public class UnitTestRepository
    {
        private MemoryRepositoryTable CreateTable(string TableName)
        {
            List<Domain.Model.Base.IRepositoryItem> rows = new List<Domain.Model.Base.IRepositoryItem>();
            var roleTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows);
            var newTable = new MemoryRepositoryTable(TableName, roleTableOps);

            return newTable;
        }

        [TestMethod]
        public void TestMethodRepositoryCreate()
        {
            var repository = new Domain.Repository.Repository();

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void TestMethodRepositorySetMemoryRepositoryEngine()
        {
            var repository = new Domain.Repository.Repository();
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();

            var result = repository.SetEngine(Domain.Model.Enums.RepositoryKind.Memory, memoryRepository);


            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMethodRepositoryGetMemoryRepositoryEngine()
        {
            var repository = new Domain.Repository.Repository();
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();

            repository.SetEngine(Domain.Model.Enums.RepositoryKind.Memory, memoryRepository);

            Assert.IsNotNull(repository.Engine);

        }

        [TestMethod]
        public void TestMethodRepositoryGetMemoryRepositoryAsMemoryRepository()
        {
            var repository = new Domain.Repository.Repository();
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();
            Domain.Repository.MemoryRepository.IMemoryRepository memoRepo = null;

            repository.SetEngine(Domain.Model.Enums.RepositoryKind.Memory, memoryRepository);

            if (repository.Engine is Domain.Repository.MemoryRepository.IMemoryRepository)
                memoRepo = repository.Engine as Domain.Repository.MemoryRepository.IMemoryRepository;

            Assert.IsNotNull(memoRepo);

        }

        [TestMethod]
        public void TestMethodMemoryRepositoryAddTableToDataBase()
        {
            var tableName = "Roles";
            var dataBaseName = "Security";
            var repository = new Domain.Repository.Repository();
            var memoryRepository = new Domain.Repository.MemoryRepository.MemoryRepository();


            memoryRepository.CreateDataBase(dataBaseName);
            var rolesTable = CreateTable(tableName);

            memoryRepository.DataBase.AddTable(NewTable: rolesTable);

            var result = repository.SetEngine(Domain.Model.Enums.RepositoryKind.Memory, memoryRepository);

            Assert.IsTrue(result);
        }
    }
}
