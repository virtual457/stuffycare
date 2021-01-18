using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StuffyCare.Models
{
    public partial class Authvendors
    {
        public int Id { get; set; }
        public string Authvendorsid { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Pno { get; set; }
    }
}
