using Joker.Identity.Models.Seeders.Base;
using Microsoft.AspNetCore.Identity;

namespace Joker.Identity.Models.Seeders.ModelSeeders;

public class RoleSeeder : ISeeder
{
    public int Order => 1;

    public async Task SeedAsync(JokerIdentityDbContext context, ILogger<JokerIdentityDbContext> logger, IServiceProvider serviceProvider)
    {
        try
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!context.Roles.Any())
            {
                string[] roles = new [] {"Admin", "FreeUser", "PaidUser"};

                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "EXCEPTION ERROR while role seed migrating {DbContextName}", nameof(JokerIdentityDbContext));
        }
    }
}