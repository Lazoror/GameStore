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
    public class PublisherTranslatorTest
    {
        private readonly PublisherTranslator _publisherTranslator;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ICrudRepository<PublisherTranslation>> _languagePublisherRepository;
        private PublisherTranslation _publisherTranslation;

        public PublisherTranslatorTest()
        {
            InitializeTestData();

            _unitOfWork = new Mock<IUnitOfWork>();
            var languageRepository = new Mock<ICrudRepository<Language>>();
            _languagePublisherRepository = new Mock<ICrudRepository<PublisherTranslation>>();

            languageRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());
            _unitOfWork.Setup(uow => uow.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(languageRepository.Object);
            _unitOfWork.Setup(uow => uow.GetRepository<ICrudRepository<PublisherTranslation>>(RepositoryType.SQL)).Returns(_languagePublisherRepository.Object);

            _publisherTranslator = new PublisherTranslator(_unitOfWork.Object);
        }

        [Fact]
        public void Translate_CallSaveChangesOnceWhenTranslationEntityAndLanguage()
        {
            _publisherTranslator.AddTranslation(_publisherTranslation, "en");

            _unitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Translate_CallSaveChangesOnceWhenTranslationEntityAndLanguageAndPublisherLang()
        {
            _languagePublisherRepository.Setup(lg => lg.FirstOrDefault(It.IsAny<Expression<Func<PublisherTranslation, bool>>>()))
                .Returns(new PublisherTranslation { CompanyName = "Publisher" });

            _publisherTranslator.AddTranslation(_publisherTranslation, "en");

            _unitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        private void InitializeTestData()
        {
            _publisherTranslation = new PublisherTranslation { CompanyName = "Publisher" };
        }
    }
}