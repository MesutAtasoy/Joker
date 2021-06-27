using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Infrastructure.Seed.Base;
using Microsoft.Extensions.Logging;

namespace Management.Infrastructure.Seed.Seeders
{
    public class CurrencySeeder : ISeeder
    {
        public int Order => 1;

        public Task SeedAsync(ManagementContext context,
            string contentRootPath,
            ILogger<ManagementContext> logger,
            IServiceProvider serviceProvider)
        {
            if (context.Currencies.Any())
                return Task.FromResult(0);
            
            logger.LogInformation("CurrencySeeder is working");

            return Task.FromResult(context.Currencies.AddRangeAsync(LoadCurrencies(contentRootPath)));
        }

        private List<Currency> LoadCurrencies(string contentRootPath)
            => Load<List<Currency>>(contentRootPath, "Seeds/currencies.json");

        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return System.Text.Json.JsonSerializer.Deserialize<T>(currencyJson);
        }
    }
}