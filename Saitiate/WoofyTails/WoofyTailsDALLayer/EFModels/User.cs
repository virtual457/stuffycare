using System;
using System.Collections.Generic;

#nullable disable

namespace WoofyTailsDALLayer.EFModels
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            Items = new HashSet<Item>();
            Pets = new HashSet<Pet>();
        }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public int? RoleId { get; set; }
        public int? IsDeleted { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
