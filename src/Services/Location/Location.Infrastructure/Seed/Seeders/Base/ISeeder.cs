using Microsoft.Extensions.Logging;

namespace Location.Infrastructure.Seed.Seeders.Base;

public interface ISeeder
{
    public int Order { get; }
    Task SeedAsync(LocationContext context, 
        string contentRootPath,
        ILogger<LocationContext> logger,
        IServiceProvider serviceProvider);
}