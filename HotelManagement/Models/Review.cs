using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HotelManagement.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("hotelId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HotelId { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("rating")]
        public int Rating { get; set; }

        [BsonElement("comment")]
        public string Comment { get; set; }

        [BsonElement("createdAtDate")]
        public DateTime CreatedAt { get; set; }
    }
}
