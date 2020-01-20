using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class BasketControllerTest
    {
        private readonly BasketController _basketController;

        public BasketControllerTest()
        {
            InitializeTestData(out var order);

            var mapperMock = new Mock<IMapper>();
            var orderServiceMock = new Mock<IOrderService>();
            var gameServiceMock = new Mock<IGameService>();
            var httpContext = new Mock<HttpContext>();

            httpContext.Setup(g => g.User.Identity.Name).Returns("user");
            var actionContext = new ActionContext();
            actionContext.HttpContext = httpContext.Object;
            actionContext.ActionDescriptor = new ControllerActionDescriptor();
            actionContext.RouteData = new RouteData();

            var controllerContext = new ControllerContext(actionContext);

            gameServiceMock.Setup(g => g.Get(It.IsAny<string>(), true)).Returns(new Game());
            orderServiceMock.Setup(o => o.GetAllCartOrder(It.IsAny<string>())).Returns(order);

            _basketController = new BasketController(orderServiceMock.Object,
                mapperMock.Object,
                gameServiceMock.Object);
            _basketController.ControllerContext = controllerContext;
        }

        [Fact]
        public void Delete_ReturnsIActionResultWhenGameKey()
        {
            // Arrange
            string gameKey = "csgo";

            // Act
            var result = _basketController.Delete(gameKey) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Index_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _basketController.Index() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ReturnsIActionResultWhenGameKeyAndQuantityAndPrice()
        {
            // Arrange
            string gameKey = "csgo";

            // Act
            var result = _basketController.AddToBasket(gameKey) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        private void InitializeTestData(out Order order)
        {
            order = new Order
            {
                OrderDetails = new List<OrderDetail>()
            };
        }
    }
}