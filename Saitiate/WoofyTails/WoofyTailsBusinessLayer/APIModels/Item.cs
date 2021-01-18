using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WoofyTailsBusinessLayer.APIModels
{
    public partial class Item
    {
        public string Itemid { get; set; }
        [Required, MaxLength(100)]
        // Allow up to 100 uppercase and lowercase 
       
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Special Characters are not allowed.")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public string Subdescription { get; set; }
        [Required]
        public string Foranimal { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public double? Price { get; set; }
        public double? Saleprice { get; set; }
        public string Sku { get; set; }
        public int? Quantity { get; set; }
        public int? Moa { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string Addedby { get; set; }
        public string Photo { get; set; }
        public double? Length { get; set; }
        public double? Breadth { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string Shippingclass { get; set; }
        public string Processingtime { get; set; }
        public string Mililitres { get; set; }
        public string Packsizeingrams { get; set; }
        public int? Unitcount { get; set; }
        public string Upsells { get; set; }
        public string Crosssells { get; set; }
        public string Policylabel { get; set; }
        public string Shippingpolicy { get; set; }
        public string Refundpolicy { get; set; }
        public string Cancelationpolicy { get; set; }
        public string Exchangepolicy { get; set; }
        public string Storename { get; set; }
        public string Commissionfor { get; set; }
        public string Commissionmode { get; set; }
        public string Authorizedby { get; set; }
        public string Authorizedstatus { get; set; }
        public byte? Deletedstatus { get; set; }
    }
}
