using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;
using GameStore.Services.Services;
using Moq;
using Xunit;

namespace GameStore.Services.Tests
{
    public class PlatformServiceTest
    {
        private const string PlatformName = "Desktop";

        private readonly Mock<ICrudRepository<Platform>> _platformRepository;
        private readonly PlatformService _platformService;
        private readonly Mock<ITranslateProvider<PlatformTranslation, Platform>> _translationRepository;
        private readonly PlatformTranslation _platformTranslation;

        public PlatformServiceTest()
        {
            InitializeTestData(out _platformTranslation);

            var logRepositoryMock = new Mock<IMongoLogger>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var platformLangRepository = new Mock<ICrudRepository<PlatformTranslation>>();
            var langRepository = new Mock<ICrudRepository<Language>>();

            _platformRepository = new Mock<ICrudRepository<Platform>>();
            _translationRepository = new Mock<ITranslateProvider<PlatformTranslation, Platform>>();

            unitOfWorkMock.Setup(u => u.GetRepository<ICrudRepository<Platform>>(RepositoryType.SQL))
                           .Returns(_platformRepository.Object);
            unitOfWorkMock.Setup(u => u.GetRepository<ICrudRepository<PlatformTranslation>>(RepositoryType.SQL))
                           .Returns(platformLangRepository.Object);
            unitOfWorkMock.Setup(u => u.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL))
                           .Returns(langRepository.Object);

            _platformRepository.Setup(p => p.FirstOrDefault(null, It.IsAny<Expression<Func<Platform, object>>>()))
                .Returns(new Platform { Languages = new List<PlatformTranslation>() });
            _platformRepository
                .Setup(p => p.GetMany(It.IsAny<int>(), It.IsAny<int>(), null, null,
                    It.IsAny<SortDirection>(), It.IsAny<Expression<Func<Platform, object>>>()))
                .Returns(new List<Platform> { new Platform { Languages = new List<PlatformTranslation>() } });
            _platformRepository
                .Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>(),
                    It.IsAny<Expression<Func<Platform, object>>>()))
                .Returns(new Platform { Languages = new List<PlatformTranslation>() });
            platformLangRepository.Setup(gl => gl.FirstOrDefault(It.IsAny<Expression<Func<PlatformTranslation, bool>>>()))
                .Returns(new PlatformTranslation());
            langRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());

            _platformService = new PlatformService(unitOfWorkMock.Object,
                logRepositoryMock.Object,
                _translationRepository.Object);
        }

        [Fact]
        public void EditPlatform_ShouldCallGetSingleOnceWhenCompanyName()
        {
            _platformRepository.Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>()))
                .Returns(new Platform());

            // Act
            _platformService.Edit(new Platform(), PlatformName);

            // Assert
            _platformRepository.Verify(p => p.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldCallGetSingleOnceWhenCompanyName()
        {
            _platformRepository.Setup(p => p.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>()))
                .Returns(new Platform());
            _translationRepository.Setup(g =>
                g.GetTranslate("ru", new Platform { Languages = new List<PlatformTranslation>() }));

            // Act
            _platformService.Delete(PlatformName);

            // Assert
            _platformRepository.Verify(p => p.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>()), Times.Once);
        }

        [Fact]
        public void CreatePlatform_ShouldCallUpdateOnceWhenPlatform()
        {
            // Act
            _platformService.Create(new Platform());

            // Assert
            _platformRepository.Verify(p => p.Insert(It.IsAny<Platform>()), Times.Once);
        }

        [Fact]
        public void GetAllPlatformTypes_ShouldCallUpdateOnceWhenPlatform()
        {
            // Act
            _platformService.GetAllPlatform();

            // Assert
            _platformRepository.Verify(
                l => l.GetMany(It.IsAny<int>(), It.IsAny<int>(), null, null, It.IsAny<SortDirection>(),
                    It.IsAny<Expression<Func<Platform, object>>>()), Times.Once);

        }

        [Fact]
        public void GetPlatformByName_ShouldCallUpdateOnceWhenPlatform()
        {
            _platformRepository.Setup(ps => ps.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>(),
                                   It.IsAny<Expression<Func<Platform, object>>>()))
                               .Returns((Platform)null);
            _translationRepository.Setup(g =>
                g.GetTranslate("ru", new Platform { Languages = new List<PlatformTranslation>() }));

            // Act
            _platformService.Get(PlatformName, true);

            // Assert
            _platformRepository.Verify(
                p => p.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>(),
                    It.IsAny<Expression<Func<Platform, object>>>()), Times.AtLeastOnce);
        }

        [Fact]
        public void GetAllPlatformNames_ShouldCallUpdateOnce_WhenEmpty()
        {
            _platformRepository.Setup(ps => ps.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>(),
                                   It.IsAny<Expression<Func<Platform, object>>>()))
                               .Returns((Platform)null);
            _translationRepository.Setup(g =>
                g.GetTranslate("ru", new Platform { Languages = new List<PlatformTranslation>() }));

            // Act
            _platformService.GetAllPlatformNames();

            // Assert
            _platformRepository.Verify(
                p => p.GetMany(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<Platform, bool>>>(),
                    It.IsAny<Expression<Func<Platform, object>>>(), It.IsAny<SortDirection>(),
                    It.IsAny<Expression<Func<Platform, object>>[]>()), Times.Once);
        }

        [Fact]
        public void AddPlatformTranslate_ShouldCallUpdateOnce_WhenEmpty()
        {
            _platformRepository.Setup(ps => ps.FirstOrDefault(It.IsAny<Expression<Func<Platform, bool>>>(),
                                   It.IsAny<Expression<Func<Platform, object>>>()))
                               .Returns((Platform)null);
            _translationRepository.Setup(g =>
                g.GetTranslate("ru", new Platform
                {
                    Languages = new List<PlatformTranslation> { _platformTranslation }
                }));

            // Act
            _platformService.AddPlatformTranslate("type", "ru", Guid.NewGuid());

            // Assert
            _translationRepository.Verify(x => x.AddTranslate(It.IsAny<PlatformTranslation>(), It.IsAny<string>()), Times.Once);
        }

        private void InitializeTestData(out PlatformTranslation platformTranslation)
        {
            platformTranslation = new PlatformTranslation
            {
                Id = Guid.NewGuid(),
                EntityId = Guid.NewGuid(),
                Language = new Language(),
                LanguageId = Guid.NewGuid(),
                Name = "name",
                Platform = new Platform()
            };
        }
    }
}