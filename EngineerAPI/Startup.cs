using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Engineer.Api.Configurations;
using Engineer.Application.Repository.Tasks;
using Engineer.Application.Services.Authentication;
using Engineer.Domain.Entities;
using Engineer.Domain.Repositories;
using Engineer.Mapping.Helpers;
using Engineer.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

namespace Engineer.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            var shouldReloadConfigurations = !environment.EnvironmentName.Equals("Test");

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: shouldReloadConfigurations)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true,
                    reloadOnChange: shouldReloadConfigurations)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddScoped<ITodoRepository, ToDoRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();


            services.AddJwtAuthentication(Configuration);
            services.AddAuthorizationPolicies();


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
