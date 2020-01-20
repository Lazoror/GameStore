using System;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation.Translator
{
    public class PublisherTranslator : ITranslator<PublisherTranslation>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<Language> _languageRepository;
        private readonly ICrudRepository<PublisherTranslation> _publisherTranslationRepository;

        public PublisherTranslator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _languageRepository = unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            _publisherTranslationRepository = unitOfWork.GetRepository<ICrudRepository<PublisherTranslation>>();
        }

        public void AddTranslation(PublisherTranslation translationModel, string code)
        {
            var currentLanguage = _languageRepository.FirstOrDefault(x => x.Code == code);
            var publisherLang = _publisherTranslationRepository.FirstOrDefault(x =>
                x.LanguageId == currentLanguage.Id && x.EntityId == translationModel.EntityId);

            if (!String.IsNullOrEmpty(translationModel.CompanyName)
               || !String.IsNullOrEmpty(translationModel.Description)
               || !String.IsNullOrEmpty(translationModel.HomePage))

                if (publisherLang != null)
                {
                    publisherLang.CompanyName = translationModel.CompanyName;
                    publisherLang.Description = translationModel.Description;
                    publisherLang.HomePage = translationModel.HomePage;

                    _publisherTranslationRepository.Update(publisherLang);
                }
                else
                {
                    translationModel.Id = Guid.NewGuid();
                    translationModel.LanguageId = currentLanguage.Id;

                    _publisherTranslationRepository.Insert(translationModel);
                }

            _unitOfWork.Commit();
        }
    }
}