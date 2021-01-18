using System;
using System.Collections.Generic;

#nullable disable

namespace WoofyTailsDALLayer.EFModels
{
    public partial class Appointment
    {
        public string Aptid { get; set; }
        public string Userid { get; set; }
        public string Petid { get; set; }
        public string Phonenumber { get; set; }
        public string Vendorid { get; set; }
        public string Category { get; set; }
        public DateTime? Servicedatetime { get; set; }
        public double? Servicefees { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public byte? Ishomeservice { get; set; }
        public byte? Ispaid { get; set; }

        public virtual Pet Pet { get; set; }
        public virtual User User { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
