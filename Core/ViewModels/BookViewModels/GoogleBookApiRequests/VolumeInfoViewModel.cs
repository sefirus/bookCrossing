namespace Core.ViewModels.BookViewModels.GoogleBookApiRequests;

public class VolumeInfoViewModel
{
    public string Title { get; set; } = string.Empty;
    public IEnumerable<string> Authors { get; set; } = new List<string>();
    public string Publisher { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public IEnumerable<IndustryIdentifier> IndustryIdentifiers { get; set; }
        = new List<IndustryIdentifier>();
    public int PageCount { get; set; }
    public IEnumerable<string> Categories { get; set; } = new List<string>();
    public ImageLinksViewModel? ImageLinks { get; set; }
    public string Language { get; set; } = string.Empty;
}