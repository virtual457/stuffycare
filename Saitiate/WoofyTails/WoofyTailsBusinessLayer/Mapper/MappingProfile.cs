using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoofyTailsDALLayer.EFModels;

namespace WoofyTailsBusinessLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, APIModels.User>();
            CreateMap<Role, APIModels.Role>();
            CreateMap<Pet, APIModels.Pet>();
            CreateMap<Vendor, APIModels.Vendor>();
            CreateMap<Vendorservice, APIModels.Vendorservice>();
            CreateMap<Appointment, APIModels.Appointment>();
            CreateMap<Item, APIModels.Item>();
            //Other way around
            CreateMap<APIModels.Item, Item>();
            CreateMap<APIModels.Appointment, Appointment>();
            CreateMap<APIModels.Vendor, Vendor>();
            CreateMap<APIModels.Vendorservice, Vendorservice>();
            CreateMap<APIModels.Pet, Pet>();
            CreateMap<APIModels.Role, Role>();
            CreateMap<APIModels.User, User>();
        }
    }
}
