using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Controllers.API;
using GameStore.Web.ViewModels.Genre;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.Web.Tests.Controllers.API
{
    public class GenreApiControllerTest
    {
        private readonly GenreApiController _genreController;
        private readonly GenreViewModel _genreViewModel;

        public GenreApiControllerTest()
        {
            InitializeTestData(out var genre, out _genreViewModel);

            var genreServiceMock = new Mock<IGenreService>();
            var mapperMock = new Mock<IMapper>();

            genreServiceMock.Setup(x => x.GetGenreByName(It.IsAny<string>(), It.IsAny<bool>()))
                             .Returns(genre);
            genreServiceMock.Setup(x => x.GetGenreById(It.IsAny<Guid>()))
                             .Returns(genre);
            genreServiceMock.Setup(x => x.GetAllGenres())
                             .Returns(new List<Genre> { genre });

            _genreController = new GenreApiController(genreServiceMock.Object, mapperMock.Object);
        }

        [Fact]
        public void Create_ShouldReturnIActionResult_WithGenreViewModel()
        {
            var result = _genreController.Insert(_genreViewModel) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Update_ShouldReturnIActionResult_WithGenreViewModel()
        {
            var result = _genreController.Update(_genreViewModel) as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Get_ShouldReturnIActionResult_WithGenreName()
        {
            var result = _genreController.Get("genreName") as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_ShouldReturnIActionResult_WithGenreName()
        {
            var result = _genreController.GetAll() as IActionResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Remove_ShouldReturnIActionResult_WithGenreName()
        {
            var result = _genreController.Remove("genreName") as IActionResult;

            Assert.NotNull(result);
        }

        private void InitializeTestData(out Genre genre, out GenreViewModel genreViewModel)
        {
            genre = new Genre
            {
                Name = "name",
                Id = Guid.NewGuid(),
                ParentGenre = new Genre(),
                GameGenres = new List<GameGenre>()
            };

            genreViewModel = new GenreViewModel
            {
                AllGenres = new List<string>(),
                Id = Guid.NewGuid(),
                Name = "name",
                NameRu = "name ru",
                ParentGenre = "parrentGenre"
            };
        }
    }
}