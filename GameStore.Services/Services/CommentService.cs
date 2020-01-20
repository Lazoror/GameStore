using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services;
using Newtonsoft.Json;

namespace GameStore.Services.Services
{
    public class CommentService : BaseService, ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICrudRepository<Comment> _commentRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ICrudRepository<GameState> _gameStateRepository;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IMongoLogger logRepository,
            ICrudRepository<GameState> gameStateRepository,
            IGameRepository gameRepository) : base(logRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gameStateRepository = gameStateRepository;
            _gameRepository = gameRepository;
            _commentRepository = _unitOfWork.GetRepository<ICrudRepository<Comment>>();
        }

        public void AddComment(Comment entity, string gameKey)
        {
            entity.Id = Guid.NewGuid();
            var game = _gameRepository.FirstOrDefault(x => x.Key == gameKey);
            var gameStateGame = _gameStateRepository.FirstOrDefault(x => x.GameKey == game.Key);

            if (gameStateGame == null)
            {
                var gameStateId = Guid.NewGuid();
                var gameStateEntity = new GameState
                {
                    Id = gameStateId,
                    GameKey = gameKey,
                    ViewCount = 1
                };

                _gameStateRepository.Insert(gameStateEntity);
                entity.GameStateId = gameStateId;
            }
            else
            {
                entity.GameStateId = gameStateGame.Id;
            }

            _commentRepository.Insert(entity);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Add", "Comment", JsonConvert.SerializeObject(entity));
        }

        public void AnswerComment(Comment entity, string gameKey)
        {
            entity.Id = Guid.NewGuid();

            var game = _gameRepository.FirstOrDefault(x => x.Key == gameKey);

            var gameStateGame = _gameStateRepository.FirstOrDefault(x => x.GameKey == game.Key);

            if (gameStateGame == null)
            {
                var gameStateId = Guid.NewGuid();
                var gameStateEntity = new GameState
                {
                    Id = gameStateId,
                    GameKey = gameKey,
                    ViewCount = 1
                };

                _gameStateRepository.Insert(gameStateEntity);
                entity.GameStateId = gameStateId;
            }
            else
            {
                entity.GameStateId = gameStateGame.Id;
            }

            _commentRepository.Insert(entity);
            _unitOfWork.Commit();

            if (String.IsNullOrEmpty(entity.Quote))
            {
                LogAction(DateTime.UtcNow.ToString(), "AddReply", "Comment", JsonConvert.SerializeObject(entity));
            }
            else
            {
                LogAction(DateTime.UtcNow.ToString(), "AddQuote", "Comment", JsonConvert.SerializeObject(entity));
            }
        }

        public void DeleteComment(Guid commentId)
        {
            var comment = _commentRepository.FirstOrDefault(x => x.Id == commentId, x => x.ParentComment);
            var allComments = _commentRepository.GetMany(includes: x => x.ParentComment);

            DeleteCommentHierarchy(allComments, commentId);
            _commentRepository.Delete(comment);
            _unitOfWork.Commit();

            LogAction(DateTime.UtcNow.ToString(), "Add", "Comment", JsonConvert.SerializeObject(comment));
        }

        public void Ban(Guid commentId, BanDuration duration)
        {
            // We don’t have users for now, so make empty service methods and test it.
        }

        public Comment GetCommentById(Guid commentId)
        {
            var comment = _commentRepository.FirstOrDefault(x => x.Id == commentId);

            return comment;
        }

        private void DeleteCommentHierarchy(IEnumerable<Comment> allComments, Guid deleteCommentId)
        {
            var toDeleteComments = allComments.Where(x => x.ParentCommentId == deleteCommentId).ToList();

            foreach (var comment in toDeleteComments)
            {
                _commentRepository.Delete(comment);

                DeleteCommentHierarchy(allComments, comment.Id);
            }
        }

        private List<DisplayCommentModel> CreateCommentsHierarchy(string gameKey,
            IEnumerable<Comment> allComments,
            Guid? currentParentId = null)
        {
            var commentDtos = allComments.Where(x => x.ParentCommentId == currentParentId)
                                         .Select(c => _mapper.Map<DisplayCommentModel>(c))
                                         .ToList();

            commentDtos.ForEach(x =>
            {
                x.GameKey = gameKey;
                x.ChildrenComments = CreateCommentsHierarchy(gameKey, allComments, x.CommentId);
            });

            return commentDtos;
        }

        public List<DisplayCommentModel> GetAllCommentsByGameKey(string gameKey)
        {
            var comments = _gameStateRepository.FirstOrDefault(x => x.GameKey == gameKey, x => x.Comments)
                                               .Comments;
            var commentsHierarchy = CreateCommentsHierarchy(gameKey, comments);

            if (commentsHierarchy == null)
            {
                return new List<DisplayCommentModel>();
            }

            return commentsHierarchy;
        }
    }
}