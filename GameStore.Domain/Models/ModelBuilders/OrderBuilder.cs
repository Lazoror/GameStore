using System;
using GameStore.Domain.Models.SqlModels.OrderModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class OrderBuilder
    {
        private Order _order;

        public OrderBuilder()
        {
            _order = new Order();
        }

        public OrderBuilder WithCustomerId(Guid id)
        {
            _order.CustomerId = id;

            return this;
        }

        public OrderBuilder WithIsCompleted()
        {
            _order.OrderStatus = OrderStatus.Shipped;

            return this;
        }

        public Order Build()
        {
            var result = _order;
            _order = new Order();

            return result;
        }
    }
}