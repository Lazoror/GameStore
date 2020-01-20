using System.Collections.Generic;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using Newtonsoft.Json;

namespace GameStore.Domain.Models.SqlModels
{
    public class Platform : BaseEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public ICollection<GamePlatform> GamePlatforms { get; set; }

        [JsonIgnore]
        public ICollection<PlatformTranslation> Languages { get; set; }
    }
}