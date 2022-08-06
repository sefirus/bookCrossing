using Core.ViewModels.BookViewModels;

namespace Core.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<SearchBookViewModel>> GetBookSearchResultsAsync(string request);
}