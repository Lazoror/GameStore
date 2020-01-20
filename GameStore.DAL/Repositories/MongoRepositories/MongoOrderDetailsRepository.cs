using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DAL.Data;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.DAL.MongoRepositories;
using MongoDB.Driver;
using SortDirection = GameStore.Domain.Models.SqlModels.SortModels.SortDirection;

namespace GameStore.DAL.Repositories.MongoRepositories
{
    public class MongoOrderDetailsRepository : IMongoReadOnlyRepository<OrderDetail>
    {
        private readonly MongoContext _mongoContext;
        private readonly IMapper _mapper;

        public MongoOrderDetailsRepository(MongoContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        public int Count(Expression<Func<OrderDetail, bool>> filter = null)
        {
            return (int)_mongoContext.OrderDetailsModel.EstimatedDocumentCount();
        }

        public IEnumerable<OrderDetail> GetMany(int skip = 0, int take = Int32.MaxValue, Expression<Func<OrderDetail, bool>> filter = null, Expression<Func<OrderDetail, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending, params Expression<Func<OrderDetail, object>>[] includes)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            var orderDetails = _mongoContext.OrderDetailsModel.AsQueryable();
            var orderDetailEntities =
                _mapper.Map<IEnumerable<OrderDetail>>(orderDetails).Where(filter.Compile()).ToList();

            return orderDetailEntities;
        }

        public OrderDetail FirstOrDefault(Expression<Func<OrderDetail, bool>> filter, params Expression<Func<OrderDetail, object>>[] includes)
        {
            var orderDetail = GetMany(filter: filter).FirstOrDefault();

            return orderDetail;
        }

        public bool Any(Expression<Func<OrderDetail, bool>> filter)
        {
            return _mongoContext.OrderDetailsModel.AsQueryable().Any();
        }
    }
}