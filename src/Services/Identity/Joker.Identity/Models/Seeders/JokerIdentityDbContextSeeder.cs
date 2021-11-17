using Microsoft.AspNetCore.Identity;

namespace Joker.Identity.Models.Seeders;

public class JokerIdentityDbContextSeeder
{
    public async Task SeedAsync(Action<JokerIdentityDbContextSeederOptions> action, IServiceProvider serviceProvider)
    {
        var options = new JokerIdentityDbContextSeederOptions();
        action.Invoke(options);
            
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
        try
        {
            if (!options.Context.Roles.Any())
            {
                string[] roles = new [] {"Admin", "FreeUser", "PaidUser"};

                foreach (string role in roles)
                {
                    if (!options.Context.Roles.Any(r => r.Name == role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                await options.Context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            options.Logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(JokerIdentityDbContext));
        }
    }
}