using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StuffyCare.Models
{
    
    public class Appointments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Aptid { get; set; }
        public string Email { get; set; }
        public string Pno { get; set; }
        public string Dt { get; set; }
        public string Tm { get; set; }
        public string ServiceType { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public Appointments()
        {

        }
        public Appointments(int aptid,string email,string pno,string dt,string tm,string servicetype,string address,string message)
        {
            this.Aptid = aptid;
            this.Email = email;
            this.Pno = pno;
            this.Dt = dt;
            this.Tm = tm;
            this.ServiceType = servicetype;
            this.Address = address;
            this.Message = message;
        }
        


    }
}
