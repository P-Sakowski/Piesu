using AutoMapper;
using Piesu.API.Entities;
using Piesu.API.Models;

namespace Piesu.API.Mappings
{
    public class CategoryAutoMapperProfile : Profile
    {
        public CategoryAutoMapperProfile()
        {
            CreateMap<CategoryDetails, Category>();
            CreateMap<Category, CategoryDetails>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
