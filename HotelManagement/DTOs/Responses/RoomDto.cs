namespace HotelManagement.DTOs.Responses
{
    public class RoomDto
    {
        public string Id { get; set; }
        public string HotelId { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public List<string> Amenities { get; set; }
        public bool IsAvailable { get; set; }
    }
}
