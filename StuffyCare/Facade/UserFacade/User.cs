using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StuffyCare.DataLayer;
using StuffyCare.EFModels;

namespace StuffyCare.Facade
{
    public class User
    {
        private readonly DataLayer.UserDAO.IUserDAO UserDao = DataAccess.UserDAO;

        public string Auth(string email, string pass)
        {
            try
            {
                return UserDao.Auth(email, pass);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public string Create(Users user)
        {
            try
            {
                return UserDao.AddUser(user);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<Orders> GetOrders(string userid)
        {
            try
            {
                return UserDao.GetOrder(userid);
            }
            catch(Exception e)
            { 
                throw e;
            }
        }
        public string AddOrder(Orders order)
        {
            var str = string.Empty;
            try
            {
                str = UserDao.AddOrder(order);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        public Users GetUser(string userid)
        {
            try
            {
                return UserDao.GetUser(userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string AddPet(Pets pet)
        {
            var str = string.Empty;
            try
            {
                str = UserDao.AddPet(pet);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        public string DeletePet(Pets pet)
        {
            var str = string.Empty;
            try
            {
                str = UserDao.DelPet(pet);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        public string Updatepet(Pets pet)
        {
            var str = string.Empty;
            try
            {
                str = UserDao.UpdatePet(pet);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        public List<Pets> GetPets(string userid)
        {
            var listobj = new List<Pets>();
            try
            {
                listobj = UserDao.GetPet(userid);
            }
            catch (Exception e)
            {
                listobj = null;
                throw e;
            }
            return listobj;
        }
        public string AddAppointments(Appointments userid)
        {
            try
            {
                return UserDao.AddAppointments(userid);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public List<Appointments> GetAppointments(string userid, string petid)
        {
            try
            {
                return UserDao.GetAppointments(userid, petid);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public List<Cart> GetCart(string userid)
        {
            try
            {
                return UserDao.GetCart(userid);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public string AddCart(Cart cart)
        {
            try
            {
                return UserDao.Addcart(cart);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public string DelCart(Cart cart)
        {
            try
            {
                return UserDao.DelCart(cart);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public List<Wishlist> GetWishlist(string userid)
        {
            try
            {
                return UserDao.GetWishlist(userid);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public string AddWishlist(Wishlist wishlist)
        {
            try
            {
                return UserDao.AddWishlist(wishlist);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public string DelWishlist(Wishlist wishlist)
        {
            try
            {
                return UserDao.DelWishlist(wishlist);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public string UpdateCart(Cart cart)
        {
            string res;
            try
            {
                res = UserDao.UpdateCart(cart);
            }
            catch (Exception e)
            {

                throw e;
            }
            return res;
        }
        public string UpdatePass(string emailorphone,string password)
        {
            string res="Update Failed";
            try
            {
                res = UserDao.ChangePassword(emailorphone,password);
            }
            catch (Exception e)
            {

                throw e;
            }
            return res;
        }
        public string GetUserId(string emailorphone)
        {
            try
            {
                return UserDao.GetUserId(emailorphone);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public string UpdateUser(Users user)
        {
            var str = string.Empty;
            try
            {
                str = UserDao.UpdateUser(user);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        public string AddAddress(Address address)
        {
            var str = string.Empty;
            try
            {
                str = UserDao.AddAddress(address);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        public string DeleteAddress(Address address)
        {
            var str = string.Empty;
            try
            {
                str = UserDao.DelAddress(address);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        public string Updateaddress(Address address)
        {
            var str = string.Empty;
            try
            {
                str = UserDao.UpdateAddress(address);
            }
            catch (Exception e)
            {
                str = e.Message;
                throw e;
            }
            return str;
        }
        public List<Address> GetAddress(string userid)
        {
            var listobj = new List<Address>();
            try
            {
                listobj = UserDao.GetAddress(userid);
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
