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
    public class NeighborhoodSeeder: ISeeder
    {
        public int Order => 4;

        public async Task SeedAsync(LocationContext context, 
        string contentRootPath,
        ILogger<LocationContext> logger,
        IServiceProvider serviceProvider)
        {
            if (context.Neighborhoods.Any()) 
                return;
            logger.LogInformation("NeighborhoodSeeder is working");

            var (neighborhoods, quarters) = LoadNeighborhoods(contentRootPath);
            foreach (var neighborhood in neighborhoods)
            {
                context.Entry(neighborhood).State = EntityState.Modified;
                await context.Neighborhoods.AddAsync(neighborhood);
            }

            foreach (var quater in quarters)
            {
                context.Entry(quater).State = EntityState.Modified;
                await context.Quarters.AddAsync(quater);
            }
        }
        
        private (List<Neighborhood> neighborhoods, List<Quarter> quarters) LoadNeighborhoods(string contentRootPath)
        {
            var neighborhoods = Load<List<TempJsonModels.TempNeighborhood>>(contentRootPath, "Seeds/neighborhoods.json");
            List<Neighborhood> neighborhoodList = new List<Neighborhood>();

            List<Quarter> quarterList = new List<Quarter>();
            foreach (var item in neighborhoods)
            {
                var neighborhood = new Neighborhood
                {
                    Country = item.District.Country,
                    CountryId = item.District.City.Country.Id,
                    City = item.District.City,
                    CityId = item.District.City.Id,
                    District = item.District,
                    DistrictId = item.District.Id,
                    Id = item.Id,
                    Name = item.Name,
                    Quarters = new List<Quarter>()
                };


                foreach (var _quarter in item.Quarters)
                {
                    Quarter quarter = new Quarter
                    {
                        IsDeleted = false,
                        CountryId = neighborhood.CountryId,
                        CityId = neighborhood.CityId,
                        DistrictId = neighborhood.DistrictId,
                        NeighborhoodId = neighborhood.Id,
                        Name = _quarter.Name,
                        Id = _quarter.Id
                    };
                    quarterList.Add(quarter);
                }

                neighborhoodList.Add(neighborhood);
            }

            return (neighborhoodList, quarterList);
        }
        
        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return JsonConvert.DeserializeObject<T>(currencyJson);
        }
    }
}