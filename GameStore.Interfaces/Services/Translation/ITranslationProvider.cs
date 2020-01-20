using GameStore.Domain.Models;

namespace GameStore.Interfaces.Services.Translation
{
    public interface ITranslationProvider<TranslationEntity>
        where TranslationEntity : BaseEntity
    {
        TranslationEntity TranslateEntity(string language, TranslationEntity entity);
    }
}