using GameStore.Domain.Models.MongoModels;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.MongoConfigurations
{
    public class OrderConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<OrderModel>(cm =>
            {
                cm.AutoMap();

                cm.MapMember(c => c.OrderDate).SetElementName("OrderDate").SetSerializer(new DateSerializer());
                cm.MapMember(c => c.ShippedDate).SetElementName("ShippedDate").SetSerializer(new DateSerializer());
                cm.MapMember(c => c.Shipper).SetElementName("ShipName");
                cm.MapMember(c => c.OrderMongoId).SetElementName("OrderID");
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}