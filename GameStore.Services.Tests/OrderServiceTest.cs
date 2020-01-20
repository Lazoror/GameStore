using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services;
using GameStore.Services.Services;
using GameStore.Services.Tests.ModelBuilders;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace GameStore.Services.Tests
{
    public class OrderServiceTest
    {
        private readonly Mock<ICrudRepository<OrderDetail>> _orderDetailRepository;
        private readonly Mock<ICrudRepository<Order>> _orderRepository;
        private readonly OrderService _orderService;
        private readonly string CustomerEmail = "user@email.com";
        private readonly Order _order;
        private readonly OrderDetail _orderDetail;

        public OrderServiceTest()
        {
            InitializeTestData(out var user, out _order, out _orderDetail);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var logRepositoryMock = new Mock<IMongoLogger>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userServiceMock = new Mock<IUserService>();
            var userRepository = new Mock<ICrudRepository<User>>();
            var httpContext = new Mock<HttpContext>();
            var cookieCollection = new Mock<IRequestCookieCollection>();
            var cookieResponse = new Mock<IResponseCookies>();

            _orderDetailRepository = new Mock<ICrudRepository<OrderDetail>>();
            _orderRepository = new Mock<ICrudRepository<Order>>();

            unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<User>>(RepositoryType.SQL)).Returns(userRepository.Object);
            unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Order>>(RepositoryType.SQL)).Returns(_orderRepository.Object);
            unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Order>>(RepositoryType.SQL)).Returns(_orderRepository.Object);
            unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<OrderDetail>>(RepositoryType.SQL)).Returns(_orderDetailRepository.Object);

            httpContext.Setup(hc => hc.Response.Cookies).Returns(cookieResponse.Object);
            contextAccessor.Setup(x => x.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(hc => hc.Request.Cookies).Returns(cookieCollection.Object);
            cookieCollection.Setup(cc => cc["Order"]).Returns(JsonConvert.SerializeObject(new Order()));
            userServiceMock.Setup(us => us.GetUserByEmail(It.IsAny<string>())).Returns(user);
            userRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), null)).Returns(user);
            _orderRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>[]>())).Returns(_order);
            _orderRepository.Setup(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<Expression<Func<Order, object>>>(), It.IsAny<SortDirection>(), It.IsAny<Expression<Func<Order, object>>[]>()))
                .Returns(new List<Order> { _order });
            _orderDetailRepository.Setup(a => a.GetMany(0, int.MaxValue, null,
                    It.IsAny<Expression<Func<OrderDetail, object>>>(), It.IsAny<SortDirection>(), It.IsAny<Expression<Func<OrderDetail, object>>>()))
                .Returns(new List<OrderDetail> { _orderDetail });
            _orderDetailRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<OrderDetail, bool>>>(),
                It.IsAny<Expression<Func<OrderDetail, object>>[]>())).Returns(_orderDetail);

            _orderService = new OrderService(unitOfWorkMock.Object,
                logRepositoryMock.Object,
                userServiceMock.Object,
                contextAccessor.Object);
        }

        [Fact]
        public void EditOrder_ShouldCallUpdateOrderDetailOnce_WhenEntity()
        {
            // Act
            _orderService.EditOrderDetail(_orderDetail);

            // Assert
            _orderDetailRepository.Verify(a => a.Delete(It.IsAny<OrderDetail>()), Times.Once);
        }

        [Fact]
        public void CreateOrder_ShouldCallGetManyOnce_WhenOrderDetailsAndCustomerId()
        {
            // Act
            _orderService.CreateOrder(_orderDetail, CustomerEmail);

            // Assert
            _orderRepository.Verify(a => a.Insert(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void GetAllCartOrders_ShouldCallGetManyOnce_WhenOrder()
        {
            // Act
            _orderService.EditOrder(_order);

            // Assert
            _orderRepository.Verify(a => a.Update(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void
            GetHistoryOrder_ShouldCallGetManyOnce_WhenDates()
        {
            // Act
            _orderService.GetHistoryOrder(DateTime.MinValue, DateTime.MaxValue);

            // Assert
            _orderRepository.Verify(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>>(), It.IsAny<SortDirection>()));
        }

        [Fact]
        public void CreateOrder_ShouldCallOnceOrderDetailGetMany_WhenEmptyOrderDetailsAndCustomerId()
        {
            // Arrange
            _orderRepository.Setup(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>>(), It.IsAny<SortDirection>())).Returns(new List<Order> { _order });

            // Act
            _orderService.CreateOrder(_orderDetail, CustomerEmail);

            // Assert
            _orderRepository.Verify(a => a.Insert(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void CreateOrder_ShouldCallOnceOrderDetailGetMany_WhenEmptyBasketAndCustomerId()
        {
            // Arrange
            _orderDetailRepository
                .Setup(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<OrderDetail, bool>>>(),
                    It.IsAny<Expression<Func<OrderDetail, object>>>(), It.IsAny<SortDirection>()))
                .Returns(new List<OrderDetail>());

            // Act
            _orderService.CreateOrder(_orderDetail, CustomerEmail);

            // Assert
            _orderRepository.Verify(a => a.Insert(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void CreateOrder_ShouldCallOnceAny_WhenEmptyBasketAndCustomerId()
        {
            // Arrange
            _orderDetailRepository.Setup(a => a.Any(It.IsAny<Expression<Func<OrderDetail, bool>>>())).Returns(true);
            _orderDetailRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<OrderDetail, bool>>>()))
                .Returns(_orderDetail);
            // Act
            _orderService.CreateOrder(_orderDetail, CustomerEmail);

            // Assert
            _orderDetailRepository.Verify(a => a.Insert(It.IsAny<OrderDetail>()), Times.Once);
        }

        [Fact]
        public void CreateOrder_ShouldCallOnceAnyAndOrderNonExist_WhenEmptyBasketAndCustomerId()
        {
            // Arrange
            var order = _order;
            order.OrderStatus = OrderStatus.Shipped;
            _orderDetailRepository.Setup(a => a.Any(It.IsAny<Expression<Func<OrderDetail, bool>>>())).Returns(true);
            _orderRepository.Setup(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>>(), It.IsAny<SortDirection>())).Returns(new List<Order> { order });
            _orderDetailRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<OrderDetail, bool>>>()))
                .Returns(_orderDetail);

            // Act
            _orderService.CreateOrder(_orderDetail, CustomerEmail);

            // Assert
            _orderDetailRepository.Verify(a => a.Insert(It.IsAny<OrderDetail>()), Times.Once);
        }

        [Fact]
        public void CreateOrder_ShouldCallOnceAnyAndOrderNonExist_WhenEmptyEmail()
        {
            // Arrange
            var order = _order;
            order.OrderStatus = OrderStatus.Shipped;
            _orderDetailRepository.Setup(a => a.Any(It.IsAny<Expression<Func<OrderDetail, bool>>>())).Returns(true);
            _orderRepository.Setup(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>>(), It.IsAny<SortDirection>())).Returns(new List<Order> { order });
            _orderDetailRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<OrderDetail, bool>>>()))
                .Returns(_orderDetail);
            // Act
            _orderService.CreateOrder(_orderDetail, It.IsAny<string>());

            // Assert
            _orderDetailRepository.Verify(a => a.Insert(It.IsAny<OrderDetail>()), Times.Once);
        }

        [Fact]
        public void DeleteOrderDetails_ShouldCallGetSingleOnce_WhenGameKey()
        {
            // Arrange
            _orderDetailRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<OrderDetail, bool>>>(), It.IsAny<Expression<Func<OrderDetail, object>>[]>())).Returns(new OrderDetail());
            string gameKey = "csgo";

            // Act
            _orderService.DeleteOrderDetail(gameKey);

            // Assert
            _orderDetailRepository.Verify(a => a.FirstOrDefault(It.IsAny<Expression<Func<OrderDetail, bool>>>()));
        }

        [Fact]
        public void DeleteOrder_ShouldCallDeleteOrderOnce_WhenGameKey()
        {
            // Arrange
            var customerId = new Guid("5265394a-6aaa-4191-35f5-08d5223464b8");

            // Act
            _orderService.DeleteOrder(customerId);

            // Assert
            _orderRepository.Verify(a => a.Delete(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void GetAllCartOrder_ShouldCallCookie_WhenEmail()
        {
            // Arrange
            _orderRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>>())).Returns(_order);

            // Act
            _orderService.GetAllCartOrder("email");

            // Assert
            _orderRepository.Verify(or => or.FirstOrDefault(It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>>()));
        }

        [Fact]
        public void GetAllCartOrder_ShouldCallCookie_WhenEmptyEmail()
        {
            // Arrange
            _orderRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>>())).Returns(_order);

            // Act
            _orderService.GetAllCartOrder(It.IsAny<string>());

            // Assert
            _orderRepository.Verify(or => or.FirstOrDefault(It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<Expression<Func<Order, object>>>()));
        }

        private void InitializeTestData(out User user, out Order order, out OrderDetail orderDetail)
        {
            var orderBuilder = new OrderBuilder();
            var orderDetailBuilder = new OrderDetailBuilder();

            user = new User
            {
                Email = "user@email.com",
                Name = "name"
            };

            order = orderBuilder.WithCustomerId(new Guid("5245390a-6aaa-4191-35f5-08d5223464b8")).Build();
            var orderDetails = orderDetailBuilder.WithGameKey("csgo").Build();

            order.OrderDetails = new List<OrderDetail>
            {
                orderDetails
            };

            orderDetail = orderDetails;
        }
    }
}