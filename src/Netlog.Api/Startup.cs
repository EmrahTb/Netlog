using Netlog.Application.Interfaces;
using Netlog.Application.Services;
using Netlog.Core.Interfaces;
using Netlog.Core.Repositories;
using Netlog.Core.Repositories.Base;
using Netlog.Infrastructure.Data;
using Netlog.Infrastructure.Logging;
using Netlog.Infrastructure.Repository;
using Netlog.Infrastructure.Repository.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System;
using System.IO;
using System.Reflection;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NETLOG LOJİSTİK API SERVICE",
                    Version = "v3",
                    Description = "Example API that shows how to implement JSON Web Token authentication and authorization with ASP.NET Core 3.1, built from scratch.",
                    Contact = new OpenApiContact
                    {
                        Name = "NetLog Lojistik Api Service",
                        Url = new Uri("https://www.netlog.com.tr")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "NetLog",
                    },
                });

                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token to access resources. Example: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new [] { string.Empty }
                    }
                });


            });
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            ConfigureDatabases(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            // Add Application Layer
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApiUserService, ApiUserService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());

            app.UseSwagger();

            app.UseSwaggerUI(options => {
                options.DocumentTitle = "Netlog Api Services";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Netlog Api Services");
            });

        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            // use in-memory database
            //services.AddDbContext<NetlogContext>(c =>
            //    c.UseInMemoryDatabase("NetlogConnection"));

            //// use real database
            services.AddDbContext<NetlogContext>(c =>
                c.UseNpgsql(Configuration.GetConnectionString("NetlogConnection")), ServiceLifetime.Singleton);
        }

    }
}
