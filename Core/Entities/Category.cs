namespace Core.Entities;

public class Category
{
    public int Id { get; set; } 
    public string Name { get; set; } 
    public string Description { get; set; }
    public IEnumerable<Book> Books { get; set; } 
        = new List<Book>();
    public int ParentCategoryId { get; set; }
    public Category PrentCategory { get; set; }
    public IEnumerable<Category> ChildCategories { get; set; } 
        = new List<Category>();

}