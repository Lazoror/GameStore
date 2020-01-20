using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DAL.Data;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.DAL.MongoRepositories;
using MongoDB.Driver;
using SortDirection = GameStore.Domain.Models.SqlModels.SortModels.SortDirection;

namespace GameStore.DAL.Repositories.MongoRepositories
{
    public class MongoPublisherRepository : IMongoReadOnlyRepository<Publisher>
    {
        private readonly MongoContext _mongoContext;
        private readonly IMapper _mapper;

        public MongoPublisherRepository(MongoContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        public int Count(Expression<Func<Publisher, bool>> filter = null)
        {
            return (int)_mongoContext.PublishersModel.EstimatedDocumentCount();
        }

        public IEnumerable<Publisher> GetMany(int skip = 0, int take = Int32.MaxValue, Expression<Func<Publisher, bool>> filter = null, Expression<Func<Publisher, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending, params Expression<Func<Publisher, object>>[] includes)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            var publishers = _mongoContext.PublishersModel.AsQueryable();
            var publisherEntities = _mapper.Map<IEnumerable<Publisher>>(publishers).Where(filter.Compile()).ToList();

            return publisherEntities;
        }

        public Publisher FirstOrDefault(Expression<Func<Publisher, bool>> filter, params Expression<Func<Publisher, object>>[] includes)
        {
            var publisher = GetMany(filter: filter).FirstOrDefault();

            return publisher;
        }

        public bool Any(Expression<Func<Publisher, bool>> filter)
        {
            return _mongoContext.PublishersModel.AsQueryable().Any();
        }
    }
}