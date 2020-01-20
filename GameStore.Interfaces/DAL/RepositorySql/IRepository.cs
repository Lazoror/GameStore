namespace GameStore.Interfaces.DAL.RepositorySql
{
    public interface ICrudRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}