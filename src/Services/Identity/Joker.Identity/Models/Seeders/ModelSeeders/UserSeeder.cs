using Joker.Identity.Models.Entities;
using Joker.Identity.Models.Seeders.Base;
using Microsoft.AspNetCore.Identity;

namespace Joker.Identity.Models.Seeders.ModelSeeders;

public class UserSeeder : ISeeder
{
    public int Order => 2;
    
    public async Task SeedAsync(JokerIdentityDbContext context, ILogger<JokerIdentityDbContext> logger, IServiceProvider serviceProvider)
    {
        try
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!context.Users.Any())
            {

                var sa = new ApplicationUser
                {
                    Email = "sa@mail.com",
                    UserName = "sa@mail.com",
                    EmailConfirmed = true
                };
                
                var identityResult  = await userManager.CreateAsync(sa, "123456");

                if (identityResult.Succeeded)
                {
                    var roleAdded = await userManager.AddToRolesAsync(sa, new []{"Admin", "FreeUser"});
                    if (!roleAdded.Succeeded)
                    {
                        logger.LogError(string.Join("," ,roleAdded.Errors.Select(x=>x.Description)));
                    }
                }

                var user = new ApplicationUser
                {
                    Email = "mesut@mail.com",
                    UserName = "mesut@mail.com",
                    EmailConfirmed = true
                };

                var identityUserResult = await userManager.CreateAsync(user, "123456");

                if (identityUserResult.Succeeded)
                {
                    var roleAdded = await userManager.AddToRolesAsync(user, new []{"FreeUser"});
                    if (!roleAdded.Succeeded)
                    {
                        logger.LogError(string.Join("," ,roleAdded.Errors.Select(x=>x.Description)));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "EXCEPTION ERROR while role seed migrating {DbContextName}", nameof(JokerIdentityDbContext));
        }
    }
}