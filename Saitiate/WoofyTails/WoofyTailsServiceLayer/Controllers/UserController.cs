using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoofyTailsBusinessLayer.APIModels;
using WoofyTailsBusinessLayer.Repository;
using WoofyTailsDALLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WoofyTailsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _user;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper)
        {
            _user = new UserRepository();
            _mapper = mapper;
        }
        //
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "UserControllerApi is working" };
        }

        #region User(Get/post/update/delete)

            #region GetUser
        /// <summary>
        /// used to get details about a given userid
        /// </summary>
        /// <param name="userid">user id of the user for whom the details has to be fetched</param>
        /// <returns>jsonresult of a list of user object containing the details of the user of the given user id</returns>
        [HttpGet("GetUser")]
        public JsonResult GetUser(string userid)
        {
            List<User> listObj = new List<User>();

            var replistObj = _user.GetUser(userid);

            if (replistObj != null)
            {
                foreach (var obj in replistObj)
                {
                    User userObj = _mapper.Map<User>(obj);
                    listObj.Add(userObj);
                }
            }

            return new JsonResult(listObj);
        }
        #endregion

            #region AddUser
        /// <summary>
        /// user to add a new user details
        /// </summary>
        /// <remarks>
        /// Instructions:
        /// 
        ///Gender must be('M','F','O')
        ///
        ///roleid= 
        /// 1:user
        /// 2:vendor
        /// 3:sub-admin
        /// 4:admin
        /// 
        ///Both email and phonenumber cannot be null    
        /// 
        ///Sample request:
        ///
        ///     {
        ///         "userId": "null",
        ///         "firstName": "Stuffy",
        ///         "lastName": "Care",
        ///         "emailId": "stuffycare@example.com",
        ///         "password": "stuffycare",
        ///         "gender":"M",
        ///         "phoneNumber": "9999999999",
        ///         "image": "image string",
        ///         "roleId": 4
        ///     }
        /// </remarks>
        /// <param name="user">user object containing details of the user</param>
        /// <returns>string signifying the outcome/result</returns>
        [HttpPost("AddUser")]
        public JsonResult AddUSer([FromBody] User user)
        {
            
            var str = _user.AddUser(_mapper.Map<WoofyTailsDALLayer.EFModels.User>(user));
            bool status = false;
            if (str.Contains("user added sucessfully"))
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? str.Substring(0,str.Length - 36) : str,userid=status? str.Substring(str.Length-36):"0" });

            //else
            //{
            //    return new JsonResult(new { Message = "Enter Valid Data", ReceivedData = ModelState });
            //}
        }
        #endregion

            #region UpdateUser
        /// <summary>
        /// To Update the details of the user
        /// </summary>
        /// <param name="user">user obect containing the new data to be modified</param>
        /// <returns>Json object  signifying the status</returns>
        [HttpPut("UpdateUser")]
        public JsonResult UpdateUser([FromBody] User user)
        {

            var str = _user.UpdateUser(_mapper.Map<WoofyTailsDALLayer.EFModels.User>(user));
            bool status = false;
            if (str == "Sucessfully Updated user")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "user updated sucessfully" : str });

        }
        #endregion

            #region Deleteuser
        /// <summary>
        /// To delete user details from the database(modify the isdeleted column)
        /// </summary>
        /// <param name="user">user object containing userid of the user to be deleted</param>
        /// <returns> Json object  signifying the status</returns>
        [HttpDelete("DeleteUser")]
        public JsonResult DeleteUser(User user)
        {
            var str = _user.DelUser(_mapper.Map<WoofyTailsDALLayer.EFModels.User>(user));

            bool status = false;
            if (str == "Sucessfully deleted")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "user deleted sucessfully" : str });
        }
        #endregion

        #endregion

        #region Appointment(Get/post/update/delete)

            #region GetAppointment
        /// <summary>
        /// used to get details about a given appointmentid
        /// </summary>
        /// <param name="appointmentid">appointment id of the appointment for whom the details has to be fetched</param>
        /// <returns>jsonresult of a list of appointment object containing the details of the appointment of the given appointment id</returns>
        [HttpGet("GetAppointment")]
        public JsonResult GetAppointment(string appointmentid)
        {
            List<Appointment> listObj = new List<Appointment>();

            var replistObj = _user.GetAppointment(appointmentid);

            if (replistObj != null)
            {
                foreach (var obj in replistObj)
                {
                    Appointment appointmentObj = _mapper.Map<Appointment>(obj);
                    listObj.Add(appointmentObj);
                }
            }

            return new JsonResult(listObj);
        }
        #endregion

            #region AddAppointment
        /// <summary>
        /// appointment to add a new appointment details
        /// </summary>
        /// <remarks>
        /// Instructions:
        /// 
        ///
        /// </remarks>
        /// <param name="appointment">appointment object containing details of the appointment</param>
        /// <returns>string signifying the outcome/result</returns>
        [HttpPost("AddAppointment")]
        public JsonResult AddUSer([FromBody] Appointment appointment)
        {

            var str = _user.AddAppointment(_mapper.Map<WoofyTailsDALLayer.EFModels.Appointment>(appointment));
            bool status = false;
            if (str.Contains("appointment added sucessfully"))
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? str.Substring(0, str.Length - 36) : str, appointmentid = status ? str.Substring(str.Length - 36) : "0" });

            //else
            //{
            //    return new JsonResult(new { Message = "Enter Valid Data", ReceivedData = ModelState });
            //}
        }
        #endregion

            #region UpdateAppointment
        /// <summary>
        /// To Update the details of the appointment
        /// </summary>
        /// <param name="appointment">appointment obect containing the new data to be modified</param>
        /// <returns>Json object  signifying the status</returns>
        [HttpPut("UpdateAppointment")]
        public JsonResult UpdateAppointment([FromBody] Appointment appointment)
        {

            var str = _user.UpdateAppointment(_mapper.Map<WoofyTailsDALLayer.EFModels.Appointment>(appointment));
            bool status = false;
            if (str == "Sucessfully Updated appointment")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "appointment updated sucessfully" : str });

        }
        #endregion

            #region Deleteappointment
        /// <summary>
        /// To delete appointment details from the database(modify the isdeleted column)
        /// </summary>
        /// <param name="appointment">appointment object containing appointmentid of the appointment to be deleted</param>
        /// <returns> Json object  signifying the status</returns>
        [HttpDelete("DeleteAppointment")]
        public JsonResult DeleteAppointment(Appointment appointment)
        {
            var str = _user.DelAppointment(_mapper.Map<WoofyTailsDALLayer.EFModels.Appointment>(appointment));

            bool status = false;
            if (str == "Sucessfully deleted")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "appointment deleted sucessfully" : str });
        }
        #endregion

        #endregion

        #region ValidateUser(Get)
        /// <summary>
        /// Used to validate user credentials
        /// </summary>
        /// <param name="emailorphone">email or phone number of the user</param>
        /// <param name="password">password of the user</param>
        /// <returns></returns>
        [HttpGet("ValidateUser")]
        public JsonResult ValidateUser(string emailorphone, string password)
        {
            var a = _user.ValidateUser(emailorphone, password);
            if (a > 0)
            {
                return new JsonResult(new { Success = true, Message = "Logged in sucessfully", RoleID = a, UserID = _user.GetUserId(emailorphone, password) });            
            }
            return new JsonResult(new { Success = true, Message = "Invalid Credentials", RoleID = 0, UserID = 0 });

        
        }
        #endregion

        #region Pet(Get/Update/Delete/Post)
        /// <summary>
        /// used to fetch the list of pets of an given user id
        /// and filter the result set with the given petid
        /// </summary>
        /// <param name="userid">user id of the user</param>
        /// <param name="petid">pet id or all if they want all pets of an user</param>
        /// <returns> json result of a list of pet objects containg the pets of the given user id</returns>
        [HttpGet("GetPet")]
        public JsonResult GetPet(string userid,string petid)
        {
            List<Pet> listObj = new List<Pet>();

            var replistObj = _user.GetPetforUser(userid,petid);

            if (replistObj != null)
            {
                foreach (var obj in replistObj)
                {
                    Pet petObj = _mapper.Map<Pet>(obj);
                    listObj.Add(petObj);
                }
            }

            return new JsonResult(listObj);
        }
        /// <summary>
        /// To add a pet data of user to the database
        /// </summary>
        /// <param name="pet">pet object containing data about the pet</param>
        /// <returns>json Object signifies the result</returns>

        [HttpPost("AddPet")]
        public JsonResult AddPet([FromBody] Pet pet)
        {

            var str = _user.AddPet(_mapper.Map<WoofyTailsDALLayer.EFModels.Pet>(pet));
            bool status = false;
            if (str == "pet added sucessfully")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "pet added sucessfully" : str });
        }
        /// <summary>
        /// To update pet data of user in the database
        /// </summary>
        /// <param name="pet">pet object containing data about the pet</param>
        /// <returns>JsonObject which signifies the result</returns>

        [HttpPut("UpdatePet")]
        public JsonResult UpdatePet([FromBody] Pet pet)
        {

            var str = _user.UpdatePet(_mapper.Map<WoofyTailsDALLayer.EFModels.Pet>(pet));
            bool status = false;
            if (str == "Sucessfully Updated pet")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "pet updated sucessfully" : str });

        }

        /// <summary>
        /// To delete a pet data of user from the database
        /// </summary>
        /// <param name="pet">pet object containing data about the pet</param>
        /// <returns>jsonObject which signifies the result</returns>
        [HttpDelete("DeletePet")]
        public JsonResult DeletePet(Pet pet)
        {
            var str = _user.DelPet(_mapper.Map<WoofyTailsDALLayer.EFModels.Pet>(pet));

            bool status = false;
            if (str == "Sucessfully deleted")
            {
                status = true;
            }
            return new JsonResult(new { Success = status, message = status ? "pet deleted sucessfully" : str });
        }
        #endregion
    }
}
