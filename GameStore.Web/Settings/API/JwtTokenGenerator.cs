using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using GameStore.Interfaces.Web.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Web.Settings.API
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private const string IssuerClaimType = "iss";
        private const string ExpirationClaimType = "exp";

        private readonly ApiAuthSettings _settings;
        private readonly SigningCredentials _credentials;

        public JwtTokenGenerator(IOptions<ApiAuthSettings> settings)
        {
            _settings = settings.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        public string Create(IList<Claim> claims)
        {
            if (claims.All(claim => claim.Type != IssuerClaimType))
            {
                claims.Add(new Claim(IssuerClaimType, _settings.Issuer));
            }

            if (claims.All(claim => claim.Type != ExpirationClaimType))
            {
                var expirationTime = DateTimeOffset.Now.ToUnixTimeSeconds() + _settings.ExpirationTimeInSeconds;
                claims.Add(new Claim(ExpirationClaimType, expirationTime.ToString()));
            }

            var header = new JwtHeader(_credentials);
            var payload = new JwtPayload(claims);

            var securityToken = new JwtSecurityToken(header, payload);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(securityToken);
        }
    }
}