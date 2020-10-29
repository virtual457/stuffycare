using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuffyCare.ApiModels
{
    public partial class Admins
    {
        public int Id { get; set; }
        public string Adminid { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Pno { get; set; }
    }
}
