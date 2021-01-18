using StuffyCare.DataLayer;
using StuffyCare.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.Facade
{
    public class Admin
    {
        private readonly DataLayer.AdminDAO.IAdminDAO AdminDao = DataAccess.AdminDAO;
        public string Auth(string email, string pass)
        {
            return AdminDao.Auth(email, pass);
        }
        public string Create(string email, string pass, string pno)
        {
            try
            {
                return AdminDao.AddAdmin(email, pass, pno);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<Users> GetUser(string email)
        {
            return AdminDao.GetUser(email);
        }
        public List<Vendors> GetVendor(string vendorid,string category,string city)
        {
            return AdminDao.GetVendors(vendorid,category,city);
        }
        public List<Appointments> GetAppointments(string category)
        {
            return AdminDao.GetAllAppointments(category);
        }
        public string AddAppointments(Appointments appointments)
        {
            return AdminDao.AddAppointments(appointments);
        }
        public string AddItem(Items items)
        {
            return AdminDao.AddItem(items);
        }
        public List<Items> GetItems(string itemid,string foranimal,string category,string subcategory,string name)
        {
            return AdminDao.GetItem(itemid, foranimal, category, subcategory,name);
        }
        public List<Orders> GetOrders(string vendorid)
        {
            return AdminDao.GetOrders(vendorid);
        }
        public string AuthVendor(string vendorid)
        {
            return AdminDao.AuthVendor(vendorid);
        }
        public List<Vendors> GetAllVendorsIdRequests()
        {
            return AdminDao.GetAllVendorIdRequests();
        }
        public List<Items> GetVendoritemsByVendorId(string vendorid)
        {
            return AdminDao.GetAllVendorItemsByVendorId(vendorid);
        }
        public string AuthVendorItem(string itemid,string adminid)
        {
            return AdminDao.AuthVendorItem(itemid, adminid);
        }
        public List<Orders> GetRecentOrders(int num)
        {
            return AdminDao.GetRecentOrders(num);
        }
        public string AddOtp(string phonenum, string otp)
        {
            return AdminDao.AddOtp(phonenum,otp);
        }
        public string VerifyOtp(string phonenum, string otp)
        {
            return AdminDao.VerifyOtp(phonenum,otp);
        }
        public string GetAdminId(string emailorphone)
        {
            return AdminDao.GetAdminId(emailorphone);
        }
        public string LoginByOTP(string phonenum, string pass)
        {
            try
            {
                return AdminDao.LoginbyOTP(phonenum, pass);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
