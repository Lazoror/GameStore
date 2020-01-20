using System.Collections.Generic;
using GameStore.Domain.Models.MongoModels;

namespace GameStore.Interfaces.Services
{
    public interface IShipperService
    {
        ShipperModel GetShipperByName(string companyName);

        IEnumerable<ShipperModel> GetAllShippers();
    }
}