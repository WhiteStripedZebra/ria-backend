using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Engineer.Application.IdentityHelpers;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Engineer.Application.Services.Authentication
{
    public interface IJwtTokenService
    {
        Task<TokenResponseDTO> GenerateAccessToken(string username);
        TokenResponseDTO GenerateRefreshToken(int seed = 32);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<EngineerUser> _userManager;

        private readonly IOptions<JwtAuthentication> _jwtAuthentication;

        public JwtTokenService(UserManager<EngineerUser> userManager, IOptions<JwtAuthentication> jwtAuthentication)
        {
            _userManager = userManager;
            _jwtAuthentication = jwtAuthentication;
        }

        public async Task<TokenResponseDTO> GenerateAccessToken(string username)
        {
            var identity = await GenerateClaimsIdentity(username);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64)
            };

            claims.AddRange(identity.Claims);

            var jsonWebToken = new JwtSecurityToken(
                issuer: "https://riaarhus.dk",
                audience: "https://riaarhus.dk",
                claims: claims,
                notBefore: DateTimeOffset.UtcNow.DateTime,
                expires: DateTimeOffset.UtcNow.DateTime.AddMinutes(5),
                signingCredentials: _jwtAuthentication.Value.SigningCredentials
                );

            var encodedJsonWebToken = new JwtSecurityTokenHandler().WriteToken(jsonWebToken);

            return new TokenResponseDTO
            {
                Token = encodedJsonWebToken,
                Expiration =  DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(5)).ToUnixTimeMilliseconds()
            };
        }

        public TokenResponseDTO GenerateRefreshToken(int seed = 32)
        {
            throw new System.NotImplementedException();
        }

        private async Task<ClaimsIdentity> GenerateClaimsIdentity(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);

            var claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim(ClaimTypes.NameIdentifier, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(CustomClaimTypes.Role, $"{user.Role}"));

            return new ClaimsIdentity(new GenericIdentity(username, "Token"), claims);
        }
    }
}