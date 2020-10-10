using AutoMapper;
using DTOs;
using Models;

namespace Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddValuesDTO, Value>();
            CreateMap<RegisterSingleUserDTO, User>();
        }
    }
}