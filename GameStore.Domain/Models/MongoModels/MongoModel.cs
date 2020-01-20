using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.Domain.Models.MongoModels
{
    public class MongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}