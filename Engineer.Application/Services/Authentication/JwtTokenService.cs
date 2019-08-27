using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Engineer.Domain.Authorization;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Authentication;
using Engineer.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Engineer.Application.Services.Authentication
{

    public class RefreshTokenDTO {
        public string Token { get; }
        public long Expiration { get; } = DateTimeOffset.UtcNow.AddDays(14).ToUnixTimeMilliseconds();

        public RefreshTokenDTO(string token)
        {
            Token = token;
        }
    }

    public interface IJwtTokenService
    {
        Task<string> GenerateAccessToken(string username);

        Task<RefreshTokenDTO> GenerateRefreshToken(string userId, int seed = 32);

        Task<string> RequestToken(LoginDTO login);

        Task<bool> CheckForValidRefreshToken(RefreshToken refreshToken);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<EngineerUser> _userManager;

        private readonly JwtIssuerOptions _options;

        private readonly ApplicationDbContext _context;

        public JwtTokenService(UserManager<EngineerUser> userManager, IOptions<JwtIssuerOptions> options, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
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

        public async Task<RefreshTokenDTO> GenerateRefreshToken(string userId, int seed = 32){
            using (var randomGenerator = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[seed];
                randomGenerator.GetBytes(randomBytes);

                var refreshToken = Convert.ToBase64String(randomBytes);
                var result = new RefreshTokenDTO(refreshToken);

                _context.RefreshTokens.Add(new RefreshToken
                {
                    Expiration = result.Expiration,
                    Token = result.Token,
                    UserId = userId
                });

                await _context.SaveChangesAsync();

                return result;
            }
        }

        public async Task<string> RequestToken(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Username);
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid Credentials");
            }

            var token = await GenerateAccessToken(login.Username);

            return token;
        }

        public async Task<bool> CheckForValidRefreshToken(RefreshToken refreshToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken.Token);

            if (token == null)
            {
                return false;
            }

            return token.Expiration >= DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        private async Task<ClaimsIdentity> GenerateClaimsIdentityAsync(EngineerUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), claims);
        }
    }
}