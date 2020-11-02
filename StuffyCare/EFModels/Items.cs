using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Items
    {
        public Items()
        {
            Cart = new HashSet<Cart>();
            Orders = new HashSet<Orders>();
            Reveiws = new HashSet<Reveiws>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int Id { get; set; }
        public string Itemid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
        public string Sku { get; set; }
        public double? Saleprice { get; set; }
        public int? Quantity { get; set; }
        public int? Moa { get; set; }
        public string Own { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Reveiws> Reveiws { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }
    }
}
