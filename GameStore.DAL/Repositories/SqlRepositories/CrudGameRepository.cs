using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Data;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.RepositorySql;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories.SqlRepositories
{
    public class CrudGameRepository : IGameRepository
    {
        // LOOK: Where do you use it in methods? (I use GameContext to update/delete entity, because built-in methods are inappropriate for my purposes)
        private readonly DbSet<Game> _gameSet;
        private readonly DbSet<Genre> _genreSet;
        private readonly DbSet<Platform> _platformSet;
        private readonly GameContext _context;

        public CrudGameRepository(GameContext context)
        {
            _context = context;
            _gameSet = context.Set<Game>();
            _genreSet = context.Set<Genre>();
            _platformSet = context.Set<Platform>();
        }

        public void Insert(Game entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Update(Game entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Game entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void UpdateFull(Game game, List<string> platforms, List<string> genres)
        {
            FillFullGame(game, platforms, genres);
        }

        public void InsertFull(Game game, List<string> platforms, List<string> genres)
        {
            _context.Entry(game).State = EntityState.Added;

            FillFullGame(game, platforms, genres);
        }

        public int Count(Expression<Func<Game, bool>> filter = null)
        {
            var count = _gameSet.Count();

            if (filter != null)
            {
                count = _gameSet.Count(filter);
            }

            return count;
        }

        public IEnumerable<Game> GetMany(int skip = 0,
            int take = Int32.MaxValue,
            Expression<Func<Game, bool>> filter = null,
            Expression<Func<Game, object>> orderBy = null,
            SortDirection sortDirection = SortDirection.Ascending,
            params Expression<Func<Game, object>>[] includes)
        {
            var set = _gameSet.AsQueryable();

            set = set
                  .Include(x => x.GameGenres)
                  .ThenInclude(y => y.Genre)
                  .Include(x => x.GamePlatforms)
                  .ThenInclude(y => y.PlatformType)
                  .Include(x => x.Publisher)
                  .Include(x => x.Languages);

            if (filter != null)
            {
                set = set.Where(filter);
            }

            if (orderBy != null)
            {
                switch (sortDirection)
                {
                    case SortDirection.Ascending:
                        set = set.OrderBy(orderBy);
                        break;
                    case SortDirection.Descending:
                        set = set.OrderByDescending(orderBy);
                        break;
                }
            }

            return set.Skip(skip).Take(take).ToList();
        }

        public Game FirstOrDefault(Expression<Func<Game, bool>> filter, params Expression<Func<Game, object>>[] includes)
        {
            var set = _gameSet.AsQueryable()
                          .Include(x => x.GameGenres)
                          .ThenInclude(y => y.Genre)
                          .Include(x => x.GamePlatforms)
                          .ThenInclude(y => y.PlatformType)
                          .Include(x => x.Publisher)
                          .Include(x => x.Languages)
                          .Include(x => x.GameState);

            return set.FirstOrDefault(filter);
        }

        public bool Any(Expression<Func<Game, bool>> filter)
        {
            return _gameSet.Any(filter);
        }

        private void FillGameGenre(Game game, List<string> genres)
        {
            if (genres == null || !genres.Any())
            {
                return;
            }

            game.GameGenres = new List<GameGenre>();

            foreach (var genre in genres)
            {
                var genreEntity = _genreSet.AsQueryable().FirstOrDefault(x => x.Name == genre);

                game.GameGenres.Add(new GameGenre
                {
                    GameId = game.Id,
                    Game = game,
                    Genre = genreEntity,
                    GenreId = genreEntity.Id
                });
            }
        }

        private void FillGamePlatform(Game game, List<string> platforms)
        {
            if (platforms == null || !platforms.Any())
            {
                return;
            }

            game.GamePlatforms = new List<GamePlatform>();

            foreach (var platform in platforms)
            {
                var platformEntity = _platformSet.AsQueryable().FirstOrDefault(x => x.Name == platform);

                game.GamePlatforms.Add(new GamePlatform
                {
                    GameId = game.Id,
                    Game = game,
                    PlatformType = platformEntity,
                    PlatformTypeId = platformEntity.Id
                });
            }
        }

        private void FillFullGame(Game game, List<string> platforms, List<string> genres)
        {
            FillGameGenre(game, genres);
            FillGamePlatform(game, platforms);
        }
    }
}