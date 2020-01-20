using System;

namespace GameStore.Domain.Models.MongoModels
{
    public class GenreModel : MongoModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public Guid GenreId { get; set; }
    }
}