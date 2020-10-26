using StuffyCare.DataLayer.AdminDAO;
using StuffyCare.DataLayer.UserDAO;
using StuffyCare.DataLayer.VendorDAO;

namespace StuffyCare.DataLayer
{
    public class DataAccess
    {
        public static IVendorDAO VendorDAO
        {
            get
            {
                return new VendorDAOImpl();
            }
        }
        public static IUserDAO UserDAO
        {
            get
            {
                return new UserDAOImpl();
            }
        }
        public static IAdminDAO AdminDAO
        {
            get
            {
                return new AdminDAOImpl();
            }
        }
    }
}
