using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StuffyCare.DataLayer;
using StuffyCare.EFModels;
using StuffyCare.Facade;
using StuffyCare;
using AutoMapper;
using System.Net.Mail;
using StuffyCare.DataLayer.ShiprocketModels;
using System.Web.Http;
using System.Web.Http.Cors;
using StuffyCare.Filters;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuffyCare.Controllers
{
    [ExceptionFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        private readonly User _UserFacade = new User();
        private readonly Connection con = new Connection();
        private readonly StuffyCareContext context = new StuffyCareContext();
        private readonly IMapper _mapper;
        private readonly ShipRocket delivery = new ShipRocket();
        private readonly Email _mailer = new Email();

        private readonly OtpData _otp = new OtpData();
        public UsersController()
        {
                
        }
        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: api/<UsersController1>
        /// <summary>
        /// TO check api is working or not
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("api/User/Get")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var obj = new ShipRocketOrder();
            var a = delivery.PlaceOrder(obj);
            return new string[] { a };
        }
        /// <summary>
        /// Api to get orders placed by that user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns> a list of orders placed by the user</returns>
        // GET api/<UsersController1>/5
        [Route("api/User/GetOrders")]
        [HttpGet]
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
                        var iter = new Models.Orders
                        {
                            Orderid = order.Orderid,
                            Userid = order.Userid,
                            Itemid = order.Itemid,
                            Dt = order.Dt,
                            Quantity = order.Quantity,
                            Status = order.Status,
                            Method = order.Method,
                            Total = order.Total,
                            SrOrderid = order.SrOrderid,
                            SrShipmentid = order.SrShipmentid


                        };
                        listobj.Add(iter);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                listobj = null;
                throw e;
            }
            return listobj;
        }
        [Route("api/User/AddOrder")]
        [HttpPost]
        public string AddOrder([FromBody] Models.Orders order)
        {
            var status = "Adding order failed";
            try
            {
                EFModels.Orders petb = new EFModels.Orders()
                {
                    Orderid = order.Orderid,
                    Userid = order.Userid,
                    Itemid = order.Itemid,
                    Dt = order.Dt,
                    Quantity = order.Quantity,
                    Status = order.Status,
                    Method = order.Method,
                    Total = order.Total,
                    SrOrderid = order.SrOrderid,
                    SrShipmentid = order.SrShipmentid
                };

                status = _UserFacade.AddOrder(petb);
            }
            catch (Exception e)
            {
                status = e.Message;
                throw e;
            }
            return status;
        }
        /// <summary>
        /// To retreive the current details of the user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>The user details of that user</returns>
        [Route("api/User/GetUser")]
        [HttpGet]
        public Models.Users GetUser(string userid)
        {
            Users obj = new Users();
            Models.Users userObj = new Models.Users();
            try
            {

                var user = _UserFacade.GetUser(userid);
                userObj = new Models.Users
                {
                    Id = user.Id,
                    Userid = user.Userid,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Pass = user.Pass,
                    Pno = user.Pno,
                    Image = user.Image,
                    LoyaltyPoints = user.LoyaltyPoints


                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
                throw e;
            }
            return userObj;
        }


        /// <summary>
        /// Post request to add user to the user table
        /// </summary>
        /// <param name="users"></param>
        /// <returns>a string corresponding to the operation</returns>
        // POST api/<UsersController1>
        [Route("api/User/AddUser")]
        [HttpPost]
        public string AddUser([FromBody] Models.Users user)
        {
            var userid = "";
            var status = "adding user failed";
            var emailstatus = "Sending email Failed";
            try
            {
                var userObj = new EFModels.Users
                {
                    Id = user.Id,
                    Userid = user.Userid,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Pass = con.Encrypt(user.Pass),
                    Pno = user.Pno,
                    Image = user.Image,
                    LoyaltyPoints = user.LoyaltyPoints
                };
                if (user.Email == "" && user.Pno == "")
                {
                    return "How can both email and Pno be null??";
                }

                status = _UserFacade.Create(userObj);
                if (status == "User added sucessfully")
                {
                    if (user.Email == "")
                    {
                        userid = _UserFacade.GetUserId(user.Pno);
                        _otp.SendMsg(user.Pno, "Welcome To StuffyCare Here we offer Great deals for your pet products and services at the lowest price.");
                    }
                    else if (user.Pno == "")
                    {
                        userid = _UserFacade.GetUserId(user.Email);
                        emailstatus = _mailer.SendEmail(user.Email, "Welcome To StuffyCare | StuffyCare", "We Offer the best products and services for your pets");
                    }
                    else {
                        userid = _UserFacade.GetUserId(user.Email);
                        emailstatus = _mailer.SendEmail(user.Email, "Welcome To StuffyCare | StuffyCare", "We Offer the best products and services for your pets");
                        _otp.SendMsg(user.Pno, "Welcome To StuffyCare Here we offer Great deals for your pet products and services at the lowest price.");

                    }
                    status = status + " as " + userid;
                }
            }
            catch (Exception e)
            {
                status = e.Message;
                throw e;
            }
            return status;
        }
        [Route("api/User/UpdateUser")]
        [HttpPut]
        public string UpdateUser([FromBody] Models.Users user)
        {
            var str = string.Empty;
            try
            {
                var userObj = new Users
                {
                    Id = user.Id,
                    Userid = user.Userid,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Pass = con.Encrypt( user.Pass),
                    Pno = user.Pno,
                    Image = user.Image,
                    LoyaltyPoints = user.LoyaltyPoints
                };


                if (context.Users.Find(userObj.Userid) != null)
                {
                    str = _UserFacade.UpdateUser(userObj);
                }
                else
                {
                    return "userid doent exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        [Route("api/User/AuthUser")]
        [HttpPost]
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
                throw e;
            }
            return status;
        }
        /// <summary>
        /// Api to get all the appointments placed by the User
        /// </summary>
        /// <param name="userid" pet="petid"></param>
        /// <returns>a list of appointments placed by that user for that pet</returns>
        [Route("api/User/GetAppointments")]
        [HttpGet]
        public List<Models.Appointments> GetAppointments(string userid, string petid)
        {
            List<Models.Appointments> listobj = new List<Models.Appointments>();
            try
            {

                var repobj = _UserFacade.GetAppointments(userid, petid);
                if (repobj != null)
                {
                    foreach (var appointments in repobj)
                    {
                        var iter = new Models.Appointments
                        {
                            Id=appointments.Id,
                            Aptid = appointments.Aptid,
                            Userid = appointments.Userid,
                            Phonenumber = appointments.Phonenumber,
                            Vendorid=appointments.Vendorid,
                            Category=appointments.Category,
                            Petid=appointments.Petid,
                            Servicedatetime = appointments.Servicedatetime,
                            Servicefees = appointments.Servicefees,
                            Address = appointments.Address,
                            Message = appointments.Message,
                            Ishomeservice=appointments.Ishomeservice,
                            Ispaid=appointments.Ispaid
                        };

                        listobj.Add(iter);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                listobj = null;
                throw e;
            }
            return listobj;
        }
        /// <summary>
        /// To add an appointment
        /// </summary>
        /// <param name="appointments"></param>
        /// <returns></returns>
        [Route("api/User/AddAppointment")]
        [HttpPost]
        public string AddAppointment([FromBody] Models.Appointments appointments)
        {
            var status = "Adding appointment failed";
            var emailstatus = "could not send email update";
            try
            {
                var app = new EFModels.Appointments()
                {
                    Id = appointments.Id,
                    Aptid = appointments.Aptid,
                    Userid = appointments.Userid,
                    Phonenumber = appointments.Phonenumber,
                    Category = appointments.Category,
                    Vendorid = appointments.Vendorid,
                    Petid = appointments.Petid,
                    Servicedatetime = appointments.Servicedatetime,
                    Servicefees = appointments.Servicefees,
                    Address = appointments.Address,
                    Message = appointments.Message,
                    Ishomeservice = appointments.Ishomeservice,
                    Ispaid = appointments.Ispaid
                }
                ;

                status = _UserFacade.AddAppointments(app);
                if (status == "appointment created sucessfully")
                {
                    emailstatus =_mailer.SendEmail(this.GetUser(appointments.Userid).Email, "Appointment Status | StuffyCare", "The Appointments Requested was Sucessfully Booked");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Adding appointment failed";
                throw e;
            }
            return status;
        }
        /// <summary>
        /// To get the list of pets for that user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Route("api/User/GetPets")]
        [HttpGet]
        public List<Models.Pets> GetPets(string userid)
        {
            List<Models.Pets> listobj = new List<Models.Pets>();
            try
            {
                var repobj = _UserFacade.GetPets(userid);
                if (repobj != null)
                {
                    foreach (var pet in repobj)
                    {
                        var iter = new Models.Pets
                        {
                            Id = pet.Id,
                            Petid = pet.Petid,
                            Userid = pet.Userid,
                            Name = pet.Name,
                            Type=pet.Type,
                            Size=pet.Size,
                            Gender=pet.Gender,
                            Age = pet.Age,
                            Allergies=pet.Allergies,
                            Breed=pet.Breed,
                            Moreinfo = pet.Moreinfo
                        };
                        listobj.Add(iter);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                listobj = null;
                throw e;
            }
            return listobj;
        }
        /// <summary>
        /// To add a pet to its respective user
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        [Route("api/User/AddPet")]
        [HttpPost]
        public string AddPet([FromBody] Models.Pets pet)
        {
            var status = "Adding pet failed";
            try
            {
                EFModels.Pets petb = new EFModels.Pets()
                {
                    Id = pet.Id,
                    Petid = pet.Petid,
                    Userid = pet.Userid,
                    Name = pet.Name,
                    Type = pet.Type,
                    Size = pet.Size,
                    Gender = pet.Gender,
                    Age = pet.Age,
                    Allergies = pet.Allergies,
                    Breed = pet.Breed,
                    Moreinfo=pet.Moreinfo
                    
                };
                
                status = _UserFacade.AddPet(petb);
            }
            catch (Exception e)
            {
                status = e.Message;
                throw e;
            }
            return status;
        }
        /// <summary>
        ///To remove the pet from user
        /// </summary>
        /// <param name="petid"></param>
        /// <returns></returns>

        [Route("api/User/DeletePet")]
        [HttpDelete]
        public string DeletePet(string petid)
        {
            var str = string.Empty;
            try
            {
                var petobj = new Pets();
                petobj.Petid = petid;
                if (context.Pets.Find(petobj.Petid) != null)
                {
                    str = _UserFacade.DeletePet(petobj);
                }
                else
                {
                    return "Petid doent exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        [Route("api/User/UpdatePet")]
        [HttpPut]
        public string UpdatePet([FromBody] Models.Pets pet)
        {
            var str = string.Empty;
            try
            {
                var petobj = new Pets();
                petobj.Petid = pet.Petid;
                petobj.Userid = pet.Userid;
                petobj.Size = pet.Size;
                petobj.Moreinfo = pet.Moreinfo;
                petobj.Name = pet.Name;
                petobj.Type = pet.Type;
                petobj.Gender = pet.Gender;
                petobj.Breed = pet.Breed;
                petobj.Age = pet.Age;
                petobj.Allergies = pet.Allergies;
                

                if (context.Pets.Find(petobj.Petid) != null)
                {
                    str = _UserFacade.Updatepet(petobj);
                }
                else
                {
                    return "Petid doent exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        /// <summary>
        /// To get cart details fo the user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Route("api/User/GetCart")]
        [HttpGet]

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
                        var iter = new Models.Cart
                        {
                            Id = cart.Id,
                            Userid = cart.Userid,
                            Itemid = cart.Itemid,
                            Quantity = cart.Quantity
                        };
                        listobj.Add(iter);
                    }
                }
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        /// <summary>
        /// To add an item to cart of that user
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [Route("api/User/AddCart")]
        [HttpPost]
        public string AddCart([FromBody] Models.Cart cart)
        {
            var status = "Update failed";
            try
            {
                EFModels.Cart cartb = new EFModels.Cart()
                {
                    Itemid = cart.Itemid,
                    Userid = cart.Userid,
                    Quantity = cart.Quantity,
                };
                
                status = _UserFacade.AddCart(cartb);
            }
            catch (Exception e)
            {
                status = e.Message;
                throw e;
            }
            return status;
        }
        /// <summary>
        /// To Delete item from cart of that user
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        [Route("api/User/DeleteCart")]
        [HttpDelete]
        public string DeleteCart([FromBody] Models.Cart cart)
        {
            var str = string.Empty;
            try
            {
                EFModels.Cart cartb = new EFModels.Cart()
                {
                    Itemid = cart.Itemid,
                    Userid = cart.Userid,
                    Quantity = cart.Quantity,
                };


                str = _UserFacade.DelCart(cartb);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        [Route("api/User/UpdateCart")]
        [HttpPut]
        public string UpdateCart([FromBody] Models.Cart cart)
        {
            var str = "Update cart failed in Controller method";
            try
            {
                EFModels.Cart cartb = new EFModels.Cart()
                {
                    Itemid = cart.Itemid,
                    Userid = cart.Userid,
                    Quantity = cart.Quantity,
                };


                str = _UserFacade.UpdateCart(cartb);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        /// <summary>
        /// To get the wishlist of items of the user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// 
        [Route("api/User/GetWishlist")]
        [HttpGet]
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
                        Models.Wishlist iter = new Models.Wishlist()
                        {
                            Id = wishlist.Id,
                            Userid = wishlist.Userid,
                            Itemid = wishlist.Itemid
                        };
                        listobj.Add(iter);
                    }
                }
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        /// <summary>
        /// To add an item to the wishlist of the user
        /// </summary>
        /// <param name="wishlist"></param>
        /// <returns></returns>
        /// 
        [Route("api/User/AddWishlist")]
        [HttpPost]
        public string AddWishlist([FromBody] Models.Wishlist wishlist)
        {
            var status = "Update failed";
            try
            {
                EFModels.Wishlist wishlistb = new EFModels.Wishlist()
                {
                    Itemid = wishlist.Itemid,
                    Userid = wishlist.Userid,
                };
               // EFModels.Wishlist wishlista = _mapper.Map<EFModels.Wishlist>(wishlistb);
                status = _UserFacade.AddWishlist(wishlistb);
            }
            catch (Exception e)
            {
                status = e.Message;
                throw e;
            }
            return status;
        }
        /// <summary>
        /// To delete an item from the wishlist of an users
        /// </summary>
        /// <param name="wishlist"></param>
        /// <returns></returns>
        /// 
        [Route("api/User/DeleteWishlist")]
        [HttpDelete]
        public string DeleteWishlist([FromBody] Models.Wishlist wishlist)
        {
            var str = string.Empty;
            try
            {
                EFModels.Wishlist wishlistb = new EFModels.Wishlist()
                {
                    Itemid = wishlist.Itemid,
                    Userid = wishlist.Userid,
                };
                str = _UserFacade.DelWishlist(wishlistb);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        [Route("api/User/UpdatePassword")]
        [HttpPut]
        public string UpdatePassword(string emailorphone, string password)
        {
            var status = "Update failed";
            try
            {

                // EFModels.Wishlist wishlista = _mapper.Map<EFModels.Wishlist>(wishlistb);
                status = _UserFacade.UpdatePass(emailorphone, con.Encrypt(password));
            }
            catch (Exception e)
            {
                status = e.Message;
                throw e;
            }
            return status;
        }
        [Route("api/User/createOrderRazorpay")]
        [HttpPost]
        public IHttpActionResult createOrderRazorpay(Models.orderRazorpay order)
        {
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_9wK0kOoAWrR2GV", "Um9kYEyRcQy8TRKGAUwGFLny");
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", order.amount);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", order.currency);
            // options.Add("payment_capture", "0"); // 1 - automatic  , 2 - manual
            //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string retorderId = orderResponse["id"].ToString();
            Models.orderRazorpay pay = new Models.orderRazorpay
            {
                amount = order.amount,
                currency = order.currency,
                name = order.name,
                email = order.email,
                mobileNumber = order.mobileNumber,
                address = order.address,
                orderId=retorderId

            };
            return Ok(pay);
        }
        [Route("api/User/verifyOrderRazorpay")]
        [HttpPost]
        public IHttpActionResult verifyOrderRazorpay(string paymentId, string orderId)
        {
            // Payment data comes in url so we have to get it from url

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string _paymentId = paymentId;

            // This is orderId
            string _orderId = orderId;

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_9wK0kOoAWrR2GV", "Um9kYEyRcQy8TRKGAUwGFLny");

            Razorpay.Api.Payment payment = client.Payment.Fetch(_paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                // Create these action method
                return Ok("Sucessfull");

            }
            else
            {
                return Ok("ERROR occured");
            }
        }
        /// <summary>
        /// To get the list of addresss for that user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Route("api/User/GetAddresss")]
        [HttpGet]
        public List<Models.Address> GetAddresss(string userid)
        {
            List<Models.Address> listobj = new List<Models.Address>();
            try
            {
                var repobj = _UserFacade.GetAddress(userid);
                if (repobj != null)
                {
                    foreach (var address in repobj)
                    {
                        var iter = new Models.Address
                        {
                            Id = address.Id,
                            Addressid = address.Addressid,
                            Userid = address.Userid,
                            Firstname = address.Firstname,
                       Lastname = address.Lastname,
                        Addresslineone = address.Addresslineone,
                       Addresslinetwo = address.Addresslinetwo,
                        Landmark = address.Landmark,
                        City = address.City,
                        Pincode = address.Pincode,
                        State = address.State,
                        Country = address.Country,
                        Email = address.Email,
                        Phone = address.Phone,
                        Isshippingaddress = address.Isshippingaddress,
                    };
                        listobj.Add(iter);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                listobj = null;
                throw e;
            }
            return listobj;
        }
        /// <summary>
        /// To add a address to its respective user
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [Route("api/User/AddAddress")]
        [HttpPost]
        public string AddAddress([FromBody] Models.Address address)
        {
            var status = "Adding address failed";
            try
            {
                EFModels.Address addressb = new EFModels.Address()
                {
                    Id = address.Id,
                    Addressid = address.Addressid,
                    Userid = address.Userid,
                    Firstname = address.Firstname,
                    Lastname = address.Lastname,
                    Addresslineone = address.Addresslineone,
                    Addresslinetwo = address.Addresslinetwo,
                    Landmark = address.Landmark,
                    City = address.City,
                    Pincode = address.Pincode,
                    State = address.State,
                    Country = address.Country,
                    Email = address.Email,
                    Phone = address.Phone,
                    Isshippingaddress = address.Isshippingaddress,

                };

                status = _UserFacade.AddAddress(addressb);
            }
            catch (Exception e)
            {
                status = e.Message;
                throw e;
            }
            return status;
        }
        /// <summary>
        ///To remove the address from user
        /// </summary>
        /// <param name="addressid"></param>
        /// <returns></returns>

        [Route("api/User/DeleteAddress")]
        [HttpDelete]
        public string DeleteAddress(string addressid)
        {
            var str = string.Empty;
            try
            {
                var addressobj = new Address();
                addressobj.Addressid = addressid;
                if (context.Address.Find(addressobj.Addressid) != null)
                {
                    str = _UserFacade.DeleteAddress(addressobj);
                }
                else
                {
                    return "Addressid doent exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        [Route("api/User/UpdateAddress")]
        [HttpPut]
        public string UpdateAddress([FromBody] Models.Address address)
        {
            var str = string.Empty;
            try
            {
                var addressobj = new Address
                {
                    Id = address.Id,
                    Addressid = address.Addressid,
                    Userid = address.Userid,
                    Firstname = address.Firstname,
                    Lastname = address.Lastname,
                    Addresslineone = address.Addresslineone,
                    Addresslinetwo = address.Addresslinetwo,
                    Landmark = address.Landmark,
                    City = address.City,
                    Pincode = address.Pincode,
                    State = address.State,
                    Country = address.Country,
                    Email = address.Email,
                    Phone = address.Phone,
                    Isshippingaddress = address.Isshippingaddress
                };

                if (context.Address.Find(addressobj.Addressid) != null)
                {
                    str = _UserFacade.Updateaddress(addressobj);
                }
                else
                {
                    return "Addressid doent exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
    }
}
