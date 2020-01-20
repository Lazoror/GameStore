using System;
using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class GameBuilder
    {
        private Game _game;

        public GameBuilder()
        {
            _game = new Game();
        }

        public GameBuilder WithId(Guid id)
        {
            _game.Id = id;

            return this;
        }

        public GameBuilder WithKey(string key)
        {
            _game.Key = key;

            return this;
        }

        public GameBuilder WithName(string name)
        {
            _game.Name = name;

            return this;
        }

        public GameBuilder WithDescription(string description)
        {
            _game.Description = description;

            return this;
        }

        public GameBuilder WithUnitsInStock(short unitsInStock)
        {
            _game.UnitsInStock = unitsInStock;

            return this;
        }

        public GameBuilder WithPrice(decimal price)
        {
            _game.Price = price;

            return this;
        }

        public GameBuilder WithPublisher(string publisher)
        {
            _game.Publisher = new Publisher { CompanyName = publisher };

            return this;
        }

        public GameBuilder WithGameGenres()
        {
            _game.GameGenres = new List<GameGenre>();

            return this;
        }

        public Game Build()
        {
            var result = _game;
            _game = new Game();

            return result;
        }
    }
}
