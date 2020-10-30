using AutoMapper;
using StuffyCare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Threading.Tasks;
using StuffyCare.EFModels;

namespace StuffyCare
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<EFModels.Admins, Models.Admins>();
            CreateMap<EFModels.Users, Models.Users>();
            CreateMap<EFModels.Vendors, Models.Vendors>();
            CreateMap<EFModels.Items, Models.Items>();
            CreateMap<EFModels.Orders, Models.Orders>();
            CreateMap<EFModels.Vendoritems, Models.Vendoritems>();
            CreateMap<EFModels.Authvendors, Models.Authvendors>();
            CreateMap<EFModels.Appointments, Models.Appointments>();




        }
    }
}
