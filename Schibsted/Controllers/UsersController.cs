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

        #region Private method

        private List<Models.Security.User.UserRequestViewModel> convertToUserViewModelList(List<Domain.Model.Security.Users.User> users)
        {
            List<Models.Security.User.UserRequestViewModel> usersVM = users.ConvertAll(x => new Models.Security.User.UserRequestViewModel
            {
                Id = x.Id,
                Username = x.UserName,
                Password = x.Password,
                Role = ((Domain.Model.Security.Roles.Role)x.Roles.FirstOrDefault()).Name
            });

            return usersVM;
        }

        private Models.Security.User.UserRequestViewModel convertToUserViewModel(Domain.Model.Security.Users.User user)
        {
            var userVM = new Models.Security.User.UserRequestViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Password = user.Password,
                Role = ((Domain.Model.Security.Roles.Role)user.Roles.FirstOrDefault()).Name
            };

            return userVM;
        }

        #endregion

        #region REST Actions
        // GET api/users
        public IHttpActionResult Get()
        {
            var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

            var users = service.User.GetAll();

            if (users != null)
            {
                var usersVM = convertToUserViewModelList(users); 

                return Ok(usersVM);
            }
            else
                return InternalServerError();
        }

        // GET api/users/5
        public IHttpActionResult Get(long id)
        {
            var service = new Service.Security.SecurityService(WebApiApplication.mainRepository);

            var user = service.User.Read(id);

            var userVM = convertToUserViewModel(user);

            return Ok(userVM);
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


            if (service.User.ExistsUserName(user.Username))
                return InternalServerError(new ArgumentNullException("Duplicate user"));

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

            var result = service.User.Update(user.Id, user.Username, user.Password, user.Role);


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
