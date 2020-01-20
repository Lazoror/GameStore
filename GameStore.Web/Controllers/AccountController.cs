using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.Services;
using GameStore.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameStore.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public AccountController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            var userFind = _userService.GetUserByEmail(registerModel.Email);

            if (userFind != null)
            {
                ModelState.AddModelError("Email", "Unavailable credentials");

                return View(registerModel);
            }

            _userService.Register(registerModel.Email, registerModel.Password);

            var user = _userService.GetUserByEmail(registerModel.Email);

            if (Request.Cookies.ContainsKey("Order"))
            {
                FillRegisterBasket(user.Id);
            }

            var claims = GenerateClaims(user);
            var principal = CreatePrincipal(claims);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Game");
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            var loginModel = new LoginViewModel { ReturnUrl = returnUrl };

            return View(loginModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = _userService.GetUserByEmail(loginModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");

                return View(loginModel);
            }

            if (user.IsDeleted)
            {
                ModelState.AddModelError("", "Account is deleted");

                return View(loginModel);
            }

            var passwordVerify = Crypto.VerifyHashedPassword(user.Password, loginModel.Password);

            if (!passwordVerify)
            {
                ModelState.AddModelError(nameof(loginModel.Password), "Invalid login or password");

                return View(loginModel);
            }

            if (Request.Cookies.ContainsKey("Order"))
            {
                FillLoginBasket(loginModel.Email);
            }

            var claims = GenerateClaims(user);
            var principal = CreatePrincipal(claims);

            await HttpContext.SignInAsync(principal);

            if (!String.IsNullOrEmpty(loginModel.ReturnUrl))
            {
                return Redirect(loginModel.ReturnUrl);
            }

            return RedirectToAction("Index", "Game");
        }

        [HttpGet("accessDenied")]
        public IActionResult AccessDenied(string returnUrl)
        {
            return View(nameof(AccessDenied), returnUrl);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Game");
        }

        private ClaimsPrincipal CreatePrincipal(IEnumerable<Claim> claims)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        }

        private IEnumerable<Claim> GenerateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Role.Name)));

            return claims;
        }

        private void FillLoginBasket(string email)
        {
            var order = _orderService.GetAllCartOrder(email);
            var cachedOrderCookie = Request.Cookies["Order"];
            var cachedOrderModified = JsonConvert.DeserializeObject<GuestBasketModel>(cachedOrderCookie);

            var guestOrder = _orderService.GetOrderById(cachedOrderModified.OrderId);

            if (guestOrder == null && !guestOrder.OrderDetails.Any())
            {
                return;
            }

            foreach (var orderDetail in guestOrder.OrderDetails)
            {
                var orderDetailEntity = order.OrderDetails.FirstOrDefault(x => x.GameKey == orderDetail.GameKey);

                if (orderDetailEntity != null)
                {
                    orderDetailEntity.Quantity += orderDetail.Quantity;
                    orderDetailEntity.Price += orderDetail.Price;
                }
                else
                {
                    order.OrderDetails.Add(orderDetail);
                }
            }

            _orderService.EditOrder(order);
            Response.Cookies.Delete("Order");

        }

        private void FillRegisterBasket(Guid userId)
        {
            var cachedOrderCookie = Request.Cookies["Order"];
            var cachedOrderModified = JsonConvert.DeserializeObject<GuestBasketModel>(cachedOrderCookie);

            var guestOrder = _orderService.GetOrderById(cachedOrderModified.OrderId);
            guestOrder.CustomerId = userId;

            _orderService.EditOrder(guestOrder);
            Response.Cookies.Delete("Order");
        }
    }
}