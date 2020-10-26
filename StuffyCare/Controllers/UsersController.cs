using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StuffyCare.Facade;
using StuffyCare.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuffyCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly User _UserFacade = new User();
        // GET: api/<UsersController1>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Api is working" };
        }

        // GET api/<UsersController1>/5
        [HttpGet("GetUser")]
        public Users GetUser(string email)
        {
            Users obj = new Users();
            try
            {
                obj = _UserFacade.Get(email);
                Console.WriteLine("Chandan is Great");
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
                obj = null;
            }
            return obj;
        }

        // POST api/<UsersController1>
        [HttpPost("AddUser")]
        public string AddUser([FromBody] Models.Users users)
        {
            var status = "Update failed";
            try
            {
                status = _UserFacade.Create(users.Email, users.Pass, users.Pno);
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return status;
        }
        [HttpPost("AuthUser")]
        public string AuthUser([FromBody] Models.Users users)
        {
            var status = "Login failed";
            try
            {
                status = _UserFacade.Auth(users.Email, users.Pass);
            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message);
                status = "Login Failed";
            }
            return status;
        }

        // PUT api/<UsersController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
