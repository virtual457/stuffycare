﻿using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Users
    {
        public Users()
        {
            Appointments = new HashSet<Appointments>();
            Cart = new HashSet<Cart>();
            Orders = new HashSet<Orders>();
            Pets = new HashSet<Pets>();
            Reveiws = new HashSet<Reveiws>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int Id { get; set; }
        public string Userid { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Pno { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Pets> Pets { get; set; }
        public virtual ICollection<Reveiws> Reveiws { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }
    }
}
