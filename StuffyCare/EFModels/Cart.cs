using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Cart
    {
        public string Userid { get; set; }
        public string Itemid { get; set; }
        public int? Quantity { get; set; }

        public virtual Items Item { get; set; }
        public virtual Users User { get; set; }
    }
}
