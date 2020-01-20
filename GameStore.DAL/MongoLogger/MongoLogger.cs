using AutoMapper;
using GameStore.DAL.Data;
using GameStore.Domain.Models.MongoModels;
using GameStore.Interfaces.DAL.MongoRepositories;

namespace GameStore.DAL.MongoLogger
{
    public class MongoLogger : IMongoLogger
    {
        private readonly MongoContext _mongoContext;

        public MongoLogger(MongoContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
        }

        public void AddLog(LogModel log)
        {
            _mongoContext.Logs.InsertOne(log);
        }
    }
}