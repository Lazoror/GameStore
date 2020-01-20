using System;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class GameGenreBuilder
    {
        private GameGenre _gameGenre;

        public GameGenreBuilder()
        {
            _gameGenre = new GameGenre();
        }

        public GameGenreBuilder WithGameId(Guid id)
        {
            _gameGenre.GameId = id;

            return this;
        }

        public GameGenreBuilder WithGenreId(Guid id)
        {
            _gameGenre.GenreId = id;

            return this;
        }

        public GameGenreBuilder WithGenre(string genreName)
        {
            var genre = new Genre { Name = genreName };
            _gameGenre.Genre = genre;

            return this;
        }

        public GameGenre Build()
        {
            var result = _gameGenre;
            _gameGenre = new GameGenre();

            return result;
        }
    }
}