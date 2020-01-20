using System;
using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels;

namespace GameStore.Interfaces.Services
{
    public interface IPlatformService
    {
        IEnumerable<Platform> GetAllPlatform();

        IEnumerable<string> GetAllPlatformNames();

        Platform Get(string platformName, bool isTranslated = true);

        void Edit(Platform platform, string platformName);

        void Delete(string platformName);

        void Create(Platform entity);

        void AddPlatformTranslate(string type, string lang, Guid platformId);
    }
}