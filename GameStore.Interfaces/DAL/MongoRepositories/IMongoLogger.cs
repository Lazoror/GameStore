using GameStore.Domain.Models.MongoModels;

namespace GameStore.Interfaces.DAL.MongoRepositories
{
    public interface IMongoLogger
    {
        void AddLog(LogModel log);
    }
}