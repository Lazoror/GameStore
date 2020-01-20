using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.ViewModels.Genre;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers.API
{
    [Route("/api/genres")]
    [ApiController]
    public class GenreApiController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreApiController(IGenreService genreService,
            IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var genres = _genreService.GetAllGenres();
            var genresView = _mapper.Map<IEnumerable<GenreViewModel>>(genres);

            if (!genres.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(genresView);
        }

        [HttpGet("{genreName}")]
        public IActionResult Get(string genreName)
        {
            var genre = _genreService.GetGenreByName(genreName);
            var genreView = _mapper.Map<GenreViewModel>(genre);

            if (genre == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(genreView);
        }


        [HttpPost("delete/{genreName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Remove(string genreName)
        {
            _genreService.Delete(genreName);

            return Ok(genreName);
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Insert([FromForm] GenreViewModel genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genreId = Guid.NewGuid();
            var genreNew = new Genre() { Name = genre.Name, Id = genreId };

            if (genre.ParentGenre != null)
            {
                var genreModel = _genreService.GetGenreByName(genre.ParentGenre);
                genreNew.ParentGenreId = genreModel.Id;
            }

            _genreService.CreateGenre(genreNew);
            _genreService.AddGenreTranslate(genre.NameRu, "ru", genreId);

            return Ok(genreNew);
        }

        [HttpPost("update/{genreName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Update([FromForm] GenreViewModel genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genreModel = _genreService.GetGenreById(genre.Id);

            genreModel.Name = genre.Name;
            genreModel.Id = genre.Id;

            _genreService.AddGenreTranslate(genre.NameRu, "ru", genreModel.Id);

            _genreService.EditGenre(genreModel);

            return Ok(genreModel);
        }
    }
}
