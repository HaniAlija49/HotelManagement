using AutoMapper;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Models;

namespace HotelManagement.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<CreateReportDto, Report>()
                .ForMember(dest => dest.GeneratedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}