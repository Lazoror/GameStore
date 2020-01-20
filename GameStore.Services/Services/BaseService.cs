using GameStore.Domain.Models.MongoModels;
using GameStore.Interfaces.DAL.MongoRepositories;

namespace GameStore.Services.Services
{
    public class BaseService
    {
        private readonly IMongoLogger _logRepository;

        public BaseService(IMongoLogger logRepository)
        {
            _logRepository = logRepository;
        }

        public void LogAction(string date, string operation, string entityType, string newEntity, string oldEntity = null)
        {
            _logRepository.AddLog(new LogModel
            {
                Date = date,
                Operation = operation,
                EntityType = entityType,
                NewEntity = newEntity,
                OldEntity = oldEntity
            });
        }
    }
}