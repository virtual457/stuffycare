using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class BillingDetails
    {
        public int Id { get; set; }
        public string Reveiwid { get; set; }
        public string Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual Users User { get; set; }
    }
}
