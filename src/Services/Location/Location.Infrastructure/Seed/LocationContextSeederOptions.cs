using Microsoft.Extensions.Logging;

namespace Location.Infrastructure.Seed;

public class LocationContextSeederOptions
{
    public string ContentRootPath { get; set; }
    public LocationContext Context { get; set; }
    public ILogger<LocationContext> Logger { get; set; }
    public int RetryCount { get; set; }
}