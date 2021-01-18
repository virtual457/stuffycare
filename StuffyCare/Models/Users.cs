using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StuffyCare.Models
{
    public partial class Users
    {


        public int Id { get; set; }
        public string Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Pno { get; set; }
        public string Image { get; set; }
        public int? LoyaltyPoints { get; set; }

    }
}
