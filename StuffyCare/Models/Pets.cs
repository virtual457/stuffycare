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
        public DateTime? Dob { get; set; }


    }
}
