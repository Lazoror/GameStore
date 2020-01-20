using GameStore.Domain.Models.MongoModels;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.MongoConfigurations
{
    public class OrderDetailsConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<OrderDetailModel>(cm =>
            {
                cm.AutoMap();

                cm.MapMember(c => c.Discount).SetElementName("Discount").SetSerializer(new IntToShortSerializer());
                cm.MapMember(c => c.Quantity).SetElementName("Quantity");
                cm.MapMember(c => c.Price).SetElementName("UnitPrice");
                cm.MapMember(c => c.OrderMongoId).SetElementName("OrderID");
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}