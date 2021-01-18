using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StuffyCare.Models
{
    public class orderRazorpay
    {
            public string orderId { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string mobileNumber { get; set; }
            public string address { get; set; }
        
    }
}