using AutoMapper;
using Piesu.API.Data;
using Piesu.API.Models;

namespace Piesu.API.Mappings
{
    public class SubcategoryAutoMapperProfile : Profile
    {
        private readonly ApplicationDbContext _dbContext;
        public SubcategoryAutoMapperProfile(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;

            CreateMap<SubcategoryDetails, Subcategory>()
                .ForMember(destination => destination.Category,
                opt => opt.MapFrom(source => _dbContext.Categories.Find(source.CategoryId)));
            CreateMap<Subcategory, SubcategoryDetails>();
            CreateMap<Subcategory, SubcategoryDto>();
        }
    }
}
