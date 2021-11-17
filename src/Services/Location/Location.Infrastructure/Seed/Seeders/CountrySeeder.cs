using Location.Core.Entities;
using Location.Infrastructure.Seed.Seeders.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Location.Infrastructure.Seed.Seeders;

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
        return System.Text.Json.JsonSerializer.Deserialize<T>(currencyJson);
    }
}