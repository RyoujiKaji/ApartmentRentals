using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApartmentRentals.Data.Models
{
    public abstract class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }

        [BsonElement("surname")]
        public string Surname { get; set; } = string.Empty;
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("middlename")]
        public string? MiddleName { get; set; } = null;
        [BsonElement("phone")]
        public string Phone { get; set; } = string.Empty;
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
    }
}
