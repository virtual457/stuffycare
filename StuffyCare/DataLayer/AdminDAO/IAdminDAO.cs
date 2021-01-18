
using StuffyCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StuffyCare.EFModels;

namespace StuffyCare.DataLayer.AdminDAO
{
    public interface IAdminDAO
    {
        string Auth(string email, string pass);
         List<EFModels.Users> GetUser(string email);
        List<EFModels.Vendors> GetVendors(string vendorid,string category,string city);
        string AddAdmin(string email, string pass, string pno);
         List<EFModels.Appointments> GetAllAppointments(string category);
         string AddAppointments(EFModels.Appointments appointments);
         string AddItem(EFModels.Items item);
         List<EFModels.Items> GetItem(string itemid,string foranimal,string category,string subcategory,string name);
         List<EFModels.Orders> GetOrders(string vendorid);
        List<EFModels.Orders> GetRecentOrders(int num);
        string AuthVendor(string vendorid);
     
        List<EFModels.Vendors> GetAllVendorIdRequests();
        List<EFModels.Items> GetAllVendorItemsByVendorId(string vendorid);
        string AuthVendorItem(string itemid, string adminid);
        string VerifyOtp(string phonenum, string otp);
        string AddOtp(string phonenum, string otp);
        string GetAdminId(string emailorphone);
        string LoginbyOTP(string phone, string otp);
    }
}
