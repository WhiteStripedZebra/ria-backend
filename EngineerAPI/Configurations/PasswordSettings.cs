using Microsoft.AspNetCore.Identity;

namespace Engineer.Api.Configurations
{
    public static class PasswordSettings
    {
        public static void GetAll(IdentityOptions options)
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.User.RequireUniqueEmail = true;
        }
    }
}