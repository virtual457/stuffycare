using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.ShiprocketModels
{
    public class ShipRocketOrder
    {
        public string order_id { get; set; }
        public string order_date { get; set; }
        public string pickup_location { get; set; }
        public string billing_customer_name { get; set; }
        public string  billing_last_name { get; set; }
        public string billing_address { get; set; }
        public string billing_address_2 { get; set; }
        public string billing_city { get; set; }
        public string billing_pincode { get; set; }
        public string billing_state { get; set; }
        public string billing_country { get; set; }
        public string billing_email { get; set; }
        public string billing_phone { get; set; }
        public Boolean shipping_is_billing { get; set; }
        public List<OrderItems> order_items { get; set; }

        public string payment_method { get; set; }
        public int sub_total { get; set; }
        public int length { get; set; }
        public int breadth { get; set; }
        public int height { get; set; }
        public float weight { get; set; }
    }
}
