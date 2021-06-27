using System;
using Management.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Management.Infrastructure.EntityTypeConfigurations
{
    public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> entity)
        {
            entity.Property(e => e.Id)
                .HasDefaultValue(Guid.NewGuid());
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);
            
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.Description)
                .HasMaxLength(250);

            entity.Property(e => e.FontColor)
                .HasMaxLength(250);

            entity.Property(e => e.BackgroundColor)
                .HasMaxLength(250);
        }
    }
}