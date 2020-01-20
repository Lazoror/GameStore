using System;
using System.Collections.Generic;

namespace GameStore.Web.ViewModels.Payment
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }

        public string GameKey { get; set; }

        public string GameName { get; set; }

        public decimal Price { get; set; }

        public List<string> Platforms { get; set; }

        public Int16 Quantity { get; set; }

        public Int16 Discount { get; set; }
    }
}