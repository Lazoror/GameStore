using GameStore.Domain.Models.MongoModels;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.MongoConfigurations
{
    public class SupplierConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PublisherModel>(cm =>
            {
                cm.AutoMap();

                cm.MapMember(c => c.SupplierId).SetElementName("SupplierID");
                cm.MapMember(c => c.CompanyName).SetElementName("CompanyName");
                cm.MapMember(c => c.Description).SetElementName("ContactTitle");
                cm.MapMember(c => c.HomePage).SetElementName("HomePage");
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}