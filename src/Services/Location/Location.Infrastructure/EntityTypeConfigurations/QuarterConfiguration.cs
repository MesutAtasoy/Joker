using Location.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Location.Infrastructure.EntityTypeConfigurations;

public class QuarterConfiguration : IEntityTypeConfiguration<Quarter>
{
    public void Configure(EntityTypeBuilder<Quarter> builder)
    {
        builder.Property(e => e.Id)
            .HasDefaultValue(Guid.NewGuid());
            
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasOne(d => d.City)
            .WithMany(p => p.Quarters)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Quarter_City");

        builder.HasOne(d => d.Country)
            .WithMany(p => p.Quarters)
            .HasForeignKey(d => d.CountryId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Quarter_Country");

        builder.HasOne(d => d.District)
            .WithMany(p => p.Quarters)
            .HasForeignKey(d => d.DistrictId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Quarter_District");

        builder.HasOne(d => d.Neighborhood)
            .WithMany(p => p.Quarters)
            .HasForeignKey(d => d.NeighborhoodId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Quarter_Neighborhood");
            
    }
}