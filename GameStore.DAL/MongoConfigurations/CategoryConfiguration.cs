using GameStore.Domain.Models.MongoModels;
using MongoDB.Bson.Serialization;

namespace GameStore.DAL.MongoConfigurations
{
    public class CategoryConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<GenreModel>(cm =>
            {
                cm.AutoMap();

                cm.MapMember(c => c.Name).SetElementName("CategoryName");
                cm.MapMember(c => c.CategoryId).SetElementName("CategoryID");
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}