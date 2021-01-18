using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Services
    {
        public int Id { get; set; }
        public string Vendorid { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        
    }
}
