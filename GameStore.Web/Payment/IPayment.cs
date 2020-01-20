using GameStore.Domain.Models.SqlModels.OrderModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Payment
{
    public interface IPayment
    {
        IActionResult Process(ProcessPaymentModel orderInfo);
    }
}