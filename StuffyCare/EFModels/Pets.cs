using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Pets
    {
        public Pets()
        {
            Appointments = new HashSet<Appointments>();
        }

        public int Id { get; set; }
        public string Petid { get; set; }
        public string Userid { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}
