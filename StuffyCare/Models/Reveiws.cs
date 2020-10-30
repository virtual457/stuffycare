using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Reveiws
    {
        public int Id { get; set; }
        public string Reveiwid { get; set; }
        public string Userid { get; set; }
        public string Itemid { get; set; }
        public DateTime? Dt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Stars { get; set; }

 
    }
}
