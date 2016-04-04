using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schibsted.Domain.Repository;
using Schibsted.Service.Security;

namespace Schibsted.Tests.Service
{
    [TestClass]
    public class UnitTestsSecurityService
    {
        private bool createUser(string username, string password, string role)
        {
            IRepository mainRepository = Domain.Repository.MemoryRepository.SecurityMemoryRepository.Instance.Repository;

            var securityService = new SecurityService(mainRepository);

            var result = securityService.User.Create(username, password, role);

            return result;
        }

        [TestMethod]
        public void TestMethodSecurityServiceCreate()
        {
            IRepository mainRepository = Domain.Repository.MemoryRepository.SecurityMemoryRepository.Instance.Repository;

            var securityService = new SecurityService(mainRepository);

            Assert.IsNotNull(securityService);
        }

        [TestMethod]
        public void TestMethodSecurityServiceCreateDefaultAdminUser()
        {
            var result = createUser("admin", "1234", "ADMIN");

            Assert.IsTrue(result);
         }

        [TestMethod]
        public void TestMethodSecurityServiceAuthenticate()
        {
            var result = createUser("admin", "1234", "ADMIN");

            IRepository mainRepository = Domain.Repository.MemoryRepository.SecurityMemoryRepository.Instance.Repository;
            var securityService = new SecurityService(mainRepository);

            var user = securityService.Authenticate("admin", "1234");

            Assert.IsNotNull(result);
        }
    }
}
