using Netlog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netlog.Infrastructure.Data
{
    public class NetlogContextSeed
    {
        public static async Task SeedAsync(NetlogContext NetlogContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // TODO: Only run this if using a real database
                // NetlogContext.Database.Migrate();
                // NetlogContext.Database.EnsureCreated();


            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<NetlogContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(NetlogContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }
    }
}
