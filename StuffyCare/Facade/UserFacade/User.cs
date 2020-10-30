using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StuffyCare.DataLayer;
using StuffyCare.EFModels;

namespace StuffyCare.Facade
{
    public class User
    {
        private readonly DataLayer.UserDAO.IUserDAO UserDao = DataAccess.UserDAO;

        public string Auth(string email, string pass)
        {
            return UserDao.Auth(email, pass);
        }
        public string Create(string email, string pass, string pno)
        {
            return UserDao.AddUser(email, pass, pno);
        }
        public List<Orders> GetOrders(string userid)
        {
            return UserDao.GetOrder(userid);
        }
        public Users GetUser(string emailid)
        {
            return UserDao.GetUser(emailid);
        }

        public string AddAppointments(Appointments userid)
        {
            return UserDao.AddAppointments(userid);
        }
        public List<Appointments> GetAppointments(string userid, string petid)
        {
            return UserDao.GetAppointments(userid, petid);
        }
    }
}
