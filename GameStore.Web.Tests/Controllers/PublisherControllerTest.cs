using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels.Publisher;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class PublisherControllerTest
    {
        private readonly Mock<IPublisherService> _publisherServiceMock;
        private readonly PublisherController _publisherController;

        public PublisherControllerTest()
        {
            var mapperMock = new Mock<IMapper>();
            _publisherServiceMock = new Mock<IPublisherService>();
            _publisherServiceMock.Setup(p => p.GetPublisherByCompany(It.IsAny<string>(), true)).Returns(new Publisher());

            mapperMock.Setup(m => m.Map<Publisher, PublisherViewModel>(It.IsAny<Publisher>()))
                .Returns(new PublisherViewModel());
            mapperMock.Setup(m => m.Map<PublisherViewModel>(It.IsAny<Publisher>()))
                .Returns(new PublisherViewModel());
            _publisherServiceMock.Setup(p => p.GetPublisherByCompany(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(new Publisher());
            _publisherController = new PublisherController(mapperMock.Object, _publisherServiceMock.Object);
        }

        [Fact]
        public void CreatePublisher_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _publisherController.CreatePublisher() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Index_ReturnsViewResultWhenRequest()
        {
            // Act
            var result = _publisherController.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsViewResultWhenCompanyName()
        {
            // Arrange
            string companyName = "alihandro";

            // Act
            var result = _publisherController.Edit(companyName) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsViewResultWhenPublisherViewModelModel()
        {
            // Arrange
            _publisherServiceMock.Setup(p => p.GetPublisherByCompany(It.IsAny<string>(), false)).Returns(new Publisher());

            // Act
            var result = _publisherController.Edit(new PublisherViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ReturnsIActionResultWhenCompanyName()
        {
            // Arrange
            string companyName = "alihandro";

            // Act
            var result = _publisherController.Delete(companyName) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void PublisherDetails_ReturnsViewResultWhenCompanyName()
        {
            // Arrange
            string companyName = "alihandro";

            // Act
            var result = _publisherController.PublisherDetails(companyName) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreatePublisher_ReturnsIActionResultWhenEntity()
        {
            // Arrange
            var entity = new PublisherViewModel();

            // Act
            var result = _publisherController.CreatePublisher(entity) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsIActionResultWhenCompanyName()
        {
            // Act
            var result = _publisherController.Edit("Company") as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}