using System;
using System.Collections.Generic;

namespace StuffyCare.Models
{
    public partial class Items
    {
        public int Id { get; set; }
        public string Itemid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subdescription { get; set; }
        public string Foranimal { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public double? Price { get; set; }
        public double? Saleprice { get; set; }
        public string Sku { get; set; }
        public int? Quantity { get; set; }
        public int? Moa { get; set; }
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
        public string Deletedstatus { get; set; }

    }
}
