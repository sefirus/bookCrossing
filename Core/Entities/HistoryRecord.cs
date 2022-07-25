namespace Core.Entities;

public class HistoryRecord
{
    public int Id { get; set; }
    public HistoryRecordType Type { get; set; }
    public BookCopy BookCopy { get; set; }
    public int BookCopyId { get; set; }
    public DateTime Time { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int ShelfId { get; set; }
    public Shelf? Shelf { get; set; }
}