﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Netlog.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Netlog.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            SeedDatabase(host);

            host.Run();
        }        

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void SeedDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var NetlogContext = services.GetRequiredService<NetlogContext>();
                    NetlogContextSeed.SeedAsync(NetlogContext, loggerFactory).Wait();
                }
                catch (Exception exception)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(exception, "An error occurred seeding the DB.");
                }
            }
        }
    }
}
