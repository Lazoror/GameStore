using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Payment
{
    public class BankPayment : IPayment
    {
        private readonly IOrderService _orderService;

        public BankPayment(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Process(ProcessPaymentModel orderInfo)
        {
            var stream = _orderService.GenerateInvoiceFile(orderInfo);

            return new FileStreamResult(stream, "application/pdf");
        }
    }
}