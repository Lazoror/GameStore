using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Attributes.Authorization;
using GameStore.Web.ViewModels.Platform;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    [Route("platforms")]
    [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
    public class PlatformController : Controller
    {
        private readonly IPlatformService _platformService;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformService platformService, IMapper mapper)
        {
            _platformService = platformService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            var platforms = _platformService.GetAllPlatform();
            var platformsView = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(platforms);

            return View(platformsView);
        }

        [HttpGet("new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("new")]
        public IActionResult Create(PlatformTypeViewModel platform)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var platformId = Guid.NewGuid();
            var platformModel = new Platform { Name = platform.Name, Id = platformId };

            _platformService.Create(platformModel);
            _platformService.AddPlatformTranslate(platform.NameRu, "ru", platformId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("remove")]
        public IActionResult Delete(string platformName)
        {
            _platformService.Delete(platformName);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit")]
        public IActionResult Edit(string platformName)
        {
            var platform = _platformService.Get(platformName, false);
            var platformView = _mapper.Map<PlatformTypeViewModel>(platform);
            platformView.OldName = platformName;

            return View(platformView);
        }

        [HttpPost("edit")]
        public IActionResult Edit(PlatformTypeViewModel platformView)
        {
            if (!ModelState.IsValid)
            {
                return View(platformView);
            }

            var platform = _mapper.Map<Platform>(platformView);
            var platformId = _platformService.Get(platformView.OldName).Id;

            _platformService.AddPlatformTranslate(platformView.NameRu, "ru", platformId);
            _platformService.Edit(platform, platformView.OldName);

            return RedirectToAction(nameof(Index));

        }
    }
}