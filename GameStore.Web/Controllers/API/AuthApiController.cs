using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Interfaces.Services;
using GameStore.Interfaces.Web.Settings;
using GameStore.Web.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers.API
{
    [Route("api")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenFactory;

        public AuthApiController(IUserService userService,
            ITokenGenerator tokenFactory)
        {
            _userService = userService;
            _tokenFactory = tokenFactory;
        }

        [HttpPost("login")]
        public IActionResult Token([FromForm]LoginViewModel credentials)
        {
            var user = _userService.GetUserByEmail(credentials.Email);

            if (user == null || !Crypto.VerifyHashedPassword(user.Password, credentials.Password))
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            return Ok(GenerateTokenForUser(user));
        }

        private string GenerateTokenForUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Role.Name)));

            return _tokenFactory.Create(claims);
        }
    }
}