using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
{
    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        builder
            .HasKey(bc => new
            {
                bc.BookId,
                bc.CategoryId
            });
        
        builder
            .HasOne<Book>(bc => bc.Book)
            .WithMany(book => book.BookCategories)
            .HasForeignKey(bc => bc.BookId)
            .OnDelete(DeleteBehavior.Restrict);  
        
        builder
            .HasOne<Category>(bc => bc.Category)
            .WithMany(cat => cat.BookCategories)
            .HasForeignKey(bc => bc.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("BookCategories");
    }
}