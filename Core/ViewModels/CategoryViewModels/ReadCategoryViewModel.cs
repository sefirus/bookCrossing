namespace Core.ViewModels.CategoryViewModels;

public class ReadCategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<ReadCategoryViewModel> ChildCategories { get; set; }
        = new List<ReadCategoryViewModel>();
}