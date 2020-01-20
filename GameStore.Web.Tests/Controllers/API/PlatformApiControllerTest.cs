using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Controllers.API;
using GameStore.Web.ViewModels.Platform;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers.API
{
    public class PlatformApiControllerTest
    {
        private readonly PlatformApiController _platformController;
        private readonly PlatformTypeViewModel _platformTypeViewModel;

        public PlatformApiControllerTest()
        {
            InitializeTestData(out _platformTypeViewModel);

            var platformServiceMock = new Mock<IPlatformService>();
            var mapperMock = new Mock<IMapper>();

            platformServiceMock.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<bool>()))
                                .Returns(new Platform());

            platformServiceMock.Setup(x => x.GetAllPlatform())
                                .Returns(new List<Platform> { new Platform() });

            _platformController = new PlatformApiController(platformServiceMock.Object, mapperMock.Object);
        }

        [Fact]
        public void Create_ShouldReturnIActionResult_WhenPlatformTypeViewModel()
        {
            var result = _platformController.Create(_platformTypeViewModel);

            Assert.NotNull(result);
        }

        [Fact]
        public void Update_ShouldReturnIActionResult_WhenPlatformTypeViewModel()
        {
            var result = _platformController.Update(_platformTypeViewModel);

            Assert.NotNull(result);
        }

        [Fact]
        public void Get_ShouldReturnIActionResult_WhenPlatformName()
        {
            var result = _platformController.Get("platformName");

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_ShouldReturnIActionResult_WhenEmpty()
        {
            var result = _platformController.GetAll();

            Assert.NotNull(result);
        }

        [Fact]
        public void Remove_ShouldReturnIActionResult_WhenPlatformName()
        {
            var result = _platformController.Remove("platformName");

            Assert.NotNull(result);
        }

        private void InitializeTestData(out PlatformTypeViewModel platformTypeViewModel)
        {
            platformTypeViewModel = new PlatformTypeViewModel
            {
                Name = "name",
                NameRu = "name ru",
                OldName = "old name"
            };
        }
    }
}