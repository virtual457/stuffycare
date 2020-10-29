using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using StuffyCare.DataLayer;
using StuffyCare.Facade;
using StuffyCare.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuffyCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        // GET: api/<AdminsController1>
        private readonly Connection con = new Connection();
        private readonly Admin _AdminFacade = new Admin();
        /// <summary>
        /// Simple api to check wheather the controller is working or not
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Api is working" };
        }
        /// <summary>
        /// This api is used to get all details of the registered user in the database
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>
        /// null---if there is no user with the email
        /// userobject---the user details of the user inside the object
        /// </returns>
        [HttpGet("GetUser")]
        public List<Users> GetUser(string userid)
        {
            List<Users> obj = new List<Users>();
            try
            {
                obj = _AdminFacade.GetUser(userid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
            }
            return obj;
        }
        /// <summary>
        /// This api gets all the appointments in that particular category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>
        /// return an list of appointments of the category mentioned 
        /// if category==all
        /// all appointments are returned
        /// </returns>
        [HttpGet("GetAppointments")]
        public List<Appointments> GetAppointments(string category)
        {
            
            List<Appointments> obj = new List<Appointments>();
            try
            {
                obj = _AdminFacade.GetAppointments(category);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
            }

            return obj;
        }
        /// <summary>
        /// Api to get items by the item id if all is passed all items are returned
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        [HttpGet("GetItems")]
        public List<Items> GetItems(string itemid)
        {

            List<Items> obj = new List<Items>();
            try
            {
                obj = _AdminFacade.GetItems(itemid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
            }
            return obj;
        }
        /// <summary>
        /// Gets all orders if all is passed or orders of the vendor if vendor id is passed
        /// </summary>
        /// <param name="vendorid"></param>
        /// <returns></returns>
        [HttpGet("GetOrders")]
        public List<Orders> GetOrders(string vendorid)
        {

            List<Orders> obj = new List<Orders>();
            try
            {
                obj = _AdminFacade.GetOrders(vendorid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
            }
            return obj;
        }
        /// <summary>
        /// Used to add admin to database
        /// </summary>
        /// <param name="admins"></param>
        /// <returns>
        /// string update failed -- if admin is not suceesfully added
        /// string updated sucessfully--if admin is added sucessfully
        /// </returns>
        [HttpPost("AddAdmin")]
        public string AddAdmin([FromBody] Models.Admins admins)
        {
            
             var status = "Update failed";
             try
             {
             status = _AdminFacade.Create(admins.Email, con.Encrypt(admins.Pass), admins.Pno);
             }
             catch (Exception e)
             {
               status = e.Message;
             }
             return status;
            
        }
        /// <summary>
        /// Api to authencticate admin returns login sucessfull if details are correct
        /// </summary>
        /// <param name="admins"></param>
        /// <returns></returns>
        [HttpPost("AuthAdmin")]
        public string AuthAdmin([FromBody] Models.Admins admins)
        {
            var status = "Login failed";
            try
            {
                status = _AdminFacade.Auth(admins.Email, con.Encrypt(admins.Pass));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Login Failed";
            }
            return status;
        }
        /// <summary>
        /// Api to add appointments
        /// </summary>
        /// <param name="appointments"></param>
        /// <returns></returns>
        [HttpPost("AddAppointment")]
        public string AddAppointment([FromBody] Models.Appointments appointments)
        {
            var status = "Adding appointment failed";
            try
            {
                status = _AdminFacade.AddAppointments(appointments);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Adding appointment failed";
            }
            return status;
        }
        /// <summary>
        /// Api to add item to database returns a string signifying the status of the addition
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [HttpPost("AddItem")]
        public string AddItem([FromBody] Models.Items items)
        {
            var status = "Adding item failed";
            try
            {
                status = _AdminFacade.AddItem(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Adding items failed";
            }
            return status;
        }

       
    }
}
