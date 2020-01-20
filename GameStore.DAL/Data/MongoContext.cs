using GameStore.Domain.Models.MongoModels;
using GameStore.Interfaces.DAL.Data.MongoSettings;
using MongoDB.Driver;

namespace GameStore.DAL.Data
{
    public class MongoContext
    {
        private const string OrdersCollection = "orders";
        private const string OrderDetailsCollection = "order-details";
        private const string CategoriesCollection = "categories";
        private const string SuppliersCollection = "suppliers";
        private const string ProductsCollection = "products";
        private const string ShippersCollection = "shippers";
        private const string LogsCollection = "logs";

        private readonly IMongoDatabase _db;

        public MongoContext(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            _db = client.GetDatabase(databaseSettings.DatabaseName);
        }

        public IMongoCollection<OrderModel> OrdersModel => _db.GetCollection<OrderModel>(OrdersCollection);

        public IMongoCollection<OrderDetailModel> OrderDetailsModel =>
            _db.GetCollection<OrderDetailModel>(OrderDetailsCollection);

        public IMongoCollection<GenreModel> CategoriesModel => _db.GetCollection<GenreModel>(CategoriesCollection);

        public IMongoCollection<PublisherModel> PublishersModel =>
            _db.GetCollection<PublisherModel>(SuppliersCollection);

        public IMongoCollection<GameModel> GamesModel => _db.GetCollection<GameModel>(ProductsCollection);

        public IMongoCollection<ShipperModel> Shippers => _db.GetCollection<ShipperModel>(ShippersCollection);

        public IMongoCollection<LogModel> Logs => _db.GetCollection<LogModel>(LogsCollection);
    }
}