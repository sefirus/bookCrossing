namespace Core.Entities;

public class Picture
{
    public Guid Id { get; set; }
    public string FullPath { get; set; }
    //Nav properties
    public int? WriterId { get; set; } = null;
    public Writer? Writer { get; set; }
    public int? BookId { get; set; } = null;
    public Book? Book { get; set; }
    public int? PublisherId { get; set; } = null;
    public Publisher? Publisher { get; set; }
    public int? ShelfId { get; set; } = null;
    public Shelf? Shelf { get; set; }
    public User? User { get; set; }
}