using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DAL.Data;
using GameStore.Domain.Models.MongoModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.DAL.MongoRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using SortDirection = GameStore.Domain.Models.SqlModels.SortModels.SortDirection;

namespace GameStore.DAL.Repositories.MongoRepositories
{
    public class MongoGameRepository : IMongoReadOnlyRepository<Game>
    {
        private readonly MongoContext _mongoContext;
        private readonly DbSet<GameState> _gameStateSet;
        private readonly IMapper _mapper;

        public MongoGameRepository(MongoContext mongoContext, IMapper mapper, GameContext gameContext)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
            _gameStateSet = gameContext.Set<GameState>();
        }

        public int Count(Expression<Func<Game, bool>> filter = null)
        {
            return (int)_mongoContext.GamesModel.EstimatedDocumentCount();
        }

        public IEnumerable<Game> GetMany(int skip = 0,
            int take = Int32.MaxValue,
            Expression<Func<Game, bool>> filter = null,
            Expression<Func<Game, object>> orderBy = null,
            SortDirection sortingDirection = SortDirection.Ascending,
            params Expression<Func<Game, object>>[] includes)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            var games = _mongoContext.GamesModel.AsQueryable().ToList();

            var gamesFilled = games.Select(game =>
            {
                game.Key = game.Name.Split(" ").Join("-");

                game.GameId = ObjectIdToGuid(game.Id);

                game.GameGenres = GetGameGenres(game);

                game.GamePlatforms = new List<GamePlatform> { new GamePlatform { PlatformType = new Platform() } };

                game.GameState = GetGameState(game.Key);

                game.Publisher = GetGamePublisher(game.SupplierId);

                return game;
            });

            var gameModels = _mapper.Map<IEnumerable<Game>>(gamesFilled).Where(filter.Compile()).ToList();

            return gameModels;
        }

        public Game FirstOrDefault(Expression<Func<Game, bool>> filter, params Expression<Func<Game, object>>[] includes)
        {
            var mongoGame = GetMany().FirstOrDefault(filter.Compile());

            return mongoGame;
        }

        public bool Any(Expression<Func<Game, bool>> filter)
        {
            return _mongoContext.GamesModel.AsQueryable().Any();
        }

        private List<GameGenre> GetGameGenres(GameModel game)
        {
            var genre = _mongoContext.CategoriesModel.AsQueryable()
                                     .FirstOrDefault(x => x.CategoryId == game.CategoryId);
            var genreModel = _mapper.Map<Genre>(genre);

            var gameGenre = new List<GameGenre>
            {
                new GameGenre
                {
                    Genre = genreModel,
                    Game =  _mapper.Map<Game>(game),
                    GenreId = genreModel.Id,
                    GameId = _mapper.Map<Game>(game).Id
                }
            };

            return gameGenre;
        }

        private Guid ObjectIdToGuid(string objectId)
        {
            var guidId = new Guid(ObjectId.Parse(objectId).ToByteArray().Concat(new byte[] { 5, 5, 5, 5 }).ToArray());

            return guidId;
        }

        private GameState GetGameState(string gameKey)
        {
            var set = _gameStateSet.AsQueryable();

            set = set.Include(x => x.Comments);

            var gameState = set.FirstOrDefault(x => x.GameKey == gameKey);

            if (gameState != null)
            {
                return gameState;
            }

            return new GameState { Comments = new List<Comment>() };
        }

        private Publisher GetGamePublisher(int supplierId)
        {
            var publisherModel = _mongoContext.PublishersModel.AsQueryable()
                                              .FirstOrDefault(x => x.SupplierId == supplierId);
            var publisher = _mapper.Map<Publisher>(publisherModel);
            publisher.Id = ObjectIdToGuid(publisherModel.Id);

            return publisher;
        }
    }
}