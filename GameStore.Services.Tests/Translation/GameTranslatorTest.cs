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
    public class GameTranslatorTest
    {
        private readonly GameTranslator _gameTranslator;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ICrudRepository<GameTranslation>> _languageGameRepository;
        private GameTranslation _gameTranslation;

        public GameTranslatorTest()
        {
            InitializeTestData();

            _unitOfWork = new Mock<IUnitOfWork>();
            var languageRepository = new Mock<ICrudRepository<Language>>();
            _languageGameRepository = new Mock<ICrudRepository<GameTranslation>>();

            languageRepository.Setup(l => l.FirstOrDefault(It.IsAny<Expression<Func<Language, bool>>>()))
                .Returns(new Language());
            _unitOfWork.Setup(uow => uow.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(languageRepository.Object);
            _unitOfWork.Setup(uow => uow.GetRepository<ICrudRepository<GameTranslation>>(RepositoryType.SQL)).Returns(_languageGameRepository.Object);

            _gameTranslator = new GameTranslator(_unitOfWork.Object);
        }

        [Fact]
        public void Translate_CallSaveChangesOnceWhenTranslationEntityAndLanguage()
        {
            _gameTranslator.AddTranslation(_gameTranslation, "en");

            _unitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void Translate_CallSaveChangesOnceWhenTranslationEntityAndLanguageAndPublisherLang()
        {
            _languageGameRepository.Setup(lg => lg.FirstOrDefault(It.IsAny<Expression<Func<GameTranslation, bool>>>()))
                .Returns(new GameTranslation { Name = "Game" });

            _gameTranslator.AddTranslation(_gameTranslation, "en");

            _unitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        private void InitializeTestData()
        {
            _gameTranslation = new GameTranslation { Name = "Game" };
        }
    }
}