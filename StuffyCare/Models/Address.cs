using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Address
    {
        public int Id { get; set; }
        public string Addressid { get; set; }
        public string Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Addresslineone { get; set; }
        public string Addresslinetwo { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? Isshippingaddress { get; set; }
        public bool? Isdeleted { get; set; }
    }
}
