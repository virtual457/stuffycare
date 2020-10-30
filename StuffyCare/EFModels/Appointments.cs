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
        public string Pno { get; set; }
        public DateTime? Dt { get; set; }
        public string Servicetype { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }

        public virtual Pets Pet { get; set; }
        public virtual Users User { get; set; }
        public Appointments()
        {

        }
        public Appointments(string aptid, string userid, string pno, string dt, string servicetype, string address, string message)
        {
            this.Aptid = aptid;
            this.Userid = userid;
            this.Pno = pno;
            this.Dt = Convert.ToDateTime(dt);
            this.Servicetype = servicetype;
            this.Address = address;
            this.Message = message;
        }

    }
}
