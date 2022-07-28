namespace Core.Entities;

public class Book
{
    public int Id { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public ICollection<BookWriter> BookWriters { get; set; }
    public string Isbn { get; set; }
    public int PageCount { get; set; } 
    public IEnumerable<Picture> Pictures { get; set; } 
    public string Language { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public IEnumerable<Comment> Comments { get; set; } 
    public IEnumerable<BookCopy> BookCopies { get; set; }
}