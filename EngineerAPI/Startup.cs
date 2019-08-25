using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Engineer.Api.Authorization;
using Engineer.Api.Authorization.Policies;
using Engineer.Application.IdentityHelpers;
using Engineer.Application.Repository.Tasks;
using Engineer.Application.Services.Authentication;
using Engineer.Domain.Entities;
using Engineer.Domain.Enums;
using Engineer.Domain.Repositories;
using Engineer.Mapping.Helpers;
using Engineer.Mapping.Profiles;
using Engineer.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Engineer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddScoped<ITodoRepository, ToDoRepository>();
            services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddAutoMapper(typeof(ProfileAnchor).GetTypeInfo().Assembly);

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo{ Title = "Rådet for Ingeniørstuderende", Version = "v1"});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                
                config.IncludeXmlComments(xmlPath);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EngineeringCouncil")));

            services.AddIdentity<EngineerUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<EngineerUser>>()
            .AddDefaultTokenProviders();

            var jwtAppSettings = Configuration.GetSection("JwtIssuerOptions");


            services.Configure<JwtAuthentication>(options =>
            {
                options.Issuer = jwtAppSettings["Issuer"];
                options.Audience = jwtAppSettings["Audience"];
                options.SecurityKey = jwtAppSettings["SecurityKey"];
            });


            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.ClaimsIssuer = jwtAppSettings["Issuer"];
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidIssuer = jwtAppSettings["Issuer"],
                        ValidateAudience = false,
                        ValidAudience = jwtAppSettings["Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtAppSettings["SecurityKey"])),
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(CouncilPolicies.IsBoardMember, policy => {
                    policy.RequireAssertion(context =>
                    {
                        var role = context.User.FindFirst(claim => claim.Type == CustomClaimTypes.Role).Value;

                        return role == CouncilRole.BoardMember.ToString();
                    });
                });

                options.AddPolicy(CouncilPolicies.IsBoardMemberOrVolunteer, policy => 
                    policy.RequireAssertion(context =>
                        {
                            var role = context.User.FindFirst(claim => claim.Type == CustomClaimTypes.Role).Value;

                            return role == CouncilRole.BoardMember.ToString() || role == CouncilRole.Volunteer.ToString();
                        }));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<EngineerUser> userManager)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            ApplicationDbInitializer.SeedUsers(userManager);

            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
