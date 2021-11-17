using Joker.EntityFrameworkCore;
using Location.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Location.Infrastructure;

public class LocationContext : JokerDbContext
{
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<District> Districts { get; set; }
    public virtual DbSet<Neighborhood> Neighborhoods { get; set; }
    public virtual DbSet<Quarter> Quarters { get; set; }

    public LocationContext()
    {
    }

    public LocationContext(DbContextOptions<LocationContext> options)
        : base(options)
    {
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql();
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocationContext).Assembly);
    }
}