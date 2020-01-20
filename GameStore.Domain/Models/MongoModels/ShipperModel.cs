using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.Domain.Models.MongoModels
{
    public class ShipperModel : MongoModel
    {
        [BsonElement("ShipperID")]
        public int ShipperId { get; set; }

        [BsonElement("CompanyName")]
        public string CompanyName { get; set; }

        [BsonElement("Phone")]
        public string Phone { get; set; }
    }
}