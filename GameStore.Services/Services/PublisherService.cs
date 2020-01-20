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
    public class PublisherService : BaseService, IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<Publisher> _publisherRepository;
        private readonly ITranslateProvider<PublisherTranslation, Publisher> _publisherTranslate;
        private readonly string _language;

        public PublisherService(IUnitOfWork unitOfWork,
            IMongoLogger logRepository,
            ITranslateProvider<PublisherTranslation, Publisher> publisherTranslate) : base(logRepository)
        {
            _language = CultureInfo.CurrentCulture.Name;
            _unitOfWork = unitOfWork;
            _publisherTranslate = publisherTranslate;
            _publisherRepository = _unitOfWork.GetRepository<ICrudRepository<Publisher>>();
        }

        public void CreatePublisher(Publisher entity)
        {
            entity.Id = Guid.NewGuid();

            _publisherRepository.Insert(entity);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Create", "Publisher", JsonConvert.SerializeObject(entity));
        }

        public void AddPublisherTranslate(string companyName, string description, string homePage, string lang, Guid publisherId)
        {
            var publisherTranslate = new PublisherTranslation
            {
                CompanyName = companyName,
                Description = description,
                HomePage = homePage,
                EntityId = publisherId
            };

            _publisherTranslate.AddTranslate(publisherTranslate, lang);
        }

        public void Delete(string companyName)
        {
            var publisher = _publisherRepository.FirstOrDefault(x => x.CompanyName == companyName);

            if (publisher != null)
            {
                publisher.IsDeleted = true;
                _publisherRepository.Update(publisher);
                _unitOfWork.Commit();

                LogAction(DateTime.UtcNow.ToString(), "Delete", "Publisher", JsonConvert.SerializeObject(publisher));
            }
        }

        public void EditPublisher(Publisher publisher)
        {
            var publisherEntity = _publisherRepository.FirstOrDefault(x => x.Id == publisher.Id);

            publisherEntity.CompanyName = publisher.CompanyName;
            publisherEntity.Description = publisher.Description;
            publisherEntity.HomePage = publisher.HomePage;

            _publisherRepository.Update(publisherEntity);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Create", "Publisher", JsonConvert.SerializeObject(publisher));
        }

        public IEnumerable<Publisher> GetAllPublishers()
        {
            var publishers = _publisherRepository.GetMany(includes: x => x.Languages);
            var translatedPublishers = new List<Publisher>();

            foreach (var publisher in publishers)
            {
                if (publisher.Languages != null && publisher.Languages.Any())
                {
                    translatedPublishers.Add(_publisherTranslate.GetTranslate(_language, publisher));
                }
                else
                {
                    translatedPublishers.Add(publisher);
                }
            }

            return translatedPublishers;
        }

        public IEnumerable<string> GetAllPublisherCompanyNames()
        {
            return _publisherRepository.GetMany().Select(x => x.CompanyName);
        }

        public Publisher GetPublisherByCompany(string companyName, bool isTranslated = true)
        {
            var publisher = _publisherRepository.FirstOrDefault(x => x.CompanyName == companyName, x => x.Languages);

            if (publisher != null)
            {
                return publisher;
            }

            var publisherId = _unitOfWork.GetRepository<ICrudRepository<PublisherTranslation>>()
                .FirstOrDefault(g => g.CompanyName == companyName).EntityId;
            publisher = _publisherRepository.FirstOrDefault(x => x.Id == publisherId, x => x.Languages);

            if (isTranslated)
            {
                publisher = _publisherTranslate.GetTranslate(_language, publisher);
            }

            var publisherTranslations = publisher.Languages.Select(x =>
            {
                if (x.Language == null)
                {
                    x.Language = _unitOfWork.GetRepository<ICrudRepository<Language>>(RepositoryType.SQL).FirstOrDefault(gr => gr.Id == x.LanguageId);
                }

                return x;
            }).ToList();

            publisher.Languages = publisherTranslations;

            return publisher;
        }
    }
}