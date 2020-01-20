using System;
using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels;

namespace GameStore.Interfaces.Services
{
    public interface IPublisherService
    {
        IEnumerable<Publisher> GetAllPublishers();

        IEnumerable<string> GetAllPublisherCompanyNames();

        Publisher GetPublisherByCompany(string companyName, bool isTranslated = true);

        void Delete(string companyName);

        void EditPublisher(Publisher publisher);

        void CreatePublisher(Publisher entity);

        void AddPublisherTranslate(string companyName, string description, string homePage, string lang,
            Guid publisherId);
    }
}