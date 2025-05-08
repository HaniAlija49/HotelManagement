namespace HotelManagement.DTOs.Responses
{
    public class HotelDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public List<string> Amenities { get; set; }
        public double Rating { get; set; }
    }
}
