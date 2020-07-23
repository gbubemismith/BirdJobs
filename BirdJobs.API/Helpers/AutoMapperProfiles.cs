using AutoMapper;
using BirdJobs.API.Dtos;
using BirdJobs.API.Models;

namespace BirdJobs.API.Helpers
{
    public class AutoMapperProfiles  : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserModelDto, User>()
                .ForMember(dest => dest.UserId, opt => 
                    opt.MapFrom(src => int.Parse(src.UserId)));
                
        }
    }
}