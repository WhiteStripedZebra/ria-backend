using System;
using System.Collections.Generic;
using System.Text;
using Engineer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Engineer.Persistence
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<EngineerUser> userManager)
        {
            if (userManager.FindByEmailAsync("ria@riaarhus.dk").Result == null)
            {
                EngineerUser user = new EngineerUser
                {
                    UserName = "ria@riaarhus.dk",
                    Email = "ria@riaarhus.dk"
                };

                IdentityResult result = userManager.CreateAsync(user, "N@vitas2018").Result;
            }
        }
    }
}
