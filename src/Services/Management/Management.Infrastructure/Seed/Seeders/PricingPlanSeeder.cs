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
    public class PricingPlanSeeder : ISeeder
    {
        public int Order => 1;

        public Task SeedAsync(ManagementContext context, 
            string contentRootPath,
            ILogger<ManagementContext> logger,
            IServiceProvider serviceProvider)
        {
            if (context.PricingPlans.Any())
                return Task.FromResult(0);

            logger.LogInformation("PricingPlanSeeder is working");

            return Task.FromResult(context.PricingPlans.AddRangeAsync(LoadPricingPlans(contentRootPath)));
        }

        private List<PricingPlan> LoadPricingPlans(string contentRootPath)
            => Load<List<PricingPlan>>(contentRootPath, "Seeds/pricingplans.json");

        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return System.Text.Json.JsonSerializer.Deserialize<T>(currencyJson);
        }
    }
}