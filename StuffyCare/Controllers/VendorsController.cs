using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StuffyCare.DataLayer;
using StuffyCare.Facade;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuffyCare.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class VendorsController : ControllerBase
    {
        private readonly Vendor _VendorFacade = new Vendor();
        private readonly Connection con = new Connection();
        // GET: api/<VendorsController>
        /// <summary>
        /// simple api to check wheather its working or not
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Vendor api is Working" };
        }
        /// <summary>
        /// Api to add vendors to vendor auth table
        /// </summary>
        /// <param name="vendors"></param>
        /// <returns></returns>
        [HttpPost("AddVendor")]
        public string AddVendor([FromBody] Models.Vendors vendors)
        {

            var status = "Update failed";
            try
            {
                status = _VendorFacade.Create(vendors.Email, con.Encrypt(vendors.Pass), vendors.Pno);
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return status;

        }
        /// <summary>
        /// Api to authenticate vendors
        /// </summary>
        /// <param name="vendors"></param>
        /// <returns></returns>
        [HttpPost("AuthVendor")]
        public string AuthVendor([FromBody] Models.Vendors vendors)
        {
            var status = "Login failed";
            try
            {
                if (vendors.Email == "")
                {
                    vendors.Email = vendors.Pno;
                }
                status = _VendorFacade.AuthVendor(vendors.Email,con.Encrypt( vendors.Pass));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Login Failed";
            }
            return status;
        }
        
    }
}
