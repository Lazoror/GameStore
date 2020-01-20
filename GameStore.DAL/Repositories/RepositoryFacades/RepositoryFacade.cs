using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;

namespace GameStore.DAL.Repositories.RepositoryFacades
{
    public class RepositoryFacade<TEntity> : ICrudRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ICrudRepository<TEntity> _sqlRepository;
        private readonly IMongoReadOnlyRepository<TEntity> _mongoRepository;

        public RepositoryFacade(IUnitOfWork unitOfWork)
        {
            _mongoRepository = unitOfWork.GetRepository<IMongoReadOnlyRepository<TEntity>>(RepositoryType.Mongo);
            _sqlRepository = unitOfWork.GetRepository<ICrudRepository<TEntity>>();
        }

        public void Insert(TEntity entity)
        {
            _sqlRepository.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            var sqlEntity = _sqlRepository.FirstOrDefault(x => x.Id == entity.Id);

            if (sqlEntity == null)
            {
                _sqlRepository.Insert(entity);
            }
            else
            {
                _sqlRepository.Update(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            _sqlRepository.Delete(entity);
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            var countSql = _sqlRepository.Count();
            var countMongo = _mongoRepository.Count();

            var result = countSql + countMongo;

            return result;
        }

        public IEnumerable<TEntity> GetMany(int skip = 0,
            int take = Int32.MaxValue,
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var entitiesSql =
                _sqlRepository.GetMany(0, int.MaxValue, filter, orderBy, SortDirection.Ascending, includes);
            var entitesMongo = _mongoRepository.GetMany(filter: filter).ToList();

            var allEntities = entitiesSql.Concat(entitesMongo)
                                         .AsQueryable()
                                         .GroupBy(x => x.Id)
                                         .Select(p => p.First())
                                         .Skip(skip)
                                         .Take(take)
                                         .ToList();

            return allEntities;
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var sqlEntity = _sqlRepository.FirstOrDefault(filter, includes);

            if (sqlEntity == null)
            {
                sqlEntity = _mongoRepository.FirstOrDefault(filter);
            }

            return sqlEntity;
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return _sqlRepository.Any(filter);
        }
    }
}