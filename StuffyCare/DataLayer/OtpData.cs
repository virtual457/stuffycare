using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace StuffyCare.DataLayer
{
    public class OtpData
    {
        public string GetOtp()
        {
            Random rnd = new Random();
            var otp = (rnd.Next(100000, 999999)).ToString();
            return otp;
        }
        public string SendMsg(string phoneNo,string msg)
        {
            string url = "http://login.bulksmsgateway.in/sendmessage.php";
            string message = msg;
           
            String strPost = "?user=" + HttpUtility.UrlPathEncode("Stuffycare") + "&password=" + HttpUtility.UrlPathEncode("Stuffy@123") + "&sender=" + HttpUtility.UrlPathEncode("AIRSMS") + "&mobile=" + HttpUtility.UrlPathEncode(phoneNo) + "&type=" + HttpUtility.UrlPathEncode("3") + "&message=" + message;

            string content = "";
            using (var client = new HttpClient())
            {

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync(url + strPost, new StringContent(content, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultmessage = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<dynamic>(resultmessage);
                    if (result.status == "success")
                    {
                        return "Message sent sucessfully";
                    }
                    else
                    {
                        return "Could not send message from message api";
                    }
                }
            }
            return "Message could not be sent";
        }
    }
}