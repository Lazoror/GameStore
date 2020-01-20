using System;

namespace GameStore.Domain.Models.SqlModels.GameModels
{
    public class GamePlatform
    {
        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public Guid PlatformTypeId { get; set; }

        public Platform PlatformType { get; set; }
    }
}