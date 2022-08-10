using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.FullPath)
            .HasMaxLength(300);

        builder
            .HasOne<Writer>(p => p.Writer)
            .WithMany(w => w.Pictures)
            .HasForeignKey(p => p.WriterId)
            .IsRequired(false);
                
        builder
            .HasOne<Book>(p => p.Book)
            .WithMany(b => b.Pictures)
            .HasForeignKey(p => p.BookId)
            .OnDelete(DeleteBehavior.Restrict)            
            .IsRequired(false);

        builder
            .HasOne<Publisher>(p => p.Publisher)
            .WithMany(p => p.Pictures)
            .HasForeignKey(p => p.PublisherId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne<Shelf>(p => p.Shelf)
            .WithMany(p => p.Pictures)
            .HasForeignKey(p => p.ShelfId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
        
        builder
            .ToTable("Pictures");
    }
}