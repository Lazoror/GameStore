using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DAL.Data;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.DAL.MongoRepositories;
using MongoDB.Driver;
using SortDirection = GameStore.Domain.Models.SqlModels.SortModels.SortDirection;

namespace GameStore.DAL.Repositories.MongoRepositories
{
    public class MongoCategoryRepository : IMongoReadOnlyRepository<Genre>
    {
        private readonly MongoContext _mongoContext;
        private readonly IMapper _mapper;

        public MongoCategoryRepository(MongoContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        public int Count(Expression<Func<Genre, bool>> filter = null)
        {
            return (int)_mongoContext.CategoriesModel.EstimatedDocumentCount();
        }

        public IEnumerable<Genre> GetMany(int skip = 0, int take = Int32.MaxValue, Expression<Func<Genre, bool>> filter = null, Expression<Func<Genre, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending, params Expression<Func<Genre, object>>[] includes)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            var genres = _mongoContext.CategoriesModel.AsQueryable().ToList();
            var genreEntities = _mapper.Map<IEnumerable<Genre>>(genres).Where(filter.Compile()).ToList();

            return genreEntities;
        }

        public Genre FirstOrDefault(Expression<Func<Genre, bool>> filter, params Expression<Func<Genre, object>>[] includes)
        {
            var genre = GetMany(filter: filter);
            var genreEntity = _mapper.Map<Genre>(genre);

            return genreEntity;
        }

        public bool Any(Expression<Func<Genre, bool>> filter)
        {
            return _mongoContext.CategoriesModel.AsQueryable().Any();
        }
    }
}