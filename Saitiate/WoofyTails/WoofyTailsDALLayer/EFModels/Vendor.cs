using System;
using System.Collections.Generic;

#nullable disable

namespace WoofyTailsDALLayer.EFModels
{
    public partial class Vendor
    {
        public Vendor()
        {
            Appointments = new HashSet<Appointment>();
            Items = new HashSet<Item>();
            Vendorservices = new HashSet<Vendorservice>();
        }

        public string Vendorid { get; set; }
        public string Description { get; set; }
        public string StoreName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public double? Yearsofexperience { get; set; }
        public string Monfrom { get; set; }
        public string Monto { get; set; }
        public string Tuefrom { get; set; }
        public string Tueto { get; set; }
        public string Wedfrom { get; set; }
        public string Wedto { get; set; }
        public string Thurfrom { get; set; }
        public string Thurto { get; set; }
        public string Frifrom { get; set; }
        public string Frito { get; set; }
        public string Satfrom { get; set; }
        public string Satto { get; set; }
        public string Sunfrom { get; set; }
        public string Sunto { get; set; }
        public string Photo { get; set; }
        public string Photoidproof { get; set; }
        public string Authorizedby { get; set; }
        public string Authorizedstatus { get; set; }
        public byte? Issellingitem { get; set; }
        public byte? Homeservice { get; set; }
        public byte? Isdeleted { get; set; }
        public byte? Isauthorized { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Vendorservice> Vendorservices { get; set; }
    }
}
