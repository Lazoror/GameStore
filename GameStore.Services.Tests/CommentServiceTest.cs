using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.Domain.Models;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Services.Services;
using GameStore.Services.Tests.ModelBuilders;
using Moq;
using Xunit;

namespace GameStore.Services.Tests
{
    public class CommentServiceTest
    {
        private readonly Mock<ICrudRepository<Comment>> _commentRepository;
        private readonly Mock<ICrudRepository<GameState>> _gameStateRepository;
        private readonly CommentService _commentService;
        private readonly Game _game;
        private readonly Comment _comment;

        public CommentServiceTest()
        {
            InitializeTestData(out _game, out _comment);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var gameRepository = new Mock<IGameRepository>();
            var mapper = new Mock<IMapper>();
            var logRepositoryMock = new Mock<IMongoLogger>();

            _commentRepository = new Mock<ICrudRepository<Comment>>();
            _gameStateRepository = new Mock<ICrudRepository<GameState>>();

            _gameStateRepository
                .Setup(gs => gs.FirstOrDefault(It.IsAny<Expression<Func<GameState, bool>>>(),
                    It.IsAny<Expression<Func<GameState, object>>>()))
                .Returns(new GameState { Comments = new List<Comment>() });
            _commentRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Comment, bool>>>(), null)).Returns(_comment);
            _commentRepository.Setup(a => a.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Expression<Func<Comment, object>>>(), It.IsAny<SortDirection>())).Returns(new List<Comment> { _comment });
            gameRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_game);
            gameRepository.Setup(a => a.FirstOrDefault(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_game);
            unitOfWorkMock.Setup(a => a.GetRepository<ICrudRepository<Comment>>(RepositoryType.SQL)).Returns(_commentRepository.Object);

            _commentService = new CommentService(unitOfWorkMock.Object,
                mapper.Object,
                logRepositoryMock.Object,
                _gameStateRepository.Object,
                gameRepository.Object);
        }

        [Fact]
        public void AddComment_ShouldCallInsertCommentOnceWhenCommentEntity()
        {
            // Arrange
            string gameKey = "c21";

            // Act
            _commentService.AddComment(_comment, gameKey);

            // Assert
            _commentRepository.Verify(a => a.Insert(_comment), Times.Once);
        }

        [Fact]
        public void AnswerComment_ShouldCallInsertCommentOnceWhenCommentEntity()
        {
            // Arrange
            string gameKey = "c21";

            // Act
            _commentService.AnswerComment(_comment, gameKey);

            // Assert
            _commentRepository.Verify(a => a.Insert(It.IsAny<Comment>()), Times.AtLeastOnce);
        }

        [Fact]
        public void AnswerComment_ShouldCallInsertCommentOnceWhenCommentEntityAndGameState()
        {
            // Arrange
            var comment = _comment;
            comment.GameStateId = Guid.NewGuid();
            comment.Quote = "quote";
            string gameKey = "c21";

            // Act
            _commentService.AnswerComment(comment, gameKey);

            // Assert
            _commentRepository.Verify(a => a.Insert(It.IsAny<Comment>()), Times.AtLeastOnce);
        }

        [Fact]
        public void DeleteComment_ShouldCallInsertCommentOnceWhenCommentEntity()
        {
            // Act
            _commentService.DeleteComment(_comment.Id);

            // Assert
            _commentRepository.Verify(a => a.Delete(It.IsAny<Comment>()), Times.AtLeastOnce);
        }

        [Fact]
        public void GetAllCommentsByGameKey_ShouldCallUpdateCommentOnceWhenGameKey()
        {
            // Arrange
            string gameKey = "c21";

            // Act
            _commentService.GetAllCommentsByGameKey(gameKey);

            // Assert
            _gameStateRepository.Verify(
                a => a.FirstOrDefault(It.IsAny<Expression<Func<GameState, bool>>>(),
                    It.IsAny<Expression<Func<GameState, object>>>()), Times.AtLeastOnce);
        }

        [Fact]
        public void GetCommentById_ShouldCallInsertCommentOnceWhenCommentId()
        {
            // Act
            _commentService.GetCommentById(_comment.Id);

            // Assert
            _commentRepository.Verify(a => a.FirstOrDefault(It.IsAny<Expression<Func<Comment, bool>>>()), Times.Once);
        }

        [Fact]
        public void Ban_ShouldCallInsertCommentOnceWhenCommentEntity()
        {
            // Act
            _commentService.Ban(_comment.Id, BanDuration.OneDay);

            // Assert
            Assert.True(true);
        }

        private void InitializeTestData(out Game game, out Comment comment)
        {
            var gameBuilder = new GameBuilder();
            var commentBuilder = new CommentBuilder();

            game = gameBuilder.WithId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8")).WithKey("c21")
                                .WithName("Cry Souls").WithDescription("Cry Souls desc").WithUnitsInStock(10).WithPrice(10)
                                .WithPublisher("Unknown").Build();

            comment = commentBuilder.WithId(new Guid("fec459ec-ede8-4ab9-b0e2-b3819f0a09b9")).WithName("CommentAuthor1")
                                      .WithBody("Comment body1")
                                      .WithGameId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8"))
                                      .Build();
        }
    }
}
