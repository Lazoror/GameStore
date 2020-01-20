using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation.TranslationProvider
{
    public class GameTranslationProvider : ITranslationProvider<Game>
    {
        private readonly ICrudRepository<Language> _languageRepository;
        private readonly ICrudRepository<GameTranslation> _gameTranslationRepository;

        public GameTranslationProvider(IUnitOfWork unitOfWork)
        {
            _languageRepository = unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            _gameTranslationRepository = unitOfWork.GetRepository<ICrudRepository<GameTranslation>>();
        }

        public Game TranslateEntity(string language, Game game)
        {
            var currentLanguage = _languageRepository.FirstOrDefault(x => x.Code == language);
            var gameTranslation = _gameTranslationRepository.FirstOrDefault(x =>
                x.LanguageId == currentLanguage.Id && x.EntityId == game.Id);

            if (gameTranslation != null)
            {
                game.Name = gameTranslation.Name;
                game.Description = gameTranslation.Description;
            }

            return game;
        }
    }
}