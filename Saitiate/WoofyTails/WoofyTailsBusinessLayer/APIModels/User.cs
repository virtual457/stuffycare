using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WoofyTailsBusinessLayer.APIModels
{
    public class User
    {
        [Required]
        public string UserId { get; set; }

        [Required, MaxLength(100)]
        // Allow up to 100 uppercase and lowercase 
        // characters. Use custom error.
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$",
         ErrorMessage = "Special Characters are not allowed.")]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$",
         ErrorMessage = "Special Characters are not allowed.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Enter Proper EmailAddress")]
        public string EmailId { get; set; }

        [Required]
        [StringLength(100,MinimumLength =8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        
        public string Gender { get; set; }

        [DataType(DataType.PhoneNumber,ErrorMessage ="Enter proper phone number")]
        
        public string PhoneNumber { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [Range(1,4,ErrorMessage ="Role Id can be 1-4(Admin-User)")]
        public int? RoleId { get; set; }

    }
}
