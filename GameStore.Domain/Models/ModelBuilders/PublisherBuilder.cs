using System;
using GameStore.Domain.Models.SqlModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class PublisherBuilder
    {
        private Publisher _publisher;

        public PublisherBuilder()
        {
            _publisher = new Publisher();
        }

        public PublisherBuilder WithId(Guid id)
        {
            _publisher.Id = id;

            return this;
        }

        public PublisherBuilder WithCompanyName(string name)
        {
            _publisher.CompanyName = name;

            return this;
        }

        public Publisher Build()
        {
            var result = _publisher;
            _publisher = new Publisher();

            return result;
        }
    }
}