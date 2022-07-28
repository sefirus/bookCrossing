using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
{
    public void Configure(EntityTypeBuilder<BookCopy> builder)
    {
        builder
            .HasKey(bc => bc.Id);

        builder
            .HasOne<Book>(bc => bc.Book)
            .WithMany(b => b.BookCopies)
            .HasForeignKey(bc => bc.BookId);

        builder
            .Property(bc => bc.State)
            .HasConversion<int>();

        builder
            .HasOne<User>(bc => bc.CurrentUser)
            .WithMany(u => u.CurrentBooks)
            .HasForeignKey(bc => bc.CurrentUserId);        
        
        builder
            .HasOne<Shelf>(bc => bc.CurrentShelf)
            .WithMany(sh => sh.Books)
            .HasForeignKey(bc => bc.CurrentShelfId);

        builder
            .ToTable("BookCopies");
    }
}