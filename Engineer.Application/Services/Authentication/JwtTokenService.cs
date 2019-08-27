using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Engineer.Domain.Authorization;
using Engineer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Engineer.Application.Services.Authentication
{

    public interface IJwtTokenService
    {
        Task<string> GenerateAccessToken(string username);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<EngineerUser> _userManager;

        private readonly JwtIssuerOptions _options;

        public JwtTokenService(UserManager<EngineerUser> userManager, IOptions<JwtIssuerOptions> options)
        {
            _userManager = userManager;
            _options = options.Value;
        }

        public async Task<string> GenerateAccessToken(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var identity = await GenerateClaimsIdentityAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, _options.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, _options.IssuedAt.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64)
            };

            claims.AddRange(identity.Claims);

            var jwt = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                _options.NotBefore.UtcDateTime,
                _options.Expires.UtcDateTime,
                _options.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private async Task<ClaimsIdentity> GenerateClaimsIdentityAsync(EngineerUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), claims);
        }
    }
}