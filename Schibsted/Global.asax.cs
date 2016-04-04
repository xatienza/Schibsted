using Schibsted.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Schibsted
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static int repositoryType;
        public static bool createDefaultAdmin;
        public static IRepository mainRepository;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            readWebApiConfig();

            createDefaultRespository();

            createDefaultAdminUser();
        }

        protected void readWebApiConfig()
        {
            WebApiApplication.repositoryType = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Repository.Kind"]);
            WebApiApplication.createDefaultAdmin = (System.Configuration.ConfigurationManager.AppSettings["Repository.AddDefaultAdmin"].ToLower() == "true")? true : false;
        }

        protected void createDefaultRespository()
        {
            if ((Domain.Model.Enums.RepositoryKind)WebApiApplication.repositoryType == Domain.Model.Enums.RepositoryKind.Memory)
                WebApiApplication.mainRepository = Domain.Repository.MemoryRepository.SecurityMemoryRepository.Instance.Repository;
        }

        protected void createDefaultAdminUser()
        {
            if (WebApiApplication.createDefaultAdmin)
            {
                var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

                var result = service.User.Create("admin", "1234", "ADMIN");
            }
        }
    }
}
