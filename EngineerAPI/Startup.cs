using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Engineer.Api.Configurations;
using Engineer.Application.Repository;
using Engineer.Application.Repository.Tasks;
using Engineer.Application.Services.Authentication;
using Engineer.Application.Services.Mail;
using Engineer.Domain.Entities;
using Engineer.Domain.Repositories;
using Engineer.Hubs;
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
using Newtonsoft.Json;
using Serilog;

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

        private readonly string AllowedSpecificOrigins = "_AllowedSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddScoped<ITodoRepository, ToDoRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IMailService, MailService>();

            services.AddJwtAuthentication(Configuration);
            services.AddAuthorizationPolicies();

            services.AddCors(options =>
            {

                options.AddDefaultPolicy(builder =>
                    {
                        builder
                            .WithOrigins("https://localhost:4200", "http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });

                options.AddPolicy(AllowedSpecificOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins("https://localhost:4200", "http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });

            });


            services.AddAutoMapper(typeof(ProfileAnchor).GetTypeInfo().Assembly);


            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo{ Title = "Rådet for Ingeniørstuderende", Version = "v1"});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                
                config.IncludeXmlComments(xmlPath);
            });


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling =
                            ReferenceLoopHandling.Ignore;
                    });
           

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EngineeringCouncil")));

            services.AddSignalR(config =>
            {
                config.KeepAliveInterval = TimeSpan.FromSeconds(120);
                config.EnableDetailedErrors = true;
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

            app.UseCors(AllowedSpecificOrigins);

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSignalR(routes => routes.MapHub<ChatHub>("/chat"));
            

            app.UseMvc();
        }
    }
}
