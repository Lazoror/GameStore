using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain;
using GameStore.Domain.Models.SqlModels;
using GameStore.Interfaces.Services;
using GameStore.Web.Attributes.Authorization;
using GameStore.Web.ViewModels.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    [Route("publisher")]
    [StoreAuthorize(AuthorizePermission.Allow, RoleName.Manager)]
    public class PublisherController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPublisherService _publisherService;

        public PublisherController(IMapper mapper, IPublisherService publisherService)
        {
            _mapper = mapper;
            _publisherService = publisherService;
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            var publishers = _publisherService.GetAllPublishers();
            var publishersView = _mapper.Map<IEnumerable<PublisherViewModel>>(publishers);

            return View(publishersView);
        }

        [HttpGet("{companyName}")]
        [AllowAnonymous]
        public ViewResult PublisherDetails(string companyName)
        {
            var publisher = _publisherService.GetPublisherByCompany(companyName);
            var publisherView = _mapper.Map<PublisherViewModel>(publisher);

            return View(publisherView);
        }

        [HttpGet("new")]
        public IActionResult CreatePublisher()
        {
            return View();
        }

        [HttpPost("new")]
        public IActionResult CreatePublisher(PublisherViewModel publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            var publisherModel = _mapper.Map<Publisher>(publisher);
            _publisherService.CreatePublisher(publisherModel);
            var publisherOrigin = _publisherService.GetPublisherByCompany(publisher.CompanyName);

            _publisherService.AddPublisherTranslate(publisher.CompanyNameRu, publisher.DescriptionRu,
                publisher.HomePageRu, "ru", publisherOrigin.Id);

            return RedirectToAction("Index", "Game");

        }

        [HttpGet("remove")]
        public IActionResult Delete(string companyName)
        {
            _publisherService.Delete(companyName);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit")]
        public ViewResult Edit(string companyName)
        {
            var publisher = _publisherService.GetPublisherByCompany(companyName, false);
            var publisherView = _mapper.Map<PublisherViewModel>(publisher);

            publisherView.OldCompanyName = publisher.CompanyName;

            return View(publisherView);
        }

        [HttpPost("edit")]
        public IActionResult Edit(PublisherViewModel publisher)
        {
            if (ModelState.IsValid)
            {
                return View(publisher);
            }

            var publisherEntity = _publisherService.GetPublisherByCompany(publisher.OldCompanyName);
            publisherEntity.CompanyName = publisher.CompanyName;
            publisherEntity.Description = publisher.Description;
            publisherEntity.HomePage = publisher.HomePage;
            _publisherService.EditPublisher(publisherEntity);

            _publisherService.AddPublisherTranslate(publisher.CompanyNameRu, publisher.DescriptionRu,
                publisher.HomePageRu, "ru", publisherEntity.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}