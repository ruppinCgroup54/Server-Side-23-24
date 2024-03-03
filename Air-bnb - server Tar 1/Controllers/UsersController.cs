using Microsoft.AspNetCore.Mvc;
using System;
using Air_bnb.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Air_bnb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            List<User> usersList= BL.User.Read();
            return usersList.Count!=0 ? Ok(usersList): Conflict("There is no users.") ;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            return newUser.Insert()==1 ? Ok(newUser) : Conflict(new { message = $"An existing record with the id" });
        }

        // POST login
        [HttpPost("login")]
        public User Login([FromBody] User loginUser)
        {
            
             return loginUser.Login();
           
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public List<User> Put([FromBody] User user)
        {
            return user.UpdateUser();
        }
        [HttpPut("{email}")]
        public User Put(string email, [FromBody] User user)
        {
            return user.UpdateUser(email);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
