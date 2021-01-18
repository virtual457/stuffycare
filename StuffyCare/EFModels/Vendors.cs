using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Vendors
    {
        public Vendors()
        {
            Appointments = new HashSet<Appointments>();
            Vendorservices = new HashSet<Vendorservices>();
        }

        public int Id { get; set; }
        public string Vendorid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Pno { get; set; }
        public string Gender { get; set; }
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
        public bool? Issellingitem { get; set; }
        public bool? Homeservice { get; set; }
        public bool? Isdeleted { get; set; }
        public bool? Isauthorized { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Vendorservices> Vendorservices { get; set; }
    }
}
