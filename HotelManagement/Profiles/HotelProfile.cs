using AutoMapper;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Models;

namespace HotelManagement.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<CreateHotelDto, Hotel>();
        }
    }
}
