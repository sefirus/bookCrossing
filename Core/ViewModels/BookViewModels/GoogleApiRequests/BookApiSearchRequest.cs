namespace Core.ViewModels.BookViewModels.GoogleApiRequests;

public class BookApiSearchRequest
{ 
    public int TotalItems { get; set; }
    public IEnumerable<VolumeViewModel> Items { get; set; } 
}