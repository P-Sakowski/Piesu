using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Piesu.API.Data;
using Piesu.API.Entities;
using Piesu.API.Models;
using System.Linq;

namespace Piesu.API.Mappings
{
    public class DogAutoMapperProfile : Profile
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public DogAutoMapperProfile(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;

            CreateMap<DogEntity, Dog>()
                .ForMember(destination => destination.Owner,
                opt => opt.MapFrom(source => _userManager.FindByIdAsync(source.UserId).Result))
                .ForMember(destination => destination.Breed,
                opt => opt.MapFrom(source => _dbContext.Breeds.Where(b => b.Id == source.BreedId).FirstOrDefault()));

            CreateMap<Dog, DogEntity>()
                .ForMember(destination => destination.UserId,
                opt => opt.MapFrom(source => source.Owner.Id))
                .ForMember(destination => destination.BreedId,
                opt => opt.MapFrom(source => source.Breed.Id));

            CreateMap<Dog, DogDto>()
                .ForMember(destination => destination.Breed,
                opt => opt.MapFrom(source => source.Breed));
        }
    }
}
