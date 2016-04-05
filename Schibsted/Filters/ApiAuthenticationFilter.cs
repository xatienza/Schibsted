using Schibsted.Service.Security;
using System.Threading;
using System.Web.Http.Controllers;
using System.Linq;

namespace Schibsted.Filters
{
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public ApiAuthenticationFilter()
        {
        }

        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive"></param>
        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        /// <summary>
        /// Protected overriden method for authorizing user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var provider = new Service.Security.SecurityService(Domain.Repository.MemoryRepository.SecurityMemoryRepository.Instance.Repository);

            if (provider != null)
            {
                var user = provider.User.Authenticate(username, password);

                if (user == null)
                    return false;
                else
                {
                    if (provider.User.IsUserAdmin(user))
                        return true;
                    else
                    {
                        if (actionContext.Request.Method == System.Net.Http.HttpMethod.Get)
                            return true;
                        else
                            return false;
                    }
                }
            }

            return false;
        }
    }
}