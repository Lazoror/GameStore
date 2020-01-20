using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameStore.Domain.Models;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace GameStore.Services.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<OrderDetail> _orderDetailRepository;
        private readonly ICrudRepository<Order> _orderRepository;
        private readonly ICrudRepository<Order> _sqlOrdeRepository;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(
            IUnitOfWork unitOfWork,
            IMongoLogger logRepository,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor) : base(logRepository)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _orderDetailRepository = _unitOfWork.GetRepository<ICrudRepository<OrderDetail>>();
            _orderRepository = _unitOfWork.GetRepository<ICrudRepository<Order>>();
            _sqlOrdeRepository = unitOfWork.GetRepository<ICrudRepository<Order>>(RepositoryType.SQL);
        }

        public void DeleteOrder(Guid userId)
        {
            var order = _orderRepository.FirstOrDefault(x =>
                x.CustomerId == userId && x.OrderStatus != OrderStatus.Shipped);

            if (order != null)
            {
                _orderRepository.Delete(order);
                _unitOfWork.Commit();

                LogAction(DateTime.UtcNow.ToString(), "Delete", "Order", JsonConvert.SerializeObject(order, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
        }

        public void EditOrderDetail(OrderDetail entity)
        {
            var oldOrderDetail = JsonConvert.SerializeObject(_orderDetailRepository.FirstOrDefault(x => x.Id == entity.Id));

            if (entity.Quantity <= 0)
            {
                _orderDetailRepository.Delete(entity);
            }
            else
            {
                _orderDetailRepository.Update(entity);
            }

            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Edit", "OrderDetail", JsonConvert.SerializeObject(entity, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), oldOrderDetail);
        }

        public void EditOrder(Order order)
        {
            if (order != null)
            {
                _orderRepository.Update(order);
                _unitOfWork.Commit();
            }
        }

        public MemoryStream GenerateInvoiceFile(ProcessPaymentModel orderInfo)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Invoice file";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create x font
            XFont font = new XFont("Verdana", 10, XFontStyle.Regular);

            // Draw the text
            gfx.DrawString($"Customer ID: {orderInfo.CustomerId}", font, XBrushes.Black,
                new XRect(10, 20, page.Width, 0), XStringFormats.BaseLineLeft);
            gfx.DrawString($"Order ID: {orderInfo.OrderId}", font, XBrushes.Black,
                new XRect(10, 35, page.Width, 0), XStringFormats.BaseLineLeft);
            gfx.DrawString($"Total: {orderInfo.OrderSum}", font, XBrushes.Black,
                new XRect(10, 50, page.Width, 0), XStringFormats.BaseLineLeft);

            // Send PDF to browser
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);

            return stream;
        }

        public Order GetOrderById(Guid orderId)
        {
            var order = _orderRepository.FirstOrDefault(x => x.Id == orderId, x => x.OrderDetails);

            return order;
        }

        public OrderDetail GetOrderDetail(Guid orderDetailId)
        {
            var orderDetail = _orderDetailRepository.FirstOrDefault(od => od.Id == orderDetailId);

            return orderDetail;
        }

        public IEnumerable<Order> GetHistoryOrder(DateTime fromDate, DateTime toDate)
        {
            return _orderRepository.GetMany(orderBy: x => x.OrderStatus)
                                   .Where(x => x.OrderDate >= fromDate && x.OrderDate <= toDate)
                                   .ToList();
        }

        public void DeleteOrderDetail(string gameKey)
        {
            var orderDetail = _orderDetailRepository.FirstOrDefault(x => x.GameKey == gameKey);

            if (orderDetail != null)
            {
                var order = _orderRepository.FirstOrDefault(x => x.Id == orderDetail.OrderId, x => x.OrderDetails);

                _orderDetailRepository.Delete(orderDetail);

                if (!order.OrderDetails.Any())
                {
                    _orderRepository.Delete(order);
                }

                _unitOfWork.Commit();

                LogAction(DateTime.UtcNow.ToString(), "Delete", "Order", JsonConvert.SerializeObject(order, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
        }

        public void CreateOrder(OrderDetail orderDetails, string email)
        {
            var orderId = Guid.NewGuid();
            var futureUserId = Guid.NewGuid();
            var user = _userService.GetUserByEmail(email);
            orderDetails.Id = Guid.NewGuid();

            if (user == null)
            {
                user = new User { Id = futureUserId };
            }

            var totalPrice = (orderDetails.Price * orderDetails.Quantity) -
                             ((orderDetails.Discount / 100) * orderDetails.Price);
            orderDetails.Price = totalPrice;

            var cookieOrder = GetCookieOrder();

            SetCookieOrder(email, futureUserId, orderId, cookieOrder);

            if (cookieOrder == null)
            {
                AddNonExistingOrder(user, orderDetails, orderId);
            }
            else
            {
                user.Id = cookieOrder.UserId;

                UpdateExistingOrder(user, orderDetails);
            }
        }

        public Order GetAllCartOrder(string email)
        {
            var user = new User();

            if (!String.IsNullOrEmpty(email))
            {
                user = _userService.GetUserByEmail(email);
            }

            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("Order") && String.IsNullOrEmpty(email))
            {
                var cachedOrderCookie = _httpContextAccessor.HttpContext.Request.Cookies["Order"];
                var cachedOrderModified = JsonConvert.DeserializeObject<GuestBasketModel>(cachedOrderCookie);

                if (user.Id == Guid.Empty)
                {
                    user.Id = cachedOrderModified.UserId;
                }
            }

            var currentOrder = _sqlOrdeRepository.FirstOrDefault(
                x => x.CustomerId == user.Id && x.OrderStatus != OrderStatus.Shipped, od => od.OrderDetails);

            return currentOrder;
        }

        private GuestBasketModel GetCookieOrder()
        {
            if (!_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("Order"))
            {
                return null;
            }

            var cachedOrderCookie = _httpContextAccessor.HttpContext.Request.Cookies["Order"];
            var cachedOrderModified = JsonConvert.DeserializeObject<GuestBasketModel>(cachedOrderCookie);

            return cachedOrderModified;
        }

        private void SetCookieOrder(string email, Guid futureUserId, Guid orderId, GuestBasketModel cookieOrder)
        {
            if (String.IsNullOrEmpty(email))
            {

                if (cookieOrder == null)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMonths(1);
                    var httpContext = _httpContextAccessor.HttpContext;

                    var cachedOrder = new GuestBasketModel
                    {
                        UserId = futureUserId,
                        OrderId = orderId
                    };

                    httpContext.Response.Cookies.Append("Order", JsonConvert.SerializeObject(cachedOrder, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
        }

        private void AddNonExistingOrder(User user, OrderDetail orderDetails, Guid orderId)
        {
            var orderNew = new Order { CustomerId = user.Id, Id = orderId, OrderDate = DateTime.UtcNow };
            _orderRepository.Insert(orderNew);

            orderDetails.OrderId = orderId;
            _orderDetailRepository.Insert(orderDetails);

            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Create", "Order", JsonConvert.SerializeObject(orderNew));
            LogAction(DateTime.UtcNow.ToString(), "Create", "OrderDetails", JsonConvert.SerializeObject(orderDetails));
        }

        private void UpdateExistingOrder(User user, OrderDetail orderDetails)
        {
            var order = _orderRepository.FirstOrDefault(
                x => x.CustomerId == user.Id && x.OrderStatus != OrderStatus.Shipped, x => x.OrderDetails);
            var isGameInBasket = order.OrderDetails.Any(x => x.GameKey == orderDetails.GameKey);

            if (isGameInBasket)
            {
                var orderDetailOld = _orderDetailRepository.FirstOrDefault(x => x.GameKey == orderDetails.GameKey);
                orderDetailOld.Quantity += orderDetails.Quantity;
                orderDetailOld.Price += orderDetails.Price;
                _orderDetailRepository.Update(orderDetailOld);

                LogAction(DateTime.UtcNow.ToString(), "Update", "OrderDetail",
                    JsonConvert.SerializeObject(orderDetailOld));
            }
            else
            {
                orderDetails.OrderId = order.Id;

                _orderDetailRepository.Insert(orderDetails);

                LogAction(DateTime.UtcNow.ToString(), "Create", "Order", JsonConvert.SerializeObject(order));
            }

            _unitOfWork.Commit();
        }
    }
}