using GameStore.Domain.Models.LanguageModels;

namespace GameStore.Interfaces.Services.Translation
{
    public interface ITranslator<LangEntity> where LangEntity : TranslationEntity
    {
        void AddTranslation(LangEntity translationModel, string code);
    }
}