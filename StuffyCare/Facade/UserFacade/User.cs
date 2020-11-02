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
            return UserDao.Auth(email, pass);
        }
        public string Create(string email, string pass, string pno)
        {
            return UserDao.AddUser(email, pass, pno);
        }
        public List<Orders> GetOrders(string userid)
        {
            return UserDao.GetOrder(userid);
        }
        public Users GetUser(string emailid)
        {
            return UserDao.GetUser(emailid);
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
                throw;
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
                throw;
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
                throw;
            }
            return listobj;
        }
        public string AddAppointments(Appointments userid)
        {
            return UserDao.AddAppointments(userid);
        }
        public List<Appointments> GetAppointments(string userid, string petid)
        {
            return UserDao.GetAppointments(userid, petid);
        }
        public List<Cart> GetCart(string userid)
        {
            return UserDao.GetCart(userid);
        }
        public string AddCart(Cart cart)
        {
            return UserDao.Addcart(cart);
        }
        public string DelCart(Cart cart)
        {
            return UserDao.DelCart(cart);
        }
        public List<Wishlist> GetWishlist(string userid)
        {
            return UserDao.GetWishlist(userid);
        }
        public string AddWishlist(Wishlist wishlist)
        {
            return UserDao.AddWishlist(wishlist);
        }
        public string DelWishlist(Wishlist wishlist)
        {
            return UserDao.DelWishlist(wishlist);
        }
    }
}
