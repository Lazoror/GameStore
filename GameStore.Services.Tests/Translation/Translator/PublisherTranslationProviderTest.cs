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
    public class PublisherTranslationProviderTest
    {
        private readonly Mock<ICrudRepository<Language>> _languageRepository;
        private readonly Mock<ICrudRepository<PublisherTranslation>> _publisherTranslationRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly PublisherTranslationProvider _publisherTranslation;
        private PublisherTranslation _publisher;

        public PublisherTranslationProviderTest()
        {
            _publisher = new PublisherTranslation { CompanyName = "publisher name" };

            _unitOfWork = new Mock<IUnitOfWork>();
            _languageRepository = new Mock<ICrudRepository<Language>>();
            _publisherTranslationRepository = new Mock<ICrudRepository<PublisherTranslation>>();

            _unitOfWork.Setup(u => u.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(_languageRepository.Object);
            _unitOfWork.Setup(u => u.GetRepository<ICrudRepository<PublisherTranslation>>(RepositoryType.SQL)).Returns(_publisherTranslationRepository.Object);
            _publisherTranslationRepository
                .Setup(gt => gt.FirstOrDefault(It.IsAny<Expression<Func<PublisherTranslation, bool>>>()))
                .Returns(_publisher);

            _publisherTranslation = new PublisherTranslationProvider(_unitOfWork.Object);
        }

        [Fact]
        public void TranslateEntity_ShouldReturnGame_WithGameAndLanguage()
        {
            var publisher = new Publisher { CompanyName = "Game" };

            _publisherTranslation.TranslateEntity("ru", publisher);

            Assert.Equal(publisher.CompanyName, _publisher.CompanyName);
        }
    }
}