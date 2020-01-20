using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Domain.Models.LanguageModels
{
    public class GameTranslation : TranslationEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Game Game { get; set; }
    }
}