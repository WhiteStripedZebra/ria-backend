using System;
using System.Linq;
using System.Threading.Tasks;
using Engineer.Application.Services.Authentication;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Authentication;
using Engineer.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Engineer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IJwtTokenService _tokenService;
        private readonly UserManager<EngineerUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AuthController(IJwtTokenService tokenService, UserManager<EngineerUser> userManager, ApplicationDbContext context)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Route("token")]
        public async Task<ActionResult<TokenResponseDTO>> GetToken(LoginDTO credentials)
        {
            if (credentials == null)
            {
                return BadRequest("No credentials were found");
            }

            var user = await _userManager.FindByEmailAsync(credentials.Username);
            var refreshToken = await _tokenService.GenerateRefreshToken(user.Id);

            HttpContext.Response.Cookies.Append("MyKey", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.FromUnixTimeMilliseconds(refreshToken.Expiration),
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            var accessToken = new TokenResponseDTO
            {
                Token = await _tokenService.RequestToken(credentials)
            };

            return Ok(accessToken);
        }

        [HttpGet]
        [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
        [Route("refresh")]
        public async Task<ActionResult<TokenResponseDTO>> RefreshToken()
        {
            HttpContext.Request.Cookies.TryGetValue("MyKey", out var refreshToken);

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Unauthorized();
            }

            var hasValidRefreshToken = await _tokenService.CheckForValidRefreshToken(new RefreshToken{Token = refreshToken});

            if (!hasValidRefreshToken)
            {
                return Unauthorized();
            }

            var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
            var user = await _userManager.FindByIdAsync(token.UserId);

            var accessToken = new TokenResponseDTO
            {
                Token = await _tokenService.GenerateAccessToken(user.Email)
            };

            return Ok(accessToken);
        }
    }
}
