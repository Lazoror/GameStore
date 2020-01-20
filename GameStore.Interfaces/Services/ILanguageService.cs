using GameStore.Domain.Models.LanguageModels;

namespace GameStore.Interfaces.Services
{
    public interface ILanguageService
    {
        Language GetLanguageByCode(string code);
    }
}