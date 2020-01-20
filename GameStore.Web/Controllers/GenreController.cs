using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.Domain;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Attributes.Authorization;
using GameStore.Web.ViewModels.Genre;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    [Route("/genres")]
    [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            var genres = _genreService.GetAllGenres();
            var genresView = _mapper.Map<IEnumerable<GenreViewModel>>(genres);

            return View(genresView);
        }

        [HttpGet("update")]
        public IActionResult Edit(string genreName)
        {
            var genre = _genreService.GetGenreByName(genreName, false);
            var genreView = _mapper.Map<GenreViewModel>(genre);

            genreView.AllGenres = _genreService.GetAllGenres().Select(a => a.Name);

            return View(genreView);
        }

        [HttpPost("update")]
        public IActionResult Edit(GenreViewModel genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var genreModel = _genreService.GetGenreById(genre.Id);

            genreModel.Name = genre.Name;
            genreModel.Id = genre.Id;

            _genreService.AddGenreTranslate(genre.NameRu, "ru", genreModel.Id);

            _genreService.EditGenre(genreModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("new")]
        public IActionResult Create()
        {
            var genreView = new GenreViewModel
            {
                AllGenres = _genreService.GetAllGenres().Select(a => a.Name)
            };

            return View(genreView);
        }

        [HttpPost("new")]
        public IActionResult Create(GenreViewModel genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var genreId = Guid.NewGuid();
            var genreNew = new Genre { Name = genre.Name, Id = genreId };

            if (genre.ParentGenre != null)
            {
                var genreModel = _genreService.GetGenreByName(genre.ParentGenre);
                genreNew.ParentGenreId = genreModel.Id;
            }

            _genreService.CreateGenre(genreNew);
            _genreService.AddGenreTranslate(genre.NameRu, "ru", genreId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("remove")]
        public IActionResult Delete(string genreName)
        {
            _genreService.Delete(genreName);

            return RedirectToAction(nameof(Index));
        }
    }
}