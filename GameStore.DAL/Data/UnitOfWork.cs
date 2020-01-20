using GameStore.Domain.Models;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;

namespace GameStore.DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameContext _context;
        private readonly IRepositoryFactory _repositoryFactory;

        public UnitOfWork(GameContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public T GetRepository<T>(RepositoryType key = RepositoryType.SQL) where T : class
        {
            return _repositoryFactory.GetRepository<T>(key);
        }
    }
}