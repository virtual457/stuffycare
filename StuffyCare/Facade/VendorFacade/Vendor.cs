using StuffyCare.DataLayer;

namespace StuffyCare.Facade
{
    public class Vendor
    {
        private readonly DataLayer.VendorDAO.IVendorDAO VendorDao = DataAccess.VendorDAO;
        public string AuthVendor(string email, string pass)
        {
            return VendorDao.Auth(email, pass);
        }
        public string Create(string email, string pass, string pno)
        {
            return VendorDao.AddVendor(email, pass, pno);
        }
        
    }
}

