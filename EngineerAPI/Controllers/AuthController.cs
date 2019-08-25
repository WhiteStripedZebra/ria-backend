using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineer.Application.Services.Authentication;
using Engineer.Domain.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Engineer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IJwtAuthenticationService _authenticationService;

        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IJwtAuthenticationService authenticationService, IJwtTokenService jwtTokenService)
        {
            _authenticationService = authenticationService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        [Route("token")]
        public async Task<ActionResult<TokenResponseDTO>> GetToken(LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest("No credentials were found");
            }

            var result = await _authenticationService.SignInAsync(login.Username, login.Password);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var accessTokenResponse = await _jwtTokenService.GenerateAccessToken(login.Username);

            return accessTokenResponse;
        }
    }
}
