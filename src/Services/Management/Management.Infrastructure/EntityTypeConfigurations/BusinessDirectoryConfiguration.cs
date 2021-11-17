using Management.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Management.Infrastructure.EntityTypeConfigurations;

public class BusinessDirectoryConfiguration : IEntityTypeConfiguration<BusinessDirectory>
{
    public void Configure(EntityTypeBuilder<BusinessDirectory> entity)
    {
        entity.Property(e => e.Id)
            .HasDefaultValue(Guid.NewGuid());
            
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.Order)
            .IsRequired();

        entity.Property(e => e.DisplayName)
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.Description)
            .HasMaxLength(250);

        entity.Property(e => e.Icon)
            .HasMaxLength(250);

        entity.Property(e => e.ImageUrl)
            .HasMaxLength(250);
    }
}