using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DAL.Data;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.DAL.MongoRepositories;
using MongoDB.Bson;
using MongoDB.Driver;
using SortDirection = GameStore.Domain.Models.SqlModels.SortModels.SortDirection;

namespace GameStore.DAL.Repositories.MongoRepositories
{
    public class MongoOrderRepository : IMongoReadOnlyRepository<Order>
    {
        private readonly MongoContext _mongoContext;
        private readonly IMapper _mapper;

        public MongoOrderRepository(MongoContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        public int Count(Expression<Func<Order, bool>> filter = null)
        {
            return (int)_mongoContext.OrdersModel.EstimatedDocumentCount();
        }

        public IEnumerable<Order> GetMany(int skip = 0, int take = Int32.MaxValue, Expression<Func<Order, bool>> filter = null, Expression<Func<Order, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending, params Expression<Func<Order, object>>[] includes)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            var orders = _mongoContext.OrdersModel.AsQueryable().ToList();

            var orderModels = orders.Select(order =>
            {
                order.OrderId = ObjectIdToGuid(order.Id);
                order.OrderStatus = OrderStatus.Shipped;

                order.OrderDetails = GetOrderDetails(order.OrderMongoId);

                return order;
            });

            var orderEntities = _mapper.Map<IEnumerable<Order>>(orderModels).Where(filter.Compile()).ToList();

            return orderEntities;
        }

        public Order FirstOrDefault(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includes)
        {
            return GetMany().FirstOrDefault(filter.Compile());
        }

        public bool Any(Expression<Func<Order, bool>> filter)
        {
            return _mongoContext.OrdersModel.AsQueryable().Any();
        }

        private List<OrderDetail> GetOrderDetails(int orderMongoId)
        {
            var orderDetailModels = _mongoContext.OrderDetailsModel.AsQueryable()
                                                 .Where(x => x.OrderMongoId == orderMongoId).ToList();

            foreach (var orderDetail in orderDetailModels)
            {
                orderDetail.OrderDetailId = ObjectIdToGuid(orderDetail.Id);
            }

            var orderDetails = _mapper.Map<IEnumerable<OrderDetail>>(orderDetailModels).ToList();

            return orderDetails;
        }

        private string GuidToObjectId(Guid guid)
        {
            var bytes = guid.ToByteArray().Take(12).ToArray();
            var oid = new ObjectId(bytes).ToString();

            return oid;
        }

        private Guid ObjectIdToGuid(string objectId)
        {
            var guidId = new Guid(ObjectId.Parse(objectId).ToByteArray().Concat(new byte[] { 5, 5, 5, 5 }).ToArray());

            return guidId;
        }
    }
}