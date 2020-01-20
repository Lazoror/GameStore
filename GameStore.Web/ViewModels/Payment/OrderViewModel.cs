using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels.OrderModels;

namespace GameStore.Web.ViewModels.Payment
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        public List<string> Shippers { get; set; }
    }
}