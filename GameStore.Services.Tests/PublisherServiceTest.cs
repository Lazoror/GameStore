using System;
using System.Collections.Generic;
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
    public class PublisherServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICrudRepository<Publisher>> _publisherRepository;
        private readonly Mock<ICrudRepository<Language>> _langRepository;
        private readonly Mock<ITranslateProvider<PublisherTranslation, Publisher>> _translationRepository;
        private readonly PublisherService _publisherService;
        private readonly Publisher _publisher;

        public PublisherServiceTest()
        {
            InitializeTestData(out var game, out _publisher);

            var gameRepository = new Mock<IGameRepository>();
            var publisherLangRepository = new Mock<ICrudRepository<PublisherTranslation>>();
            var logRepositoryMock = new Mock<IMongoLogger>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _publisherRepository = new Mock<ICrudRepository<Publisher>>();
            _langRepository = new Mock<ICrudRepository<Language>>();
            _translationRepository = new Mock<ITranslateProvider<PublisherTranslation, Publisher>>();

            gameRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>())).Returns(game);
            gameRepository.Setup(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Game, bool>>>(),
                It.IsAny<Expression<Func<Game, object>>>(), SortDirection.Ascending)).Returns(new List<Game> { game });
            publisherLangRepository.Setup(gl => gl.FirstOrDefault(It.IsAny<Expression<Func<PublisherTranslation, bool>>>()))
                .Returns(new PublisherTranslation());
            _langRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());
            _publisherRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns(_publisher);

            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Publisher>>(RepositoryType.SQL)).Returns(_publisherRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<PublisherTranslation>>(RepositoryType.SQL)).Returns(publisherLangRepository.Object);
            _unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(_langRepository.Object);

            _publisherService = new PublisherService(_unitOfWorkMock.Object,
                logRepositoryMock.Object,
                _translationRepository.Object);
        }

        [Fact]
        public void EditOrder_ShouldCallGetSingleOnceWhenCompanyName()
        {
            // Act
            _publisherService.Delete(_publisher.CompanyName);

            // Assert
            _publisherRepository.Verify(p => p.FirstOrDefault(It.IsAny<Expression<Func<Publisher, bool>>>()));
        }

        [Fact]
        public void Update_ShouldCallUpdateOnceWhenPublisher()
        {
            // Act
            _publisherService.EditPublisher(_publisher);

            // Assert
            _publisherRepository.Verify(p => p.Update(It.IsAny<Publisher>()));
        }

        [Fact]
        public void GetPublisherByCompany_ShouldCallGetSingleOnceWhenCompanyNameAndLang()
        {
            // Arrange
            _publisherRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Publisher, bool>>>(),
                It.IsAny<Expression<Func<Publisher, object>>>())).Returns(_publisher);
            _langRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());

            // Act
            _publisherService.GetPublisherByCompany(It.IsAny<string>());

            // Assert
            _publisherRepository.Verify(p => p.FirstOrDefault(It.IsAny<Expression<Func<Publisher, bool>>>(), It.IsAny<Expression<Func<Publisher, object>>>()));
        }

        [Fact]
        public void Create_ShouldCallInsertOnceWhenPublisher()
        {
            // Act
            _publisherService.CreatePublisher(_publisher);

            // Assert
            _publisherRepository.Verify(p => p.Insert(It.IsAny<Publisher>()));
        }

        [Fact]
        public void GetAllPublisherCompanyNames_ShouldCallPublisherRepositoryOnceWhenRequest()
        {
            // Act
            _publisherService.GetAllPublisherCompanyNames();

            // Assert
            _unitOfWorkMock.Verify(u => u.GetRepository<ICrudRepository<Publisher>>(RepositoryType.SQL), Times.Once);
        }

        [Fact]
        public void GetAllPublishers_ShouldCallPublisherRepositoryOnceWhenRequest()
        {
            // Act
            _publisherService.GetAllPublishers();

            // Assert
            _unitOfWorkMock.Verify(u => u.GetRepository<ICrudRepository<Publisher>>(RepositoryType.SQL), Times.Once);
        }

        [Fact]
        public void GetAllPublishers_ShouldCallPublisherRepositoryOnceWhenPublishers()
        {
            // Arrange
            _publisherRepository.Setup(a => a.GetMany(It.IsAny<int>(), It.IsAny<int>(), null, null,
                It.IsAny<SortDirection>(), It.IsAny<Expression<Func<Publisher, object>>>())).Returns(new List<Publisher> { _publisher });

            // Act
            _publisherService.GetAllPublishers();

            // Assert
            _unitOfWorkMock.Verify(u => u.GetRepository<ICrudRepository<Publisher>>(RepositoryType.SQL), Times.Once);
        }

        [Fact]
        public void AddPublisherTranslate_ShouldCallPublisherRepositoryOnceWhenPublishers()
        {
            // Arrange
            _publisherRepository.Setup(a => a.GetMany(It.IsAny<int>(), It.IsAny<int>(), null, null,
                It.IsAny<SortDirection>(), It.IsAny<Expression<Func<Publisher, object>>>())).Returns(new List<Publisher> { _publisher });

            // Act
            _publisherService.AddPublisherTranslate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), "ru",
                It.IsAny<Guid>());

            // Assert
            _translationRepository.Verify(pl => pl.AddTranslate(It.IsAny<PublisherTranslation>(), It.IsAny<string>()), Times.Once);
        }

        private void InitializeTestData(out Game game, out Publisher publisher)
        {
            var gameBuilder = new GameBuilder();
            var publisherBuilder = new PublisherBuilder();

            game = gameBuilder.WithId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8")).WithKey("c21")
                                   .WithName("Cry Souls").WithDescription("Cry Souls desc").WithUnitsInStock(10).WithPrice(10).WithPublisher("Unknown").Build();

            game.Languages = new List<GameTranslation> { new GameTranslation() };

            publisher = publisherBuilder.WithId(Guid.Empty).WithCompanyName("Unknown").Build();

            publisher.Languages = new List<PublisherTranslation> { new PublisherTranslation() };
        }
    }
}