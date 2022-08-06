namespace Core.ViewModels.CategoryViewModels;

public class ReadCategoryViewModel : CategoryVmBase
{
    public int Id { get; set; }
    public IEnumerable<ReadCategoryViewModel> ChildCategories { get; set; }
        = new List<ReadCategoryViewModel>();
}