using System;

namespace GameStore.Domain.Models.MongoModels
{
    public class PublisherModel : MongoModel
    {
        public int SupplierId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public Guid PublisherId { get; set; }
    }
}