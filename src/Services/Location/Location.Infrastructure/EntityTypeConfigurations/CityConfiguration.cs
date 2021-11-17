using Location.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Location.Infrastructure.EntityTypeConfigurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(e => e.Id)
            .HasDefaultValue(Guid.NewGuid());
            
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasOne(d => d.Country)
            .WithMany(p => p.Cities)
            .HasForeignKey(d => d.CountryId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_City_Country");
    }
}