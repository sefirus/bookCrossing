namespace Core.ViewModels.BookViewModels.GoogleApiRequests;

public class VolumeInfoViewModel
{
    public string Title { get; set; } = string.Empty;
    public IEnumerable<string> Authors { get; set; } = new List<string>();
    public ImageLinksViewModel ImageLinks { get; set; }
}