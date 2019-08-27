using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Engineer.Domain.Authorization;
using Engineer.Domain.Entities;
using Engineer.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace Engineer.Persistence
{
    public class ApplicationDbInitializer
    {
        public static async Task SeedUsers(UserManager<EngineerUser> userManager)
        {
            if (userManager.Users.Any())
            {
                return;
            }

            var admin = new EngineerUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = "ria@riaarhus.dk",
                UserName = "ria@riaarhus.dk"
            };

            await userManager.CreateAsync(admin, "N@vitas2018");

            var user = await userManager.FindByEmailAsync(admin.Email);

            var claims = new List<Claim>
            {
                new Claim(Permissions.BoardMember, "")
            };

            await userManager.AddClaimsAsync(user, claims);
        }
    }
}
