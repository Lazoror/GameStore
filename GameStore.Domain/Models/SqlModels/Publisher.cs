using System.Collections.Generic;
using GameStore.Domain.Models.LanguageModels;
using Newtonsoft.Json;

namespace GameStore.Domain.Models.SqlModels
{
    public class Publisher : BaseEntity
    {
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public ICollection<PublisherTranslation> Languages { get; set; }
    }
}