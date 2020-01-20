using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.Services;
using GameStore.Services.Tests.ModelBuilders;
using GameStore.Web.Controllers.API;
using GameStore.Web.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers.API
{
    public class GameApiControllerTest
    {
        private readonly GameApiController _gameController;
        private readonly Game _game;
        private readonly GameViewModel _gameViewModel;
        private readonly FilterDataModel _filterDataModel;
        private readonly Publisher _publisher;

        public GameApiControllerTest()
        {
            InitializeTestData(out _game, out _publisher, out var displayComment, out _gameViewModel,
                out _filterDataModel);

            var gameServiceMock = new Mock<IGameService>();
            var publisherService = new Mock<IPublisherService>();
            var commentService = new Mock<ICommentService>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(a => a.Map<Game, GameViewModel>(It.IsAny<Game>())).Returns(_gameViewModel);
            mapperMock.Setup(a => a.Map<GameViewModel, Game>(It.IsAny<GameViewModel>())).Returns(_game);
            gameServiceMock.Setup(a => a.Get(It.IsAny<string>(), It.IsAny<bool>())).Returns(_game);
            commentService.Setup(x => x.GetAllCommentsByGameKey(It.IsAny<string>()))
                           .Returns(new List<DisplayCommentModel> { displayComment });
            gameServiceMock.Setup(x => x.FilterGames(It.IsAny<FilterDataModel>()))
                            .Returns(new List<Game> { _game });
            publisherService.Setup(x => x.GetPublisherByCompany(It.IsAny<string>(), It.IsAny<bool>()))
                             .Returns(_publisher);

            _gameController = new GameApiController(gameServiceMock.Object,
                mapperMock.Object,
                publisherService.Object,
                commentService.Object);
        }

        [Fact]
        public void Create_ShouldReturnIActionResult_WithGameViewModel()
        {
            var game = _gameViewModel;
            game.Publisher = "publisher";

            var result = _gameController.Create(game) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnIActionResult_WithGameViewModelEmptyKey()
        {
            var game = _gameViewModel;
            game.Key = null;

            var result = _gameController.Create(game) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnIActionResult_WithGameViewModelEmptyName()
        {
            var game = _gameViewModel;
            game.Name = null;

            var result = _gameController.Create(game) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnIActionResult_WithGameViewModelEmptyDescription()
        {
            var game = _gameViewModel;
            game.Description = null;

            var result = _gameController.Create(game) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Update_ShouldReturnIActionResult_WithGameViewModel()
        {
            var result = _gameController.Update(_gameViewModel) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void GetComment_ShouldReturnIActionResult_WithGameKeyAndCommentId()
        {
            var result = _gameController.GetComment(_game.Key, Guid.NewGuid()) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void GetPublisher_ShouldReturnIActionResult_WithGameKey()
        {
            var result = _gameController.GetPublisher(_game.Key) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void GetPlatforms_ShouldReturnIActionResult_WithGameKey()
        {
            var result = _gameController.GetPlatforms(_game.Key) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void GetGenres_ShouldReturnIActionResult_WithGameKey()
        {
            var result = _gameController.GetGenres(_game.Key) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Get_ShouldReturnIActionResult_WithGameKey()
        {
            var result = _gameController.Get(_game.Key) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_ShouldReturnIActionResult_WithFilterDataModel()
        {
            var result = _gameController.GetAll(_filterDataModel) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void GetComments_ShouldReturnIActionResult_WithGameKey()
        {
            var result = _gameController.GetComments(_game.Key) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Remove_ShouldReturnIActionResult_WithGameKey()
        {
            var result = _gameController.Remove(_game.Key) as IActionResult;

            Assert.NotNull(result);
        }

        private void InitializeTestData(out Game game,
            out Publisher publisher,
            out DisplayCommentModel displayComment,
            out GameViewModel gameViewModel,
            out FilterDataModel filterDataModel)
        {
            var gameBuilder = new GameBuilder();

            publisher = new Publisher
            {
                CompanyName = "name",
                Description = "description",
                HomePage = "homepage",
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Languages = new List<PublisherTranslation>()
            };

            displayComment = new DisplayCommentModel
            {
                Body = "body",
                ChildrenComments = new List<DisplayCommentModel>(),
                CommentId = Guid.NewGuid(),
                GameKey = "c21",
                Name = "name",
                Quote = "quote"
            };

            game = gameBuilder.WithId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8")).WithKey("c21")
                                   .WithName("Cry Souls").WithDescription("Cry Souls desc").WithUnitsInStock(10).WithPrice(10).WithPublisher("Unknown").Build();

            game.Publisher = _publisher;
            game.GamePlatforms = new List<GamePlatform> { new GamePlatform() };
            game.GameGenres = new List<GameGenre> { new GameGenre() };

            gameViewModel = new GameViewModel
            {
                Description = "Desc",
                DescriptionRu = "Desc ru",
                Discontinued = false,
                GameGenres = new List<string>(),
                GamePlatforms = new List<string>(),
                IsDeleted = false,
                Key = "key",
                Name = "name",
                NameRu = "name ru",
                Price = 1
            };

            filterDataModel = new FilterDataModel
            {
                CurrentPage = 1,
                Genres = new List<string>(),
                ItemsPerPage = 10,
                Platforms = new List<string>(),
                PriceFrom = 0,
                PriceTo = 0,
                Publishers = new List<string>(),
                ReleaseDate = ReleaseDate.LastMonth,
                SearchString = "search",
                SortType = SortType.MostCommented
            };
        }
    }
}