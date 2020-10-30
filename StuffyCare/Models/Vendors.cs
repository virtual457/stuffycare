using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Vendors
    {
        public Vendors()
        {

        }

        public int Id { get; set; }
        public string Vendorid { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Pno { get; set; }

 
    }
}
