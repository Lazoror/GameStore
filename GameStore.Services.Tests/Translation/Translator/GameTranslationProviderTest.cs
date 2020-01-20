using System;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Services.Translation.TranslationProvider;
using Moq;
using Xunit;

namespace GameStore.Services.Tests.Translation.Translator
{
    public class GameTranslationProviderTest
    {
        private readonly Mock<ICrudRepository<Language>> _languageRepository;
        private readonly Mock<ICrudRepository<GameTranslation>> _gameTranslationRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly GameTranslationProvider _gameTranslation;
        private GameTranslation _game;

        public GameTranslationProviderTest()
        {
            _game = new GameTranslation { Name = "game name" };

            _unitOfWork = new Mock<IUnitOfWork>();
            _languageRepository = new Mock<ICrudRepository<Language>>();
            _gameTranslationRepository = new Mock<ICrudRepository<GameTranslation>>();

            _unitOfWork.Setup(y => y.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)).Returns(_languageRepository.Object);
            _unitOfWork.Setup(y => y.GetRepository<ICrudRepository<GameTranslation>>(RepositoryType.SQL)).Returns(_gameTranslationRepository.Object);
            _gameTranslationRepository
                .Setup(y => y.FirstOrDefault(It.IsAny<Expression<Func<GameTranslation, bool>>>()))
                .Returns(_game);

            _gameTranslation = new GameTranslationProvider(_unitOfWork.Object);
        }

        [Fact]
        public void TranslateEntity_ShouldReturnGame_WithGameAndLanguage()
        {
            var game = new Game { Name = "Game" };

            _gameTranslation.TranslateEntity("ru", game);

            Assert.Equal(game.Name, _game.Name);
        }
    }
}