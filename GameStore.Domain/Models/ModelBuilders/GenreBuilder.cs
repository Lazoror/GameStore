using System;
using GameStore.Domain.Models.SqlModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class GenreBuilder
    {
        private Genre _genre;

        public GenreBuilder()
        {
            _genre = new Genre();
        }

        public GenreBuilder WithName(string name)
        {
            _genre.Name = name;

            return this;
        }

        public GenreBuilder WithId(Guid id)
        {
            _genre.Id = id;

            return this;
        }

        public GenreBuilder WithParentGenreId(Guid id)
        {
            _genre.ParentGenreId = id;

            return this;
        }

        public Genre Build()
        {
            var result = _genre;
            _genre = new Genre();

            return result;
        }
    }
}