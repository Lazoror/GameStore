using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;

namespace GameStore.DAL.Repositories.RepositoryFacades
{
    public class GameRepositoryFacade : IGameRepository
    {
        private readonly IGameRepository _sqlGameRepository;
        private readonly IMongoReadOnlyRepository<Game> _mongoGameRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GameRepositoryFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sqlGameRepository = unitOfWork.GetRepository<IGameRepository>(RepositoryType.SQL);
            _mongoGameRepository = unitOfWork.GetRepository<IMongoReadOnlyRepository<Game>>(RepositoryType.Mongo);
        }

        public void Insert(Game game)
        {
            _sqlGameRepository.Insert(game);
        }

        public void Update(Game game)
        {
            var sqlEntity = _sqlGameRepository.FirstOrDefault(x => x.Id == game.Id);

            if (sqlEntity == null)
            {
                _sqlGameRepository.Insert(game);
            }
            else
            {
                _sqlGameRepository.Update(game);
            }
        }

        public void Delete(Game game)
        {
            _sqlGameRepository.Delete(game);
        }

        public void UpdateFull(Game game, List<string> platforms, List<string> genres)
        {
            var isSqlGame = _sqlGameRepository.Any(x => x.Key == game.Key);

            if (!isSqlGame)
            {
                game.GameGenres = game.GameGenres.Select(x => new GameGenre { GameId = x.GameId, GenreId = x.GenreId }) as ICollection<GameGenre>;
                game.GamePlatforms =
                    game.GamePlatforms.Select(x => new GamePlatform
                    { GameId = x.GameId, PlatformTypeId = x.PlatformTypeId }) as ICollection<GamePlatform>;
                game.Publisher = null;

                _sqlGameRepository.Insert(game);
                _unitOfWork.Commit();
            }

            _sqlGameRepository.UpdateFull(game, platforms, genres);
        }

        public void InsertFull(Game game, List<string> platforms, List<string> genres)
        {
            _sqlGameRepository.InsertFull(game, platforms, genres);
        }

        public int Count(Expression<Func<Game, bool>> filter = null)
        {
            var entitiesSql = _sqlGameRepository.GetMany(0, int.MaxValue, filter).ToList();
            var entitiesMongo = _mongoGameRepository
                                .GetMany()
                                .Where(filter.Compile())
                                .ToList();
            var result = entitiesSql
                         .Concat(entitiesMongo)
                         .AsQueryable()
                         .GroupBy(x => x.Id)
                         .Select(x => x.First())
                         .Count(x => !x.IsDeleted);

            return result;
        }

        public IEnumerable<Game> GetMany(int skip = 0,
            int take = Int32.MaxValue,
            Expression<Func<Game, bool>> filter = null,
            Expression<Func<Game, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending,
            params Expression<Func<Game, object>>[] includes)
        {
            var entitiesSql = _sqlGameRepository.GetMany(0, int.MaxValue, filter, orderBy).ToList();
            var entitiesMongo = _mongoGameRepository.GetMany().Where(filter.Compile()).ToList();

            var allEntities = entitiesSql.Concat(entitiesMongo)
                                         .AsQueryable()
                                         .GroupBy(x => x.Id)
                                         .Select(x => x.First())
                                         .Where(x => !x.IsDeleted);

            if (orderBy != null)
            {
                switch (sortingDirection)
                {
                    case SortDirection.Ascending:
                        allEntities = allEntities.OrderBy(orderBy);
                        break;
                    case SortDirection.Descending:
                        allEntities = allEntities.OrderByDescending(orderBy);
                        break;
                }
            }

            return allEntities.ToList().Skip(skip).Take(take);
        }

        public Game FirstOrDefault(Expression<Func<Game, bool>> filter, params Expression<Func<Game, object>>[] includes)
        {
            var sqlEntity = _sqlGameRepository.FirstOrDefault(filter);

            if (sqlEntity == null)
            {
                sqlEntity = _mongoGameRepository.FirstOrDefault(filter);
            }

            return sqlEntity;
        }

        public bool Any(Expression<Func<Game, bool>> filter)
        {
            return _sqlGameRepository.Any(filter);
        }
    }
}