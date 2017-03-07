using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestAPISample.Domain;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RestAPISample.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET: api/values
        [HttpGet]
        public List<User> Get()
        {
            return new  List<User>{
                new User {
                    Id = 1,
                    Name = "user 1" },
                new User {
                    Id = 2,
                    Name = "user 2" },
                new User {
                    Id = 3,
                    Name = "user 3" },
                new User {
                    Id = 4,
                    Name = "user 4" }
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return new User
            {
                Id = 5,
                Name = "user 5"
            };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]User user)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User user)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
