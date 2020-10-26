using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using StuffyCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare.DataLayer.AdminDAO
{
    public interface IAdminDAO
    {
        public string Auth(string email, string pass);
        public List<Users> GetUser(string email);
        public string AddAdmin(string email, string pass, string pno);
        public List<Appointments> GetAllAppointments(string category);
        public string AddAppointments(Appointments appointments);
        public string AddItem(Items item);
        public List<Items> GetItem(string itemid);
    }
}
