using Microsoft.Extensions.Logging;

namespace Management.Infrastructure.Seed
{
    public class ManagementContextSeederOptions
    {
        public string ContentRootPath { get; set; }
        public ManagementContext Context { get; set; }
        public ILogger<ManagementContext> Logger { get; set; }
        public int RetryCount { get; set; }
    }
}