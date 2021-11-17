using Management.Core.Entities;
using Management.Infrastructure.Seed.Base;
using Microsoft.Extensions.Logging;

namespace Management.Infrastructure.Seed.Seeders;

public class BusinessDirectorySeeder: ISeeder
{
    public int Order => 1;

    public Task SeedAsync(ManagementContext context, 
        string contentRootPath,
        ILogger<ManagementContext> logger,
        IServiceProvider serviceProvider)
    {
        if (context.BusinessDirectories.Any())
            return Task.FromResult(0);

        logger.LogInformation("BusinessDirectorySeeder is working");
        return Task.FromResult(context.BusinessDirectories.AddRangeAsync(LoadBusinessDirectories(contentRootPath)));
    }

    private List<BusinessDirectory> LoadBusinessDirectories(string contentRootPath)
        => Load<List<BusinessDirectory>>(contentRootPath, "Seeds/businessdirectories.json");

    private T Load<T>(string contentRootPath, string fileLocation)
    {
        var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
        return System.Text.Json.JsonSerializer.Deserialize<T>(currencyJson);
    }
}