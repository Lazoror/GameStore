using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.ViewModels.Platform;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers.API
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformApiController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        private readonly IMapper _mapper;

        public PlatformApiController(IPlatformService platformService,
            IMapper mapper)
        {
            _platformService = platformService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var platforms = _platformService.GetAllPlatform();
            var platformsView = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(platforms);

            if (!platforms.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(platformsView);
        }

        [HttpGet("{platformName}")]
        public IActionResult Get(string platformName)
        {
            var platform = _platformService.Get(platformName);
            var platformView = _mapper.Map<PlatformTypeViewModel>(platform);

            if (platform == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(platformView);
        }


        [HttpPost("delete/{platformName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Remove(string platformName)
        {
            _platformService.Delete(platformName);

            return Ok(platformName);
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Create([FromForm] PlatformTypeViewModel platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var platformId = Guid.NewGuid();
            var platformModel = new Platform { Name = platform.Name, Id = platformId };

            _platformService.Create(platformModel);
            _platformService.AddPlatformTranslate(platform.NameRu, "ru", platformId);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("update/{platformName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Update([FromForm] PlatformTypeViewModel platformView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var platform = _mapper.Map<Platform>(platformView);

            _platformService.AddPlatformTranslate(platformView.NameRu, "ru", _platformService.Get(platformView.OldName).Id);
            _platformService.Edit(platform, platformView.OldName);

            return Ok(platform);
        }
    }
}