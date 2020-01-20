using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.DAL.Data;
using GameStore.Domain.Models.MongoModels;
using GameStore.Interfaces.DAL.MongoRepositories;
using MongoDB.Driver;
using SortDirection = GameStore.Domain.Models.SqlModels.SortModels.SortDirection;

namespace GameStore.DAL.Repositories.MongoRepositories
{
    public class MongoShipperRepository : IMongoReadOnlyRepository<ShipperModel>
    {
        private readonly MongoContext _mongoContext;

        public MongoShipperRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public int Count(Expression<Func<ShipperModel, bool>> filter = null)
        {
            return (int)_mongoContext.Shippers.EstimatedDocumentCount();
        }

        public IEnumerable<ShipperModel> GetMany(int skip = 0, int take = Int32.MaxValue, Expression<Func<ShipperModel, bool>> filter = null, Expression<Func<ShipperModel, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending, params Expression<Func<ShipperModel, object>>[] includes)
        {
            var shippers = _mongoContext.Shippers.Find(_ => true).ToList();

            return shippers;
        }

        public ShipperModel FirstOrDefault(Expression<Func<ShipperModel, bool>> filter, params Expression<Func<ShipperModel, object>>[] includes)
        {
            var shipper = _mongoContext.Shippers.Find(filter).FirstOrDefault();

            return shipper;
        }

        public bool Any(Expression<Func<ShipperModel, bool>> filter)
        {
            return _mongoContext.Shippers.AsQueryable().Any();
        }
    }
}