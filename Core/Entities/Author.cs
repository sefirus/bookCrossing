namespace Core.Entities;

public class Author
{
    public int Id { get; set; } 
    public string FullName  { get; set; } 
    public IEnumerable<Picture> Pictures { get; set; } 
    public string Description { get; set; } 
    public IEnumerable<Book> Books { get; set; } 
}