using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.Indexed;
using AutoMapper;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using GameStore.Services.Tests.ModelBuilders;
using GameStore.Web.Controllers;
using GameStore.Web.Payment;
using GameStore.Web.ViewModels.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IGameService> _gameServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrderController _orderController;
        private readonly Mock<IUserService> _userService;
        private readonly CardViewModel _cardViewModel;
        private readonly List<OrderDetailViewModel> _orderDetailViewModel;
        private readonly Game _game;

        public OrderControllerTest()
        {
            InitializeTestData(out _game, out _cardViewModel, out _orderDetailViewModel, out var orderDetail);

            var orderServiceMock = new Mock<IOrderService>();
            var paymentResolver = new Mock<IIndex<PaymentType, IPayment>>();
            var actionResultMock = new Mock<IActionResult>();
            var shipperServiceMock = new Mock<IShipperService>();
            var httpContext = new Mock<HttpContext>();

            _mapperMock = new Mock<IMapper>();
            _gameServiceMock = new Mock<IGameService>();
            _userService = new Mock<IUserService>();

            httpContext.Setup(g => g.User.Identity.Name).Returns("user");
            var actionContext = new ActionContext();
            actionContext.HttpContext = httpContext.Object;
            actionContext.ActionDescriptor = new ControllerActionDescriptor();
            actionContext.RouteData = new RouteData();

            var controllerContext = new ControllerContext(actionContext);

            _userService.Setup(u => u.GetUserByEmail("user")).Returns(new User());
            orderServiceMock.Setup(a => a.GetAllCartOrder(It.IsAny<string>())).Returns(new Order { OrderDetails = orderDetail.ToList() });
            paymentResolver.Setup(pr => pr[It.IsAny<PaymentType>()].Process(It.IsAny<ProcessPaymentModel>()))
                .Returns(actionResultMock.Object);

            _orderController = new OrderController(
                    orderServiceMock.Object,
                    paymentResolver.Object,
                    shipperServiceMock.Object,
                    _userService.Object)
            { ControllerContext = controllerContext };
        }

        [Fact]
        public void Order_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _orderController.Order() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void History_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _orderController.History() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Pay_ReturnsIActionResultWhenPaymentType()
        {
            // Act
            var result = _orderController.Pay(PaymentType.Bank, "shipper") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Card_ReturnsIActionResultWhenPaymentType()
        {
            // Act
            var result = _orderController.Card(_cardViewModel) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void History_ReturnsIActionResultWhenDates()
        {
            // Act
            var result = _orderController.History(DateTime.MinValue, DateTime.MaxValue) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Order_ReturnsIActionResultWhenOrderDEtails()
        {
            // Arrange
            _mapperMock.Setup(a =>
                    a.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailViewModel>>(It.IsAny<IEnumerable<OrderDetail>>()))
                .Returns(_orderDetailViewModel);
            _gameServiceMock.Setup(g => g.Get(It.IsAny<string>(), true)).Returns(_game);

            // Act
            var result = _orderController.Order() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Card_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _orderController.Card() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ManageOrders_ReturnsIActionResult_WhenRequest()
        {
            // Act
            var result = _orderController.ManageOrders() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void SetShippedStatus_ReturnsIActionResult_WhenRequest()
        {
            // Act
            var guid = Guid.NewGuid();
            _userService.Setup(x => x.GetUserById(guid)).Returns(new User());
            var result = _orderController.SetShippedStatus(guid) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        private void InitializeTestData(out Game game,
            out CardViewModel cardViewModel,
            out List<OrderDetailViewModel> orderDetailView,
            out List<OrderDetail> orderDetails)
        {
            var gameBuilder = new GameBuilder();
            var orderDetailBuilder = new OrderDetailBuilder();

            game = gameBuilder.WithId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8"))
                                .WithKey("c21")
                                .WithName("Cry Souls")
                                .WithDescription("Cry Souls desc")
                                .WithUnitsInStock(10)
                                .WithPrice(10)
                                .WithPublisher("Unknown")
                                .Build();

            cardViewModel = new CardViewModel
            {
                CardNumber = 123123123123123,
                CartHolderName = "card",
                MonthExpiry = 1,
                SecurityCode = 1,
                YearExpiry = 2000
            };

            orderDetailView = new List<OrderDetailViewModel>
            {
                new OrderDetailViewModel
                {
                    Discount = 1,
                    GameKey = "key",
                    GameName = "gameName",
                    Platforms = new List<string>(),
                    Price = 1,
                    Quantity = 1
                }
            };

            orderDetails = new List<OrderDetail>
            {
                orderDetailBuilder.WithGameKey("csgo").Build()
            };
        }
    }
}