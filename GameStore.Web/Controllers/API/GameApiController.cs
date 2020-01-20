using System;
using System.Linq;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.Services;
using GameStore.Web.ViewModels.Game;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers.API
{
    [Route("api/games")]
    [ApiController]
    public class GameApiController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public GameApiController(IGameService gameService,
            IMapper mapper,
            IPublisherService publisherService,
            ICommentService commentService)
        {
            _gameService = gameService;
            _mapper = mapper;
            _publisherService = publisherService;
            _commentService = commentService;
            _gameService = gameService;
        }

        [HttpGet("")]
        public IActionResult GetAll([FromForm] FilterDataModel filters)
        {
            var games = _gameService.FilterGames(filters);

            if (!games.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(games);
        }

        [HttpGet("{gameKey}")]
        public IActionResult Get(string gameKey)
        {
            var game = _gameService.Get(gameKey);

            if (game == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(game);
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Create([FromForm] GameViewModel model)
        {
            if (String.IsNullOrEmpty(model.Key))
            {
                ModelState.AddModelError(nameof(GameViewModel.Key), "The Key field is required.");
            }

            if (String.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError(nameof(GameViewModel.Name), "The Name field is required.");
            }

            if (String.IsNullOrEmpty(model.Description))
            {
                ModelState.AddModelError(nameof(GameViewModel.Description), "The Description field is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = _mapper.Map<GameViewModel, Game>(model);
            var gameId = Guid.NewGuid();
            game.Id = gameId;

            if (!String.IsNullOrEmpty(model.Publisher))
            {
                game.Publisher = new Publisher();
                var publisher = _publisherService.GetPublisherByCompany(model.Publisher);
                game.Publisher.Id = publisher.Id;
            }

            _gameService.CreateGame(game, model.GamePlatforms, model.GameGenres);
            _gameService.AddGameTranslate(model.NameRu, model.DescriptionRu, "ru", gameId);

            return Ok(game);
        }

        [HttpPost("delete/{gameKey}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Remove(string gameKey)
        {
            _gameService.Delete(gameKey);

            return Ok();
        }

        [HttpPost("update/{gameKey}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Update([FromForm] GameViewModel entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = _mapper.Map<Game>(entity);
            var gameId = _gameService.Get(entity.Key).Id;

            _gameService.EditGame(game, entity.GamePlatforms, entity.GameGenres);
            _gameService.AddGameTranslate(entity.NameRu, entity.DescriptionRu, "ru", gameId);

            return Ok(game);
        }

        [HttpGet("{gameKey}/comments")]
        public IActionResult GetComments(string gameKey)
        {
            var gameComments = _commentService.GetAllCommentsByGameKey(gameKey);

            return Ok(gameComments);
        }

        [HttpGet("{gameKey}/comments/{commentId}")]
        public IActionResult GetComment(string gameKey, Guid commentId)
        {
            var gameComment = _commentService.GetAllCommentsByGameKey(gameKey)
                .FirstOrDefault(c => c.CommentId == commentId && c.GameKey == gameKey);

            if (gameComment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(gameComment);
        }

        [HttpGet("{gameKey}/genres")]
        public IActionResult GetGenres(string gameKey)
        {
            var game = _gameService.Get(gameKey);

            if (game.GameGenres == null || !game.GameGenres.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(game.GameGenres);
        }

        [HttpGet("{gameKey}/platforms")]
        public IActionResult GetPlatforms(string gameKey)
        {
            var game = _gameService.Get(gameKey);

            if (game.GamePlatforms == null || !game.GamePlatforms.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(game.GamePlatforms);
        }

        [HttpGet("{gameKey}/publisher")]
        public IActionResult GetPublisher(string gameKey)
        {
            var game = _gameService.Get(gameKey);

            if (game.Publisher == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(game.Publisher);
        }
    }
}