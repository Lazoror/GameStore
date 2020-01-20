using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Controllers.API;
using GameStore.Web.ViewModels.Publisher;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers.API
{
    public class PublisherApiControllerTest
    {
        private readonly PublisherApiController _publisherController;
        private readonly PublisherViewModel _publisherViewModel;

        public PublisherApiControllerTest()
        {
            InitializeTestData(out _publisherViewModel);

            var mapperMock = new Mock<IMapper>();
            var publisherServiceMock = new Mock<IPublisherService>();

            publisherServiceMock.Setup(x => x.GetPublisherByCompany(It.IsAny<string>(), It.IsAny<bool>()))
                                 .Returns(new Publisher());
            publisherServiceMock.Setup(x => x.GetAllPublishers())
                                 .Returns(new List<Publisher> { new Publisher() });

            _publisherController = new PublisherApiController(publisherServiceMock.Object, mapperMock.Object);
        }

        [Fact]
        public void Update_ShouldReturnIActionResult_WhenPublisherViewModel()
        {
            var result = _publisherController.Update(_publisherViewModel) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnIActionResult_WhenPublisherViewModel()
        {
            var result = _publisherController.Create(_publisherViewModel) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Get_ShouldReturnIActionResult_WhenCompanyName()
        {
            var result = _publisherController.Get(_publisherViewModel.CompanyName) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_ShouldReturnIActionResult_WhenEmpty()
        {
            var result = _publisherController.GetAll() as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Remove_ShouldReturnIActionResult_WhenCompanyName()
        {
            var result = _publisherController.Remove(_publisherViewModel.CompanyName) as IActionResult;

            Assert.NotNull(result);
        }

        private void InitializeTestData(out PublisherViewModel publisherViewModel)
        {
            publisherViewModel = new PublisherViewModel
            {
                CompanyName = "name",
                CompanyNameRu = "name ru",
                Description = "desc",
                DescriptionRu = "desc ru",
                HomePage = "homePage",
                HomePageRu = "homePage ru",
                OldCompanyName = "old name"
            };
        }
    }
}