using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.Models
{
    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Itemid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        public string Sku   { get; set; }
        public float Saleprice { get; set; }
        public int Quantity { get; set; }
        public int Moa { get; set; }
        public string Photo { get; set; }
        public Items()
        {

        }
        public Items(int itemid, string name, string description, string category,float price , string sku, float saleprice,int quantity,int moa,string photo)
        {
            this.Itemid = itemid;
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Price = price;
            this.Sku = sku;
            this.Saleprice = saleprice;
            this.Quantity = quantity;
            this.Moa = moa;
            this.Photo=photo;
        }
    }
}
