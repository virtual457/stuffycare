﻿using StuffyCare.Facade;
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
        public string Auth(string email, string pass);
        public Users GetUser(string email);
        public string AddUser(string email,string pass, string pno);
        public List<Orders> GetOrder(string userid);
        public string AddPet(Pets pet);
        public string DelPet(Pets pet);
        public List<Pets> GetPet(string userid);
        public string AddToWishlist(Wishlist wishlist);
        public string AddAppointments(Appointments apt);
        public List<Appointments> GetAppointments(string userid, string petid);
        public List<Cart> GetCart(string userid);
        public string Addcart(Cart cart);
        public string DelCart(Cart cart);
        public List<Wishlist> GetWishlist(string userid);
        public string AddWishlist(Wishlist wish);
        public string DelWishlist(Wishlist wish);

    }
}
