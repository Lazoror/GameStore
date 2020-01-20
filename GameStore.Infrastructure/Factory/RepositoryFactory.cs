using Autofac;
using GameStore.Domain.Models;
using GameStore.Interfaces.DAL.RepositorySql;

namespace GameStore.Infrastructure.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly ILifetimeScope _lifeTimeScope;

        public RepositoryFactory(ILifetimeScope lifeTimeScope)
        {
            _lifeTimeScope = lifeTimeScope;
        }

        public ICrudRepository<T> GetRepository<T>() where T : class
        {
            return _lifeTimeScope.Resolve<ICrudRepository<T>>();
        }

        public T GetRepository<T>(RepositoryType key) where T : class
        {
            return _lifeTimeScope.ResolveKeyed<T>(key);
        }
    }
}