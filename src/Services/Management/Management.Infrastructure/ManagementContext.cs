using Joker.EntityFrameworkCore;
using Management.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure
{
    public class ManagementContext : JokerDbContext
    {
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<BusinessDirectory> BusinessDirectories { get; set; }
        public virtual DbSet<BusinessDirectoryFeature> BusinessDirectoryFeatures { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PricingPlan> PricingPlans { get; set; }
        public virtual DbSet<SocialProvider> SocialProviders { get; set; }

        public ManagementContext()
        {
        }

        public ManagementContext(DbContextOptions<ManagementContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ManagementContext).Assembly);
        }
    }
}