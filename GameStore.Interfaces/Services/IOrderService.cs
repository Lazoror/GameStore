using GameStore.Domain.Models.SqlModels.OrderModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameStore.Interfaces.Services
{
    public interface IOrderService
    {
        void CreateOrder(OrderDetail orderDetails, string email);

        Order GetAllCartOrder(string email);

        Order GetOrderById(Guid orderId);

        OrderDetail GetOrderDetail(Guid orderDetailId);

        IEnumerable<Order> GetHistoryOrder(DateTime fromDate, DateTime toDate);

        void DeleteOrderDetail(string gameKey);

        void DeleteOrder(Guid userId);

        void EditOrderDetail(OrderDetail entity);

        void EditOrder(Order order);

        MemoryStream GenerateInvoiceFile(ProcessPaymentModel orderInfo);
    }
}