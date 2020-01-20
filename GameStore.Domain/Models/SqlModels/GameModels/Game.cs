using System;
using System.Collections.Generic;
using GameStore.Domain.Models.LanguageModels;
using Newtonsoft.Json;

namespace GameStore.Domain.Models.SqlModels.GameModels
{
    public class Game : BaseEntity
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool IsDeleted { get; set; }

        public bool Discontinued { get; set; }

        public decimal Rating { get; set; }

        public int RatingQuantity { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime AddDate { get; set; }

        [JsonIgnore]
        public Publisher Publisher { get; set; }

        [JsonIgnore]
        public GameState GameState { get; set; }

        [JsonIgnore]
        public ICollection<GameTranslation> Languages { get; set; }

        [JsonIgnore]
        public ICollection<GamePlatform> GamePlatforms { get; set; }

        [JsonIgnore]
        public ICollection<GameGenre> GameGenres { get; set; }
    }
}