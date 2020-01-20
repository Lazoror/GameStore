using System;
using System.Linq;
using Autofac.Features.Indexed;
using GameStore.Domain;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Attributes.Authorization;
using GameStore.Web.Payment;
using GameStore.Web.ViewModels.Payment;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShipperService _shipperService;
        private readonly IUserService _userService;
        private readonly IIndex<PaymentType, IPayment> _paymentResolver;

        public OrderController(IOrderService orderService,
            IIndex<PaymentType, IPayment> paymentResolver,
            IShipperService shipperService,
            IUserService userService)
        {
            _orderService = orderService;
            _paymentResolver = paymentResolver;
            _shipperService = shipperService;
            _userService = userService;
        }

        [HttpGet("")]
        [StoreAuthorize(AuthorizePermission.Disallow, "Guest")]
        public IActionResult Order()
        {
            var customerEmail = User.Identity.Name;
            var order = _orderService.GetAllCartOrder(customerEmail);
            var shippers = _shipperService.GetAllShippers().Select(x => x.CompanyName).ToList();

            var orderViewModel = new OrderViewModel { Order = order, Shippers = shippers };

            return View(orderViewModel);
        }

        [HttpPost("card")]
        [StoreAuthorize(AuthorizePermission.Disallow, "Guest")]
        public IActionResult Card(CardViewModel cardData)
        {
            var res = _paymentResolver[PaymentType.Visa].Process(GetPaymentInfo());

            return res;
        }

        [HttpGet("card")]
        [StoreAuthorize(AuthorizePermission.Disallow, "Guest")]
        public IActionResult Card()
        {
            return View();
        }

        [HttpGet("edit")]
        public IActionResult Edit(Order order)
        {
            if (order != null)
            {
                if (ModelState.IsValid)
                {
                    _orderService.EditOrder(order);
                }
            }

            return RedirectToAction(nameof(Order));
        }

        [HttpGet("pay")]
        [StoreAuthorize(AuthorizePermission.Disallow, "Guest")]
        public IActionResult Pay(PaymentType payType, string shipper)
        {
            var paymentInfo = GetPaymentInfo();

            var order = _orderService.GetAllCartOrder(User.Identity.Name);
            order.Shipper = shipper;
            order.OrderStatus = OrderStatus.Paid;

            _orderService.EditOrder(order);

            var res = _paymentResolver[payType].Process(paymentInfo);

            return res;
        }

        private ProcessPaymentModel GetPaymentInfo()
        {
            var customerEmail = User.Identity.Name;
            decimal orderSum = _orderService.GetAllCartOrder(customerEmail).OrderDetails.Sum(a => a.Price);
            var orderId = _orderService.GetAllCartOrder(customerEmail).OrderDetails.Select(a => a.OrderId).First();
            var user = _userService.GetUserByEmail(customerEmail);

            var paymentInfo = new ProcessPaymentModel { CustomerId = user.Id, OrderId = orderId, OrderSum = orderSum };

            return paymentInfo;
        }

        [HttpPost("history")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public IActionResult History(DateTime fromDate, DateTime toDate)
        {
            var orders = _orderService.GetHistoryOrder(fromDate, toDate);

            return View(orders);
        }

        [HttpGet("history")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public IActionResult History()
        {
            var orders = _orderService.GetHistoryOrder(DateTime.MinValue, DateTime.UtcNow.AddDays(-30));

            return View(orders);
        }

        [HttpGet("manage")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public IActionResult ManageOrders()
        {
            var orders = _orderService.GetHistoryOrder(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow);

            return View(orders);
        }

        [HttpGet("manageShipped")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public IActionResult SetShippedStatus(Guid userId)
        {
            var user = _userService.GetUserById(userId);
            var order = _orderService.GetAllCartOrder(user.Email);
            order.ShippedDate = DateTime.UtcNow;
            order.OrderStatus = OrderStatus.Shipped;
            _orderService.EditOrder(order);

            return RedirectToAction(nameof(ManageOrders));
        }
    }
}