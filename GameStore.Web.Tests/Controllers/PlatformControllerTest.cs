using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels.Platform;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class PlatformControllerTest
    {
        private readonly PlatformController _platformController;

        public PlatformControllerTest()
        {
            var platformServiceMock = new Mock<IPlatformService>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Platform, PlatformTypeViewModel>(It.IsAny<Platform>()))
                .Returns(new PlatformTypeViewModel());
            mapperMock.Setup(m => m.Map<PlatformTypeViewModel>(It.IsAny<Platform>()))
                .Returns(new PlatformTypeViewModel());
            platformServiceMock.Setup(ps => ps.Get(It.IsAny<string>(), true))
                .Returns(new Platform());

            _platformController = new PlatformController(platformServiceMock.Object, mapperMock.Object);
        }

        [Fact]
        public void Create_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _platformController.Create() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Index_ReturnsViewResultWhenRequest()
        {
            // Act
            var result = _platformController.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsIActionResultWhenPlatformName()
        {
            // Arrange
            string platformName = "Desktop";

            // Act
            var result = _platformController.Edit(platformName) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsIActionResultWhenPlatformTypeViewModelModel()
        {
            // Act
            var result = _platformController.Edit(new PlatformTypeViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ReturnsIActionResultWhenPlatformName()
        {
            // Arrange
            string platformName = "Desktop";

            // Act
            var result = _platformController.Delete(platformName) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void AddGamePlatform_ReturnsIActionResultWhenEntity()
        {
            // Act
            var result = _platformController.Create(new PlatformTypeViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsIActionResultWhenEntityAndPlatform()
        {
            // Arrange
            string platformName = "Desktop";

            // Act
            var result = _platformController.Edit(platformName) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}