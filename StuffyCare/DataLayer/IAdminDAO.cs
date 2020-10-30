using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
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
        public string Auth(string email, string pass);
        public List<EFModels.Users> GetUser(string email);
        public string AddAdmin(string email, string pass, string pno);
        public List<EFModels.Appointments> GetAllAppointments(string category);
        public string AddAppointments(EFModels.Appointments appointments);
        public string AddItem(EFModels.Items item);
        public List<EFModels.Items> GetItem(string itemid);
        public List<EFModels.Orders> GetOrders(string vendorid);

        public List<EFModels.Orders> RecentOrders(int num);
    }
}
