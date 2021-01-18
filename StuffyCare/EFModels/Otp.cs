using System;
using System.Collections.Generic;

namespace StuffyCare.EFModels
{
    public partial class Otp
    {
        public string Phoneno { get; set; }
        public string Otpstring { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
