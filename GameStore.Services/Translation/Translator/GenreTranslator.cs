using System;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation.Translator
{
    public class GenreTranslator : ITranslator<GenreTranslation>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<Language> _languageRepository;
        private readonly ICrudRepository<GenreTranslation> _genreTranslationRepository;

        public GenreTranslator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _languageRepository = unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            _genreTranslationRepository = unitOfWork.GetRepository<ICrudRepository<GenreTranslation>>();
        }

        public void AddTranslation(GenreTranslation translationModel, string code)
        {
            var currentLanguage = _languageRepository.FirstOrDefault(x => x.Code == code);
            var genreLang = _genreTranslationRepository.FirstOrDefault(x =>
                x.LanguageId == currentLanguage.Id && x.EntityId == translationModel.EntityId);

            if (!string.IsNullOrEmpty(translationModel.Name))
            {
                if (genreLang != null)
                {
                    genreLang.Name = translationModel.Name;

                    _genreTranslationRepository.Update(genreLang);
                }
                else
                {
                    translationModel.Id = Guid.NewGuid();
                    translationModel.LanguageId = currentLanguage.Id;

                    _genreTranslationRepository.Insert(translationModel);
                }

                _unitOfWork.Commit();
            }
        }
    }
}