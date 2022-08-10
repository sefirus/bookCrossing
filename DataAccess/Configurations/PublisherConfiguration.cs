using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder
            .HasKey(p => p.Id);
        
        builder
            .Property(p => p.Name)
            .HasMaxLength(250);

        builder
            .Property(p => p.Description)
            .HasMaxLength(600)
            .IsRequired(false);

        builder
            .ToTable("Publishers");
    }
}