namespace Core.ViewModels.CategoryViewModels;

public class CreateCategoryViewModel
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public int? ParentCategoryId { get; set; }
}