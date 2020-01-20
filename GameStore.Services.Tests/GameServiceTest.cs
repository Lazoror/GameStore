using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Autofac.Features.Indexed;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;
using GameStore.Services.Services;
using GameStore.Services.Tests.ModelBuilders;
using Moq;
using Xunit;

namespace GameStore.Services.Tests
{
    public class GameServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IGameRepository> _gameRepository;
        private readonly Mock<ICrudRepository<Genre>> _genreRepository;
        private readonly Mock<ICrudRepository<GameState>> _gameStateRepository;
        private readonly Mock<ICrudRepository<Language>> _languageRepository;
        private readonly Mock<ICrudRepository<GameTranslation>> _gameLangRepository;
        private readonly GameService _gameService;
        private readonly Mock<ITranslateProvider<GameTranslation, Game>> _translationRepository;
        private Platform _platform;
        private Publisher _publisher;
        private Game _game;
        private Genre _genre;

        public GameServiceTest()
        {
            var publisherRepository = new Mock<ICrudRepository<Publisher>>();
            var gamePlatformRepository = new Mock<ICrudRepository<GamePlatform>>();
            var logRepositoryMock = new Mock<IMongoLogger>();
            var gameGenreRepository = new Mock<ICrudRepository<GameGenre>>();
            var platformRepository = new Mock<ICrudRepository<Platform>>();
            var index = new Mock<IIndex<RepositoryType, ICrudRepository<Platform>>>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _gameRepository = new Mock<IGameRepository>();
            _languageRepository = new Mock<ICrudRepository<Language>>();
            _gameLangRepository = new Mock<ICrudRepository<GameTranslation>>();
            _gameStateRepository = new Mock<ICrudRepository<GameState>>();
            _genreRepository = new Mock<ICrudRepository<Genre>>();
            _translationRepository = new Mock<ITranslateProvider<GameTranslation, Game>>();

            InitializeTestData(out _game, out _platform, out _publisher, out _genre);

            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<GamePlatform>>(RepositoryType.SQL)).Returns(gamePlatformRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(_languageRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<GameTranslation>>(RepositoryType.SQL)).Returns(_gameLangRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<GameGenre>>(RepositoryType.SQL)).Returns(gameGenreRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Publisher>>(RepositoryType.SQL)).Returns(publisherRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Genre>>(RepositoryType.SQL)).Returns(_genreRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Platform>>(RepositoryType.SQL)).Returns(platformRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<GameState>>(RepositoryType.SQL)).Returns(_gameStateRepository.Object);

            _gameRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_game);
            _gameRepository.Setup(a => a.GetMany(It.IsAny<int>(), It.IsAny<int>(),
                               It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, object>>>(),
                               SortDirection.Ascending))
                           .Returns(new List<Game> { _game });
            publisherRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns(_publisher);
            _genreRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>(), null)).Returns(_genre);
            platformRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>(), null)).Returns(_platform);
            _gameStateRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<GameState, bool>>>()))
                .Returns(new GameState());
            platformRepository.Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>()))
                .Returns(new Platform());
            _genreRepository.Setup(g => g.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>())).Returns(new Genre());
            _gameLangRepository.Setup(gl => gl.FirstOrDefault(It.IsAny<Expression<Func<GameTranslation, bool>>>()))
                .Returns(new GameTranslation());
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Publisher>>(RepositoryType.SQL)).Returns(publisherRepository.Object);

            index.Setup(g => g[RepositoryType.SQL]).Returns(platformRepository.Object);
            _gameService = new GameService(_unitOfWorkMock.Object,
                logRepositoryMock.Object,
                _translationRepository.Object,
                _gameRepository.Object);
        }

        [Fact]
        public void GetAllGameGenres_ShouldCallGetRepositoryGameGenreOnceWhenGameKey()
        {
            // Arrange
            var gameKey = "csgo";
            _genreRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>())).Returns(_genre);

            // Act
            _gameService.GetAllGameGenres(gameKey);

            // Assert
            _genreRepository.Verify(a => a.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>()), Times.Once);
        }

        [Fact]
        public void DeleteMark_ShouldCallGetSingleOnceWhenGameKey()
        {
            // Arrange
            _genreRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>())).Returns(_genre);

            // Act
            _gameService.Delete(_game.Key);

            // Assert
            _gameRepository.Verify(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Fact]
        public void GetGameByKey_ShouldReturnOneGameWhenNullGenres()
        {
            // Arrange
            var game = _game;
            game.GameGenres = null;

            // Act
            var gameByKey = _gameService.Get(game.Key);

            // Assert
            Assert.Equal(game.Key, gameByKey.Key);
        }

        [Fact]
        public void GetGameByKey_ShouldReturnOneGameWhenNullGenresAndLang()
        {
            // Arrange
            var game = _game;
            game.Languages = new List<GameTranslation> { new GameTranslation() };
            game.GameGenres = new List<GameGenre> { new GameGenre { Genre = null } };
            _gameRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>())).Returns(game);
            _translationRepository.Setup(tr => tr.GetTranslate(It.IsAny<string>(), game)).Returns(game);

            // Act
            var gameByKey = _gameService.Get(game.Key);

            // Assert
            Assert.Equal(game.Key, gameByKey.Key);
        }

        [Fact]
        public void GetGameByKey_ShouldReturnOneGameWhenNullGenresAndLangAndGameStateLangNull()
        {
            // Arrange
            var game = _game;
            game.Languages = new List<GameTranslation> { new GameTranslation() };
            game.GameGenres = new List<GameGenre> { new GameGenre { Genre = null } };
            _gameStateRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<GameState, bool>>>()))
                .Returns((GameState)null);
            _gameRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>())).Returns(game);
            _translationRepository.Setup(tr => tr.GetTranslate(It.IsAny<string>(), game)).Returns(game);

            // Act
            var gameByKey = _gameService.Get(game.Key);

            // Assert
            Assert.Equal(game.Key, gameByKey.Key);
        }

        [Fact]
        public void GetAllGamesByName_ShouldReturnOneGameWhenNullGenres()
        {
            // Arrange
            var game = _game;
            game.GameGenres = null;

            // Act
            _gameService.Get(game.Name);

            // Assert
            _gameRepository.Verify(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Fact]
        public void GetGameByKey_ShouldReturnOneGameWhenGameKey()
        {
            // Act
            var gameByKey = _gameService.Get(_game.Key);

            // Assert
            Assert.Equal(_game.Key, gameByKey.Key);
        }

        [Fact]
        public void CountAllGames_ShouldCallCountOnceWhenRequest()
        {
            // Act
            _gameService.CountAllGames(It.IsAny<Expression<Func<Game, bool>>>());

            // Assert
            _gameRepository.Verify(a => a.Count(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Fact]
        public void CreateGame_ShouldCallCountOnce_WhenGame()
        {
            // Act
            _gameService.CreateGame(_game, new List<string> { "platrofm" }, new List<string> { "genre" });

            // Assert
            _gameRepository.Verify(a => a.InsertFull(It.IsAny<Game>(), It.IsAny<List<string>>(), It.IsAny<List<string>>()), Times.Once);
        }

        [Fact]
        public void EditGame_ShouldCallCountOnce_WhenGame()
        {
            // Act
            _gameService.EditGame(_game, new List<string> { "platrofm" }, new List<string> { "genre" });

            // Assert
            _gameRepository.Verify(a => a.UpdateFull(It.IsAny<Game>(), It.IsAny<List<string>>(), It.IsAny<List<string>>()), Times.Once);
        }

        [Fact]
        public void GetTotalPages_ShouldCallCountOnce_WhengameCountAndItemsPerPage()
        {
            // Act
            var result = _gameService.GetTotalPages(1, 10);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Publish_ShouldCallGetSingleOnceWhenGameKey()
        {
            // Arrange
            var key = "c21";

            // Act
            _gameService.Publish(key);

            // Assert
            _gameRepository.Verify(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Fact]
        public void GetAllGames_ShouldCallGetManyOnceWhenValues()
        {
            // Act
            _gameService.GetAllGames(SortType.None, "ru", It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<int>(), It.IsAny<int>());

            // Assert
            _gameRepository.Verify(
                a => a.GetMany(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<Expression<Func<Game, object>>>(), SortDirection.Ascending),
                Times.Once);
        }

        [Fact]
        public void GetAllGames_ShouldCallGetManyOnceWhenNullValues()
        {
            // Arrange
            var game = new List<Game> { _game, _game };
            game.ForEach(g =>
            {
                foreach (var gameGenre in g.GameGenres)
                {
                    gameGenre.Genre = null;
                }

                foreach (var gamePlatforms in g.GamePlatforms)
                {
                    gamePlatforms.PlatformType = null;
                }
                g.GameState = new GameState();
                g.GameState.Comments = null;

                g.Languages = new List<GameTranslation> { new GameTranslation() };
            });

            _gameRepository.Setup(a => a.GetMany(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<Expression<Func<Game, object>>>(), SortDirection.Ascending))
                .Returns(game);
            _unitOfWorkMock.Setup(u => u.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(_languageRepository.Object);
            _languageRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());

            // Act
            _gameService.GetAllGames(SortType.None, "ru", It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<int>(), It.IsAny<int>());

            // Assert
            _gameRepository.Verify(
                a => a.GetMany(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<Expression<Func<Game, object>>>(), SortDirection.Ascending),
                Times.Once);
        }

        [Fact]
        public void ProcessFiltering_ShouldCallGetManyOnceWhenFilters()
        {
            // Act
            _gameService.FilterGames(new FilterDataModel());

            // Assert
            _gameRepository.Verify(
                a => a.GetMany(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<Expression<Func<Game, object>>>(), SortDirection.Ascending), Times.Once);
        }

        [Fact]
        public void AddGameTranslate_ShouldCallGetManyOnceWhenParams()
        {
            // Act
            _gameService.AddGameTranslate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>());

            // Assert
            _translationRepository.Verify(a => a.AddTranslate(It.IsAny<GameTranslation>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void AddGameTranslate_ShouldCallGetManyOnceWhenNulGameLang()
        {
            _gameLangRepository.Setup(gl => gl.FirstOrDefault(It.IsAny<Expression<Func<GameTranslation, bool>>>()))
                .Returns((GameTranslation)null);
            _languageRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());

            // Act
            _gameService.AddGameTranslate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>());

            // Assert
            _translationRepository.Verify(a => a.AddTranslate(It.IsAny<GameTranslation>(), It.IsAny<string>()), Times.Once);
        }

        private void InitializeTestData(out Game game,
            out Platform platform,
            out Publisher publisher,
            out Genre genre)
        {
            var genreBuilder = new GenreBuilder();
            var gameBuilder = new GameBuilder();
            var gamePlatformBuilder = new GamePlatformBuilder();
            var publisherBuilder = new PublisherBuilder();
            var gameGenreBuilder = new GameGenreBuilder();
            var platformTypeBuilder = new PlatformTypeBuilder();

            game = gameBuilder.WithId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8")).WithKey("c21")
                                   .WithName("Cry Souls").WithDescription("Cry Souls desc").WithUnitsInStock(10).WithPrice(10).WithPublisher("Unknown").Build();

            game.GameGenres = new List<GameGenre>
            {
                gameGenreBuilder.WithGenre("Sport").Build()
            };

            game.GamePlatforms = new List<GamePlatform>
            {
                gamePlatformBuilder.WithPlatformType("Desktop").Build()
            };

            platform = platformTypeBuilder.WithId(Guid.NewGuid()).WithType("Desktop").Build();
            publisher = publisherBuilder.WithCompanyName("Unknown").Build();

            genre = genreBuilder.WithName("Sport").WithId(new Guid("1255390a-6aaa-4191-35f5-08d7223464b8")).Build();
        }
    }
}