using GameStore.Domain.Models.SqlModels;

namespace GameStore.Domain.Models.LanguageModels
{
    public class PlatformTranslation : TranslationEntity
    {
        public string Name { get; set; }

        public Platform Platform { get; set; }
    }
}