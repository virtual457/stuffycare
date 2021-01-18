using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace StuffyCare.DataLayer
{
    public class Email
    {
        public string SendEmail(string to,string subject,string msg)
        {
            string status = "could not send email";
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("Hello@stuffycare.com");
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = false; //to make message body as html  
                message.Body = msg;
                smtp.Port = 80;
                smtp.Host = "smtpout.asia.secureserver.net"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("Hello@stuffycare.com", "Stuffy@123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

            }
            catch (Exception e) 
            {
                status = e.Message;
            }
            return status;
        }
    }
}