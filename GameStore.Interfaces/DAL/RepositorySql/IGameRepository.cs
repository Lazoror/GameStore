using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Interfaces.DAL.RepositorySql
{
    public interface IGameRepository : ICrudRepository<Game>
    {
        void UpdateFull(Game game, List<string> platforms, List<string> genres);

        void InsertFull(Game game, List<string> platforms, List<string> genres);
    }
}