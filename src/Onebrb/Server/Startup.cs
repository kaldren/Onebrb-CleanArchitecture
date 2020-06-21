using System;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Onebrb.Core.Entities;
using Onebrb.Core.Interfaces.Repos;
using Onebrb.Core.Interfaces.Services.Messages;
using Onebrb.Infrastructure.Data;
using Onebrb.Infrastructure.Entities;
using Onebrb.Infrastructure.Repositories;
using Onebrb.Server.Utils.Options;
using Onebrb.Services.Messages;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Onebrb.Core.Interfaces.Repos.User;
using Onebrb.Infrastructure.Repositories.User;
using Onebrb.Core.Interfaces.Services.User;
using Onebrb.Services.User;
using AutoMapper;

namespace Onebrb.Server
{
    public class Startup
    {
        private readonly OnebrbOptions _onebrbOptions;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _onebrbOptions = configuration.GetSection(nameof(OnebrbOptions)).Get<OnebrbOptions>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetSection(nameof(OnebrbOptions))[nameof(_onebrbOptions.ConnectionString)]));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddUserManager<ApplicationUserManager>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMediatR(typeof(Startup));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IAsyncRepository<Message, int>, EfRepository<Message, int>>();
            services.AddScoped<IAsyncRepository<ApplicationUser, int>, EfRepository<ApplicationUser, int>>();

            // Messages services
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService<Message>, MessageService>();

            // Users services
            services.AddScoped<IUserRepository<ApplicationUser>, UserRepository>();
            services.AddScoped<IUserService<ApplicationUser>, UserService>();

            // Options
            services.Configure<OnebrbOptions>(Configuration.GetSection(OnebrbOptions.Options));

            // Swagger / OpenAPI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo { 
                        Title = "Onebrb API", 
                        Version = "v1",
                        Description = "Onebrb API to be accessed by Blazor client",
                        Contact = new OpenApiContact
                        {
                            Name = "Kaloyan Drenski",
                            Email = "drenski666@gmail.com",
                            Url = new Uri("https://github.com/kaldren")
                        }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Onebrb API V1");
            });

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
