using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameStore.Domain.Models.SqlModels.OrderModels
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Shipper { get; set; }

        [JsonIgnore]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}