using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WoofyTailsBusinessLayer.APIModels
{
    public partial class Appointment
    {
        public string Aptid { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string Userid { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string Petid { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Enter proper phone number")]
        public string Phonenumber { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string Vendorid { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [DataType(DataType.DateTime,ErrorMessage ="Enter Proper format of Datetime for ServiceDateTime")]
        public DateTime? Servicedatetime { get; set; }
        [Required]
        public double? Servicefees { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public bool? Ishomeservice { get; set; }
        [Required]
        public bool? Ispaid { get; set; }
    }
}
