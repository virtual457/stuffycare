using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public string Itemid { get; set; }
        public int? Quantity { get; set; }

    }
}
