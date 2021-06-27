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
    public class LanguageSeeder : ISeeder
    {
        public int Order => 1;

        public Task SeedAsync(ManagementContext context, 
            string contentRootPath,
            ILogger<ManagementContext> logger,
            IServiceProvider serviceProvider)
        {
            if (context.Languages.Any()) 
                return Task.FromResult(0);
            
            logger.LogInformation("LanguageSeeder is working");

            return Task.FromResult(context.Languages.AddRangeAsync( LoadLanguages(contentRootPath)));
        }
        
        private List<Language> LoadLanguages(string contentRootPath)
            => Load<List<Language>>(contentRootPath, "Seeds/languages.json");
        
        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return System.Text.Json.JsonSerializer.Deserialize<T>(currencyJson);
        }
    }
}