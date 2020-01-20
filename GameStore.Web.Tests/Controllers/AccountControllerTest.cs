using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class AccountControllerTest
    {
        private readonly Mock<IUserService> _userService;
        private Order _order;

        private readonly AccountController _accountController;

        public AccountControllerTest()
        {
            InitializeTestData(out _order);

            _userService = new Mock<IUserService>();
            var orderService = new Mock<IOrderService>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

            var httpContext = new Mock<HttpContext>();
            var cookieCollection = new Mock<IRequestCookieCollection>();
            var cookieResponse = new Mock<IResponseCookies>();

            contextAccessor.Setup(x => x.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(hc => hc.Response.Cookies).Returns(cookieResponse.Object);
            cookieCollection.Setup(cc => cc["Order"]).Returns(JsonConvert.SerializeObject(_order));

            httpContext.Setup(hc => hc.Request.Cookies).Returns(cookieCollection.Object);
            httpContext.Setup(hc => hc.Request.Cookies.ContainsKey("Order")).Returns(true);

            orderService.Setup(x => x.GetOrderById(It.IsAny<Guid>()))
                         .Returns(_order);

            orderService.Setup(x => x.GetAllCartOrder(It.IsAny<string>()))
                         .Returns(_order);

            var actionContext = new ActionContext
            {
                HttpContext = httpContext.Object,
                ActionDescriptor = new ControllerActionDescriptor(),
                RouteData = new RouteData()
            };

            var controllerContext = new ControllerContext(actionContext);

            _accountController = new AccountController(_userService.Object, orderService.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Fact]
        public void Login_ShouldReturnIActionResult_WhenLoginModelAndNullUser()
        {
            var result = _accountController.Login(new LoginViewModel()) as Task<IActionResult>;

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Login_ShouldReturnIActionResult_WhenLoginModelAndDeletedUser()
        {
            _userService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(new User { IsDeleted = true, Id = Guid.NewGuid() });

            var result = _accountController.Login(new LoginViewModel()) as Task<IActionResult>;

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Login_ShouldReturnIActionResult_WhenLoginModelValid()
        {
            var loginModel = new LoginViewModel
            {
                Email = "email",
                Password = "123123",
            };

            var user = new User
            {
                Email = "email",
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Name = "name",
                Password = "AN7BSusv8nJ2oa46X2c25VDNyJZKvjg9H4niysKHq5PLOknxOpRaUIOaU1Y6JW1XzQ==",
                Roles = new List<UserRole>
                {
                    new UserRole
                    {
                        Role = new Role
                        {
                            Name = "role"
                        },
                        RoleId = Guid.NewGuid(),
                        User = new User(),
                        UserId = Guid.NewGuid()
                    }
                }
            };

            _userService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(user);

            var result = _accountController.Login(loginModel) as Task<IActionResult>;

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Register_ShouldReturnIActionResult_WhenRegisterModel()
        {
            var result = _accountController.Register(new RegisterViewModel()) as Task<IActionResult>;

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Logout_ShouldReturnIActionResult_WhenEmpty()
        {
            var result = _accountController.Logout() as Task<IActionResult>;

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Login_ShouldReturnIActionResult_WhenReturnUrl()
        {
            var result = _accountController.Login("returnUrl") as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void AccessDenied_ShouldReturnIActionResult_WhenReturnUrl()
        {
            var result = _accountController.AccessDenied("returnUrl") as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Register_ShouldReturnIActionResult_WhenReturnUrl()
        {
            var result = _accountController.Register() as IActionResult;

            Assert.NotNull(result);
        }

        private void InitializeTestData(out Order order)
        {
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
                Shipper = "shipper",
            };
        }
    }
}