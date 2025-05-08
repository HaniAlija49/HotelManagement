namespace HotelManagement.DTOs.Responses
{
    public class ReportDto
    {
        public string Id { get; set; }
        public string HotelId { get; set; }
        public DateTime GeneratedAt { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public Dictionary<string, int> BookingsByRoomType { get; set; }
    }
}
