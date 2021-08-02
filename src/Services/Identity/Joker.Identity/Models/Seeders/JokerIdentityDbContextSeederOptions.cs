using Microsoft.Extensions.Logging;

namespace Joker.Identity.Models.Seeders
{
    public class JokerIdentityDbContextSeederOptions
    {
        public JokerIdentityDbContext Context { get; set; }
        public ILogger<JokerIdentityDbContext> Logger { get; set; }
        public int RetryCount { get; set; }
    }
}