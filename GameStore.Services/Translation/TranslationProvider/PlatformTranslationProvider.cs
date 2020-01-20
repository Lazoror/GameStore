using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation.TranslationProvider
{
    public class PlatformTranslationProvider : ITranslationProvider<Platform>
    {
        private readonly ICrudRepository<Language> _languageRepository;
        private readonly ICrudRepository<PlatformTranslation> _platformTranslationRepository;

        public PlatformTranslationProvider(IUnitOfWork unitOfWork)
        {
            _languageRepository = unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            _platformTranslationRepository = unitOfWork.GetRepository<ICrudRepository<PlatformTranslation>>();
        }

        public Platform TranslateEntity(string language, Platform platform)
        {
            var currentLanguage = _languageRepository.FirstOrDefault(x => x.Code == language);
            var platformTranslation = _platformTranslationRepository.FirstOrDefault(x =>
                x.LanguageId == currentLanguage.Id && x.EntityId == platform.Id);

            if (platformTranslation != null)
            {
                platform.Name = platformTranslation.Name;
            }

            return platform;
        }
    }
}