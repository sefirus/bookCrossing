namespace Core.Entities;

public class Book
{
    public int Id { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public ICollection<BookAuthor> BookAuthor { get; set; }
    public IEnumerable<string> Isbns { get; set; }
    public int PageCount { get; set; } 
    public IEnumerable<Picture> Pictures { get; set; } 
    public string Language { get; set; }
    public Category Category { get; set; }
    public IEnumerable<Comment> Comments { get; set; } 
}