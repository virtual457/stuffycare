using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StuffyCare.Facade;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuffyCare.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class VendorsController : ControllerBase
    {
        private readonly Vendor _VendorFacade = new Vendor();
        // GET: api/<VendorsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Vendor api is Working" };
        }

        // GET api/<VendorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpPost("AddVendor")]
        public string AddVendor([FromBody] Models.Vendors vendors)
        {

            var status = "Update failed";
            try
            {
                status = _VendorFacade.Create(vendors.Email, vendors.Pass, vendors.Pno);
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return status;

        }
        [HttpPost("AuthVendor")]
        public string AuthVendor([FromBody] Models.Vendors vendors)
        {
            var status = "Login failed";
            try
            {
                status = _VendorFacade.AuthVendor(vendors.Email, vendors.Pass);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = "Login Failed";
            }
            return status;
        }
        // POST api/<VendorsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VendorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VendorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
