using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class BookWriterConfiguration : IEntityTypeConfiguration<BookWriter>
{
    public void Configure(EntityTypeBuilder<BookWriter> builder)
    {
        builder
            .HasKey(ps => new
            {
                ps.BookId,
                ps.WriterId
            });

        builder
            .HasOne<Writer>(bw => bw.Writer)
            .WithMany(w => w.BookWriters)
            .HasForeignKey(bw => bw.WriterId)
            .OnDelete(DeleteBehavior.Restrict);          
        
        builder
            .HasOne<Book>(bw => bw.Book)
            .WithMany(b => b.BookWriters)
            .HasForeignKey(bw => bw.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("BookWriters");
    }
}