using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models
{
    public partial class Appointments
    {
        public int Id { get; set; }
        public string Aptid { get; set; }
        public string Userid { get; set; }
        public string Pno { get; set; }
        public DateTime? Dt { get; set; }
        public TimeSpan? Tm { get; set; }
        public string Servicetype { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }

        public virtual Users User { get; set; }
    }
}
