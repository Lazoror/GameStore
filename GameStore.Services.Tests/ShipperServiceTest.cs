using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Models.MongoModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.Services;
using GameStore.Services.Services;
using Moq;
using Xunit;

namespace GameStore.Services.Tests
{
    public class ShipperServiceTest
    {
        private readonly IShipperService _shipperService;
        private readonly Mock<IMongoReadOnlyRepository<ShipperModel>> _shipperRepository;

        public ShipperServiceTest()
        {
            var shipperModel = new ShipperModel
            {
                CompanyName = "name",
                Id = "id",
                Phone = "phone",
                ShipperId = 1
            };

            _shipperRepository = new Mock<IMongoReadOnlyRepository<ShipperModel>>();

            _shipperRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<ShipperModel, bool>>>()))
                              .Returns(shipperModel);

            _shipperRepository.Setup(x => x.GetMany(It.IsAny<int>(), It.IsAny<int>(), null, null,
                                  It.IsAny<SortDirection>(), It.IsAny<Expression<Func<ShipperModel, object>>[]>()))
                              .Returns(new List<ShipperModel> { shipperModel });

            _shipperService = new ShipperService(_shipperRepository.Object);
        }

        [Fact]
        public void GetShipperByName_ShouldCallOnce_WhenName()
        {
            // Act
            var result = _shipperService.GetShipperByName("name");

            // Assert
            _shipperRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<ShipperModel, bool>>>()));
        }

        [Fact]
        public void GetAllShippers_ShouldCallOnce_WhenEmpty()
        {
            // Act
            _shipperService.GetAllShippers();

            // Assert
            _shipperRepository.Verify(x => x.GetMany(It.IsAny<int>(), It.IsAny<int>(), null, null,
                It.IsAny<SortDirection>(), It.IsAny<Expression<Func<ShipperModel, object>>[]>()), Times.Once);
        }
    }
}