using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Location.Core.Entities;
using Location.Infrastructure.Seed.Seeders.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Location.Infrastructure.Seed.Seeders
{
    public class CountrySeeder : ISeeder
    {
        public int Order => 1;

        public async Task SeedAsync(LocationContext context,
            string contentRootPath,
            ILogger<LocationContext> logger,
            IServiceProvider serviceProvider)
        {
            if (context.Countries.Any())
            {
                return;
            }

            logger.LogInformation("CountrySeeder is working");
            var countries = LoadCountries(contentRootPath);
            foreach (var country in countries)
            {
                context.Entry(country).State = EntityState.Modified;
                await context.Countries.AddAsync(country);
            }
        }

        private List<Country> LoadCountries(string contentRootPath)
            => Load<List<Country>>(contentRootPath, "Seeds/countries.json");

        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return JsonConvert.DeserializeObject<T>(currencyJson);
        }
    }
}