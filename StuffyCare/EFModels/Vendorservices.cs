using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Vendorservices
    {
        public int Id { get; set; }
        public string Vendorid { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }

        public virtual Vendors Vendor { get; set; }
    }
}
