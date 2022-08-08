namespace Core.ViewModels.BookViewModels.GoogleBookApiRequests;

public class BookApiSearchResponse
{ 
    public int TotalItems { get; set; }
    public IEnumerable<VolumeViewModel> Items { get; set; } 
}