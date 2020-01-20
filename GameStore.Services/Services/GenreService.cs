using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services;
using GameStore.Interfaces.Services.Translation;
using Newtonsoft.Json;

namespace GameStore.Services.Services
{
    public class GenreService : BaseService, IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<Genre> _genreRepository;
        private readonly ITranslateProvider<GenreTranslation, Genre> _genreTranslation;
        private readonly string _language;

        public GenreService(IUnitOfWork unitOfWork,
            IMongoLogger logRepository,
            ITranslateProvider<GenreTranslation, Genre> genreTranslation) : base(logRepository)
        {
            _language = CultureInfo.CurrentCulture.Name;
            _unitOfWork = unitOfWork;
            _genreTranslation = genreTranslation;
            _genreRepository = _unitOfWork.GetRepository<ICrudRepository<Genre>>();
        }

        public void CreateGenre(Genre entity)
        {
            _genreRepository.Insert(entity);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Create", "Genre", JsonConvert.SerializeObject(entity));
        }

        public void EditGenre(Genre entity)
        {
            var oldGenre = JsonConvert.SerializeObject(_genreRepository.FirstOrDefault(x => x.Id == entity.Id));

            _genreRepository.Update(entity);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Edit", "Genre", JsonConvert.SerializeObject(entity), oldGenre);
        }

        public void AddGenreTranslate(string name, string lang, Guid genreId)
        {
            var genreTranslate = new GenreTranslation
            {
                Name = name,
                EntityId = genreId
            };

            _genreTranslation.AddTranslate(genreTranslate, lang);
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genres = _genreRepository.GetMany(0, int.MaxValue, null, null, SortDirection.Ascending,
                x => x.Languages, x => x.ParentGenre);
            var languageRepo = _unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL);
            var translatedGenres = new List<Genre>();

            foreach (var genre in genres)
            {
                if (genre.Languages != null && genre.Languages.Any() && _language != null)
                {
                    var currentLanguage = languageRepo.FirstOrDefault(x => x.Code == _language);
                    var publisherTranslate = genre.Languages.FirstOrDefault(x => x.LanguageId == currentLanguage.Id);

                    if (publisherTranslate != null)
                    {
                        translatedGenres.Add(_genreTranslation.GetTranslate(_language, genre));
                    }
                }
            }

            return genres;
        }

        public IEnumerable<string> GetAllGenreNames()
        {
            var genres = _genreRepository.GetMany();

            return genres.Select(x => x.Name);
        }

        public Genre GetGenreByName(string genreName, bool isTranslated = true)
        {
            var originalGenre = _genreRepository.FirstOrDefault(x => x.Name == genreName, x => x.Languages);

            if (originalGenre == null)
            {
                var genreTranslation = _unitOfWork.GetRepository<ICrudRepository<GenreTranslation>>()
                                                  .FirstOrDefault(x => x.Name == genreName);

                if (genreTranslation != null)
                {
                    originalGenre = _genreRepository.FirstOrDefault(x => x.Id == genreTranslation.EntityId, x => x.Languages);
                }

                if (isTranslated)
                {
                    originalGenre = _genreTranslation.GetTranslate(_language, originalGenre);
                }
            }

            var genreTranslations = originalGenre.Languages.Select(gameLanguage =>
            {
                if (gameLanguage.Language == null)
                {
                    gameLanguage.Language = _unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL)
                                                       .FirstOrDefault(x => x.Id == gameLanguage.LanguageId);
                }

                return gameLanguage;
            }).ToList();

            originalGenre.Languages = genreTranslations;

            return originalGenre;
        }

        public Genre GetGenreById(Guid genreId)
        {
            return _genreRepository.FirstOrDefault(x => x.Id == genreId);
        }

        public void Delete(string genreName)
        {
            var genre = _genreRepository.FirstOrDefault(x => x.Name == genreName);

            genre.IsDeleted = true;
            _genreRepository.Update(genre);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Delete", "Genre", JsonConvert.SerializeObject(genre));
        }
    }
}