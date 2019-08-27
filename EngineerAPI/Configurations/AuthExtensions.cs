using System;
using System.Text;
using AutoMapper.Configuration;
using Engineer.Application.Services.Authentication;
using Engineer.Domain.Authorization;
using Engineer.Domain.Entities;
using Engineer.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Engineer.Api.Configurations
{
    public static class AuthExtensions
    {
        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<EngineerUser, IdentityRole>(PasswordSettings.GetAll)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var settings = new JwtIssuerOptions();  
            configuration.GetSection(nameof(JwtIssuerOptions)).Bind(settings);

            var signingKey = configuration["SIGNING_KEY"] ?? "Signing key only for testing";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));
            var issuer = settings.Issuer ?? "https://localhost";
            var audience = settings.Audience ?? "https://localhost";

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = issuer;
                options.Audience = audience;
                options.SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            });

            var authenticationBuilder = services.AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.ClaimsIssuer = settings.Issuer;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = symmetricSecurityKey,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return authenticationBuilder;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(Policies.BoardMember), p => p.RequireClaim(Permissions.BoardMember));
                options.AddPolicy(nameof(Policies.Volunteer), p => p.RequireClaim(Permissions.Volunteer));
            });

            return services;
        }
    }
}