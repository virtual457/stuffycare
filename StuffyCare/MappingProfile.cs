using AutoMapper;
using StuffyCare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Threading.Tasks;

namespace StuffyCare
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Admins, ApiModels.Admins>();
            CreateMap<Users, ApiModels.Users>();
            CreateMap<Vendors, ApiModels.Vendors>();
            CreateMap<Items, ApiModels.Items>();
            CreateMap<Orders, ApiModels.Orders>();
            CreateMap<Vendoritems, ApiModels.Vendoritems>();
            CreateMap<Authvendors, ApiModels.Authvendors>();
            CreateMap<Appointments, ApiModels.Appointments>();




        }
    }
}
