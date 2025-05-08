using AutoMapper;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Models;

namespace HotelManagement.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<CreateRoomDto, Room>();
        }
    }
}
