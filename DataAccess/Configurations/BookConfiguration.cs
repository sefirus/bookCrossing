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
            .HasMaxLength(1500);

        builder
            .HasOne<Publisher>(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);

        builder
            .Property(b => b.Isbn)
            .HasMaxLength(50);

        // builder
        //     .HasMany<Picture>(b => b.Pictures)
        //     .WithOne(p => p.Book)
        //     .HasForeignKey(b => b.BookId);

        builder
            .Property(b => b.Language)
            .HasMaxLength(20);

        builder
            .HasOne<Category>(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);

        builder
            .ToTable("Books");
    }
}