using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Appointments
    {
        public int Id { get; set; }
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
        public bool? Ishomeservice { get; set; }
        public bool? Ispaid { get; set; }

        public virtual Pets Pet { get; set; }
        public virtual Users User { get; set; }
        public virtual Vendors Vendor { get; set; }
    }
}
