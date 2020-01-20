using System;
using AutoMapper;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers.API
{
    [Route("api/orders")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public OrderApiController(IOrderService orderService,
            IMapper mapper,
            IGameService gameService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _gameService = gameService;
        }

        [HttpGet("{orderId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get(Guid orderId)
        {
            var order = _orderService.GetOrderById(orderId);

            if (order == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(order);
        }

        [HttpGet("current")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetCurrent()
        {
            var order = _orderService.GetAllCartOrder(User.Identity.Name);

            if (order == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(order);
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create(string gameKey)
        {
            var game = _gameService.Get(gameKey);

            if (game.IsDeleted)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var orderDetail = new OrderDetail { GameKey = gameKey, Discount = 0, Price = game.Price, Quantity = 1 };
            var customerEmail = User.Identity.Name;

            _orderService.CreateOrder(orderDetail, customerEmail);

            return Ok(orderDetail);
        }

        [HttpPost("remove")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Remove(string gameKey)
        {
            if (String.IsNullOrEmpty(gameKey))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _orderService.DeleteOrderDetail(gameKey);

            return Ok(gameKey);
        }

        [HttpPost("update/{gameKey}/{quantity}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateQuantity(string gameKey, int quantity)
        {
            var order = _orderService.GetAllCartOrder(User.Identity.Name);

            if (order == null)
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.GameKey == gameKey)
                {
                    orderDetail.Quantity = (short)quantity;
                }
            }

            _orderService.EditOrder(order);

            return Ok(order);
        }
    }
}