using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace Schibsted.Tests.Repository.MemoryRespository
{
    [TestClass]
    public class UnitTestMemoryRepositoryTableOperations
    {

        private void IRepositoryItemConversion()
        {
            Domain.Model.Security.Roles.Role a = new Domain.Model.Security.Roles.Role();

            a.Id = 1;
            a.Name = "pepe";

            List<Domain.Model.Base.IRepositoryItem> a1 = new List<Domain.Model.Base.IRepositoryItem>();

            a1.Add(a);

            var b = a1[0];

            var i = b.Id;

            var c = b as Domain.Model.Security.Roles.Role;

            var nm = c.Name;
        }

        private bool CreateRow(Domain.Repository.Operations.MemoryRepositoryTableOperations ops)
        {
            var newRow = new Domain.Model.Security.Roles.Role();

            newRow.Id = 1;
            newRow.Name = "ADMIN";

            var result = ops.Create(item: newRow);

            return result;
        }

        [TestMethod]
        public void TestMethodCreateTableOperations()
        {
            List<Domain.Model.Base.IRepositoryItem> roleRows = new List<Domain.Model.Base.IRepositoryItem>();
            
            var memoryRepoTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows: roleRows);

            Assert.IsNotNull(memoryRepoTableOps);

        }

        [TestMethod]
        public void TestMethodCreateRow()
        {
            List<Domain.Model.Base.IRepositoryItem> roleRows = new List<Domain.Model.Base.IRepositoryItem>();
            var memoryRepoTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows: roleRows);

            var result = CreateRow(ops: memoryRepoTableOps);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMethodDeleteRow()
        {
            List<Domain.Model.Base.IRepositoryItem> roleRows = new List<Domain.Model.Base.IRepositoryItem>();
            var memoryRepoTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows: roleRows);
            long mockId = 1;

            var createResult = CreateRow(ops: memoryRepoTableOps);

            if (createResult)
            {
                var result = memoryRepoTableOps.Delete(mockId);

                Assert.IsTrue(result);
            }
            else
                Assert.Fail();
        }

        [TestMethod]
        public void TestMethodReadRow()
        {
            List<Domain.Model.Base.IRepositoryItem> roleRows = new List<Domain.Model.Base.IRepositoryItem>();
            var memoryRepoTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows: roleRows);
            long mockId = 1;

            var result = CreateRow(ops: memoryRepoTableOps);

            var item = memoryRepoTableOps.Read(Id: mockId);

            Assert.AreEqual(mockId, item.Id);
        }

        [TestMethod]
        public void TestMethodUpdateRow()
        {
            List<Domain.Model.Base.IRepositoryItem> roleRows = new List<Domain.Model.Base.IRepositoryItem>();
            var memoryRepoTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows: roleRows);
            long mockId = 1;
            string assertEqual = "Modified";

            var resultCreate = CreateRow(ops: memoryRepoTableOps);

            var updRole = memoryRepoTableOps.Read(Id: mockId) as Domain.Model.Security.Roles.Role;

            if (updRole != null)
            {
                updRole.Name = assertEqual;

                var result = memoryRepoTableOps.Update(updRole);

                Assert.IsTrue(result);

            }
            else
                Assert.Fail();           
        }

        [TestMethod]
        public void TestMethodGetAllRows()
        {
            List<Domain.Model.Base.IRepositoryItem> roleRows = new List<Domain.Model.Base.IRepositoryItem>();
            var memoryRepoTableOps = new Domain.Repository.Operations.MemoryRepositoryTableOperations(rows: roleRows);
            
            var result = CreateRow(ops: memoryRepoTableOps);

            var allRows = memoryRepoTableOps.GetAll();

            Assert.IsNotNull(allRows);
        }

    }
}
