using Core.ViewModels.BookViewModels.GoogleBookApiRequests;

namespace Core.ViewModels.ExceptionViewModels;

public class VolumeIncompleteExceptionViewModel
{
    public IEnumerable<string> MissingProperties { get; set; } 
    public VolumeViewModel Volume { get; set; }
    
}