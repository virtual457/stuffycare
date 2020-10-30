using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Wishlist
    {
        public string Userid { get; set; }
        public string Itemid { get; set; }

        public virtual Items Item { get; set; }
        public virtual Users User { get; set; }
    }
}
