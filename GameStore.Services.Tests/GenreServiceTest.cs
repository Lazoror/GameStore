using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
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
    public class GenreServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IGameRepository> _gameRepository = new Mock<IGameRepository>();
        private readonly Mock<ICrudRepository<Genre>> _genreRepository = new Mock<ICrudRepository<Genre>>();
        private readonly Mock<ICrudRepository<GameGenre>> _gameGenreRepository = new Mock<ICrudRepository<GameGenre>>();
        private readonly Mock<ICrudRepository<GenreTranslation>> _genreLangRepository =
            new Mock<ICrudRepository<GenreTranslation>>();
        private readonly Mock<ICrudRepository<Publisher>> _publisherRepository = new Mock<ICrudRepository<Publisher>>();
        private readonly Mock<ITranslateProvider<GenreTranslation, Genre>> _translationRepository =
            new Mock<ITranslateProvider<GenreTranslation, Genre>>();
        private readonly Mock<ICrudRepository<Language>> _languageRepository = new Mock<ICrudRepository<Language>>();
        private readonly Mock<IMongoLogger> _logRepositoryMock = new Mock<IMongoLogger>();
        private readonly GenreService _genreService;
        private readonly Genre _genre;
        private readonly Game _game;
        private readonly List<Genre> _genres;

        public GenreServiceTest()
        {
            InitializeTestData(out _genre, out _game, out _genres);

            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Genre>>(RepositoryType.SQL)).Returns(_genreRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<GameGenre>>(RepositoryType.SQL)).Returns(_gameGenreRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Publisher>>(RepositoryType.SQL)).Returns(_publisherRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<GenreTranslation>>(RepositoryType.SQL)).Returns(_genreLangRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(_languageRepository.Object);

            _gameRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_game);
            _genreLangRepository.Setup(gl => gl.FirstOrDefault(It.IsAny<Expression<Func<GenreTranslation, bool>>>()))
                .Returns(new GenreTranslation());
            _genreRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>(),
                It.IsAny<Expression<Func<Genre, object>>>())).Returns(_genre);

            _genreRepository.Setup(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<Expression<Func<Genre, object>>>(), It.IsAny<SortDirection>(), It.IsAny<Expression<Func<Genre, object>>[]>()))
                .Returns(_genres);

            _publisherRepository.Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<Publisher, bool>>>()))
                .Returns(new Publisher());

            _genreService = new GenreService(_unitOfWorkMock.Object,
                _logRepositoryMock.Object,
                _translationRepository.Object);
        }

        [Fact]
        public void CreateGenre_ShouldCallInsertGenreOnceWhenEntity()
        {
            // Act
            _genreService.CreateGenre(_genre);

            // Assert
            _genreRepository.Verify(a => a.Insert(It.IsAny<Genre>()), Times.Once);
        }

        [Fact]
        public void EditGenre_ShouldCallUpdateGenreOnceWhenEntity()
        {
            // Act
            _genreService.EditGenre(_genre);

            // Assert
            _genreRepository.Verify(a => a.Update(It.IsAny<Genre>()), Times.Once);
        }

        [Fact]
        public void GetAllGenres_ShouldReturnAllGenresWhenNoParameters()
        {
            // Act
            _genreRepository.Setup(g => g.GetMany(0, int.MaxValue, null, null, It.IsAny<SortDirection>(), It.IsAny<Expression<Func<Genre, object>>[]>())).Returns(_genres);

            var result = _genreService.GetAllGenres();

            // Assert
            Assert.Equal(result.Count(), _genres.Count());
        }

        [Fact]
        public void GetAllGenreNames_ShouldReturnAllGenresWhenNoParameters()
        {
            // Arrange
            _genreRepository.Setup(g => g.GetMany(0, int.MaxValue, null, null, It.IsAny<SortDirection>(), It.IsAny<Expression<Func<Genre, object>>[]>())).Returns(_genres);

            // Act
            var result = _genreService.GetAllGenreNames();

            // Assert
            Assert.Equal(result.Count(), _genres.Count());
        }

        [Fact]
        public void GetGenreByName_ShouldReturnGenreWhenName()
        {
            // Arrange
            _genreRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>(),
                It.IsAny<Expression<Func<Genre, object>>[]>())).Returns(_genre);

            // Act
            var result = _genreService.GetGenreByName(_genre.Name);

            // Assert
            Assert.Equal(result.Name, _genre.Name);
        }

        [Fact]
        public void GetGenreById_ShouldReturnGenreWhenGenreId()
        {
            // Arrange
            _genreRepository.Setup(g => g.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>())).Returns(_genre);

            // Act
            _genreService.GetGenreById(_genre.Id);

            // Assert
            _genreRepository.Verify(a => a.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>()), Times.Once);
        }

        [Fact]
        public void SetDeleteMark_ShouldCallUpdateGenreOnceWhenGenreName()
        {
            // Arrange
            _genreRepository.Setup(g => g.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>())).Returns(_genre);

            // Act
            _genreService.Delete(_genre.Name);

            // Assert
            _genreRepository.Verify(a => a.Update(It.IsAny<Genre>()), Times.Once);
        }

        [Fact]
        public void AddGenreTranslate_ShouldCallGenreRepositoryOnceWhenParams()
        {
            // Act
            _genreService.AddGenreTranslate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>());

            // Assert
            _translationRepository.Verify(a => a.AddTranslate(It.IsAny<GenreTranslation>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public void GetGenreByName_ShouldReturnGenreWhenNameAndLang()
        {
            // Arrange
            _genreRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Genre, bool>>>(), It.IsAny<Expression<Func<Genre, object>>>())).Returns(_genre);
            _gameRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_game);

            // Act
            var result = _genreService.GetGenreByName(It.IsAny<string>());

            // Assert
            Assert.Equal(result.Name, _genre.Name);
        }

        private void InitializeTestData(out Genre genre, out Game game, out List<Genre> genres)
        {
            var gameBuilder = new GameBuilder();
            var genreBuilder = new GenreBuilder();

            genre = genreBuilder.WithName("Sport").WithId(Guid.NewGuid()).Build();
            genre.Languages = new List<GenreTranslation>();

            genres = new List<Genre>
            {
                genreBuilder.WithName("Sport").Build(),
                genreBuilder.WithName("Rally").Build(),
                genreBuilder.WithName("TBS").Build()
            };

            game = gameBuilder
                       .WithId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8"))
                       .WithKey("c21")
                       .WithName("Cry Souls")
                       .WithDescription("Cry Souls desc")
                       .WithUnitsInStock(10)
                       .WithPrice(10)
                       .WithPublisher("Unknown")
                       .WithGameGenres()
                       .Build();

            game.Languages = new List<GameTranslation>();
        }
    }
}