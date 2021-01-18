using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.ShiprocketModels
{
    public class OrderItems
    {
        public string name { get; set; }
        public string sku { get; set; }
        public int  units { get; set; }
        public int selling_price { get; set; }

    }
}
