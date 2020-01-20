using System;
using AutoMapper;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.Services;
using GameStore.Services.Tests.ModelBuilders;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class CommentControllerTest
    {
        private readonly CommentController _commentController;
        private readonly Comment _comment;

        public CommentControllerTest()
        {
            InitializeTestData(out _comment);

            var commentServiceMock = new Mock<ICommentService>();
            var mapperMock = new Mock<IMapper>();
            var gameServiceMock = new Mock<IGameService>();

            gameServiceMock.Setup(g => g.Get(It.IsAny<string>(), true)).Returns(new Game());
            commentServiceMock.Setup(cs => cs.GetCommentById(It.IsAny<Guid>())).Returns(_comment);

            _commentController = new CommentController(
                commentServiceMock.Object,
                mapperMock.Object,
                gameServiceMock.Object);
        }

        [Fact]
        public void AddCommentPost_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _commentController.Add(new CommentViewModel(), "csgo") as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Ban_ReturnsIActionResult_WithCommentIdAndBanDuration()
        {
            // Act
            var result = _commentController.Ban(Guid.Empty, BanDuration.OneDay) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Ban_ReturnsViewResult_WhenCommentId()
        {
            // Act
            var result = _commentController.Ban(Guid.Empty) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Reply_ShouldReturnIActionResult_WhenCreateCommentViewModel()
        {
            // Act
            var result = _commentController.Reply(new CreateCommentViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Reply_ReturnsIActionResultWhenGameKeyAndCommentId()
        {
            // Arrange
            string gameKey = "c21";

            // Act
            var result = _commentController.Reply(gameKey, Guid.Empty) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Quote_ReturnsIActionResultWhenGameKeyAndCommentId()
        {
            // Arrange
            string gameKey = "c21";

            // Act
            var result = _commentController.Quote(gameKey, _comment.Id) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Quote_ReturnsIActionResultWhenCreateCommentViewModel()
        {
            // Act
            var result = _commentController.Quote(new CreateCommentViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ReturnsIActionResultWhenCommentIdAndGameKey()
        {
            // Arrange
            string gameKey = "c21";

            // Act
            var result = _commentController.Delete(_comment.Id, gameKey) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        private void InitializeTestData(out Comment comment)
        {
            var commentBuilder = new CommentBuilder();

            comment = commentBuilder.WithId(new Guid("fec459ec-ede8-4ab9-b0e2-b3819f0a09b9")).WithName("CommentAuthor1")
                                      .WithBody("Comment body1")
                                      .WithGameId(new Guid("2245390a-6aaa-4191-35f5-08d7223464b8"))
                                      .Build();
        }
    }
}
