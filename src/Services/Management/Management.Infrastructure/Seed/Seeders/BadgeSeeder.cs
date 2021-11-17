using Management.Core.Entities;
using Management.Infrastructure.Seed.Base;
using Microsoft.Extensions.Logging;

namespace Management.Infrastructure.Seed.Seeders;

public class BadgeSeeder : ISeeder
{
    public int Order => 1;

    public Task SeedAsync(ManagementContext context, 
        string contentRootPath,
        ILogger<ManagementContext> logger,
        IServiceProvider serviceProvider)
    {

        if (context.Badges.Any())
            return Task.FromResult(0);

        logger.LogInformation("BadgeSeeder is working");

        return Task.FromResult(context.Badges.AddRangeAsync(LoadBadges(contentRootPath)));
    }

    private List<Badge> LoadBadges(string contentRootPath)
        => Load<List<Badge>>(contentRootPath, "Seeds/badges.json");

    private T Load<T>(string contentRootPath, string fileLocation)
    {
        var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
        return System.Text.Json.JsonSerializer.Deserialize<T>(currencyJson);
    }
}