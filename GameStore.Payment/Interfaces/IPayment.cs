using System;

namespace GameStore.Payment.Interfaces
{
    public interface IPayment
    {
        bool Process(Guid orderId, decimal sum);
    }
}