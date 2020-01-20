using System;
using System.Collections.Generic;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using Newtonsoft.Json;

namespace GameStore.Domain.Models.SqlModels
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? ParentGenreId { get; set; }

        [JsonIgnore]
        public Genre ParentGenre { get; set; }

        [JsonIgnore]
        public ICollection<GenreTranslation> Languages { get; set; }

        [JsonIgnore]
        public ICollection<GameGenre> GameGenres { get; set; }
    }
}