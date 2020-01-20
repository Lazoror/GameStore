using GameStore.Domain.Models;
using GameStore.Domain.Models.MongoModels;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.MongoConfigurations
{
    public static class MongoConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(
                cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id).SetSerializer(new ObjectIdSerializer());
                    cm.SetIsRootClass(true);
                });

            BsonClassMap.RegisterClassMap<MongoModel>(
                cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id);
                    cm.SetIsRootClass(true);
                });

            ProductConfiguration.Configure();
            CategoryConfiguration.Configure();
            SupplierConfiguration.Configure();
            OrderDetailsConfiguration.Configure();
            OrderConfiguration.Configure();
        }
    }
}