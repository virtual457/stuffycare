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
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        private readonly Vendor _vendorFacade = new Vendor();
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "Api is working" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("AuthVendor")]
        public string AuthVendor(string email,string pass)
        {
            return _vendorFacade.AuthVendor(email, pass);
        }

        // POST api/<ValuesController>
        
    }
}
