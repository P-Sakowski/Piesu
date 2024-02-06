using AutoMapper;
using Piesu.API.Entities;
using Piesu.API.Enums;
using Piesu.API.Models;
using System;

namespace Piesu.API.Mappings
{
    public class BreedAutoMapperProfile : Profile
    {

        public BreedAutoMapperProfile()
        {

            CreateMap<BreedEntity, Breed>()
                .ForMember(destination => destination.PeopleAttitude,
                opt => opt.MapFrom(source => Enum.GetName(typeof(Attitude), source.PeopleAttitude)))
                .ForMember(destination => destination.AnimalsAttitude,
                opt => opt.MapFrom(source => Enum.GetName(typeof(Attitude), source.AnimalsAttitude)));

            CreateMap<Breed, BreedEntity>()
                .ForMember(destination => destination.PeopleAttitude,
                opt => opt.MapFrom(source => (short)source.PeopleAttitude))
                .ForMember(destination => destination.AnimalsAttitude,
                opt => opt.MapFrom(source => (short)source.AnimalsAttitude));

        }
    }
}
