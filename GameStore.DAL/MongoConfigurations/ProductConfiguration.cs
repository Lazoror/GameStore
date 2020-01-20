using GameStore.Domain.Models.MongoModels;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.MongoConfigurations
{
    public static class ProductConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<GameModel>(cm =>
            {
                cm.AutoMap();

                cm.MapMember(c => c.Name).SetElementName("ProductName");
                cm.MapMember(c => c.ProductId).SetElementName("ProductID");
                cm.MapMember(c => c.CategoryId).SetElementName("CategoryID");
                cm.MapMember(c => c.SupplierId).SetElementName("SupplierID");
                cm.MapMember(c => c.Price).SetElementName("UnitPrice");
                cm.MapMember(c => c.Discontinued).SetElementName("Discontinued");
                cm.MapMember(c => c.UnitsInStock).SetElementName("UnitsInStock");
                cm.MapMember(c => c.Description).SetElementName("QuantityPerUnit");
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}