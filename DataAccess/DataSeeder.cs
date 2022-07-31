using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public static class DataSeeder
{
    
    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole<int>>().HasData(new[]
        {
            new IdentityRole<int>() {Id = 1, Name = "Admin", NormalizedName = "Admin".ToUpper()},
            new IdentityRole<int>() {Id = 4, Name = "Client", NormalizedName = "Client".ToUpper()}
        });
    }
    
    private static void AppendChildCategories(Category parentCategory, IEnumerable<Category> childCategories)
    {
        foreach (var cat in childCategories)
        {
            cat.ParentCategoryId = parentCategory.Id;
            cat.Description = string.Empty;
        }
    }
    
    public static void SeedCategories(this ModelBuilder modelBuilder)
    {
        var parentCategory = new Category()
        {
            Id = 1,
            ParentCategoryId = null,
            Description = string.Empty,
            Name = string.Empty,
        };
        var firstLayer = new List<Category>
        {
            new (){ Id = 2, Name = "Magazines" }, //0
            new (){ Id = 3, Name = "Fiction" }, //1
            new (){ Id = 4, Name = "Non-Fiction" }, //2
            new (){ Id = 5, Name = "Handbooks" }, //3
            new (){ Id = 6, Name = "Uncategorized" } //4
        };
        AppendChildCategories(parentCategory, firstLayer);
        
        var fictions = new List<Category>
        {
            new() { Id = 7, Name = "Detective" },
            new() { Id = 8, Name = "Prose" },
            new() { Id = 9, Name = "Sci-Fi" },
            new() { Id = 10, Name = "Fantasy" },
            new() { Id = 11, Name = "Horrors" },
            new() { Id = 12, Name = "Poetry" },
            new() { Id = 13, Name = "Drama" },
            new() { Id = 14, Name = "Historical Novels" }
        };
        AppendChildCategories(firstLayer[1], fictions);

        var nonFictions = new List<Category>
        {
            new() { Id = 15, Name = "Esotericism" },
            new() { Id = 16, Name = "Business" },
            new() { Id = 17, Name = "Social" },
            new() { Id = 18, Name = "Biographies" },
            new() { Id = 19, Name = "Economics" },
            new() { Id = 20, Name = "Technical literature" },
            new() { Id = 21, Name = "Science" },
            new() { Id = 22, Name = "Philosophy" },
            new() { Id = 23, Name = "Motivational literature" },
            new() { Id = 24, Name = "Religion" },
            new() { Id = 25, Name = "Self Development" },
            new() { Id = 26, Name = "Hobbies" }
        };
        AppendChildCategories(firstLayer[2], nonFictions);

        var handbooks = new List<Category>
        {
            new() { Id = 27, Name = "Reference books" },
            new() { Id = 28, Name = "Encyclopedias" },
            new() { Id = 29, Name = "Reference books" },
            new() { Id = 30, Name = "Educational Literature" },
            new() { Id = 31, Name = "Dictionaries" },
            new() { Id = 32, Name = "Maps" },
            new() { Id = 33, Name = "Atlases" }
        };
        AppendChildCategories(firstLayer[3], handbooks);

        var secondLayer = fictions.Concat(nonFictions).Concat(handbooks);
        var resultList = new List<Category> {parentCategory}.Concat(firstLayer).Concat(secondLayer).AsEnumerable();

        modelBuilder
            .Entity<Category>()
            .HasData(resultList);
    }
}