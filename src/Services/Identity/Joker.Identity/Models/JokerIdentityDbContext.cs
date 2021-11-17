using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Joker.Identity.Models;

public class JokerIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        
    public JokerIdentityDbContext(DbContextOptions<JokerIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}