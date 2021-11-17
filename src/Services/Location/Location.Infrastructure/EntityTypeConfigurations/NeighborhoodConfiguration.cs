using Location.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Location.Infrastructure.EntityTypeConfigurations;

public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
{
    public void Configure(EntityTypeBuilder<Neighborhood> builder)
    {
        builder.Property(e => e.Id)
            .HasDefaultValue(Guid.NewGuid());
            
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasOne(d => d.City)
            .WithMany(p => p.Neighborhoods)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Neighborhood_City");

        builder.HasOne(d => d.Country)
            .WithMany(p => p.Neighborhoods)
            .HasForeignKey(d => d.CountryId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Neighborhood_Country");

        builder.HasOne(d => d.District)
            .WithMany(p => p.Neighborhoods)
            .HasForeignKey(d => d.DistrictId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Neighborhood_District");
    }
}