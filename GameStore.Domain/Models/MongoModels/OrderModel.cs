using System;
using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels.OrderModels;

namespace GameStore.Domain.Models.MongoModels
{
    public class OrderModel : MongoModel
    {
        public Guid OrderId { get; set; }

        public int OrderMongoId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Shipper { get; set; }

        public bool IsCompleted { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}