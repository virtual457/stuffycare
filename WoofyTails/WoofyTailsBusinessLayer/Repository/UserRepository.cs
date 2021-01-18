using System;
using System.Collections.Generic;
using WoofyTailsDALLayer.EFModels;
using System.Linq;
using WoofyTailsBusinessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace WoofyTailsBusinessLayer.Repository
{
    public class UserRepository:BaseRepository
    {
        
        public string AddUser(User user)
        {
            var str = "";
            try
            {
                var userobj = (from c in context.Users
                           where c.EmailId == user.EmailId
                           select c).AsNoTracking();
           
            if (user.EmailId == "" && user.PhoneNumber == "")
            {
                return "Both email and phone number cannot be null";
            }
            if (userobj.Count() > 0 && user.EmailId != "")
            {
                return "email alredy exists";
            }
            userobj = (from c in context.Users
                       where c.PhoneNumber == user.PhoneNumber
                       select c).AsNoTracking();
            if (userobj.Count() > 0 && user.PhoneNumber != "")
            {
                return "Phone Number alredy exists";
            }

                user.UserId= Guid.NewGuid().ToString();
                user.IsDeleted = false;
                context.Users.Add(user);
                if (this.save()>0)
                {
                    return "user added sucessfully";
                }
            }

            catch (Exception e)
            {
                str= e.Message;
                throw e;

            }
            return str;
        }

        public string DelUser(User user)
        {
            var str = string.Empty;
            try
            {
                var dbuser = context.Users.Find(user.UserId);
                
                if (dbuser != null)
                {
                    dbuser.IsDeleted = true;
                   // context.Users.Update(dbuser);

                    context.SaveChanges();
                    str = "Sucessfully deleted";
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
        public string UpdateUser(User user)
        {
            var str = string.Empty;
            try
            {
                var dbuser = context.Users.Find(user.UserId);
                if (dbuser != null)
                {

                   
                    dbuser.FirstName = user.FirstName;
                    dbuser.LastName = user.LastName;
                    dbuser.EmailId = user.EmailId;
                    dbuser.PhoneNumber = user.PhoneNumber;
                    dbuser.RoleId = user.RoleId;
                    dbuser.Image = user.Image;
                    dbuser.Password = user.Password;
                    

                    context.Users.Update(dbuser);

                    context.SaveChanges();
                    str = "Sucessfully Updated user";
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

        public List<User> GetUser(string userId)
        {
            List<User> listobj = new List<User>();
            try
            {
                listobj = (from user in context.Users
                           where user.UserId == userId && user.IsDeleted == false
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

    }
}
