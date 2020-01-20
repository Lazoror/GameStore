using GameStore.Domain.Models.SqlModels;

namespace GameStore.Domain.Models.LanguageModels
{
    public class GenreTranslation : TranslationEntity
    {
        public string Name { get; set; }

        public Genre Genre { get; set; }
    }
}