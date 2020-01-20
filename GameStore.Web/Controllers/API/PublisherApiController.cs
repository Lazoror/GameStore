using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.ViewModels.Publisher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers.API
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherApiController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherApiController(IPublisherService publisherService,
            IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var publishers = _publisherService.GetAllPublishers();
            var publishersView = _mapper.Map<IEnumerable<PublisherViewModel>>(publishers);

            if (!publishers.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(publishersView);
        }

        [HttpGet("{companyName}")]
        public IActionResult Get(string companyName)
        {
            var publisher = _publisherService.GetPublisherByCompany(companyName);
            var publisherView = _mapper.Map<PublisherViewModel>(publisher);

            if (publisher == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(publisherView);
        }


        [HttpPost("delete/{companyName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Remove(string companyName)
        {
            _publisherService.Delete(companyName);

            return Ok(companyName);
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Create([FromForm] PublisherViewModel publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisherModel = _mapper.Map<Publisher>(publisher);
            _publisherService.CreatePublisher(publisherModel);
            var publisherOrigin = _publisherService.GetPublisherByCompany(publisher.CompanyName);

            _publisherService.AddPublisherTranslate(publisher.CompanyNameRu, publisher.DescriptionRu, publisher.HomePageRu, "ru", publisherOrigin.Id);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("update/{companyName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public IActionResult Update([FromForm] PublisherViewModel publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisherEntity = _publisherService.GetPublisherByCompany(publisher.OldCompanyName);
            publisherEntity.CompanyName = publisher.CompanyName;
            publisherEntity.Description = publisher.Description;
            publisherEntity.HomePage = publisher.HomePage;
            _publisherService.EditPublisher(publisherEntity);

            _publisherService.AddPublisherTranslate(publisher.CompanyNameRu, publisher.DescriptionRu, publisher.HomePageRu, "ru", publisherEntity.Id);

            return Ok(publisherEntity);
        }
    }
}