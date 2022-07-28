using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class HistoryRecordConfiguration : IEntityTypeConfiguration<HistoryRecord>
{
    public void Configure(EntityTypeBuilder<HistoryRecord> builder)
    {
        builder
            .HasKey(hr => hr.Id);

        builder
            .Property(hr => hr.Type)
            .HasConversion<int>();

        builder
            .HasOne<BookCopy>(hr => hr.BookCopy)
            .WithMany(bc => bc.HistoryRecords)
            .HasForeignKey(hr => hr.BookCopyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne<User>(hr => hr.User)
            .WithMany(bc => bc.HistoryRecords)
            .HasForeignKey(hr => hr.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne<Shelf>(hr => hr.Shelf)
            .WithMany(bc => bc.HistoryRecords)
            .HasForeignKey(hr => hr.ShelfId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .ToTable("HistoryRecords");
    }
}