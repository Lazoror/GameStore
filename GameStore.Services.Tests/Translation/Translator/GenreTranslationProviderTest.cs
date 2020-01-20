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
    public class GenreTranslationProviderTest
    {
        private readonly Mock<ICrudRepository<Language>> _languageRepository;
        private readonly Mock<ICrudRepository<GenreTranslation>> _genreTranslationRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly GenreTranslationProvider _genreTranslation;
        private GenreTranslation _genre;

        public GenreTranslationProviderTest()
        {
            _genre = new GenreTranslation { Name = "genre name" };

            _unitOfWork = new Mock<IUnitOfWork>();
            _languageRepository = new Mock<ICrudRepository<Language>>();
            _genreTranslationRepository = new Mock<ICrudRepository<GenreTranslation>>();

            _unitOfWork.Setup(u => u.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(_languageRepository.Object);
            _unitOfWork.Setup(u => u.GetRepository<ICrudRepository<GenreTranslation>>(RepositoryType.SQL)).Returns(_genreTranslationRepository.Object);
            _genreTranslationRepository
                .Setup(gt => gt.FirstOrDefault(It.IsAny<Expression<Func<GenreTranslation, bool>>>()))
                .Returns(_genre);

            _genreTranslation = new GenreTranslationProvider(_unitOfWork.Object);
        }

        [Fact]
        public void TranslateEntity_ShouldReturnGame_WithGameAndLanguage()
        {
            var genre = new Genre { Name = "Game" };

            _genreTranslation.TranslateEntity("ru", genre);

            Assert.Equal(genre.Name, _genre.Name);
        }
    }
}