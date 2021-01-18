using AutoMapper;
using StuffyCare.Models;
using System;
using System.Collections.Generic;
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
            CreateMap<EFModels.Pets, Models.Pets>();
            CreateMap<EFModels.Reveiws, Models.Reveiws>();
            CreateMap<EFModels.Cart, Models.Cart>();
            CreateMap<EFModels.Wishlist, Models.Wishlist>();
            //other way around
            CreateMap<Models.Admins, EFModels.Admins>();
            CreateMap<Models.Users, EFModels.Users>();
            CreateMap<Models.Vendors, EFModels.Vendors>();
            CreateMap<Models.Items, EFModels.Items>();
            CreateMap<Models.Orders, EFModels.Orders>();
            CreateMap<Models.Vendoritems, EFModels.Vendoritems>();
            CreateMap<Models.Authvendors, EFModels.Authvendors>();
            CreateMap<Models.Appointments, EFModels.Appointments>();
            CreateMap<Models.Pets, EFModels.Pets>();
            CreateMap<Models.Reveiws, EFModels.Reveiws>();
            CreateMap<Models.Cart, EFModels.Cart>();
            CreateMap<Models.Wishlist, EFModels.Wishlist>();

        }
    }
}
