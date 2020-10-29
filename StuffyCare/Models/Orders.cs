using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Orders
    {
        public int Id { get; set; }
        public string Orderid { get; set; }
        public string Userid { get; set; }
        public string Itemid { get; set; }
        public DateTime? Dt { get; set; }
        public int? Quantity { get; set; }
        public string Status { get; set; }
        public string Method { get; set; }
        public double? Total { get; set; }

        public virtual Items Item { get; set; }
        public virtual Users User { get; set; }
    }
}
