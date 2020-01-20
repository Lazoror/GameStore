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
    public class GenreTranslatorTest
    {
        private readonly GenreTranslator _genreTranslator;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ICrudRepository<GenreTranslation>> _languageGenreRepository;
        private GenreTranslation _genreTranslation;

        public GenreTranslatorTest()
        {
            InitializeTestData();

            _unitOfWork = new Mock<IUnitOfWork>();
            var languageRepository = new Mock<ICrudRepository<Language>>();
            _languageGenreRepository = new Mock<ICrudRepository<GenreTranslation>>();

            languageRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());
            _unitOfWork.Setup(uow => uow.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(languageRepository.Object);
            _unitOfWork.Setup(uow => uow.GetRepository<ICrudRepository<GenreTranslation>>(RepositoryType.SQL)).Returns(_languageGenreRepository.Object);

            _genreTranslator = new GenreTranslator(_unitOfWork.Object);
        }

        [Fact]
        public void Translate_CallSaveChangesOnceWhenTranslationEntityAndLanguage()
        {
            _genreTranslator.AddTranslation(_genreTranslation, "en");

            _unitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Translate_CallSaveChangesOnceWhenTranslationEntityAndLanguageAndPublisherLang()
        {
            _languageGenreRepository.Setup(lg => lg.FirstOrDefault(It.IsAny<Expression<Func<GenreTranslation, bool>>>()))
                .Returns(new GenreTranslation { Name = "genre" });

            _genreTranslator.AddTranslation(_genreTranslation, "en");

            _unitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        private void InitializeTestData()
        {
            _genreTranslation = new GenreTranslation { Name = "Genre" };
        }
    }
}