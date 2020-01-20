using System;
using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels;

namespace GameStore.Interfaces.Services
{
    public interface IGenreService
    {
        IEnumerable<Genre> GetAllGenres();

        IEnumerable<string> GetAllGenreNames();

        Genre GetGenreByName(string genreName, bool isTranslated = true);

        Genre GetGenreById(Guid genreId);

        void Delete(string genreName);

        void CreateGenre(Genre entity);

        void EditGenre(Genre entity);

        void AddGenreTranslate(string name, string lang, Guid genreId);
    }
}