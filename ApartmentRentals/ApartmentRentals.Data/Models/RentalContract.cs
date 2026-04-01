using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApartmentRentals.Data.Models
{
    public class RentalContract
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }

        public string SpaceId { get; set; } = "0";
        public string TenantId { get; set; } = "0";

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }
        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }
        //public decimal FinalPrice { get; set; } //Итоговая цена
    }
}
