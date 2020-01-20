using System;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class GamePlatformBuilder
    {
        private GamePlatform _gamePlatform;

        public GamePlatformBuilder()
        {
            _gamePlatform = new GamePlatform();
        }

        public GamePlatformBuilder WithGameId(Guid id)
        {
            _gamePlatform.GameId = id;

            return this;
        }

        public GamePlatformBuilder WithPlatformId(Guid id)
        {
            _gamePlatform.PlatformTypeId = id;

            return this;
        }

        public GamePlatformBuilder WithPlatformType(string platformName)
        {
            var platformType = new Platform { Name = platformName };
            _gamePlatform.PlatformType = platformType;

            return this;
        }

        public GamePlatform Build()
        {
            var result = _gamePlatform;
            _gamePlatform = new GamePlatform();

            return result;
        }
    }
}