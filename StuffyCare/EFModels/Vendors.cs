using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Vendors
    {
        public Vendors()
        {
            Vendoritems = new HashSet<Vendoritems>();
        }

        public int Id { get; set; }
        public string Vendorid { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Pno { get; set; }

        public virtual ICollection<Vendoritems> Vendoritems { get; set; }
    }
}
