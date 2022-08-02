namespace Core.ViewModels;

public class PagedViewModel<T>
{
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Entities { get; set; }
}