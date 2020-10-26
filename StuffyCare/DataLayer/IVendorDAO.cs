using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.VendorDAO
{
    public interface IVendorDAO
    {
        public string Auth(string email, string pass);
    
        public string AddVendor(string email, string pass, string pno);
        
        
    }
}
