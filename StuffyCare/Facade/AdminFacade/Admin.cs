using StuffyCare.DataLayer;
using StuffyCare.Models;
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
            return AdminDao.AddAdmin(email, pass, pno);
        }
        public List<Users> GetUser(string email)
        {
            return AdminDao.GetUser(email);
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
        public List<Items> GetItems(string itemid)
        {
            return AdminDao.GetItem(itemid);
        }
    }
}
