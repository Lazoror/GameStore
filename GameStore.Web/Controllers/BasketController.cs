using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using GameStore.Web.ViewModels.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace GameStore.Web.Controllers
{
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;

        public BasketController(IOrderService orderService,
            IMapper mapper,
            IGameService gameService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _gameService = gameService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var customerEmail = User.Identity.Name;
            var order = _orderService.GetAllCartOrder(customerEmail);

            if (order == null || !EnumerableExtensions.Any(order.OrderDetails))
            {
                return View("EmptyBasket");
            }

            var ordersView = GetBasketItems();

            return View(ordersView);
        }

        [HttpPost("item/increment")]
        public IActionResult ChangeQuantityBasketItem([FromBody] OrderDetailViewModel order)
        {
            if (order.Id == Guid.Empty)
            {
                return PartialView("_NotFound");
            }

            var orderDetail = _orderService.GetOrderDetail(order.Id);
            var game = _gameService.Get(orderDetail.GameKey);

            orderDetail.Quantity += order.Quantity;
            orderDetail.Price = orderDetail.Quantity * game.Price;

            if (game.UnitsInStock - orderDetail.Quantity < 0)
            {
                return PartialView("_NotFound");
            }

            _orderService.EditOrderDetail(orderDetail);

            var ordersView = GetBasketItems();

            return PartialView("_BasketItems", ordersView);
        }

        [HttpPost("item/remove")]
        public IActionResult RemoveBasketItem([FromBody] OrderDetailViewModel order)
        {
            if (String.IsNullOrEmpty(order.GameKey))
            {
                return PartialView("_NotFound");
            }

            _orderService.DeleteOrderDetail(order.GameKey);

            var ordersView = GetBasketItems();

            if (ordersView == null || !ordersView.Any())
            {
                return PartialView("EmptyBasket");
            }

            return PartialView("_BasketItems", ordersView);
        }

        [HttpGet("/game/{gameKey}/buy")]
        public IActionResult AddToBasket(string gameKey)
        {
            var game = _gameService.Get(gameKey);

            if (game.IsDeleted)
            {
                return RedirectToAction("NotFound", "Game");
            }

            var orderDetail = new OrderDetail { GameKey = gameKey, Discount = 0, Price = game.Price, Quantity = 1 };
            var customerEmail = User.Identity.Name;

            _orderService.CreateOrder(orderDetail, customerEmail);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("remove")]
        public IActionResult Delete(string gameKey)
        {
            _orderService.DeleteOrderDetail(gameKey);

            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<OrderDetailViewModel> GetBasketItems()
        {
            var customerEmail = User.Identity.Name;
            var order = _orderService.GetAllCartOrder(customerEmail);

            var ordersView =
                _mapper.Map<IEnumerable<OrderDetailViewModel>>(order.OrderDetails);

            var orderViews = ordersView.Select(orderViewModel =>
            {
                var game = _gameService.Get(orderViewModel.GameKey);
                orderViewModel.GameName = game.Name;
                orderViewModel.Platforms = new List<string>(game.GamePlatforms.Select(gp => gp.PlatformType.Name));

                return orderViewModel;
            });

            return orderViews;
        }
    }
}