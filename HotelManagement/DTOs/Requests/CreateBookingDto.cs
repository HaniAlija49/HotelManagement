using HotelManagement.Models;

namespace HotelManagement.DTOs.Requests
{
    public class CreateBookingDto
    {
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
    }
}
