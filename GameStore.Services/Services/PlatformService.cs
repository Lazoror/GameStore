using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services;
using GameStore.Interfaces.Services.Translation;
using Newtonsoft.Json;

namespace GameStore.Services.Services
{
    public class PlatformService : BaseService, IPlatformService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<Platform> _platformRepository;
        private readonly ITranslateProvider<PlatformTranslation, Platform> _platformTranslate;
        private readonly string _language;

        public PlatformService(IUnitOfWork unitOfWork,
            IMongoLogger logRepository,
            ITranslateProvider<PlatformTranslation, Platform> platformTranslate) : base(logRepository)
        {
            _language = CultureInfo.CurrentCulture.Name;
            _unitOfWork = unitOfWork;
            _platformTranslate = platformTranslate;
            _platformRepository = _unitOfWork.GetRepository<ICrudRepository<Platform>>();
        }

        public void Delete(string platformName)
        {
            var platform = _platformRepository.FirstOrDefault(x => x.Name == platformName);

            if (platform != null)
            {
                platform.IsDeleted = true;
                _unitOfWork.Commit();
            }

            LogAction(DateTime.UtcNow.ToString(), "Delete", "Platform", JsonConvert.SerializeObject(platform));
        }

        public void Create(Platform entity)
        {
            _platformRepository.Insert(entity);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Create", "Platform", JsonConvert.SerializeObject(entity));
        }

        public void AddPlatformTranslate(string type, string lang, Guid platformId)
        {
            var platformTranslate = new PlatformTranslation
            {
                EntityId = platformId,
                Name = type
            };

            _platformTranslate.AddTranslate(platformTranslate, lang);
        }

        public void Edit(Platform platform, string platformName)
        {
            var platformModel = _platformRepository.FirstOrDefault(x => x.Name == platformName);
            var oldPlatform = JsonConvert.SerializeObject(platformModel);

            if (platformModel != null)
            {
                platformModel.Name = platform.Name;
                _platformRepository.Update(platformModel);
                _unitOfWork.Commit();

                LogAction(DateTime.UtcNow.ToString(), "Update", "Platform", JsonConvert.SerializeObject(platformModel),
                    oldPlatform);
            }
        }

        public IEnumerable<Platform> GetAllPlatform()
        {
            var platforms = _platformRepository.GetMany(includes: x => x.Languages);
            var translatedPlatforms = new List<Platform>();

            foreach (var platform in platforms)
            {
                if (platform.Languages != null)
                {
                    translatedPlatforms.Add(_platformTranslate.GetTranslate(_language, platform));
                }
            }

            return translatedPlatforms;
        }

        public IEnumerable<string> GetAllPlatformNames()
        {
            var platforms = _platformRepository.GetMany();

            return platforms.Select(x => x.Name);
        }

        public Platform Get(string platformName, bool isTranslated = true)
        {
            var originalPlatform =
                _platformRepository.FirstOrDefault(x => x.Name == platformName, x => x.Languages);

            if (originalPlatform == null)
            {
                var platform = _unitOfWork.GetRepository<ICrudRepository<PlatformTranslation>>().FirstOrDefault(x => x.Name == platformName);
                originalPlatform = _platformRepository.FirstOrDefault(x => x.Id == platform.EntityId, x => x.Languages);

                if (isTranslated)
                {
                    originalPlatform = _platformTranslate.GetTranslate(_language, originalPlatform);
                }
            }

            if (originalPlatform != null)
            {
                var platformTranslations = originalPlatform.Languages.Select(gameLanguage =>
                {
                    if (gameLanguage.Language == null)
                    {
                        gameLanguage.Language = _unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL).FirstOrDefault(x => x.Id == gameLanguage.LanguageId);
                    }

                    return gameLanguage;
                }).ToList();

                originalPlatform.Languages = platformTranslations;
            }

            return originalPlatform;
        }
    }
}