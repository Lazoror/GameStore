using GameStore.Domain.Models.LanguageModels;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services;

namespace GameStore.Services.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ICrudRepository<Language> _languageRepository;

        public LanguageService(ICrudRepository<Language> languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public Language GetLanguageByCode(string code)
        {
            var language = _languageRepository.FirstOrDefault(x => x.Code == code);

            return language;
        }
    }
}