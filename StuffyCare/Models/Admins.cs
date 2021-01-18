using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StuffyCare.Models
{
    public partial class Admins
    {
        public int Id { get; set; }
        public string Adminid { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string Pass { get; set; }
        
        [DataType(DataType.PhoneNumber,ErrorMessage ="Phone Number not in correct format")]
        public string Pno { get; set; }
    }
}
