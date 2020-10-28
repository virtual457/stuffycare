﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StuffyCare.DataLayer;
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
        private readonly Connection con = new Connection();
        // GET: api/<UsersController1>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var a = con.GenerateEncryptionKey();
            return new string[] { a };
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
                status = _UserFacade.Create(users.Email, con.Encrypt(users.Pass), users.Pno);
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
                status = _UserFacade.Auth(users.Email,con.Encrypt( users.Pass));
            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message);
                status = "Login Failed";
            }
            return status;
        }

       
    }
}
