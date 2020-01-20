using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation.TranslationProvider
{
    public class PublisherTranslationProvider : ITranslationProvider<Publisher>
    {
        private readonly ICrudRepository<Language> _languageRepository;
        private readonly ICrudRepository<PublisherTranslation> _publisherTranslationRepository;

        public PublisherTranslationProvider(IUnitOfWork unitOfWork)
        {
            _languageRepository = unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            _publisherTranslationRepository = unitOfWork.GetRepository<ICrudRepository<PublisherTranslation>>();
        }

        public Publisher TranslateEntity(string language, Publisher publisher)
        {
            var currentLanguage = _languageRepository.FirstOrDefault(x => x.Code == language);
            var publisherTranslation = _publisherTranslationRepository.FirstOrDefault(x =>
                x.LanguageId == currentLanguage.Id && x.EntityId == publisher.Id);

            if (publisherTranslation != null)
            {
                publisher.CompanyName = publisherTranslation.CompanyName;
                publisher.Description = publisherTranslation.Description;
                publisher.HomePage = publisherTranslation.HomePage;
            }

            return publisher;
        }
    }
}