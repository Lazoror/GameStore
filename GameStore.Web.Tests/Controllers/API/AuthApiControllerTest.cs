using GameStore.Interfaces.Services;
using GameStore.Interfaces.Web.Settings;
using GameStore.Web.Controllers.API;
using GameStore.Web.ViewModels.Account;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers.API
{
    public class AuthApiControllerTest
    {
        private readonly AuthApiController _authController;

        public AuthApiControllerTest()
        {
            var userService = new Mock<IUserService>();
            var tokenFactory = new Mock<ITokenGenerator>();

            _authController = new AuthApiController(userService.Object, tokenFactory.Object);
        }

        [Fact]
        public void Token_ShouldReturnIActionResult_WhenLoginViewModel()
        {
            var result = _authController.Token(new LoginViewModel { ReturnUrl = "return url" });

            Assert.NotNull(result);
        }
    }
}