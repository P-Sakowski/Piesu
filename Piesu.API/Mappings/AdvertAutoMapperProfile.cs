using AutoMapper;
using Piesu.API.Data;
using Piesu.API.Entities;
using Piesu.API.Enums;
using Piesu.API.Models;
using System;
using System.Linq;

namespace Piesu.API.Mappings
{
    public class AdvertAutoMapperProfile : Profile
    {
        private readonly ApplicationDbContext _dbContext;
        public AdvertAutoMapperProfile(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;

            CreateMap<Advert, AdvertDto>()
                .ForMember(destination => destination.Category,
                opt => opt.MapFrom(source => _dbContext.Categories.Where(c => c.Id == source.CategoryId).FirstOrDefault()))
                .ForMember(destination => destination.Subcategory,
                opt => opt.MapFrom(source => _dbContext.Subcategories.Where(s => s.Id == source.SubcategoryId).FirstOrDefault()));

        }
    }
}
