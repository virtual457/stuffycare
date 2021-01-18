using System;
using System.Collections.Generic;

#nullable disable

namespace WoofyTailsDALLayer.EFModels
{
    public partial class Pet
    {
        public Pet()
        {
            Appointments = new HashSet<Appointment>();
        }

        public string Petid { get; set; }
        public string Userid { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public string Allergies { get; set; }
        public double? Age { get; set; }
        public string Moreinfo { get; set; }
        public byte? Isdeleted { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
