namespace Core.Entities;

public class Picture
{
    public Guid Id { get; set; }
    public string FullPath { get; set; }
    //Nav properties
    public int WriterId { get; set; }
    public Writer? Writer { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public int PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    public int ShelfId { get; set; }
    public Shelf? Shelf { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}