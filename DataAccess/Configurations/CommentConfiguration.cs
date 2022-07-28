using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasKey(c => c.Id);
        
        builder
            .Property(c => c.Content)
            .HasMaxLength(400);
        
        builder
            .HasOne<User>(comment => comment.Author)
            .WithMany(user => user.Comments)
            .HasForeignKey(comment => comment.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);        
        
        builder
            .HasOne<Shelf>(comment => comment.Shelf)
            .WithMany(c => c.Comments)
            .HasForeignKey(comment => comment.ShelfId)
            .OnDelete(DeleteBehavior.Restrict);        
        
        builder
            .HasOne<BookCopy>(comment => comment.BookCopy)
            .WithMany(c => c.Comments)
            .HasForeignKey(comment => comment.BookCopyId)
            .OnDelete(DeleteBehavior.Restrict);        
        
        builder
            .HasOne<Book>(comment => comment.Book)
            .WithMany(c => c.Comments)
            .HasForeignKey(comment => comment.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Comments");
    }
}