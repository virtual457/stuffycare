using AutoMapper;
using StuffyCare.DataLayer;
using StuffyCare.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using StuffyCare.Filters;

namespace StuffyCare.Controllers
{
    [ExceptionFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminController : ApiController
    {
        private readonly Connection con = new Connection();
        private readonly Admin _AdminFacade = new Admin();
        private readonly IMapper _mapper;
        private readonly OtpData _otp = new OtpData();
        private readonly User _UserFacade = new User();
        private readonly Vendor _VendorFacade = new Vendor();
        public AdminController()
        {

        }
        public AdminController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [Route("api/admin/GetUser")]
        [HttpGet]
        public List<Models.Users> GetsUser(string userid)
        {
            List<Models.Users> obj = new List<Models.Users>();
            try
            {
                var repobj = _AdminFacade.GetUser(userid);
                if (repobj != null)
                {
                    var iter = new Models.Users();
                    foreach (var user in repobj)
                    {
                        iter = new Models.Users
                        {
                            Id = user.Id,
                            Userid = user.Userid,
                            Email = user.Email,
                            Pass = null,
                            Pno = user.Pno,
                            Firstname= user.Firstname,
                            Lastname=user.Lastname,
                            LoyaltyPoints=user.LoyaltyPoints
                        };
                        obj.Add(iter);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
                throw e;
            }
            return obj;
        }
        [Route("api/admin/GetVendor")]
        [HttpGet]
        public List<Models.Vendors> GetVendor(string vendorid,string category,string city)
        {
            List<Models.Vendors> obj = new List<Models.Vendors>();
            try
            {
                var repobj = _AdminFacade.GetVendor(vendorid,category,city);
                if (repobj != null)
                {
                    var iter = new Models.Vendors();
                    foreach (var vendors in repobj)
                    {
                        iter = new Models.Vendors
                        {
                            Vendorid=vendors.Vendorid,
                            Name = vendors.Name,
                            Description = vendors.Description,
                            Email = vendors.Email,
                            Pass = con.Encrypt(vendors.Pass),
                            Pno = vendors.Pno,
                            Gender = vendors.Gender,
                            StoreName = vendors.StoreName,
                            City = vendors.City,
                            Location = vendors.Location,
                            Yearsofexperience = vendors.Yearsofexperience,
                            Monfrom = vendors.Monfrom,
                            Monto = vendors.Monto,
                            Tuefrom = vendors.Tuefrom,
                            Tueto = vendors.Tueto,
                            Wedfrom = vendors.Wedfrom,
                            Wedto = vendors.Wedto,
                            Thurfrom = vendors.Thurfrom,
                            Thurto = vendors.Thurto,
                            Frifrom = vendors.Frifrom,
                            Frito = vendors.Frito,
                            Satfrom = vendors.Satfrom,
                            Satto = vendors.Satto,
                            Sunfrom = vendors.Sunfrom,
                            Sunto = vendors.Sunto,
                            Photo = vendors.Photo,
                            Issellingitem = vendors.Issellingitem,
                            Homeservice = vendors.Homeservice,
                            Photoidproof = vendors.Photoidproof,
                            Isauthorized = vendors.Isauthorized,
                            Isdeleted = vendors.Isdeleted
                        };
                        obj.Add(iter);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                obj = null;
                throw e;
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
        [Route("api/Admin/GetAppointments")]
        [HttpGet]
        public IHttpActionResult GetAppointments(string category)
        {

            List<Models.Appointments> obj = new List<Models.Appointments>();
            try
            {
                var repobj = _AdminFacade.GetAppointments(category);
                if (repobj != null)
                {
                    foreach (var appointments in repobj)
                    {
                        var iter = new Models.Appointments
                        {
                            Id = appointments.Id,
                            Aptid = appointments.Aptid,
                            Userid = appointments.Userid,
                            Phonenumber = appointments.Phonenumber,
                            Vendorid = appointments.Vendorid,
                            Petid = appointments.Petid,
                            Servicedatetime = appointments.Servicedatetime,
                            Servicefees = appointments.Servicefees,
                            Address = appointments.Address,
                            Message = appointments.Message,
                            Ishomeservice = appointments.Ishomeservice,
                            Ispaid = appointments.Ispaid
                        };
                        obj.Add(iter);
                    }
                }

            }
            catch (Exception e)
            {
                obj = null;
                throw e;

            }

            return Ok(obj);
        }
        /// <summary>
        /// Api to get items by the item id if all is passed all items are returned
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        [Route("api/Admin/GetItems")]
        [HttpGet]
        public List<Models.Items> GetItems(string itemid,string foranimal,string category,string subcategory,string name)
        {

            List<Models.Items> retobj = new List<Models.Items>();
            try
            {

                var repobj = _AdminFacade.GetItems(itemid,foranimal,category,subcategory,name);
                if (repobj != null)
                {
                    foreach (var item in repobj)
                    {
                        var iter = new Models.Items
                        {
                            Itemid = item.Itemid,
                            Name = item.Name,
                            Description = item.Description,
                            Subdescription = item.Subdescription,
                            Foranimal = item.Foranimal,

                            Category = item.Category,
                            Subcategory = item.Subcategory,

                            Price = item.Price,
                            Saleprice = item.Saleprice,
                            Sku = item.Sku,

                            Quantity = item.Quantity,
                            Moa = item.Moa,
                            Addedby = item.Addedby,
                            Photo = item.Photo,
                            Length = item.Length,
                            Breadth = item.Breadth,
                            Height = item.Height,
                            Weight = item.Weight,
                            Shippingclass = item.Shippingclass,
                            Processingtime = item.Processingtime,
                            Mililitres = item.Mililitres,
                            Packsizeingrams = item.Packsizeingrams,
                            Unitcount = item.Unitcount,
                            Upsells = item.Upsells,
                            Crosssells = item.Crosssells,
                            Policylabel = item.Policylabel,
                            Shippingpolicy = item.Shippingpolicy,
                            Refundpolicy = item.Refundpolicy,
                            Cancelationpolicy = item.Cancelationpolicy,
                            Exchangepolicy = item.Exchangepolicy,
                            Storename = item.Storename,
                            Commissionfor = item.Commissionfor,
                            Commissionmode = item.Commissionmode,
                            Authorizedby = item.Authorizedby,
                            Authorizedstatus = item.Authorizedstatus,
                            Deletedstatus = item.Deletedstatus

                        };
                        retobj.Add(iter);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                retobj = null;
                throw e;
            }
            return retobj;
        }
        /// <summary>
        /// Gets all orders if all is passed or orders of the vendor if vendor id is passed
        /// </summary>
        /// <param name = "vendorid" ></ param >
        /// < returns ></ returns >
        [Route("api/Admin/GetOrders")]
        [HttpGet]
        public List<Models.Orders> GetOrders(string vendorid)
        {

            List<Models.Orders> retobj = new List<Models.Orders>();
            try
            {

                var repobj = _AdminFacade.GetOrders(vendorid);
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
                        retobj.Add(iter);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                retobj = null;
                throw e;
            }
            return retobj;
        }
        /// <summary>
        /// Used to add admin to database
        /// </summary>
        /// <param name="admins"></param>
        /// <returns>
        /// string update failed -- if admin is not suceesfully added
        /// string updated sucessfully--if admin is added sucessfull
        /// </returns>
        
        [Route("api/Admin/AddAdmin")]
        [HttpPost]
        public string AddAdmin([FromBody] Models.Admins admins)
        {

            var status = "Adding admin Failed";
            try
            { 
                status = _AdminFacade.Create(admins.Email, con.Encrypt(admins.Pass), admins.Pno);
                
            }
            catch (Exception e)
            {
                
                status = e.Message;
                throw e;
            }
            return status;

        }
        /// <summary>
        /// Api to authencticate admin returns login sucessfull if details are correct
        /// </summary>
        /// <param name="admins"></param>
        /// <returns></returns>
        [Route("api/Admin/AuthAdmin")]
        [HttpPost]
        public string AuthAdmin([FromBody] Models.Admins admins)
        {
            var status = "Login failed";
            try
            {
                if (admins.Email == "")
                {
                    admins.Email = admins.Pno;
                }
                status = _AdminFacade.Auth(admins.Email, con.Encrypt(admins.Pass));
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
        /// Api to add appointments
        /// </summary>
        /// <param name="appointments"></param>
        /// <returns></returns>
        [Route("api/Admin/AddAppointment")]
        [HttpPost]
        public string AddAppointment([FromBody] Models.Appointments appointments)
        {
            var status = "Adding appointment failed";
            try
            {
                var app = new EFModels.Appointments()
                {
                    Id = appointments.Id,
                    Aptid = appointments.Aptid,
                    Userid = appointments.Userid,
                    Phonenumber = appointments.Phonenumber,
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

                status = _AdminFacade.AddAppointments(app);
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
        /// Api to add item to database returns a string signifying the status of the addition
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [Route("api/Admin/AddItem")]
        [HttpPost]
        public string AddItem([FromBody] Models.Items item)
        {
            var status = "Adding item failed";
            try
            {

                var iter = new EFModels.Items
                {
                    Itemid = item.Itemid,
                    Name = item.Name,
                    Description = item.Description,
                    Subdescription=item.Subdescription,
                    Foranimal=item.Foranimal,

                    Category = item.Category,
                    Subcategory=item.Subcategory,

                    Price = item.Price,
                    Saleprice = item.Saleprice,
                    Sku = item.Sku,

                    Quantity = item.Quantity,
                    Moa = item.Moa,
                    Addedby = item.Addedby,
                    Photo = item.Photo,
                    Length=item.Length,
                    Breadth=item.Breadth,
                    Height=item.Height,
                    Weight=item.Weight,
                    Shippingclass=item.Shippingclass,
                    Processingtime=item.Processingtime,
                    Mililitres=item.Mililitres,
                    Packsizeingrams=item.Packsizeingrams,
                    Unitcount=item.Unitcount,
                    Upsells=item.Upsells,
                    Crosssells=item.Crosssells,
                    Policylabel=item.Policylabel,
                    Shippingpolicy=item.Shippingpolicy,
                    Refundpolicy=item.Refundpolicy,
                    Cancelationpolicy=item.Cancelationpolicy,
                    Exchangepolicy=item.Exchangepolicy,
                    Storename=item.Storename,
                    Commissionfor=item.Commissionfor,
                    Commissionmode=item.Commissionmode,
                    Authorizedby=item.Authorizedby,
                    Authorizedstatus=item.Authorizedstatus,
                    Deletedstatus=item.Deletedstatus
                    



                };


                status = _AdminFacade.AddItem(iter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Adding items failed";
                throw e;
            }
            return status;
        }
        [Route("api/Admin/AuthorizeVendor")]
        [HttpGet]
        public string AuthorizeVendor(string vendorid)
        {
            var status = "Authorising vendor failed";
            try
            {
                status = _AdminFacade.AuthVendor(vendorid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Authorising vendor failed";
                throw e;
            }
            return status;
        }
        [Route("api/Admin/GetAllVendorIdRequests")]
        [HttpGet]
        public List<Models.Vendors> GetAllVendorIdRequests()
        {
            var returnlist = new List<Models.Vendors>();
            try
            {
                var replist = _AdminFacade.GetAllVendorsIdRequests();
                foreach (var vendors in replist)
                {
                    var iter = new Models.Vendors
                    {
                        Vendorid=vendors.Vendorid,
                        Name = vendors.Name,
                        Description = vendors.Description,
                        Email = vendors.Email,
                        Pass = con.Encrypt(vendors.Pass),
                        Pno = vendors.Pno,
                        Gender = vendors.Gender,
                        StoreName = vendors.StoreName,
                        City = vendors.City,
                        Location = vendors.Location,
                        Yearsofexperience = vendors.Yearsofexperience,
                        Monfrom = vendors.Monfrom,
                        Monto = vendors.Monto,
                        Tuefrom = vendors.Tuefrom,
                        Tueto = vendors.Tueto,
                        Wedfrom = vendors.Wedfrom,
                        Wedto = vendors.Wedto,
                        Thurfrom = vendors.Thurfrom,
                        Thurto = vendors.Thurto,
                        Frifrom = vendors.Frifrom,
                        Frito = vendors.Frito,
                        Satfrom = vendors.Satfrom,
                        Satto = vendors.Satto,
                        Sunfrom = vendors.Sunfrom,
                        Sunto = vendors.Sunto,
                        Photo = vendors.Photo,
                        Issellingitem = vendors.Issellingitem,
                        Homeservice = vendors.Homeservice,
                        Photoidproof = vendors.Photoidproof,
                        Isauthorized = vendors.Isauthorized,
                        Isdeleted = vendors.Isdeleted
                    };
                    returnlist.Add(iter);
                }
            }
            catch (Exception e)
            {
                returnlist = null;
                throw e;
            }
            return returnlist;
        }
        [Route("api/Admin/GetAllVendorItemsByVendorId")]
        [HttpGet]
        public List<Models.Items> GetAllVendorItemsByVendorId(string vendorid)
        {
            var returnlist = new List<Models.Items>();
            try
            {
                var replist = _AdminFacade.GetVendoritemsByVendorId(vendorid);
                foreach (var item in replist)
                {
                    var iter = new Models.Items
                    {
                        Itemid = item.Itemid,
                        Name = item.Name,
                        Description = item.Description,
                        Subdescription = item.Subdescription,
                        Foranimal = item.Foranimal,

                        Category = item.Category,
                        Subcategory = item.Subcategory,

                        Price = item.Price,
                        Saleprice = item.Saleprice,
                        Sku = item.Sku,

                        Quantity = item.Quantity,
                        Moa = item.Moa,
                        Addedby = item.Addedby,
                        Photo = item.Photo,
                        Length = item.Length,
                        Breadth = item.Breadth,
                        Height = item.Height,
                        Weight = item.Weight,
                        Shippingclass = item.Shippingclass,
                        Processingtime = item.Processingtime,
                        Mililitres = item.Mililitres,
                        Packsizeingrams = item.Packsizeingrams,
                        Unitcount = item.Unitcount,
                        Upsells = item.Upsells,
                        Crosssells = item.Crosssells,
                        Policylabel = item.Policylabel,
                        Shippingpolicy = item.Shippingpolicy,
                        Refundpolicy = item.Refundpolicy,
                        Cancelationpolicy = item.Cancelationpolicy,
                        Exchangepolicy = item.Exchangepolicy,
                        Storename = item.Storename,
                        Commissionfor = item.Commissionfor,
                        Commissionmode = item.Commissionmode,
                        Authorizedby = item.Authorizedby,
                        Authorizedstatus = item.Authorizedstatus,
                        Deletedstatus = item.Deletedstatus

                    };
                    returnlist.Add(iter);
                }
            }
            catch (Exception e)
            {
                returnlist = null;
                throw e;
            }
            return returnlist;
        }
        [Route("api/Admin/AuthVendorItem")]
        [HttpGet]
        public string AuthVendorItem(string itemid,string adminid)
        {
            var status = "Authorising vendor item failed";
            try
            {
                status = _AdminFacade.AuthVendorItem(itemid,adminid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Authorising vendor item failed";
                throw e;
            }
            return status;
        }
        [Route("api/Admin/GetRecentOrders")]
        [HttpGet]
        public List<Models.Orders> GetRecentOrders(int num)
        {
            var retobj = new List<Models.Orders>();
            try
            {
                var repobj = _AdminFacade.GetRecentOrders(num);
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
                        retobj.Add(iter);
                    }
                }

            }
            catch (Exception e)
            {
                retobj = null;
                throw e;
            }
            return retobj;
        }
        
        [Route("api/Admin/SendOtpToPhone")]
        [HttpGet]
        public string SendOtpToPhone(string phoneno)
        {
            var status = "Otp could not be sent";
            try
            {
                var otp=_otp.GetOtp();
                var encyptedOtp = con.Encrypt(otp);
                status = _otp.SendMsg(phoneno, "Your OTP for stuffycare is "+otp);
                status = _AdminFacade.AddOtp(phoneno, encyptedOtp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Otp could not be sent";
                throw e;
            }
            return status;
        }
        [Route("api/Admin/VerifyPhoneOtp")]
        [HttpGet]
        public string VerifyPhoneOtp(string phoneno,string otp)
        {
            var status = "Otp could not be verified";
            try
            {
                status = _AdminFacade.VerifyOtp(phoneno, con.Encrypt(otp));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Otp could not be verified";
                throw e;
            }
            return status;
        }
        [Route("api/Admin/AllUsersLogin")]
        [HttpGet]
        public string AllUsersLogin(string emailOrPhone,string pass)
        {
            var status = "Invalid Credentials";
            try
            {
                if (_AdminFacade.Auth(emailOrPhone, con.Encrypt(pass)) == "Logged in successful")
                {
                    return _AdminFacade.GetAdminId(emailOrPhone);
                }
                else if (_UserFacade.Auth(emailOrPhone, con.Encrypt(pass)) == "Logged in successful")
                {
                    return _UserFacade.GetUserId(emailOrPhone);
                }
                else if (_VendorFacade.AuthVendor(emailOrPhone, con.Encrypt(pass)) == "Logged in successful")
                {
                    return _VendorFacade.GetVendorId(emailOrPhone);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Exception occured during validating credntials";
                throw e;
            }
            return status;
        }
        [Route("api/Admin/LoginByOTP")]
        [HttpGet]
        public string LoginByOTP(string phoneno, string otp)
        {
            var status = "Otp could not be verified";
            try
            {
                status = _AdminFacade.LoginByOTP(phoneno, con.Encrypt(otp));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Otp could not be verified";
                throw e;
            }
            return status;
        }
    }
}
