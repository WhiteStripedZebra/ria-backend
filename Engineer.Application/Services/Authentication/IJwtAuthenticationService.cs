using System;
using System.Threading.Tasks;
using Engineer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Engineer.Application.Services.Authentication
{
    public interface IJwtAuthenticationService
    {
        Task<IdentityResult> SignInAsync(string email, string password);
    }

    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly UserManager<EngineerUser> _userManager;

        public JwtAuthenticationService(UserManager<EngineerUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null) { return IdentityResult.Failed(); }

            var validPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!validPassword) { return IdentityResult.Failed(); }

            user.LastLoginTimestamp = DateTimeOffset.Now;

            await _userManager.UpdateAsync(user);

            return IdentityResult.Success;
        }
    }
}