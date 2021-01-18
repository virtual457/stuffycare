using System;
using System.Collections.Generic;

#nullable disable

namespace WoofyTailsDALLayer.EFModels
{
    public partial class Vendorservice
    {
        public int Id { get; set; }
        public string Vendorid { get; set; }
        public string Nameofservice { get; set; }
        public double? Price { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
