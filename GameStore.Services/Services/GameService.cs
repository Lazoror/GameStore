using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services;
using GameStore.Interfaces.Services.Translation;
using GameStore.Services.Filtering;
using Newtonsoft.Json;

namespace GameStore.Services.Services
{
    public class GameService : BaseService, IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameRepository _gameRepository;
        private readonly ICrudRepository<Genre> _genreRepository;
        private readonly ICrudRepository<Publisher> _publisherRepository;
        private readonly ITranslateProvider<GameTranslation, Game> _gameTranslate;
        private readonly string _language;

        public GameService(IUnitOfWork unitOfWork,
            IMongoLogger logRepository,
            ITranslateProvider<GameTranslation, Game> gameTranslate,
            IGameRepository gameRepository) : base(logRepository)
        {
            _language = CultureInfo.CurrentCulture.Name;
            _unitOfWork = unitOfWork;
            _gameTranslate = gameTranslate;
            _gameRepository = gameRepository;
            _genreRepository = unitOfWork.GetRepository<ICrudRepository<Genre>>();
            _publisherRepository = unitOfWork.GetRepository<ICrudRepository<Publisher>>();
        }

        public Guid CreateGame(Game entity, List<string> platforms, List<string> genres)
        {
            entity.AddDate = DateTime.UtcNow;
            entity.Id = Guid.NewGuid();

            _gameRepository.InsertFull(entity, platforms, genres);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Create", "Game", JsonConvert.SerializeObject(entity));

            return entity.Id;
        }

        public void EditGame(Game entity, List<string> platforms, List<string> genres)
        {
            var oldGame = JsonConvert.SerializeObject(Get(entity.Key));
            var game = _gameRepository.FirstOrDefault(x => x.Key == entity.Key);

            game.Name = entity.Name;
            game.Description = entity.Description;
            game.Discontinued = entity.Discontinued;
            game.Price = entity.Price;
            game.UnitsInStock = entity.UnitsInStock;

            _gameRepository.UpdateFull(game, platforms, genres);

            if (entity.Publisher != null && !String.IsNullOrEmpty(entity.Publisher.CompanyName))
            {
                var publisherEntity =
                    _publisherRepository.FirstOrDefault(x => x.CompanyName == entity.Publisher.CompanyName);

                game.Publisher = publisherEntity;
            }

            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Edit", "Game", JsonConvert.SerializeObject(game), oldGame);
        }

        public void Publish(string gameKey)
        {
            var game = _gameRepository.FirstOrDefault(x => x.Key == gameKey);

            if (game.PublishDate == default(DateTime))
            {
                game.PublishDate = DateTime.UtcNow;

                _gameRepository.Update(game);
                _unitOfWork.Commit();
            }
        }

        public FileStream Download(string gameKey)
        {
            var stream = new FileStream(@"wwwroot/files/game.exe", FileMode.Open);

            return stream;
        }

        public IEnumerable<string> GetAllGameGenres(string gameKey)
        {
            var game = _gameRepository.FirstOrDefault(x => x.Key == gameKey); // LOOK: please receive genres through genres table. (If I will take genres from table GameGenre, i would receive both game ang gameGenre, because receiving game genres from another table requires game ID )
            var genres = new List<string>();

            foreach (GameGenre genre in game.GameGenres)
            {
                var genreEntity = _genreRepository.FirstOrDefault(x => x.Id == genre.GenreId);
                genres.Add(genreEntity.Name);
            }

            return genres;
        }

        public IEnumerable<Game> FilterGames(FilterDataModel filters)
        {
            var gamePipelineExpression = GetFilterGameExpression(filters);
            var games = GetAllGames(filters.SortType, _language, gamePipelineExpression,
                (filters.CurrentPage - 1) * filters.ItemsPerPage, filters.ItemsPerPage);

            return games;
        }

        public Expression<Func<Game, bool>> GetFilterGameExpression(FilterDataModel filters)
        {
            var gamePipelineExpression = new GamePipelineBuilder(filters)
                .WithSearchFilter()
                .WithGameGenreFilter()
                .WithGamePlatformsFilter()
                .WithGamePriceFilter()
                .WithGamePublisherFilter()
                .WithGameReleaseDateFilter()
                .Build();

            return gamePipelineExpression;
        }

        public int CountAllGames(Expression<Func<Game, bool>> filterExpression)
        {
            return _gameRepository.Count(filterExpression);
        }

        public void Delete(string key)
        {
            var game = _gameRepository.FirstOrDefault(x => x.Key == key);

            if (game != null)
            {
                game.IsDeleted = true;
                _gameRepository.Update(game);
                _unitOfWork.Commit();

                LogAction(DateTime.UtcNow.ToString(), "Delete", "Game", JsonConvert.SerializeObject(game));
            }
        }

        public Game Get(string key, bool isTranslated = true)
        {
            var game = _gameRepository.FirstOrDefault(x => x.Key == key);

            game.GameState = GetGameState(game.Key);


            _unitOfWork.Commit();

            if (isTranslated)
            {
                if (game.Languages != null && game.Languages.Any())
                {
                    game = _gameTranslate.GetTranslate(_language, game);
                }
            }

            return game;
        }

        public IEnumerable<Game> GetAllGames(SortType sortType,
            string lang,
            Expression<Func<Game, bool>> filter = null,
            int skip = 0,
            int take = Int32.MaxValue)
        {
            var sortModel = new GameSortingBuilder().ResolveSorting(sortType).Build();
            var games = _gameRepository.GetMany(skip, take, filter, sortModel.OrderExpression, sortModel.SortDirection)
                                       .ToList();
            var translatedGames = new List<Game>();

            foreach (var game in games)
            {
                if (game.Languages != null && game.Languages.Any())
                {
                    translatedGames.Add(_gameTranslate.GetTranslate(_language, game));
                }
                else
                {
                    translatedGames.Add(game);
                }
            }

            return translatedGames;
        }

        public void AddGameTranslate(string name, string description, string lang, Guid gameId)
        {
            var gameTranslate = new GameTranslation
            {
                Description = description,
                EntityId = gameId,
                Name = name
            };

            _gameTranslate.AddTranslate(gameTranslate, lang);
        }

        public int GetTotalPages(int allGamesCount, int itemsPerPage)
        {
            decimal totalPages = (decimal)allGamesCount / itemsPerPage;
            int result = allGamesCount / itemsPerPage;

            if (totalPages % 2 != 0 && allGamesCount - itemsPerPage != 0)
            {
                result++;
            }

            return result;
        }

        private GameState GetGameState(string gameKey)
        {
            var gameStateRepository = _unitOfWork.GetRepository<ICrudRepository<GameState>>();
            var gameState = gameStateRepository.FirstOrDefault(x => x.GameKey == gameKey);

            if (gameState == null)
            {
                var gameStateEntity = new GameState
                {
                    Id = Guid.NewGuid(),
                    GameKey = gameKey,
                    ViewCount = 1
                };
                gameStateRepository.Insert(gameStateEntity);
                return gameStateEntity;
            }

            gameState.ViewCount++;
            gameStateRepository.Update(gameState);
            return gameState;
        }
    }
}