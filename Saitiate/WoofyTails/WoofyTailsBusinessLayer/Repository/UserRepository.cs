using System;
using System.Collections.Generic;
using WoofyTailsDALLayer.EFModels;
using System.Linq;
using WoofyTailsBusinessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace WoofyTailsBusinessLayer.Repository
{
    public class UserRepository : BaseRepository
    {
        //user region of the user repository
        #region user region(add/update/delete/get)
        /// <summary>
        /// user to add a new user details
        /// </summary>
        /// <param name="user">user object containing details of the user</param>
        /// <returns>string signifying the outcome/result</returns>
        public string AddUser(User user)
        {
            var str = "";
            try
            {
                var userobj = (from c in context.Users
                               where c.EmailId == user.EmailId
                               select c).AsNoTracking();

                if (user.EmailId == null && user.PhoneNumber == null)
                {
                    return "Both email and phone number cannot be null";
                }
                if (userobj.Count() > 0 && user.EmailId != null)
                {
                    return "email alredy exists";
                }
                userobj = (from c in context.Users
                           where c.PhoneNumber == user.PhoneNumber
                           select c).AsNoTracking();
                if (userobj.Count() > 0 && user.PhoneNumber != null)
                {
                    return "Phone Number alredy exists";
                }

                user.UserId = Guid.NewGuid().ToString();
                user.IsDeleted = 0;
                context.Users.Add(user);
                if (this.save() > 0)
                {
                    return "user added sucessfully as "+user.UserId;
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
        /// To delete user details from the database(modify the isdeleted column)
        /// </summary>
        /// <param name="user">user object containing userid of the user to be deleted</param>
        /// <returns> a string signifying the status</returns>
        public string DelUser(User user)
        {
            var str = string.Empty;
            try
            {
                var dbuser = context.Users.Find(user.UserId);

                if (dbuser != null)
                {
                    dbuser.IsDeleted = 1;
                    // context.Users.Update(dbuser);

                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully deleted";
                    }
                }
                else
                {
                    str = "User doesnt exist";
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
        /// To Update the details of the user
        /// </summary>
        /// <param name="user">user obecjt containing the new data to be modified</param>
        /// <returns>string signifying the status</returns>
        public string UpdateUser(User user)
        {
            var str = string.Empty;
            try
            {
                var dbuser = context.Users.Find(user.UserId);
                if (dbuser != null)
                {
                    var userobj = (from c in context.Users
                               where c.EmailId == user.EmailId && c.UserId != user.UserId
                               select c).AsNoTracking();

                    if (user.EmailId == null && user.PhoneNumber == null)
                    {
                         return "Both email and phone number cannot be null";
                    }
                    if (userobj.Count() > 0 && user.EmailId != null)
                    {
                        return "email alredy exists";
                    }
                    userobj = (from c in context.Users
                               where c.PhoneNumber == user.PhoneNumber
                               select c).AsNoTracking();
                    if (userobj.Count() > 0 && user.PhoneNumber != null)
                    {
                        return "Phone Number alredy exists";
                    }
                    dbuser.FirstName = user.FirstName;
                    dbuser.LastName = user.LastName;
                    dbuser.EmailId = user.EmailId;
                    dbuser.PhoneNumber = user.PhoneNumber;
                    dbuser.RoleId = user.RoleId;
                    dbuser.Image = user.Image;
                    dbuser.Password = user.Password;
                    context.Users.Update(dbuser);
                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully Updated user";
                    }
                }
                else
                {
                    str = "User doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        /// <summary>
        /// used to get details about a given userid
        /// </summary>
        /// <param name="userId">user id of the user for whom the details has to be fetched</param>
        /// <returns>a list of user object containing the details of the user of the given user id</returns>
        public List<User> GetUser(string userId)
        {
            List<User> listobj = new List<User>();
            try
            {
                listobj = (from user in context.Users
                           where user.UserId == userId && user.IsDeleted == 0
                           select user
                         ).ToList();
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        #endregion

        //pet region of the pet repository
        #region pet(add/update/delete/get)


        /// <summary>
        /// To add a pet data of user to the database
        /// </summary>
        /// <param name="pet">pet object containing data about the pet</param>
        /// <returns>string which signifies the result</returns>
        public string AddPet(Pet pet)
        {
            var str = "";
            try
            {
                var userobj = (from c in context.Users
                               where c.UserId == pet.Userid && c.IsDeleted != 1
                               select c).AsNoTracking();

               
                if (userobj.Count() < 0 )
                {
                    return "user doesnt exists";
                }
                
                pet.Petid = Guid.NewGuid().ToString();
                pet.Isdeleted = 0;
                context.Pets.Add(pet);
                if (this.save() > 0)
                {
                    return "pet added sucessfully";
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
        /// To delete a pet data of user from the database
        /// </summary>
        /// <param name="pet">pet object containing data about the pet</param>
        /// <returns>string which signifies the result</returns>
        public string DelPet(Pet pet)
        {
            var str = string.Empty;
            try
            {
                var dbpet = context.Pets.Find(pet.Petid);

                if (dbpet != null)
                {
                    dbpet.Isdeleted = 1;
                    // context.Users.Update(dbuser);

                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully deleted";
                    }
                }
                else
                {
                    str = "Pet id doesnt exist";
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
        /// To update pet data of user in the database
        /// </summary>
        /// <param name="pet">pet object containing data about the pet</param>
        /// <returns>string which signifies the result</returns>
        public string UpdatePet(Pet pet)
        {
            var str = string.Empty;
            try
            {
                var dbpet = context.Pets.Find(pet.Petid);
                if (dbpet != null && dbpet.Isdeleted != 1)
                {
                    
                    var userobj = (from c in context.Users
                               where c.UserId == pet.Userid
                               select c).AsNoTracking();
                    if (userobj.Count() < 0)
                    {
                        return "User Doesn't exists";
                    }
                    dbpet.Userid = pet.Userid;
                    dbpet.Name = pet.Name;
                    dbpet.Type = pet.Type;
                    dbpet.Size = pet.Size;
                    dbpet.Gender = pet.Gender;
                    dbpet.Breed = pet.Breed;
                    dbpet.Allergies = pet.Allergies;
                    dbpet.Age = pet.Age;
                    dbpet.Moreinfo = pet.Moreinfo;
                    
                    context.Pets.Update(dbpet);
                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully Updated pet";
                    }
                }
                else
                {
                    str = "Petid doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        /// <summary>
        /// used to fetch the list of pets of an given user id
        /// and filter the result set with the given petid
        /// </summary>
        /// <param name="userId">user id of the user</param>
        /// <param name="petId">pet id or all if they want all pets of an user</param>
        /// <returns> a list of pet objects containg the pets of the given user id</returns>
        public List<Pet> GetPetforUser(string userId,string petId)
        {
            List<Pet> listobj = new List<Pet>();
            try
            {
                if (userId != "all" && petId != "all")
                {
                    listobj = (from pet in context.Pets
                               where pet.Userid == userId && pet.Isdeleted == 0 && pet.Petid == petId
                               select pet
                             ).AsNoTracking().ToList();
                }
                else if (petId == "all" && userId != "all")
                {
                    listobj = (from pet in context.Pets
                               where pet.Userid == userId && pet.Isdeleted == 0
                               select pet
                             ).AsNoTracking().ToList();

                }
                else 
                {
                    listobj = (from pet in context.Pets
                               where pet.Isdeleted == 0
                               select pet
                                 ).AsNoTracking().ToList();
                }
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        #endregion

        //user login related function of user repository
        #region user login/verification
        /// <summary>
        /// used to validate the credentials of user
        /// accepts phone or email of user to validate
        /// </summary>
        /// <param name="emailidorphone">email id or phone number of user</param>
        /// <param name="pass">password of the user</param>
        /// <returns> string message signifying the result</returns>
        public int ValidateUser(string emailidorphone,string pass)
        {
            int roleid = 0;
            try
            {
                var obj = (from user in context.Users
                           where (user.EmailId == emailidorphone || user.PhoneNumber==emailidorphone )&& user.IsDeleted == 0 && user.Password==pass
                           select user
                         ).FirstOrDefault();
                if (obj != null)
                {
                    roleid = (int)obj.RoleId;
                }
            }
            catch (Exception e)
            {
                roleid = 0;
                throw e;
            }
            return roleid;
        }
        /// <summary>
        /// used to retrive the userid of the user
        /// accepts phone or email of user to validate
        /// </summary>
        /// <param name="emailidorphone">email of phone number of the user</param>
        /// <param name="pass">password of the user</param>
        /// <returns> string message signifying the result</returns>
        public string GetUserId(string emailidorphone, string pass)
        {
            string userid = null;
            try
            {
                var obj =(from user in context.Users
                               where (user.EmailId == emailidorphone || user.PhoneNumber == emailidorphone) && user.IsDeleted == 0 && user.Password == pass
                               select user
                         ).FirstOrDefault();
                if (obj != null)
                {
                    userid = obj.UserId;
                }
            }
            catch (Exception e)
            {
                userid = null;
                throw e;
            }
            return userid;
        }
        #endregion

        //booking appointmets for user
        #region appointment region(add/update/delete/get)
        /// <summary>
        /// appointment to add a new appointment details
        /// </summary>
        /// <param name="appointment">appointment object containing details of the appointment</param>
        /// <returns>string signifying the outcome/result</returns>
        public string AddAppointment(Appointment appointment)
        {
            var str = "";
            try
            {
                var userobj = (from c in context.Users
                               where c.UserId == appointment.Userid && c.IsDeleted==0
                               select c).AsNoTracking().FirstOrDefault();
                if (userobj == null)
                {
                    return "user id doesnt exist";
                }
                var petobj = (from c in context.Pets
                               where c.Petid == appointment.Petid && c.Isdeleted == 0
                               select c).AsNoTracking().FirstOrDefault();
                if (petobj == null)
                {
                    return "pet id doesnt exist";
                }
                var vendorobj = (from c in context.Users
                               where c.UserId == appointment.Vendorid && c.IsDeleted == 0 && c.RoleId ==2
                               select c).AsNoTracking().FirstOrDefault();
                if (vendorobj == null)
                {
                    return "vendor id doesnt exist";
                }
                

                appointment.Aptid = Guid.NewGuid().ToString();
                
                context.Appointments.Add(appointment);
                if (this.save() > 0)
                {
                    return "appointment added sucessfully as " + appointment.Aptid;
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
        /// To delete appointment details from the database(Here it deletes the appointment)
        /// </summary>
        /// <param name="appointment">appointment object containing appointmentid of the appointment to be deleted</param>
        /// <returns> a string signifying the status</returns>
        public string DelAppointment(Appointment appointment)
        {
            var str = string.Empty;
            try
            {
                var dbappointment = context.Appointments.Find(appointment.Aptid);

                if (dbappointment != null)
                {
                    context.Appointments.Remove(dbappointment);
                    // context.Appointments.Update(dbappointment);

                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully deleted";
                    }
                }
                else
                {
                    str = "Appointment doesnt exist";
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
        /// To Update the details of the appointment
        /// </summary>
        /// <param name="appointment">appointment obecjt containing the new data to be modified</param>
        /// <returns>string signifying the status</returns>
        public string UpdateAppointment(Appointment appointment)
        {
            var str = string.Empty;
            try
            {
                var dbappointment = context.Appointments.Find(appointment.Aptid);
                if (dbappointment != null)
                {
                    var appointmentobj = (from c in context.Appointments
                                   where c.Aptid != appointment.Aptid
                                   select c).AsNoTracking();

                    context.Appointments.Update(dbappointment);
                    if (context.SaveChanges() > 0)
                    {
                        str = "Sucessfully Updated appointment";
                    }
                }
                else
                {
                    str = "Appointment doesnt exist";
                }
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            };
            return str;
        }
        /// <summary>
        /// used to get details about a given appointmentid
        /// </summary>
        /// <param name="appointmentId">appointment id of the appointment for whom the details has to be fetched</param>
        /// <returns>a list of appointment object containing the details of the appointment of the given appointment id</returns>
        public List<Appointment> GetAppointment(string appointmentId)
        {
            List<Appointment> listobj = new List<Appointment>();
            try
            {
                listobj = (from appointment in context.Appointments
                           where appointment.Aptid == appointmentId 
                           select appointment
                         ).ToList();
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        #endregion
    }
}
