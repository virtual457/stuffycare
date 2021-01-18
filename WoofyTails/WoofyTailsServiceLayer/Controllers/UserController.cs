using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoofyTailsBusinessLayer;
using WoofyTailsBusinessLayer.Repository;
using WoofyTailsDALLayer.EFModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WoofyTailsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly UserRepository _user;
        public UserController()
        {
            _user = new UserRepository();
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("GetUser")]
        public JsonResult GetUser(string userid)
        {
            List<User> listObj = new List<User>();
            try
            {
                listObj = _user.GetUser(userid);
            }
            catch (Exception e)
            {

                throw e;
            }
            return new JsonResult(listObj);
        }

        // POST api/<ValuesController> ok bool???
        [HttpPost("AddUser")]
        public JsonResult AddUSer([FromBody] User user)
        {
            
            var str = string.Empty;
            str=_user.AddUser(user);
            int save=0;
            if (str == "dal executed compltely")
            {
                save = _user.save();
            }

            return new JsonResult(new {Success=save>0?true:false, message = save > 0 ? "str" : "" });

            
        }

        // PUT api/<ValuesController>/5 ok sir
        [HttpPut("UpdateUser")]
        public string UpdateUser([FromBody] User user)
        {
            var str = "could not add user";
            try
            {
                str = _user.UpdateUser(user);
            }
            catch (Exception e)
            {
                throw e;
            }
            return str;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("DeleteUser")]
        public string DeleteUser(User user)
        {
            var str = "could not add user";
            try
            {
                str = _user.DelUser(user);
            }
            catch (Exception e)
            {
                throw e;
            }
            return str;
        }
    }
}
