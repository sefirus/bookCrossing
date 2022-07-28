using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class BookCrossingContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Writer> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }
    public DbSet<Category> Categories { get; set; }    
    public DbSet<Comment> Comments { get; set; }
    public DbSet<HistoryRecord> HistoryRecords { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Shelf> Shelves { get; set; }
    public DbSet<User> Users { get; set; }

    public BookCrossingContext(DbContextOptions<BookCrossingContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
            
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    }
}