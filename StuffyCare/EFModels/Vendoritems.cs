﻿using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Vendoritems
    {
        public int Id { get; set; }
        public string Itemid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
        public string Sku { get; set; }
        public double? Saleprice { get; set; }
        public int? Quantity { get; set; }
        public int? Moa { get; set; }
        public string Own { get; set; }
        public string Photo { get; set; }
        public int? Length { get; set; }
        public int? Breadth { get; set; }
        public int? Height { get; set; }
        public double? Weight { get; set; }
    }
}
