using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
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
