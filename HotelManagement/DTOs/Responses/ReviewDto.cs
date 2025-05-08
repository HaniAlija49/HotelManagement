namespace HotelManagement.DTOs.Responses
{
    public class ReviewDto
    {
        public string Id { get; set; }
        public string HotelId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}