using System;
using System.Collections.Generic;

namespace StuffyCare.ApiModels
{
    public partial class Users
    {
        public Users()
        {
            Appointments = new HashSet<Appointments>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Userid { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Pno { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
