namespace Core.Entities;

public class Shelf 
{
    public int Id { get; set; }
    public IEnumerable<Picture> Picture { get; set; }
    public string Adress { get; set; }
    public IEnumerable<BookCopy> Books { get; set; } 
    public IEnumerable<BookCopy> ReservedBooks { get; set; }
    public IEnumerable<HistoryRecord> HistoryRecords { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}