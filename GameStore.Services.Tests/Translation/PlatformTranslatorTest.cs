using System;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Services.Translation.Translator;
using Moq;
using Xunit;

namespace GameStore.Services.Tests.Translation
{
    public class PlatformTranslatorTest
    {
        private readonly PlatformTranslator _platformTranslator;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ICrudRepository<PlatformTranslation>> _languagePlatformRepository;
        private PlatformTranslation _platformTranslation;

        public PlatformTranslatorTest()
        {
            InitializeTestData();

            _unitOfWork = new Mock<IUnitOfWork>();
            var languageRepository = new Mock<ICrudRepository<Language>>();
            _languagePlatformRepository = new Mock<ICrudRepository<PlatformTranslation>>();

            languageRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());
            _unitOfWork.Setup(uow => uow.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(languageRepository.Object);
            _unitOfWork.Setup(uow => uow.GetRepository<ICrudRepository<PlatformTranslation>>(RepositoryType.SQL)).Returns(_languagePlatformRepository.Object);

            _platformTranslator = new PlatformTranslator(_unitOfWork.Object);
        }

        [Fact]
        public void Translate_CallSaveChangesOnceWhenTranslationEntityAndLanguage()
        {
            _platformTranslator.AddTranslation(_platformTranslation, "en");

            _unitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Translate_CallSaveChangesOnceWhenTranslationEntityAndLanguageAndPublisherLang()
        {
            _languagePlatformRepository.Setup(lg => lg.FirstOrDefault(It.IsAny<Expression<Func<PlatformTranslation, bool>>>()))
                .Returns(new PlatformTranslation { Name = "genre" });

            _platformTranslator.AddTranslation(_platformTranslation, "en");

            _unitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        private void InitializeTestData()
        {
            _platformTranslation = new PlatformTranslation { Name = "Genre" };
        }
    }
}