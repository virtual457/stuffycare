using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StuffyCare.DataLayer;
using StuffyCare.Models;

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
        public Users Get(string email)
        {
            return UserDao.GetUser(email);
        }
    }
}
