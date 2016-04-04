using Schibsted.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Schibsted.Controllers
{
    [ApiAuthenticationFilter(true)]
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        // GET api/users        
        public IHttpActionResult Get()
        {
            var service = new Service.Security.SecurityService(Domain.Repository.MemoryRepository.SecurityMemoryRepository.Instance.Repository);

            var result = service.Roles.GetAll();

            return Ok(result);
        }

        // GET api/users/5
        public IHttpActionResult Get(long id)
        {
            var service = new Service.Security.SecurityService(Domain.Repository.MemoryRepository.SecurityMemoryRepository.Instance.Repository);

            var result = service.Roles.Read(id);
            
            return Ok(result);
        }

        // POST api/users
        public void Post([FromBody]string value)
        {
            var service = new Service.Security.SecurityService(Domain.Repository.MemoryRepository.SecurityMemoryRepository.Instance.Repository);

            var result = service.Roles.Create("ADMIN");

        }

        // PUT api/users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
        }
    }
}
