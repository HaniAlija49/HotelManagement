using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HotelManagement.Models
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("hotelId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HotelId { get; set; }

        [BsonElement("roomNumber")]
        public string RoomNumber { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("amenities")]
        public List<string> Amenities { get; set; }

        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }
    }
}
