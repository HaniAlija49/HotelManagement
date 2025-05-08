namespace HotelManagement.DTOs.Requests
{
    public class CreateReportDto
    {
        public string HotelId { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public Dictionary<string, int> BookingsByRoomType { get; set; }
    }
}
