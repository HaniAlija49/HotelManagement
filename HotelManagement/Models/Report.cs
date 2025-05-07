using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HotelManagement.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("hotelId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HotelId { get; set; }

        [BsonElement("generatedAt")]
        public DateTime GeneratedAt { get; set; }

        [BsonElement("totalBookings")]
        public int TotalBookings { get; set; }

        [BsonElement("totalRevenue")]
        public decimal TotalRevenue { get; set; }

        [BsonElement("bookingsByRoomType")]
        public Dictionary<string, int> BookingsByRoomType { get; set; }
    }
}
