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
        /// <returns> a list of orders placed by the user</returns>
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
        /// <returns>The user details of that user</returns>
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
        /// Post request to add user to the user table
        /// </summary>
        /// <param name="users"></param>
        /// <returns>a string corresponding to the operation</returns>
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
                if (users.Email == "")
                {
                    users.Email = users.Pno;
                }
                status = _UserFacade.Auth(users.Email, con.Encrypt(users.Pass));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Login Failed";
            }
            return status;
        }
        /// <summary>
        /// Api to get all the appointments placed by the User
        /// </summary>
        /// <param name="userid" pet="petid"></param>
        /// <returns>a list of appointments placed by that user for that pet</returns>
        [HttpGet("GetAppointments")]
        public List<Models.Appointments> GetAppointments(string userid, string petid)
        {
            List<Models.Appointments> listobj = new List<Models.Appointments>();
            try
            {

                var repobj = _UserFacade.GetAppointments(userid, petid);
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
        /// To add an appointment
        /// </summary>
        /// <param name="appointments"></param>
        /// <returns></returns>
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
        /// <summary>
        /// To get the list of pets for that user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("GetPets")]
        public List<Models.Pets> GetPets(string userid)
        {
            List<Models.Pets> listobj = new List<Models.Pets>();
            try
            {
                var repobj = _UserFacade.GetPets(userid);
                if (repobj != null)
                {
                    foreach (var order in repobj)
                    {
                        Models.Pets userObj = _mapper.Map<Models.Pets>(order);
                        listobj.Add(userObj);
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
        /// To add a pet to its respective user
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        [HttpPost("AddPet")]
        public string AddPet([FromBody] Models.Pets pet)
        {
            var status = "Update failed";
            try
            {
                Models.Pets petb = new Models.Pets()
                {
                    Userid = pet.Userid,
                    Dob = pet.Dob,
                    Name = pet.Name
                };
                EFModels.Pets peta = _mapper.Map<EFModels.Pets>(petb);
                status = _UserFacade.AddPet(peta);
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return status;
        }
        /// <summary>
        ///To remove the pet from user
        /// </summary>
        /// <param name="petid"></param>
        /// <returns></returns>
        
        
        [HttpDelete("DeletePet")]
        public string DeletePet(string petid)
        {
            var str = string.Empty;
            try
            {
                var petobj = new Pets();
                petobj.Petid = petid;
                str = _UserFacade.DeletePet(petobj);
            }
            catch (Exception e)
            {
                str = e.Message;
               
            }
            return str;
        }
        /// <summary>
        /// To get cart details fo the user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("GetCart")]
        public List<Models.Cart> GetCart(string userid)
        {
            List<Models.Cart> listobj = new List<Models.Cart>();
            try
            {
                
                var repobj = _UserFacade.GetCart(userid);
          
                if (repobj != null)
                {
                    foreach (var cart in repobj)
                    {
                        Models.Cart Obj = _mapper.Map<Models.Cart>(cart);
                        listobj.Add(Obj);
                    }
                }
            }
            catch (Exception e)
            {
                listobj = null;
            }
            return listobj;
        }
        /// <summary>
        /// To add an item to cart of that user
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpPost("AddCart")]
        public string AddCart([FromBody] Models.Cart cart)
        {
            var status = "Update failed";
            try
            {
                Models.Cart cartb = new Models.Cart()
                {
                    Itemid=cart.Itemid,
                    Userid=cart.Userid,
                    Quantity=cart.Quantity,
                };
                EFModels.Cart carta = _mapper.Map<EFModels.Cart>(cartb);
                status = _UserFacade.AddCart(carta);
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return status;
        }
        /// <summary>
        /// To Delete item from cart of that user
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCart")]
        public string DeleteCart([FromBody] Models.Cart cart)
        {
            var str = string.Empty;
            try
            {
                var cartobj = _mapper.Map<EFModels.Cart>(cart);
                
                str = _UserFacade.DelCart(cartobj);
            }
            catch (Exception e)
            {
                str = e.Message;

            }
            return str;
        }
        /// <summary>
        /// To get the wishlist of items of the user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("GetWishlist")]
        public List<Models.Wishlist> GetWishlist(string userid)
        {
            List<Models.Wishlist> listobj = new List<Models.Wishlist>();
            try
            {

                var repobj = _UserFacade.GetWishlist(userid);

                if (repobj != null)
                {
                    foreach (var wishlist in repobj)
                    {
                        Models.Wishlist Obj = _mapper.Map<Models.Wishlist>(wishlist);
                        listobj.Add(Obj);
                    }
                }
            }
            catch (Exception e)
            {
                listobj = null;
            }
            return listobj;
        }
        /// <summary>
        /// To add an item to the wishlist of the user
        /// </summary>
        /// <param name="wishlist"></param>
        /// <returns></returns>
        [HttpPost("AddWishlist")]
        public string AddWishlist([FromBody] Models.Wishlist wishlist)
        {
            var status = "Update failed";
            try
            {
                Models.Wishlist wishlistb = new Models.Wishlist()
                {
                    Itemid = wishlist.Itemid,
                    Userid = wishlist.Userid,
                };
                EFModels.Wishlist wishlista = _mapper.Map<EFModels.Wishlist>(wishlistb);
                status = _UserFacade.AddWishlist(wishlista);
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return status;
        }
        /// <summary>
        /// To delete an item from the wishlist of an users
        /// </summary>
        /// <param name="wishlist"></param>
        /// <returns></returns>
        [HttpDelete("DeleteWishlist")]
        public string DeleteWishlist([FromBody] Models.Wishlist wishlist)
        {
            var str = string.Empty;
            try
            {
                var wishlistobj = _mapper.Map<EFModels.Wishlist>(wishlist);

                str = _UserFacade.DelWishlist(wishlistobj);
            }
            catch (Exception e)
            {
                str = e.Message;

            }
            return str;
        }
    }
}
