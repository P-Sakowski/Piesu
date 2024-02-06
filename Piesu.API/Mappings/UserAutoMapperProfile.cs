using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Piesu.API.Data;
using Piesu.API.Entities;
using Piesu.API.Enums;
using Piesu.API.Models;
using System;
using System.Linq;

namespace Piesu.API.Mappings
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
