using System;
using Management.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Management.Infrastructure.EntityTypeConfigurations
{
    public class BusinessDirectoryFeatureConfiguration : IEntityTypeConfiguration<BusinessDirectoryFeature>
    {
        public void Configure(EntityTypeBuilder<BusinessDirectoryFeature> entity)
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
            
            entity.HasOne(d => d.BusinessDirectory)
                .WithMany(p => p.BusinessDirectoryFeatures)
                .HasForeignKey(d => d.BusinessDirectoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_BusinessDirectoryFeature_BusinessDirectory");
        }
    }
}