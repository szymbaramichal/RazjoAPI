using AutoMapper;
using API.DTOs;
using API.Models;

namespace API.Helpers
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