using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Pets
    {
        public int Id { get; set; }
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
        public bool? Isdeleted { get; set; }
    }
}
