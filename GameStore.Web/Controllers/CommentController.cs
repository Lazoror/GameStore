using System;
using AutoMapper;
using GameStore.Domain;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Attributes.Authorization;
using GameStore.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameStore.Web.Controllers
{
    [Route("comments")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService,
            IMapper mapper,
            IGameService gameService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _gameService = gameService;
        }

        [HttpPost("{gameKey}/newcomment")]
        [StoreAuthorize(AuthorizePermission.Disallow, RoleName.Admin)]
        public IActionResult Add(CommentViewModel comment, string gameKey)
        {
            var game = _gameService.Get(comment.GameKey);

            if (!ModelState.IsValid || game.IsDeleted)
            {
                return RedirectToAction("GetDetails", "Game", new { gameKey = gameKey });
            }

            var commentModel = _mapper.Map<Comment>(comment);

            _commentService.AddComment(commentModel, comment.GameKey);

            var gameComments = _commentService.GetAllCommentsByGameKey(gameKey);

            return PartialView("_CommentsRender", gameComments);
        }


        [HttpGet("{gameKey}/reply")]
        [StoreAuthorize(AuthorizePermission.Disallow, RoleName.Admin)]
        public IActionResult Reply(string gameKey, Guid parentCommentId)
        {
            var replyCommentViewModel = new CreateCommentViewModel
            {
                GameKey = gameKey,
                ParentCommentId = parentCommentId
            };

            return View(nameof(Reply), replyCommentViewModel);
        }

        [HttpPost("{gameKey}/reply")]
        [StoreAuthorize(AuthorizePermission.Disallow, RoleName.Admin)]
        public IActionResult Reply(CreateCommentViewModel replyComment)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Reply), replyComment);
            }

            var game = _gameService.Get(replyComment.GameKey);

            if (game.IsDeleted)
            {
                return RedirectToAction("NotFound", "Game");
            }

            var comment = _mapper.Map<Comment>(replyComment);

            _commentService.AnswerComment(comment, replyComment.GameKey);

            return RedirectToAction("GetDetails", "Game", new { gameKey = replyComment.GameKey });

        }

        [HttpGet("{gameKey}/quote")]
        [StoreAuthorize(AuthorizePermission.Disallow, RoleName.Admin)]
        public IActionResult Quote(string gameKey, Guid parentCommentId)
        {
            var parentComment = _commentService.GetCommentById(parentCommentId);

            var replyCommentViewModel = new CreateCommentViewModel
            {
                GameKey = gameKey,
                ParentCommentId = parentCommentId,
                Quote = parentComment.Body
            };

            return View(nameof(Quote), replyCommentViewModel);
        }

        [HttpPost("{gameKey}/quote")]
        [StoreAuthorize(AuthorizePermission.Disallow, RoleName.Admin)]
        public IActionResult Quote(CreateCommentViewModel quoteComment)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Quote), quoteComment);
            }

            var game = _gameService.Get(quoteComment.GameKey);

            if (game.IsDeleted)
            {
                return RedirectToAction("NotFound", "Game");
            }

            var comment = _mapper.Map<Comment>(quoteComment);

            _commentService.AnswerComment(comment, quoteComment.GameKey);

            var gameComments = _commentService.GetAllCommentsByGameKey(quoteComment.GameKey);
            var commentViewModel = new DisplayCommentViewModel { GameKey = game.Key, Comments = gameComments };

            return PartialView("_GameComments", commentViewModel);
        }

        [HttpGet("{gameKey}/delete")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Moderator)]
        public IActionResult Delete(Guid commentId, string gameKey)
        {
            _commentService.DeleteComment(commentId);

            return RedirectToAction("GetDetails", "Game", new { gameKey = gameKey });
        }

        [HttpGet("ban")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Moderator)]
        public ViewResult Ban(Guid commentId)
        {
            return View(commentId);
        }

        [HttpPost("ban")]
        [StoreAuthorize(AuthorizePermission.Allow, RoleName.Moderator)]
        public IActionResult Ban(Guid commentId, BanDuration banDuration)
        {
            _commentService.Ban(commentId, banDuration);

            return RedirectToAction(nameof(Index), "Game");
        }
    }
}