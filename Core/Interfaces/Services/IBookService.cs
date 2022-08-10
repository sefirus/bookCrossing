using Core.ViewModels.BookViewModels;

namespace Core.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<SearchBookViewModel>> SearchBookAsync(string request);
    Task AddBookToLibraryAsync(SearchBookViewModel viewModel);
}