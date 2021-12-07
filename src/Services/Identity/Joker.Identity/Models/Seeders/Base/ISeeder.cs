namespace Joker.Identity.Models.Seeders.Base;

public interface ISeeder
{
    int Order { get; }
    Task SeedAsync(JokerIdentityDbContext context,  
        ILogger<JokerIdentityDbContext> logger,
        IServiceProvider serviceProvider);

}