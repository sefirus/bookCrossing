namespace Core.Entities;

public class BookCopy 
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; } 
    public BookCopyState State { get; set; }
    public User CurrentUserId { get; set; } 
    public User CurrentUser { get; set; } 
    public int CurrentShelfId { get; set; } 
    public Shelf CurrentShelf { get; set; } 
    public IEnumerable<HistoryRecord> HistoryRecords { get; set; } 
    public IEnumerable<Comment> Comments { get; set; }
}