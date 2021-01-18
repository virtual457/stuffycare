using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WoofyTailsBusinessLayer.APIModels
{
    public partial class Pet
    {
       

        public string Petid { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string Userid { get; set; }
        [Required, MaxLength(100)]
        // Allow up to 100 uppercase and lowercase 
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Special Characters are not allowed.")]
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
