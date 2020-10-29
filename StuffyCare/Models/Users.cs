using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Users
    {
        public Users()
        {
            Appointments = new HashSet<Appointments>();
            Orders = new HashSet<Orders>();
            Reveiws = new HashSet<Reveiws>();
        }

        public int Id { get; set; }
        public string Userid { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Pno { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Reveiws> Reveiws { get; set; }
    }
}
