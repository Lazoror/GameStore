using System;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Services.Translation.TranslationProvider;
using Moq;
using Xunit;

namespace GameStore.Services.Tests.Translation.Translator
{
    public class PlatformTranslationProviderTest
    {
        private readonly Mock<ICrudRepository<Language>> _languageRepository;
        private readonly Mock<ICrudRepository<PlatformTranslation>> _platformTranslationRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly PlatformTranslationProvider _platformTranslation;
        private PlatformTranslation _platform;

        public PlatformTranslationProviderTest()
        {
            _platform = new PlatformTranslation { Name = "platform name" };

            _unitOfWork = new Mock<IUnitOfWork>();
            _languageRepository = new Mock<ICrudRepository<Language>>();
            _platformTranslationRepository = new Mock<ICrudRepository<PlatformTranslation>>();

            _unitOfWork.Setup(u => u.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(_languageRepository.Object);
            _unitOfWork.Setup(u => u.GetRepository<ICrudRepository<PlatformTranslation>>(RepositoryType.SQL)).Returns(_platformTranslationRepository.Object);
            _platformTranslationRepository
                .Setup(gt => gt.FirstOrDefault(It.IsAny<Expression<Func<PlatformTranslation, bool>>>()))
                .Returns(_platform);

            _platformTranslation = new PlatformTranslationProvider(_unitOfWork.Object);
        }

        [Fact]
        public void TranslateEntity_ShouldReturnGame_WithGameAndLanguage()
        {
            var platform = new Platform { Name = "Game" };

            _platformTranslation.TranslateEntity("ru", platform);

            Assert.Equal(platform.Name, _platform.Name);
        }
    }
}