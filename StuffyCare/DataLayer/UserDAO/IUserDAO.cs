using StuffyCare.Facade;
using StuffyCare.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Policy;

namespace StuffyCare.DataLayer.UserDAO
{
    public interface IUserDAO
    {
         string Auth(string email, string pass);
         Users GetUser(string userid);
         string AddUser(Users user);
         List<Orders> GetOrder(string userid);
        string AddOrder(Orders order);
         string AddPet(Pets pet);
         string DelPet(Pets pet);
        string UpdatePet(Pets pet);
         List<Pets> GetPet(string userid);
         string AddToWishlist(Wishlist wishlist);
         string AddAppointments(Appointments apt);
         List<Appointments> GetAppointments(string userid, string petid);
         List<Cart> GetCart(string userid);
         string Addcart(Cart cart);
         string DelCart(Cart cart);
         string UpdateCart(Cart cart);
         List<Wishlist> GetWishlist(string userid);
         string AddWishlist(Wishlist wish);
         string DelWishlist(Wishlist wish);
        string ChangePassword(string emailorphone, string password);
        string GetUserId(string emailorphone);

        string UpdateUser(Users user);
        string AddAddress(Address pet);
        string DelAddress(Address pet);
        string UpdateAddress(Address pet);
        List<Address> GetAddress(string userid);
    }
}
