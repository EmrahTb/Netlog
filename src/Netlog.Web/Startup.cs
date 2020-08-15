using Netlog.Application.Interfaces;
using Netlog.Application.Services;
using Netlog.Core;
using Netlog.Core.Interfaces;
using Netlog.Infrastructure.Logging;
using Netlog.Infrastructure.Data;
using Netlog.Infrastructure.Repository;
using Netlog.Web.HealthChecks;
using Netlog.Web.Interfaces;
using Netlog.Web.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netlog.Core.Repositories;
using Netlog.Core.Repositories.Base;
using Netlog.Core.Configuration;
using Netlog.Infrastructure.Repository.Base;

namespace Netlog.Web
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
            // Netlog dependencies
            ConfigureNetlogServices(services);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for Userion scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureNetlogServices(IServiceCollection services)
        {
            // Add Core Layer
            services.Configure<NetlogSettings>(Configuration);

            // Add Infrastructure Layer
            ConfigureDatabases(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            // Add Application Layer
            services.AddScoped<IUserService, UserService>();

            // Add Web Layer
            services.AddAutoMapper(typeof(Startup)); // Add AutoMapper
            services.AddScoped<IIndexPageService, IndexPageService>();
            services.AddScoped<IUserPageService, UserPageService>();
            services.AddScoped<IMaintenancePageService, MaintenancePageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();

            // Add Miscellaneous
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                .AddCheck<IndexPageHealthCheck>("home_page_health_check");
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
