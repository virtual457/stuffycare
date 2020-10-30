using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StuffyCare.DataLayer;
using StuffyCare.EFModels;
using StuffyCare.Facade;
using StuffyCare;
using AutoMapper;



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
        private readonly IMapper _mapper;
        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

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
        public List<Models.Orders> GetOrders(string userid)
        {
            List<Models.Orders> listobj = new List<Models.Orders>();
            try
            {
                var repobj = _UserFacade.GetOrders(userid);
                if (repobj != null)
                {
                    foreach (var order in repobj)
                    {
                        Models.Orders userObj = _mapper.Map<Models.Orders>(order);
                        listobj.Add(userObj);
                    }
                }
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
        public Models.Users GetUser(string emailid)
        {
            Users obj = new Users();
            Models.Users userObj = new Models.Users();
            try
            {
                
                var repobj = _UserFacade.GetUser(emailid);
                userObj = _mapper.Map<Models.Users>(repobj);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
            }
            return userObj;
        }
        /// <summary>
        /// Api to get all the appointments placed by the User
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("GetAppointments")]
        public List<Models.Appointments> GetAppointments(string userid,string petid)
        {
            List<Models.Appointments> listobj = new List<Models.Appointments>();
            try
            {
                
                var repobj = _UserFacade.GetAppointments(userid,petid);
                if (repobj != null)
                {
                    foreach (var Appointment in repobj)
                    {
                        Models.Appointments Obj = _mapper.Map<Models.Appointments>(Appointment);
                        listobj.Add(Obj);
                    }
                }
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
                if (users.Email == "")
                {
                    users.Email = users.Pno;
                }
                status = _UserFacade.Auth(users.Email,con.Encrypt( users.Pass));
            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message);
                status = "Login Failed";
            }
            return status;
        }
        [HttpPost("AddAppointment")]
        public string AddAppointment([FromBody] Models.Appointments appointments)
        {
            var status = "Adding appointment failed";
            try
            {
                var app = new EFModels.Appointments()
                {
                    Aptid = appointments.Aptid,
                    Servicetype = appointments.Servicetype,
                    Dt = appointments.Dt,
                    Address = appointments.Address,
                    Id = appointments.Id,
                    Message = appointments.Message,
                    Pno = appointments.Pno,
                    Userid = appointments.Userid
                }
                ;

                status = _UserFacade.AddAppointments(app);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Adding appointment failed";
            }
            return status;
        }

    }
}
