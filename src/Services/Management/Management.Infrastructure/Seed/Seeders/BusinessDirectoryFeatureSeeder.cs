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
    public class BusinessDirectoryFeatureSeeder : ISeeder
    {
        public int Order => 2;

        public Task SeedAsync(ManagementContext context, 
            string contentRootPath,
            ILogger<ManagementContext> logger,
            IServiceProvider serviceProvider)
        {
            if (context.BusinessDirectoryFeatures.Any())
                return Task.FromResult(0);
            
            logger.LogInformation("BusinessDirectoryFeatureSeeder is working");
            var entities = LoadBusinessDirectoryFeatures(contentRootPath);
            return Task.FromResult(context.BusinessDirectoryFeatures.AddRangeAsync(entities));
        }

        private List<BusinessDirectoryFeature> LoadBusinessDirectoryFeatures(string contentRootPath)
            => Load<List<BusinessDirectoryFeature>>(contentRootPath, "Seeds/businessdirectoryfeatures.json");

        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return System.Text.Json.JsonSerializer.Deserialize<T>(currencyJson);
        }
    }
}