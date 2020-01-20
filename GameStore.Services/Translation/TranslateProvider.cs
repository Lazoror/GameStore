using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Interfaces.Services.Translation;

namespace GameStore.Services.Translation
{
    public class TranslateProvider<LangEntity, TranslateEntity> : ITranslateProvider<LangEntity, TranslateEntity>
        where LangEntity : TranslationEntity
        where TranslateEntity : BaseEntity
    {
        private readonly ITranslationProvider<TranslateEntity> _translationProvider;
        private readonly ITranslator<LangEntity> _translator;

        public TranslateProvider(ITranslator<LangEntity> translateFabric,
            ITranslationProvider<TranslateEntity> translationProvider)
        {
            _translator = translateFabric;
            _translationProvider = translationProvider;
        }

        public TranslateEntity GetTranslate(string language, TranslateEntity entity)
        {
            var translatedEntity = _translationProvider.TranslateEntity(language, entity);

            return translatedEntity;
        }

        public void AddTranslate(LangEntity translationEntity, string language)
        {
            _translator.AddTranslation(translationEntity, language);
        }
    }
}