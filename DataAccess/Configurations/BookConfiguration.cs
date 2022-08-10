using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Title)
            .HasMaxLength(500);        
        
        builder
            .Property(b => b.Description)
            .HasMaxLength(1500)
            .IsRequired(false);

        builder
            .HasOne<Publisher>(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);

        builder
            .Property(b => b.Isbn)
            .HasMaxLength(50);

        builder
            .Property(b => b.Language)
            .HasMaxLength(20);

        builder
            .ToTable("Books");
    }
}