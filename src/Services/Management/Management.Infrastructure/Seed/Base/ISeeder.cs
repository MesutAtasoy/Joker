using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Management.Infrastructure.Seed.Base
{
    public interface ISeeder
    {
        public int Order { get; }
        Task SeedAsync(ManagementContext context, string contentRootPath, ILogger<ManagementContext> logger, IServiceProvider serviceProvider);
    }
}