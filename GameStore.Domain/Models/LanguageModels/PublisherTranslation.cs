using GameStore.Domain.Models.SqlModels;

namespace GameStore.Domain.Models.LanguageModels
{
    public class PublisherTranslation : TranslationEntity
    {
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public Publisher Publisher { get; set; }
    }
}