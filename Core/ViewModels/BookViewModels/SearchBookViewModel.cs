using Core.Enums;

namespace Core.ViewModels.BookViewModels;

public class SearchBookViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Authors { get; set; } = string.Empty;
    public string? ThumbnailLink { get; set; } = string.Empty;
    public SearchResultType SearchResultType { get; set; } = SearchResultType.GoogleBookApi;
}