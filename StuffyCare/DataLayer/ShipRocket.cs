
using Newtonsoft.Json;
using StuffyCare.DataLayer.ShiprocketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StuffyCare.Controllers
{
    public class ShipRocket
    {
        
        public string GetToken()
        {
            var str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://apiv2.shiprocket.in/v1/external/auth/login");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var user = new ShipRocketUser();
                user.email="chandangowda.keelara@gmail.com";
                user.password = "Chandan@1998";
                var json = JsonConvert.SerializeObject(user);
                HttpResponseMessage response = client.PostAsync("",new StringContent(json,Encoding.UTF8,"application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultmessage = response.Content.ReadAsStringAsync().Result;
                    var tester = JsonConvert.DeserializeObject<dynamic>(resultmessage);
                    str = tester.token;
                }
            }
            return str;
        }
        public string PlaceOrder( ShipRocketOrder order)
        {
            var str = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.BaseAddress = new Uri("https://apiv2.shiprocket.in/v1/external/orders/create/adhoc");
                order.order_id = "O10001";
                order.order_date = "2020-11-04";
                order.pickup_location = "Primary";
                order.billing_customer_name = "chandan";
                order.billing_last_name = "Gowda";
                order.billing_address = "my address 1";
                order.billing_address_2 = "my address 2";
                order.billing_city = "Bangalore";
                order.billing_pincode = "560018";
                order.billing_state = "Karnataka";
                order.billing_country = "India";
                order.billing_email = "chandan@something.com";
                order.billing_phone = "9945583998";
                order.shipping_is_billing = true;
                var obj = new OrderItems();
                obj.name = "my item";
                obj.sku = "item sku";
                obj.units = 10;
                obj.selling_price = 100;
                var listobj = new List<OrderItems>();
                listobj.Add(obj);
                order.order_items=listobj;
                order.payment_method = "prepaid";
                order.sub_total = 9999;
                order.length = 10;
                order.breadth = 10;
                order.height = 10;
                order.weight = 2;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken());
                var json = JsonConvert.SerializeObject(order);
                HttpResponseMessage response = client.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                str = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultmessage = response.Content.ReadAsStringAsync().Result;
                    var tester = JsonConvert.DeserializeObject<dynamic>(resultmessage);
                    str = resultmessage;
                }
            }
            return str;



        }

    }
}
