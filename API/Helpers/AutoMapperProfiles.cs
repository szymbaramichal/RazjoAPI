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
            CreateMap<RegisterUserDTO, User>();
            CreateMap<AddCalendarNoteDTO, CalendarNote>();
            CreateMap<CalendarNote, ReturnCalendarNoteDTO>();
            CreateMap<Family, ReturnFamilyDTO>();
            CreateMap<User, UserInfoDTO>();
            CreateMap<ReturnPrivateNoteDTO, PrivateNote>();
            CreateMap<PrivateNote, ReturnPrivateNoteDTO>();
        }
    }
}