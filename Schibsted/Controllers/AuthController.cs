using Schibsted.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Schibsted.Controllers
{
    [RoutePrefix("api/Auth")]
    [EnableCors("*", "*", "*")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("SignIn")]
        public IHttpActionResult SignIn([FromBody]AuthenticateViewModel credentials)
        {
            if (String.IsNullOrEmpty(credentials.Username))
                return InternalServerError(new ArgumentNullException("You have to set password"));

            if (String.IsNullOrEmpty(credentials.Password))
                return InternalServerError(new ArgumentNullException("You have to set role"));

            var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

            var result = service.User.Authenticate(credentials.Username, credentials.Password);

            if (result != null)
                return Ok(result);
            else
                return Unauthorized();
        }
    }
}
