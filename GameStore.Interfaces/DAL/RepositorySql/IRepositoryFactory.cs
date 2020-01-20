using GameStore.Domain.Models;

namespace GameStore.Interfaces.DAL.RepositorySql
{
    public interface IRepositoryFactory
    {
        T GetRepository<T>(RepositoryType key = RepositoryType.SQL) where T : class;
    }
}