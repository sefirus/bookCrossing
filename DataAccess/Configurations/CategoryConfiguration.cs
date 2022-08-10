using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .HasMaxLength(150);

        builder
            .Property(c => c.Description)
            .HasMaxLength(600)
            .IsRequired(false);

        builder
            .Property(c => c.ParentCategoryId)
            .IsRequired(false);

        builder
            .HasOne<Category>(c => c.PrentCategory)
            .WithMany(c => c.ChildCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .ToTable("Categories");
    }
}