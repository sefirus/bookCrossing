namespace Core.Entities;

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Picture> Pictures { get; set; }
    public string Description { get; set; }
    public IEnumerable<Book> Books { get; set; }
}