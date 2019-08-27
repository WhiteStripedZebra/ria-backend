using System.Threading.Tasks;
using Engineer.Application.Services.Authentication;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Engineer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IJwtTokenService _tokenService;

        public AuthController(IJwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        [Route("token")]
        public async Task<ActionResult<TokenResponseDTO>> GetToken(LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest("No credentials were found");
            }

            var accessToken = new TokenResponseDTO
            {
                Token = await _tokenService.GenerateAccessToken(login.Username)
            };

            return accessToken;
        }
    }
}
