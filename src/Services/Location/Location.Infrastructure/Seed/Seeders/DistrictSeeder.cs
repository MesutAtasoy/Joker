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
    public class DistrictSeeder: ISeeder
    {
        public int Order => 3;

        public async Task SeedAsync(LocationContext context, 
            string contentRootPath, 
            ILogger<LocationContext> logger,
            IServiceProvider serviceProvider)
        {
            if (context.Districts.Any())
            {
                return;
            }
            
            logger.LogInformation("DistrictSeeder is working");

            var districts = LoadDistricts(contentRootPath);
            foreach (var district in districts)
            {
                context.Entry(district).State = EntityState.Modified;
                await context.Districts.AddAsync(district);
            }
        }
        
        private List<District> LoadDistricts(string contentRootPath)
        {
            var districts = Load<List<TempJsonModels.TempDistrict>>(contentRootPath, "Seeds/districts.json");

            List<District> districtsList = new List<District>();
            foreach (var item in districts)
            {
                District district = new District();
                district = item;
                district.CountryId = item.City.Country.Id;
                district.CityId = item.City.Id;
                districtsList.Add(district);
            }

            return districtsList;
        }
        
        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return JsonConvert.DeserializeObject<T>(currencyJson);
        }
    }
}