using Schibsted.Filters;
using Schibsted.Models.Security.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Schibsted.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        #region REST Actions
        // GET api/users
        public IHttpActionResult Get()
        {
            var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

            var result = service.User.GetAll();

            return Ok(result);
        }

        // GET api/users/5
        public IHttpActionResult Get(long id)
        {
            var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

            var result = service.User.Read(id);
            
            return Ok(result);
        }

        // POST api/users
        //[ApiAuthenticationFilter(true)]
        public IHttpActionResult Post([FromBody]UserRequestViewModel user)
        {
            if (user == null)
                return InternalServerError(new ArgumentNullException("You have to set user data"));

            if (String.IsNullOrEmpty(user.Username))
                return InternalServerError(new ArgumentNullException("You have to set username"));

            if (String.IsNullOrEmpty(user.Password))
                return InternalServerError(new ArgumentNullException("You have to set password"));

            if (String.IsNullOrEmpty(user.Role))
                return InternalServerError(new ArgumentNullException("You have to set role"));

            var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

            var role = service.Roles.GetByName(user.Role);

            if (role == null)
                return InternalServerError(new ArgumentNullException("Role does no exists"));

            var result = service.User.Create(user.Username, user.Password, role);

            if (result == true)
                return Ok(); 
            else 
                return InternalServerError();
        }

        // PUT api/users/Update
        [HttpPut]
        [Route("Update")]
        //[ApiAuthenticationFilter(true)]
        public IHttpActionResult Update([FromBody]UserRequestViewModel user)
        {

            if (user.Id <= 0)
                return InternalServerError(new ArgumentNullException("User Id cannot be lower than 0"));

            if (String.IsNullOrEmpty(user.Password))
                return InternalServerError(new ArgumentNullException("You have to set password"));

            if (String.IsNullOrEmpty(user.Role))
                return InternalServerError(new ArgumentNullException("You have to set role"));

            var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

            var result = service.User.Update(user.Id, user.Password, user.Role);

            if (result == true)
                return Ok();
            else
                return InternalServerError();
        }

        // DELETE api/users/5
        //[ApiAuthenticationFilter(true)]
        public IHttpActionResult Delete(int id)
        {
            var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

            var result = service.User.Delete(id);

            if (result == true)
                return Ok();
            else
                return InternalServerError();
        }

        #endregion
    }
}
