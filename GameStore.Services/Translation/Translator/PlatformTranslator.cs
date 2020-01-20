using System;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation.Translator
{
    public class PlatformTranslator : ITranslator<PlatformTranslation>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<Language> _languageRepository;
        private readonly ICrudRepository<PlatformTranslation> _platformTranslationRepository;

        public PlatformTranslator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _languageRepository = unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            _platformTranslationRepository = unitOfWork.GetRepository<ICrudRepository<PlatformTranslation>>();
        }

        public void AddTranslation(PlatformTranslation translationModel, string code)
        {
            var currentLanguage = _languageRepository.FirstOrDefault(x => x.Code == code);
            var platformLang = _platformTranslationRepository.FirstOrDefault(x =>
                x.LanguageId == currentLanguage.Id && x.EntityId == translationModel.EntityId);

            if (!String.IsNullOrEmpty(translationModel.Name))
            {
                if (platformLang != null)
                {
                    platformLang.Name = translationModel.Name;

                    _platformTranslationRepository.Update(platformLang);
                }
                else
                {
                    translationModel.Id = Guid.NewGuid();
                    translationModel.LanguageId = currentLanguage.Id;

                    _platformTranslationRepository.Insert(translationModel);
                }

                _unitOfWork.Commit();
            }
        }
    }
}