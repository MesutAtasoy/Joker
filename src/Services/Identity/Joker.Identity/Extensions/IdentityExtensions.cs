using Joker.Identity.Models;
using Joker.Identity.Models.Entities;
using Microsoft.AspNetCore.Identity;


namespace Joker.Identity.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddJokerIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(c =>
            {
                c.Password.RequireDigit = false;
                c.Password.RequiredLength = 6;
                c.Password.RequireLowercase = false;
                c.Password.RequireUppercase = false;
                c.Password.RequireNonAlphanumeric = false;
                c.Password.RequiredUniqueChars = 0;
                c.Lockout.AllowedForNewUsers = false;
                c.Lockout.MaxFailedAccessAttempts = 20;
            })
            .AddEntityFrameworkStores<JokerIdentityDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}