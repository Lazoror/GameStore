using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Interfaces.Services
{
    public interface IGameService
    {
        Game Get(string gameKey, bool isTranslated = true);

        IEnumerable<Game> FilterGames(FilterDataModel filters);

        Expression<Func<Game, bool>> GetFilterGameExpression(FilterDataModel filters);

        IEnumerable<Game> GetAllGames(SortType sortType,
            string lang,
            Expression<Func<Game, bool>> filter = null,
            int skip = 0,
            int take = Int32.MaxValue);

        IEnumerable<string> GetAllGameGenres(string gameKey);

        int CountAllGames(Expression<Func<Game, bool>> filterExpression);

        int GetTotalPages(int allGamesCount, int itemsPerPage);

        void AddGameTranslate(string name, string description, string lang, Guid gameId);

        void Delete(string gameKey);

        Guid CreateGame(Game entity, List<string> platforms, List<string> genres);

        void EditGame(Game entity, List<string> platforms, List<string> genres);

        void Publish(string gameKey);

        FileStream Download(string gameKey);
    }
}