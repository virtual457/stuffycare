using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using StuffyCare.DataLayer;
using StuffyCare.Facade;
using StuffyCare.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuffyCare.Controllers
{
    [ExceptionFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VendorsController : ApiController
    {
        private readonly Vendor _VendorFacade = new Vendor();
        private readonly Connection con = new Connection();
        // GET: api/<VendorsController>
        /// <summary>
        /// simple api to check wheather its working or not
        /// </summary>
        /// <returns></returns>
        [Route("api/Vendor/Get")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Vendor api is Working" };
        }
        [Route("api/Vendor/GetVendor")]
        [HttpGet]
        public Models.Vendors GetVendor(string vendorid)
        {
            Models.Vendors venobj = new Models.Vendors();
            try
            {
                var vendors = _VendorFacade.GetVendor(vendorid);
                venobj = new Models.Vendors
                {
                    Vendorid=vendors.Vendorid,
                    Name = vendors.Name,
                    Description = vendors.Description,
                    Email = vendors.Email,
                    Pass = vendors.Pass,
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
                    Homeservice=vendors.Homeservice,
                    Issellingitem=vendors.Issellingitem,
                    Photoidproof = vendors.Photoidproof,
                    Isauthorized = vendors.Isauthorized,
                    Isdeleted = vendors.Isdeleted
                };
            }
            catch (Exception e)
            {

                throw e;
            }
            return venobj;
        }
        /// <summary>
        /// Api to add vendors to vendor auth table
        /// </summary>
        /// <param name="vendors"></param>
        /// <returns></returns>
        /// 
        [Route("api/Vendor/AddVendor")]
        [HttpPost]
        public string AddVendor([FromBody] Models.Vendors vendors)
        {

            var status = "adding vendor failed";
            try
            {
                var venobj = new EFModels.Vendors
                {
                    Name=vendors.Name,
                    Description=vendors.Description,
                    Email=vendors.Email,
                    Pass=con.Encrypt(vendors.Pass),
                    Pno=vendors.Pno,
                    Gender=vendors.Gender,
                    StoreName=vendors.StoreName,
                    City=vendors.City,
                    Location=vendors.Location,
                    Yearsofexperience=vendors.Yearsofexperience,
                    Monfrom=vendors.Monfrom,
                    Monto=vendors.Monto,
                    Tuefrom=vendors.Tuefrom,
                    Tueto=vendors.Tueto,
                    Wedfrom=vendors.Wedfrom,
                    Wedto=vendors.Wedto,
                    Thurfrom=vendors.Thurfrom,
                    Thurto=vendors.Thurto,
                    Frifrom=vendors.Frifrom,
                    Frito=vendors.Frito,
                    Satfrom=vendors.Satfrom,
                    Satto=vendors.Satto,
                    Sunfrom=vendors.Sunfrom,
                    Sunto=vendors.Sunto,
                    Photo=vendors.Photo,
                    Issellingitem=vendors.Issellingitem,
                    Homeservice=vendors.Homeservice,
                    Photoidproof=vendors.Photoidproof,
                    Isauthorized=vendors.Isauthorized,
                    Isdeleted=vendors.Isdeleted
                };
                status = _VendorFacade.Create(venobj);
            }
            catch (Exception e)
            {
                status =e.Message;
                throw e;
            }
            if (status == "Vendor added sucessfully")
            {
                return status +" as "+ _VendorFacade.GetVendorId(vendors.Email);
            }
            return status;

        }
        /// <summary>
        /// Api to authenticate vendors
        /// </summary>
        /// <param name="vendors"></param>
        /// <returns></returns>
        /// 
        [Route("api/Vendor/AuthVendor")]
        [HttpPost]
        public string AuthVendor([FromBody] Models.Vendors vendors)
        {
            var status = "Login failed";
            try
            {
                if (vendors.Email == "")
                {
                    vendors.Email = vendors.Pno;
                }
                status = _VendorFacade.AuthVendor(vendors.Email, con.Encrypt(vendors.Pass));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Login Failed";
                throw e;
            }
            return status;
        }
        [Route("api/Vendor/VendorAddItem")]
        [HttpPost]
        public string VendorAddItem([FromBody] Models.Items item)
        {
            var status = "Adding item Failed";
            try
            {
                var iter = new EFModels.Items
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

                    status = _VendorFacade.AddVendorItem(iter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = e.Message;
                throw e;
            }
            return status;
        }
        [Route("api/User/GetServices")]
        [HttpGet]

        public List<Models.Services> GetServices(string vendorid)
        {
            List<Models.Services> listobj = new List<Models.Services>();
            try
            {

                var repobj = _VendorFacade.GetServices(vendorid);

                if (repobj != null)
                {
                    foreach (var service in repobj)
                    {
                        var iter = new Models.Services
                        {
                            Id = service.Id,
                            Name = service.Name,
                            Vendorid = service.Vendorid,
                            Price = service.Price
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
        /// To add an item to service of that user
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [Route("api/User/AddServices")]
        [HttpPost]
        public string AddServices([FromBody] Models.Services service)
        {
            var status = "could not add services";
            try
            {
                EFModels.Vendorservices cartb = new EFModels.Vendorservices()
                {
                    Name = service.Name,
                    Vendorid = service.Vendorid,
                    Price = service.Price,
                };

                status = _VendorFacade.AddServices(cartb);
            }
            catch (Exception e)
            {
                status = e.Message;
                throw e;
            }
            return status;
        }
        /// <summary>
        /// To Delete item from service of that user
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [Route("api/User/DeleteServices")]
        [HttpDelete]
        public string DeleteCart([FromBody] Models.Services service)
        {
            var str = string.Empty;
            try
            {
                EFModels.Vendorservices cartb = new EFModels.Vendorservices()
                {
                    Id = service.Id,
                    Name = service.Name,
                    Vendorid = service.Vendorid,
                    Price = service.Price
                };


                str = _VendorFacade.DelServices(cartb);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;

            }
            return str;
        }
        [Route("api/User/UpdateServices")]
        [HttpPut]
        public string UpdateServices([FromBody] Models.Services service)
        {
            var str = "Update service failed in Controller method";
            try
            {
                EFModels.Vendorservices cartb = new EFModels.Vendorservices()
                {
                    Id = service.Id,
                    Name = service.Name,
                    Vendorid = service.Vendorid,
                    Price = service.Price
                };
                str = _VendorFacade.UpdateServices(cartb);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        [Route("api/User/GetVendorsByServiceName")]
        [HttpGet]

        public List<Models.Services> GetVendorsByServiceName(string name)
        {
            List<Models.Services> listobj = new List<Models.Services>();
            try
            {

                var repobj = _VendorFacade.GetVendorsByServiceName(name);

                if (repobj != null)
                {
                    foreach (var service in repobj)
                    {
                        var iter = new Models.Services
                        {
                            Id = service.Id,
                            Name = service.Name,
                            Vendorid = service.Vendorid,
                            Price = service.Price
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
        [Route("api/User/GetAppointmentsByVendorid")]
        [HttpGet]

        public List<Models.Appointments> GetAppointmentsByVendorid(string vendorid)
        {
            List<Models.Appointments> listobj = new List<Models.Appointments>();
            try
            {

                var repobj = _VendorFacade.GetAppointmentsByVendorid(vendorid);

                if (repobj != null)
                {
                    foreach (var service in repobj)
                    {
                        var iter = new Models.Appointments
                        {
                            Id = service.Id,
                            Aptid = service.Aptid,
                            Userid = service.Userid,
                            Phonenumber = service.Phonenumber,
                            Vendorid = service.Vendorid,
                            Category = service.Category,
                            Petid = service.Petid,
                            Servicedatetime = service.Servicedatetime,
                            Servicefees = service.Servicefees,
                            Address = service.Address,
                            Message = service.Message,
                            Ishomeservice = service.Ishomeservice,
                            Ispaid = service.Ispaid
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
    }
}
