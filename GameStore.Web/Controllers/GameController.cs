using System;
using System.Linq;
using AutoMapper;
using GameStore.Domain;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Attributes.Authorization;
using GameStore.Web.ViewModels.Comment;
using GameStore.Web.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    [Route("games")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IPublisherService _publisherService;
        private readonly IPlatformService _platformService;
        private readonly IGenreService _genreService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public GameController(
            IGameService gameService,
            IMapper mapper,
            IPublisherService publisherService,
            IPlatformService platformService,
            IGenreService genreService,
            ICommentService commentService)
        {
            _gameService = gameService;
            _mapper = mapper;
            _publisherService = publisherService;
            _platformService = platformService;
            _genreService = genreService;
            _commentService = commentService;
        }

        [HttpGet("")]
        public ViewResult Index(FilterDataModel filters)
        {
            var valuesDto = GetFilterValues();
            var filteredGames = _gameService.FilterGames(filters);
            var filterExpression = _gameService.GetFilterGameExpression(filters);
            filters.TotalPages =
                _gameService.GetTotalPages(_gameService.CountAllGames(filterExpression), filters.ItemsPerPage);

            var gameFilters = new GamesFilters { DefaultValues = valuesDto, Filters = filters, Games = filteredGames };

            return View(gameFilters);
        }

        [HttpPost("list")]
        public IActionResult GetGameList(FilterDataModel filters)
        {
            var valuesDto = GetFilterValues();
            var filteredGames = _gameService.FilterGames(filters);
            var filterExpression = _gameService.GetFilterGameExpression(filters);
            filters.TotalPages =
                _gameService.GetTotalPages(_gameService.CountAllGames(filterExpression), filters.ItemsPerPage);

            var gameFilters = new GamesFilters { DefaultValues = valuesDto, Filters = filters, Games = filteredGames };

            return PartialView("_Games", gameFilters);
        }

        [HttpGet("new")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public ViewResult Create()
        {
            var platforms = _platformService.GetAllPlatform().Select(a => a.Name).ToList();
            var genres = _genreService.GetAllGenres().Select(a => a.Name).ToList();
            var publishers = _publisherService.GetAllPublisherCompanyNames().ToList();

            var gameCreate = new GameViewModel
            {
                GamePlatforms = platforms,
                GameGenres = genres,
                Publishers = publishers
            };

            return View(gameCreate);
        }

        [HttpPost("new")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public IActionResult Create(GameViewModel entity)
        {
            if (String.IsNullOrEmpty(entity.Key))
            {
                ModelState.AddModelError(nameof(GameViewModel.Key), "The Key field is required.");
            }

            if (String.IsNullOrEmpty(entity.Name))
            {
                ModelState.AddModelError(nameof(GameViewModel.Name), "The Name field is required.");
            }

            if (String.IsNullOrEmpty(entity.Description))
            {
                ModelState.AddModelError(nameof(GameViewModel.Description), "The Description field is required.");
            }

            if (!ModelState.IsValid)
            {
                var platforms = _platformService.GetAllPlatform().Select(a => a.Name).ToList();
                var publishers = _publisherService.GetAllPublisherCompanyNames().ToList();
                var genres = _genreService.GetAllGenres().Select(a => a.Name).ToList();

                entity.GameGenres = genres;
                entity.GamePlatforms = platforms;
                entity.Publishers = publishers;

                return View(entity);
            }

            var game = _mapper.Map<GameViewModel, Game>(entity);
            game.Publisher = null;

            if (!String.IsNullOrEmpty(entity.Publisher))
            {
                game.Publisher = new Publisher();
                var publisher = _publisherService.GetPublisherByCompany(entity.Publisher);
                game.Publisher.Id = publisher.Id;
            }

            var gameId = _gameService.CreateGame(game, entity.GamePlatforms, entity.GameGenres);
            _gameService.AddGameTranslate(entity.NameRu, entity.DescriptionRu, "ru", gameId);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet("update")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public IActionResult Edit(string gameKey)
        {
            var game = _gameService.Get(gameKey, false);
            var gameView = _mapper.Map<GameViewModel>(game);

            var platforms = _platformService.GetAllPlatform().Select(a => a.Name).ToList();
            var genres = _genreService.GetAllGenres().Select(a => a.Name).ToList();
            var publishers = _publisherService.GetAllPublisherCompanyNames().ToList();

            gameView.Publishers = publishers;
            gameView.GameGenres = genres;
            gameView.GamePlatforms = platforms;

            return View(gameView);
        }

        [HttpPost("update")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public IActionResult Edit(GameViewModel entity)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }

            var game = _mapper.Map<Game>(entity);
            var gameId = _gameService.Get(entity.Key).Id;

            _gameService.EditGame(game, entity.GamePlatforms, entity.GameGenres);
            _gameService.AddGameTranslate(entity.NameRu, entity.DescriptionRu, "ru", gameId);

            return RedirectToAction(nameof(GetDetails), new { gameKey = entity.Key });
        }

        [Route("{gameKey}")]
        [HttpGet]
        public ViewResult GetDetails(string gameKey)
        {
            var game = _gameService.Get(gameKey);
            var displayGameView = new DisplayGameDetailsByKeyRequestModel();

            if (game == null)
            {
                return View(nameof(Index));
            }

            var gameComments = _commentService.GetAllCommentsByGameKey(gameKey);
            var commentViewModel = new DisplayCommentViewModel { GameKey = gameKey, Comments = gameComments };
            var gameViewModel = _mapper.Map<Game, GameViewModel>(game);

            displayGameView.GameViewModel = gameViewModel;
            displayGameView.GameKey = gameKey;
            displayGameView.Comments = commentViewModel;

            return View(displayGameView);
        }

        [Route("remove")]
        [HttpGet]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        public IActionResult Delete(string gameKey)
        {
            _gameService.Delete(gameKey);

            return RedirectToAction(nameof(Index));
        }

        [Route("{gameKey}/download")]
        [HttpGet]
        public IActionResult Download(string gameKey)
        {
            var stream = _gameService.Download(gameKey);

            return File(stream, "application/octet-stream", "application/exe");
        }

        [HttpGet("notFound")]
        public new ViewResult NotFound()
        {
            return View();
        }

        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
        [HttpGet("publish")]
        public IActionResult Publish(string gameKey)
        {
            _gameService.Publish(gameKey);

            return RedirectToAction(nameof(GetDetails), new { gameKey = gameKey });
        }

        [HttpPost]
        public IActionResult ChangeRating([FromBody] GameViewModel gameViewModel)
        {
            var game = _gameService.Get(gameViewModel.Key, false);

            if (game == null)
            {
                return PartialView("NotFound");
            }

            var oldRating = game.Rating * game.RatingQuantity + gameViewModel.Rating;

            var newRating = oldRating / ++game.RatingQuantity;

            game.Rating = newRating;

            _gameService.EditGame(game, gameViewModel.GamePlatforms, gameViewModel.GameGenres);

            var gameView = _mapper.Map<GameViewModel>(game);

            return PartialView("_GameRating", gameView);
        }

        private FilterValues GetFilterValues()
        {
            var filterValuesDto = new FilterValues
            {
                Genres = _genreService.GetAllGenreNames().ToList(),
                Platforms = _platformService.GetAllPlatformNames().ToList(),
                Publishers = _publisherService.GetAllPublisherCompanyNames().ToList()
            };

            return filterValuesDto;
        }
    }
}