using System.Collections.Generic;
using GameStore.Domain.Models.MongoModels;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.Services;

namespace GameStore.Services.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IMongoReadOnlyRepository<ShipperModel> _mongoRepository;

        public ShipperService(IMongoReadOnlyRepository<ShipperModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public ShipperModel GetShipperByName(string companyName)
        {
            return _mongoRepository.FirstOrDefault(x => x.CompanyName == companyName);
        }

        public IEnumerable<ShipperModel> GetAllShippers()
        {
            return _mongoRepository.GetMany();
        }
    }
}