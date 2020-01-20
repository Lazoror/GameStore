using GameStore.Interfaces.DAL.RepositorySql;

namespace GameStore.Interfaces.DAL.MongoRepositories
{
    public interface IMongoReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
    }
}