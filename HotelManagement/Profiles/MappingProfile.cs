using AutoMapper;
using HotelManagement.Models;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using Microsoft.AspNetCore.Identity.Data;

namespace HotelManagement.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOs.Requests.RegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); // populate roles manually if needed
        }
    }
}
