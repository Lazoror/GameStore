using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation.TranslationProvider
{
    public class GenreTranslationProvider : ITranslationProvider<Genre>
    {
        private readonly ICrudRepository<Language> _languageRepository;
        private readonly ICrudRepository<GenreTranslation> _genreTranslationRepository;

        public GenreTranslationProvider(IUnitOfWork unitOfWork)
        {
            _languageRepository = unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            _genreTranslationRepository = unitOfWork.GetRepository<ICrudRepository<GenreTranslation>>();
        }

        public Genre TranslateEntity(string language, Genre genre)
        {
            var currentLanguage = _languageRepository.FirstOrDefault(x => x.Code == language);
            var genreTranslation = _genreTranslationRepository.FirstOrDefault(x =>
                x.LanguageId == currentLanguage.Id && x.EntityId == genre.Id);

            if (genreTranslation != null)
            {
                genre.Name = genreTranslation.Name;
            }

            return genre;
        }
    }
}