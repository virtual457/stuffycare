using System;
using System.Collections;
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
        private readonly StuffyCareContext context = new StuffyCareContext();

        // GET: api/<UsersController1>
        /// <summary>
        /// TO check api is working or not
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var a = con.GenerateEncryptionKey();
            return new string[] { a };
        }
        /// <summary>
        /// Api to get orders placed by that user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        // GET api/<UsersController1>/5
        [HttpGet("GetOrders")]
        public List<Orders> GetOrders(string userid)
        {
            List<Orders> listobj = new List<Orders>();
            try
            {
                listobj = _UserFacade.GetOrders(userid);
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
                listobj = null;
            }
            return listobj;
        }
        /// <summary>
        /// To retreive the current details of the user
        /// </summary>
        /// <param name="emailid"></param>
        /// <returns></returns>
        [HttpGet("GetUser")]
        public Users GetUser(string emailid)
        {
            Users obj = new Users();
            try
            {
                obj = _UserFacade.GetUser(emailid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
            }
            return obj;
        }
        /// <summary>
        /// Api to get all the appointments placed by the User
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("GetAppointments")]
        public List<Appointments> GetAppointments(string userid)
        {
            List<Appointments> listobj = new List<Appointments>();
            try
            {
                listobj = _UserFacade.GetAppointments(userid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                listobj = null;
            }
            return listobj;
        }
        /// <summary>
        /// Post request to add user to the user table
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
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
        /// <summary>
        /// post request to authenticate user
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
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
