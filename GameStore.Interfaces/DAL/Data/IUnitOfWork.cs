using GameStore.Domain.Models;

namespace GameStore.Interfaces.DAL.Data
{
    public interface IUnitOfWork
    {
        void Commit();

        T GetRepository<T>(RepositoryType key = RepositoryType.SQL) where T : class;
    }
}