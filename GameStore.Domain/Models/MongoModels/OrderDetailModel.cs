using System;

namespace GameStore.Domain.Models.MongoModels
{
    public class OrderDetailModel : MongoModel
    {
        public Guid OrderId { get; set; }

        public Guid OrderDetailId { get; set; }

        public int OrderMongoId { get; set; }

        public double Price { get; set; }

        public Int16 Quantity { get; set; }

        public short Discount { get; set; }
    }
}