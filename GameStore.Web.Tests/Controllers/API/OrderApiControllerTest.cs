
using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using GameStore.Services.Tests.ModelBuilders;
using GameStore.Web.Controllers.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers.API
{
    public class OrderApiControllerTest
    {
        private readonly OrderApiController _orderController;
        private readonly Game _game;

        public OrderApiControllerTest()
        {
            InitializeTestData(out var order, out _game);

            var orderServiceMock = new Mock<IOrderService>();
            var mapperMock = new Mock<IMapper>();
            var gameServiceMock = new Mock<IGameService>();

            gameServiceMock.Setup(a => a.Get(It.IsAny<string>(), It.IsAny<bool>())).Returns(_game);
            orderServiceMock.Setup(x => x.GetAllCartOrder(It.IsAny<string>()))
                             .Returns(order);
            orderServiceMock.Setup(x => x.GetOrderById(It.IsAny<Guid>()))
                             .Returns(order);

            var httpContext = new Mock<HttpContext>();

            httpContext.Setup(g => g.User.Identity.Name).Returns("user");

            var actionContext = new ActionContext
            {
                HttpContext = httpContext.Object,
                ActionDescriptor = new ControllerActionDescriptor(),
                RouteData = new RouteData()
            };

            var controllerContext = new ControllerContext(actionContext);

            _orderController = new OrderApiController(orderServiceMock.Object,
                mapperMock.Object,
                gameServiceMock.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Fact]
        public void UpdateQuantity_ShouldReturnIActionResult_WithGameKeyAndQuantity()
        {
            var result = _orderController.UpdateQuantity(_game.Key, 1);

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnIActionResult_WithGameKey()
        {
            var result = _orderController.Create(_game.Key);

            Assert.NotNull(result);
        }

        [Fact]
        public void Remove_ShouldReturnIActionResult_WithGameKey()
        {
            var result = _orderController.Remove(_game.Key);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetCurrent_ShouldReturnIActionResult_WithEmpty()
        {
            var result = _orderController.GetCurrent();

            Assert.NotNull(result);
        }

        [Fact]
        public void Get_ShouldReturnIActionResult_WithOrderId()
        {
            var result = _orderController.Get(Guid.NewGuid());

            Assert.NotNull(result);
        }

        private void InitializeTestData(out Order order, out Game game)
        {
            var gameBuilder = new GameBuilder();

            order = new Order
            {
                CustomerId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                OrderDate = DateTime.MinValue,
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail
                    {
                        GameKey = "c21"
                    }
                },
                OrderStatus = OrderStatus.New,
                ShippedDate = DateTime.MinValue,
                Shipper = "shipper"
            };

            game = gameBuilder.WithId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8")).WithKey("c21")
                                   .WithName("Cry Souls").WithDescription("Cry Souls desc").WithUnitsInStock(10).WithPrice(10).WithPublisher("Unknown").Build();

            game.GamePlatforms = new List<GamePlatform> { new GamePlatform() };
            game.GameGenres = new List<GameGenre> { new GameGenre() };
        }
    }
}