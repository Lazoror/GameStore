using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Services.Tests.ModelBuilders;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels.Genre;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers
{
    public class GenreControllerTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly GenreController _genreController;
        private readonly Genre _genre;

        public GenreControllerTest()
        {
            InitializeTestData(out _genre);

            var genreServiceMock = new Mock<IGenreService>();
            _mapperMock = new Mock<IMapper>();

            _mapperMock.Setup(m => m.Map<Genre, GenreViewModel>(It.IsAny<Genre>())).Returns(new GenreViewModel());

            genreServiceMock.Setup(a => a.GetGenreById(It.IsAny<Guid>())).Returns(new Genre());
            genreServiceMock.Setup(a => a.GetAllGenres()).Returns(new List<Genre> { _genre });
            genreServiceMock.Setup(g => g.GetGenreByName(It.IsAny<string>(), true)).Returns(_genre);
            genreServiceMock.Setup(g => g.GetGenreByName(It.IsAny<string>(), true)).Returns(_genre);

            _genreController = new GenreController(genreServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResultWhenRequest()
        {
            // Act
            var result = _genreController.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ReturnsIActionResultWhenRequest()
        {
            // Act
            var result = _genreController.Create() as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsIActionResultWhenGenreName()
        {
            // Arrange
            _mapperMock.Setup(m => m.Map<GenreViewModel>(It.IsAny<Genre>()))
                .Returns(new GenreViewModel { AllGenres = new List<string>() });

            // Act
            var result = _genreController.Edit(_genre.Name) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ReturnsIActionResultWhenGenreName()
        {
            // Act
            var result = _genreController.Delete(_genre.Name) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ReturnsIActionResultWhenViewModel()
        {
            // Act
            var result = _genreController.Create(new GenreViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ReturnsIActionResultWhenViewModelAndParentGenre()
        {
            // Act
            var result = _genreController.Create(new GenreViewModel { ParentGenre = _genre.Name }) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsIActionResultWhenViewModel()
        {
            // Act
            var result = _genreController.Edit(new GenreViewModel()) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_ReturnsIActionResultWhenGameKey()
        {
            // Arrange
            string gameKey = "csgo";

            // Act
            var result = _genreController.Delete(gameKey) as IActionResult;

            // Assert
            Assert.NotNull(result);
        }

        private void InitializeTestData(out Genre genre)
        {
            var genreBuilder = new GenreBuilder();

            genre = genreBuilder.WithName("Sport")
                                  .WithParentGenreId(new Guid("5245390a-6aaa-4191-35f5-08d7223464b8"))
                                  .WithId(new Guid("5245390a-6aaa-4141-35f5-08d7223464b8"))
                                  .Build();
        }
    }
}