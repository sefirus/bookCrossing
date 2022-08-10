using Core.ViewModels.BookViewModels.GoogleBookApiRequests;

namespace Core.Exceptions;

public class VolumeIncompleteException : Exception
{
    public IEnumerable<string> MissingProperties { get; set; } 
    public VolumeViewModel Volume { get; set; }

    public VolumeIncompleteException(IEnumerable<string> missingProperties, VolumeViewModel volume)
    {
        MissingProperties = missingProperties;
        Volume = volume;
    }
}