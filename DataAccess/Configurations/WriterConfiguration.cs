using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class WriterConfiguration : IEntityTypeConfiguration<Writer>
{
    public void Configure(EntityTypeBuilder<Writer> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.FullName)
            .HasMaxLength(200);

        builder
            .Property(u => u.Description)
            .HasMaxLength(1000);

        builder
            .ToTable("Writers");
    }
}