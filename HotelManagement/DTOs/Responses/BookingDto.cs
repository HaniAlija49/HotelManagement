using HotelManagement.Models;

namespace HotelManagement.DTOs.Responses
{
    public class BookingDto
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
