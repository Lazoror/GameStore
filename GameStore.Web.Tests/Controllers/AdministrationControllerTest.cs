using AutoMapper;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class AdministrationControllerTest
    {
        private readonly Mock<IUserService> _userService;
        private readonly AdministrationController _administrationController;

        public AdministrationControllerTest()
        {
            var mapper = new Mock<IMapper>();
            _userService = new Mock<IUserService>();

            var httpContext = new Mock<HttpContext>();

            httpContext.Setup(g => g.User.Identity.Name).Returns("user");
            var actionContext = new ActionContext();
            actionContext.HttpContext = httpContext.Object;
            actionContext.ActionDescriptor = new ControllerActionDescriptor();
            actionContext.RouteData = new RouteData();

            var controllerContext = new ControllerContext(actionContext);

            _administrationController = new AdministrationController(_userService.Object, mapper.Object)
            {
                ControllerContext = controllerContext
            };

        }

        [Fact]
        public void CreateRole_ShouldReturnIActionResult_WhenRequest()
        {
            // Act
            var result = _administrationController.CreateRole("role") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ManageUsers_ShouldReturnIActionResult_WhenNoParameters()
        {
            // Act
            var result = _administrationController.ManageUsers() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ManageRoles_ShouldReturnIActionResult_WhenNoParameters()
        {
            // Act
            var result = _administrationController.ManageRoles() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ChangeRole_ShouldReturnIActionResult_WhenEmail()
        {
            // Act
            var result = _administrationController.ChangeRole("email") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EditRole_ShouldReturnIActionResult_WhenEditRole()
        {
            // Act
            var result = _administrationController.EditRole("Role") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ChangeRole_ShouldReturnIActionResult_WhenChangeRoleUserViewModelModel()
        {
            // Act
            var result = _administrationController.ChangeRole(new ChangeRoleUserViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateRole_ShouldReturnIActionResult_WhenRole()
        {
            // Act
            var result = _administrationController.CreateRole("role") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EditRole_ShouldReturnIActionResult_WhenEditRoleViewModelModel()
        {
            // Act
            var result = _administrationController.EditRole(new EditRoleViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EditRole_ShouldReturnIActionResult_WhenUserRole()
        {
            // Act
            var result = _administrationController.EditRole(new EditRoleViewModel { Name = "User" }) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EditRole_ShouldReturnIActionResult_WhenAdminRole()
        {
            // Act
            var result = _administrationController.EditRole(new EditRoleViewModel { Name = "Administrator" }) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateRole_ShouldReturnIActionResult_WhenEmpty()
        {
            _userService.Setup(us => us.GetUserByEmail("email")).Returns(new User());

            // Act
            var result = _administrationController.CreateRole() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteUser_ShouldReturnIActionResult_WhenDeleteUserViewModel()
        {
            _userService.Setup(us => us.GetUserByEmail("email")).Returns(new User());

            // Act
            var result = _administrationController.DeleteUser(new DeleteUserViewModel { Email = "email" }, "Delete") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void DeleteUser_ShouldReturnIActionResult_WhenUserNull()
        {
            _userService.Setup(us => us.GetUserByEmail("email")).Returns((User)null);

            // Act
            var result = _administrationController.DeleteUser(new DeleteUserViewModel { Email = "email" }, "Delete") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteUser_ShouldReturnIActionResult_WhenUserDeleted()
        {
            _userService.Setup(us => us.GetUserByEmail("email")).Returns(new User { IsDeleted = true });

            // Act
            var result = _administrationController.DeleteUser(new DeleteUserViewModel { Email = "user" }, "Delete") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteUser_ShouldReturnIActionResult_WhenEmail()
        {
            _userService.Setup(us => us.GetUserByEmail("email")).Returns(new User { IsDeleted = true });

            // Act
            var result = _administrationController.DeleteUser("emailUser") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteRole_ShouldReturnIActionResult_WhenRole()
        {
            _userService.Setup(us => us.GetUserByEmail("email")).Returns(new User { IsDeleted = true });

            // Act
            var result = _administrationController.DeleteRole("role") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteUser_ShouldReturnIActionResult_WhenLastAdmin()
        {
            _userService.Setup(us => us.GetUserByEmail("email")).Returns(new User { IsDeleted = true });
            _userService.Setup(us => us.IsLastAdmin("email")).Returns(true);

            // Act
            var result = _administrationController.DeleteUser(new DeleteUserViewModel { Email = "email" }, "Delete") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}