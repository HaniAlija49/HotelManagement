namespace HotelManagement.DTOs.Requests
{
    public class CreateReviewRequest
    {
        public string HotelId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1 to 5
    }
}