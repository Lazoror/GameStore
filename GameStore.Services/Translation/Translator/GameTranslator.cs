using System;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation.Translator
{
    public class GameTranslator : ITranslator<GameTranslation>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<Language> _languageRepository;
        private readonly ICrudRepository<GameTranslation> _gameTranslationRepository;

        public GameTranslator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _languageRepository = unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            _gameTranslationRepository = unitOfWork.GetRepository<ICrudRepository<GameTranslation>>();
        }

        public void AddTranslation(GameTranslation translationModel, string code)
        {
            var currentLanguage = _languageRepository.FirstOrDefault(x => x.Code == code);
            var gameLang = _gameTranslationRepository.FirstOrDefault(x =>
                x.LanguageId == currentLanguage.Id && x.EntityId == translationModel.EntityId);

            if (!String.IsNullOrEmpty(translationModel.Description) ||
                !String.IsNullOrEmpty(translationModel.Name))
            {
                if (gameLang != null)
                {
                    gameLang.Name = translationModel.Name;
                    gameLang.Description = translationModel.Description;

                    _gameTranslationRepository.Update(gameLang);
                }
                else
                {
                    translationModel.Id = Guid.NewGuid();
                    translationModel.LanguageId = currentLanguage.Id;

                    _gameTranslationRepository.Insert(translationModel);
                }

                _unitOfWork.Commit();
            }
        }
    }
}