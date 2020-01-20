using System;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.Services;
using GameStore.Services.Tests.ModelBuilders;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class GameControllerTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly GameController _gameController;
        private readonly Game _game;
        private readonly GameViewModel _gameViewModel;

        public GameControllerTest()
        {
            InitializeTestData(out _game, out _gameViewModel, out var publisher);

            var gameServiceMock = new Mock<IGameService>();
            var publisherService = new Mock<IPublisherService>();
            var platformService = new Mock<IPlatformService>();
            var commentService = new Mock<ICommentService>();
            var genreService = new Mock<IGenreService>();
            _mapperMock = new Mock<IMapper>();

            _mapperMock.Setup(a => a.Map<Game, GameViewModel>(It.IsAny<Game>())).Returns(_gameViewModel);
            _mapperMock.Setup(a => a.Map<GameViewModel, Game>(It.IsAny<GameViewModel>())).Returns(_game);
            publisherService.Setup(a => a.GetPublisherByCompany(It.IsAny<string>(), true)).Returns(publisher);
            gameServiceMock.Setup(a => a.Get(It.IsAny<string>(), true)).Returns(_game);

            _gameController = new GameController(
                gameServiceMock.Object,
                _mapperMock.Object,
                publisherService.Object,
                platformService.Object,
                genreService.Object,
                commentService.Object);
        }

        [Fact]
        public void Create_ReturnsViewResultWhenRequest()
        {
            // Act
            var result = _gameController.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Publish_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _gameController.Publish(_game.Key) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreatePost_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _gameController.Create(_gameViewModel) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreatePost_ReturnsIActionResult_WhenNotValid()
        {
            // Act
            var result = _gameController.Create(new GameViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DetailsByKey_ReturnsViewResultWhenRequest()
        {
            // Act
            var result = _gameController.GetDetails(_game.Key) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _gameController.Delete(_game.Key) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsIActionResultWhenRequest()
        {
            // Arrange
            _mapperMock.Setup(m => m.Map<GameViewModel>(It.IsAny<Game>())).Returns(new GameViewModel());

            // Act
            var result = _gameController.Edit(_game.Key) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsViewResultWhenRequest()
        {
            // Act
            var result = _gameController.Edit(_gameViewModel) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        private void InitializeTestData(out Game game, out GameViewModel gameViewModel, out Publisher publisher)
        {
            var gameBuilder = new GameBuilder();
            var publisherBuilder = new PublisherBuilder();

            game = gameBuilder.WithId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8"))
                                .WithKey("c21")
                                .WithName("Cry Souls")
                                .WithDescription("Cry Souls desc")
                                .WithUnitsInStock(10)
                                .WithPrice(10)
                                .WithPublisher("Unknown")
                                .Build();

            gameViewModel = new GameViewModel()
            {
                Key = "c21",
                Name = "Cry Souls55",
                Description = "Cry Souls 4 desc",
                UnitsInStock = 10,
                Price = 10,
                Publisher = "publisher"
            };

            publisher = publisherBuilder.WithCompanyName("Unknown").Build();
        }
    }
}

