using GameStore.Domain.Models.SqlModels.OrderModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class OrderDetailBuilder
    {
        private OrderDetail _orderDetail;

        public OrderDetailBuilder()
        {
            _orderDetail = new OrderDetail();
        }

        public OrderDetailBuilder WithGameKey(string key)
        {
            _orderDetail.GameKey = key;

            return this;
        }

        public OrderDetail Build()
        {
            var result = _orderDetail;
            _orderDetail = new OrderDetail();

            return result;
        }
    }
}