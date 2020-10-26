using StuffyCare.Facade;
using StuffyCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.UserDAO
{
    public interface IUserDAO
    {
        public string Auth(string email, string pass);
        public Users GetUser(string email);
        public string AddUser(string email,string pass, string pno);
    }
}
