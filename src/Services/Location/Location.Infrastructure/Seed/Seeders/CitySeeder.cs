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
    public class CitySeeder : ISeeder
    {
        public int Order => 2;

        public async Task SeedAsync(LocationContext context, 
            string contentRootPath, 
            ILogger<LocationContext> logger,
            IServiceProvider serviceProvider)
        {
            if (context.Cities.Any())
            {
                return;
            } 
            
            logger.LogInformation("CitySeeder is working");

            var cities = LoadCities(contentRootPath);
            
            foreach (var city in cities)
            {
                context.Entry(city).State = EntityState.Modified;
                await context.Cities.AddAsync(city);
            }
        }
        
        private List<City> LoadCities(string contentRootPath)
            => Load<List<City>>(contentRootPath, "Seeds/cities.json");
        
        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return JsonConvert.DeserializeObject<T>(currencyJson);
        }
    }
}