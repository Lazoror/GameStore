using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.SortModels;

namespace GameStore.Interfaces.DAL.RepositorySql
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        int Count(Expression<Func<TEntity, bool>> filter = null);

        IEnumerable<TEntity> GetMany(
            int skip = 0,
            int take = Int32.MaxValue,
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending,
            params Expression<Func<TEntity, object>>[] includes);

        TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes);

        bool Any(Expression<Func<TEntity, bool>> filter);
    }
}