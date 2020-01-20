using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;

namespace GameStore.Interfaces.Services.Translation
{
    public interface ITranslateProvider<LangEntity, TranslateEntity>
        where LangEntity : TranslationEntity
        where TranslateEntity : BaseEntity
    {
        TranslateEntity GetTranslate(string language, TranslateEntity entity);

        void AddTranslate(LangEntity translationEntity, string language);
    }
}