using StuffyCare.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.VendorDAO
{
    public interface IVendorDAO
    {
         string Auth(string email, string pass);
         Vendors GetVendor(string vendorid);
         string AddVendor(Vendors vendor);

        string VendorAddItem(Items item);
        string GetVendorId(string emailorphone);
        List<Vendorservices> GetServices(string vendorid);
        string AddServices(Vendorservices services);
        string DelServices(Vendorservices services);
        string UpdateServices(Vendorservices services);
        List<Vendorservices> GetVendorByServices(string name);
        List<Appointments> GetVendorAppointments(string vendorid);
    }
}
